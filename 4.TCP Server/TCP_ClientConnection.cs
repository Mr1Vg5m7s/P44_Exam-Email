using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace _4.TCP_Server
{
    public class TCP_ClientConnection
    {
        static int _idCounter = 0;  
        public int ID { get; private set; }

        TcpClient client;

        public event Action<TCP_ClientConnection, string>? IncomingMessage;
        public event Action<TCP_ClientConnection>? DisconnectMessage;

        public TCP_ClientConnection(TcpClient client)
        {
            ID = ++_idCounter;
            this.client = client;
        }


        public Task WorkAsync() => Task.Run(() => Work());

        public void Work()
        {
            if(client == null || !client.Connected)
                return;

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            buffer = Encoding.UTF8.GetBytes($"Welcome to the TCP Server! Your ID is {ID}");
            stream.Write(buffer, 0, buffer.Length);

            int bytesRead;

            try
            {
                while (true)
                {
                    StringBuilder? builder = new StringBuilder();
                    do
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        builder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                    }
                    while (stream.DataAvailable);

                    if(bytesRead == 0)
                    {
                        //DisconnectMessage?.Invoke(this);
                        break;
                    }

                    IncomingMessage?.Invoke(this, builder.ToString());

                    builder = null;
                }
            }
            catch (Exception)
            {
                //throw;
            }

            client.Close();
        }

        public void SendMessage(string message)
        {
            if (client == null || !client.Connected)
            {
                return;
            }

            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                DisconnectMessage?.Invoke(this);
            }

        }
    }
}
