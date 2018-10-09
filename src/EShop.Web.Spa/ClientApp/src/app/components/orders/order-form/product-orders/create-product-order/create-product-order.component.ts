import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductOrder } from '../../../../../models/productOrder';
import { Product } from '../../../../../models/product';
import { ProductService } from '../../../../../services/product.service';
import { ProductOrderService } from '../../../../../services/product-order.service';
import { NotificationService } from 'ng2-notify-popup';

@Component({
  selector: 'app-create-product-order',
  templateUrl: './create-product-order.component.html',
  styleUrls: ['./create-product-order.component.css'],
  providers: [NotificationService]
})
export class CreateProductOrderComponent implements OnInit {

  products: Product[];
  product: Product;
  orderCount: 1;
  orderId = +this.route.snapshot.paramMap.get("id");

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private productOrderService: ProductOrderService,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    this.getProducts();
  }

  onSubmit(productOrder: ProductOrder) {
    console.log("create");
    console.dir(productOrder);
    this.createProductOrder(productOrder);
  }
  
  getProducts() {
    this.productService.getProducts().subscribe(
      products => {
        this.products = products,
        this.product = products[0]
      }
    );
  }

  createProductOrder(productOrder: ProductOrder) {
    this.productOrderService.createProductOrder(productOrder).subscribe(
      () => {
        this.show("Done!", "success");
        this.router.navigate([`orders/${this.orderId}`]);
      },
      error => this.show(error, "error")
    );
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
