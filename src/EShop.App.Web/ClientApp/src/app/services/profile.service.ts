import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profile } from '../models/profile';

const profileUrl = 'api/profiles';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(
    private http: HttpClient
  ) { }

  getProfile(id: string): Observable<Profile> {
    return this.http.get<Profile>(`${profileUrl}/${id}`);
  }
}
