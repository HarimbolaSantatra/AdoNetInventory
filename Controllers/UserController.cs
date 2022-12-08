using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Utils;

namespace AppInventaire.Controllers
{
    public class UserController : Controller
    {
        UserRepository _rep = new UserRepository();

        public UserController()
        {
            UserRepository _rep = new UserRepository();
        }

        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<User> Users = _rep.Fetch();
            return View(Users);
        }

        public ActionResult Create(User user_instance, FormCollection collection)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Request.HttpMethod == "POST")
            {
                if (ModelState.IsValid)
                {
                    string FirstName = Validation.StringOrNull(collection["FirstName"]);
                    string LastName = Validation.StringOrNull(collection["LastName"]);
                    string Email = Validation.StringOrNull(collection["Email"]);
                    string Password = Validation.StringOrNull(collection["Password"]);
                    _rep.AddUser(FirstName, LastName, Email, Password);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            User single_User = _rep.FetchSingle(id);
            return View(single_User);
        }

        public ActionResult Edit(User user_instance, int id, FormCollection collection)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            User single_User = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                if (ModelState.IsValid)
                {
                    string FirstName = Validation.StringOrNull(collection["FirstName"]);
                    string LastName = Validation.StringOrNull(collection["LastName"]);
                    string Email = Validation.StringOrNull(collection["Email"]);
                    string Password = Validation.StringOrNull(collection["Password"]);
                    _rep.EditUser(id, FirstName, LastName, Email, Password);
                    return RedirectToAction("Index");
                }
            }
            return View(single_User);
        }

        public ActionResult Delete(int id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            User single_User = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                _rep.DeleteUser(id);
                return RedirectToAction("Index");
            }
            return View(single_User);
        }
    }
}