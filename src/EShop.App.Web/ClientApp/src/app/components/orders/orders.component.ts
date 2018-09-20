import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order';
import { NotificationService } from 'ng2-notify-popup';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
  providers: [NotificationService]
})
export class OrdersComponent implements OnInit {

  orders: Order[];

  constructor(
    private orderService: OrderService,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrders().subscribe(
      orders => {
        this.orders = orders,
        console.dir(this.orders);
      },
      error => this.show(error, "error")
    );
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
