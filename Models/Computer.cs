using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppInventaire.Models
{
    public class Computer
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Marque")]
        public string Brand { get; set; }

        [Display(Name = "Modèle")]
        public string Model { get; set; }

        [Display(Name = "OS")]
        public string OS { get; set; }

        [Display(Name = "Nom de l'hôte")]
        public string Hostname { get; set; }

        [Display(Name = "CPU")]
        public string Processor { get; set; }

        [Display(Name = "RAM(Go)")]
        public float? RamGB{ get; set; }

        [Display(Name = "Carte Graphique")]
        public string GraphCard{ get; set; }

        [Display(Name = "Mem(Go)")]
        public float? GraphCardGB{ get; set; }

        public static List<string> GetPropertiesInFrench()
        {
            return new List<string> {
                "ID", "Marque", "Modèle", "OS", "Nom d'hôte",
                "CPU", "RAM(Go)", "Carte Graphique", "Mem(Go)"
            };
        }

        public List<SelectListItem> GetBrandSelectListItems()
        {
            ComputerRepository _rep = new ComputerRepository();
            List<SelectListItem> BrandSelectListItem = new List<SelectListItem>();
            List<ComputerBrand> computerBrands = _rep.FetchBrand();
            foreach (ComputerBrand cb in computerBrands)
            {
                BrandSelectListItem.Add(new SelectListItem()
                {
                    Text = $"{cb.Brand}",
                    Value = $"{cb.Brand}"
                });
            };
            return BrandSelectListItem;
        }
}
}