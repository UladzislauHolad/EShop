import { Component, OnInit } from '@angular/core';
import { Order } from '../../../models/order';
import { OrderService } from '../../../services/order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from 'ng2-notify-popup';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.css'],
  providers: [NotificationService]
})
export class EditOrderComponent implements OnInit {

  order: Order;
  processing: boolean;

  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private router: Router,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    this.getOrder();
  }

  onSubmit(order: Order) {
    this.processing = true;
    this.updateOrder(order);
  }
  
  getOrder() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.orderService.getOrder(id).subscribe(
      order => this.order = order
    );
  }

  updateOrder(order: Order) {
    this.orderService.updateOrder(order).subscribe(
      () => {
        this.show("Done!", "success");
        this.processing = false;
      },
      error => this.show(error, "error")
    );
  }
  
  goBack() {
    this.router.navigate(['/spa/orders']);
  }

  show(text: string, type: string): void {
    this.notify.show(text, { position:'bottom', duration:'2000', type: type, location: '#notification' });
  }
}
