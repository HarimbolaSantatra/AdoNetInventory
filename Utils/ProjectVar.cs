using System;
using System.IO;
using System.Web;
using System.Web.Hosting; // Return correct path
using System.Collections.Generic;
using System.Linq;

namespace AppInventaire.Utils
{
    /// <summary>
    /// Contains all the constant project variable.
    /// </summary>
    public class ProjectVar
    {
        // Get absolute path
        // C:\Program Files (x86)\IIS Express || "E:\\Santatra\\ASP.NET\\AppInventaire\\Results\\iText.pdf"
        public static string PDF_DEST = HostingEnvironment.MapPath("\\Results\\temp_pdf.pdf");
        public static string LOGO_DEST = HostingEnvironment.MapPath("\\Content\\img\\logo_dark.png");
        public static string SERVER_BASE_URL = Path.Combine(
                HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority);
        public static string SERVER = "http://37.187.143.160:8301/";
        public static string ADMIN_EMAIL_YAHOO = "rvnjks2000@yahoo.fr";
        public static string ADMIN_EMAIL_ANDRANA = "andrana@crystal-frame.fr";
        public static string ADMIN_PWD_ANDRANA = "$$SML99**md255";
        public static string CREATE_USER_EMAIL_MSG(
            string firstName, string lastName, string email, string roleName, string link, DateTime expirationDate)
        {
            return $@"
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
    (Ce lien expire le {expirationDate})
</p>

Merci !
";
        }

        public static string DELETE_USER_EMAIL_MSG(string firstName, string lastName, string email, string roleName)
        {
            return $@"
Bonjour,

Nous vous informons que l'utilisateur suivant a été supprimer du base de donnée:
    - Nom: {lastName}
    - Prénom: {firstName}
    - Rôle: {roleName}
    - Adresse Mail: {email}

Merci !
";
        }

        public static string RESET_PWD_MSG(string link, DateTime expirationDate)
        {
            return $@"
<p> Bonjour, </p>

<p> Veuillez visiter le lien suivant pour réinitialiser votre mot de passe : <a href=""{link}"">Lien</a> 
    <br> 
    (Ce lien expire le {expirationDate})
</p>
<p>
    Pour annuler cette opération, veuillez ignorer cette email.
</p>

Merci !
";
        }
    }
}