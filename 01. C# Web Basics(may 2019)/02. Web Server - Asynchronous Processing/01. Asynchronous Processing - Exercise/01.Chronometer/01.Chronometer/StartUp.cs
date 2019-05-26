namespace _01.Chronometer
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var chronometer = new Chronometer();

            string input;
            while ((input = Console.ReadLine()) != "exit")
            {
                var result = string.Empty;
                switch (input.ToLower())
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                    case "laps":
                        var allLaps = chronometer.Laps;
                        if(allLaps.Count > 0)
                        {
                            for (int i = 0; i < allLaps.Count; i++)
                            {
                                var currentLap = allLaps[i];
                                result += $"{i}. {currentLap.ToString()}\r\n";
                            }
                        }
                        else
                        {
                            result = "Laps: no laps";
                        }
                        break;
                    case "time":
                        result = chronometer.GetTime;
                        break;
                    case "lap":
                        result = chronometer.Lap();
                        break;
                    default:
                        result = "Invalid command.";
                        break;
                }

                if(result.Length != 0)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
