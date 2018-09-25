import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PackedStateComponent } from './packed-state.component';

describe('PackedStateComponent', () => {
  let component: PackedStateComponent;
  let fixture: ComponentFixture<PackedStateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PackedStateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PackedStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
