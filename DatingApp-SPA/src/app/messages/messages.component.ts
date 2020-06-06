import { Component, OnInit } from '@angular/core';
import { LockerHistory } from '../_models/lockerhistory';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css'],
})
export class MessagesComponent implements OnInit {
  lockersHistory: LockerHistory[];

  constructor(
    private userService: UserService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.loadLockersHistory();
  }

  loadLockersHistory() {
    this.userService.getLockersHistory().subscribe(
      (lockersHistory: LockerHistory[]) => {
        this.lockersHistory = lockersHistory;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
