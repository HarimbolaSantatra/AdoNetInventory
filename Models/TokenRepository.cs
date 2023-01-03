﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace AppInventaire.Models
{
    public class TokenRepository : BaseRepository
    {

        public DetailsToken FetchSingle(string token_key)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM token_details WHERE token=\"{token_key}\"", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<DetailsToken> outputTokens = null;
            if (reader.HasRows)
            {
                outputTokens = new List<DetailsToken>();
                while (reader.Read())
                {
                    DetailsToken current_token = new DetailsToken
                    {
                        UserId = int.Parse(reader["userid"].ToString()),
                        TokenKey = reader["token"].ToString(),
                        CreationDate = DateTime.Parse(reader["creation_date"].ToString()),
                        DetailsId = int.Parse(reader["details_id"].ToString())
                    };
                    outputTokens.Add(current_token);
                }
            }
            reader.Close();

            return outputTokens.Count == 0 ? null : outputTokens.First();
        }

        public void Add(int userid, string token, int details_id)
        {
            string cmd_string = $"INSERT INTO token_details " +
                $"VALUES ({userid}, \"{token}\", CURRENT_TIMESTAMP,  {details_id})";

            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string token_key)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM token_details WHERE token=\"{token_key}\"", _con);
            cmd.ExecuteNonQuery();
        }
    }
}