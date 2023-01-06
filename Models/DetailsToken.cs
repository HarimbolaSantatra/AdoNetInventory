using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class DetailsToken : Token
    {
        public int UserId { get; set; }         // Owner of the token
        public int AddedUserId { get; set; }    // ID of the new user
    }
}