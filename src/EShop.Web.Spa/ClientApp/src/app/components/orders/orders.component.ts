import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order';
import { ButtonEventService } from './buttons/button-event.service';
import { Router } from '@angular/router';
import { Subscription, fromEvent, merge } from 'rxjs';
import { OdataDataSource } from '../categories/odata-data-source';
import { MatPaginator, MatSort } from '@angular/material';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { Filter } from 'src/app/helpers/filters/filter';
import { ContainsFilter } from 'src/app/helpers/filters/contains-filter';
import { GreaterThanFilter } from 'src/app/helpers/filters/greater-than-filter';
import { LessThanFilter } from 'src/app/helpers/filters/less-than-filter';
import { EventEmitter } from 'protractor';
import { IFilter } from 'src/app/helpers/filters/ifilter';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
})
export class OrdersComponent implements OnInit {

  subscriptions = new Subscription();
  columnsToDisplay = ['orderId', 'status', 'date', 'actions'];
  dataSource: OdataDataSource<Order>;
  total: number;
  filters = new Array<IFilter>();
  maxStartDate: Date;
  minEndDate: Date;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('status') statusFilter: ElementRef;
  @ViewChild('startDate') startDate: ElementRef;
  @ViewChild('endDate') endDate: ElementRef;

  constructor(
    private orderService: OrderService,
    private buttonEventService: ButtonEventService,
    private router: Router
  ) { 
    this.subscriptions.add(
      buttonEventService.onDeleteSource.subscribe(
        order => this.delete(order)
      )
    );
    this.subscriptions.add(
      buttonEventService.onInfoSource.subscribe(
        order => this.info(order)
      )
    );
    this.subscriptions.add(
      buttonEventService.onChangeStateSource.subscribe(
        order => this.changeState(order)
      )
    );
    this.subscriptions.add(
      buttonEventService.onEditSource.subscribe(
        order => this.edit(order)
      )
    );
  }

  ngOnInit() {
    this.dataSource = new OdataDataSource<Order>(this.orderService);
    this.dataSource.loadData(this.filters, 0, 5, 'OrderId', 'asc');
    this.dataSource.total$.subscribe(total => this.total = total);
  }

  ngAfterViewInit() {
    fromEvent(this.statusFilter.nativeElement, 'keyup')
      .pipe(
        debounceTime(250),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.loadPage();
        })
      )
      .subscribe();

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page).pipe(
      tap(() => this.loadPage())
    )
      .subscribe();
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  startDateChange(currentStartDate: Date) {
    this.minEndDate = new Date(currentStartDate);
    this.minEndDate.setDate(currentStartDate.getDate() + 1);
    if(currentStartDate >= this.endDate.nativeElement)
      this.endDate.nativeElement.value = null;
    
    this.paginator.pageIndex = 0;
    this.loadPage();
  }  

  endDateChange(currentEndDate: Date) {
    this.maxStartDate = new Date(currentEndDate);
    this.maxStartDate.setDate(currentEndDate.getDate() - 1);
    if(currentEndDate <= this.startDate.nativeElement)
      this.startDate.nativeElement.value = null;

    this.paginator.pageIndex = 0;
    this.loadPage();
  }

  loadPage() {
    if(this.startDate.nativeElement.value)
      this.filters.push(new GreaterThanFilter('Date', new Date(this.startDate.nativeElement.value).toISOString()));
    if(this.endDate.nativeElement.value)
      this.filters.push(new LessThanFilter('Date', new Date(this.endDate.nativeElement.value).toISOString()));
    
    this.filters.push(new ContainsFilter('Status', this.statusFilter.nativeElement.value));

    this.dataSource.loadData(
      this.filters,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sort.active,
      this.sort.direction
    );

    this.filters = [];
  }

  delete(order: Order) {
    this.orderService.deleteOrder(order.orderId).subscribe(
      () => this.loadPage()
    );
  }

  info(order: Order) {
    this.router.navigate([`orders/${order.orderId}/details`]);
  }

  edit(order: Order) {
    this.router.navigate([`orders/${order.orderId}`]);
  }

  changeState(order: Order) {
    this.orderService.changeState(order).subscribe(
      () => this.loadPage()
    );
  }

}
