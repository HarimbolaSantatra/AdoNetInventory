using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using AppInventaire.Utils;

namespace AppInventaire.Models
{
    [AuthorizeCustom(Roles = "Admin")]
    public class ComputerRepository : BaseRepository
    {
        // Creer un fonction Fetch() qui retourne un liste d'object du model
        public List<Computer> Fetch()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Computer", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Computer> output = null;
            if (reader.HasRows)
            {
                output = new List<Computer>();

                while (reader.Read())
                {
                    Computer current_Computer = new Computer
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Brand = reader["Brand"].ToString(),
                        Model = Validation.StringOrEmpty(reader["Model"].ToString()),
                        OS = Validation.StringOrEmpty(reader["OS"].ToString()),
                        Hostname =Validation.StringOrEmpty( reader["Hostname"].ToString()),
                        Processor = Validation.StringOrEmpty(reader["Processor"].ToString()),
                        RamGB = Validation.FloatOrZero(reader["RamGB"].ToString()),
                        GraphCard = Validation.StringOrEmpty(reader["GraphCard"].ToString()),
                        GraphCardGB = Validation.FloatOrZero(reader["GraphCardGB"].ToString())
                    };
                    output.Add(current_Computer);
                }
            }
            reader.Close();
            return output;
        }

        public Computer FetchSingle(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Computer WHERE id={id}", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Computer> output = null;
            if (reader.HasRows)
            {
                output = new List<Computer>();

                while (reader.Read())
                {
                    Computer current_Computer = new Computer
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Brand = reader["Brand"].ToString(),
                        Model = Validation.StringOrEmpty(reader["Model"].ToString()),
                        OS = Validation.StringOrEmpty(reader["OS"].ToString()),
                        Hostname = Validation.StringOrEmpty(reader["Hostname"].ToString()),
                        Processor = Validation.StringOrEmpty(reader["Processor"].ToString()),
                        RamGB = Validation.FloatOrZero(reader["RamGB"].ToString()),
                        GraphCard = Validation.StringOrEmpty(reader["GraphCard"].ToString()),
                        GraphCardGB = Validation.FloatOrZero(reader["GraphCardGB"].ToString())
                    };
                    output.Add(current_Computer);
                }
            }
            reader.Close();
            return output == null ? new Computer() : output.First();
        }

        public List<ComputerBrand> FetchBrand()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM computer_brand", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<ComputerBrand> output = null;
            if (reader.HasRows)
            {
                output = new List<ComputerBrand>();

                while (reader.Read())
                {
                    ComputerBrand current_brand = new ComputerBrand
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

        public void AddComputer(string brand, string model, string os, string hostname, string processor, string RamGB, string GraphCard, string GraphCardGB)
        {
            string cmd_string = $"INSERT INTO Computer(brand, model, os, hostname, processor, ramgb, graphcard, graphcardgb)" +
                $"VALUES (@brand, @model, @os, @hostname, @processor, @RamGB , @GraphCard, @GraphCardGB)";

            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            cmd.Parameters.AddWithValue("@brand", brand);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@os", os);
            cmd.Parameters.AddWithValue("@hostname", hostname);
            cmd.Parameters.AddWithValue("@processor", processor);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@RamGB", RamGB);
            cmd.Parameters.AddWithValue("@GraphCard", GraphCard);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@GraphCardGB", GraphCardGB);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void AddComputerBrand(string brand)
        {
            string cmd_string = $"INSERT INTO computer_brand(brand) VALUES (@brand)";

            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            cmd.Parameters.AddWithValue("@brand", brand);

            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void EditComputer(int id, string brand, string model, string os, string hostname, string processor, string RamGB, string GraphCard, string GraphCardGB)
        {
            string cmd_string = $"UPDATE Computer SET brand=@brand, model=@model, " +
                $"os=@os, hostname=@hostname, processor=@processor, RamGB=@RamGB, " +
                $"GraphCard=@GraphCard, GraphCardGB=@GraphCardGB WHERE ID={id}";

            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);

            cmd.Parameters.AddWithValue("@brand", brand);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@os", os);
            cmd.Parameters.AddWithValue("@hostname", hostname);
            cmd.Parameters.AddWithValue("@processor", processor);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@RamGB", RamGB);
            cmd.Parameters.AddWithValue("@GraphCard", GraphCard);
            Validation.Parameter_AddWithValue_ForInt(cmd, "@GraphCardGB", GraphCardGB);

            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        public void DeleteComputer(int id)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM Computer WHERE ID={id}", _con);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        
    }
}