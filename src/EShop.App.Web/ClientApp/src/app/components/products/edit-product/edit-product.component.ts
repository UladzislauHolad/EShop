import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from 'ng2-notify-popup';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
  providers: [NotificationService]
})
export class EditProductComponent implements OnInit {

  product: Product;
  processing: boolean;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private productService: ProductService,
    private notify: NotificationService
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
        this.show("Product is updated!", "success");
        setTimeout(() => {
          this.location.back();
        }, 3000);
      },
      error => {
        this.show(error, "error");
        this.processing = false;
      }
    );
  }

  goBack() {
    this.location.back();
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
