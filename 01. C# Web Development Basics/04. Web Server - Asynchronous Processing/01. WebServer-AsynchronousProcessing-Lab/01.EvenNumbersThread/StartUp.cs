namespace _01.EvenNumbersThread
{
    using System;
    using System.Threading;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var range = Console.ReadLine().Split(' ');
            var startNumber = int.Parse(range[0]);
            var endNumber = int.Parse(range[1]);

            Thread thread = new Thread(() => PrintEvenNumbers(startNumber, endNumber));
            thread.Start();
            thread.Join();
            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int startNumber, int endNumber)
        {
            for (int i = startNumber; i <= endNumber; i++)
            {
                var currentNumber = i;
                if(currentNumber % 2 == 0)
                {
                    Console.WriteLine(currentNumber);
                }
            }
        }
    }
}
