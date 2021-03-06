import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Locker } from '../_models/locker';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css'],
})
export class ListsComponent implements OnInit {
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
