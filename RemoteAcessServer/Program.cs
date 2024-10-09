using System;

namespace RemoteAcessServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Server...");
            Server server = new Server();
            server.Start();
        }
    }
}
