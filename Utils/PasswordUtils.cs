using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class PasswordUtils
    {
        public string password_string;
        public string hashed;
        public PasswordUtils(string password)
        {
            this.password_string = password.Trim(); // Remove leading and trailing whitespace
            this.hashed = Operation.Sha1Hash(this.password_string);
        }

        
        public bool CheckSpecialChar()
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (this.password_string.Contains(item)) return true;
            }

            return false;
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

        public static bool CheckConfirm(string password1, string password2)
        {
            return String.Equals(password1, password2);
        }
    }

    
}