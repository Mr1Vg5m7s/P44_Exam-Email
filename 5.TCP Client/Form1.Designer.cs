namespace _5.TCP_Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            txtClientName = new TextBox();
            label3 = new Label();
            btnConnect = new Button();
            btnDisconnect = new Button();
            txtPort = new TextBox();
            label2 = new Label();
            txtIPAdrress = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            btnSend = new Button();
            txtMessage = new TextBox();
            groupBox3 = new GroupBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            richTextBox1 = new RichTextBox();
            groupBox4 = new GroupBox();
            listBox1 = new ListBox();
            tabPage2 = new TabPage();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtClientName);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnConnect);
            groupBox1.Controls.Add(btnDisconnect);
            groupBox1.Controls.Add(txtPort);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtIPAdrress);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 68);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Connection Info";
            // 
            // txtClientName
            // 
            txtClientName.Location = new Point(502, 37);
            txtClientName.Name = "txtClientName";
            txtClientName.Size = new Size(100, 23);
            txtClientName.TabIndex = 7;
            txtClientName.Text = "Serg";
            txtClientName.TextAlign = HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(564, 19);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 6;
            label3.Text = "Name";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(617, 15);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 45);
            btnConnect.TabIndex = 5;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Enabled = false;
            btnDisconnect.Location = new Point(695, 15);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(75, 45);
            btnDisconnect.TabIndex = 4;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(171, 37);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(51, 23);
            txtPort.TabIndex = 3;
            txtPort.Text = "1000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(171, 19);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 2;
            label2.Text = "Port";
            // 
            // txtIPAdrress
            // 
            txtIPAdrress.Location = new Point(6, 37);
            txtIPAdrress.Name = "txtIPAdrress";
            txtIPAdrress.Size = new Size(100, 23);
            txtIPAdrress.TabIndex = 1;
            txtIPAdrress.Text = "10.6.28.0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 0;
            label1.Text = "Server IP";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnSend);
            groupBox2.Controls.Add(txtMessage);
            groupBox2.Location = new Point(12, 447);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(776, 93);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Message";
            // 
            // btnSend
            // 
            btnSend.Enabled = false;
            btnSend.Location = new Point(626, 21);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(144, 66);
            btnSend.TabIndex = 1;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_ClickAsync;
            // 
            // txtMessage
            // 
            txtMessage.Enabled = false;
            txtMessage.Location = new Point(6, 22);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(614, 65);
            txtMessage.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tabControl1);
            groupBox3.Location = new Point(12, 86);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(620, 355);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Chats";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(6, 22);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(608, 327);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(600, 299);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Enabled = false;
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(604, 299);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(listBox1);
            groupBox4.Location = new Point(638, 86);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(150, 355);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Clients List";
            // 
            // listBox1
            // 
            listBox1.Enabled = false;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(6, 22);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(138, 319);
            listBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(600, 299);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 552);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnDisconnect;
        private TextBox txtPort;
        private Label label2;
        private TextBox txtIPAdrress;
        private Label label1;
        private Button btnConnect;
        private TextBox txtClientName;
        private Label label3;
        private GroupBox groupBox2;
        private Button btnSend;
        private TextBox txtMessage;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private RichTextBox richTextBox1;
        private ListBox listBox1;
        private TabPage tabPage2;
    }
}
