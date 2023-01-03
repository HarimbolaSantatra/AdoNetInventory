using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppInventaire.Models;
using AppInventaire.Utils;

namespace AppInventaire.Services
{
    public class EmailSender
    {
        string receiverEmail;
        string senderEmail;
        string senderPasswd;
        public EmailSender(string sender_email, string receiver_email, string sender_password)
        {
            receiverEmail = receiver_email;
            senderEmail = sender_email;
            senderPasswd = sender_password;
        }
        public void NotifyCreateUser(string firstName, string lastName, string email, string roleName, DetailsToken token)
        {
            string link = "https://localhost:44389/Token/VerifyToken/";
            link += token.UserId;
            link += "?token_key=" + token.TokenKey;
            string Subject = "Ajout d'un nouveau utilisateur - AppInventaire";
            string Message = ProjectVariables.CREATE_USER_EMAIL_MSG(firstName, lastName, email, roleName, link, token.ExpirationDate);
            EmailManager emailManager = new EmailManager("ssl0.ovh.net", 465);
            //emailManager.InitActor("andrana@crystal-frame.fr", "rvnjks2000@yahoo.fr", "$$SML99**md255");
            emailManager.InitActor(senderEmail, receiverEmail, "$$SML99**md255");
            emailManager.Send(Subject, Message);
        }

        public void NotifyDeleteUser(string firstName, string lastName, string email, string roleName)
        {
            string Subject = "Suppression d'un utilisateur - AppInventaire";
            string Message = ProjectVariables.DELETE_USER_EMAIL_MSG(firstName, lastName, email, roleName);
            EmailManager emailManager = new EmailManager("ssl0.ovh.net", 465);
            emailManager.InitActor(senderEmail, receiverEmail, senderPasswd);
            emailManager.Send(Subject, Message);
        }
    }
}