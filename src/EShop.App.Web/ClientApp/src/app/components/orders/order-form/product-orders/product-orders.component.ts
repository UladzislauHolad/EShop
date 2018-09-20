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

  constructor(
    private productOrderService: ProductOrderService,
    private route: ActivatedRoute,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    this.getProductOrders();
  }

  getProductOrders() {
    const id = +this.route.snapshot.paramMap.get("id");
    this.productOrderService.getProductOrders(id).subscribe(
      productOrders => {
        this.productOrders = productOrders;
      },
      error => this.show(error, "error")
    );
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
