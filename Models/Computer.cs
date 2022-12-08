using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppInventaire.Models
{
    public class Computer
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la marque")]
        [Display(Name = "Marque")]
        public string Brand { get; set; }

        [Display(Name = "Modèle")]
        public string Model { get; set; }

        [Display(Name = "Système d'exploitation")]
        public string OS { get; set; }

        [Display(Name = "Nom de l'hôte")]
        public string Hostname { get; set; }

        [Display(Name = "Processeur")]
        public string Processor { get; set; }

        [Display(Name = "RAM (Go)")]
        public float RamGB{ get; set; }

        [Display(Name = "Carte Graphique")]
        public string GraphCard{ get; set; }

        [Display(Name = "Mémoire Carte Graphique(Go)")]
        public float GraphCardGB{ get; set; }
    }
}