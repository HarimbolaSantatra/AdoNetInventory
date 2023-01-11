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
        public static void LogOut()
        {
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
        }

        public static void LogUserIfNotLogged(User user)
        {
            // if no User is logged in
            if (String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                // Create cookie ticket 
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    user.Email,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(1), // value of time out property
                    false,                      // Value of 'IsPersistent' property
                    String.Empty,               // User Data
                    FormsAuthentication.FormsCookiePath
                );

                string encryptedTicket = FormsAuthentication.Encrypt(ticket);   // Encrypt ticket

                // Save logged user info
                HttpContext.Current.Session["Email"] = user.Email;
                HttpContext.Current.Session["userRole"] = user.userRole.RoleName;
                HttpContext.Current.Session["userId"] = user.ID;
            }
        }

        public static void LogTokenOwner(string token_key)
        {
            UserRepository _user_rep = new UserRepository();
            User tokenOwner = _user_rep.FetchByToken(token_key);
            _user_rep.CloseConnection();
            LogUserIfNotLogged(tokenOwner);
        }
    }
}