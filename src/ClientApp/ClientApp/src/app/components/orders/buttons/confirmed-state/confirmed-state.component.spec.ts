import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmedStateComponent } from './confirmed-state.component';

describe('ConfirmedStateComponent', () => {
  let component: ConfirmedStateComponent;
  let fixture: ComponentFixture<ConfirmedStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmedStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmedStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
