using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;
using AppInventaire.Utils;

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
        [EmailAddress(ErrorMessage = "L'adresse mail n'est pas valide !")]
        [Required(ErrorMessage = "Champ Requis !")]
        public string Email { get; set; }

        // Role: primary key
        public Role userRole;
        
        // Password
        [Display(Name = "Mot De Passe")]
        [MinLength(8, ErrorMessage = "Mot de passe trop courte !")]
        [Required(ErrorMessage = "Champ Requis !")]
        [PasswordCustom]
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
                    // Value must be a 'Role' object
                    Value = $"{ro.ID}"
                });
            };
            _rep.CloseConnection();
            return RoleSelectListItem;
        }

    }
}