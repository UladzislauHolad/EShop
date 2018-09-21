import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Order } from '../../../models/order';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormControl } from '@angular/forms';
import { Location } from '@angular/common';
import { PaymentService } from '../../../services/payment.service';
import { PaymentMethod } from '../../../models/paymentMethod';
import { NotificationService } from 'ng2-notify-popup';
import { DeliveryService } from '../../../services/delivery.service';
import { DeliveryMethod } from '../../../models/deliveryMethod';
import { PickupService } from '../../../services/pickup.service';
import { PickupPoint } from '../../../models/pickupPoint';
import { Router } from '@angular/router';


@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css'],
  providers: [NotificationService]
})
export class OrderFormComponent implements OnInit {

  @Input() order: Order;
  @Input() processing: boolean;
  @Output() onFormSubmit = new EventEmitter<Order>();

  existPayments: PaymentMethod[];
  existDeliveries: DeliveryMethod[];
  existPickups: PickupPoint[];

  myForm: FormGroup;
  customerId: AbstractControl;
  firstName: AbstractControl;
  lastName: AbstractControl;
  patronymic: AbstractControl;
  phone: AbstractControl;
  address: AbstractControl;
  comment: AbstractControl;
  payment: AbstractControl;
  delivery: AbstractControl;
  pickup: AbstractControl;


  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private paymentService: PaymentService,
    private deliveryService: DeliveryService,
    private pickupService: PickupService,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    this.getPayments();
    this.getDeliveries();
    this.getPickups();
    this.createForm(this.order);
  }

  onSubmit() {
    console.dir(this.order);
    this.onFormSubmit.emit(this.order);
  }

  onDeliveryChange() {
    console.dir(this.delivery.value);
    console.dir(this.pickup.value);
    if(this.delivery.value !== null) {
      this.order.deliveryMethodId = this.delivery.value.deliveryMethodId;
      if(this.delivery.value.name === 'Pickup') {
        this.pickup.enable();
        this.order.pickupPointId = this.pickup.value;
      }
      else {
        this.pickup.disable();
        this.order.pickupPointId = null;
      }
    }
    else {
      this.pickup.disable();
      this.order.pickupPointId = null;
      this.order.deliveryMethodId = null;
    }

    console.dir(this.order);
  }

  createForm(order: Order) {
    this.myForm = this.formBuilder.group({
      'customerId': order.customer.customerId,
      'date': order.date,
      'status': order.status,
      'firstName': [
        order.customer.firstName,
        [
          Validators.required
        ]
      ],
      'lastName': [
        order.customer.lastName,
        [
          Validators.required
        ]
      ],
      'patronymic': [
        order.customer.patronymic,
        [
          Validators.required
        ]
      ],
      'phone': [
        order.customer.phone,
        [
          Validators.required
        ]
      ],
      'address': [
        order.customer.address,
        [
          Validators.required
        ]
      ],
      'comment': [
        order.comment,
        [
          Validators.required
        ]
      ],
      'payment': [
        order.paymentMethodId,
        [
          Validators.required
        ]
      ],
      'delivery': [
        ,
        [
          Validators.required
        ]
      ],
      'pickup': [
        order.pickupPointId,
        [
          Validators.required,
        ]
      ]
    });

    this.firstName = this.myForm.controls['firstName'];
    this.lastName = this.myForm.controls['lastName'];
    this.patronymic = this.myForm.controls['patronymic'];
    this.phone = this.myForm.controls['phone'];
    this.address = this.myForm.controls['address'];
    this.comment = this.myForm.controls['comment'];
    this.payment = this.myForm.controls['payment'];
    this.delivery = this.myForm.controls['delivery'];
    this.pickup = this.myForm.controls['pickup'];

    if(this.existDeliveries === null || this.order.deliveryMethodId === null)
      this.pickup.disable();
    else {
      this.pickup.disable();
    }
  }

  getPayments() {
    this.paymentService.getPayments().subscribe(
      payments => this.existPayments = payments,
      error => this.show(error, "error")
    );
  }

  getDeliveries() {
    this.deliveryService.getDeliveries().subscribe(
      deliveries => {
        this.existDeliveries = deliveries;
        this.delivery.setValue(deliveries.find(d => d.deliveryMethodId === this.order.deliveryMethodId));
      },
      error => this.show(error, "error")
    );
  }

  getPickups() {
    this.pickupService.getPickups().subscribe(
      pickups => this.existPickups = pickups,
      error => this.show(error, "error")
    );
  }

  goBack() {
    this.router.navigate(['spa/orders']);
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
