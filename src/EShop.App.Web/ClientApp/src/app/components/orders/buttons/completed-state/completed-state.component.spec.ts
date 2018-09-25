import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletedStateComponent } from './completed-state.component';

describe('CompletedStateComponent', () => {
  let component: CompletedStateComponent;
  let fixture: ComponentFixture<CompletedStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompletedStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompletedStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
