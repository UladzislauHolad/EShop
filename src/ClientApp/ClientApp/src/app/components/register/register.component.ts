import { Component, OnInit } from '@angular/core';
import { FormGroup, AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { User } from 'src/app/models/user';
import { AuthenticationService } from '../../services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../../services/alert.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  returnUrl: string;
  user = new User();
  registerForm: FormGroup;
  userName: AbstractControl;
  email: AbstractControl;
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
    this.createForm();
  }

  onSubmit(user: User) {
    // this.authenticationService.register(user)
    // .pipe(first())
    // .subscribe(
    //   data => {
    //       this.alertService.success('Registration successful', true);
    //       this.router.navigate(['spa/login']);
    //   },
    //   error => {
    //       this.serverErrors = error.errors;
    //   }
    // );
  }
  createForm() {
    this.registerForm = this.formBuilder.group({
      'userName': [
        this.user.userName,
        [
          Validators.required
        ]
      ],
      'email': [
        this.user.email,
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

    this.userName = this.registerForm.controls['userName'];
    this.email = this.registerForm.controls['email'];
    this.password = this.registerForm.controls['password'];
  }
}
