import { Component, OnInit } from '@angular/core';
import { Order } from '../../../models/order';
import { NotificationService } from 'ng2-notify-popup';
import { Location } from '@angular/common';
import { OrderService } from '../../../services/order.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css'],
  providers: [NotificationService]
})
export class CreateOrderComponent implements OnInit {

  order: Order = {
    orderId: 0,
    status: 'OnCreating',
    date: new Date(),
    comment: '',
    customerId: 0,
    customer: {
      customerId: 0,
      firstName: '',
      lastName: '',
      patronymic: '',
      phone: '',
      address: '',
    },
    paymentMethodId: null,
    paymentMethodName: null,
    deliveryMethodName: null,
    deliveryMethodId: null,
    pickupPointId: null,
    deliveryMethod: null,
    paymentMethod: null,
    pickupPoint: null,
    productOrders: null
  };

  processing: boolean;


  constructor(
    private orderService: OrderService,
    private location: Location,
    private notify: NotificationService,
    private router: Router    
  ) { }

  ngOnInit() {
    this.processing = false;
  }

  onSubmit(order: Order) {
    this.processing = true;
    this.createOrder(order);
  }

  createOrder(order: Order) {
    this.orderService.createOrder(order).subscribe(
      order => {
        this.show("Done!", "success");
        setTimeout(() => {
          this.router.navigate([`spa/orders/${order.orderId}`])
        }, 3000);
      },
      error => {
        this.show(error, "error");
        this.processing = false;
      }
    );
  }

  goBack() {
    this.location.back();
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
