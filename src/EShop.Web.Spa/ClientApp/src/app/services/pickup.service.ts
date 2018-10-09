import { Injectable } from '@angular/core';
import { errorHandler } from './errorHandler';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PickupPoint } from '../models/pickupPoint';
import { catchError } from 'rxjs/operators';
import { ConfigService } from './config.service';

let pickupsUrl = 'api/pickups'

@Injectable({
  providedIn: 'root'
})
export class PickupService {

  errorHandler: errorHandler = new errorHandler();

  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {
    pickupsUrl = this.configService.getApiUrl().concat(pickupsUrl);
  }

  getPickups(): Observable<PickupPoint[]> {
    return this.http.get<PickupPoint[]>(pickupsUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  };
}
