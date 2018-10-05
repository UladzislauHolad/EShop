import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ProductOrder } from '../../../../../models/productOrder';
import { ProductService } from '../../../../../services/product.service';
import { ProductOrderService } from '../../../../../services/product-order.service';
import { Product } from '../../../../../models/product';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-order-form',
  templateUrl: './product-order-form.component.html',
  styleUrls: ['./product-order-form.component.css']
})
export class ProductOrderFormComponent implements OnInit {

  @Output() onFormSubmit = new EventEmitter<ProductOrder>();
  @Input() orderId: number;
  @Input() oldOrderCount: number;
  @Input() product: Product;

  orderCount: number;

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
    this.orderCount = this.oldOrderCount;
  }

  onSubmit(productOrder: ProductOrder) {
    this.onFormSubmit.emit(productOrder);
  }

  goBack() {
    this.router.navigate([`spa/orders/${this.orderId}`]);
  }
}
