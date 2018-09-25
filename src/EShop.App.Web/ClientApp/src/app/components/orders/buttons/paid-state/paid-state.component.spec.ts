import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaidStateComponent } from './paid-state.component';

describe('PaidStateComponent', () => {
  let component: PaidStateComponent;
  let fixture: ComponentFixture<PaidStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaidStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaidStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
