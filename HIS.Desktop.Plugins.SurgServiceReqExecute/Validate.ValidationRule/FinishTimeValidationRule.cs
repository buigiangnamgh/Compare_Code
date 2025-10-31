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
	internal class FinishTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal DateEdit startTime;

		internal DateEdit finishTime;

		internal long treatmentOutTime;

		internal long instructionTime;

		internal bool keyCheck;

		internal bool keyCheckStatsTime;

		public override bool Validate(Control control, object value)
		{
			bool flag = true;
			try
			{
				if (finishTime.EditValue == null)
				{
					return flag;
				}
				List<string> list = new List<string>();
				long num = ((finishTime.EditValue != null) ? Parse.ToInt64(finishTime.DateTime.ToString("yyyyMMddHHmm") + "00") : 0);
				if (startTime.EditValue != null && finishTime.EditValue != null && startTime.DateTime > finishTime.DateTime)
				{
					list.Add(ResourceMessage.ThoiGianKetThucKhongDuocNhoHonThoiGianBatDau);
					flag = false;
				}
				if (!keyCheckStatsTime && finishTime.EditValue != null && finishTime.DateTime > DateTime.Now)
				{
					list.Add(ResourceMessage.ThoiGianKetThucKhongDuocLonHonThoiGianHeThong);
					flag = false;
				}
				if (!keyCheck && num < instructionTime)
				{
					list.Add(string.Format(ResourceMessage.ThoiGianKetThucKhongDuocNhoHonThoiGianYLenh));
					flag = false;
				}
				if (treatmentOutTime > 0 && num > treatmentOutTime)
				{
					list.Add(string.Format(ResourceMessage.ThoiGianKetThucThoiGianRaVien));
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
