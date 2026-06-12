using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _5.TCP_Client
{
    public class UserConnection
    {
        TcpClient? client;
        IPAddress address;
        int port;
        string name;

        public string Name
        {
            get => name;
            set
            {
                if (!name.Equals(value))
                {
                    name = value;
                }
            }
        }

        public bool Connected => client != null && client.Connected;

        public event Action<bool> ConnectedEstablished;

        public event Action<string> IncomingMessage;

        public event Action<bool> SendComlete;


        public UserConnection(string name, IPAddress address, int port)
        {
            client = new TcpClient();
            this.name = name;
            this.address = address;
            this.port = port;
        }

        public async Task ConnectAsync() => await Task.Run(Connect);

        public void Connect()
        {
            if (Connected)
            {
                return;
            }

            try
            {
                client!.Connect(address, port);
                ConnectedEstablished?.Invoke(true);
            }
            catch (Exception)
            {
                client = null;
                ConnectedEstablished?.Invoke(false);
            }
        }


        public void Disconnect()
        {
            try
            {
                if (Connected)
                {
                    SendMessage("exit");
                    client!.Close();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                ConnectedEstablished?.Invoke(false);
                client = null;
            }
        }

        public async Task SendMessageAsync(string message) => await Task.Run(() => SendMessage(message));

        public void SendMessage(string message)
        {
            if (!Connected)
                return;

            try
            {
                NetworkStream stream = client!.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
                SendComlete?.Invoke(true);
            }
            catch
            {
                SendComlete?.Invoke(false);
            }
        }

        public async Task ReadMessageAsync() => await Task.Run(ReadMessage);

        public void ReadMessage()
        {
            if (!Connected)
                return;
            while (Connected)
            {
                NetworkStream stream = client!.GetStream();
                int bytes;
                byte[] buffer = new byte[256];
                StringBuilder sb = new StringBuilder();

                do
                {
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    sb.Append(Encoding.UTF8.GetString(buffer));

                } while (stream.DataAvailable);
                IncomingMessage?.Invoke(sb.ToString());
            }
        }
    }
}
