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
            @"Server=localhost\SQLEXPRESS; Database=SiteDevelopment; Trusted_Connection=True;";

        private static string _sqlQuery = string.Empty;
        private static SqlConnection _connection;
        private static SqlCommand _command;
        private static SqlDataReader _reader;

        public static IEnumerable<string> DropDownListGeneration()
        {
            _connection = new SqlConnection(ConnectionString);
            
            _sqlQuery = "Select Name from Teams";
            _command = new SqlCommand(_sqlQuery, _connection);
            _connection.Open();

            _reader = _command.ExecuteReader();

            var collection = (from DbDataRecord item in _reader select item.GetString(0)).ToList();
            collection.Sort();
            _connection.Close();
            return collection;
        }

        public static string[] GetPlace (string s)
        {
            _connection = new SqlConnection(ConnectionString);

            _sqlQuery = $"Select City_Area, Arena from Teams where Name = '{s}'";
            _command = new SqlCommand(_sqlQuery, _connection);
            _connection.Open();

            _reader = _command.ExecuteReader();
            string[] result = new string[2];

            while (_reader.Read())
            {
                result[0] = _reader["City_Area"].ToString();
                result[1] = _reader["Arena"].ToString();
            }              

            _connection.Close();
            return result;
        }

        public static string GetShortName(string name)
        {
            _connection = new SqlConnection(ConnectionString);

            _sqlQuery = $"Select ShortTeamName from Teams where Name = '{name}'";
            _command = new SqlCommand(_sqlQuery, _connection);
            _connection.Open();
            _command.Prepare();

            _reader = _command.ExecuteReader();
            string shortName = string.Empty;

            while (_reader.Read())
            {
                shortName = _reader["ShortTeamName"].ToString();
            }

            _connection.Close();
            return shortName;
        }

        public static IEnumerable<string> GetAllBoards()
        {
            _connection = new SqlConnection(ConnectionString);

            _sqlQuery = $"Select ImageName from Boards";
            _command = new SqlCommand(_sqlQuery, _connection);
            var boards = new List<string>();
            _connection.Open();

            _reader = _command.ExecuteReader();

            while (_reader.Read())
            {
                boards.Add(_reader["ImageName"].ToString());
            }

            return boards;
        }
    }
}