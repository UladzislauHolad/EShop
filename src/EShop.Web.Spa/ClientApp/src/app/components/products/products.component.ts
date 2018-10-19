import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { OdataDataSource } from '../categories/odata-data-source';
import { MatSort, MatPaginator } from '@angular/material';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  columnsToDisplay = ['name', 'count', 'price', 'description', 'actions'];
  dataSource: OdataDataSource<Product>;
  total: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('input') input: ElementRef;

  constructor(
    private productService: ProductService) { }

  ngOnInit() {
    this.dataSource = new OdataDataSource<Product>(this.productService);
    this.dataSource.loadData('Name', '', 0, 5, 'ProductId', 'asc');
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

  delete(product) {
    this.productService.deleteProduct(product.ProductId).subscribe(
      () => this.loadPage()
    );
  }

  loadPage() {
    this.dataSource.loadData(
      "Name",
      this.input.nativeElement.value,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sort.active,
      this.sort.direction
    );
  }
}
