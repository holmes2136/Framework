using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Util { 
/// <summary>
/// WebUtil 的摘要描述
/// </summary>
    public class WebUtil
    {
	    public WebUtil(){}

        public static string QueryString(string Name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[Name] != null)
                result = HttpContext.Current.Request.QueryString[Name].ToString();
            return result;
        }

        public static int QueryStringInt(string Name)
        {
            string resultStr = QueryString(Name).ToUpperInvariant();
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }

       
    }
}