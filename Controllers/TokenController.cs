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
            LoginManager.LogTokenOwner(token_key);

            TokenRepository _tok_rep = new TokenRepository();
            DetailsToken token = _tok_rep.FetchSingleDetails(token_key);
            _tok_rep.CloseConnection();
            return RedirectToAction("Details", "User", new { id = token.AddedUserId });
        }

        public ActionResult NewPassword(string token_key)
        {
            if (!TokenManager.VerifyToken(token_key)) return RedirectToAction("LinkExpired", "Error");

            // connect as the owner of the ticket
            LoginManager.LogTokenOwner(token_key);

            return RedirectToAction("NewPassword", "User");
        }
    }
}