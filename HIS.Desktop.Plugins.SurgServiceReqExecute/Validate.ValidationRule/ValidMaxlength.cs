using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
	internal class ValidMaxlength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal TextEdit textEdit;

		public override bool Validate(Control control, object value)
		{
			bool result = false;
			try
			{
				if (string.IsNullOrEmpty(textEdit.Text.Trim()))
				{
					base.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage((LibraryMessage.Message.Enum)49);
					return result;
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
