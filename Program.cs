using System;
using System.Threading;

namespace PVPNetConnect
{
    class Program
    {
        static void OnConnect(object sender, EventArgs eventArguments)
        {
            Console.WriteLine("OnConnect");
        }

        static void OnDisconnect(object sender, EventArgs eventArguments)
        {
            Console.WriteLine("OnDisconnect");
        }

        static void OnLogin(object sender, string username, string ipAddress)
        {
            Console.WriteLine("OnLogin");
            PVPNetConnection connection = (PVPNetConnection)sender;
            DateTime start = DateTime.Now;
            const int Repetitions = 10;
            int counter = 0;
            for (int i = 0; i < Repetitions; i++)
            {
                
            }
            while (counter < Repetitions)
                Thread.Sleep(0);
            TimeSpan duration = DateTime.Now - start;
            Console.WriteLine("Duration: {0} ms", duration.TotalMilliseconds);
        }

        static void Main(string[] arguments)
        {
            if (arguments.Length != 3)
                throw new Exception(string.Format("Invalid argument count: {0}", arguments.Length));
            PVPNetConnection connection = new PVPNetConnection();
            connection.OnConnect += OnConnect;
            connection.OnLogin += OnLogin;
            connection.OnDisconnect += OnDisconnect;
            connection.Connect(arguments[0], arguments[1], Region.NA, arguments[2]);
            ManualResetEvent terminationEvent = new ManualResetEvent(false);
            terminationEvent.WaitOne();
        }
    }
}