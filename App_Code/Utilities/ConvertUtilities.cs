using System;
using System.Globalization;
namespace Utilities
{
	public static class ConvertUtilities
	{
		private static bool IsEmptyObject(object source)
		{
			return source == null || string.IsNullOrEmpty(source.ToString()) || source is DBNull;
		}
		private static CultureInfo USCultureInfo()
		{
			return new CultureInfo("en-US");
		}
		private static string ReplaceEngNumberStirng(string number)
		{
			return number.Replace(',', '.');
		}
		public static string ToString(object source)
		{
			string result;
			if (ConvertUtilities.IsEmptyObject(source))
			{
				result = string.Empty;
			}
			else
			{
				result = source.ToString();
			}
			return result;
		}
		public static bool ToBoolean(object source)
		{
			return !ConvertUtilities.IsEmptyObject(source) && Convert.ToBoolean(source);
		}
		public static string ToYesNo(object source)
		{
			string result;
			if (ConvertUtilities.IsEmptyObject(source))
			{
				result = "No";
			}
			else
			{
				result = (Convert.ToBoolean(source) ? "Yes" : "No");
			}
			return result;
		}
		public static string ToYN(object source)
		{
			string result;
			if (ConvertUtilities.IsEmptyObject(source))
			{
				result = "N";
			}
			else
			{
				result = (Convert.ToBoolean(source) ? "Y" : "N");
			}
			return result;
		}
		public static decimal ToDecimal(object source)
		{
			decimal result;
			if (ConvertUtilities.IsEmptyObject(source))
			{
				result = 0.0m;
			}
			else
			{
				result = Convert.ToDecimal(source);
			}
			return result;
		}
		public static double ToDouble(object source)
		{
			double result;
			if (ConvertUtilities.IsEmptyObject(source))
			{
				result = 0.0;
			}
			else
			{
				result = Convert.ToDouble(source);
			}
			return result;
		}
		public static int ToInt32(object source)
		{
			int result;
			if (ConvertUtilities.IsEmptyObject(source))
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(source);
			}
			return result;
		}
		public static DateTime ToDateTime(object source)
		{
			DateTime result;
			if (ConvertUtilities.IsEmptyObject(source))
			{
				result = default(DateTime);
			}
			else
			{
				result = Convert.ToDateTime(source);
			}
			return result;
		}
		public static double EnglishStringToDouble(string number)
		{
			return Convert.ToDouble(ConvertUtilities.ReplaceEngNumberStirng(number), ConvertUtilities.USCultureInfo());
		}
		public static decimal EnglishStringToDecimal(string number)
		{
			return Convert.ToDecimal(ConvertUtilities.ReplaceEngNumberStirng(number), ConvertUtilities.USCultureInfo());
		}
	}
}
