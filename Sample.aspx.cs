using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using DataAccessLayer;
using Config = System.Configuration.ConfigurationManager;


public partial class Sample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Sample1();
        //test();

        //DataAccessHelper.SetUp(
        //            GetConnectionString(),
        //            ConfigurationManager.ConnectionStrings["StoreConnection"].ProviderName,
        //            false,
        //            true,
        //            null,
        //            "",
        //            SystemConst.DefaultUrlCultureName,
        //            HttpContext.Current.Server.MapPath(SystemConst.LicenseFilePath));

        //DataAccessHelper.SetUp(
        //        ConfigurationManager.ConnectionStrings["StoreConnection"].ConnectionString,
        //        "System.Data.SqlClient"
        //        );

       
        //SqlConnection connection = new SqlConnection("connectionString");
        //DbCommand command = new SqlCommand("commandText", connection);
        //DataAccess.ExecuteNonQueryNoParameter(command);
    }


    private void test() {

        using (SqlConnection conn = new SqlConnection(Config.ConnectionStrings["StoreConnection"].ConnectionString)) {

            string sql = "select * from sample where col1=@col1;select * from sample where col1=@col1";
            DbCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@col1", "test1"));
            cmd.Parameters.Add(new SqlParameter("@col1", "test2"));
            cmd.Connection = conn;
            cmd.CommandText = sql;
            conn.Open();

            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Response.Write(reader["col1"].ToString() + "<BR>");
                Response.Write(reader["col2"].ToString());
            }
        
        }
    }

    private void Sample1() {

        DataTable dt = DataAccess.ExecuteSelect("select * from sample where col1=@col1;select * from sample where col1=@col1", new DbParameter[]{
                DataAccess.CreateParameterString("test2"),
                DataAccess.CreateParameterString("test1")
        });

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(dt.Columns[j].ToString() + ":" + dt.Rows[i][j].ToString() + "<BR>");
            }
        }

        //DataAccess.ExecuteNonQuery("INSERT INTO SearchLog( Keyword, SearchDate, IsFound ) VALUES ( @Keyword, @SearchDate, @IsFound )", new DbParameter[]
        //    {
        //        DataAccess.CreateParameterString(keyword),
        //        DataAccess.CreateParameterDateTime(searchDate),
        //        DataAccess.CreateParameterBool(isFound)
        //    });
    }

    private void Sample2() { 
    
        //public static string Create(string productID, string cultureID, string customerID, double reviewRating, bool enabled, string subject, string body)
        //{
        //    return DataAccess.ExecuteScalar("INSERT INTO CustomerReview (ProductID, CustomerID, ReviewRating, Enabled, Subject, Body, CultureID ) VALUES ( @productID, @customerID, @reviewRating, @enabled, @subject, @body, @cultureID ); SELECT @@Identity; ", new DbParameter[]
        //    {
        //        DataAccess.CreateParameterString(productID),
        //        DataAccess.CreateParameterString(customerID),
        //        DataAccess.CreateParameterDouble(reviewRating),
        //        DataAccess.CreateParameterBool(enabled),
        //        DataAccess.CreateParameterString(subject),
        //        DataAccess.CreateParameterString(body),
        //        DataAccess.CreateParameterString(cultureID)
        //    });
        //}
    }


    private void Sample3() {

        //DataTable table = DataAccess.ExecuteSelect("SELECT  AdminID, MenuPageName, ViewMode, ModifyMode  FROM AdminMenuPermission WHERE AdminID = @AdminID AND MenuPageName = @MenuPageName; ", new DbParameter[]
        //    {
        //        DataAccess.CreateParameterString(adminID),
        //        DataAccess.CreateParameterString(menuPageName)
        //    });
        //return AdminMenuPermissionAccess.ConvertDataTableToDetails(table);
    }

    private void Sample4() {

        //return DataAccess.ExecuteSelect("SELECT AdminMenuAdvancedPermission.MenuPageName, SortOrder, IsEnabled, MenuName , ViewMode, ModifyMode FROM ( AdminMenuAdvancedPermission LEFT JOIN AdminMenuAdvancedPermissionLocale ON AdminMenuAdvancedPermission.MenuPageName = AdminMenuAdvancedPermissionLocale.MenuPageName ) LEFT JOIN AdminMenuPermission ON AdminMenuAdvancedPermission.MenuPageName = AdminMenuPermission.MenuPageName WHERE CultureID = @CultureID AND AdminID = @AdminID AND ViewMode = @ViewMode " + FlagFilterHelper.CreateFlagFilterWithAnd("IsEnabled", isEnabled) + AdminMenuAdvancedPermissionAccess.CreateSortByText(sortBy), new DbParameter[]
        //    {
        //        DataAccess.CreateParameterString(cultureID),
        //        DataAccess.CreateParameterString(adminID),
        //        DataAccess.CreateParameterBool(true)
        //    });
    
    }

}
