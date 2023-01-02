using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Services
{
    public class EmailSender
    {
        public void NotifyCreateUser(string firstName, string lastName, string email, string roleName)
        {
            string Subject = "Ajout d'un nouveau utilisateur";
            string Message = $@"
Bonjour,

Nous vous informons l'ajout d'un nouveau utilisateur ayant les informations suivants:
    - Nom: {firstName}
    - Prénom: {lastName}
    - Rôle: {roleName}
    - Adresse Mail: {email}

Merci !
";
            EmailManager emailManager = new EmailManager("ssl0.ovh.net", 465);
            emailManager.InitActor("andrana@crystal-frame.fr", "andrana@crystal-frame.fr", "$$SML99**md255");
            emailManager.Send(Subject, Message);
        }
    }
}