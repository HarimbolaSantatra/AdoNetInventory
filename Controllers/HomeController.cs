using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Utils;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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
                    return View("Index", new { controller = "Home" });
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Index", new { controller = "Computer" });
        }
    }
}