import { Component, OnInit } from '@angular/core';
import { FormGroup, AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {

  myForm: FormGroup;
  userName: AbstractControl;
  email: AbstractControl;
  password: AbstractControl;
  errors: string[];
  user: User = {
    userName: '',
    email: '',
    password: ''
  };
  hide = true;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.myForm = this.formBuilder.group({
      'userName': [
        this.user.userName,
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(20)
        ]
      ],
      'email': [
        this.user.email,
        [
          Validators.required,
          Validators.email
        ]
      ],
      'password': [
        this.user.password,
        [
          Validators.required,
          Validators.minLength(8)
        ]
      ]
    });

    this.userName = this.myForm.controls['userName'];
    this.email = this.myForm.controls['email'];
    this.password = this.myForm.controls['password'];
  }

  onSubmit(user: User) {
    this.userService.createUser(user).subscribe(
      () => this.goBack(),
      error => this.errors = error.error
    );
  }

  goBack() {
    this.router.navigate(['users'])
  }
}
