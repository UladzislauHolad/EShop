import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { ODataQuery, ODataService, ODataResponse } from 'odata-v4-ng';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {

  categories: Category[];

  constructor(
    private categoryService: CategoryService,
    private odataService: ODataService) { }

  ngOnInit() {
    // this.categoryService.getCategories()
    //   .subscribe(categories => this.categories = categories)
    let odataQuery = new ODataQuery(this.odataService, "http://localhost:5001/api/")
      .entitySet('Categories');
    odataQuery.get().subscribe(
      (odataResponse: ODataResponse) => {
        console.dir(odataResponse.toString());
      },
      (error: string) => {
        console.log(error);
      }
    );
  }

  delete(category: Category){
    this.categoryService.deleteCategory(category.categoryId).subscribe(
      () => {
        this.categories = this.categories.filter(p => p !== category);
      },
      error => {
      }
    );
  }
}
