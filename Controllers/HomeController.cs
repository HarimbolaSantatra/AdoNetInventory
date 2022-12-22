using System;
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
            
            return View(searchResult);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
                List<User> UserList = _rep.Fetch();

                // Check if User exist/is valid
                bool doUserExist = (UserList.Any(u => u.Email.ToString() == LoginEmail && u.Password.ToString() == LoginPassword));
                if (doUserExist)
                {
                    FormsAuthentication.SetAuthCookie(LoginEmail, false);

                    // Create cookie ticket 
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        LoginEmail,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30), // value of time out property
                        false,                      // Value of 'IsPersistent' property
                        String.Empty,
                        FormsAuthentication.FormsCookiePath
                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);   // Encrypt ticket
                    Session["Email"] = LoginEmail;

                    UserRepository _user_rep = new UserRepository();
                    @Session["userRole"] = _user_rep.FetchByEmail(Session["Email"].ToString()).userRole.RoleName;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}