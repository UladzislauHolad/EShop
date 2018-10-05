import { Component, OnInit, Input } from '@angular/core';
import { ChartOptions } from '../chart-options';

@Component({
  selector: 'app-category-chart',
  templateUrl: './category-chart.component.html',
  styleUrls: ['./category-chart.component.css']
})
export class CategoryChartComponent implements OnInit {
  
  @Input() data;

  options = new ChartOptions();

  ngOnInit() {
    this.options.legendTitle = 'Category'
    this.options.view = [,400];
  }

  onSelect(obj)
  {
    console.dir(obj);
  }
}
