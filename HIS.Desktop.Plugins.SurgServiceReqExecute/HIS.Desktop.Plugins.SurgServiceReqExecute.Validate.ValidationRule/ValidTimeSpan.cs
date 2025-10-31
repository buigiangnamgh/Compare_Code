using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
	internal class ValidTimeSpan : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal DateEdit dateFromEdit;

		internal DateEdit dateToEdit;

		internal LayoutControlItem lciDate;

		internal CalendarControl calendarControl;

		internal LayoutControlItem lciCa;

		internal TimeSpanEdit timeSpanEdit;

		internal long? inTime;

		internal long? outTime;

		public override bool Validate(Control control, object value)
		{
			bool result = true;
			try
			{
				if (timeSpanEdit.EditValue == null)
				{
					base.ErrorText = ResourceMessage.TruongDuLieuBatBuoc;
					return false;
				}
				if (lciCa.Visible)
				{
					if (inTime.HasValue && long.Parse(calendarControl.DateTime.ToString("yyyyMMdd") + (DateTime.Today.Date + timeSpanEdit.TimeSpan).ToString("HHmm")) < long.Parse(inTime.ToString().Substring(0, 12)))
					{
						base.ErrorText = "Thời gian nhỏ hơn thời gian vào viện";
						base.ErrorType = ErrorType.Warning;
						return false;
					}
					if (outTime.HasValue && long.Parse(calendarControl.DateTime.ToString("yyyyMMdd") + (DateTime.Today.Date + timeSpanEdit.TimeSpan).ToString("HHmm")) > long.Parse(outTime.ToString().Substring(0, 12)))
					{
						base.ErrorText = "Thời gian lớn hơn thời gian ra viện";
						base.ErrorType = ErrorType.Warning;
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}
	}
}
