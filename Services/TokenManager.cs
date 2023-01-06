using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppInventaire.Models;

namespace AppInventaire.Services
{
    public class TokenManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> ID of the owner of the token. Must be an admin. </param>
        /// <param name="token_key"> Unique Key </param>
        /// <returns> Redirect to the request page or to login page </returns>
        public static bool Verify(Token token)
        {
            // if token is expired or token doesn't exist
            if (token.isExpired())
            {
                TokenRepository _tok_rep = new TokenRepository();
                Token _token = _tok_rep.FetchSingle(token.TokenKey);
                _tok_rep.CloseConnection();
                _tok_rep.Delete(token.TokenKey);
                HttpContext.Current.Session.Clear();
                return false;
            }
            if (token == null) return false;
            return true;
        }
    }
}