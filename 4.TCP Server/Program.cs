using System.Net;

namespace _4.TCP_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Server";


            TCP_Server server = new TCP_Server(/*IPAddress.Loopback*/ IPAddress.Parse("10.6.28.0"), 1000);
            server.Message += (message) =>
            {
                Console.WriteLine(message);
            };

            server.StartAsync();


            string command;
            while (true)
            {
                command = Console.ReadLine();
                if (command == "exit")
                {
                    server.Stop();
                    break;
                }
            }
        }
    }
}
