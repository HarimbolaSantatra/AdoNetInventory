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
    public class ItemController : Controller
    {
        ItemRepository _rep = new ItemRepository();

        public ItemController()
        {
            ItemRepository _rep = new ItemRepository();
        }

        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Item> items = _rep.Fetch();
                return View(items);
        }

        public ActionResult Create(Item item_instance, FormCollection collection)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            ItemRepository item_repo = new ItemRepository();

            // Appeler la fonction AddItem pour ajouter un element dans la table MySql
            if(Request.HttpMethod == "POST") // Si qqch est entré sur la forme
            {

                bool brandInputValid = collection["brandInput"] != null && !String.IsNullOrWhiteSpace(collection["brandInput"]);
                bool typeInputValid = collection["typeInput"] != null && !String.IsNullOrWhiteSpace(collection["typeInput"]);

                if (brandInputValid)
                {
                    _rep.AddBrand(collection["brandInput"].ToString());
                }

                if (typeInputValid)
                {
                    _rep.AddType(collection["typeInput"].ToString());
                }

                // Form validation
                if (ModelState.IsValid && !typeInputValid && !brandInputValid)
                {
                    string type = Validation.StringOrNull(collection["Type"]);
                    string brand = Validation.StringOrNull(collection["Brand"]);
                    string model = Validation.StringOrNull(collection["Model"]);
                    string serial_number = Validation.StringOrNull(collection["SerialNumber"]);
                    string quantity = Validation.StringOrNull(collection["Quantity"]);
                    string comment = Validation.StringOrNull(collection["Comment"]);
                    item_repo.AddItem(type, brand, model, serial_number, quantity, comment);
                    return RedirectToAction("Index");
                }
            }
            return View(item_instance);
        }

        public ActionResult Details(int id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Item> items = _rep.Fetch();
            Item single_item = items.Single(i => i.ID == id);
            return View(single_item);
        }

        public ActionResult Edit(Item item_instance, int id, FormCollection collection)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Item> items = _rep.Fetch();
            Item single_item = items.Single(i => i.ID == id);
            if (Request.HttpMethod == "POST")
            {

                bool brandInputValid = collection["brandInput"] != null && !String.IsNullOrWhiteSpace(collection["brandInput"]);
                bool typeInputValid = collection["typeInput"] != null && !String.IsNullOrWhiteSpace(collection["typeInput"]);

                if (brandInputValid)
                {
                    _rep.AddBrand(collection["brandInput"].ToString());
                }

                if (typeInputValid)
                {
                    _rep.AddType(collection["typeInput"].ToString());
                }

                // Form validation
                if (ModelState.IsValid && !brandInputValid && !typeInputValid)
                {
                    ItemRepository item_repo = new ItemRepository();
                    string type = Validation.StringOrNull(collection["Type"]);
                    string brand = Validation.StringOrNull(collection["Brand"]);
                    string model = Validation.StringOrNull(collection["Model"]);
                    string serial_number = Validation.StringOrNull(collection["SerialNumber"]);
                    string quantity = Validation.StringOrNull(collection["Quantity"]);
                    string comment = Validation.StringOrNull(collection["Comment"]);
                    item_repo.EditItem(id, type, brand, model, serial_number, quantity, comment);
                    return RedirectToAction("Index");
                }
            }
            return View(single_item);
        }

        public ActionResult Delete(int id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Item> items = _rep.Fetch();
            Item single_item = items.Single(i => i.ID == id);
            if (Request.HttpMethod == "POST")
            {
                ItemRepository item_repo = new ItemRepository();
                item_repo.DeleteItem(id);
                return RedirectToAction("Index");
            }
            return View(single_item);
        }

        public ActionResult PrintList(List<Item> Items)
        {
            List<Item> ItemList = _rep.Fetch();
            _rep.CloseConnection();

            // fields: list of name of column
            List<String> fields = ModelUtils.GetModelPropertiesName(ItemList.First());

            float[] float_param = new float[] { 1, 1, 1, 1, 3, 2, 1, 5};
            PdfUtils.CreateTablePdf(ItemList, float_param);

            return File(ProjectVariables.PDF_DEST, MediaTypeNames.Application.Pdf, $"Liste");
        }

        public ActionResult PrintDetails(int id)
        {
            Item item = _rep.FetchSingle(id);
            _rep.CloseConnection();

            // Get value and name of each property
            List<String> value_list = ModelUtils.GetModelPropertiesValue(item);
            List<String> fields = Item.GetPropertiesInFrench();
            
            PdfUtils.GenerateDetails(value_list, fields);

            return File(ProjectVariables.PDF_DEST, MediaTypeNames.Application.Pdf, $"Detail");
        }
    }
}