using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AppInventaire.Models;

namespace AppInventaire.Utils
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {
        // Les variables Users & Roles sont deux string contenant/definissant les utilisateurs/rôles autorisés.
        public AuthorizeCustomAttribute()
        {
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (!String.IsNullOrEmpty(Users))
                {
                    // Redirect to error page
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Item", action = "Index" }));
                }
            }
        }
    }
}