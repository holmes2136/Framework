using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Utilities { 
/// <summary>
/// StringUtilities 的摘要描述
/// </summary>
public class StringUtilities
{
	public StringUtilities()
	{
		
	}

    public static int CountOccurrencesInString(string source, char value)
    {
        int num = 0;
        for (int i = 0; i < source.Length; i++)
        {
            if (source[i] == value)
            {
                num++;
            }
        }
        return num;
    }

    public static int CountOccurrencesInString(string source, string subString)
    {
        int num = 0;
        int num2 = 0;
        do
        {
            num2 = source.IndexOf(subString, num2);
            if (num2 != -1)
            {
                num++;
                num2 += subString.Length;
            }
        }
        while (num2 != -1);
        return num;
    }

}

}