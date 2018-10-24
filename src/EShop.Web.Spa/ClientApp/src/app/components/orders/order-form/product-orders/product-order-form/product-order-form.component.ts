import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ProductOrder } from '../../../../../models/productOrder';
import { Product } from '../../../../../models/product';
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
    this.router.navigate([`/orders/${this.orderId}`]);
  }
}
