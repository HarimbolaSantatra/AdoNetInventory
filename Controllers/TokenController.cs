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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> ID of the owner of the token. Must be an admin. </param>
        /// <param name="token_key"> Unique Key </param>
        /// <returns> Redirect to the request page or to login page </returns>
        public ActionResult VerifyToken(string token_key)
        {
            TokenRepository _tok_rep = new TokenRepository();
            DetailsToken token = _tok_rep.FetchSingleDetails(token_key);

            // if token is expired or token doesn't exist
            if(token.isExpired() || token == null)
            {
                _tok_rep.Delete(token.TokenKey);
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }

            // is no User is logged in
            if(String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
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
    }
}