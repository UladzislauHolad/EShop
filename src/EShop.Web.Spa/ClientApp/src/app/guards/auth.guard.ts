import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { tap } from 'rxjs/operators';
import { Route } from '@angular/compiler/src/core';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(
      private router: Router,
      private oidcSecurityService: OidcSecurityService
    ) { }
  
    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
      console.log(route + '' + state);
      console.log('AuthorizationGuard, canActivate');
      console.log("waiting");

      setTimeout(null, 10000);
      return this.oidcSecurityService.getIsAuthorized().pipe(
        map((isAuthorized: boolean) => {
          console.log('AuthorizationGuard, canActivate isAuthorized: ' + isAuthorized);
  
          if (isAuthorized) {
            return true;
          }
  
          this.router.navigate(['/callback']);
          if (!window.location.hash) {
           console.log('AuthorizationGuard auto login');
           this.router.navigate(['/autologin']);
          }
  
          return false;
        })
      );
    }
  }
  