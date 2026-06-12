using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Message;

namespace _4.TCP_Server
{
    public class TCP_Server
    {

        TcpListener listener;

        List<TCP_ClientConnection> clients = new List<TCP_ClientConnection>();

        public event Action<string>? Message;

        public TCP_Server(IPAddress ipaddress, int port)
        {
            listener = new TcpListener(ipaddress, port);
        }

        public void StartAsync() => Task.Run(() => Start());

        public void Start()
        {
            try
            {
                Message?.Invoke($"Starting TCP Server on {listener.LocalEndpoint}");
                listener.Start();
                Message?.Invoke("TCP Server started. Waiting for clients...");
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    TCP_ClientConnection connection = new TCP_ClientConnection(client);
                    connection.IncomingMessage += (conn, message) =>
                    {
                        Message.Message mes = new Message.Message { Text = message , Date = DateTime.Now, Name = "jj"};

                        Message?.Invoke($"Received from Client {conn.ID}: {message}");
                        // Echo the message back to the client

                        foreach (var item in clients)
                        {
                            item.SendMessage($"Client {conn.ID}: {message}");
                        }

                    };
                    connection.DisconnectMessage += (conn) =>
                    {
                        Message?.Invoke($"Client {conn.ID} disconnected.");
                        clients.Remove(conn);
                    };
                    clients.Add(connection);
                    Message?.Invoke($"Client {connection.ID} connected.");
                    connection.WorkAsync();
                }
            }
            catch (Exception ex)
            {
                Message?.Invoke($"Error starting TCP Server: {ex.Message}");
                return;
            }
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}
