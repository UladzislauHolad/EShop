import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class LogginEventService {
  private loginChange = new Subject<boolean>();

  $isLoggedIn = this.loginChange.asObservable();
  currentState: boolean;

  constructor() {
    const helper = new JwtHelperService();
    if (localStorage.getItem('currentUser') === null || JSON.parse(localStorage.getItem('currentUser')).token === null)
      this.currentState = false;
    else {
      const myRawToken = JSON.parse(localStorage.getItem('currentUser')).token;
      this.currentState = !helper.isTokenExpired(myRawToken);
    }
   
  }

  logIn() {
    this.currentState = true;
    this.loginChange.next(true);
  }

  logOut() {
    this.currentState = false;
    this.loginChange.next(false);
  }
}
