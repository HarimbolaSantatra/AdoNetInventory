using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppInventaire.Models
{
    public class ItemType
    {
        public int ID { get; set; }
        [Required]
        public string Type { get; set; }
    }
}