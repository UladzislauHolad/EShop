import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { OidcSecurityService, AuthorizationResult } from 'angular-auth-oidc-client';
import { Router } from '@angular/router';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    constructor(
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

    ngOnInit() {
    }

    ngOnDestroy(): void {
        this.oidcSecurityService.onModuleSetup.unsubscribe();
    }

    login() {
        this.oidcSecurityService.authorize();
    }

    refreshSession() {
        console.log('start refreshSession');
        this.oidcSecurityService.authorize();
    }

    logout() {
        this.oidcSecurityService.logoff();
    }

    private onOidcModuleSetup() {
        if (window.location.hash) {
            this.oidcSecurityService.authorizedCallback();
        } else {
            this.write('redirect', window.location.pathname);
            this.oidcSecurityService.getIsAuthorized().subscribe((authorized: boolean) => {
                if (!authorized) {
                    this.router.navigate(['/callback']);
                }
            });
        }
    }

    private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {
        console.log('AppComponent:onAuthorizationResultComplete');
        const path = this.read('redirect');
        if (authorizationResult === AuthorizationResult.authorized) {
            this.router.navigate([path]);
        } else {
            this.router.navigate(['/callback']);
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
