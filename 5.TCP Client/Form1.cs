namespace _5.TCP_Client
{
    public partial class Form1 : Form
    {
        UserConnection client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (client != null)
                {
                    client.Disconnect();
                }
            }
            catch (Exception)
            {

            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false;
                client = new UserConnection(
                    txtClientName.Text,
                    System.Net.IPAddress.Parse(txtIPAdrress.Text),
                    Convert.ToInt32(txtPort.Text));
                client.ConnectedEstablished += Client_ConnectedEstablished;
                client.IncomingMessage += Client_IncomingMessage;
                await client.ConnectAsync();
                await client.ReadMessageAsync();
            }
            catch (Exception ex)
            {
                client = null;
                btnConnect.Enabled = false;
                richTextBox1.Text += $"ERROR: {ex.Message}";
            }
        }

        private void Client_IncomingMessage(string message)
        {
            Action action = () =>
            {
                richTextBox1.Text += $"\n{message}";
            };
            Invoke(action);
        }

        private void Client_ConnectedEstablished(bool isConnected)
        {
            Action action = () =>
            {
                if (isConnected)
                {
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    btnSend.Enabled = true;
                    txtMessage.Enabled = true;
                    txtIPAdrress.Enabled = false;
                    txtPort.Enabled = false;
                    txtClientName.Enabled = false;
                    listBox1.Enabled = true;
                }
                else
                {
                    btnConnect.Enabled = true;
                    btnDisconnect.Enabled = false;
                    btnSend.Enabled = false;
                    txtMessage.Enabled = false;
                    txtIPAdrress.Enabled = true;
                    txtPort.Enabled = true;
                    txtClientName.Enabled = true;
                    listBox1.Enabled = false;
                }
            };
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected)
            {
                try
                {
                    client.Disconnect();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private async void btnSend_ClickAsync(object sender, EventArgs e)
        {
            if (client != null && client.Connected && !string.IsNullOrEmpty(txtMessage.Text))
            {
                
                await client.SendMessageAsync(txtClientName.Text + ": " + txtMessage.Text);
                txtMessage.Text = "";
                txtMessage.Focus();

                await client.ReadMessageAsync();

            }
        }
    }
}
