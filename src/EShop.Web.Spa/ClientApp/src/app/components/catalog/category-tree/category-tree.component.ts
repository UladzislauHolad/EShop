import { Component, OnInit } from '@angular/core';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material';
import { CatalogService } from '../../../services/catalog.service';
import { CategoryNestedNode } from './category-nested-node';
import { SideNavEventService } from '../sidenav/sidenav-event.service';

@Component({
  selector: 'app-category-tree',
  templateUrl: './category-tree.component.html',
  styleUrls: ['./category-tree.component.css'],
})
export class CategoryTreeComponent {
  nestedTreeControl: NestedTreeControl<CategoryNestedNode>;
  nestedDataSource: MatTreeNestedDataSource<CategoryNestedNode>;

  constructor(
    private catalogService: CatalogService,
    private sidenavEventService: SideNavEventService
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

  chooseCategory(id: number) {
    this.sidenavEventService.choose(id);
  }

  private _getChildren = (node: CategoryNestedNode) => { 
    return node.childs
  };
}
