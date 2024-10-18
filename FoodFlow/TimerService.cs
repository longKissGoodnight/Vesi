using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FoodFlow.Services
{
    public class TimerService
    {
        private readonly System.Timers.Timer _timer;
        public event Action<string>? TimeUpdated;

        public TimerService()
        {
            _timer = new System.Timers.Timer(1000); // Обновляем время каждую секунду
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            TimeUpdated?.Invoke(currentTime);
        }
    }
}
