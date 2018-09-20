import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { errorHandler } from './errorHandler';
import { DeliveryMethod } from '../models/deliveryMethod';

const deliveriesUrl = '/api/deliveries';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {
  private errorHandler: errorHandler = new errorHandler();

  constructor(private http: HttpClient) { }

  getDeliveries(): Observable<DeliveryMethod[]> {
    return this.http.get<DeliveryMethod[]>(deliveriesUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  };
}
