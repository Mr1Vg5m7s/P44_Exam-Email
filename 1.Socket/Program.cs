using System.Net;

namespace _1.Socket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

            IPAddress.TryParse("127.0.0.1", out IPAddress? parsedIpAddress);

            IPAddress local = IPAddress.Loopback;

            IPEndPoint endPoint = new IPEndPoint(ipAddress, 8080);

        }
    }
}
