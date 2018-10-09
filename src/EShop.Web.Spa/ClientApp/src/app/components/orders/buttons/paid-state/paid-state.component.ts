import { Component, OnInit, Input } from '@angular/core';
import { Order } from '../../../../models/order';
import { ButtonEventService } from '../button-event.service';

@Component({
  templateUrl: './paid-state.component.html',
  styleUrls: ['./paid-state.component.css']
})
export class PaidStateComponent implements OnInit {

  @Input() order: Order;

  constructor(
    private buttonEventService: ButtonEventService
  ) { }

  ngOnInit() {
  }

  info(order: Order) {
    this.buttonEventService.info(order);
  }

  nextState(order: Order) {
    this.buttonEventService.changeState(order);
  }
}
