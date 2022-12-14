using System;
using System.ComponentModel.DataAnnotations; // Pour validation
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class Item
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Veuillez entrer le type du materiel")]
        public string Type { get; set; }

        [Display(Name = "Marque")]
        public string Brand { get; set; }

        [Display(Name = "Modèle")]
        public string Model { get; set; }

        [Display(Name = "Numero de Serie")]
        public string SerialNumber { get; set; }

        [Display(Name = "Date de création")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Quantité")]
        public int Quantity { get; set; }

        [Display(Name = "Commentaire")]
        public string Comment { get; set; }

        public static List<string> GetPropertiesInFrench()
        {
            return new List<string> {
                "ID", "Type", "Marque", "Modèle", "Numero de Serie", "Date de création",
                "Quantité", "Commentaire"
            };
        }

    }
}