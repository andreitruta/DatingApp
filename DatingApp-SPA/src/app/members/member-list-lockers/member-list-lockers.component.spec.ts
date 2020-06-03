/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MemberListLockersComponent } from './member-list-lockers.component';

describe('MemberListLockersComponent', () => {
  let component: MemberListLockersComponent;
  let fixture: ComponentFixture<MemberListLockersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MemberListLockersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MemberListLockersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
