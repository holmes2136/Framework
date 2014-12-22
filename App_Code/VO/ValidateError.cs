using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ValidateError 的摘要描述
/// </summary>
public class ValidateError
{
    public string field{get;set;}
    public string errorText{get;set;}

	public ValidateError()
	{
		
	}

    public ValidateError(string field,string errorText)
    {
        this.field = field;
        this.errorText = errorText;
       
    }
}