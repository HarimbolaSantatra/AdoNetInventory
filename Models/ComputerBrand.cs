using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AppInventaire.Models
{
    public class ComputerBrand
    {
        public int ID { get; set; }
        public string Brand { get; set; }

        public static List<string> GetPropertiesInFrench()
        {
            return new List<string> {
                "ID", "Marque"
            };
        }
    }
}