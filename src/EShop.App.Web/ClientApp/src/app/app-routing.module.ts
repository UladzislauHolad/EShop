import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { CreateProductComponent } from './components/products/create-product/create-product.component';
import { EditProductComponent } from './components/products/edit-product/edit-product.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { CreateCategoryComponent } from './components/categories/create-category/create-category.component';
import { EditCategoryComponent } from './components/categories/edit-category/edit-category.component';
import { OrdersComponent } from './components/orders/orders.component';
import { CreateOrderComponent } from './components/orders/create-order/create-order.component';
import { EditOrderComponent } from './components/orders/edit-order/edit-order.component';
import { CreateProductOrderComponent } from './components/orders/order-form/product-orders/create-product-order/create-product-order.component';
import { OrderInfoComponent } from './components/orders/order-info/order-info.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const spa = 'spa';

const routes: Routes = [
  { path: spa + '', redirectTo: spa + '/products', pathMatch: 'full' },
  { path: spa + '/products', component: ProductsComponent },
  { path: spa + '/products/new', component: CreateProductComponent },
  { path: spa + '/products/:id', component: EditProductComponent },
  { path: spa + '/categories', component: CategoriesComponent },
  { path: spa + '/categories/new', component: CreateCategoryComponent },
  { path: spa + '/categories/:id', component: EditCategoryComponent },
  { path: spa + '/orders', component: OrdersComponent },
  { path: spa + '/orders/new', component: CreateOrderComponent },
  { path: spa + '/orders/:id', component: EditOrderComponent },
  { path: spa + '/orders/:id/details', component: OrderInfoComponent },
  { path: spa + '/orders/:id/products/new', component: CreateProductOrderComponent },
  { path: spa + '/dashboard', component: DashboardComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
