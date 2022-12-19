using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class Search
    {
        public int ID { get; set; }
        public string ModelType {get; set; }
        public string Column { get; set; }
        public string ColumnValue { get; set; } // Get an excerpt (extrait) of the result
    }
}