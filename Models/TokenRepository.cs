using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace AppInventaire.Models
{
    public class TokenRepository : BaseRepository
    {
        public Token FetchSingle(string token_key)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM token WHERE token=\"{token_key}\"", _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Token> outputTokens = null;
            if (reader.HasRows)
            {
                outputTokens = new List<Token>();
                while (reader.Read())
                {
                    Token current_token = new Token
                    {
                        UserId = int.Parse(reader["userid"].ToString()),
                        TokenKey = reader["token"].ToString(),
                        CreationDate = DateTime.Parse(reader["creation_date"].ToString()),
                    };
                    outputTokens.Add(current_token);
                }
            }
            reader.Close();
            // if there's many token, fetch the last and delete other
            if (outputTokens == null) return new Token();
            if (outputTokens.Count > 1)
            {
                DateTime lastTokenCreateDate = outputTokens.Last().CreationDate;
                outputTokens.RemoveAll(item => item.CreationDate < lastTokenCreateDate);
            }
            return outputTokens.Last();
        }

        public DetailsToken FetchSingleDetails(string token_key)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM token WHERE token=\"{token_key}\"", _con);
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
                        AddedUserId = int.Parse(reader["added_user_id"].ToString()),
                        TokenKey = reader["token"].ToString(),
                        CreationDate = DateTime.Parse(reader["creation_date"].ToString()),
                    };
                    outputTokens.Add(current_token);
                }
            }
            reader.Close();
            return (outputTokens == null) ? new DetailsToken() : outputTokens.First();
        }

        public void Add(int userid, string token, int added_user_id)
        {
            string cmd_string = $"INSERT INTO token " +
                $"VALUES ({userid}, {added_user_id}, \"{token}\", CURRENT_TIMESTAMP)";

            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);
            cmd.ExecuteNonQuery();
        }

        public void Add(int userid, string token)
        {
            string cmd_string = $"INSERT INTO token(userid, token, creation_date) " +
                $"VALUES ({userid}, \"{token}\", CURRENT_TIMESTAMP)";

            MySqlCommand cmd = new MySqlCommand(cmd_string, _con);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string token_key)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM token WHERE token=\"{token_key}\"", _con);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Delete every token possessed by a User
        /// </summary>
        /// <param name="user"> User object</param>
        public void DeleteByOwner(User user)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM token WHERE userid=\"{user.ID}\"", _con);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Delete every token possessed by a User
        /// </summary>
        /// <param name="userId"> ID of the User </param>
        public void DeleteByOwner(int userId)
        {
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM token WHERE userid=\"{userId}\"", _con);
            cmd.ExecuteNonQuery();
        }
    }
}