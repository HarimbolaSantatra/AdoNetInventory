using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Services;

namespace AppInventaire.Controllers
{
    public class TokenController : Controller
    {

        public ActionResult UserDetails(string token_key)
        {
            TokenRepository _tok_rep = new TokenRepository();
            DetailsToken token = _tok_rep.FetchSingleDetails(token_key);
            _tok_rep.CloseConnection();

            if (!TokenManager.Verify(token)) return RedirectToAction("Login", "Home");

            // is no User is logged in
            if (String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
            {
                // connect as the owner of the ticker and redirect to detail page
                UserRepository _user_rep = new UserRepository();
                User tokenOwner = _user_rep.FetchSingle(token.UserId);
                _user_rep.CloseConnection();
                LoginManager loginManager = new LoginManager();
                loginManager.LogUserByEmail(tokenOwner.Email);
            }

            return RedirectToAction("Details", "User", new { id = token.AddedUserId });
        }

        public ActionResult ForgotPassword(User user)
        {
            return RedirectToAction("NewPassword", "Home", new { id = user.ID});
        }
    }
}