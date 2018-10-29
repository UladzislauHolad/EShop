import { Component, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { SideNavEventService } from '../sidenav/sidenav-event.service';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product';
import { map, tap } from 'rxjs/operators';
import { MatPaginator } from '@angular/material';
import { Filter } from 'src/app/helpers/filters/filter';
import { OdataDataSource } from '../../categories/odata-data-source';
import { TableData } from 'src/app/models/table-data';
import { ContainsFilter } from 'src/app/helpers/filters/contains-filter';
import { IFilter } from 'src/app/helpers/filters/ifilter';
import { AnyFilter } from 'src/app/helpers/filters/any-filter';
import { EqualFilter } from 'src/app/helpers/filters/equal-filter';
import { ConditionalExpr } from '@angular/compiler';

@Component({
  selector: 'app-product-grid',
  templateUrl: './product-grid.component.html',
  styleUrls: ['./product-grid.component.css']
})
export class ProductGridComponent implements OnInit {

  subscription: Subscription;
  products = new Array<Product>();

  sortField = 'name';
  sortDirection = 'asc';
  dataSource: OdataDataSource<Product>;
  total: number;
  filters = new Array<IFilter>();

  batch = 15;
  batchIndex = 0;
  maxBatchIndex;
  finished = false;

  // @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private sidenavEventService: SideNavEventService,
    private productService: ProductService
  ) { }

  ngOnInit() {
    this.dataSource = new OdataDataSource<Product>(this.productService);
    this.dataSource.loadData(this.filters, 0, this.batch, this.sortField, this.sortDirection, 'categories');
    this.dataSource.total$.subscribe(total => {
      this.total = total;
      this.maxBatchIndex = Math.ceil(total/this.batch);
    });
    this.dataSource.data$.subscribe(products => {
      this.products = this.products.concat(products);
    });
    
    this.subscription = new Subscription();
    this.subscription.add(this.sidenavEventService.onChoose$.subscribe(
      id => {
        this.batchIndex = 0;
        this.products.splice(0);
        this.loadBatch(id.toString());
      }
    ));
  }

  loadBatch(id?: string) {
    if (id) {
      let filter = new EqualFilter('categoryId', id);
      this.filters.push(new AnyFilter(filter, "categories"));
    }

    this.dataSource.loadData(
      this.filters,
      this.batchIndex,
      this.batch,
      this.sortField,
      this.sortDirection,
      'categories'
    );

    this.filters = [];
  }

  onScroll() {
    console.log('scroll');
    if(this.batchIndex < this.maxBatchIndex) {
      this.batchIndex++;
      this.loadBatch();
    }
    else {
      this.finished = true;
    }
  }
  // ngAfterViewInit() {
  //   this.paginator.page.pipe(
  //     tap(() => this.loadPage())
  //   ).subscribe();
  // }

  // loadPage(id?: string) {
  //   if(id){
  //     let filter = new EqualFilter('categoryId', id);
  //     this.filters.push(new AnyFilter(filter, "categories"));
  //   }

  //   this.dataSource.loadData(
  //     this.filters,
  //     this.paginator.pageIndex,
  //     this.paginator.pageSize,
  //     this.sortField,
  //     this.sortDirection
  //   );

  //   this.filters = [];
  // }

  // ngOnDestroy() {
  //   this.subscription.unsubscribe();
  // }

  // getProductsByCategoryId(id: number) {
  //   this.productService.getProductsByCategoryId(id).subscribe(
  //     products => this.products = products
  //   );
  // }
}
