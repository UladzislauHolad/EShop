import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order';
import { ButtonEventService } from './buttons/button-event.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {

  orders: Order[];
  subscriptions = new Subscription();
  

  order = {
    name: 'name'
  }

  constructor(
    private orderService: OrderService,
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
        this.getOrders();
      },
      error => {
      }
    );
  }

  info(order: Order) {
    this.router.navigate([`orders/${order.orderId}/details`]);
  }

  edit(order: Order) {
    this.router.navigate([`orders/${order.orderId}`]);
  }

  changeState(order: Order) {
    this.orderService.changeState(order).subscribe(
      () => {
        this.getOrders();
      },
      error => {
      }
    );
  }

  getOrders() {
    this.orderService.getOrders().subscribe(
      orders => {
        this.orders = orders
      },
    );
  }
}
