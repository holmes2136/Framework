using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using DataAccessSample.VO;


namespace DataAccessSample.DataAccessLayer
{
    public  class SQLDataAccess:DataAccess
    {

        private delegate void TGenerateListFromReader<t>(SqlDataReader returnData, ref List<t> List);

        public override void AddParamToSQLCmd(System.Data.SqlClient.SqlCommand sqlCmd, string paramId, System.Data.SqlDbType sqlType, int paramSize, System.Data.ParameterDirection paramDirection, object paramvalue)
        {
            if (sqlCmd == null)
            {
                throw new ArgumentException("sqlCmd");
            }

            if (paramId == string.Empty)
            {
                throw new ArgumentOutOfRangeException("paramId");
            }

            SqlParameter newSqlParam = new SqlParameter();
            newSqlParam.ParameterName = paramId;
            newSqlParam.SqlDbType = sqlType;
            newSqlParam.Direction = paramDirection;

            if (paramSize > 0)
            {
                newSqlParam.Size = paramSize;
            }

            if (paramvalue != null)
            {
                newSqlParam.Value = paramvalue;
            }

            sqlCmd.Parameters.Add(newSqlParam);
        }

        public override void setCommandType(SqlCommand sqlCmd, CommandType cmdType, string cmdText)
        {
            sqlCmd.CommandText = cmdText;
            sqlCmd.CommandType = cmdType;

        }

        public override object ExecuteScalarCmd(SqlCommand sqlCmd)
        {

            if (this.GetConnectionString() == string.Empty)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (sqlCmd == null)
            {
                throw new ArgumentNullException("SqlCmd");
            }

            using (SqlConnection conn = new SqlConnection(this.GetConnectionString()))
            {

                sqlCmd.Connection = conn;

                conn.Open();

                object obj = sqlCmd.ExecuteScalar();

                return obj;

            }
        }

        public override DataTable ExecuteScalarCmd3(SqlCommand sqlCmd)
        {
            DataTable dt = null;

            if (this.GetConnectionString() == string.Empty)
            {
                throw new ArgumentNullException("ConnectionString");
            }

            using (SqlConnection conn = new SqlConnection(this.GetConnectionString()))
            {
                sqlCmd.Connection = conn;

                conn.Open();

                dt = new DataTable();

                dt.Load(sqlCmd.ExecuteReader());

                return dt;

            }
        }

        private void TExecuteReaderCmd<t>(SqlCommand sqlCmd, TGenerateListFromReader<t> gcfr, ref List<t> List)
        {
            if (string.IsNullOrEmpty(this.GetConnectionString()))
            {
                throw new ArgumentOutOfRangeException("ConnectionString");
            }

            if (sqlCmd == null)
            {
                throw new ArgumentOutOfRangeException("sqlCmd");
            }

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                sqlCmd.Connection = conn;
                conn.Open();
                gcfr(sqlCmd.ExecuteReader(), ref List);

            }
        }


        //People Methods


        public override void AddPeople(People people) { 
        

            // Access DB data


        }


    }
}
