using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppInventaire.CustomAttribute
{
    public class CorrectEmailCustomAttribute : ValidationAttribute
    {
        public object CorrectEmail = HttpContext.Current.Session["Email"];
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Valid only if value == email of the logged user
            if (CorrectEmail == null) return new ValidationResult(" Utilisateur non connecté !");

            if (!String.Equals(value, CorrectEmail.ToString()))
            {
                return new ValidationResult("Echec de confirmation de l'adresse mail !");
            }

            return ValidationResult.Success;
        }
    }
}