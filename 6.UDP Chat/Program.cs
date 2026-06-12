using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _6.UDP_Chat
{
    internal class Program
    {
        static int localPort;
        static int remotePort;

        static IPAddress remoteAddress = IPAddress.Parse("127.0.0.1");

        static void Main(string[] args)
        {
            Console.SetWindowSize(30, 20);

            try
            {
                Console.WriteLine("Local port: ");
                Int32.TryParse(Console.ReadLine(), out localPort);

                Console.WriteLine("Remote port: ");
                Int32.TryParse(Console.ReadLine(), out remotePort);

                Thread serverThread = new Thread(new ThreadStart(StartServer));
                serverThread.IsBackground = true;
                serverThread.Start();

                while (true)
                {
                    SendMessage(Console.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        private static void SendMessage(string? message)
        {
            UdpClient udpClient = new UdpClient();

            try
            {
                IPEndPoint remoteEP = new IPEndPoint(remoteAddress, remotePort);
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                udpClient.Send(buffer, buffer.Length, remoteEP); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR SD: {ex.Message}");
            }
            finally
            {
                udpClient.Close();
            }
        }

        private static void StartServer()
        {
            try
            {
                while (true)
                {
                    UdpClient udpServer = new UdpClient(localPort);
                    IPEndPoint remoteEP = null;

                    byte[] buffer = udpServer.Receive(ref remoteEP);
                    string message = Encoding.UTF8.GetString(buffer);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(message);
                    Console.ForegroundColor = ConsoleColor.Red;

                    buffer = Encoding.UTF8.GetBytes("Тут буде відповідь");
                    UdpClient udpClient = new UdpClient();
                    udpClient.Send(buffer, buffer.Length, remoteEP);

                    udpServer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR (SS): {ex.Message}");
            }
        }
    }
}
