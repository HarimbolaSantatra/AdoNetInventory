using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime; // For pdf type file: MediaTypeNames.Application.Pdf
using System.Web.Security;
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Utils;
using AppInventaire.Services;

namespace AppInventaire.Controllers
{
    [Authorize]
    [AuthorizeCustom(Roles = "Admin")]
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
                if (collection["roleInput"] != null && !String.IsNullOrWhiteSpace(collection["roleInput"]))
                {
                    RoleRepository _role_rep = new RoleRepository();
                    _role_rep.AddRole(collection["roleInput"].ToString());
                    _role_rep.CloseConnection();
                }
                if (ModelState.IsValid)
                {
                    string Passwd = Validation.StringOrNull(collection["Password"]);
                    Password password_object = new Password(Passwd);
                    if(password_object.CheckComplete())
                    {
                        string FirstName = Validation.StringOrNull(collection["FirstName"]);
                        string LastName = Validation.StringOrNull(collection["LastName"]);
                        string Email = Validation.StringOrNull(collection["Email"]);
                        int userRoleId = Validation.IntOrDefault(collection["userRole"], 1);
                        _rep.AddUser(FirstName, LastName, Email, password_object.password_string, userRoleId);

                        // Get RoleName
                        RoleRepository _role_rep = new RoleRepository();
                        string role_name = _role_rep.FetchSingle(userRoleId).RoleName;
                        _role_rep.CloseConnection();

                        // Send Email to Admin
                        EmailSender emailSender = new EmailSender(
                            ProjectVar.ADMIN_EMAIL_ANDRANA,     // Sender
                            ProjectVar.ADMIN_EMAIL_YAHOO,     // Receiver
                            ProjectVar.ADMIN_PWD_ANDRANA,
                            ProjectVar.ADMIN_EMAIL_ANDRANA);      // Sender password
                        emailSender.NotifyCreateUser(FirstName, LastName, Email, role_name);

                        return RedirectToAction("Index");
                    }
                }
            }
            return View(user_instance);
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
                if (collection["roleInput"] != null && !String.IsNullOrWhiteSpace(collection["roleInput"]))
                {
                    RoleRepository _role_rep = new RoleRepository();
                    _role_rep.AddRole(collection["roleInput"].ToString());
                    _role_rep.CloseConnection();
                    return RedirectToAction("Edit", new { id = id });
                }

                if (collection["Email"] != null && !String.IsNullOrWhiteSpace(collection["Email"]))
                {
                    string FirstName = Validation.StringOrNull(collection["FirstName"]);
                    string LastName = Validation.StringOrNull(collection["LastName"]);
                    string Email = Validation.StringOrNull(collection["Email"]);
                    int userRoleId = Validation.IntOrDefault(collection["userRole"], 1);
                    _rep.EditUser(id, FirstName, LastName, Email, userRoleId);
                    return RedirectToAction("Index");
                }
            }
            return View(single_User);
        }

        public ActionResult EditPassword(int id, FormCollection collection)
        {
            User singleUser = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                if (!String.IsNullOrWhiteSpace(collection["old_password"].ToString()) &&
                    !String.IsNullOrWhiteSpace(collection["new_password"].ToString()) &&
                    !String.IsNullOrWhiteSpace(collection["new_password_confirm"].ToString()))
                { 
                    Password oldPasswordInput = new Password(collection["old_password"].ToString());
                    string hashedOldPassword = Operation.Sha1Hash(oldPasswordInput.password_string);
                    if (!String.Equals(singleUser.Password, hashedOldPassword))
                    {
                        // If old password is not correct
                        string Message = "Ancien mot de passe incorrect";
                        return RedirectToAction("PasswordError", "Error", new { message = Message }); 
                    }
                    
                    if(!String.Equals( collection["new_password"].ToString(), collection["new_password_confirm"].ToString() ))
                    {
                        // If new password not confirmed 
                        string Message = "Les deux nouveaux mot de passe ne sont pas similaires.";
                        return RedirectToAction("PasswordError", "Error", new { message = Message }); 
                    }

                    Password newPassword = new Password(collection["new_password"].ToString());
                    if(!newPassword.CheckComplete())
                    {
                        // Check Password validation 
                        string Message = "Nouveau mot de passe de format invalide.";
                        return RedirectToAction("PasswordError", "Error", new { message=Message }); 
                    }
                    _rep.EditPassword(id, newPassword.password_string);
                    _rep.CloseConnection();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            User singleUser = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                _rep.DeleteUser(id);

                // Send Email
                EmailSender emailSender = new EmailSender(
                            ProjectVar.ADMIN_EMAIL_ANDRANA,     // Sender
                            ProjectVar.ADMIN_EMAIL_YAHOO,     // Receiver
                            ProjectVar.ADMIN_PWD_ANDRANA);      // Sender password
                emailSender.NotifyDeleteUser(
                    singleUser.FirstName, singleUser.LastName, 
                    singleUser.Email, singleUser.userRole.RoleName);

                return RedirectToAction("Index");
            }
            return View(singleUser);
        }

        public ActionResult PrintList(List<Computer> Users)
        {
            List<User> UserList = _rep.Fetch();
            _rep.CloseConnection();

            float[] float_param = new float[] { 1, 3, 4, 3};
            PdfUtils.CreateTablePdf(UserList, float_param);

            return File(ProjectVar.PDF_DEST, MediaTypeNames.Application.Pdf, $"Liste");
        }

        public ActionResult PrintDetails(int id)
        {
            User user= _rep.FetchSingle(id);
            _rep.CloseConnection();
            
            // Get value and name of each property
            List<String> value_list = ModelUtils.GetModelPropertiesValue(user);
            List<String> fields = Models.User.GetPropertiesInFrench();
            
            PdfUtils.GenerateDetails(value_list, fields);

            return File(ProjectVar.PDF_DEST, MediaTypeNames.Application.Pdf, $"Detail");
        }
    }
}