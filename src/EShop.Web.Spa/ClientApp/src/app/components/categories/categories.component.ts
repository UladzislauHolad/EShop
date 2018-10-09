import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {

  categories: Category[];

  constructor(
    private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories)
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
