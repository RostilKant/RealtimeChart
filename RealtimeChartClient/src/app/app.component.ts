import {Component, OnInit} from '@angular/core';
import {SignalRService} from './services/signal-r.service';
import {HttpClient} from '@angular/common/http';
import {ChartType} from 'chart.js';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  public chartOptions = {
    scaleShowVerticalLines: true,
    responsive: true,
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true
        }
      }]
    }
  };

  public chartLabels = ['Real time data for chart'];
  public chartType: ChartType = 'bar';
  public chartLegend = true;
  public colors = [
    {backgroundColor: '#82E0AA'},
    {backgroundColor: '#66E'},
    {backgroundColor: '#93E'},
    {backgroundColor: '#42E'},
  ];

  constructor(
    public signalRService: SignalRService,
    private http: HttpClient
  ) {
  }

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();
    this.signalRService.addBroadcastChartDataListener();
    this.startHttpRequest();
  }

  private startHttpRequest(): void {
    this.http.get('https://localhost:5001/api/chart')
      .subscribe(res => {
        console.log(res);
      });
  }

  public chartClicked(event: any): void {
    console.log(event);
    this.signalRService.broadcastChartData();
  }

}
