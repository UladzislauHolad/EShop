import { Injectable } from '@angular/core';
import { NewStateComponent } from './new-state/new-state.component';
import { State } from './state';
import { Order } from '../../../models/order';
import { ConfirmedStateComponent } from './confirmed-state/confirmed-state.component';
import { PaidStateComponent } from './paid-state/paid-state.component';
import { PackedStateComponent } from './packed-state/packed-state.component';
import { OnDeliveringStateComponent } from './on-delivering-state/on-delivering-state.component';
import { CompletedStateComponent } from './completed-state/completed-state.component';


@Injectable({
  providedIn: 'root'
})
export class StateService {

  configurations: { [state:string] : State; } = {};

  constructor(){
    this.configurations["New"] = new State(NewStateComponent, null);
    this.configurations["Confirmed"] = new State(ConfirmedStateComponent, null);
    this.configurations["Paid"] = new State(PaidStateComponent, null);
    this.configurations["Packed"] = new State(PackedStateComponent, null);
    this.configurations["OnDelivering"] = new State(OnDeliveringStateComponent, null);
    this.configurations["Completed"] = new State(CompletedStateComponent, null);
  }

  getComponent(order: Order) {
    return this.configurations[order.status];
  }
}
