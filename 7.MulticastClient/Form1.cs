using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _7.MulticastClient
{
    public partial class Form1 : Form
    {

        Thread thread;

        public Form1()
        {
            InitializeComponent();
            thread = new Thread(new ThreadStart(MulticastRecieve));
            thread.IsBackground = true; 
            thread.Start();
        }

        private void MulticastRecieve()
        {
            Socket socket = new Socket(
        AddressFamily.InterNetwork,
        SocketType.Dgram,
        ProtocolType.Udp);

            socket.SetSocketOption(
                SocketOptionLevel.Socket,
                SocketOptionName.ReuseAddress,
                true);

            socket.Bind(new IPEndPoint(IPAddress.Any, 4567));

            IPAddress group = IPAddress.Parse("224.5.5.5");

            socket.SetSocketOption(
                SocketOptionLevel.IP,
                SocketOptionName.AddMembership,
                new MulticastOption(group));

            byte[] buffer = new byte[1024];

            while (true)
            {
                int received = socket.Receive(buffer);
                string message = Encoding.UTF8.GetString(buffer, 0, received);

                Invoke(new Action<string>(AppendText), message);
            }
        }

        void AppendText(string message)
        {
            richTextBox1.ForeColor = Color.Red;
            richTextBox1.Text = message;
        }
    }
}
