import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderLineChartComponent } from './order-line-chart.component';

describe('OrderLineChartComponent', () => {
  let component: OrderLineChartComponent;
  let fixture: ComponentFixture<OrderLineChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderLineChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderLineChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
