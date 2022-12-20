using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


        public static List<string> GetPropertiesInFrench()
        {
            return new List<string> { 
                "ID", "Prénom", "Nom", "E-Mail"
            };
        }

    }
}