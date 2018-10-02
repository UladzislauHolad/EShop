import { Component, OnInit } from '@angular/core';
import { FormGroup, AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { User } from '../../models/user';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  returnUrl: string;
  user = new User();
  loginForm: FormGroup;
  userName: AbstractControl;
  password: AbstractControl;
  serverErrors: string[];

  constructor(
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.createForm()
    // reset login status
    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/spa';
  }

  onSubmit(user: User) {
    this.authenticationService.login(user.userName, user.password)
      .pipe(first())
      .subscribe(
        data => {
            this.router.navigate([this.returnUrl]);
        },
        error => {
          this.serverErrors = error.errors;
      });
  }

  createForm() {
    this.loginForm = this.formBuilder.group({
      'userName': [
        this.user.userName,
        [
          Validators.required
        ]
      ],
      'password': [
        this.user.password,
        [
          Validators.required
        ]
      ]
    });

    this.userName = this.loginForm.controls['userName'];
    this.password = this.loginForm.controls['password'];
  }
}
