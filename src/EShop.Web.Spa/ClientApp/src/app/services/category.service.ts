import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category';
import { errorHandler } from './errorHandler';
import { catchError } from 'rxjs/operators';
import { ConfigService } from './config.service';

let categoriesUrl = 'api/categories';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})

export class CategoryService {

  private errorHandler: errorHandler = new errorHandler();

  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {
    categoriesUrl = this.configService.getApiUrl().concat(categoriesUrl);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(categoriesUrl).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  getCategory(id: number): Observable<Category> {
    return this.http.get<Category>(`${categoriesUrl}/${id}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  createCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(categoriesUrl, category, httpOptions).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  updateCategory(category: Category): Observable<Category> {
    return this.http.patch<Category>(`${categoriesUrl}/${category.categoryId}`, category, httpOptions).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  deleteCategory(id: number): Observable<Category> {
    return this.http.delete<Category>(`${categoriesUrl}/${id}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
