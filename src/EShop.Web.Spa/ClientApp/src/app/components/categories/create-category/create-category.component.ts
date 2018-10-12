import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category.service';
import { Router } from '@angular/router';


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
    private router: Router
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
          this.goBack()
      },
      error => {
        this.processing = false;
      }
    );
  }

  goBack() {
    this.router.navigate(['/categories'])
  }
}
