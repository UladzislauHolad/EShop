import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { OdataDataSource } from './odata-data-source';
import { TableData } from 'src/app/models/table-data';
import { DataSource } from '@angular/cdk/table';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { merge } from 'rxjs/internal/observable/merge';
import { fromEvent } from 'rxjs';
import { Filter } from 'src/app/helpers/filters/filter';
import { ContainsFilter } from 'src/app/helpers/filters/contains-filter';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  columnsToDisplay = ['name', 'actions'];
  dataSource: OdataDataSource<Category>;
  total: number;
  filters = new Array<Filter>();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('input') nameFilter: ElementRef;

  constructor(
    private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.dataSource = new OdataDataSource<Category>(this.categoryService);
    this.dataSource.loadData(this.filters, 0, 5, 'CategoryId', 'asc');
    this.dataSource.total$.subscribe(total => this.total = total);
  }

  ngAfterViewInit() {

    fromEvent(this.nameFilter.nativeElement, 'keyup')
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
  }

  delete(category) {
    this.categoryService.deleteCategory(category.CategoryId).subscribe(
      () => this.loadPage()
    );
  }

  loadPage() {
    this.filters.push(new ContainsFilter("Name", this.nameFilter.nativeElement.value));

    this.dataSource.loadData(
      this.filters,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sort.active,
      this.sort.direction,
      );
    
    this.filters = [];
  }
}

