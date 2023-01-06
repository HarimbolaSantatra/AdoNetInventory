using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class DetailsToken : Token
    {
        public int AddedUserId { get; set; }    // ID of the new user
    }
}