using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class DetailsToken
    {
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int DetailsId { get; set; }
        public string TokenKey { get; set; }
        public DateTime ExpirationDate
        {
            get { return CreationDate + new TimeSpan(24, 0, 0); } // Date d'expiration: dans 24 heures
        }

        public bool isExpired()
        {
            return DateTime.Now >= ExpirationDate;
        }
    }
}