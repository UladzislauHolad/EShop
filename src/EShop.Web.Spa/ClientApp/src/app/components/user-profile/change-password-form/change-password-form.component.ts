import { Component, OnInit } from '@angular/core';
import { FormGroup, AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ChangePasswordModel } from 'src/app/models/change-password.-model';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-change-password-form',
  templateUrl: './change-password-form.component.html',
  styleUrls: ['./change-password-form.component.css']
})
export class ChangePasswordFormComponent implements OnInit {

  changePasswordModel = new ChangePasswordModel;
  myForm: FormGroup;
  oldPassword: AbstractControl;
  newPassword: AbstractControl;
  confirmPassword: AbstractControl;
  hide = true;
  username: string;
  

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private profileService: ProfileService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.createForm();
    this.getProfile();
  }

  getProfile() {
    this.profileService.getProfile().subscribe(
      profile => this.username = profile.name
    )
  }

  createForm() {
    this.myForm = this.formBuilder.group({
      'oldPassword': [
        '',
        Validators.required
      ],
      'newPassword': [
        '',
        Validators.required
      ],
      'confirmPassword': [
        '',
        [
          Validators.required
        ]
      ]
    });

    this.oldPassword = this.myForm.controls['oldPassword'];
    this.newPassword = this.myForm.controls['newPassword'];
    this.confirmPassword = this.myForm.controls['confirmPassword'];
  }

  onSubmit() {
    this.changePasswordModel.oldPassword = this.oldPassword.value;
    this.changePasswordModel.newPassword = this.newPassword.value;

    this.userService.changePassword(this.username, this.changePasswordModel)
      .subscribe(
        () => {
          this.router.navigate(['profile'])
        }
      )
  }
}
