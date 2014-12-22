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
using DataAccessSample.VO;

namespace DataAccessSample.DataAccessLayer
{
   
    
    public abstract class DataAccess{


        public DataAccess() { }



        public string GetConnectionString(){

            string connStr = "";

            if (!String.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["sql connection string in web config"].ConnectionString)){
                connStr = ConfigurationManager.ConnectionStrings["sql connection string in web config"].ConnectionString;
            }
            else{

                throw new NullReferenceException("ConnectionString is missing");
            }

            return connStr;


        }


        //SQL Methods
        public abstract void setCommandType(SqlCommand sqlCmd, CommandType cmdType, string cmdText);
        public abstract object ExecuteScalarCmd(SqlCommand sqlCmd);
        public abstract void AddParamToSQLCmd(SqlCommand sqlCmd, string paramId, SqlDbType sqlType, int paramSize, ParameterDirection paramDirection, object paramvalue);
        public abstract DataTable ExecuteScalarCmd3(SqlCommand cmd);
        

        //People Methods
        public abstract void AddPeople(People people);
    
    
    
    
    }


    

}
