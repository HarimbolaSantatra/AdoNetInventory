using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AppInventaire.Utils;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class RoleRepository : BaseRepository
    {
        public List<Role> Fetch()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM role", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Role> output = null;
            if (reader.HasRows)
            {
                output = new List<Role>();

                while (reader.Read())
                {
                    Role current_role = new Role
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        RoleName = Validation.StringOrEmpty(reader["rolename"].ToString())
                    };
                    output.Add(current_role);
                }
            }
            reader.Close();
            return output;
        }

        public Role FetchSingle(int roleId)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM role WHERE id={roleId}", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Role> output = null;
            if (reader.HasRows)
            {
                output = new List<Role>();

                while (reader.Read())
                {
                    Role current_role = new Role
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        RoleName = Validation.StringOrEmpty(reader["rolename"].ToString())
                    };
                    output.Add(current_role);
                }
            }
            reader.Close();
            return output.First();
        }
    }
}