import { Component, OnInit, Input } from '@angular/core';
import { ProductOrder } from '../../../../models/productOrder';
import { ProductOrderService } from '../../../../services/product-order.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-orders',
  templateUrl: './product-orders.component.html',
  styleUrls: ['./product-orders.component.css'],
})
export class ProductOrdersComponent implements OnInit {

  productOrders: ProductOrder[];
  oldCounts = new Array<number>();

  displayedColumns: string[] = ['name', 'price', 'count', 'total', 'actions'];

  private orderId = +this.route.snapshot.paramMap.get("id");

  constructor(
    private productOrderService: ProductOrderService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.getProductOrders();
  }

  getProductOrders() {
    this.productOrderService.getProductOrders(this.orderId).subscribe(
      productOrders => {
        this.productOrders = productOrders;
      }
    );
  }

  delete(productOrder: ProductOrder) {
    this.productOrderService.deleteProductOrder(this.orderId, productOrder.productOrderId).subscribe(
      () => {
        this.productOrders = this.productOrders.filter(po => po != productOrder);
      },
    );
  }

  save(productOrder: ProductOrder) {
    this.productOrderService.updateProductOrder(this.orderId, productOrder).subscribe(
      () => {
        productOrder.isEditing = false;
      }
    );
  }

  edit(productOrder: ProductOrder) {
    productOrder.isEditing = true;
    this.oldCounts[productOrder.productOrderId] = productOrder.orderCount;
  }

  cancel(productOrder: ProductOrder) {
    productOrder.isEditing = false;
    productOrder.orderCount = this.oldCounts[productOrder.productOrderId];
  }  

  getTotalCost() {
    if(this.productOrders) {
      return this.productOrders.reduce((acc, value) => acc + value.orderCount * value.price, 0);
    }
    return 0;    
  }
}
