using System;
using System.Net;
using System.Net.Mail;

using MailKit.Net.Smtp;
using MimeKit;

namespace AppInventaire.Services
{
    public class EmailManager
    {
        MailAddress Sender;
        MailAddress Receiver;
        string Password;
        string SmtpAddress;
        int Port;
        public EmailManager(string smtpAddress, int port)
        {
            SmtpAddress = smtpAddress;
            Port = port;
        }

        public void InitActor(string senderAddress, string receiverAddress, string senderPassword)
        {
            Sender = new MailAddress(senderAddress);
            Receiver = new MailAddress(receiverAddress);
            Password = senderPassword;
        }
        
        public void Send(string subject, string message)
        {
            MailMessage Message = new MailMessage(Sender, Receiver);
            Message.Subject = subject;
            Message.Body = message;

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // We use MailKit.Net.Smtp for SmtpClient becauce System.Net.Mail's SmtpClient class is not recommended: 
                // voir lien: https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=net-7.0
                client.Connect(SmtpAddress, Port, true);
                client.Authenticate(Sender.Address, Password);    // Code needed if authentication required
                MimeMessage mimeMessage = (MimeMessage) Message;    // Convert System.Net.Mail.MailMessage to MimeKit.MimeMessage
                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            Message.Dispose();
        }

    }
}