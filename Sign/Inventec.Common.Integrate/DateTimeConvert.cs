using System;
using System.Globalization;
using System.Text;

namespace Inventec.Common.Integrate
{
	public class DateTimeConvert
	{
		private const string DATE_SEPARATE = "Ngày     tháng     năm       ";

		private const string MONTH_SEPARATE = "Tháng     năm       ";

		public static string TimeNumberToMonthString(long time)
		{
			string result = null;
			try
			{
				string text = time.ToString();
				if (text != null && text.Length >= 8)
				{
					result = new StringBuilder().Append(text.Substring(4, 2)).Append("/").Append(text.Substring(0, 4))
						.ToString();
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static string TimeNumberToDateString(long time)
		{
			string text = null;
			try
			{
				return TimeNumberToDateString(time.ToString());
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static string TimeNumberToDateString(string time)
		{
			string result = null;
			try
			{
				if (time != null && time.Length >= 8)
				{
					result = new StringBuilder().Append(time.Substring(6, 2)).Append("/").Append(time.Substring(4, 2))
						.Append("/")
						.Append(time.Substring(0, 4))
						.ToString();
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static string TimeNumberToTimeString(long time)
		{
			string result = null;
			try
			{
				string text = time.ToString();
				if (text != null && text.Length >= 14)
				{
					result = new StringBuilder().Append(text.Substring(6, 2)).Append("/").Append(text.Substring(4, 2))
						.Append("/")
						.Append(text.Substring(0, 4))
						.Append(" ")
						.Append(text.Substring(8, 2))
						.Append(":")
						.Append(text.Substring(10, 2))
						.Append(":")
						.Append(text.Substring(12, 2))
						.ToString();
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static string TimeNumberToTimeStringWithoutSecond(long time)
		{
			string result = null;
			try
			{
				string text = time.ToString();
				if (text != null && text.Length >= 14)
				{
					result = new StringBuilder().Append(text.Substring(6, 2)).Append("/").Append(text.Substring(4, 2))
						.Append("/")
						.Append(text.Substring(0, 4))
						.Append(" ")
						.Append(text.Substring(8, 2))
						.Append(":")
						.Append(text.Substring(10, 2))
						.ToString();
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static string SystemDateTimeToDateString(DateTime? dateTime)
		{
			string result = null;
			try
			{
				if (dateTime.HasValue)
				{
					long? num = SystemDateTimeToTimeNumber(dateTime);
					if (num.HasValue)
					{
						result = TimeNumberToDateString(num.Value);
					}
				}
				else
				{
					result = "Ngày     tháng     năm       ";
				}
			}
			catch (Exception)
			{
				result = "Ngày     tháng     năm       ";
			}
			return result;
		}

		public static string TimeNumberToDateStringSeparateString(long time)
		{
			string result = null;
			try
			{
				string text = time.ToString();
				if (text != null && text.Length >= 14)
				{
					result = new StringBuilder().Append("Ngày ").Append(text.Substring(6, 2)).Append(" tháng ")
						.Append(text.Substring(4, 2))
						.Append(" năm ")
						.Append(text.Substring(0, 4))
						.ToString();
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static string SystemDateTimeToDateSeparateString(DateTime? dateTime)
		{
			string text = "Ngày     tháng     năm       ";
			try
			{
				if (dateTime.HasValue)
				{
					return new StringBuilder().Append("Ngày ").Append(dateTime.Value.Day).Append(" tháng ")
						.Append(dateTime.Value.Month)
						.Append(" năm ")
						.Append(dateTime.Value.Year)
						.ToString();
				}
				return "Ngày     tháng     năm       ";
			}
			catch (Exception)
			{
				return "Ngày     tháng     năm       ";
			}
		}

		public static string SystemDateTimeToTimeSeparateString(DateTime? dateTime)
		{
			string text = "Ngày     tháng     năm       ";
			try
			{
				if (dateTime.HasValue)
				{
					return new StringBuilder().Append("Ngày ").Append(dateTime.Value.Day).Append(" tháng ")
						.Append(dateTime.Value.Month)
						.Append(" năm ")
						.Append(dateTime.Value.Year)
						.Append(" ")
						.Append(dateTime.Value.Hour)
						.Append(" giờ ")
						.Append(dateTime.Value.Minute)
						.Append(" phút ")
						.Append(dateTime.Value.Second)
						.Append(" giây ")
						.ToString();
				}
				return "Ngày     tháng     năm       ";
			}
			catch (Exception)
			{
				return "Ngày     tháng     năm       ";
			}
		}

		public static string SystemDateTimeToMonthSeparateString(DateTime? dateTime)
		{
			string text = "Tháng     năm       ";
			try
			{
				if (dateTime.HasValue)
				{
					return new StringBuilder().Append("Tháng ").Append(dateTime.Value.Month).Append(" năm ")
						.Append(dateTime.Value.Year)
						.ToString();
				}
				return "Tháng     năm       ";
			}
			catch (Exception)
			{
				return "Tháng     năm       ";
			}
		}

		public static long? SystemDateTimeToTimeNumber(DateTime? dateTime)
		{
			long? result = null;
			try
			{
				if (dateTime.HasValue)
				{
					return long.Parse(dateTime.Value.ToString("yyyyMMddHHmmss"));
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static DateTime? TimeNumberToSystemDateTime(long time)
		{
			DateTime? result = null;
			try
			{
				if (time > 0)
				{
					return DateTime.ParseExact(time.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
