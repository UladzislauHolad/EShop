import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category.service';


@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnInit{
  
  category: Category = {
    categoryId: 0,
    name: '',
    parentId: 0
  };

  processing: boolean;

  constructor(
    private categoryService: CategoryService,
    private location: Location,
  ) { }

  ngOnInit(): void {
    this.processing = false;
  }

  onSubmit(category: Category) {
    this.processing = true;
    this.createCategory(category);
  }

  createCategory(category: Category) {
    this.categoryService.createCategory(category).subscribe(
      () => {
        setTimeout(() => {
          this.goBack()
        }, 3000);
      },
      error => {
        this.processing = false;
      }
    );
  }

  goBack() {
    this.location.back();
  }
}
