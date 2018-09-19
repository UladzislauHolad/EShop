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
