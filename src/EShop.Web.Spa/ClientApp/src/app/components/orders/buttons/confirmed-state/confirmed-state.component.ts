import { Component, OnInit, Input } from '@angular/core';
import { Order } from '../../../../models/order';
import { ButtonEventService } from '../button-event.service';

@Component({
  templateUrl: './confirmed-state.component.html',
  styleUrls: ['./confirmed-state.component.css']
})
export class ConfirmedStateComponent implements OnInit {

  @Input() order: Order;

  constructor(
    private buttonEventService: ButtonEventService
  ) { }

  ngOnInit() {
  }

  info(order: Order) {
    this.buttonEventService.info(order);
  }

  pay(order: Order) {
    this.buttonEventService.changeState(order);
  }
}
