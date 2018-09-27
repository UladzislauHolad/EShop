import { Component } from '@angular/core';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { of } from 'rxjs';
import { CatalogService } from '../../../services/catalog.service';
import { CategoryNode } from './category-node';

@Component({
  selector: 'app-category-tree',
  templateUrl: './category-tree.component.html',
  styleUrls: ['./category-tree.component.css'],
})
export class CategoryTreeComponent {
  nestedTreeControl: NestedTreeControl<CategoryNode>;
  nestedDataSource: MatTreeNestedDataSource<CategoryNode>;
  dataChange = new BehaviorSubject<CategoryNode[]>([]);

  constructor(
    private catalogService: CatalogService
  ) {
    this.nestedTreeControl = new NestedTreeControl<CategoryNode>(this._getChildren);
    this.nestedDataSource = new MatTreeNestedDataSource();

    this.getTopLevelCategoryNodes();
  }

  getTopLevelCategoryNodes() {
    this.catalogService.getCategoryNodesByParentId(0).subscribe(
      data => this.nestedDataSource.data = data
    );
  }

  hasNestedChild = (_: number, nodeData: CategoryNode) => nodeData.hasChilds;

  private _getChildren = (node: CategoryNode) => { 
    if(node.hasChilds)
      return this.catalogService.getCategoryNodesByParentId(node.categoryId);
  };
}
