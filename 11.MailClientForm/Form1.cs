using MailKit;

namespace _11.MailClientForm
{
    public partial class Form1 : Form
    {

        List<MimeKit.MimeMessage> inboxes = new List<MimeKit.MimeMessage>();
        List<MimeKit.MimeMessage> outboxes = new List<MimeKit.MimeMessage>();
        List<MimeKit.MimeMessage> korzina = new List<MimeKit.MimeMessage>();
        List<MimeKit.MimeMessage> mostPrority = new List<MimeKit.MimeMessage>();
        List<MimeKit.MimeMessage> allFolders = new List<MimeKit.MimeMessage>();
        List<MimeKit.MimeMessage> spam = new List<MimeKit.MimeMessage>();
        List<MimeKit.MimeMessage> markered = new List<MimeKit.MimeMessage>();
        List<MimeKit.MimeMessage> draft = new List<MimeKit.MimeMessage>();

        string email = "fedorishchevvadym@gmail.com";
        string password = "qtwp xjtb fxem twmv";
        string imapServer = "imap.gmail.com";
        int port = 993;


        public Form1()
        {
            InitializeComponent();
            LoadFolder();
        }


        void LoadFolder()
        {
            using (var client = new MailKit.Net.Imap.ImapClient())
            {
                try
                {
                    client.Connect(imapServer, port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                    client.Authenticate(email, password);

                    foreach (var ns in client.PersonalNamespaces)
                    {
                        var folders = client.GetFolders(ns);

                        foreach (var folder in folders)
                        {
                            string[] folderNames = folder.FullName.Split('/');
                            if (folderNames.Length == 1)
                            {
                                treeView1.Nodes.Add(new TreeNode { Name = folderNames[0], Text = folderNames[0] });

                            }
                            else
                            {
                                TreeNode[] foundNodes = treeView1.Nodes.Find(folderNames[0], true);

                                if (foundNodes.Length > 0)
                                {
                                    TreeNode node = foundNodes[0]; // Отримуємо перший знайдений
                                    node.Nodes.Add(new TreeNode { Name = folderNames[1], Text = folderNames[1] });
                                }

                            }
                        }
                    }

                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            using (var client = new MailKit.Net.Imap.ImapClient())
            {
                try
                {
                    client.Connect(imapServer, port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                    client.Authenticate(email, password);

                    var inbox = client.Inbox;
                    inbox.Open(MailKit.FolderAccess.ReadOnly);
                    for (int i = inbox.Count - 1; i >= Math.Max(0, inbox.Count - 5); i--)
                    {
                        var message = inbox.GetMessage(i);
                        inboxes.Add(message);
                    }
                    ////////
                    var sentFolder = client.GetFolder("[Gmail]/Отправленные");

                    sentFolder.Open(FolderAccess.ReadOnly);
                    for (int i = sentFolder.Count - 1; i >= Math.Max(0, sentFolder.Count - 5); i--)
                    {
                        var message = sentFolder.GetMessage(i);
                        outboxes.Add(message);
                    }
                    ///////
                    var trash = client.GetFolder("[Gmail]/Корзина");

                    trash.Open(FolderAccess.ReadOnly);
                    for (int i = trash.Count - 1; i >= Math.Max(0, trash.Count - 5); i--)
                    {
                        var message = trash.GetMessage(i);
                        korzina.Add(message);
                    }

                    /////
                    var priroty = client.GetFolder("[Gmail]/Важное");

                    priroty.Open(FolderAccess.ReadOnly);
                    for (int i = priroty.Count - 1; i >= Math.Max(0, priroty.Count - 5); i--)
                    {
                        var message = priroty.GetMessage(i);
                        mostPrority.Add(message);
                    }
                    /////
                    var all = client.GetFolder("[Gmail]/Вся почта");

                    all.Open(FolderAccess.ReadOnly);
                    for (int i = all.Count - 1; i >= Math.Max(0, all.Count - 5); i--)
                    {
                        var message = all.GetMessage(i);
                        allFolders.Add(message);
                    }
                    /////
                    var traspam = client.GetFolder("[Gmail]/Спам");

                    traspam.Open(FolderAccess.ReadOnly);
                    for (int i = traspam.Count - 1; i >= Math.Max(0, traspam.Count - 5); i--)
                    {
                        var message = traspam.GetMessage(i);
                        spam.Add(message);
                    }
                    //
                    var marker = client.GetFolder("[Gmail]/Помеченные");

                    marker.Open(FolderAccess.ReadOnly);
                    for (int i = marker.Count - 1; i >= Math.Max(0, marker.Count - 5); i--)
                    {
                        var message = marker.GetMessage(i);
                        markered.Add(message);
                    }
                    //
                    var drafter = client.GetFolder("[Gmail]/Черновики");

                    drafter.Open(FolderAccess.ReadOnly);
                    for (int i = drafter.Count - 1; i >= Math.Max(0, drafter.Count - 5); i--)
                    {
                        var message = drafter.GetMessage(i);
                        draft.Add(message);
                    }
                    client.Disconnect(true);

                    ListViewUpdate(outboxes);//
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int ind = listView1.SelectedIndices[0];
                if (inboxes[ind].HtmlBody != null)
                {
                    await webView21.EnsureCoreWebView2Async();
                    webView21.CoreWebView2.NavigateToString(inboxes[ind].HtmlBody);
                }
                else
                {
                    webView21.CoreWebView2.NavigateToString(inboxes[ind].TextBody);
                }
            }
        }


        void ListViewUpdate(List<MimeKit.MimeMessage> folder)
        {
            listView1.Items.Clear();
            foreach (var mail in folder)
            {
                ListViewItem item = new ListViewItem(mail.From.ToString());
                item.SubItems.Add(mail.To.ToString());
                item.SubItems.Add(mail.Date.DateTime.ToString());
                item.SubItems.Add(mail.Subject);
                item.SubItems.Add("0");
                listView1.Items.Add(item);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node?.Text == "INBOX")
            {
                //e.Node.Text = "Входящие";//
                ListViewUpdate(inboxes);
            }

            if (e.Node?.Text == "Отправленные")
            {
                ListViewUpdate(outboxes);
            }

            if (e.Node?.Text == "Корзина")
            {
                ListViewUpdate(korzina);
            }
            if (e.Node?.Text == "Важное")
            {
                ListViewUpdate(mostPrority);

            }

            if (e.Node?.Text == "Вся почта")
            {
                ListViewUpdate(allFolders);
            }

            if (e.Node?.Text == "Спам")
            {
                ListViewUpdate(spam);
            }

            if (e.Node?.Text == "Помеченные")
            {
                ListViewUpdate(markered);
            }

            if (e.Node?.Text == "Черновики")
            {
                ListViewUpdate(draft);
            }
        }
    }
}
