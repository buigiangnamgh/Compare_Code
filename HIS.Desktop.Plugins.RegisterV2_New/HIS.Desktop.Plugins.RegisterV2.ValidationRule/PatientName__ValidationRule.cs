using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Inventec.Common.Logging;
using Inventec.Desktop.Common.LibraryMessage;

namespace HIS.Desktop.Plugins.RegisterV2.ValidationRule
{
	internal class PatientName__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
	{
		internal TextEdit txtPatientName;

		public override bool Validate(Control control, object value)
		{
			bool flag = true;
			try
			{
				flag = flag && txtPatientName != null;
				if (flag)
				{
					string text = "";
					string text2 = txtPatientName.Text.Trim();
					if (string.IsNullOrEmpty(text2))
					{
						flag = false;
						text = MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
					}
					else
					{
						string text3 = "";
						string text4 = "";
						int num = text2.LastIndexOf(" ");
						if (num > -1)
						{
							text3 = text2.Substring(num).Trim();
							text4 = text2.Substring(0, num).Trim();
						}
						else
						{
							text3 = text2;
							text4 = "";
						}
						if (!string.IsNullOrEmpty(text3) && text3.Length > 30)
						{
							flag = false;
							text = text + ((!string.IsNullOrEmpty(text)) ? "\r\n" : "") + string.Format(ResourceMessage.TenBNVuotQuaMaxLength, 30);
						}
						if (!string.IsNullOrEmpty(text4) && text4.Length > 70)
						{
							flag = false;
							text = text + ((!string.IsNullOrEmpty(text)) ? "\r\n" : "") + string.Format(ResourceMessage.HoDemBNVuotQuaMaxLength, 70);
						}
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
	}
}
