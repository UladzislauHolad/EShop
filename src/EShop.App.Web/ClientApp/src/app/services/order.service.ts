import { Injectable } from '@angular/core';
import { errorHandler } from './errorHandler';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Order } from '../models/order';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const ordersUrl = '/api/orders';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private errorHandler: errorHandler = new errorHandler();

  constructor(
    private http: HttpClient
  ) { }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(ordersUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
