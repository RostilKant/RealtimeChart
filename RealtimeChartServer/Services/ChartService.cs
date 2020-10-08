using System.Data.Common;
using Entities.DataStorage;
using Entities.HubConfig;
using Microsoft.AspNetCore.SignalR;

namespace Services
{

    public interface IChartService
    {
        public void PostDataToClient();
    }
    
    public class ChartService: IChartService
    {
        private readonly IHubContext<ChartHub> _hub;
        private readonly ITimerService _timer;
        
        public ChartService(IHubContext<ChartHub> hub, ITimerService timer)
        {
            _hub = hub;
            _timer = timer;
        }

        public void PostDataToClient()
        {
            _timer.CreateTimer(() =>
                _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));
        }
    }
}