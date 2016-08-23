using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Repository
{
    public class GeneratorRepository : BaseRepository
    {
        public GeneratorRepository(string connectionString) : base(connectionString)
        {
        }

        public List<string> DropDownListGeneration()
        {
            var sqlQuery = "Select Name from Teams";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();

            var reader = command.ExecuteReader();

            var collection = (from DbDataRecord item in reader select item.GetString(0)).ToList();
            collection.Sort();
            command.Connection.Close();
            return collection;
        }

        public string[] GetPlace(string s)
        {
            var sqlQuery = $"Select CityArea, Arena from Teams where Name = '{s}'";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();

            var reader = command.ExecuteReader();
            string[] result = new string[2];

            while (reader.Read())
            {
                result[0] = reader["CityArea"].ToString();
                result[1] = reader["Arena"].ToString();
            }

            command.Connection.Close();
            return result;
        }

        public string GetShortName(string name)
        {
            var sqlQuery = $"Select ShortTeamName from Teams where Name = '{name}'";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);
            command.Connection.Open();
            command.Prepare();

            var reader = command.ExecuteReader();
            string shortName = string.Empty;

            while (reader.Read())
            {
                shortName = reader["ShortTeamName"].ToString();
            }

            command.Connection.Close();
            return shortName;
        }

        public List<string> GetAllBoards()
        {
            var boards = new List<string>();
            var sqlQuery = "Select ImageName from Boards";
            var command = DbQuery.CreateCommand(sqlQuery, ConnectionString);            
            command.Connection.Open();
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                boards.Add(reader["ImageName"].ToString());
            }
            command.Connection.Close();
            return boards;
        }
    }
}