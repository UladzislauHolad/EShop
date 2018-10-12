import { Injectable } from '@angular/core';
import { errorHandler } from './errorHandler';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { LineItem } from '../components/dashboard/models/line-item';
import { ConfigService } from './config.service';

let categoriesUrl = 'api/categories/pie-chart';
let dashboardUrl = 'api/dashboard/';
const lineChartStates = new Array<string>(
  'New',
  'Confirmed',
  'Paid',
  'Packed',
  'OnDelivering',
  'Completed'
);


@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private errorHandler: errorHandler = new errorHandler();

  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {
    categoriesUrl = this.configService.getApiUrl().concat(categoriesUrl);
    dashboardUrl = this.configService.getApiUrl().concat(dashboardUrl);
  }

  getCategoryChartInfo(): Observable<Object> {
    return this.http.get(categoriesUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  getOrderLineChartInfo(): Observable<LineItem[]> {
    return this.http.get<LineItem[]>(`${dashboardUrl}/line/orders`, {
      params: {
        states: lineChartStates
      }
    }).pipe(
      map(list => list.map(item => {
        item.series = item.series.map(s => {
          s.name = new Date(s.name);
          return s;
        });
        return item;
      })
      ),
      catchError(this.errorHandler.handleError)
    );
  }
}
