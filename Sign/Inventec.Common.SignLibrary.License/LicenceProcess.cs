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
			try
			{
				if (!string.IsNullOrEmpty(Licenses.Aspose_Key))
				{
					Stream license = new MemoryStream(Convert.FromBase64String(Licenses.Aspose_Key));
					Aspose.Pdf.License license2 = new Aspose.Pdf.License();
					license2.SetLicense(license);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal static void SetLicenseForAsposeCell()
		{
			try
			{
				if (!string.IsNullOrEmpty(Licenses.Aspose_Key))
				{
					Stream license = new MemoryStream(Convert.FromBase64String(Licenses.Aspose_Key));
					Aspose.Cells.License license2 = new Aspose.Cells.License();
					license2.SetLicense(license);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
