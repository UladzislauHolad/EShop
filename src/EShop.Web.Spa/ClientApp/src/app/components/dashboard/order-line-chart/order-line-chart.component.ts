import { Component, OnInit, Input } from '@angular/core';
import { ChartOptions } from '../chart-options';
import { LineItem } from '../models/line-item';

@Component({
  selector: 'app-order-line-chart',
  templateUrl: './order-line-chart.component.html',
  styleUrls: ['./order-line-chart.component.css']
})
export class OrderLineChartComponent implements OnInit {

  @Input() data: any[];

  options = new ChartOptions();

  view: any[];
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Date';
  showYAxisLabel = true;
  yAxisLabel = 'Count';
  timeline = true;
  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  

  // line, area
  autoScale = true;

  ngOnInit() {
    this.options.legendTitle = 'Status';
  }

}
