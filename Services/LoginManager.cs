using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using AppInventaire.Models;

namespace AppInventaire.Services
{
    public class LoginManager
    {
        public void LogUserByEmail(string Email)
        {
            FormsAuthentication.SetAuthCookie(Email, false);
            // Create cookie ticket 
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(1), // value of time out property
                false,                      // Value of 'IsPersistent' property
                String.Empty,               // User Data
                FormsAuthentication.FormsCookiePath
            );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);   // Encrypt ticket

            // Save logged user info
            HttpContext.Current.Session["Email"] = Email;
            UserRepository _user_rep = new UserRepository();
            User loggedUser = _user_rep.FetchByEmail(Email);
            _user_rep.CloseConnection();
            HttpContext.Current.Session["userRole"] = loggedUser.userRole.RoleName;
            HttpContext.Current.Session["userId"] = loggedUser.ID;
        }
    }
}