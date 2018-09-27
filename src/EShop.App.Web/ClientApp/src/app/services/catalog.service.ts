import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { CategoryNode } from '../components/catalog/category-tree/category-node';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

const catalogUrl = "/api/catalog"

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  constructor(private http: HttpClient) { }
  getCategoryNodesByParentId(id: number): Observable<CategoryNode[]> {
    return this.http.get<CategoryNode[]>(`${catalogUrl}/category-nodes/${id}`).pipe(
      tap(data => console.dir(data))
    )
  }
}
