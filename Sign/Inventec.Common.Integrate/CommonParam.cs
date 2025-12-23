using System;
using System.Collections.Generic;
using System.Text;

namespace Inventec.Common.Integrate
{
	public class CommonParam
	{
		public List<string> Messages = new List<string>();

		public List<string> BugCodes = new List<string>();

		private bool hasException;

		public int? Start { get; set; }

		public int? Limit { get; set; }

		public int? Count { get; set; }

		public string ModuleCode { get; set; }

		public string LanguageCode { get; set; }

		public bool HasException
		{
			get
			{
				return hasException;
			}
			set
			{
				if (value)
				{
					hasException = value;
				}
			}
		}

		public CommonParam()
		{
			Messages = new List<string>();
			BugCodes = new List<string>();
		}

		public CommonParam(int? start, int? limit, int? count)
		{
			Start = start;
			Limit = limit;
			Count = count;
			Messages = new List<string>();
			BugCodes = new List<string>();
		}

		public CommonParam(int? start, int? limit)
		{
			Start = start;
			Limit = limit;
		}

		public string GetMessage()
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (Messages != null && Messages.Count > 0)
				{
					foreach (string message in Messages)
					{
						if (!string.IsNullOrWhiteSpace(message))
						{
							if (message.Trim().EndsWith("."))
							{
								stringBuilder.Append(message.Trim()).Append(" ");
							}
							else
							{
								stringBuilder.Append(message.Trim()).Append(". ");
							}
						}
					}
				}
				return stringBuilder.ToString();
			}
			catch (Exception)
			{
				return "";
			}
		}

		public string GetBugCode()
		{
			try
			{
				if (BugCodes != null && BugCodes.Count > 0)
				{
					return string.Format("[{0}]", string.Join(",", BugCodes));
				}
			}
			catch (Exception)
			{
			}
			return "";
		}
	}
}
