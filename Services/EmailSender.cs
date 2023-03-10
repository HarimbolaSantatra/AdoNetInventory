using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using AppInventaire.Models;
using AppInventaire.Utils;

namespace AppInventaire.Services
{
    public class EmailSender
    {
        string receiverEmail;
        string senderEmail;
        string senderPasswd;
        string copyRecipient;
        /// <summary>
        /// Initialize an EmailSender object.
        /// </summary>
        /// <param name="sender_email"> Sender </param>
        /// <param name="receiver_email"> Receiver </param>
        /// <param name="sender_password"> Sender's Password </param>
        /// <param name="copy_receiver_email"> One CC Receiver. Do not set or let empty string if no copy receiver. </param>
        public EmailSender(string sender_email, string receiver_email, string sender_password, string copy_receiver_email = "")
        {
            receiverEmail = receiver_email;
            senderEmail = sender_email;
            senderPasswd = sender_password;
            copyRecipient = copy_receiver_email;
        }


        /// <summary>
        /// Send email to ovh.net to notify the creation of a new user.
        /// </summary>
        /// <param name="firstName">First name of the new user.</param>
        /// <param name="lastName"> Last name of the new user.</param>
        /// <param name="email"> Email of the new user.</param>
        /// <param name="roleName"> Role name of the new user.</param>
        /// <param name="token"> Token send to the receiver to view the new user's information.</param>
        public void NotifyCreateUser(string firstName, string lastName, string email, string roleName)
        {
            // Create token
            // Fetch Users and Token in database
            UserRepository _user_rep = new UserRepository();
            DetailsToken token = new DetailsToken()
            {
                UserId = _user_rep.FetchByEmail(receiverEmail).ID,
                AddedUserId = _user_rep.FetchByEmail(email).ID,
                TokenKey = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
            };
            TokenRepository _tok_rep = new TokenRepository();
            _tok_rep.Add(token.UserId, token.TokenKey, token.AddedUserId);

            // TRIED SOLUTION: the following solution doesn't work because the link in the email is modified
            // HostingEnvironment.MapPath("~\\Content\\img");
            // HttpContext.Current.Server.MapPath("~\\Content\\img");
            // HttpContext.Current.Request.UserHostAddress + ("\\Content\\img");
            // WORKING SOLUTION:
            string verifyTokenPath = "/Token/UserDetails/";
            string tokenKeyPath = "?token_key=" + token.TokenKey;
            string path = Path.Combine(ProjectVar.SERVER + verifyTokenPath + tokenKeyPath);

            string Subject = "Ajout d'un nouveau utilisateur - AppInventaire";
            string Message = ProjectVar.CREATE_USER_EMAIL_MSG(firstName, lastName, email, roleName, path, token.ExpirationDate);

            EmailManager emailManager = new EmailManager("ssl0.ovh.net", 465);

            emailManager.InitActor(senderEmail, receiverEmail, senderPasswd, copyRecipient);
            emailManager.Send(Subject, Message);
        }

        public void NotifyDeleteUser(string firstName, string lastName, string email, string roleName)
        {
            string Subject = "Suppression d'un utilisateur - AppInventaire";
            string Message = ProjectVar.DELETE_USER_EMAIL_MSG(firstName, lastName, email, roleName);

            EmailManager emailManager = new EmailManager("ssl0.ovh.net", 465);
            emailManager.InitActor(senderEmail, receiverEmail, senderPasswd);
            emailManager.Send(Subject, Message);
        }

        public void NotifyLogin()
        {
            // For test and future usage.
            string path = Path.Combine( ProjectVar.SERVER_BASE_URL + ("\\Content\\img\\logo_dark.png"));

            string Subject = "Login Notification - AppInventaire";
            string Message = $@"
<h1> Vérification du lien : </h1>
<p> Lien sous forme texte: {path} </p>
<p> Visitez lien réel: <a href=""{path}""> ici </a> </p>
";
            
            EmailManager emailManager = new EmailManager("ssl0.ovh.net", 465);
            emailManager.InitActor(senderEmail, receiverEmail, senderPasswd);
            emailManager.Send(Subject, Message);
        }

        public void ForgetPassword(Token token)
        {
            string verifyTokenPath = "/Token/NewPassword/";
            string tokenKeyPath = "?token_key=" + token.TokenKey;
            string path = Path.Combine(ProjectVar.SERVER_BASE_URL + verifyTokenPath + tokenKeyPath);

            string Subject = "Réinitialisation du mot de passe - AppInventaire";
            string Message = ProjectVar.RESET_PWD_MSG(path, token.ExpirationDate);

            EmailManager emailManager = new EmailManager("ssl0.ovh.net", 465);
            emailManager.InitActor(senderEmail, receiverEmail, senderPasswd);
            emailManager.Send(Subject, Message);
        }
    }
}