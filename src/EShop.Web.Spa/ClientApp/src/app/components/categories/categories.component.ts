import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { ODataQuery, ODataService, ODataResponse } from 'odata-v4-ng';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements AfterViewInit {

  categories: Category[];
  columnsToDisplay = ['name'];
  dataSource: MatTableDataSource<Category>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private categoryService: CategoryService,
    private odataService: ODataService) {
    this.dataSource = new MatTableDataSource<Category>([]);
    let odataQuery = new ODataQuery(this.odataService, "http://localhost:5001/api/")
      .entitySet('Categories');
    odataQuery.get().subscribe(
      (odataResponse: ODataResponse) => {
        this.dataSource.data = odataResponse.getBodyAsJson();
      },
      (error: string) => {
        console.log(error);
      }
    );
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  delete(category: Category) {

  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
