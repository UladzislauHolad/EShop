import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { NgHttpLoaderModule } from 'ng-http-loader';
import { NgNotifyPopup } from 'ng2-notify-popup';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';

import { AppComponent } from './app.component';
import { ProductsComponent } from './components/products/products.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CreateProductComponent } from './components/products/create-product/create-product.component';

import { AppRoutingModule } from './/app-routing.module';
import { EditProductComponent } from './components/products/edit-product/edit-product.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { CreateCategoryComponent } from './components/categories/create-category/create-category.component';
import { EditCategoryComponent } from './components/categories/edit-category/edit-category.component';
import { CategoryMultiselectComponent } from './components/products/category-multiselect/category-multiselect.component';
import { ProductFormComponent } from './components/products/product-form/product-form.component';

/** IMPORTANT : IE10 and IE11 requires the following to support `@angular/animation` (which is used by this module).
Run `npm install --save web-animations-js`.
*/
import 'web-animations-js';
import { CategoryFormComponent } from './components/categories/category-form/category-form.component';
import { OrdersComponent } from './components/orders/orders.component';
import { CreateOrderComponent } from './components/orders/create-order/create-order.component';
import { OrderFormComponent } from './components/orders/order-form/order-form.component';
import { ProductOrdersComponent } from './components/orders/order-form/product-orders/product-orders.component';  
import { ProductOrderFormComponent } from './components/orders/order-form/product-orders/product-order-form/product-order-form.component';
import { EditOrderComponent } from './components/orders/edit-order/edit-order.component';
import { CreateProductOrderComponent } from './components/orders/order-form/product-orders/create-product-order/create-product-order.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    NavbarComponent,
    CreateProductComponent,
    EditProductComponent,
    CategoriesComponent,
    CreateCategoryComponent,
    EditCategoryComponent,
    CategoryMultiselectComponent,
    ProductFormComponent,
    CategoryFormComponent,
    OrdersComponent,
    CreateOrderComponent,
    OrderFormComponent,
    ProductOrderFormComponent,
    ProductOrdersComponent,
    EditOrderComponent,
    CreateProductOrderComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule,
    NgHttpLoaderModule,
    NgNotifyPopup,
    SweetAlert2Module.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
