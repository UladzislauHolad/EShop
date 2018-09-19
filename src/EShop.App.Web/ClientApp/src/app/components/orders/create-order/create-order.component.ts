import { Component, OnInit } from '@angular/core';
import { Order } from '../../../models/order';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {

  order: Order = {
    orderId: 0,
    status: 'OnCreating',
    date: new Date(),
    comment: ''
  };

  constructor() { }

  ngOnInit() {
  }

}
