import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserTableInfo } from '../../models/user-table-info';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: UserTableInfo[];

  constructor(
    private userService: UserService
  ) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers().subscribe(
      users => this.users = users
    );
  }

  delete(userName: string) {
    this.userService.deleteUser(userName).subscribe(
      () => this.getUsers()
    );
  }
}
