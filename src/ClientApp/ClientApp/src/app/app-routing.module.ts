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
import { UsersComponent } from './components/users/users.component';
import { AutoLoginComponent } from './auto-login/auto-login.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';

const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full'},
  { path: 'products', component: ProductsComponent },
  { path: 'products/new', component: CreateProductComponent, canActivate: [AuthGuard] },
  { path: 'products/:id', component: EditProductComponent, canActivate: [AuthGuard] },
  { path: 'categories', component: CategoriesComponent, canActivate: [AuthGuard] },
  { path: 'categories/new', component: CreateCategoryComponent, canActivate: [AuthGuard] },
  { path: 'categories/:id', component: EditCategoryComponent, canActivate: [AuthGuard] },
  { path: 'orders', component: OrdersComponent, canActivate: [AuthGuard] },
  { path: 'orders/new', component: CreateOrderComponent, canActivate: [AuthGuard] },
  { path: 'orders/:id', component: EditOrderComponent, canActivate: [AuthGuard] },
  { path: 'orders/:id/details', component: OrderInfoComponent, canActivate: [AuthGuard] },
  { path: 'orders/:id/products/new', component: CreateProductOrderComponent, canActivate: [AuthGuard] },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'catalog', component: CatalogComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },  
  { path: 'register', component: RegisterComponent },  
  { path: 'profile', component: UserProfileComponent, canActivate: [AuthGuard] },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: 'autologin', component: AutoLoginComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
