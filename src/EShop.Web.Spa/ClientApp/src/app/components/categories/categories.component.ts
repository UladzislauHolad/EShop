import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { OdataDataSource } from './odata-data-source';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  // categories: Category[];
  columnsToDisplay = ['name'];
  dataSource;

  // @ViewChild(MatPaginator) paginator: MatPaginator;
  // @ViewChild(MatSort) sort: MatSort;

  constructor(
    private categoryService: CategoryService,
    ) {
    
  }

  ngOnInit(): void {
    this.dataSource = new OdataDataSource<Category>(this.categoryService);
    this.dataSource.loadCategories(5);
  }
  // ngAfterViewInit() {
  //   this.dataSource.paginator = this.paginator;
  //   this.dataSource.sort = this.sort;
  // }

  delete(category: Category) {

  }

  // applyFilter(filterValue: string) {
  //   this.dataSource.filter = filterValue.trim().toLowerCase();
  // }

  // onRowClicked(row) {
  //   console.log('Row clicked: ', row);
}

