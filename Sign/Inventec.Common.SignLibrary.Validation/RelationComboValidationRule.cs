using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary.Validation
{
	internal class RelationComboValidationRule : ValidationRule
	{
		internal GridLookUpEdit cboRelation;

		public override bool Validate(Control control, object value)
		{
			bool result = false;
			try
			{
				if (cboRelation == null)
				{
					return result;
				}
				if (cboRelation.EditValue == null || TypeConvertParse.ToInt64(cboRelation.EditValue.ToString()) <= 0)
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
