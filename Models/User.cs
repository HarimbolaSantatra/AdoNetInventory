using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Security.Principal;

namespace AppInventaire.Models
{
    public class User
    {
        public int ID { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Display(Name = "Adresse Mail")]
        [Required(ErrorMessage = "Veuillez entrer une adresse mail valide !")]
        [EmailAddress(ErrorMessage = "Une adresse mail est requis!")]
        public string Email { get; set; }

        // Role: primary key
        public Role userRole;
        
        // Password
        [Display(Name = "Mot De Passe")]
        [Required(ErrorMessage = "Un mot de passe valide est requis !")]
        public string Password { get; set; }

        public Boolean IsActive { get; set; }

        public static List<string> GetPropertiesInFrench()
        {
            return new List<string> { 
                "ID", "Prénom", "Nom", "E-Mail"
            };
        }

        public List<SelectListItem> GetRoleSelectListItems()
        {
            RoleRepository _rep = new RoleRepository();
            List<SelectListItem> RoleSelectListItem = new List<SelectListItem>();
            List<Role> roles = _rep.Fetch();
            foreach (Role ro in roles)
            {
                RoleSelectListItem.Add(new SelectListItem()
                {
                    Text = $"{ro.RoleName}",
                    Value = $"{ro.RoleName}"
                });
            };
            return RoleSelectListItem;
        }

    }
}