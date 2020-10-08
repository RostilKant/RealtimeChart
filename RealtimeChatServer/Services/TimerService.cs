using System;
using System.Threading;

namespace Services
{
    public interface ITimerService
    {
        public void CreateTimer(Action action);
    }
    
    public class TimerService: ITimerService
    {
        private Timer _timer;
        private readonly AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime TimerStarted { get; set; }

        public TimerService()
        {
            _autoResetEvent = new AutoResetEvent(false);
            TimerStarted = DateTime.Now;
        }

        public void CreateTimer(Action action)
        {
            _timer = null;
            _action = action;
            _timer = new Timer(Execute, _autoResetEvent, 2000, 2000);

        }
        
        private void Execute(object stateInfo)
        {
            _action();

            if ((DateTime.Now - TimerStarted).Seconds > 60)
            {
                _timer.Dispose();
            }
        }
    }
}