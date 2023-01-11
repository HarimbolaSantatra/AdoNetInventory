using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime; // For pdf type file: MediaTypeNames.Application.Pdf
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Utils;
using AppInventaire.CustomAttribute;

namespace AppInventaire.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        ItemRepository _rep = new ItemRepository();

        public ItemController()
        {
            ItemRepository _rep = new ItemRepository();
        }

        public ActionResult Index(int id)
        {
            int samplePerPage = 5;  // Par exemple

            int nbRecord = _rep.FetchRecordNumber();
            int nbPage = (int) Math.Ceiling((double)nbRecord / samplePerPage);

            int currentIndex = id;
            if (!Validation.IsInRange(id, 0, nbPage-1))
            {
                currentIndex = Validation.ForceInRange(id, 0, nbPage - 1);
                return RedirectToAction("Index", new { id = currentIndex });
            }
            List<Item> Items = _rep.FetchSample(samplePerPage, samplePerPage * currentIndex); // Sampled Items

            ViewBag.current_index = currentIndex;
            ViewBag.nbPage = nbPage;

            _rep.CloseConnection();
            return View(Items);
        }

        [AuthorizeCustom(Roles = "Admin")]
        public ActionResult Create(Item item_instance, FormCollection collection)
        {
            ItemRepository _item_repo = new ItemRepository();

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
                    _item_repo.AddItem(type, brand, model, serial_number, quantity, comment);
                    _item_repo.CloseConnection();
                    return RedirectToAction("Index");
                }
            }
            return View(item_instance);
        }

        public ActionResult Details(int id)
        {
            Item single_item = _rep.FetchSingle(id);
            return View(single_item);
        }

        [AuthorizeCustom(Roles = "Admin")]
        public ActionResult Edit(Item item_instance, int id, FormCollection collection)
        {
            Item single_item = _rep.FetchSingle(id);
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
                    ItemRepository _item_repo = new ItemRepository();
                    string type = Validation.StringOrNull(collection["Type"]);
                    string brand = Validation.StringOrNull(collection["Brand"]);
                    string model = Validation.StringOrNull(collection["Model"]);
                    string serial_number = Validation.StringOrNull(collection["SerialNumber"]);
                    string quantity = Validation.StringOrNull(collection["Quantity"]);
                    string comment = Validation.StringOrNull(collection["Comment"]);
                    _item_repo.EditItem(id, type, brand, model, serial_number, quantity, comment);
                    _item_repo.CloseConnection();
                    return RedirectToAction("Index");
                }
            }
            return View(single_item);
        }

        [AuthorizeCustom(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Item single_item = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                ItemRepository _item_repo = new ItemRepository();
                _item_repo.DeleteItem(id);
                _item_repo.CloseConnection();
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

            return File(ProjectVar.PDF_DEST, MediaTypeNames.Application.Pdf, $"Liste");
        }

        public ActionResult PrintDetails(int id)
        {
            Item item = _rep.FetchSingle(id);
            _rep.CloseConnection();

            // Get value and name of each property
            List<String> value_list = ModelUtils.GetModelPropertiesValue(item);
            List<String> fields = Item.GetPropertiesInFrench();
            
            PdfUtils.GenerateDetails(value_list, fields);

            return File(ProjectVar.PDF_DEST, MediaTypeNames.Application.Pdf, $"Detail");
        }
    }
}