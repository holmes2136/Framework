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

public partial class Sample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        SqlConnection connection = new SqlConnection("connectionString");
        DbCommand command = new SqlCommand("commandText", connection);
        DataAccess.ExecuteNonQueryNoParameter(command);
    }

    
}
