import { Component, OnInit } from '@angular/core';
import { Profile } from 'src/app/models/profile';
import { ProfileService } from '../../services/profile.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  profile: Profile;

  constructor(
    private profileService: ProfileService
  ) { }

  ngOnInit() {
    this.profileService.getProfile().subscribe(
      profile => this.profile = profile
    );
  }

}
