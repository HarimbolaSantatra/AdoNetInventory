using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using AppInventaire.Utils;

namespace AppInventaire.Models
{
    public class UserRepository : BaseRepository
    {
        public List<User> Fetch()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM user", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<User> output = null;
            if (reader.HasRows)
            {
                output = new List<User>();

                while (reader.Read())
                {
                    User current_user = new User
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        FirstName = Validation.StringOrEmpty(reader["FirstName"].ToString()),
                        LastName = Validation.StringOrEmpty(reader["LastName"].ToString()),
                        Email = Validation.StringOrEmpty(reader["Email"].ToString()),
                        Password = Validation.StringOrEmpty(reader["Password"].ToString())
                    };
                    output.Add(current_user);
                }
            }
            reader.Close();
            return output;
        }

        public User FetchSingle(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM User WHERE id={id}", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<User> output = null;
            if (reader.HasRows)
            {
                output = new List<User>();

                while (reader.Read())
                {
                    User current_User = new User
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        FirstName = Validation.StringOrEmpty(reader["FirstName"].ToString()),
                        LastName = Validation.StringOrEmpty(reader["LastName"].ToString()),
                        Email = Validation.StringOrEmpty(reader["Email"].ToString()),
                        Password = Validation.StringOrEmpty(reader["Password"].ToString())
                    };
                    output.Add(current_User);
                }
            }
            reader.Close();
            return output.First();
        }

        public void AddUser(string firstname, string lastname, string email, string password)
        {
            string cmd_string = $"INSERT INTO User(firstname, lastname, email, password) " +
                $"VALUES (\"{firstname}\", \"{lastname}\", \"{email}\", \"{password}\")";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void EditUser(int id, string firstname, string lastname, string email, string password)
        {
            string cmd_string = $"UPDATE User SET firstname=\"{firstname}\", lastname=\"{lastname}\", " +
                $"email=\"{email}\", password=\"{password}\" WHERE ID={id}";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void DeleteUser(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM User WHERE ID={id}", _con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
    }
}