using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AppInventaire.Utils;

namespace AppInventaire.Models
{
    public class NewPasswordViewModel
    {
        [Display(Name = "Adresse Mail")]
        [EmailAddress(ErrorMessage = "L'adresse mail n'est pas valide !")]
        [Required(ErrorMessage = "Champ Requis !")]
        public string Email { get; set; }

        [Display(Name = "Mot De Passe")]
        [MinLength(8, ErrorMessage = "Mot de passe trop courte !")]
        [Required(ErrorMessage = "Champ Requis !")]
        [PasswordCustom]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Erreur de confirmation, Veuillez réessayer!")]
        public string ConfirmPassword { get; set; }
    }
}