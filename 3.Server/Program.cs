using System.Net;
using System.Net.Sockets;

namespace _3.Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 1000);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(endPoint);
            socket.Listen(5);

            Console.WriteLine("Server start. Waiting for client connection...");

            while (true)
            {
                Socket listen = await socket.AcceptAsync();
                Console.WriteLine($"Client connected. {listen.RemoteEndPoint}");

                _ = ReciveMessage(listen);
            }
        }

        static async Task ReciveMessage(Socket socket)
        {
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytes = await socket.ReceiveAsync(buffer, SocketFlags.None);
                    if (bytes == 0)
                        break;

                    string message = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
                    Console.WriteLine($"{socket.RemoteEndPoint}: {message}");

                    string response = $"Received: {DateTime.Now}";
                    byte[] responseBuffer = System.Text.Encoding.UTF8.GetBytes(response);
                    await socket.SendAsync(responseBuffer, SocketFlags.None);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine($"Client disconnected. {socket.RemoteEndPoint}");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}
