using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace DataAccessLayer{ 
/// <summary>
/// DatabaseConverter 的摘要描述
/// </summary>
public class DatabaseConverter
{
	public DatabaseConverter()
	{
		
	}

    private Dictionary<string, string> GetProductIDAndSku()
    {
        Dictionary<string, string> skuProductID = new Dictionary<string, string>();

        DataTable table = DataAccess.ExecuteSelect("Select ProductID, Sku from Product");
        foreach (DataRow row in table.Rows)
        {
            if (!skuProductID.ContainsKey(row["Sku"].ToString()))
            {
                skuProductID.Add(row["Sku"].ToString(), row["ProductID"].ToString());
            }
        }
        return skuProductID;
    }

    private DataTable GetWishListItems(string existingWishListID)
    {
        return DataAccess.ExecuteSelect(
            "SELECT * FROM WishListItem WHERE WishListID = @WishListID; ",
            DataAccess.CreateParameterString(existingWishListID));
    }

    private DataTable GetAllConfigurations(string cultureID)
    {
        DataTable table = DataAccess.ExecuteSelect(
            "SELECT C.ConfigID, Name, ItemValue, V.CultureID " +
            "FROM ((Configuration AS C " +
            "    LEFT JOIN (SELECT * FROM ConfigurationValue " +
            "        WHERE CultureID = @ValueCultureID OR CultureID = 0) AS V " +
            "    ON C.ConfigID = V.ConfigID)); ",
            DataAccess.CreateParameterString(cultureID));

        table.Columns.Add("IsMultiValue", Type.GetType("System.Boolean"));
        foreach (DataRow row in table.Rows)
        {
            if (row["CultureID"].ToString() == "0")
                row["IsMultiValue"] = false;
            else
                row["IsMultiValue"] = true;
        }

        return table;
    }
}

}