using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Utils;

namespace AppInventaire.Controllers
{
    public class ComputerController : Controller
    {
        ComputerRepository _rep = new ComputerRepository();

        public ComputerController()
        {
            ComputerRepository _rep = new ComputerRepository();
        }

        public ActionResult Index()
        {
            // Session check
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Computer> Computers = _rep.Fetch();
            _rep.CloseConnection();
            return View(Computers);
        }

        public ActionResult Create(Computer computer_instance, FormCollection collection)
        {
            // Session check
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ComputerRepository Computer_repo = new ComputerRepository();

            if (Request.HttpMethod == "POST")
            {
                if(ModelState.IsValid)
                {
                    Computer_repo.AddComputer(
                        collection["Brand"].ToString(),
                        Validation.StringOrNull(collection["Model"].ToString()),
                        Validation.StringOrNull(collection["OS"].ToString()),
                        Validation.StringOrNull(collection["Hostname"].ToString()),
                        Validation.StringOrNull(collection["Processor"].ToString()),
                        Validation.FloatOrNull(collection["RamGB"].ToString()),
                        Validation.StringOrNull(collection["GraphCard"].ToString()),
                        Validation.FloatOrNull(collection["GraphCardGB"].ToString())
                    );
                    _rep.CloseConnection();
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

            Computer single_computer = _rep.FetchSingle(id);
            _rep.CloseConnection();
            return View(single_computer);
        }

        public ActionResult Edit(int id, Computer computer_instance, FormCollection collection)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            Computer single_computer = _rep.FetchSingle(id);

            if (Request.HttpMethod == "POST")
            {
                if (ModelState.IsValid)
                {
                    _rep.EditComputer(
                        id,
                        collection["Brand"].ToString(),
                        Validation.StringOrNull(collection["Model"].ToString()),
                        Validation.StringOrNull(collection["OS"].ToString()),
                        Validation.StringOrNull(collection["Hostname"].ToString()),
                        Validation.StringOrNull(collection["Processor"].ToString()),
                        Validation.FloatOrNull(collection["RamGB"].ToString()),
                        Validation.StringOrNull(collection["GraphCard"].ToString()),
                        Validation.FloatOrNull(collection["GraphCardGB"].ToString())
                    );
                    return RedirectToAction("Index");
                }
            }
            return View(single_computer);
        }

        public ActionResult Delete(int id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            Computer single_computer = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                _rep.DeleteComputer(id);
                return RedirectToAction("Index");
            }
            return View(single_computer);
        }
    }
}