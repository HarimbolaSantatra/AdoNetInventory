using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class ModelUtils
    {
        public static System.Reflection.PropertyInfo[] GetModelProperties(Object cObject)
        {
            return cObject.GetType().GetProperties();
        }

        public static List<String> GetModelPropertiesName(Object cObject)
        {
            List<String> properties_names = new List<String>();
            if (cObject != null)
            {
                foreach (var prop in cObject.GetType().GetProperties())
                {
                    properties_names.Add(prop.Name);
                }
            }
            return properties_names;
        }

        public static List<String> GetModelPropertiesValue(Object cObject)
        {
            List<String> property_values = new List<string>();
            if (cObject != null)
            {
                foreach (var prop in cObject.GetType().GetProperties())
                {
                    property_values.Add(prop.GetValue(cObject).ToString());
                }
            }
            return property_values;
        }
    }
}