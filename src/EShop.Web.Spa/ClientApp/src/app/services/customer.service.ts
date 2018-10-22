import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../models/customer';
import { Observable } from 'rxjs';
import { ConfigService } from './config.service';

let customersUrl = 'api/customers';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(
    private http: HttpClient,
    private configService: ConfigService    
  ) {
    customersUrl = this.configService.getApiUrl().concat(customersUrl);
  }

  getCustomersToList(): Observable<Customer[]> {
    return this.http.get<Customer[]>(customersUrl);
  }
}
