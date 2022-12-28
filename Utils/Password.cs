using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class Password
    {
        public string password_string;
        public Password(string password)
        {
            this.password_string = password.Trim(); // Remove leading and trailing whitespace
        }

        
        public bool CheckSpecialChar()
        { 
            List<string> special_chars = new List<string>()
            { "&","ç","$","ù","%", "~","@",
                "#","+","£", "é", "§", "?", "!",
                "-", "è", "_", "à", "=", "^"
            };
            return special_chars.Any(s => this.password_string.Contains(s));
        }
    
        public bool CheckLen()
        {
            if(this.password_string.Length >= 8)
            { return true; }
            return false;
        }

        public bool CheckInteger()
        {
            return this.password_string.Any(char.IsDigit);
        }

        /// <summary>
        /// Check if the password contains a least both a lower case and an upper case.
        /// </summary>
        /// <returns> Bool </returns>
        public bool CheckCase()
        {
            return this.password_string.Any(char.IsUpper) && this.password_string.Any(char.IsLower);
        }

        public bool CheckComplete()
        {
            if (this.CheckCase() & this.CheckInteger() & this.CheckLen() & this.CheckSpecialChar())
            {
                return true;
            }
            return false;
        }
    }

    
}