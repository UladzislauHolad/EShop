import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
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

  processing: boolean;

  constructor(
    private location: Location,
    private productService: ProductService
  ) { }

  ngOnInit() {
    this.processing = false;
  }

  onSubmit(product: Product) {
    this.processing = true;
    this.productService.createProduct(product).subscribe(
      () => {
          this.goBack()
      },
      error => {
        this.processing = false;
      }
    );
  }

  goBack() {    
    this.location.back();
  }
}
