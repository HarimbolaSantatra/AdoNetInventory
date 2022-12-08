using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class Validation
    {
        public static string IntOrNull(string val)
        {
            // Return "NULL" if val is not a int
            if (! int.TryParse(val.ToString(), out _))
            {
                return "NULL";
            }
            return val.ToString();
        }

        public static string FloatOrNull(string val)
        {
            // Return "NULL" if val is not a int
            if (!float.TryParse(val.ToString(), out _))
            {
                return "NULL";
            }
            return val.ToString();
        }

        public static int IntOrZero(string val)
        {
            // Return Zero if the string is in SlqNull format
            if (val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return 0;
            }
            return int.Parse(val.ToString());
        }

        public static float FloatOrZero(string val)
        {
            if (val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return 0;
            }
            return float.Parse(val.ToString());
        }

        public static string StringOrNull(string val)
        {
            // Return "NULL" if string is Null or whitespace 
            if (val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return "NULL";
            }
            return val.ToString();
        }

        public static string StringOrEmpty(string val)
        {
            if(val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return String.Empty;
            }
            return val;
        }
    }
}