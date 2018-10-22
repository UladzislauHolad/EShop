import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { OdataDataSource } from '../categories/odata-data-source';
import { MatSort, MatPaginator } from '@angular/material';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { Filter } from 'src/app/helpers/filters/filter';
import { ContainsFilter } from 'src/app/helpers/filters/contains-filter';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  columnsToDisplay = ['name', 'count', 'price', 'description', 'actions'];
  dataSource: OdataDataSource<Product>;
  total: number;
  filters = new Array<Filter>();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('nameInput') nameFilter: ElementRef;
  @ViewChild('descInput') descFilter: ElementRef;

  constructor(
    private productService: ProductService) { }

  ngOnInit() {
    this.dataSource = new OdataDataSource<Product>(this.productService);
    this.dataSource.loadData(this.filters, 0, 5, 'ProductId', 'asc');
    this.dataSource.total$.subscribe(total => this.total = total);
  }

  ngAfterViewInit() {
  
    merge(
      fromEvent(this.nameFilter.nativeElement, 'keyup'),
      fromEvent(this.descFilter.nativeElement, 'keyup')
      )
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

  delete(product) {
    this.productService.deleteProduct(product.ProductId).subscribe(
      () => this.loadPage()
    );
  }

  loadPage() {
    this.filters.push(new ContainsFilter("Name", this.nameFilter.nativeElement.value));
    this.filters.push(new ContainsFilter("Description", this.descFilter.nativeElement.value));

    this.dataSource.loadData(
      this.filters,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sort.active,
      this.sort.direction
    );

    this.filters = [];
  }
}
