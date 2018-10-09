import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductOrder } from '../models/productOrder';
import { catchError } from 'rxjs/operators';
import { errorHandler } from './errorHandler';
import { ConfigService } from './config.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
let ordersUrl = 'api/orders'

@Injectable({
  providedIn: 'root'
})
export class ProductOrderService {

  private errorHandler: errorHandler = new errorHandler();

  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {
    ordersUrl = this.configService.getApiUrl().concat(ordersUrl);
  }

  getProductOrders(orderId: number): Observable<ProductOrder[]> {
    return this.http.get<ProductOrder[]>(`${ordersUrl}/${orderId}/products`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  createProductOrder(productOrder: ProductOrder): Observable<ProductOrder> {
    return this.http.post<ProductOrder>(`${ordersUrl}/${productOrder.orderId}/products`, productOrder, httpOptions).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  deleteProductOrder(orderId: number, id: number): Observable<ProductOrder> {
    return this.http.delete<ProductOrder>(`${ordersUrl}/${orderId}/products/${id}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  updateProductOrder(orderId: number, productOrder: ProductOrder): Observable<ProductOrder> {
    return this.http.patch<ProductOrder>(`${ordersUrl}/${orderId}/products/${productOrder.productOrderId}`,
      productOrder, httpOptions).pipe(
        catchError(this.errorHandler.handleError)
      )
  }
}
