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
import { CatalogComponent } from './components/catalog/catalog.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

const spa = 'spa';

const routes: Routes = [
  { path: spa + '', redirectTo: spa + '/products', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: spa + '/products', component: ProductsComponent, canActivate: [AuthGuard] },
  { path: spa + '/products/new', component: CreateProductComponent, canActivate: [AuthGuard] },
  { path: spa + '/products/:id', component: EditProductComponent, canActivate: [AuthGuard] },
  { path: spa + '/categories', component: CategoriesComponent, canActivate: [AuthGuard] },
  { path: spa + '/categories/new', component: CreateCategoryComponent, canActivate: [AuthGuard] },
  { path: spa + '/categories/:id', component: EditCategoryComponent, canActivate: [AuthGuard] },
  { path: spa + '/orders', component: OrdersComponent, canActivate: [AuthGuard] },
  { path: spa + '/orders/new', component: CreateOrderComponent, canActivate: [AuthGuard] },
  { path: spa + '/orders/:id', component: EditOrderComponent, canActivate: [AuthGuard] },
  { path: spa + '/orders/:id/details', component: OrderInfoComponent, canActivate: [AuthGuard] },
  { path: spa + '/orders/:id/products/new', component: CreateProductOrderComponent, canActivate: [AuthGuard] },
  { path: spa + '/dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: spa + '/catalog', component: CatalogComponent, canActivate: [AuthGuard] },
  { path: spa + '/login', component: LoginComponent },  
  { path: spa + '/register', component: RegisterComponent },  
  { path: spa + '/profile', component: UserProfileComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
