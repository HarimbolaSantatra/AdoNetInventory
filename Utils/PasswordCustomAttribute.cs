using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppInventaire.Utils
{
    /// <summary>
    /// Implement a custom passwrd validation for User Model
    /// </summary>
    public class PasswordCustomAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // value: the value to validate
            // validationContext: access to model type, model object instance, and display name

            // Return Success if there's no value yet
            if (value == null) return ValidationResult.Success;

            // Create a password object
            PasswordUtils password = new PasswordUtils(value.ToString());

            if (!password.CheckCase())
            {
                return new ValidationResult("Le mot de passe doit contenir au moins une lettre majuscule et une minuscule");
            }
            if(!password.CheckInteger())
            {
                return new ValidationResult("Le mot de passe doit contenir au moins une chiffre");
            }
            if(!password.CheckSpecialChar())
            {
                return new ValidationResult("Le mot de passe doit contenir au moins un caractère spécial.");
            }
            return ValidationResult.Success;
        }
    }
}