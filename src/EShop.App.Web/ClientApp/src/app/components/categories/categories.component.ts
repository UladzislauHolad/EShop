import { Component, OnInit } from '@angular/core';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category.service';
import { NotificationService } from 'ng2-notify-popup';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
  providers: [NotificationService]
})
export class CategoriesComponent implements OnInit {

  categories: Category[];

  constructor(
    private notify: NotificationService,
    private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories)
  }

  delete(category: Category){
    this.categoryService.deleteCategory(category.categoryId).subscribe(
      () => {
        this.show("Category is deleted!", "success");
        this.categories = this.categories.filter(p => p !== category);
      },
      error => {
        this.show(error, "error");
      }
    );
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
