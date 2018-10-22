import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Order } from '../../../models/order';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormControl } from '@angular/forms';
import { PaymentService } from '../../../services/payment.service';
import { PaymentMethod } from '../../../models/paymentMethod';
import { DeliveryService } from '../../../services/delivery.service';
import { DeliveryMethod } from '../../../models/deliveryMethod';
import { PickupService } from '../../../services/pickup.service';
import { PickupPoint } from '../../../models/pickupPoint';
import { Router } from '@angular/router';


@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css']
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
  ) { }

  ngOnInit() {
    this.getPayments();
    this.getDeliveries();
    this.getPickups();
    this.createForm(this.order);
  }

  onSubmit() {
    this.onFormSubmit.emit(this.order);
  }

  onDeliveryChange() {
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
  }

  createForm(order: Order) {
    this.myForm = this.formBuilder.group({
      'customerId': order.customer.customerId,
      'date': order.date,
      'status': order.status,
      'firstName': [
        order.customer.firstName,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(20)
        ]
      ],
      'lastName': [
        order.customer.lastName,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(20)
        ]
      ],
      'patronymic': [
        order.customer.patronymic,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(20)
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
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(50)
        ]
      ],
      'comment': [
        order.comment,
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(50)
        ]
      ],
      'payment': [
        order.paymentMethodId,
        [
          Validators.required
        ]
      ],
      'delivery': [
        order.deliveryMethodId,
        [
          Validators.required
        ]
      ],
      'pickup': [
        order.pickupPointId,
        [
          Validators.required
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
    );
  }

  getDeliveries() {
    this.deliveryService.getDeliveries().subscribe(
      deliveries => {
        this.existDeliveries = deliveries;
        this.delivery.setValue(deliveries.find(d => d.deliveryMethodId === this.order.deliveryMethodId));
      },
    );
  }

  getPickups() {
    this.pickupService.getPickups().subscribe(
      pickups => this.existPickups = pickups,
    );
  }

  goBack() {
    this.router.navigate(['orders']);
  }
}
