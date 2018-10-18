import { Component, OnInit } from '@angular/core';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {
  category: Category;

  processing: boolean;

  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getCategory();
    this.processing = false;
  }

  onSubmit(category: Category) {
    this.processing = true;
    this.updateCategory(category);
  }

  getCategory() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.categoryService.getCategory(id).subscribe(
      category => this.category = category
    );
  }

  updateCategory(category: Category) {
    this.categoryService.updateCategory(category).subscribe(
      () => {
        this.goBack();
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
