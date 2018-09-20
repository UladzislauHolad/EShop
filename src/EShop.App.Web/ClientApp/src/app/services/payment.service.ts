import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PaymentMethod } from '../models/paymentMethod';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { errorHandler } from './errorHandler';

const paymentsUrl = '/api/payments';


@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  private errorHandler: errorHandler = new errorHandler();

  constructor(private http: HttpClient) { }

  getPayments(): Observable<PaymentMethod[]> {
    return this.http.get<PaymentMethod[]>(paymentsUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  };
}
