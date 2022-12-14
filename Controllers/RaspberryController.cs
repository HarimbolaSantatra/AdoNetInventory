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
    public class RaspberryController : Controller
    {
        RaspberryRepository _rep = new RaspberryRepository();

        public RaspberryController()
        {
            RaspberryRepository _rep = new RaspberryRepository();
        }

        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            // Extraire les Raspberrys
            List<Raspberry> Raspberrys = _rep.Fetch();
            return View(Raspberrys);

        }

        public ActionResult Create(Raspberry rasp_instance, FormCollection collection )
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Request.HttpMethod == "POST")
            {
                if(ModelState.IsValid)
                {
                    string Rasp_Version = Validation.IntOrNull(collection["Rasp_Version"]);
                    string Screen = Validation.IntOrNull(collection["Screen"]);
                    string OS = Validation.StringOrNull(collection["OS"]);
                    string Client = Validation.StringOrNull(collection["Client"]);
                    string Accessories = Validation.StringOrNull(collection["Accessories"]);
                    string Comment = Validation.StringOrNull(collection["Comment"]);
                    _rep.AddRaspberry(Rasp_Version, OS, Screen, Client, Accessories, Comment);
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

            Raspberry single_Raspberry = _rep.FetchSingle(id);
            return View(single_Raspberry);
        }

        public ActionResult Edit(Raspberry rasp_instance, int id, FormCollection collection)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            Raspberry single_Raspberry = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                if (ModelState.IsValid)
                {
                    string Rasp_Version = Validation.IntOrNull(collection["Rasp_Version"]);
                    string Screen = Validation.IntOrNull(collection["Screen"]);
                    string OS = Validation.StringOrNull(collection["OS"]);
                    string Client = Validation.StringOrNull(collection["Client"]);
                    string Accessories = Validation.StringOrNull(collection["Accessories"]);
                    string Comment = Validation.StringOrNull(collection["Comment"]);
                    _rep.EditRaspberry(id, Rasp_Version, OS, Screen, Client, Accessories, Comment);
                    return RedirectToAction("Index");
                }
            }
            return View(single_Raspberry);
        }

        public ActionResult Delete(int id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Raspberry> Raspberrys = _rep.Fetch();
            Raspberry single_Raspberry = Raspberrys.Single(i => i.ID == id);
            if (Request.HttpMethod == "POST")
            {
                _rep.DeleteRaspberry(id);
                return RedirectToAction("Index");
            }
            return View(single_Raspberry);
        }

        public ActionResult PrintList(List<Raspberry> Raspberrys)
        {
            List<Raspberry> RaspberryList = _rep.Fetch();
            _rep.CloseConnection();

            // fields: list of name of column
            List<String> fields = ModelUtils.GetModelPropertiesName(RaspberryList.First());

            string html_string = PdfUtils.GenerateHtmlTable(RaspberryList);
            PdfUtils.CreatePdf(html_string, ProjectVariables.PDF_DEST);

            return File(ProjectVariables.PDF_DEST, MediaTypeNames.Application.Pdf, $"Liste");
        }

        public ActionResult PrintDetails(int id)
        {
            Raspberry raspberry = _rep.FetchSingle(id);
            _rep.CloseConnection();

            // Get value and name of each property
            List<String> value_list = ModelUtils.GetModelPropertiesValue(raspberry);
            List<String> fields = Raspberry.GetPropertiesInFrench();

            string html_string = PdfUtils.GenerateHtmlDetails(value_list, fields);
            PdfUtils.CreatePdf(html_string, ProjectVariables.PDF_DEST);

            return File(ProjectVariables.PDF_DEST, MediaTypeNames.Application.Pdf, $"Detail");
        }
    }
}