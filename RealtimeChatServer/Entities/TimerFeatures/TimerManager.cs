using System;
using System.Threading;

namespace Entities.TimerFeatures
{
    public class TimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime TimerStarted { get; set; }

        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            TimerStarted = DateTime.Now;
        }

        private void Execute(object stateInfo)
        {
            _action();

            if ((TimerStarted - DateTime.Now).Seconds > 60)
            {
                _timer.Dispose();
            }
        }
    }
}