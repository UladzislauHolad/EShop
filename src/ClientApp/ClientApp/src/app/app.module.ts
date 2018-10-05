import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { NgHttpLoaderModule } from 'ng-http-loader';
import { AppRoutingModule } from './/app-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { MatTreeModule, MatIconModule, MatProgressBarModule, MatButtonModule, MatSidenavModule, MatGridListModule } from '@angular/material';



import { AppComponent } from './app.component';
import { ProductsComponent } from './components/products/products.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CreateProductComponent } from './components/products/create-product/create-product.component';
import { EditProductComponent } from './components/products/edit-product/edit-product.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { CreateCategoryComponent } from './components/categories/create-category/create-category.component';
import { EditCategoryComponent } from './components/categories/edit-category/edit-category.component';
import { ProductFormComponent } from './components/products/product-form/product-form.component';
import { CategoryFormComponent } from './components/categories/category-form/category-form.component';
import { OrdersComponent } from './components/orders/orders.component';
import { CreateOrderComponent } from './components/orders/create-order/create-order.component';
import { OrderFormComponent } from './components/orders/order-form/order-form.component';
import { ProductOrdersComponent } from './components/orders/order-form/product-orders/product-orders.component';  
import { ProductOrderFormComponent } from './components/orders/order-form/product-orders/product-order-form/product-order-form.component';
import { EditOrderComponent } from './components/orders/edit-order/edit-order.component';
import { CreateProductOrderComponent } from './components/orders/order-form/product-orders/create-product-order/create-product-order.component';
import { StateDirective } from './components/orders/buttons/state.directive';
import { ButtonsComponent } from './components/orders/buttons/buttons.component';
import { CompletedStateComponent } from './components/orders/buttons/completed-state/completed-state.component';
import { OnDeliveringStateComponent } from './components/orders/buttons/on-delivering-state/on-delivering-state.component';
import { PackedStateComponent } from './components/orders/buttons/packed-state/packed-state.component';
import { PaidStateComponent } from './components/orders/buttons/paid-state/paid-state.component';
import { ConfirmedStateComponent } from './components/orders/buttons/confirmed-state/confirmed-state.component';
import { NewStateComponent } from './components/orders/buttons/new-state/new-state.component';
import { OrderInfoComponent } from './components/orders/order-info/order-info.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CategoryChartComponent } from './components/dashboard/category-chart/category-chart.component';
import { OrderLineChartComponent } from './components/dashboard/order-line-chart/order-line-chart.component';
import { CatalogComponent } from './components/catalog/catalog.component';
import { CategoryTreeComponent } from './components/catalog/category-tree/category-tree.component';
import { CdkTreeModule } from '@angular/cdk/tree';
import { SidenavComponent } from './components/catalog/sidenav/sidenav.component';
import { ProductGridComponent } from './components/catalog/product-grid/product-grid.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { JwtInterceptor } from 'src/app/helpers/jwt.interceptor';
import { ErrorInterceptor } from 'src/app/helpers/error.interceptor';
import { AlertComponent } from './directives/alert/alert.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UsersComponent } from './components/users/users.component';


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
    ProductFormComponent,
    CategoryFormComponent,
    OrdersComponent,
    CreateOrderComponent,
    OrderFormComponent,
    ProductOrderFormComponent,
    ProductOrdersComponent,
    EditOrderComponent,
    CreateProductOrderComponent,
    NewStateComponent,
    StateDirective,
    ButtonsComponent,
    ConfirmedStateComponent,
    PaidStateComponent,
    PackedStateComponent,
    OnDeliveringStateComponent,
    CompletedStateComponent,
    OrderInfoComponent,
    DashboardComponent,
    CategoryChartComponent,
    OrderLineChartComponent,
    CatalogComponent,
    CategoryTreeComponent,
    SidenavComponent,
    ProductGridComponent,
    LoginComponent,
    RegisterComponent,
    AlertComponent,
    UserProfileComponent,
    UsersComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule, 
    NgxChartsModule,
    HttpClientModule,
    AppRoutingModule,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule,
    NgHttpLoaderModule,
    MatTreeModule,
    CdkTreeModule,
    MatIconModule,
    MatProgressBarModule,
    MatButtonModule,
    MatSidenavModule,
    MatGridListModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    NewStateComponent,
    ConfirmedStateComponent,
    PaidStateComponent,
    PackedStateComponent,
    OnDeliveringStateComponent,
    CompletedStateComponent
  ]
})
export class AppModule { }
