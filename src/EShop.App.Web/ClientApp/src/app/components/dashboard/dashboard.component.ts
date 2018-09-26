import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  
  categoryChartData;

  constructor(private dashboardService: DashboardService) {
  }

  ngOnInit(): void {
    this.getCategoryChartInfo();
  }

  getCategoryChartInfo() {
    this.dashboardService.getCategoryChartInfo().subscribe(
      data => this.categoryChartData = data
    )
  }
}
