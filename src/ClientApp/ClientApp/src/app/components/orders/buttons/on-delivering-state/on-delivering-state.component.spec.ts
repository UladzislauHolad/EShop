import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnDeliveringStateComponent } from './on-delivering-state.component';

describe('OnDeliveringStateComponent', () => {
  let component: OnDeliveringStateComponent;
  let fixture: ComponentFixture<OnDeliveringStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnDeliveringStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnDeliveringStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
