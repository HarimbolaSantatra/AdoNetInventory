using System;
using System.Collections.Generic;
using System.Net.Mime; // For pdf type file: MediaTypeNames.Application.Pdf
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

        [Authorize]
        public ActionResult Index()
        {
            List<Computer> Computers = _rep.Fetch();
            _rep.CloseConnection();
            return View(Computers);
        }

        [Authorize]
        public ActionResult Create(Computer computer_instance, FormCollection collection)
        {
            ComputerRepository _rep = new ComputerRepository();

            if (Request.HttpMethod == "POST")
            {

                if(collection["brandInput"] != null && !String.IsNullOrWhiteSpace(collection["brandInput"]))
                {
                    _rep.AddComputerBrand(collection["brandInput"].ToString());
                }

                if(ModelState.IsValid)
                {
                    _rep.AddComputer(
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
            return View(computer_instance);
            
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            Computer single_computer = _rep.FetchSingle(id);
            _rep.CloseConnection();
            return View(single_computer);
        }

        [Authorize]
        public ActionResult Edit(int id, Computer computer_instance, FormCollection collection)
        {
            Computer single_computer = _rep.FetchSingle(id);

            if (Request.HttpMethod == "POST")
            {

                if (collection["brandInput"] != null && !String.IsNullOrWhiteSpace(collection["brandInput"]))
                {
                    _rep.AddComputerBrand(collection["brandInput"].ToString());
                    return RedirectToAction("Edit", new { id=id });
                }

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

        [Authorize]
        public ActionResult Delete(int id)
        {
            Computer single_computer = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                _rep.DeleteComputer(id);
                return RedirectToAction("Index");
            }
            return View(single_computer);
        }

        public ActionResult PrintList(List<Computer> Computers)
        {
            List<Computer> ComputerList = _rep.Fetch();
            _rep.CloseConnection();

            // fields: list of name of column
            List<String> fields = ModelUtils.GetModelPropertiesName(ComputerList.First());

            PdfUtils.CreateTablePdf(ComputerList, new float[] {1, 1, 1, 2, 2, 3, 1, 3, 1 });

            return File(ProjectVariables.PDF_DEST, MediaTypeNames.Application.Pdf, $"Liste");
        }

        public ActionResult PrintDetails(int id)
        {
            Computer computer = _rep.FetchSingle(id);
            _rep.CloseConnection();

            // Get value and name of each property
            List<String> value_list = ModelUtils.GetModelPropertiesValue(computer);
            List<String> fields = Computer.GetPropertiesInFrench();

            PdfUtils.GenerateDetails(value_list, fields);

            return File(ProjectVariables.PDF_DEST, MediaTypeNames.Application.Pdf, $"Detail");
        }


    }
}