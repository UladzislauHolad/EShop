import { Component, OnInit, Input } from '@angular/core';
import { Order } from '../../../../models/order';
import { ButtonEventService } from '../button-event.service';

@Component({
  selector: 'app-completed-state',
  templateUrl: './completed-state.component.html',
  styleUrls: ['./completed-state.component.css']
})
export class CompletedStateComponent implements OnInit {

  @Input() order: Order;

  constructor(
    private buttonEventService: ButtonEventService
  ) { }

  ngOnInit() {
  }

  info(order: Order) {
    this.buttonEventService.info(order);
  }
}
