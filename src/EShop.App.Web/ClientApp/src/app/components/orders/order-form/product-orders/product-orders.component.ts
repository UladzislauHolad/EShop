import { Component, OnInit, Input } from '@angular/core';
import { ProductOrder } from '../../../../models/productOrder';
import { ProductOrderService } from '../../../../services/product-order.service';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from 'ng2-notify-popup';

@Component({
  selector: 'app-product-orders',
  templateUrl: './product-orders.component.html',
  styleUrls: ['./product-orders.component.css'],
  providers: [NotificationService]
})
export class ProductOrdersComponent implements OnInit {

  productOrders: ProductOrder[];

  private orderId = +this.route.snapshot.paramMap.get("id");

  constructor(
    private productOrderService: ProductOrderService,
    private route: ActivatedRoute,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    this.getProductOrders();
  }

  getProductOrders() {
    this.productOrderService.getProductOrders(this.orderId).subscribe(
      productOrders => {
        this.productOrders = productOrders;
      },
      error => this.show(error, "error")
    );
  }

  delete(productOrder: ProductOrder)
  {
    this.productOrderService.deleteProductOrder(this.orderId, productOrder.productOrderId).subscribe(
      () => {
        this.show("Done!", "success");
        this.productOrders = this.productOrders.filter(po => po != productOrder);
      },
      error => this.show(error, "error")
    );
  }

  save(productOrder: ProductOrder)
  {
    console.dir(productOrder);
    this.productOrderService.updateProductOrder(this.orderId, productOrder).subscribe(
      () => {
        this.show("Done!", "success");
        productOrder.isEditing = !productOrder.isEditing;
      },
      error => this.show(error, "error")
    );
  }

  edit(productOrder: ProductOrder)
  {
    productOrder.isEditing = !productOrder.isEditing;
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
