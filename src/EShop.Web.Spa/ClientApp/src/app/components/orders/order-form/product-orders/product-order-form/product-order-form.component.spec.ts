import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductOrderFormComponent } from './product-order-form.component';

describe('ProductOrderFormComponent', () => {
  let component: ProductOrderFormComponent;
  let fixture: ComponentFixture<ProductOrderFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductOrderFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductOrderFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
