using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AppInventaire.CustomAttribute;

namespace AppInventaire.Models
{
    public class NewPassword
    {
        string LoggedUserEmail = HttpContext.Current.Session["Email"].ToString();
        [Display(Name = "Adresse Mail")]
        [EmailAddress(ErrorMessage = "L'adresse mail n'est pas valide !")]
        [Required(ErrorMessage = "Champ Requis !")]
        [CorrectEmailCustom]
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