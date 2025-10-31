using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using Inventec.Common.Logging;
using Inventec.Common.TypeConvert;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
	internal class StartTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal DateEdit startTime;

		internal DateEdit finishTime;

		internal long instructionTime;

		internal long treatmentOutTime;

		internal bool keyCheck;

		internal bool keyCheckStatsTime;

		public override bool Validate(Control control, object value)
		{
			bool flag = true;
			try
			{
				if (startTime.EditValue == null)
				{
					base.ErrorText = ResourceMessage.TruongDuLieuBatBuoc;
					return false;
				}
				List<string> list = new List<string>();
				long num = ((startTime.EditValue != null) ? Parse.ToInt64(startTime.DateTime.ToString("yyyyMMddHHmm") + "00") : 0);
				if (!keyCheck && num < instructionTime)
				{
					list.Add(ResourceMessage.ThoiGianBatDauPhaiLonHonThoiGianYLenh);
					flag = false;
				}
				if (finishTime.EditValue != null && startTime.DateTime > finishTime.DateTime)
				{
					list.Add(ResourceMessage.ThoiGianBatDauKhongDuocLonHonThoiGianKetThuc);
					flag = false;
				}
				if (!keyCheckStatsTime && startTime.EditValue != null && startTime.DateTime > DateTime.Now)
				{
					list.Add(ResourceMessage.ThoiGianKetThucKhongDuocLonHonThoiGianHeThong);
					flag = false;
				}
				if (treatmentOutTime > 0 && num > treatmentOutTime)
				{
					list.Add(string.Format(ResourceMessage.ThoiGianBatDauThoiGianRaVien));
					flag = false;
				}
				if (!flag)
				{
					base.ErrorText = string.Join(";", list);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return flag;
		}
	}
}
