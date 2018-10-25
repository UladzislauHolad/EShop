import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserTableInfo } from '../models/user-table-info';
import { User } from '../models/user';
import { ChangePasswordModel } from '../models/change-password.-model';

const usersUrl = 'http://localhost:5000/users'

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

  createUser(user: User): Observable<any> {
    return this.http.post(`${usersUrl}`, user)
  }

  changePassword(name: string, changePasswordModel: ChangePasswordModel) {
    return this.http.post(`${usersUrl}/${name}/password`, changePasswordModel);
  }
}
