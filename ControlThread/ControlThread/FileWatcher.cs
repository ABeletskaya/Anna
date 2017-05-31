using System;
using System.IO;
using System.Threading;

namespace ControlThread
{
    class FileWatcher
    {
        private string patch;


        public event Action<DateTime> DataChanged;


        public FileWatcher(string patch)
        {
            this.patch = patch;
        }

        private static readonly object syncObject = new object();

        public void Start()
        {
            Thread t = new Thread(() =>
            {
                DateTime dt;
                Console.WriteLine("Method Start");
                // string path = @"..\..\test.txt";
                if (!File.Exists(patch))
                {
                    File.Create(patch);
                } else
                {
                    dt = File.GetLastWriteTime(patch);
                    while (true)
                    {
                        if (dt != File.GetLastWriteTime(patch))
                        {
                            dt = File.GetLastWriteTime(patch);
                            //Console.WriteLine("Last write time: {0}", dt);
                            Console.WriteLine("data change");
                            DataChanged?.Invoke(dt);
                        }
                    }
                }
            });
            t.Start();
        }
    }
}
