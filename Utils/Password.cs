using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class Password
    {
        private string passwd;
        Password(string password)
        {
            passwd = password.Trim(); // Remove leading and trailing whitespace
        }

        
        public bool CheckSpecialChar()
        { 
            List<string> special_chars = new List<string>()
            { "&","ç","$","ù","%", "~","@",
                "#","+","£", "é", "§", "?", "!",
                "-", "è", "_", "à", "=", "^"
            };
            return special_chars.Any(s => passwd.Contains(s));
        }
    
        public bool CheckLen()
        {
            if(passwd.Length >= 8)
            { return true; }
            return false;
        }

        public bool CheckInteger()
        {
            return passwd.Any(char.IsDigit);
        }

        /// <summary>
        /// Check if the password contains a least both a lower case and an upper case.
        /// </summary>
        /// <returns> Bool </returns>
        public bool CheckCase()
        {
            return passwd.Any(char.IsUpper) && passwd.Any(char.IsLower);
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