using System;
using HIS.Desktop.Utility;
using Inventec.Common.Logging;
using Inventec.Common.TypeConvert;
using MOS.LibraryHein.Bhyt;

namespace HIS.Desktop.Plugins.RegisterV2
{
	internal class DateUtil
	{
		internal class DateValidObject
		{
			public int Age { get; set; }

			public string OutDate { get; set; }

			public string Message { get; set; }

			public bool HasNotDayDob { get; set; }
		}

		private static bool CheckIsChild(DateTime dtDob)
		{
			bool result = false;
			try
			{
				if (dtDob != DateTime.MinValue)
				{
					result = BhytPatientTypeData.IsChild(dtDob);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		internal static DateValidObject ValidPatientDob(string inputDate)
		{
			DateValidObject dateValidObject = new DateValidObject();
			try
			{
				int num = Parse.ToInt32(inputDate);
				if (string.IsNullOrEmpty(inputDate))
				{
					dateValidObject.Message = ResourceMessage.TruongDuLieuBatBuoc;
				}
				else if (inputDate.Length == 1 || inputDate.Length == 2)
				{
					if (num < 7)
					{
						dateValidObject.Message = ResourceMessage.NgaySinhKhongDuocNhoHon7;
					}
					else
					{
						dateValidObject.Age = DateTime.Now.Year - num;
						dateValidObject.OutDate = "01/01/" + dateValidObject.Age;
					}
				}
				else if (inputDate.Length == 4)
				{
					if (num <= DateTime.Now.Year)
					{
						dateValidObject.OutDate = "01/01/" + inputDate;
						dateValidObject.HasNotDayDob = true;
					}
				}
				else if (inputDate.Length == 8)
				{
					dateValidObject.OutDate = inputDate.Substring(0, 2) + "/" + inputDate.Substring(2, 2) + "/" + inputDate.Substring(4, 4);
				}
				else if (inputDate.Length == 10)
				{
					dateValidObject.OutDate = inputDate;
				}
				else
				{
					dateValidObject.Message = ResourceMessage.NhapNgaySinhKhongDungDinhDang;
				}
				DateTime? dateTime;
				if (!string.IsNullOrEmpty(dateValidObject.OutDate) && string.IsNullOrEmpty(dateValidObject.Message))
				{
					dateTime = DateTimeHelper.ConvertDateStringToSystemDate(dateValidObject.OutDate);
					if (!dateTime.HasValue || dateTime.Value == DateTime.MinValue)
					{
						dateValidObject.Message = ResourceMessage.NhapNgaySinhKhongDungDinhDang;
						dateValidObject.OutDate = "";
					}
					else
					{
						if (!dateTime.HasValue)
						{
							goto IL_0229;
						}
						DateTime value = dateTime.Value;
						if (!(dateTime.Value.Date > DateTime.Now.Date))
						{
							goto IL_0229;
						}
						dateValidObject.Message = ResourceMessage.ThongTinNgaySinhPhaiNhoHonNgayHienTai;
					}
				}
				goto end_IL_0007;
				IL_0229:
				if (CheckIsChild(dateTime.Value) && inputDate.Length < 8)
				{
					dateValidObject.Message = ResourceMessage.YeuCauNhapDayDuNgayThangNamSinhVoiBNDuoi6Tuoi;
				}
				end_IL_0007:;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return dateValidObject;
		}
	}
}
