using System;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.RegisterV2
{
	internal class CalculatePatientAge
	{
		internal class AgeObject
		{
			public int AgeType { get; set; }

			public string OutDate { get; set; }

			public DateTime OutDateDate { get; set; }
		}

		internal static AgeObject Calculate(long dob)
		{
			AgeObject ageObject = new AgeObject();
			try
			{
				DateTime dateTime = (ageObject.OutDateDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(dob).Value);
				if (dateTime == DateTime.MinValue)
				{
					throw new ArgumentNullException("dtNgSinh");
				}
				TimeSpan timeSpan = DateTime.Now - dateTime;
				TimeSpan timeSpan2 = DateTime.Now.Date - dateTime.Date;
				double totalHours = timeSpan.TotalHours;
				if (totalHours < 24.0)
				{
					ageObject.AgeType = 4;
					ageObject.OutDate = ((int)totalHours).ToString() ?? "";
				}
				else
				{
					long ticks = timeSpan.Ticks;
					DateTime dateTime2 = new DateTime(ticks);
					if ((dateTime2.Year - 1) * 12 + dateTime2.Month - 1 == 0)
					{
						ageObject.AgeType = 3;
						ageObject.OutDate = ((int)timeSpan2.TotalDays).ToString() ?? "";
					}
					else
					{
						long ticks2 = timeSpan2.Ticks;
						DateTime dateTime3 = new DateTime(ticks2);
						int num = (dateTime3.Year - 1) * 12 + dateTime3.Month - 1;
						if (num == 0)
						{
							ageObject.OutDate = ((int)timeSpan2.TotalDays).ToString() ?? "";
							ageObject.AgeType = 3;
						}
						else if (num < 72)
						{
							ageObject.OutDate = num.ToString() ?? "";
							ageObject.AgeType = 2;
						}
						else
						{
							ageObject.OutDate = (DateTime.Now.Year - dateTime.Year).ToString() ?? "";
							ageObject.AgeType = 1;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return ageObject;
		}
	}
}
