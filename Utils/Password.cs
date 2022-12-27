using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class Password
    {
        public string passwd;
        Password(string passwd)
        {
            this.passwd = passwd;
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
    
        public bool CheckLen(string passwd)
        {
            if(passwd.Length >= 8)
            { return true; }
            return false;
        }
    }

    
}