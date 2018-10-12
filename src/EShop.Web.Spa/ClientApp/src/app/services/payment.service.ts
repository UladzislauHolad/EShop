import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PaymentMethod } from '../models/paymentMethod';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { errorHandler } from './errorHandler';
import { ConfigService } from './config.service';

let paymentsUrl = 'api/payments';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  private errorHandler: errorHandler = new errorHandler();
 
  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {
    paymentsUrl = this.configService.getApiUrl().concat(paymentsUrl);
  }

  getPayments(): Observable<PaymentMethod[]> {
    return this.http.get<PaymentMethod[]>(paymentsUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  };
}
