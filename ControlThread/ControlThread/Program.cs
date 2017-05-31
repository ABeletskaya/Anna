using System;

namespace ControlThread
{
    class Program
    {
        static void Main(string[] args)
        {
            FileWatcher observer = new FileWatcher(@"..\..\test.txt");
            observer.DataChanged += TimeChanged;
            observer.Start();
        }

        static void TimeChanged(DateTime data)
        {
            Console.WriteLine("Last write time: {0}", data);
        }
    }
}
