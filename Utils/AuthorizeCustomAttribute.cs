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
                var current_user = HttpContext.Current.User.Identity.Name;
                if (!String.IsNullOrEmpty(Users))
                {
                    // Check if user is allowed
                    if(Users.ToLower() != current_user.ToLower())
                    {
                        // Redirect to error page
                        filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    }
                }
            }
        }
    }
}