import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category.service';
import { NotificationService } from 'ng2-notify-popup';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css'],
  providers: [NotificationService]

})
export class EditCategoryComponent implements OnInit {
  category: Category;

  processing: boolean;

  constructor(
    private categoryService: CategoryService,
    private location: Location,
    private notify: NotificationService,
    private route: ActivatedRoute
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
      category => this.category = category,
      error => this.show(error, "error")
    );
  }

  updateCategory(category: Category) {
    this.categoryService.updateCategory(category).subscribe(
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
