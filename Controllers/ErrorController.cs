using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppInventaire.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult PasswordError(string message)
        {
            ViewBag.message = message;
            return View();
        }

        public ActionResult LinkExpired()
        {
            return View();
        }
    }
}