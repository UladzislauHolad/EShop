import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Order } from '../../../models/order';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Location } from '@angular/common';


@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
})
export class OrderFormComponent implements OnInit {

  @Input() order: Order;
  @Output() onFormSubmit = new EventEmitter<Order>();

  myForm: FormGroup;


  constructor(
    private formBuilder: FormBuilder,
    private location: Location,
  ) { }

  ngOnInit() {
    this.createForm(this.order);
  }

  onSubmit(order: Order) {
    this.onFormSubmit.emit(order);
  }

  createForm(order: Order) {
    this.myForm = this.formBuilder.group({
    });


  }

}
