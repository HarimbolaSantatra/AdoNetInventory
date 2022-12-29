using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class Validation
    {
        public static void Parameter_AddWithValue_ForInt(MySqlCommand cmd, string parameter, string input_variable)
        {
            // Use for MySqlCommand.Parameters.AddWithValue() for integer type.
            if (! int.TryParse(input_variable.ToString(), out _))
            {
                cmd.Parameters.AddWithValue(parameter, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(parameter, input_variable);
            }
        }

        public static string IntOrNull(string val)
        {
            // Return "NULL" if val is not a int
            // Used to clean input: form -> sql
            if (!int.TryParse(val.ToString(), out _))
            {
                return "NULL";
            }
            return val.ToString();
        }

        public static int IntOrZero(string val)
        {
            // Return Zero if val is in SlqNull format; otherwise return an int
            // Used to clean input: sql -> controller
            if (val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return 0;
            }
            return int.Parse(val.ToString());
        }

        public static int IntOrDefault(string val, int default_value)
        {
            if (val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return default_value;
            }
            return int.Parse(val.ToString());
        }

        public static string FloatOrNull(string val)
        {
            // Return "NULL" if val is not a float
            // Used to clean input: form -> sql
            if (!float.TryParse(val.ToString(), out _))
            {
                return "NULL";
            }
            return val.ToString();
        }

        public static float FloatOrZero(string val)
        {
            // Used to clean input: sql -> controller
            if (val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return 0;
            }
            return float.Parse(val.ToString());
        }

        public static string StringOrNull(string val)
        {
            // Return "NULL" if string is Null or whitespace 
            // Used to clean input: sql -> controller
            if (val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return "NULL";
            }
            return val.ToString();
        }

        public static string StringOrEmpty(string val)
        {
            // Used to clean input: sql -> controller
            if (val == "NULL" || string.IsNullOrWhiteSpace(val) || val == null)
            {
                return String.Empty;
            }
            return val;
        }

        public static string EscapeQuotes(string val)
        {
            return val.Replace("\"", "\\\"");
        }

        public static int ForceInRange(int input, int min, int max)
        {
            if (input > max)
            {
                return max;
            }
            if (input < min)
            {
                return min;
            }
            return input;
        }

        public static bool IsInRange(int input, int min, int max)
        {
            if (input > max || input < min)
            {
                return false;
            }
            return true;
        }

    }
}