namespace _01.Chronometer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using _01.Chronometer.Contracts;

    public class Chronometer : IChronometer
    {
        private const string TimeFormat = "mm\\:ss\\.ffff";
        private long miliseconds;
        private bool isRunning;
        public Chronometer()
        {
            this.Laps = new List<string>();
        }

        public string GetTime => $"{this.miliseconds / 60000:D2}:{this.miliseconds / 1000:D2}:{this.miliseconds % 1000:D4}"; 

        public List<string> Laps { get; private set; }

        public string Lap()
        {
            var lap = this.GetTime;
            this.Laps.Add(lap);

            return lap;
        }

        public void Reset()
        {
            this.Stop();
            this.miliseconds = 0;
            this.Laps = new List<string>();
        }

        public void Start()
        {
            this.isRunning = true;

            Task.Run(() =>
            {
                while (this.isRunning)
                {
                    Thread.Sleep(1);
                    this.miliseconds++;
                }
            });
        }

        public void Stop()
        {
            this.isRunning = false;
        }
    }
}
