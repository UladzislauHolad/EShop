import { Injectable } from '@angular/core';
import { IPagingService } from './IPagingService';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OdataService<T> {
  
  constructor(private pagginService: IPagingService<T>) {}

  getPaggedData(): Observable<T> {
    const params = new HttpParams()
    return this.pagginService.getPaggedData(params);
  }
}
