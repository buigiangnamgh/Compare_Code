using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary.Validation
{
	internal class RelationNameValidationRule : ValidationRule
	{
		internal TextEdit txtRelationName;

		public override bool Validate(Control control, object value)
		{
			bool result = false;
			try
			{
				if (txtRelationName == null)
				{
					return result;
				}
				if (string.IsNullOrEmpty(txtRelationName.Text))
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
