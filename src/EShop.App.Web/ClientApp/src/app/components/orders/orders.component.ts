import { Component, OnInit, ViewChild, ComponentFactory, ComponentFactoryResolver } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order';
import { NotificationService } from 'ng2-notify-popup';
import { ButtonEventService } from './buttons/button-event.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
  providers: [NotificationService]
})
export class OrdersComponent implements OnInit {

  orders: Order[];
  subscriptions = new Subscription();
  

  order = {
    name: 'name'
  }

  constructor(
    private orderService: OrderService,
    private notify: NotificationService,
    private buttonEventService: ButtonEventService,
    private router: Router
  ) { 
    this.subscriptions.add(
      buttonEventService.onDeleteSource.subscribe(
        order => this.delete(order)
      )
    );
    this.subscriptions.add(
      buttonEventService.onInfoSource.subscribe(
        order => this.info(order)
      )
    );
    this.subscriptions.add(
      buttonEventService.onChangeStateSource.subscribe(
        order => this.changeState(order)
      )
    );
    this.subscriptions.add(
      buttonEventService.onEditSource.subscribe(
        order => this.edit(order)
      )
    );
  }

  ngOnInit() {
    this.getOrders();
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  delete(order: Order) {
    this.orderService.deleteOrder(order.orderId).subscribe(
      () => {
        this.show("Done!", "success");
        this.orders = this.orders.filter(o => o !== order);
      },
      error => {
        this.show(error, "error");
      }
    );
  }

  info(order: Order) {
    this.router.navigate([`spa/orders/${order.orderId}/details`]);
  }

  edit(order: Order) {
    this.router.navigate([`spa/orders/${order.orderId}`]);
  }

  changeState(order: Order) {
    this.orderService.changeState(order).subscribe(
      order => {
        this.orders = this.orders.map(o => {
          if(o.orderId === order.orderId)
            o = order;
          return o;
        });
        this.show("Done!", "success");
      },
      error => {
        this.show(error, "error");
      }
    );
  }

  getOrders() {
    this.orderService.getOrders().subscribe(
      orders => {
        this.orders = orders
      },
      error => this.show(error, "error")
    );
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
