import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, catchError, tap } from 'rxjs/operators';
import { User } from '../models/user';
import { LogginEventService } from './loggin-event.service';
import { OidcSecurityService, OpenIDImplicitFlowConfiguration, AuthorizationResult } from 'angular-auth-oidc-client';
import { Router } from '@angular/router';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    constructor(
        private http: HttpClient,
        private loginEventService: LogginEventService,
        public oidcSecurityService: OidcSecurityService,
        private router: Router
    ) {
        if (this.oidcSecurityService.moduleSetup) {
            this.onOidcModuleSetup();
        } else {
            this.oidcSecurityService.onModuleSetup.subscribe(() => {
                this.onOidcModuleSetup();
            });
        }

        this.oidcSecurityService.onAuthorizationResult.subscribe(
            (authorizationResult: AuthorizationResult) => {
                this.onAuthorizationResultComplete(authorizationResult);
            });
    }

    ngOnDestroy(): void {
        this.oidcSecurityService.onModuleSetup.unsubscribe();
    }

    login() {
        console.log('start login');
        this.oidcSecurityService.authorize();
    }

    refreshSession() {
        console.log('start refreshSession');
        this.oidcSecurityService.authorize();
    }

    logout() {
        console.log('start logoff');
        this.oidcSecurityService.logoff();
    }

    private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {
        console.log('AppComponent:onAuthorizationResultComplete');
        const path = this.read('redirect');
        if (authorizationResult === AuthorizationResult.authorized) {
            this.router.navigate([path]);
        } else {
            this.router.navigate(['']);
        }
    }

    private onOidcModuleSetup() {
        if (window.location.hash) {
            this.oidcSecurityService.authorizedCallback();
        } else {
            if ('/autologin' !== window.location.pathname) {
                this.write('redirect', window.location.pathname);
            }
            console.log('AppComponent:onModuleSetup');
        }
    }

    private read(key: string): any {
        const data = localStorage.getItem(key);
        if (data != null) {
            return JSON.parse(data);
        }

        return;
    }

    private write(key: string, value: any): void {
        localStorage.setItem(key, JSON.stringify(value));
    }
}
