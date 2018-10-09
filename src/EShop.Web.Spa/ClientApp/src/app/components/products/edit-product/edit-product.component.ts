import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
})
export class EditProductComponent implements OnInit {

  product: Product;
  processing: boolean;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private productService: ProductService
  ) { }

  ngOnInit() {
    this.getProduct();
    this.processing = false;
  }

  getProduct(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.productService.getProduct(id)
      .subscribe(product => {
        this.product = product;
      });
  }

  onSubmit(product: Product): void {
    this.processing = true;
    this.productService.updateProduct(product).subscribe(
      () => {
          this.location.back();
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
