import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, OnInit, ViewChild, ElementRef, Input } from '@angular/core';
import { FormControl, Validators, AbstractControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { MatAutocomplete, MatChipInputEvent, MatAutocompleteSelectedEvent, ErrorStateMatcher } from '@angular/material';
import { startWith, map, tap, endWith } from 'rxjs/operators';
import { Category } from 'src/app/models/category';

@Component({
  selector: 'app-category-chips',
  templateUrl: './category-chips.component.html',
  styleUrls: ['./category-chips.component.css']
})
export class CategoryChipsComponent implements OnInit {
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = false;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  categoryCtrl = new FormControl();
  filteredCategories: Observable<Category[]>;

  @Input() categories: Category[];
  @Input() existCategories: Category[];
  @Input() chipsCtrl: AbstractControl;

  @ViewChild('categoryInput') categoryInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto') matAutocomplete: MatAutocomplete;

  constructor() {
    this.filteredCategories = this.categoryCtrl.valueChanges.pipe(
      startWith(null),
      map((name: Category | string | null) => {
        let fcategories;

        if (!name) {
          fcategories = this.existCategories.slice();
        }
        else {
          fcategories = typeof name === 'string' ? this._filter(name) : this._filterC(name);
        }

        return fcategories.filter(c => !this.categories.includes(c));
      })
    );
  }

  ngOnInit() {
  }

  remove(category: Category): void {
    this.categories = this.categories.filter(c => c.categoryId !== category.categoryId);

    this.categoryInput.nativeElement.value = '';
    this.categoryCtrl.setValue(null);

    this.chipsCtrl.setValue(this.categories);
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.categories.push(event.option.value);

    this.categoryInput.nativeElement.value = '';
    this.categoryCtrl.setValue(null);

    this.chipsCtrl.setValue(this.categories);
  }

  private _filter(name: string): Category[] {
    const filterValue = name.toLowerCase();

    return this.existCategories.filter(category => category.name.toLowerCase().indexOf(filterValue) === 0);
  }

  private _filterC(category: Category): Category[] {
    const filterValue = category.categoryId;

    return this.existCategories.filter(category => (category.categoryId === filterValue));
  }
}
