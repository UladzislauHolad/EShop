import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';
import { NotificationService } from 'ng2-notify-popup';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css'],
  providers: [NotificationService]
})
export class CreateProductComponent implements OnInit {

  product: Product = {
    productId: 0,
    name: '',
    price: 0,
    description: '',
    count: 0,
    categories: []
  };

  constructor(
    private location: Location,
    private productService: ProductService,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    console.dir(this.product);
  }

  onSubmit(product: Product) {
    this.productService.createProduct(product).subscribe(
      () => {
        this.show("Done! You will be redirected to the previous page", "success");
        setTimeout(() => {
          this.goBack()
        }, 3000);
      },
      error => {
        this.show(error, "error");
      }
    );
  }

  goBack() {    
    this.location.back();
  }
   // to append in body
  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
