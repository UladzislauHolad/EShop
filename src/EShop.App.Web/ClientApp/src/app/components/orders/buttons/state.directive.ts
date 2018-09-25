import { Directive, ViewContainerRef, Output } from '@angular/core';
import { EventEmitter } from 'events';

@Directive({
  selector: '[state-host]'
})
export class StateDirective {
  constructor(
    public viewContainerRef: ViewContainerRef
  ) { }

}
