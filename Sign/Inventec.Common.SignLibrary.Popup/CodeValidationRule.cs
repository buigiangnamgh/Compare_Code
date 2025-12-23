using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary.Popup
{
	internal class CodeValidationRule : ValidationRule
	{
		internal TextEdit txt;

		public override bool Validate(Control control, object value)
		{
			bool result = false;
			try
			{
				if (txt == null)
				{
					return result;
				}
				if (string.IsNullOrEmpty(txt.Text))
				{
					base.ErrorText = MessageUitl.GetMessage("DuLieuKhongHopLe");
					base.ErrorType = ErrorType.Warning;
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
