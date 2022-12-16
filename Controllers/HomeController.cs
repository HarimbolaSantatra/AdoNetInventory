using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;

namespace AppInventaire.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public ActionResult About()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(FormCollection collection)
        {

            if(Session["Email"] != null)
            {
                Session.Clear(); // Close Session = Logout
            }

            if (Request.HttpMethod == "POST")
            {
                // Save info
                var LoginEmail = collection["inputEmail"].ToString();
                var LoginPassword = collection["inputPassword"].ToString();

                if ( !(String.IsNullOrWhiteSpace(LoginEmail)) && !(String.IsNullOrWhiteSpace(LoginPassword)) )
                {
                    UserRepository _rep = new UserRepository();
                    List<User> UserList = _rep.Fetch();

                    // Check if User exist/is valid
                    bool c1 = (UserList.Any(u => u.Email.ToString() == LoginEmail && u.Password.ToString() == LoginPassword));
                    if (c1)
                    {
                        // Get first element
                        User LoggedUser = UserList.FirstOrDefault(u => u.Email == LoginEmail && u.Password == LoginPassword);
                        // Save user session
                        Session["Email"] = LoginEmail;
                        Session["FirstName"] = LoggedUser.FirstName;
                        Session["LastName"] = LoggedUser.LastName;
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        public ActionResult Search(FormCollection collection)
        {
            SearchRepository _rep = new SearchRepository();
            List<Search> searchResult = null;
            if (Request.HttpMethod == "GET")
            {
                string searchQuery = collection["searchInput"];
                searchResult = _rep.FetchResult(searchQuery);
            }
            return View(searchResult);
        }
    }
}