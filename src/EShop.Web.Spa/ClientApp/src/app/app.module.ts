import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { NgHttpLoaderModule } from 'ng-http-loader';
import { AppRoutingModule } from './/app-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { MatTreeModule, MatIconModule, MatProgressBarModule, MatButtonModule, MatSidenavModule, MatGridListModule, MatTableModule, MatPaginator, MatPaginatorModule, MatTableDataSource, MatSortModule, MatFormFieldModule, MatInputModule } from '@angular/material';
import { AuthModule, OidcSecurityService, OpenIDImplicitFlowConfiguration, AuthWellKnownEndpoints, OidcConfigService } from 'angular-auth-oidc-client';
import { ODataModule } from 'odata-v4-ng';

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
import { JwtInterceptor } from 'src/app/helpers/jwt.interceptor';
import { AlertComponent } from './directives/alert/alert.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UsersComponent } from './components/users/users.component';
import { AutoLoginComponent } from './auto-login/auto-login.component';
import { CallbackComponent } from './components/callback/callback.component';
import { ConfigService } from './services/config.service';
import { CreateUserComponent } from './components/users/create-user/create-user.component';

export function loadConfig(oidcConfigService: OidcConfigService) {
  console.log('APP_INITIALIZER STARTING');
  return () => oidcConfigService.load(`${window.location.origin}/api/config/configuration`);
}

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
    AlertComponent,
    UserProfileComponent,
    UsersComponent,
    AutoLoginComponent,
    CallbackComponent,
    CreateUserComponent
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
    MatGridListModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSortModule,
    MatInputModule,
    AuthModule.forRoot(),
    ODataModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    OidcSecurityService,
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: loadConfig,
      deps: [OidcConfigService],
      multi: true
    }
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
export class AppModule {
  constructor(
    private oidcSecurityService: OidcSecurityService,
    private oidcConfigService: OidcConfigService,
    private configService: ConfigService
  ) {
    this.oidcConfigService.onConfigurationLoaded.subscribe(() => {

      const authWellKnownEndpoints = new AuthWellKnownEndpoints();
      authWellKnownEndpoints.setWellKnownEndpoints(this.oidcConfigService.wellKnownEndpoints);

      const openIDImplicitFlowConfiguration = new OpenIDImplicitFlowConfiguration();
      openIDImplicitFlowConfiguration.stsServer = this.oidcConfigService.wellKnownEndpoints.issuer;
      openIDImplicitFlowConfiguration.redirect_url = this.oidcConfigService.clientConfiguration.redirect_url;
      // The Client MUST validate that the aud (audience) Claim contains its client_id value registered at the Issuer
      // identified by the iss (issuer) Claim as an audience.
      // The ID Token MUST be rejected if the ID Token does not list the Client as a valid audience,
      // or if it contains additional audiences not trusted by the Client.
      openIDImplicitFlowConfiguration.client_id = this.oidcConfigService.clientConfiguration.client_id;
      openIDImplicitFlowConfiguration.response_type = this.oidcConfigService.clientConfiguration.response_type;
      openIDImplicitFlowConfiguration.scope = this.oidcConfigService.clientConfiguration.scope;
      openIDImplicitFlowConfiguration.post_logout_redirect_uri = this.oidcConfigService.clientConfiguration.post_logout_redirect_uri;
      openIDImplicitFlowConfiguration.start_checksession = this.oidcConfigService.clientConfiguration.start_checksession;
      openIDImplicitFlowConfiguration.silent_renew = this.oidcConfigService.clientConfiguration.silent_renew;
      openIDImplicitFlowConfiguration.silent_renew_url = this.oidcConfigService.clientConfiguration.silent_renew_url;
      openIDImplicitFlowConfiguration.post_login_route = this.oidcConfigService.clientConfiguration.startup_route;
      // HTTP 403
      openIDImplicitFlowConfiguration.forbidden_route = this.oidcConfigService.clientConfiguration.forbidden_route;
      // HTTP 401
      openIDImplicitFlowConfiguration.unauthorized_route = this.oidcConfigService.clientConfiguration.unauthorized_route;
      openIDImplicitFlowConfiguration.auto_userinfo = this.oidcConfigService.clientConfiguration.auto_userinfo;
      openIDImplicitFlowConfiguration.log_console_warning_active = this.oidcConfigService.clientConfiguration.log_console_warning_active;
      openIDImplicitFlowConfiguration.log_console_debug_active = this.oidcConfigService.clientConfiguration.log_console_debug_active;
      // id_token C8: The iat Claim can be used to reject tokens that were issued too far away from the current time,
      // limiting the amount of time that nonces need to be stored to prevent attacks.The acceptable range is Client specific.
      openIDImplicitFlowConfiguration.max_id_token_iat_offset_allowed_in_seconds =
        this.oidcConfigService.clientConfiguration.max_id_token_iat_offset_allowed_in_seconds;
      this.oidcSecurityService.setupModule(openIDImplicitFlowConfiguration, authWellKnownEndpoints);
      this.oidcSecurityService.setCustomRequestParameters(this.oidcConfigService.clientConfiguration.additional_login_parameters);
      this.configService.setApiUrl(this.oidcConfigService.clientConfiguration.apiUrl);
    });
  }
}
