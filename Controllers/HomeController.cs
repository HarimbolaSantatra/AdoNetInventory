using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Utils;
using System.Web.Security;

namespace AppInventaire.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index(FormCollection collection)
        {
            List<Search> searchResult = null;
            if(Request.HttpMethod == "POST")
            {
                if (collection["searchInput"] != null && !String.IsNullOrWhiteSpace(collection["searchInput"]))
                {
                    SearchRepository _rep = new SearchRepository();
                    string searchQuery = collection["searchInput"];
                    ViewBag.searchQuery = searchQuery;
                    searchResult = _rep.FetchResult(searchQuery);
                    return View("Search", searchResult);
                }
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // Save info
            var LoginEmail = collection["inputEmail"].ToString();
            var LoginPassword = collection["inputPassword"].ToString();

            if ( !(String.IsNullOrWhiteSpace(LoginEmail)) && !(String.IsNullOrWhiteSpace(LoginPassword)) )
            {
                UserRepository _rep = new UserRepository();

                // Fetch user by email
                User loggedUser;
                loggedUser = _rep.FetchByEmail(LoginEmail);
                // Check if User exist
                if (loggedUser != null)
                {
                    string HashedPassword = Operation.Sha1Hash(LoginPassword);

                    // Check if Password is true
                    if( String.Equals(loggedUser.Password, HashedPassword) )
                    {
                        FormsAuthentication.SetAuthCookie(LoginEmail, false);

                        // Create cookie ticket 
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                            1,
                            LoginEmail,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(1), // value of time out property
                            false,                      // Value of 'IsPersistent' property
                            String.Empty,
                            FormsAuthentication.FormsCookiePath
                        );

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);   // Encrypt ticket
                        // Save logged user info
                        Session["Email"] = LoginEmail;
                        Session["userRole"] = loggedUser.userRole.RoleName;
                        Session["userId"] = loggedUser.ID;
                        return RedirectToAction("Index", "Home");
                    }
                }
                _rep.CloseConnection();
            }
            return View();
        }

        public ActionResult Login()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}