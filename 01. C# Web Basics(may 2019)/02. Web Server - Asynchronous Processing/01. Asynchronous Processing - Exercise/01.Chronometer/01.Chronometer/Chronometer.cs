namespace _01.Chronometer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using _01.Chronometer.Contracts;

    public class Chronometer : IChronometer
    {
        private const string TimeFormat = "mm\\:ss\\.ffff";
        private Stopwatch time = new Stopwatch();
        private Stopwatch currentLap = new Stopwatch();
        public Chronometer()
        {
            this.Laps = new List<string>();
        }

        public string GetTime => this.time.Elapsed.ToString(TimeFormat);

        public List<string> Laps { get; private set; }

        public string Lap()
        {
            var currentLapTime = this.currentLap.Elapsed;
            this.currentLap.Restart();
            if(currentLapTime.TotalMilliseconds > 0)
            {
                this.Laps.Add(currentLapTime.ToString(TimeFormat));
            }

            return currentLapTime.ToString(TimeFormat);
        }

        public void Reset()
        {
            this.time.Reset();
            this.currentLap.Reset();
        }

        public void Start()
        {
            this.time.Start();
            this.currentLap.Start();
        }

        public void Stop()
        {
            this.time.Stop();
            this.currentLap.Stop();
        }
    }
}
