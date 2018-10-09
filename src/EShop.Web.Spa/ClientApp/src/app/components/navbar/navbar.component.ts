import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
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
    private authenticationService: AuthenticationService,
    public oidcSecurityService: OidcSecurityService
  ) { }

  ngOnInit() {
    this.oidcSecurityService.getIsAuthorized().subscribe(
      isLoggedIn => {
        this.isLoggedIn = isLoggedIn;
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
