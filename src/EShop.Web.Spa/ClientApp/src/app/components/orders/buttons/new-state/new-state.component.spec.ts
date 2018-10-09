import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewStateComponent } from './new-state.component';

describe('NewStateComponent', () => {
  let component: NewStateComponent;
  let fixture: ComponentFixture<NewStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
