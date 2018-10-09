import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { LogginEventService } from '../../services/loggin-event.service';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';

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
    private router: Router,
    public oidcSecurityService: OidcSecurityService
  ) { }

  ngOnInit() {
    this.oidcSecurityService.getIsAuthorized().subscribe(
      isLoggedIn => {
        this.isLoggedIn = isLoggedIn;
        console.log("loggedIn");
        console.dir(this.isLoggedIn);
      }
    );
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
}
