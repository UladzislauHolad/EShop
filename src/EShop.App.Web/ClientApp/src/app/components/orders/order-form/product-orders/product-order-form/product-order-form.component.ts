import { Component, OnInit, Input } from '@angular/core';
import { ProductOrder } from '../../../../../models/productOrder';
import { ProductService } from '../../../../../services/product.service';
import { ProductOrderService } from '../../../../../services/product-order.service';
import { Product } from '../../../../../models/product';

@Component({
  selector: 'app-product-order-form',
  templateUrl: './product-order-form.component.html',
  styleUrls: ['./product-order-form.component.css']
})
export class ProductOrderFormComponent implements OnInit {

  @Input() productOrder: ProductOrder;
  products: Product[];
  selectedProduct: Product;

  constructor(
    private productService: ProductService,
    private productOrderService: ProductOrderService
  ) { }

  ngOnInit() {
  }

}
