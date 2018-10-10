import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isLoggedIn: boolean;
  subscription = new Subscription();
  userName: string;
  userData;

  constructor(
    private authenticationService: AuthenticationService,
    private oidcSecurityService: OidcSecurityService,
    private profileService: ProfileService
  ) { }

  ngOnInit() {
    this.oidcSecurityService.getIsAuthorized().subscribe(
      isLoggedIn => {
        this.isLoggedIn = isLoggedIn;
        
        if(isLoggedIn)
          this.getProfile();
      }
    );
  }

  getProfile() {
    this.profileService.getProfile().subscribe(
      profile => this.userName = profile.name
    )
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
