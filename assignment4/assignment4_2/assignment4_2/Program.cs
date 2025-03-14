using System;
using System.Threading;

namespace assignment4_2
{
    public class Timer
    {
        public event EventHandler Tick;
        public event EventHandler Ring;

        private int targetH;
        private int targetM;
        private bool isActive;

        public void Schedule(int h, int m)
        {
            targetH = h;
            targetM = m;
            isActive = true;
            Console.WriteLine($"Alarm at {h:D2}:{m:D2}");
        }

        public void Run()
        {
            while (true)
            {
                var now = DateTime.Now;

                Tick?.Invoke(this, EventArgs.Empty);
                Console.WriteLine($"Current: {now:HH:mm:ss}");

                if (isActive && now.Hour == targetH && now.Minute == targetM)
                {
                    Ring?.Invoke(this, EventArgs.Empty);
                    Console.WriteLine("WAKE UP!");
                    isActive = false;
                }

                Thread.Sleep(1000);
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            var timer = new Timer();

            timer.Tick += (s, e) => Console.WriteLine("Ticking...");
            timer.Ring += (s, e) => Console.WriteLine("RINGING!");

            var current = DateTime.Now;
            timer.Schedule(current.Hour, (current.Minute + 1) % 60);

            timer.Run();
        }
    }
}