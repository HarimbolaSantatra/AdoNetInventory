using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class Role
    {
        public int ID { get; set; }

        [Display(Name = "Rôle")]
        public string RoleName { get; set; }


        public bool? writePermission { get; set; }
        public List<User> Users { get; set; }
    }
}