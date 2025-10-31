using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.LibraryMessage;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
	public class InstructionDateFromValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal DateEdit dateFromEdit;

		internal DateEdit dateToEdit;

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
					if (dateFromEdit == null || dateToEdit == null)
					{
						return result;
					}
					if (isRequired && (dateFromEdit.EditValue == null || dateFromEdit.DateTime == DateTime.MinValue || dateToEdit.EditValue == null || dateToEdit.DateTime == DateTime.MinValue))
					{
						base.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage((LibraryMessage.Message.Enum)49);
						base.ErrorType = ErrorType.Warning;
						return result;
					}
					if (dateToEdit.DateTime.Date < dateFromEdit.DateTime.Date)
					{
						base.ErrorText = "Ngày từ phải nhỏ hơn ngày đến";
						base.ErrorType = ErrorType.Warning;
						return result;
					}
					if (inTime.HasValue && long.Parse(dateFromEdit.DateTime.ToString("yyyyMMdd")) < long.Parse(inTime.ToString().Substring(0, 8)))
					{
						base.ErrorText = "Ngày từ nhỏ hơn ngày vào viện";
						base.ErrorType = ErrorType.Warning;
						return result;
					}
					if (outTime.HasValue && long.Parse(dateToEdit.DateTime.ToString("yyyyMMdd")) > long.Parse(outTime.ToString().Substring(0, 8)))
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
