import { Injectable } from '@angular/core';
import { errorHandler } from './errorHandler';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

const categoriesUrl = 'api/categories-chart'

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private errorHandler: errorHandler = new errorHandler();

  constructor(private http: HttpClient) {
  }

  getCategoryChartInfo(): Observable<Object>{
    return this.http.get(categoriesUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
