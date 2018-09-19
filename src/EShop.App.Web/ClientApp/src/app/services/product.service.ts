import { Injectable, ErrorHandler } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { Product } from '../models/product';
import { catchError, map, tap } from 'rxjs/operators';
import { errorHandler } from './errorHandler';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class ProductService {

  private errorHandler: errorHandler = new errorHandler();

  private produtcsUrl = '/api/products';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.produtcsUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  };

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.produtcsUrl}/${id}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.produtcsUrl, product, httpOptions).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.patch<Product>(`${this.produtcsUrl}/${product.productId}`, product, httpOptions).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  deleteProduct(id: number): Observable<Product> {
    return this.http.delete<Product>(`${this.produtcsUrl}/${id}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
