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
        public void NotifyCreateUser(string firstName, string lastName, string email, string roleName, DetailsToken token)
        {
            string link = "https://localhost:44389/Token/VerifyToken/?token_key=";
            link += token.TokenKey;
            string Subject = "Ajout d'un nouveau utilisateur - AppInventaire";
            string Message = $@"
<p> Bonjour, </p>

<p> Nous vous informons l'ajout d'un nouveau utilisateur ayant les informations suivants:
    <ul>
        <li> Nom: {firstName} </li>
        <li> Prénom: {lastName} </li>
        <li> Rôle: {roleName} </li>
        <li> Adresse Mail: {email} </li>
    </ul>
</p> 
<p>
    Voir les informations de l'utilisateur: <a href=""{link}"">Lien</a> <br> 
    (Ce lien expire le {token.ExpirationDate})
</p> 

Merci !
";
            EmailManager emailManager = new EmailManager("ssl0.ovh.net", 465);
            //emailManager.InitActor("andrana@crystal-frame.fr", "rvnjks2000@yahoo.fr", "$$SML99**md255");
            emailManager.InitActor("andrana@crystal-frame.fr", "andrana@crystal-frame.fr", "$$SML99**md255");
            emailManager.Send(Subject, Message);
        }

        public void NotifyDeleteUser(string firstName, string lastName, string email, string roleName)
        {
            string Subject = "Suppression d'un utilisateur - AppInventaire";
            string Message = $@"
Bonjour,

Nous vous informons que l'utilisateur suivant a été supprimer du base de donnée:
    - Nom: {lastName}
    - Prénom: {firstName}
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