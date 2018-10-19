import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order';
import { ButtonEventService } from './buttons/button-event.service';
import { Router } from '@angular/router';
import { Subscription, fromEvent, merge } from 'rxjs';
import { OdataDataSource } from '../categories/odata-data-source';
import { MatPaginator, MatSort } from '@angular/material';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';

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

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('input') input: ElementRef;

 

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
    this.dataSource.loadData('Status', '', 0, 5, 'OrderId', 'asc');
    this.dataSource.total$.subscribe(total => this.total = total);
  }

  ngAfterViewInit() {

    fromEvent(this.input.nativeElement, 'keyup')
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

  loadPage() {
    this.dataSource.loadData(
      "Status",
      this.input.nativeElement.value,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sort.active,
      this.sort.direction
    );
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
