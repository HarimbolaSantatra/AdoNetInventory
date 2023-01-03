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
        public ActionResult VerifyToken(int id, string token_key)
        {
            TokenRepository _tok_rep = new TokenRepository();
            DetailsToken token = _tok_rep.FetchSingle(token_key);

            // if token is expired or token doesn't exist
            if(token.isExpired() || token == null)
            {
                _tok_rep.Delete(token.TokenKey);
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }

            // if token is not expired: connect as the admin and redirect to detail page
            if(String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
            {
                // if not connected, ...
                UserRepository _user_rep = new UserRepository();
                User tokenOwner = _user_rep.FetchSingle(id);
                LoginManager loginManager = new LoginManager();
                loginManager.LogUserByEmail(tokenOwner.Email);
            }

            return RedirectToAction("Details", "User", new { id = token.AddedUserId });
        }
    }
}