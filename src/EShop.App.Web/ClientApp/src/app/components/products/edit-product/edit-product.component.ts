import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {

  msgVisible: boolean;
  msg: string;
  msgHeader: string;
  product: Product;
  msgClass: string;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private productService: ProductService,
  ) { }

  ngOnInit() {
    this.getProduct();
    this.msgVisible = false;
  }

  getProduct(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.productService.getProduct(id)
      .subscribe(product => {
        this.product = product;
      });
  }

  onSubmit(product: Product): void {
    console.log('You submited form: ', product);
    this.productService.updateProduct(product).subscribe(
      () => {
        this.buildMsg("Success!", "positive", "Product is updated!");
        setTimeout(() => {
          this.location.back();
        }, 3000);
      },
      error => {
        this.buildMsg("Error!", "negative", error);
        setTimeout(() => {
          this.closeMsg();
        }, 3000);
      }
    );
  }

  goBack() {
    this.location.back();
  }

  buildMsg(msgHeader: string, msgClass: string, msg: string)
  {
    this.msgHeader = msgHeader;
    this.msgClass = msgClass;
    this.msg = msg;
    this.msgVisible = true;
  }

  closeMsg() {
    this.msgVisible = false;
  }
}
