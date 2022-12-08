using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace AppInventaire.Models
{
    public class BaseRepository
    {
        // Creer connection MySql
        protected MySqlConnection _con;

        public BaseRepository()
        {
            if (_con == null)
            {
                string cnx = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                _con = new MySqlConnection(cnx);
            }

            if (_con.State != System.Data.ConnectionState.Open)
                _con.Open();
        }

        public void CloseConnection()
        {
            if (_con != null && _con.State != System.Data.ConnectionState.Closed)
                _con.Close();

            _con.Dispose();
        }
        
    }
}