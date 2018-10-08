import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { LogginEventService } from '../../services/loggin-event.service';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isLoggedIn: boolean;
  subscription = new Subscription();
  userName: string;

  constructor(
    private logginEventService: LogginEventService,
    private authenticationService: AuthenticationService,
    private router: Router
  ) { }

  ngOnInit() {
    this.isLoggedIn = this.logginEventService.currentState;
    this.getCurrentUser();

    this.subscription.add(this.logginEventService.$isLoggedIn.subscribe(
      isLoggedIn => {
        this.isLoggedIn = isLoggedIn;
        this.getCurrentUser()
      }
    ));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  login() {
    this.authenticationService.login();
  }

  logout() {
    this.authenticationService.logout();
  }

  getCurrentUser() {
    if(this.isLoggedIn)
      this.userName = JSON.parse(localStorage.getItem('currentUser')).userName;
  }
}
