using System;
using System.Collections.Generic;
using System.Net.Mime; // For pdf type file: MediaTypeNames.Application.Pdf
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Utils;
using AppInventaire.CustomAttribute;
using AppInventaire.Services;

namespace AppInventaire.Controllers
{
    
    public class UserController : Controller
    {
        UserRepository _rep = new UserRepository();

        public UserController()
        {
            
        }

        [Authorize]
        [AuthorizeCustom(Roles = "Admin")]
        public ActionResult Index()
        {
            List<User> Users = _rep.Fetch();
            return View(Users);
        }

        [Authorize]
        [AuthorizeCustom(Roles = "Admin")]
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
                    PasswordUtils password_object = new PasswordUtils(Passwd);
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
                            ProjectVar.SENDER_EMAIL,     // Sender
                            ProjectVar.ADMIN_EMAIL,     // Receiver
                            ProjectVar.SENDER_PWD,      // Sender password
                            ProjectVar.CC_EMAIL);      // CC
                        emailSender.NotifyCreateUser(FirstName, LastName, Email, role_name);

                        // Return Json object with no error. (For the popup)
                        return Json(" { error : 0 } ");
                    }
                }
            }
            return View(user_instance);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            User single_User = _rep.FetchSingle(id);
            return View(single_User);
        }

        [Authorize]
        [AuthorizeCustom(Roles = "Admin")]
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

        [Authorize]
        [AuthorizeCustom(Roles = "Admin")]
        public ActionResult EditPassword(int id, FormCollection collection)
        {
            User singleUser = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                if (!String.IsNullOrWhiteSpace(collection["old_password"].ToString()) &&
                    !String.IsNullOrWhiteSpace(collection["new_password"].ToString()) &&
                    !String.IsNullOrWhiteSpace(collection["new_password_confirm"].ToString()))
                { 
                    PasswordUtils oldPasswordInput = new PasswordUtils(collection["old_password"].ToString());
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

                    PasswordUtils newPassword = new PasswordUtils(collection["new_password"].ToString());
                    if(!newPassword.CheckComplete())
                    {
                        // Check Password validation 
                        string Message = "Nouveau mot de passe de format invalide.";
                        return RedirectToAction("PasswordError", "Error", new { message=Message }); 
                    }
                    _rep.EditPassword(id, newPassword.hashed);
                    _rep.CloseConnection();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult ForgotPassEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassEmail(FormCollection collection)
        {
            var LoginEmail = collection["inputEmail"].ToString();

            // Create a new  token
            if (!(String.IsNullOrWhiteSpace(LoginEmail)))
            {
                UserRepository _user_rep = new UserRepository();
                Token token = new Token()
                {
                    UserId = _user_rep.FetchByEmail(LoginEmail).ID,
                    TokenKey = Guid.NewGuid().ToString(),
                    CreationDate = DateTime.Now,
                };
                TokenRepository _tok_rep = new TokenRepository();
                _tok_rep.Add(token.UserId, token.TokenKey);

                EmailSender emailSender = new EmailSender(
                    ProjectVar.SENDER_EMAIL,    // Sender
                    LoginEmail,                 // Receiver
                    ProjectVar.SENDER_PWD);     // Sender password
                emailSender.ForgetPassword(token);

                // Confirm to user
                return RedirectToAction("Message", "Home", new { mk = "email_sent" });
            }
            return View();
        }

        [Authorize]
        public ActionResult NewPassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult NewPassword(NewPassword newPass, FormCollection collection)
        {
            string email = collection["Email"];
            string newPassword = collection["ConfirmPassword"];

            if (ModelState.IsValid)
            {
                // Save new password
                PasswordUtils NewPassword = new PasswordUtils(newPassword);
                UserRepository _user_rep = new UserRepository();
                User user = _user_rep.FetchByEmail(email);
                _user_rep.EditPassword(user.ID, NewPassword.hashed);
                _user_rep.CloseConnection();

                // Delete used token
                TokenRepository _tk_rep = new TokenRepository();
                _tk_rep.DeleteByOwner(user);
                _tk_rep.CloseConnection();

                // Disconnect user and Redirect to login
                LoginManager.LogOut();
                return RedirectToAction("Message", "Home", new { mk = "passwd_changed" });
            }
            return View();
        }

        [Authorize]
        [AuthorizeCustom(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            User singleUser = _rep.FetchSingle(id);
            if (Request.HttpMethod == "POST")
            {
                _rep.DeleteUser(id);

                // Send Email
                EmailSender emailSender = new EmailSender(
                            ProjectVar.SENDER_EMAIL,     // Sender
                            ProjectVar.ADMIN_EMAIL,     // Receiver
                            ProjectVar.SENDER_PWD);      // Sender password
                emailSender.NotifyDeleteUser(
                    singleUser.FirstName, singleUser.LastName, 
                    singleUser.Email, singleUser.userRole.RoleName);

                return RedirectToAction("Index");
            }
            return View(singleUser);
        }

        [Authorize]
        [AuthorizeCustom(Roles = "Admin")]
        public ActionResult PrintList(List<Computer> Users)
        {
            List<User> UserList = _rep.Fetch();
            _rep.CloseConnection();

            float[] float_param = new float[] { 1, 3, 4, 3};
            PdfUtils.CreateTablePdf(UserList, float_param);

            return File(ProjectVar.PDF_DEST, MediaTypeNames.Application.Pdf, $"Liste");
        }

        [Authorize]
        [AuthorizeCustom(Roles = "Admin")]
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