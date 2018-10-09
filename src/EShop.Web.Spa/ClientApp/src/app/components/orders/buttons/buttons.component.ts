import { Component, OnInit, ViewChild, ComponentFactoryResolver, Input, EventEmitter, Output } from '@angular/core';
import { StateDirective } from './state.directive';
import { StateService } from './state.service';
import { StateComponent } from './stateComponent';
import { Order } from '../../../models/order';

@Component({
  selector: 'app-buttons',
  templateUrl: './buttons.component.html',
  styleUrls: ['./buttons.component.css']
})
export class ButtonsComponent implements OnInit {

  @ViewChild(StateDirective) stateHost: StateDirective;
  @Input() order: Order;
  
  constructor(
    private stateService: StateService,
    private componentFactoryResolver: ComponentFactoryResolver
  ) { }

  ngOnInit() {
    let state = this.stateService.getComponent(this.order);
    let componentFactory = this.componentFactoryResolver.resolveComponentFactory(state.component);
    
    let viewContainerRef = this.stateHost.viewContainerRef;
    viewContainerRef.clear();

    let componentRef = viewContainerRef.createComponent(componentFactory);
    (<StateComponent>componentRef.instance).order = this.order;
  }
}
