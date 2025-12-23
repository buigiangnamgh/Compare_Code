using System;
using System.IO;
using Aspose.Cells;
using Aspose.Pdf;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.License
{
	internal class LicenceProcess
	{
		internal static void SetLicenseForAspose()
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			try
			{
				if (!string.IsNullOrEmpty(Licenses.Aspose_Key))
				{
					Stream license = new MemoryStream(Convert.FromBase64String(Licenses.Aspose_Key));
					License val = new License();
					val.SetLicense(license);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal static void SetLicenseForAsposeCell()
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			try
			{
				if (!string.IsNullOrEmpty(Licenses.Aspose_Key))
				{
					Stream license = new MemoryStream(Convert.FromBase64String(Licenses.Aspose_Key));
					License val = new License();
					val.SetLicense(license);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
