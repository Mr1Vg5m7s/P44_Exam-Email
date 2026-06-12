using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _2.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client Test";
            //Console.Write("IP Address:");
            //string ipAddress = Console.ReadLine();
            //Console.Write("Port: ");
            //int port = int.Parse(Console.ReadLine());

            EndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 1000);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Console.WriteLine("Starting client...");
                socket.Connect(endPoint);
                if (socket.Connected)
                {
                    Console.WriteLine("Connected to server.");
                }
                else
                    throw new Exception("Failed to connect to server.");

                Console.Write("Write message : ");
                string? message = Console.ReadLine();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message!);

                socket.Send(buffer);
                Console.WriteLine("Message sent.");


                Console.WriteLine("Wait for response...");
                int bytes = 0;
                buffer = new byte[1024];
                StringBuilder messageBuilder = new StringBuilder();
                do
                {
                    bytes = socket.Receive(buffer);
                    messageBuilder.Append(System.Text.Encoding.UTF8.GetString(buffer, 0, bytes));
                } while (socket.Available > 0);

                Console.WriteLine($"Response received: {messageBuilder.ToString()}");

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
