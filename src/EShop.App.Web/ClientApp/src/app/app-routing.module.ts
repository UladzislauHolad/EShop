import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { CreateProductComponent } from './components/products/create-product/create-product.component';
import { EditProductComponent } from './components/products/edit-product/edit-product.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { CreateCategoryComponent } from './components/categories/create-category/create-category.component';
import { EditCategoryComponent } from './components/categories/edit-category/edit-category.component';

const spa = 'spa';

const routes: Routes = [
  { path: spa + '', redirectTo: spa + '/products', pathMatch: 'full' },
  { path: spa + '/products', component: ProductsComponent },
  { path: spa + '/products/new', component: CreateProductComponent },
  { path: spa + '/products/:id', component: EditProductComponent },
  { path: spa + '/categories', component: CategoriesComponent },
  { path: spa + '/categories/new', component: CreateCategoryComponent },
  { path: spa + '/categories/:id', component: EditCategoryComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
