using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Config = System.Configuration.ConfigurationManager;

namespace CountryFilterService.DataAccess
{
    class Conn
    {


        public static SqlConnection getTestConn()
        {

            System.Configuration.ConnectionStringSettings ConnStr = null;

            if (!string.IsNullOrEmpty(Config.ConnectionStrings["Conn"].ConnectionString))
            {

                ConnStr = Config.ConnectionStrings["Conn"];

            }
            else {
                throw new ArgumentException("connection string is null or empty");
            }


            SqlConnection Conn = new SqlConnection(ConnStr.ConnectionString);

            return Conn;


        }

    }
}