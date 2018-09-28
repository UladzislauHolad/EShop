import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { CategoryNestedNode } from '../components/catalog/category-tree/category-nested-node';

const catalogUrl = "/api/catalog"

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  constructor(private http: HttpClient) { }
  
  getCategoryNestedNodes(): Observable<CategoryNestedNode[]> {
    return this.http.get<CategoryNestedNode[]>(`${catalogUrl}/category-nodes`).pipe(
      tap(data => console.dir(data))
    )
  }
}
