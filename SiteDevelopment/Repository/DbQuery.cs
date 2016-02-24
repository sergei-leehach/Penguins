using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace SiteDevelopment.Repository
{
    public static class DbQuery
    {
        private const string ConnectionString =
            @"Server=localhost\SQLEXPRESS; Database=Site Development; Trusted_Connection=True;";

        private static string sqlQuery = string.Empty;
        private static SqlConnection connection;
        private static SqlCommand command;
        private static SqlDataReader reader;

        public static IEnumerable<string> DropDownListGeneration()
        {
            connection = new SqlConnection(ConnectionString);
            
            sqlQuery = "Select Team from TableOfTeams";
            command = new SqlCommand(sqlQuery, connection);
            connection.Open();

            reader = command.ExecuteReader();

            var collection = (from DbDataRecord item in reader select item.GetString(0)).ToList();
            collection.Sort();
            connection.Close();
            return collection;
        }

        public static string[] GetPlace (string s)
        {
            connection = new SqlConnection(ConnectionString);

            sqlQuery = $"Select City_Area, Arena from TableOfTeams where Team = '{s}'";
            command = new SqlCommand(sqlQuery, connection);
            connection.Open();

            reader = command.ExecuteReader();
            string[] result = new string[2];

            while (reader.Read())
            {
                result[0] = reader["City_Area"].ToString();
                result[1] = reader["Arena"].ToString();
            }              

            connection.Close();
            return result;
        }

        public static string GetShortName(string name)
        {
            connection = new SqlConnection(ConnectionString);

            sqlQuery = $"Select ShortTeamName from TableOfTeams where Team = '{name}'";
            command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            command.Prepare();

            reader = command.ExecuteReader();
            string shortName = string.Empty;

            while (reader.Read())
            {
                shortName = reader["ShortTeamName"].ToString();
            }

            connection.Close();
            return shortName;
        }

        public static IEnumerable<string> GetAllBoards()
        {
            connection = new SqlConnection(ConnectionString);

            sqlQuery = $"Select ImageName from Boards";
            command = new SqlCommand(sqlQuery, connection);
            var boards = new List<string>();
            connection.Open();

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                boards.Add(reader["ImageName"].ToString());
            }

            return boards;
        }
    }
}