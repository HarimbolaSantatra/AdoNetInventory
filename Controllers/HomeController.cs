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
using AppInventaire.Services;

namespace AppInventaire.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index(FormCollection collection)
        {
            if(Request.HttpMethod == "POST")
            {
                if (collection["searchInput"] != null && !String.IsNullOrWhiteSpace(collection["searchInput"]))
                {
                    // SEARCH
                    SearchRepository _rep = new SearchRepository();
                    string searchQuery = collection["searchInput"];
                    ViewBag.searchQuery = searchQuery;
                    List<Search> searchResult = _rep.FetchResult(searchQuery);
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
                        LoginManager.LogUserIfNotLogged(loggedUser);
                        return RedirectToAction("Index", "Home");
                    }
                }
                _rep.CloseConnection();
            }
            return View();
        }

        public ActionResult Login()
        {
            LoginManager.LogOut();
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            LoginManager.LogOut();
            return RedirectToAction("Index", "Home");
        }

    }
}