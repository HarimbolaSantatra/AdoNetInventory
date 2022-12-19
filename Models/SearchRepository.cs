using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AppInventaire.Utils;
using System.Linq;
using System.Web;

namespace AppInventaire.Models
{
    public class SearchRepository : BaseRepository
    {
        public List<Search> FetchResult(string searchQuery)
        {
            List<Search> searchResults = new List<Search>();
            List<String> Tables = new List<String>() { "computer", "item", "user", "raspberry" };
            foreach (String table in Tables)
            {
                List<string> Columns = FetchColumnNames(table);
                foreach (String column in Columns)
                {
                    //string sqlQuery = $"SELECT * FROM {table} WHERE {column} LIKE \'%{searchQuery}%\'";
                    string sqlQuery = $"SELECT * FROM @table WHERE @column LIKE %@search_query%";
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, _con);
                    cmd.Parameters.AddWithValue("@table", table);
                    cmd.Parameters.AddWithValue("@column", column);
                    cmd.Parameters.AddWithValue("@search_query", searchQuery);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Search searchResult = new Search
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                ModelType = table,
                                Column = column
                            };
                            searchResults.Add(searchResult);
                        }
                    }
                    reader.Close();
                }
            }
            return searchResults;
        }

        public List<string> FetchColumnNames(string Table)
        {
            List<string> columns = new List<string>();
            string sqlQuery = $"SHOW COLUMNS FROM {Table}";
            MySqlCommand cmd = new MySqlCommand(sqlQuery, _con);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    columns.Add(reader["Field"].ToString());
                }
            }
            reader.Close();
            return columns;
        }
    }
}