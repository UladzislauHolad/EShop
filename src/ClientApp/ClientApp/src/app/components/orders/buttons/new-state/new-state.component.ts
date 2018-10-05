import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Order } from 'src/app/models/order';
import { ButtonEventService } from '../button-event.service';

@Component({
  templateUrl: './new-state.component.html',
  styleUrls: ['./new-state.component.css']
})
export class NewStateComponent implements OnInit {

  @Input() order: Order;

  constructor(
    private buttonEventService: ButtonEventService
  ) { }

  ngOnInit() {
  }

  delete(order: Order) {
    this.buttonEventService.delete(order);
  }

  edit(order: Order) {
    this.buttonEventService.edit(order);
  }

  confirm(order: Order) {
    this.buttonEventService.changeState(order);
  }
}
