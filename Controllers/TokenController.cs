using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;
using AppInventaire.Services;
using AppInventaire.Utils;

namespace AppInventaire.Controllers
{
    public class TokenController : Controller
    {
        public ActionResult UserDetails(string token_key)
        {
            if (!TokenManager.VerifyToken(token_key)) return RedirectToAction("Login", "Home");

            // connect as the owner of the ticker
            UserRepository _user_rep = new UserRepository();
            TokenRepository _tok_rep = new TokenRepository();
            DetailsToken token = _tok_rep.FetchSingleDetails(token_key);
            _tok_rep.CloseConnection();
            User tokenOwner = _user_rep.FetchSingle(token.UserId);
            LoginManager.LogUserIfNotLogged(tokenOwner);

            return RedirectToAction("Details", "User", new { id = token.AddedUserId });
        }

        public ActionResult NewPassword(string token_key)
        {
            if (!TokenManager.VerifyToken(token_key)) return RedirectToAction("LinkExpired", "Error");

            // connect as the owner of the ticket
            TokenRepository _tok_rep = new TokenRepository();
            Token token = _tok_rep.FetchSingle(token_key);
            UserRepository _user_rep = new UserRepository();
            User tokenOwner = _user_rep.FetchSingle(token.UserId);
            _tok_rep.CloseConnection(); _user_rep.CloseConnection();
            LoginManager.LogUserIfNotLogged(tokenOwner);

            return RedirectToAction("NewPassword", "User");
        }
    }
}