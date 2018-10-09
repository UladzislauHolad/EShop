import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { CategoryService } from '../../../services/category.service';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, AbstractControl, Validators } from '@angular/forms';
import { Category } from '../../../models/category';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent implements OnInit {

  myForm: FormGroup;
  name: AbstractControl;
  parentId: AbstractControl;
  
  existCategories: Category[];
  noParentCategory: Category = {
    categoryId: 0,
    name: "No Parent",
    parentId: 0
  }

  @Input() processing: boolean;
  @Input() category: Category;
  @Output() submitForm = new EventEmitter();

  constructor(
    private categoryService: CategoryService,
    private location: Location,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.getCategories();
    this.createForm(this.category);
  }

  getCategories() {
    this.categoryService.getCategories().subscribe(
      categories => {
        this.existCategories = categories
        this.existCategories.push(this.noParentCategory);
        console.dir(this.existCategories);
      }
    );
  }

  createForm(category: Category) {
    this.myForm = this.formBuilder.group({
      'categoryId': category.categoryId,
      'name': [
        category.name,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(50)
        ]
      ],
      'parentId': [
        category.parentId,
        Validators.required
      ]
    });

    this.name = this.myForm.controls['name'];
    this.parentId = this.myForm.controls['parentId'];
  }

  onSubmit(category: Category){
    this.submitForm.emit(category);
  }

  goBack(){
    this.location.back();
  }
}
