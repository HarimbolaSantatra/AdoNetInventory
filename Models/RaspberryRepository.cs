using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using AppInventaire.Utils;

namespace AppInventaire.Models
{
    public class RaspberryRepository : BaseRepository
    {
        // Creer un fonction Fetch() qui retourne un liste d'object du model
        public List<Raspberry> Fetch()
        {
            // Creer commande SQL
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Raspberry", _con);
            // Creer MySql Reader. L'objet Reader contient les résultats
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Raspberry> output = null;
            if (reader.HasRows)
            {
                output = new List<Raspberry>();

                while (reader.Read())
                {
                    Raspberry current_Raspberry = new Raspberry
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Rasp_Version = int.Parse(reader["Version"].ToString()),
                        OS = Validation.StringOrEmpty(reader["OS"].ToString()),
                        Screen = Validation.IntOrZero(reader["Screen"].ToString()),
                        Client = Validation.StringOrEmpty(reader["Client"].ToString()),
                        Accessories = Validation.StringOrEmpty(reader["Accessories"].ToString()),
                        Comment = Validation.StringOrEmpty(reader["Comment"].ToString()),
                        CreationDate = DateTime.Parse(reader["CreationDate"].ToString())
                    };
                    output.Add(current_Raspberry);
                }
                reader.Close();
            };
            return output;
        }
            

        public Raspberry FetchSingle(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Raspberry WHERE id={id}", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Raspberry> output = null;
            if (reader.HasRows)
            {
                output = new List<Raspberry>();

                while (reader.Read())
                {
                    Raspberry current_Raspberry = new Raspberry
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Rasp_Version = int.Parse(reader["Version"].ToString()),
                        OS = Validation.StringOrEmpty(reader["OS"].ToString()),
                        Screen = Validation.IntOrZero(reader["Screen"].ToString()),
                        Client = Validation.StringOrEmpty(reader["Client"].ToString()),
                        Accessories = Validation.StringOrEmpty(reader["Accessories"].ToString()),
                        Comment = Validation.StringOrEmpty(reader["Comment"].ToString()),
                        CreationDate = DateTime.Parse(reader["CreationDate"].ToString())
                    };
                    output.Add(current_Raspberry);
                }
            }
            reader.Close();
            return output.Count == 0 ? null : output.First();
        }

        public void AddRaspberry(string version, string os, string screen, string client, string accessories, string comment)
        {
            string cmd_string = $"INSERT INTO Raspberry(version, os, screen, client, accessories, comment)" +
                $"VALUES (@version, @os, @screen, @client, @accessories, @comment)";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            Validation.Parameter_AddWithValue_ForInt(cmd, "@version", version );
            cmd.Parameters.AddWithValue("@os", os);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@screen", screen);
            cmd.Parameters.AddWithValue("@client", client);
            cmd.Parameters.AddWithValue("@accessories", accessories);
            cmd.Parameters.AddWithValue("@comment", comment);

            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void EditRaspberry(int id, string version, string os, string screen, string client, string accessories, string comment)
        {
            string cmd_string = $"UPDATE Raspberry SET version=@version, os=@os, screen=@screen, client=@client," +
                $"accessories=@accessories, comment=@comment WHERE ID={id}";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            Validation.Parameter_AddWithValue_ForInt(cmd, "@version", version);
            cmd.Parameters.AddWithValue("@os", os);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@screen", screen);
            cmd.Parameters.AddWithValue("@client", client);
            cmd.Parameters.AddWithValue("@accessories", accessories);
            cmd.Parameters.AddWithValue("@comment", comment);

            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void DeleteRaspberry(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM Raspberry WHERE ID={id}", _con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
    }
}