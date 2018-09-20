import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductOrder } from '../../../../../models/productOrder';

@Component({
  selector: 'app-create-product-order',
  templateUrl: './create-product-order.component.html',
  styleUrls: ['./create-product-order.component.css']
})
export class CreateProductOrderComponent implements OnInit {

  productOrder: ProductOrder = {
    productOrderId: 0,
    orderId: +this.route.snapshot.paramMap.get("id"),
    orderCount: 0,
    productId: 0,
    name: '',
    price: 0
  };

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
  }

}
