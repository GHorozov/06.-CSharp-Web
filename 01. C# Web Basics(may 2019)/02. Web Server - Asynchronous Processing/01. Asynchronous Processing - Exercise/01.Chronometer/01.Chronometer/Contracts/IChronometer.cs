namespace _01.Chronometer.Contracts
{
    using System.Collections.Generic;

    public interface IChronometer
    {
        string GetTime { get; }

        List<string>Laps { get; }

        void Start();

        void Stop();

        string Lap();

        void Reset();
    }
}
