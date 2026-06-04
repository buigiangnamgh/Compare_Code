using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Inventec.Common.Logging;
using MOS.LibraryHein.Bhyt;

namespace HIS.Desktop.Plugins.RegisterV2.ValidationRule
{
	internal class PatientDob__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal ButtonEdit txtDob;

		internal DateEdit dtDob;

		public override bool Validate(Control control, object value)
		{
			bool flag = true;
			try
			{
				bool flag2 = CheckIsChild();
				flag = flag && txtDob != null && dtDob != null;
				if (flag)
				{
					string text = "";
					DateUtil.DateValidObject dateValidObject = DateUtil.ValidPatientDob(txtDob.Text);
					flag = !string.IsNullOrEmpty(dateValidObject.OutDate);
					if (!string.IsNullOrEmpty(dateValidObject.Message))
					{
						text = dateValidObject.Message;
						flag = false;
					}
					base.ErrorText = text;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return flag;
		}

		private bool CheckIsChild()
		{
			bool result = false;
			try
			{
				if (dtDob.EditValue != null && dtDob.DateTime != DateTime.MinValue)
				{
					DateTime dateTime = dtDob.DateTime;
					result = BhytPatientTypeData.IsChild(dateTime);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}
	}
}
