using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using Inventec.Common.Logging;
using Inventec.Common.String;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
	internal class ICDValidationRuleControl : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal TextEdit txtIcdCode;

		internal TextEdit txtMainText;

		internal GridLookUpEdit btnBenhChinh;

		internal CheckEdit chkCheck;

		internal int? maxLengthCode;

		internal int? maxLengthText;

		internal bool IsObligatoryTranferMediOrg;

		public override bool Validate(Control control, object value)
		{
			bool result = false;
			try
			{
				if (txtIcdCode == null || btnBenhChinh == null || txtMainText == null || chkCheck == null)
				{
					return result;
				}
				if (maxLengthCode.HasValue && CheckString.IsOverMaxLengthUTF8(txtIcdCode.Text.Trim(), maxLengthCode.GetValueOrDefault()))
				{
					base.ErrorText = ResourceMessage.MaBenhChinhVuotQuaKyTuChoPhep;
					return result;
				}
				if (!string.IsNullOrEmpty(txtIcdCode.ErrorText) && txtIcdCode.ErrorText != ResourceMessage.TruongDuLieuBatBuoc)
				{
					base.ErrorText = ResourceMessage.MaICDKhongDungVuiLongKiemTraLai;
					return result;
				}
				if (chkCheck.Checked)
				{
					if (maxLengthText.HasValue && CheckString.IsOverMaxLengthUTF8(txtMainText.Text.Trim(), maxLengthText.GetValueOrDefault()))
					{
						base.ErrorText = ResourceMessage.TenBenhChinhVuotQuaKyTuChoPhep;
						return result;
					}
					if (IsObligatoryTranferMediOrg)
					{
						if (string.IsNullOrEmpty(txtMainText.Text))
						{
							return result;
						}
					}
					else if (string.IsNullOrEmpty(txtIcdCode.Text) || string.IsNullOrEmpty(txtMainText.Text))
					{
						return result;
					}
				}
				else if (string.IsNullOrEmpty(txtIcdCode.Text) || btnBenhChinh.EditValue == null)
				{
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
