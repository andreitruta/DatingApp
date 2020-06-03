import { Component, OnInit, Input } from '@angular/core';
import { Locker } from 'src/app/_models/locker';

@Component({
  selector: 'app-locker-card',
  templateUrl: './locker-card.component.html',
  styleUrls: ['./locker-card.component.css'],
})
export class LockerCardComponent implements OnInit {
  @Input() locker: Locker;

  constructor() {}

  ngOnInit() {}
}
