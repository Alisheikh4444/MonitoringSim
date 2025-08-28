using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;

namespace BLL
{
    public class Methods
    {
        public class NumberGenerator
        {
            private readonly Timer _timer;
            private readonly Random _rnd = new();

            
            public double Min { get; }
            public double Max { get; }

            public event Action<double>? NumberGenerated;

            public NumberGenerator(double periodMs = 16.7, double min = 0, double max = 100)
            {
                if (max < min) (min, max) = (max, min);
                Min = min; Max = max;

                _timer = new Timer(periodMs) { AutoReset = true, Enabled = false };
                _timer.Elapsed += (_, __) =>
                {
                    
                    double v = Min + _rnd.NextDouble() * (Max - Min);

                    
                    if (_rnd.NextDouble() < 0.02) v += (Max - Min) * (_rnd.NextDouble() < 0.5 ? 0.3 : -0.3);

                    NumberGenerated?.Invoke(v);
                };
            }

            public void Start() => _timer.Start();
            public void Stop() => _timer.Stop();
        }
    }

}
