import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { CategoryService } from '../../../services/category.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, AbstractControl, FormGroup, Validators } from '@angular/forms';
import { Category } from '../../../models/category';
import { Product } from '../../../models/product';
import { Location } from '@angular/common';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {

  @Input() product: Product;
  @Input() processing: boolean;

  existCategories: Category[];

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

  ngOnInit() {
    this.getCategories();
    this.createForm(this.product);
  }

  getProduct(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.productService.getProduct(id)
      .subscribe(product => {
        console.dir(product);
        this.product = product;
        this.createForm(product);
        this.getCategories();
      });
  }

  @Output() onFormSubmit = new EventEmitter<Product>();

  onSubmit(product: Product) {
    this.onFormSubmit.emit(product);
  }

  getCategories(): void {
    this.categoryService.getCategories()
      .subscribe(categories => {
        this.existCategories = categories;
      });
  }

  createForm(product: Product) {
    this.myForm = this.formBuilder.group({
      'productId': product.productId,
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
        product.categories,
        [
          Validators.required
        ]
      ]
    });

    this.name = this.myForm.controls['name'];
    this.price = this.myForm.controls['price'];
    this.count = this.myForm.controls['count'];
    this.description = this.myForm.controls['description'];
    this.categories = this.myForm.controls['categories'];
  }

  goBack() {
    this.location.back();
  }
}
