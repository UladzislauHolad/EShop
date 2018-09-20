import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductOrder } from '../models/productOrder';
import { catchError } from 'rxjs/operators';
import { errorHandler } from './errorHandler';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const ordersUrl = 'api/orders'

@Injectable({
  providedIn: 'root'
})
export class ProductOrderService {

  private errorHandler: errorHandler = new errorHandler();

  constructor(private http: HttpClient) { }

  getProductOrders(orderId: number): Observable<ProductOrder[]> {
    return this.http.get<ProductOrder[]>(`${ordersUrl}/${orderId}/products`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
