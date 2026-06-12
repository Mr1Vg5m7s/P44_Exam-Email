using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _8.MulticastServer
{
    public partial class Form1 : Form
    {

        static int timeout = 1000;
        static string message = "";

        Thread thread;

        public Form1()
        {
            InitializeComponent();
            thread = new Thread(new ThreadStart(MulticastSend));
            thread.IsBackground = true;
            thread.Start();
        }

        private void MulticastSend()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            IPAddress address = IPAddress.Parse("224.5.5.5");
            IPEndPoint endPoint = new IPEndPoint(address, 4567);
            socket.Connect(endPoint);

            while (true)
            {
                Thread.Sleep(timeout);
                socket.Send(Encoding.UTF8.GetBytes(message));
            }
            socket.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            message = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked)
            {
                MessageBox.Show("Виберіть тип повідомлення", "Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                textBox2.Text = "";
                textBox2.Focus();

                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
            }

            

        }
    }
}
