using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using AppInventaire.Utils;

namespace AppInventaire.Models
{
    public class ItemRepository : BaseRepository
    {
        // Creer un fonction Fetch() qui retourne un liste d'object du model
        public List<Item> Fetch()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM item", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Item> output = null;
            if (reader.HasRows)
            {
                output = new List<Item>();

                while(reader.Read())
                {
                    Item current_item = new Item
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Type = Validation.StringOrEmpty(reader["Type"].ToString()),
                        Brand =Validation.StringOrEmpty( reader["Brand"].ToString()),
                        Model = Validation.StringOrEmpty(reader["Model"].ToString()),
                        SerialNumber = Validation.StringOrEmpty(reader["SerialNumber"].ToString()),
                        CreationDate = DateTime.Parse(reader["creation_date"].ToString()),
                        Quantity = Validation.IntOrZero(reader["Quantity"].ToString()),
                        Comment = Validation.StringOrEmpty(reader["Comment"].ToString())
                    };
                    output.Add(current_item);
                }
            }

            reader.Close();

            return output;
        }

        public Item FetchSingle(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Item WHERE id={id}", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Item> output = null;
            if (reader.HasRows)
            {
                output = new List<Item>();

                while (reader.Read())
                {
                    Item current_Raspberry = new Item
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Type = Validation.StringOrEmpty(reader["Type"].ToString()),
                        Brand = Validation.StringOrEmpty(reader["Brand"].ToString()),
                        Model = Validation.StringOrEmpty(reader["Model"].ToString()),
                        SerialNumber = Validation.StringOrEmpty(reader["SerialNumber"].ToString()),
                        CreationDate = DateTime.Parse(reader["creation_date"].ToString()),
                        Quantity = Validation.IntOrZero(reader["Quantity"].ToString()),
                        Comment = Validation.StringOrEmpty(reader["Comment"].ToString())
                    };
                    output.Add(current_Raspberry);
                }
            }
            reader.Close();
            return output.First();
        }

        public List<ItemBrand> FetchBrand()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Item_brand", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<ItemBrand> output = null;
            if (reader.HasRows)
            {
                output = new List<ItemBrand>();

                while (reader.Read())
                {
                    ItemBrand current_brand = new ItemBrand
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Brand = reader["Brand"].ToString()
                    };
                    output.Add(current_brand);
                }
            }
            reader.Close();
            return output;
        }

        public void AddItem(string type, string brand, string model, string serialnumber, string quantity, string comment)
        {
            // SQL Command
            string cmd_string = $"INSERT INTO item(type, brand, model, serialnumber, quantity, comment) " +
                $"VALUES (\"{type}\", \"{brand}\", \"{model}\", \"{serialnumber}\",  {quantity}, \"{comment}\")";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@brand", brand);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@serialnumber", serialnumber);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@quantity", quantity);
            cmd.Parameters.AddWithValue("@comment", comment);

            cmd.ExecuteNonQuery();
            
            // Close Connection
            CloseConnection();
        }

        public void EditItem(int id, string type, string brand, string model, string serialnumber, string quantity, string comment)
        {
            string cmd_string = $"UPDATE item SET type=\"{type}\", brand=\"{brand}\", model=\"{model}\", " +
                $"serialnumber=\"{serialnumber}\", quantity={quantity}, comment=\"{comment}\" WHERE ID={id}";
            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            cmd.Parameters.AddWithValue("@type", type);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@brand", brand);
            cmd.Parameters.AddWithValue("@model", model);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@serialnumber", serialnumber);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@comment", comment);

            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void DeleteItem(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM item WHERE ID={id}", _con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
    }
}