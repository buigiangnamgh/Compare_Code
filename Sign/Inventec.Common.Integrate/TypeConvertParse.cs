using System;

namespace Inventec.Common.Integrate
{
	public class TypeConvertParse
	{
		public static bool ToBoolean(string inputValue)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(inputValue))
			{
				bool.TryParse(inputValue, out result);
			}
			return result;
		}

		public static byte ToByte(string inputValue)
		{
			byte result = 0;
			if (!string.IsNullOrEmpty(inputValue))
			{
				byte.TryParse(inputValue, out result);
			}
			return result;
		}

		public static sbyte ToSByte(string inputValue)
		{
			sbyte result = 0;
			if (!string.IsNullOrEmpty(inputValue))
			{
				sbyte.TryParse(inputValue, out result);
			}
			return result;
		}

		public static short ToInt16(string inputValue)
		{
			short result = 0;
			if (!string.IsNullOrEmpty(inputValue))
			{
				short.TryParse(inputValue, out result);
			}
			return result;
		}

		public static ushort ToUInt16(string inputValue)
		{
			ushort result = 0;
			if (!string.IsNullOrEmpty(inputValue))
			{
				ushort.TryParse(inputValue, out result);
			}
			return result;
		}

		public static int ToInt32(string inputValue)
		{
			int result = 0;
			if (!string.IsNullOrEmpty(inputValue))
			{
				int.TryParse(inputValue, out result);
			}
			return result;
		}

		public static uint ToUInt32(string inputValue)
		{
			uint result = 0u;
			if (!string.IsNullOrEmpty(inputValue))
			{
				uint.TryParse(inputValue, out result);
			}
			return result;
		}

		public static long ToInt64(string inputValue)
		{
			long result = 0L;
			if (!string.IsNullOrEmpty(inputValue))
			{
				long.TryParse(inputValue, out result);
			}
			return result;
		}

		public static ulong ToUInt64(string inputValue)
		{
			ulong result = 0uL;
			if (!string.IsNullOrEmpty(inputValue))
			{
				ulong.TryParse(inputValue, out result);
			}
			return result;
		}

		public static float ToFloat(string inputValue)
		{
			float result = 0f;
			if (!string.IsNullOrEmpty(inputValue))
			{
				float.TryParse(inputValue, out result);
			}
			return result;
		}

		public static double ToDouble(string inputValue)
		{
			double result = 0.0;
			if (!string.IsNullOrEmpty(inputValue))
			{
				double.TryParse(inputValue, out result);
			}
			return result;
		}

		public static decimal ToDecimal(string inputValue)
		{
			decimal result = 0m;
			if (!string.IsNullOrEmpty(inputValue))
			{
				decimal.TryParse(inputValue, out result);
			}
			return result;
		}

		public static DateTime ToDateTime(string inputValue)
		{
			DateTime result = DateTime.Now;
			if (!string.IsNullOrEmpty(inputValue))
			{
				DateTime.TryParse(inputValue, out result);
			}
			return result;
		}
	}
}
