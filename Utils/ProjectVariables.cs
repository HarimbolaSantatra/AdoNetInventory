using System;
using System.IO;
using System.Web;
using System.Web.Hosting; // Return correct path
using System.Collections.Generic;
using System.Linq;

namespace AppInventaire.Utils
{
    public class ProjectVariables
    {
        // Get absolute path
        // C:\Program Files (x86)\IIS Express: "E:\\Santatra\\ASP.NET\\AppInventaire\\Results\\iText.pdf"
        public static string PDF_DEST = HostingEnvironment.MapPath("\\Results\\temp_pdf.pdf");
        public static string LOGO_DEST = HostingEnvironment.MapPath("\\Content\\img\\logo_dark.png");
        public static string BASE_DIR = HostingEnvironment.MapPath("https://localhost:44389/");
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
    }
}