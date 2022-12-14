using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // For ForeignKey

namespace AppInventaire.Models
{
    public class Raspberry
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la version")]
        public int Rasp_Version { get; set; }

        [Display(Name = "Système d'exploitation")]
        public string OS { get; set; }

        [Display(Name = "Ecran")]
        public int? Screen { get; set; }

        [Display(Name = "Client")]
        public string Client { get; set; }

        [Display(Name = "Accessoires")]
        public string Accessories { get; set; }

        [Display(Name = "Commentaire")]
        public string Comment { get; set; }

        [Display(Name = "Date de Création")]
        public DateTime CreationDate { get; set; }

        public static List<string> GetPropertiesInFrench()
        {
            return new List<string> {
                "ID", "Version", "OS", "Ecran", "Client", "Accessoires","Commentaire","Date de Création"
            };
        }

    }
}