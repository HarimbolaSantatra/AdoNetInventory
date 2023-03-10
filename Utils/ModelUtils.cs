using System;
using System.Collections.Generic;
using System.Reflection;
using AppInventaire.Models;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class ModelUtils
    {
        public static PropertyInfo[] GetModelProperties(Object cObject)
        {
            return cObject.GetType().GetProperties();
        }

        public static List<String> GetModelPropertiesName(Object cObject)
        {
            List<String> properties_names = new List<String>();
            if (cObject != null)
            {
                foreach (PropertyInfo prop in GetModelProperties(cObject))
                {
                    if (prop.Name == "Password" || prop.Name == "password")
                    {
                        continue;
                    }
                    else
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
                foreach (PropertyInfo prop in GetModelProperties(cObject))
                {
                    if(prop.Name == "Password" || prop.Name == "password")
                    {
                        continue;
                    }
                    else
                    {
                        property_values.Add(prop.GetValue(cObject, null).ToString());
                    }
                }
            }
            return property_values;
        }

        public static List<String> GetModelPropertiesInFrench(string modelName)
        {
            switch (modelName.ToLower())
            {

                case "computer":
                    return Computer.GetPropertiesInFrench();

                case "item":
                    return Item.GetPropertiesInFrench();

                case "raspberry":
                    return Raspberry.GetPropertiesInFrench();

                case "user":
                    return User.GetPropertiesInFrench();

                default:
                    return new List<String> { null };
            }  
        }

        public static Type GetModelType(string modelName)
        {
            switch (modelName.ToLower())
            {

                case "computer":
                    return typeof(Computer);

                case "item":
                    return typeof(Item);

                case "raspberry":
                    return typeof(Raspberry);

                case "user":
                    return typeof(User);

                default:
                    return null;
            }
        }
    }
}