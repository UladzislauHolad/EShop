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

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  columnsToDisplay = ['name'];
  dataSource: OdataDataSource<Category>;
  total: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('input') input: ElementRef;

  constructor(
    private categoryService: CategoryService,
  ) {

  }

  ngOnInit(): void {
    this.dataSource = new OdataDataSource<Category>(this.categoryService);
    this.dataSource.loadData('Name', '', 0, 5, 'CategoryId', 'asc');
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
  }

  delete(category: Category) {

  }

  loadPage() {
    this.dataSource.loadData(
      "Name",
      this.input.nativeElement.value,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sort.active,
      this.sort.direction,
      );
  }
}

