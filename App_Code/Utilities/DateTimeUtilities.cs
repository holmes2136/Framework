using System;
namespace Utilities
{
	public static class DateTimeUtilities
	{
		private static DateTime FirstDayOfWeek(DayOfWeek firstday, DateTime date)
		{
			if (firstday == DayOfWeek.Monday)
			{
				if (date.DayOfWeek == DayOfWeek.Tuesday)
				{
					date = date.AddDays(-1.0);
				}
				else
				{
					if (date.DayOfWeek == DayOfWeek.Wednesday)
					{
						date = date.AddDays(-2.0);
					}
					else
					{
						if (date.DayOfWeek == DayOfWeek.Thursday)
						{
							date = date.AddDays(-3.0);
						}
						else
						{
							if (date.DayOfWeek == DayOfWeek.Friday)
							{
								date = date.AddDays(-4.0);
							}
							else
							{
								if (date.DayOfWeek == DayOfWeek.Saturday)
								{
									date = date.AddDays(-5.0);
								}
								else
								{
									if (date.DayOfWeek == DayOfWeek.Sunday)
									{
										date = date.AddDays(-6.0);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (firstday == DayOfWeek.Sunday)
				{
					if (date.DayOfWeek == DayOfWeek.Monday)
					{
						date = date.AddDays(-1.0);
					}
					else
					{
						if (date.DayOfWeek == DayOfWeek.Tuesday)
						{
							date = date.AddDays(-2.0);
						}
						else
						{
							if (date.DayOfWeek == DayOfWeek.Wednesday)
							{
								date = date.AddDays(-3.0);
							}
							else
							{
								if (date.DayOfWeek == DayOfWeek.Thursday)
								{
									date = date.AddDays(-4.0);
								}
								else
								{
									if (date.DayOfWeek == DayOfWeek.Friday)
									{
										date = date.AddDays(-5.0);
									}
									else
									{
										if (date.DayOfWeek == DayOfWeek.Saturday)
										{
											date = date.AddDays(-6.0);
										}
									}
								}
							}
						}
					}
				}
			}
			return new DateTime(date.Year, date.Month, date.Day);
		}
		public static DateTime GetFirstDayOfTheWeek(DateTime dateTime)
		{
			return DateTimeUtilities.FirstDayOfWeek(DayOfWeek.Sunday, dateTime);
		}
		public static DateTime GetLastDayOfTheWeek(DateTime dateTime)
		{
			return DateTimeUtilities.GetFirstDayOfTheWeek(dateTime).AddDays(6.0);
		}
		public static DateTime GetFirstDayOfTheMonth(DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, 1);
		}
		public static DateTime GetLastDayOfTheMonth(DateTime dateTime)
		{
			int num = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
			return DateTimeUtilities.GetFirstDayOfTheMonth(dateTime).AddDays((double)(num - 1));
		}
		public static DateTime GetFirstDayOfTheYear(DateTime dateTime)
		{
			return new DateTime(dateTime.Year, 1, 1);
		}
		public static DateTime StartTimeOfDay(DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
		}
		public static DateTime EndTimeOfDay(DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
		}
		public static bool IsWithinTimeSpan(DateTime dateTime1, DateTime dateTime2, TimeSpan timeSpan)
		{
			TimeSpan t;
			if (dateTime1 > dateTime2)
			{
				t = dateTime1.Subtract(dateTime2);
			}
			else
			{
				t = dateTime2.Subtract(dateTime1);
			}
			return timeSpan >= t;
		}
	}
}
