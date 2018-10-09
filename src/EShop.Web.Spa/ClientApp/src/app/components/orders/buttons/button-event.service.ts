import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Order } from '../../../models/order';

@Injectable({
  providedIn: 'root'
})
export class ButtonEventService {

  onDeleteSource = new Subject<Order>();
  onInfoSource = new Subject<Order>();
  onChangeStateSource = new Subject<Order>();
  onEditSource = new Subject<Order>();

  onDelete$ = this.onDeleteSource.asObservable();
  onInfo$ = this.onInfoSource.asObservable();
  onChangeState$ = this.onChangeStateSource.asObservable();
  onEdit$ = this.onEditSource.asObservable();

  edit(order: Order) {
    this.onEditSource.next(order);
  }

  delete(order: Order) {
    this.onDeleteSource.next(order);
  }

  info(order: Order) {
    this.onInfoSource.next(order);
  }

  changeState(order: Order) {
    this.onChangeStateSource.next(order);
  }

  constructor() { }
}
