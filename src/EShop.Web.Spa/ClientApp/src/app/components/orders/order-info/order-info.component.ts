import { Component, OnInit, Input } from '@angular/core';
import { Order } from '../../../models/order';
import { OrderService } from '../../../services/order.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-order-info',
  templateUrl: './order-info.component.html',
  styleUrls: ['./order-info.component.css']
})
export class OrderInfoComponent implements OnInit {

  order: Order;
  total: number;
  orderId = +this.route.snapshot.paramMap.get('id');

  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.getOrder();
  }

  getOrder() {
    this.orderService.getOrder(this.orderId).subscribe(
      order => {
        this.order = order
      }
    )
  }

  goBack() {
    this.router.navigate([`orders`])
  }
}
