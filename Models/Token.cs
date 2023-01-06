using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class Token
    {
        public int UserId { get; set; }         // Owner of the token
        public string TokenKey { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate
        {
            get { return CreationDate + new TimeSpan(0, 10, 0); } // Date d'expiration: dans 10 minutes
        }

        public bool isExpired()
        {
            return DateTime.Now >= ExpirationDate;
        }
    }
}