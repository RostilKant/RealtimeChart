import {Injectable} from '@angular/core';
import {ChartModel} from '../interfaces/chartmodel.model';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: ChartModel[];
  public broadcastedData: ChartModel[];

  private hubConnection: signalR.HubConnection;

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chart')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Something goes wrong: ', err));
  }

  public addTransferChartDataListener(): void {
    this.hubConnection.on('transferchartdata', (data) => {
      this.data = data;
      console.log(data);
    });
  }

  public broadcastChartData(): void {
    const data = this.data.map(m => {
      return {
        data: m.data,
        label: m.label
      };
    });
    this.hubConnection.invoke('broadcastchartdata', data)
      .catch(err => console.log(err));
  }

  public addBroadcastChartDataListener(): void {
    this.hubConnection.on('broadcastchartdata', (data) => {
      this.broadcastedData = data;
    });
  }
}
