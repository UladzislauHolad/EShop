import { Component, OnInit, Input } from '@angular/core';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-category-multiselect',
  templateUrl: './category-multiselect.component.html',
  styleUrls: ['./category-multiselect.component.css']
})
export class CategoryMultiselectComponent implements OnInit {

  categories: Category[]; 
  @Input() selectedCategories = [];

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getCategories()
      .subscribe(categories => {
        this.categories = categories;
      });
  }

}
