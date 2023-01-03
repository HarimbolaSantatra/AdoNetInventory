using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppInventaire.Models;

namespace AppInventaire.Controllers
{
    public class TokenController : Controller
    {
        public ActionResult VerifyToken(string token_key)
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
            // if token is not expired: Redirect to detail page
            return RedirectToAction("Details", "User", new { id = token.DetailsId });
        }
    }
}