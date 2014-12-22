<%@ WebHandler Language="C#" Class="Demo" %>

using System;
using System.Web;
using liarAttributes;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using Iface;
using Util;
using Params;

public class Demo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        HttpRequest req = context.Request;
        HttpResponse res = context.Response;

        string name = WebUtil.QueryString("name");
        string pwd = WebUtil.QueryString("pwd");
        string sex = WebUtil.QueryString("sex");

        List<ValidateError> errors = new Demo1Params(name, pwd, sex).Validate();      
        
        for(int i =0;i<errors.Count;i++){
            res.Write(errors[i].errorText + "<BR>");
        }
        
     
    }
 

    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}