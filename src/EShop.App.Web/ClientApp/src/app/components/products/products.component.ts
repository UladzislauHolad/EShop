import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { NotificationService } from 'ng2-notify-popup';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  providers: [NotificationService]
})
export class ProductsComponent implements OnInit {

  products: Product[];

  constructor(
    private notify: NotificationService,
    private productService: ProductService) { }

  ngOnInit() {
    this.productService.getProducts()
      .subscribe(products => this.products = products);
  }

  delete(product: Product){
    this.productService.deleteProduct(product.productId).subscribe(
      () => {
        this.show("Product is deleted!", "success");
        this.products = this.products.filter(p => p !== product);
      },
      error => {
        this.show(error, "error");
      }
    );
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
