using System;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using AppInventaire.Models;

namespace AppInventaire.CustomAttribute
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
                var user_mail = HttpContext.Current.User.Identity.Name;
                
                UserRepository _rep = new UserRepository();
                User user = _rep.FetchByEmail(user_mail);
                string user_role = user.userRole.RoleName;

                if (!String.IsNullOrEmpty(Roles))
                {
                    // Check if user is allowed
                    if(Roles.ToLower() != user_role)
                    {
                        // Redirect to error page
                        filterContext.Result = new RedirectToRouteResult(new
                            RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    }
                }

                _rep.CloseConnection();
            }
        }
    }
}