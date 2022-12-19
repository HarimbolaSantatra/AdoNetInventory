using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime; // For pdf type file: MediaTypeNames.Application.Pdf
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
            List<User> Users = _rep.Fetch();
            return View(Users);
        }

        public ActionResult Create(User user_instance, FormCollection collection)
        {
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
            User single_User = _rep.FetchSingle(id);
            return View(single_User);
        }

        public ActionResult Edit(User user_instance, int id, FormCollection collection)
        {
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
            User single_User = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                _rep.DeleteUser(id);
                return RedirectToAction("Index");
            }
            return View(single_User);
        }

        public ActionResult PrintList(List<Computer> Users)
        {
            List<User> UserList = _rep.Fetch();
            _rep.CloseConnection();

            float[] float_param = new float[] { 1, 3, 4, 3};
            PdfUtils.CreateTablePdf(UserList, float_param);

            return File(ProjectVariables.PDF_DEST, MediaTypeNames.Application.Pdf, $"Liste");
        }

        public ActionResult PrintDetails(int id)
        {
            User user= _rep.FetchSingle(id);
            _rep.CloseConnection();
            
            // Get value and name of each property
            List<String> value_list = ModelUtils.GetModelPropertiesValue(user);
            List<String> fields = Models.User.GetPropertiesInFrench();
            
            PdfUtils.GenerateDetails(value_list, fields);

            return File(ProjectVariables.PDF_DEST, MediaTypeNames.Application.Pdf, $"Detail");
        }
    }
}