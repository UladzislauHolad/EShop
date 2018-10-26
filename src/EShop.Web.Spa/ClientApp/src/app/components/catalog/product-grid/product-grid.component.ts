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

@Component({
  selector: 'app-product-grid',
  templateUrl: './product-grid.component.html',
  styleUrls: ['./product-grid.component.css']
})
export class ProductGridComponent implements OnInit {

  subscription: Subscription;
  products: Product[];

  sortField = 'name';
  sortDirection = 'asc';
  dataSource: OdataDataSource<Product>;
  total: number;
  filters = new Array<IFilter>();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private sidenavEventService: SideNavEventService,
    private productService: ProductService
  ) { }

  ngOnInit() {
    this.dataSource = new OdataDataSource<Product>(this.productService);
    this.dataSource.loadData(this.filters, 0, 5, this.sortField, this.sortDirection);
    this.dataSource.total$.subscribe(total => this.total = total);
    this.dataSource.data$.subscribe(products => this.products = products);
    
    this.subscription = new Subscription();
    this.subscription.add(this.sidenavEventService.onChoose$.subscribe(
      id => {
        this.loadPage(id.toString());
      }
    ));
  }

  ngAfterViewInit() {
    this.paginator.page.pipe(
      tap(() => this.loadPage())
    ).subscribe();
  }

  loadPage(id?: string) {
    if(id){
      let filter = new EqualFilter('categoryId', id);
      this.filters.push(new AnyFilter(filter, "categories"));
    }

    this.dataSource.loadData(
      this.filters,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sortField,
      this.sortDirection
    );

    this.filters = [];
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getProductsByCategoryId(id: number) {
    this.productService.getProductsByCategoryId(id).subscribe(
      products => this.products = products
    );
  }
}
