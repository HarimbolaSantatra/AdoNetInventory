using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace AppInventaire.Models
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserPrincipal(string UserPrincipalname)
        {
            this.Identity = new GenericIdentity(UserPrincipalname);
        }


        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }

    }
}