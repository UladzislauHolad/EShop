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

  msgVisible: boolean;
  msg: string;
  msgHeader: string;
  msgClass: string;
  
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
    private productService: ProductService
  ) { }

  ngOnInit() {
    console.dir(this.product);
    this.msgVisible = false;
  }

  onSubmit(product: Product) {
    console.dir(product);
    console.log("submit");
    this.productService.createProduct(product).subscribe(
      () => {
        this.buildMsg("Success!", "positive", "Done! You will be redirected to the previous page");
        setTimeout(() => {
          this.goBack()
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
