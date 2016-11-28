using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Npgsql;

namespace WebGIS.DataLayer
{
    public class SqlDataQueryEngine
    {

        public static void ExecuteNonQuery(NpgsqlConnection conn, string sqlStatement)
        {
            NpgsqlCommand command = new NpgsqlCommand(sqlStatement, conn);
            command.ExecuteNonQuery();
        }


        public static NpgsqlDataReader ExecuteReader(NpgsqlConnection conn, string sqlStatement)
        {
            NpgsqlCommand command = new NpgsqlCommand(sqlStatement, conn);
            return command.ExecuteReader();
        }

        public static object ExecuteScalar(NpgsqlConnection conn, string sqlStatement)
        {
            NpgsqlCommand command = new NpgsqlCommand(sqlStatement, conn);
            return command.ExecuteScalar();
        }


    }
}