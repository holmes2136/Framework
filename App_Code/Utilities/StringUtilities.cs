using System;
using System.Collections;
using System.Globalization;
using System.Text;
namespace Utilities
{
	public static class StringUtilities
	{
		public static string GetNameByLength(string name, int length)
		{
			string result;
			if (name.Length <= length)
			{
				result = name;
			}
			else
			{
				result = name.Substring(0, length) + "..";
			}
			return result;
		}
		public static string Trim(string text, int maxLength)
		{
			string text2 = text.Trim();
			string result;
			if (text2.Length > maxLength)
			{
				result = text2.Substring(0, maxLength);
			}
			else
			{
				result = text2;
			}
			return result;
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
		public static string[] SplitString(string source, params char[] separators)
		{
			string[] array = source.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			ArrayList arrayList = new ArrayList();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text != null)
				{
					string text2 = text.Trim();
					if (text2.Length > 0)
					{
						arrayList.Add(text2);
					}
				}
			}
			string[] array3 = new string[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}
		public static bool IsUnicodeLetterOrDigit(char c)
		{
			bool result;
			switch (CharUnicodeInfo.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.LetterNumber:
				result = true;
				return result;
			}
			result = false;
			return result;
		}
		public static string RemoveDiacritics(string s)
		{
			string text = s.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}
		public static bool IsStringInArray(string[] array, string text, bool ignoreCase)
		{
			bool result;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					string strA = array[i];
					if (string.Compare(strA, text, ignoreCase) == 0)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}
		public static bool ContainsNumber(string source)
		{
			bool result;
			for (int i = 0; i < source.Length; i++)
			{
				char c = source[i];
				if (char.IsDigit(c))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public static bool ContainsAlphabet(string source)
		{
			bool result;
			for (int i = 0; i < source.Length; i++)
			{
				char c = source[i];
				if (char.IsLetter(c))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public static bool ContainsSymbol(string source)
		{
			bool result;
			for (int i = 0; i < source.Length; i++)
			{
				char c = source[i];
				if (char.IsPunctuation(c) || char.IsSymbol(c))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public static bool ContainsLowercase(string source)
		{
			bool result;
			for (int i = 0; i < source.Length; i++)
			{
				char c = source[i];
				if (char.IsLower(c))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public static bool ContainsUppercase(string source)
		{
			bool result;
			for (int i = 0; i < source.Length; i++)
			{
				char c = source[i];
				if (char.IsUpper(c))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
	}
}
