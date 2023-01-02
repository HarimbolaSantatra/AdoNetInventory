using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Services
{
    public class EmailManager
    {
        MailAddress sender;
        MailAddress receiver;
        SmtpClient client;
        public EmailManager(string senderAddress, string receiverAddress, string senderPassword)
        {
            sender = new MailAddress(senderAddress);
            receiver = new MailAddress(receiverAddress);
        }
        
        public void Send(string subject, string message)
        {
            MailMessage Message = new MailMessage(sender, receiver);
            Message.Subject = subject;
            Message.Body = message;
            SmtpClient client = new SmtpClient("smtp.server.address", 2525)
            {
                Credentials = new NetworkCredential("smtp_username", password),
                EnableSsl = true
                // specify whether your host accepts SSL connections
            };
            // code in brackets above needed if authentication required
            client.Send(message);
            
        }

        public void Dispose()
        {

        }

    }
}