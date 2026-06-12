using MailKit.Net.Smtp;
using MailKit.Net.Imap;
using MimeKit;
using System.Net.Mail;

namespace _10.MailClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /// Send mail

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Від кого", "gololobov.serhiy@gmail.com"));
            message.To.Add(new MailboxAddress("Кому", "gololobov.serhiy@gmail.com"));
            message.Subject = "Test mail";
            message.Body = new TextPart()
            {
                Text = "Hello. It`s test message"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("gololobov.serhiy@gmail.com", "xxxx xxxx xxxx xxxx");
                    client.Send(message);
                    Console.WriteLine("Message send");
                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            ///Inbox
            ///

            string email = "gololobov.serhiy@gmail.com";
            string password = "xxxx xxxx xxxx xxxx";
            string imapServer = "imap.gmail.com";
            int port = 993;

            using(var client = new MailKit.Net.Imap.ImapClient())
            {
                try
                {
                    client.Connect(imapServer, port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                    client.Authenticate(email, password);
                    var inbox = client.Inbox;
                    inbox.Open(MailKit.FolderAccess.ReadOnly);
                    Console.WriteLine($"All message - {inbox.Count}");
                    for (int i = inbox.Count - 1; i >= Math.Max(0, inbox.Count - 5); i--)
                    {
                        var message = inbox.GetMessage(i);
                        Console.WriteLine( $"Лист №{i + 1}" );
                        Console.WriteLine( $"Від   : {message.From}" );
                        Console.WriteLine( $"Тема  : {message.Subject}" );
                        Console.WriteLine( $"Дата  : {message.Date}" );
                        Console.WriteLine( $"Текст : {message.TextBody?.Substring(0, Math.Min(80, message.TextBody.Length))}" );
                        Console.WriteLine("----------------------------------------------------------------------");
                    }

                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
