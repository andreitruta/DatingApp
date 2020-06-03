/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LockerCardComponent } from './locker-card.component';

describe('LockerCardComponent', () => {
  let component: LockerCardComponent;
  let fixture: ComponentFixture<LockerCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LockerCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LockerCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
