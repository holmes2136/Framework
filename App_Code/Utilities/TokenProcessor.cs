using System;
namespace Utilities
{
	public class TokenProcessor
	{
		public enum CharType
		{
			NonLetterDigit
		}
		private string _text;
		private int _currentIndex;
		public TokenProcessor(string text)
		{
			this._text = text;
			this._currentIndex = 0;
		}
		private bool IsCharType(char value, TokenProcessor.CharType type)
		{
			bool result = false;
			if (type == TokenProcessor.CharType.NonLetterDigit)
			{
				if (!char.IsLetterOrDigit(value))
				{
					result = true;
				}
			}
			return result;
		}
		public bool IsEndOfString()
		{
			return this._text.Length <= 0 || this._currentIndex >= this._text.Length;
		}
		public void SkipTo(char marker)
		{
			this._currentIndex = this._text.IndexOf(marker, this._currentIndex);
			if (this._currentIndex == -1)
			{
				this._currentIndex = this._text.Length;
			}
		}
		public string ExtractTo(TokenProcessor.CharType endMarkerType)
		{
			int num = this._currentIndex;
			bool flag = false;
			while (!flag && num < this._text.Length)
			{
				if (this.IsCharType(this._text[num], endMarkerType))
				{
					flag = true;
				}
				num++;
			}
			if (flag)
			{
				num--;
			}
			string result = this._text.Substring(this._currentIndex, num - this._currentIndex);
			this._currentIndex = num;
			return result;
		}
		public void SeekRelative(int distance)
		{
			this._currentIndex += distance;
			if (this._currentIndex < 0)
			{
				this._currentIndex = 0;
			}
			else
			{
				if (this._currentIndex > this._text.Length)
				{
					this._currentIndex = this._text.Length;
				}
			}
		}
		public char GetRelativeChar(int index)
		{
			return this._text[this._currentIndex + index];
		}
	}
}
