using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.LibraryMessage;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
	public class InstructionDateCalendarValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal CalendarControl calendarControl;

		internal LayoutControlItem lci;

		internal long? inTime;

		internal long? outTime;

		internal TimeSpanEdit timeSpan;

		internal bool isRequired = false;

		public override bool Validate(Control control, object value)
		{
			bool result = false;
			try
			{
				if (lci == null || lci.Visibility == LayoutVisibility.Always)
				{
					if (calendarControl == null)
					{
						return result;
					}
					if (isRequired && calendarControl.EditValue == null)
					{
						base.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage((LibraryMessage.Message.Enum)49);
						base.ErrorType = ErrorType.Warning;
						return result;
					}
					if (inTime.HasValue && long.Parse(calendarControl.DateTime.ToString("yyyyMMdd")) < long.Parse(inTime.ToString().Substring(0, 8)))
					{
						base.ErrorText = "Ngày từ nhỏ hơn ngày vào viện";
						base.ErrorType = ErrorType.Warning;
						return result;
					}
					if (outTime.HasValue && long.Parse(calendarControl.DateTime.ToString("yyyyMMdd")) > long.Parse(outTime.ToString().Substring(0, 8)))
					{
						base.ErrorText = "Ngày đến lớn hơn ngày ra viện";
						base.ErrorType = ErrorType.Warning;
						return result;
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}
	}
}
