using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class ItemBrand
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