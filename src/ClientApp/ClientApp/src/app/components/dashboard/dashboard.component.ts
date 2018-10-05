import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { LineItem } from './models/line-item';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  
  categoryChartData;
  orderLineChartData;

  constructor(private dashboardService: DashboardService) {
  }

  ngOnInit(): void {
    this.getCategoryChartInfo();
    this.getOrderLineChartInfo();
  }

  getCategoryChartInfo() {
    this.dashboardService.getCategoryChartInfo().subscribe(
      data => this.categoryChartData = data
    )
  }

  getOrderLineChartInfo() {
    this.dashboardService.getOrderLineChartInfo().subscribe(
      data => {
        this.orderLineChartData = data;
        console.dir(this.orderLineChartData);
      }
    )
  }
}
