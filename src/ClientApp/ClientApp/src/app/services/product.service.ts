import { Injectable, ErrorHandler, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { Product } from '../models/product';
import { catchError, map, tap } from 'rxjs/operators';
import { errorHandler } from './errorHandler';
import { ConfigService } from './config.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

let categoriesUrl = 'api/categories';
let productsUrl = 'api/products';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private errorHandler: errorHandler = new errorHandler();

  constructor(
    private http: HttpClient,
    private configService: ConfigService
    ) {
      productsUrl = this.configService.getApiUrl().concat(productsUrl);
      categoriesUrl = this.configService.getApiUrl().concat(categoriesUrl);
    }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${productsUrl}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  };

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${productsUrl}/${id}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(productsUrl, product, httpOptions).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.patch<Product>(`${productsUrl}/${product.productId}`, product, httpOptions).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  deleteProduct(id: number): Observable<Product> {
    return this.http.delete<Product>(`${productsUrl}/${id}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  getProductsByCategoryId(id: number): Observable<Product[]> {
    return this.http.get<Product[]>(`${categoriesUrl}/${id}/products`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
