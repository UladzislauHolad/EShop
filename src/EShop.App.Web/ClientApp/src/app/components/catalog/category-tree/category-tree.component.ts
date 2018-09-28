import { Component } from '@angular/core';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { CatalogService } from '../../../services/catalog.service';
import { CategoryNestedNode } from './category-nested-node';

@Component({
  selector: 'app-category-tree',
  templateUrl: './category-tree.component.html',
  styleUrls: ['./category-tree.component.css'],
})
export class CategoryTreeComponent {
  nestedTreeControl: NestedTreeControl<CategoryNestedNode>;
  nestedDataSource: MatTreeNestedDataSource<CategoryNestedNode>;

  constructor(
    private catalogService: CatalogService
  ) {
    this.nestedTreeControl = new NestedTreeControl<CategoryNestedNode>(this._getChildren);
    this.nestedDataSource = new MatTreeNestedDataSource();
    this.getCategoryNestedNodes();
  }

  getCategoryNestedNodes() {
    this.catalogService.getCategoryNestedNodes().subscribe(
      data => this.nestedDataSource.data = data
    )
  }

  hasNestedChild = (_: number, nodeData: CategoryNestedNode) => nodeData.childs.length > 0;

  private _getChildren = (node: CategoryNestedNode) => { 
    return node.childs
  };
}
