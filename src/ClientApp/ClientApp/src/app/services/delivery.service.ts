import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { errorHandler } from './errorHandler';
import { DeliveryMethod } from '../models/deliveryMethod';
import { ConfigService } from './config.service';

let deliveriesUrl = '/api/deliveries';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {
  private errorHandler: errorHandler = new errorHandler();

  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {
    deliveriesUrl = this.configService.getApiUrl().concat(deliveriesUrl);
  }

  getDeliveries(): Observable<DeliveryMethod[]> {
    return this.http.get<DeliveryMethod[]>(deliveriesUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  };
}
