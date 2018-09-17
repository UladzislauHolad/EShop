import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryMultiselectComponent } from './category-multiselect.component';

describe('CategoryMultiselectComponent', () => {
  let component: CategoryMultiselectComponent;
  let fixture: ComponentFixture<CategoryMultiselectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoryMultiselectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryMultiselectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
