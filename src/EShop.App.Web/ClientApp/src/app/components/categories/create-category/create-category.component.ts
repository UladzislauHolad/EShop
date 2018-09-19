import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category.service';
import { NotificationService } from 'ng2-notify-popup';


@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css'],
  providers: [NotificationService]
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
    private notify: NotificationService
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
        this.show("Done! You will be redirected to the previous page", "success");
        setTimeout(() => {
          this.goBack()
        }, 3000);
      },
      error => {
        this.show(error, "error");
        this.processing = false;
      }
    );
  }

  goBack() {
    this.location.back();
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
