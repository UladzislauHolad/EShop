import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor(private http: HttpClient) {
  }

  getApiUrl(): string {
    return localStorage.getItem('apiUrl');
  }

  setApiUrl(apiUrl: string) {
    localStorage.setItem('apiUrl', apiUrl);
  }
}
