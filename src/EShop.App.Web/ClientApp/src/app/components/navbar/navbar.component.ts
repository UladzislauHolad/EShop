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

  constructor(
    private logginEventService: LogginEventService,
    private authenticationService: AuthenticationService,
    private router: Router
  ) { }

  ngOnInit() {
    this.isLoggedIn = this.logginEventService.currentState;
    this.subscription.add(this.logginEventService.$isLoggedIn.subscribe(
      isLoggedIn => this.isLoggedIn = isLoggedIn
    ));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['spa/login']);
  }
}
