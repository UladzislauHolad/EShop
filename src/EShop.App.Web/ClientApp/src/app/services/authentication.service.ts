import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, catchError, tap } from 'rxjs/operators';
import { User } from '../models/user';
import { Observable, pipe } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LogginEventService } from './loggin-event.service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    constructor(
        private http: HttpClient,
        private loginEventService: LogginEventService
    ) { }

    login(username: string, password: string) {
        return this.http.post<any>(`api/login`, { username, password }).pipe(
            map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.loginEventService.logIn();
                }

                return user;
            })
        );
    }

    logout() {
        return this.http.post(`api/logoff`,{j:"sd"}, httpOptions).pipe(
            tap(() => {
                localStorage.removeItem('currentUser');
                this.loginEventService.logOut();
            })
        )
        // remove user from local storage to log user out
        
    }

    register(user: User) {
        return this.http.post('api/register', user);
    }
}