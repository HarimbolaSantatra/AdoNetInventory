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
                RoleRepository _role_rep = new RoleRepository();
                while (reader.Read())
                {
                    User current_user = new User
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        FirstName = Validation.StringOrEmpty(reader["FirstName"].ToString()),
                        LastName = Validation.StringOrEmpty(reader["LastName"].ToString()),
                        Email = Validation.StringOrEmpty(reader["Email"].ToString()),
                        Password = Validation.StringOrEmpty(reader["Password"].ToString()),
                        userRole = _role_rep.FetchSingle(Validation.IntOrDefault(reader["role_id"].ToString(), 1))
                    };
                    output.Add(current_user);
                }
                _role_rep.CloseConnection();
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
                RoleRepository _role_rep = new RoleRepository();
                while (reader.Read())
                {
                    User current_User = new User
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        FirstName = Validation.StringOrEmpty(reader["FirstName"].ToString()),
                        LastName = Validation.StringOrEmpty(reader["LastName"].ToString()),
                        Email = Validation.StringOrEmpty(reader["Email"].ToString()),
                        Password = Validation.StringOrEmpty(reader["Password"].ToString()),
                        userRole = _role_rep.FetchSingle(Validation.IntOrDefault(reader["role_id"].ToString(), 1))
                    };
                    output.Add(current_User);
                }
                _role_rep.CloseConnection();
            }
            reader.Close();
            return (output == null) ? new User() : output.First();
        }

        public User FetchByEmail(string emailQuery) // MBOLA TSY METY
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM User WHERE email=@searchQuery", _con);
            cmd.Parameters.AddWithValue("@searchQuery", emailQuery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<User> output = null;
            if (reader.HasRows)
            {
                output = new List<User>();
                RoleRepository _role_rep = new RoleRepository();
                while (reader.Read())
                {
                    User current_User = new User
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        FirstName = Validation.StringOrEmpty(reader["FirstName"].ToString()),
                        LastName = Validation.StringOrEmpty(reader["LastName"].ToString()),
                        Email = Validation.StringOrEmpty(reader["Email"].ToString()),
                        Password = Validation.StringOrEmpty(reader["Password"].ToString()),
                        userRole = _role_rep.FetchSingle(int.Parse(reader["role_id"].ToString()))
                    };
                    output.Add(current_User);
                }
                _role_rep.CloseConnection();
            }
            reader.Close();
            return output == null ? new User() : output.First();
        }

        public void AddUser(string firstname, string lastname, string email, string password, int role_id)
        {
            // string that begin with @ are SqlParameter
            string cmd_string = $"INSERT INTO User(firstname, lastname, email, password, role_id) " +
                $"VALUES (@firstname, @lastname, @email, sha(@password), @role_id)";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@role_id", role_id);

            cmd.ExecuteNonQuery();
        }

        public void EditUser(int id, string firstname, string lastname, string email, int role_id)
        {
            string cmd_string = $"UPDATE User SET firstname=@firstname, lastname=@lastname, " +
                $"email=@email, role_id=@role_id WHERE ID={id}";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@role_id", role_id);

            cmd.ExecuteNonQuery();
        }

        public void EditPassword(int id,string password)
        { 
            string hashedPassword = Operation.Sha1Hash(password);
            string cmd_string = $"UPDATE User SET password=\"{hashedPassword}\" WHERE ID={id}";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con); 
            cmd.ExecuteNonQuery();
        }

        public void DeleteUser(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM User WHERE ID={id}", _con);
            cmd.ExecuteNonQuery();
        }
    }
}