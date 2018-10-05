import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserTableInfo } from '../models/user-table-info';

const usersUrl = 'api/users'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient
  ) { }

  getUsers(): Observable<UserTableInfo[]> {
    return this.http.get<UserTableInfo[]>(`${usersUrl}`);
  }

  deleteUser(userName: string) {
    return this.http.delete(`${usersUrl}/${userName}`);
  }
}
