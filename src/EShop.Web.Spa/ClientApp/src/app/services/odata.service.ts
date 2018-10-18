import { Injectable } from '@angular/core';
import { IPagingService } from './IPagingService';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OdataService<T> {

  constructor(private pagginService: IPagingService<T>) { }

  getPaggedData(
    filterField: string,
    filter: string = '',
    pageIndex: number = 0,
    pageSize: number = 5,
    sortField: string = '',
    sortDirection: string = ''): Observable<T> {

    let params = new HttpParams()
      .set('$skip', (pageIndex * pageSize).toString())
      .set('$top', pageSize.toString())
      .set('$orderby', `${sortField} ${sortDirection}`)
      .set('$filter', `contains(tolower(${filterField}),'${filter.toLowerCase()}')`)
      .set('$count', 'true');

    return this.pagginService.getPaggedData(params);
  }
}
