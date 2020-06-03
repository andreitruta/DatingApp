import { Component, OnInit } from '@angular/core';
import { Locker } from 'src/app/_models/locker';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-member-list-lockers',
  templateUrl: './member-list-lockers.component.html',
  styleUrls: ['./member-list-lockers.component.css'],
})
export class MemberListLockersComponent implements OnInit {
  lockers: Locker[];

  constructor(
    private userService: UserService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.loadLockers();
  }

  loadLockers() {
    this.userService.getLockers().subscribe(
      (lockers: Locker[]) => {
        this.lockers = lockers;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
