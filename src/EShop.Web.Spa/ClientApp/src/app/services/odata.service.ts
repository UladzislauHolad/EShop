import { Injectable } from '@angular/core';
import { IPagingService } from './IPagingService';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Filter } from '../helpers/filters/filter';

@Injectable({
  providedIn: 'root'
})
export class OdataService<T> {

  constructor(private pagingService: IPagingService<T>) { }

  getPaggedData(
    filters: Filter[],
    pageIndex: number = 0,
    pageSize: number = 5,
    sortField: string = '',
    sortDirection: string = ''): Observable<T> {
    
    console.dir(filters);

    let params = new HttpParams()
      .set('$skip', (pageIndex * pageSize).toString())
      .set('$top', pageSize.toString())
      .set('$orderby', `${sortField} ${sortDirection}`)
      .set('$count', 'true');

      params = this.setFilter(params, filters);

    return this.pagingService.getPaggedData(params);
  }

  setFilter(params: HttpParams, filters: Filter[]) : HttpParams {
    if(filters.length > 0) {
      let filter = filters.reduce((acc, cur, index) => {
        return index > 0 ? `${acc} and ${cur.toString()}` : `${cur.toString()}`;
      }, '');
      
      params = params.set('$filter', filter);
    }
    
    return params;
  }
}
