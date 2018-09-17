import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product';
import { ActivatedRoute } from '@angular/router';

import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { CategoryService } from '../../../services/category.service';
import { Category } from '../../../models/category';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {

  product: Product;
  existCategories: Category[];
  // selectedCategories: Category[];
  
  myForm: FormGroup;
  name: AbstractControl;
  price: AbstractControl;
  count: AbstractControl;
  description: AbstractControl;
  categories: AbstractControl;



  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private location: Location,
    private categoryService: CategoryService,
    private productService: ProductService,
    ) { }

  getProduct(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.productService.getProduct(id)
      .subscribe(product => {
        this.product = product
        this.createForm(product);
        this.getCategories();
      });
  }

  onSubmit(form: any): void {
    console.log('You submited form: ', form);
  }

  getCategories(): void {
    this.categoryService.getCategories()
      .subscribe(categories => {
        this.existCategories = categories;
      });
  }

  createForm(product: Product) {
    this.myForm = this.formBuilder.group({
      'name': [
        product.name,
        [ 
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50)
        ]
      ],
      'price': [
        product.price,
        [
          Validators.required,
          Validators.min(0.01)
        ]
      ],
      'count': [
        product.count,
        [
          Validators.required,
          Validators.min(1)
        ]
      ],
      'description': [
        product.description,
        [
          Validators.required,
          Validators.minLength(20),
          Validators.maxLength(500)
        ]
      ],
      'categories': [
        product.categories        
      ]
    });

    this.name = this.myForm.controls['name'];
    this.price = this.myForm.controls['price'];
    this.count = this.myForm.controls['count'];
    this.description = this.myForm.controls['description'];
    this.categories = this.myForm.controls['categories'];
  }

  ngOnInit() {
    this.getProduct();
  }

  goBack() {
    this.location.back();
  }
}
