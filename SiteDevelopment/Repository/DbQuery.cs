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
        public const string ConnectionString = @"Server=localhost\SQLEXPRESS; Database=SiteDevelopment; Trusted_Connection=True;";

        public static SqlCommand CreateCommand(string yourQuery, string connectionString)
        {
            string sqlQuery = yourQuery;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            return command;
        }

        public static void ExecuteCommand(SqlCommand command)
        {
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }
    }
}