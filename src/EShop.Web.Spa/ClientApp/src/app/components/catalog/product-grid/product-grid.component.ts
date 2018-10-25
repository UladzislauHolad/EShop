import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SideNavEventService } from '../sidenav/sidenav-event.service';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-product-grid',
  templateUrl: './product-grid.component.html',
  styleUrls: ['./product-grid.component.css']
})
export class ProductGridComponent implements OnInit {

  subscription: Subscription;
  products: Product[];

  constructor(
    private sidenavEventService: SideNavEventService,
    private productService: ProductService
  ) { }

  ngOnInit() {
    this.productService.getProducts().pipe(
      map(odataProducts => {
        return (odataProducts as any).value;
      })
    ).subscribe(products => this.products = products)
    
    this.subscription = new Subscription();
    this.subscription.add(this.sidenavEventService.onChoose$.subscribe(
      id => this.getProductsByCategoryId(id)
    ));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getProductsByCategoryId(id: number) {
    this.productService.getProductsByCategoryId(id).subscribe(
      products => this.products = products
    );
  }
}
