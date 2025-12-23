using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Aspose.Cells;
using DevExpress.Spreadsheet;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary;
using Inventec.Common.SignLibrary.License;
using Microsoft.Office.Interop.Excel;

namespace Inventec.Common.Integrate
{
	public class FileConvert
	{
		public static bool ExcelToPdf(MemoryStream inputStream, string inputFile, MemoryStream outputStream, string outputFile)
		{
			try
			{
				if (inputStream != null && inputStream.Length > 0)
				{
					return ExportExcelToPdfUsingApose(inputStream, outputFile);
				}
				if (!string.IsNullOrEmpty(inputFile))
				{
					return ExportExcelToPdfUsingApose(inputFile, outputFile);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return true;
		}

		public static bool ExcelToPdf__Old(string ext, MemoryStream inputStream, string inputFile, MemoryStream outputStream, string outputFile)
		{
			bool result = false;
			try
			{
				if ((inputStream == null || inputStream.Length == 0L) && string.IsNullOrEmpty(inputFile))
				{
					throw new ArgumentNullException("inStream & inFile is null");
				}
				if (outputStream == null && string.IsNullOrEmpty(outputFile))
				{
					throw new ArgumentNullException("outStream & outFile is null");
				}
				DevExpress.Spreadsheet.Workbook workbook = new DevExpress.Spreadsheet.Workbook();
				bool flag = false;
				if (inputStream != null && inputStream.Length > 0)
				{
					flag = ((!(ext == ".xls")) ? workbook.LoadDocument(inputStream, DevExpress.Spreadsheet.DocumentFormat.OpenXml) : workbook.LoadDocument(inputStream, DevExpress.Spreadsheet.DocumentFormat.Xls));
				}
				else if (!string.IsNullOrEmpty(inputFile))
				{
					switch (ext)
					{
					case ".xls":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xls);
						LogSystem.Debug("valid:" + flag + "____inputFile:" + inputFile);
						break;
					case ".xlsm":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xlsm);
						break;
					case ".xltx":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xltx);
						break;
					case ".xlt":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xlt);
						break;
					case ".xltm":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xltm);
						break;
					default:
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
						break;
					}
				}
				if (flag)
				{
					if (outputStream != null)
					{
						workbook.ExportToPdf(outputStream);
						outputStream.Position = 0L;
					}
					if (!string.IsNullOrEmpty(outputFile))
					{
						workbook.ExportToPdf(outputFile);
					}
					workbook.Dispose();
					result = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		public static bool ExportWorkbookToPdf(string workbookPath, string outputPath)
		{
			if (string.IsNullOrEmpty(workbookPath) || string.IsNullOrEmpty(outputPath))
			{
				return false;
			}
			Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
			application.ScreenUpdating = false;
			application.DisplayAlerts = false;
			Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Open(workbookPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
			if (workbook == null)
			{
				application.Quit();
				application = null;
				workbook = null;
				return false;
			}
			bool result = true;
			try
			{
				workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, outputPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
			}
			catch (Exception)
			{
				result = false;
			}
			finally
			{
				workbook.Close(Type.Missing, Type.Missing, Type.Missing);
				application.Quit();
				application = null;
				workbook = null;
			}
			return result;
		}

		public static bool ExportExcelToPdfUsingApose(MemoryStream sourceFile, string pdfFile)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			try
			{
				if (sourceFile == null || sourceFile.Length == 0L || string.IsNullOrEmpty(pdfFile))
				{
					return false;
				}
				LicenceProcess.SetLicenseForAsposeCell();
				Workbook val = new Workbook((Stream)sourceFile);
				string saveExcelFile = Utils.GenerateTempFileWithin(".xlsx");
				val.Save(saveExcelFile, (SaveFormat)6);
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => saveExcelFile)), (object)saveExcelFile));
				val.Save(pdfFile, (SaveFormat)13);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return true;
		}

		public static bool ExportExcelToPdfUsingApose(string sourceFile, string pdfFile)
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			try
			{
				if (sourceFile == null || sourceFile.Length == 0 || string.IsNullOrEmpty(pdfFile))
				{
					return false;
				}
				LicenceProcess.SetLicenseForAsposeCell();
				Workbook val = new Workbook(sourceFile);
				val.Save(pdfFile, (SaveFormat)13);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return true;
		}

		public static bool CombineMultiExcelFile(List<string> sourceFiles, string combineFile)
		{
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Expected O, but got Unknown
			//IL_0160: Unknown result type (might be due to invalid IL or missing references)
			//IL_0167: Expected O, but got Unknown
			try
			{
				if (sourceFiles == null || sourceFiles.Count == 0 || string.IsNullOrEmpty(combineFile))
				{
					return false;
				}
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<List<string>>((Expression<Func<List<string>>>)(() => sourceFiles)), (object)sourceFiles));
				LicenceProcess.SetLicenseForAsposeCell();
				Workbook val = null;
				Worksheet val2 = null;
				Range val3 = null;
				int num = 0;
				int num2 = 0;
				string text = "";
				string strA = "";
				foreach (string sourceFile in sourceFiles)
				{
					if (num == 0)
					{
						val = new Workbook(sourceFile);
						val2 = val.Worksheets[0];
						val3 = val2.Cells.MaxDisplayRange;
						num2 = val3.RowCount;
						PageSetup pageSetup = val2.PageSetup;
						LogSystem.Debug(LogUtil.TraceData("pageSetup1.PrintArea", (object)pageSetup.PrintArea));
						string[] array = pageSetup.PrintArea.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Length != 0)
						{
							text = array[0];
							strA = array[1];
						}
					}
					if (num > 0)
					{
						Workbook val4 = new Workbook(sourceFile);
						Worksheet val5 = val4.Worksheets[0];
						Range maxDisplayRange = val5.Cells.MaxDisplayRange;
						PageSetup pageSetup2 = val5.PageSetup;
						string[] array2 = pageSetup2.PrintArea.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
						if (array2 != null && array2.Length != 0)
						{
							int num3 = string.Compare(array2[0], text);
							if (num3 < 0)
							{
								text = array2[0];
							}
							int num4 = string.Compare(strA, array2[1]);
							if (num4 < 0)
							{
								strA = array2[1];
							}
						}
						Range val6 = val2.Cells.CreateRange(val3.FirstRow + num2, maxDisplayRange.FirstColumn, maxDisplayRange.RowCount, maxDisplayRange.ColumnCount);
						val6.Copy(maxDisplayRange);
						num2 = maxDisplayRange.RowCount + num2;
					}
					num++;
				}
				PageSetup pageSetup3 = val2.PageSetup;
				Range maxDisplayRange2 = val2.Cells.MaxDisplayRange;
				string[] array3 = ((object)maxDisplayRange2).ToString().Split(new string[2] { "!", "]" }, StringSplitOptions.RemoveEmptyEntries);
				if (array3 != null && array3.Length != 0)
				{
					string[] array4 = array3[1].Trim().Replace(" ", "").Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
					if (array4 != null && array4.Length != 0)
					{
						pageSetup3.PrintArea = text + ":";
					}
				}
				val.Save(combineFile);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return true;
		}

		private string GetSheetArea(string str, bool isNumber)
		{
			string result = "";
			if (!string.IsNullOrEmpty(str))
			{
				for (int i = 0; i < str.Length; i++)
				{
				}
			}
			return result;
		}

		public static bool CombineMultiExcelFile(List<MemoryStream> sourceStreams, MemoryStream combineFile)
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			try
			{
				if (sourceStreams == null || sourceStreams.Count == 0 || combineFile == null)
				{
					return false;
				}
				LicenceProcess.SetLicenseForAsposeCell();
				Workbook val = null;
				int num = 0;
				foreach (MemoryStream sourceStream in sourceStreams)
				{
					if (num == 0)
					{
						val = new Workbook((Stream)sourceStream);
					}
					if (num > 0)
					{
						Workbook val2 = new Workbook((Stream)sourceStream);
						val.Combine(val2);
					}
				}
				val.Save((Stream)combineFile, (SaveFormat)0);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return true;
		}

		public static bool ExcelToPdf__Old(string ext, MemoryStream inputStream, string inputFile, byte[] inputByte, MemoryStream outputStream, string outputFile)
		{
			bool result = false;
			try
			{
				if ((inputStream == null || inputStream.Length == 0L) && (inputByte == null || inputByte.Length == 0) && string.IsNullOrEmpty(inputFile))
				{
					throw new ArgumentNullException("inStream & inFile is null");
				}
				if (outputStream == null && string.IsNullOrEmpty(outputFile))
				{
					throw new ArgumentNullException("outStream & outFile is null");
				}
				DevExpress.Spreadsheet.Workbook workbook = new DevExpress.Spreadsheet.Workbook();
				bool flag = false;
				if (inputStream != null && inputStream.Length > 0)
				{
					flag = workbook.LoadDocument(inputStream, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
				}
				else if (inputByte != null && inputByte.Length != 0)
				{
					switch (ext)
					{
					case ".xls":
					{
						string fullPathFile = Utils.GetFullPathFile(Guid.NewGuid().ToString() + ".xls");
						flag = workbook.LoadDocument(inputByte, DevExpress.Spreadsheet.DocumentFormat.Xls);
						LogSystem.Info("valid:" + flag + "____outfileConvert:" + fullPathFile);
						break;
					}
					case ".xlsm":
						flag = workbook.LoadDocument(inputByte, DevExpress.Spreadsheet.DocumentFormat.Xlsm);
						break;
					case ".xltx":
						flag = workbook.LoadDocument(inputByte, DevExpress.Spreadsheet.DocumentFormat.Xltx);
						break;
					case ".xlt":
						flag = workbook.LoadDocument(inputByte, DevExpress.Spreadsheet.DocumentFormat.Xlt);
						break;
					case ".xltm":
						flag = workbook.LoadDocument(inputByte, DevExpress.Spreadsheet.DocumentFormat.Xltm);
						break;
					default:
						flag = workbook.LoadDocument(inputByte, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
						break;
					}
				}
				else if (!string.IsNullOrEmpty(inputFile))
				{
					switch (ext)
					{
					case ".xls":
					{
						string fullPathFile2 = Utils.GetFullPathFile(Guid.NewGuid().ToString() + ".xls");
						File.Copy(inputFile, fullPathFile2);
						flag = workbook.LoadDocument(fullPathFile2, DevExpress.Spreadsheet.DocumentFormat.Xls);
						LogSystem.Info("valid:" + flag + "____outfileConvert:" + fullPathFile2);
						break;
					}
					case ".xlsm":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xlsm);
						break;
					case ".xltx":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xltx);
						break;
					case ".xlt":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xlt);
						break;
					case ".xltm":
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.Xltm);
						break;
					default:
						flag = workbook.LoadDocument(inputFile, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
						break;
					}
				}
				if (flag)
				{
					if (outputStream != null)
					{
						workbook.ExportToPdf(outputStream);
						outputStream.Position = 0L;
					}
					if (!string.IsNullOrEmpty(outputFile))
					{
						workbook.ExportToPdf(outputFile);
					}
					workbook.Dispose();
					result = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		public static bool DocToPdf(MemoryStream inputStream, string inputFile, MemoryStream outputStream, string outputFile, string extension = "")
		{
			bool result = false;
			try
			{
				if ((inputStream == null || inputStream.Length == 0L) && string.IsNullOrEmpty(inputFile))
				{
					throw new ArgumentNullException("inStream & inFile is null");
				}
				if (outputStream == null && string.IsNullOrEmpty(outputFile))
				{
					throw new ArgumentNullException("outStream is null");
				}
				string ext = "";
				RichEditDocumentServer richEditDocumentServer = new RichEditDocumentServer();
				if (!string.IsNullOrEmpty(inputFile))
				{
					ext = Path.GetExtension(inputFile);
					if (ext == ".doc")
					{
						richEditDocumentServer.LoadDocument(inputFile, DevExpress.XtraRichEdit.DocumentFormat.Doc);
					}
					else if (ext == ".html")
					{
						richEditDocumentServer.LoadDocument(inputFile, DevExpress.XtraRichEdit.DocumentFormat.Html);
					}
					else if (ext == ".mht")
					{
						richEditDocumentServer.LoadDocument(inputFile, DevExpress.XtraRichEdit.DocumentFormat.Mht);
					}
					else if (ext == ".txt")
					{
						richEditDocumentServer.LoadDocument(inputFile, DevExpress.XtraRichEdit.DocumentFormat.PlainText);
					}
					else if (ext == ".rtf")
					{
						richEditDocumentServer.LoadDocument(inputFile, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
					}
					else
					{
						richEditDocumentServer.LoadDocument(inputFile, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
					}
				}
				else
				{
					ext = extension;
					LogSystem.Debug("DocToPdf____" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => ext)), (object)ext));
					if (ext == ".doc")
					{
						richEditDocumentServer.LoadDocument(inputStream, DevExpress.XtraRichEdit.DocumentFormat.Doc);
					}
					else if (ext == ".html")
					{
						richEditDocumentServer.LoadDocument(inputStream, DevExpress.XtraRichEdit.DocumentFormat.Html);
					}
					else if (ext == ".mht")
					{
						richEditDocumentServer.LoadDocument(inputStream, DevExpress.XtraRichEdit.DocumentFormat.Mht);
					}
					else if (ext == ".txt")
					{
						richEditDocumentServer.LoadDocument(inputStream, DevExpress.XtraRichEdit.DocumentFormat.PlainText);
					}
					else if (ext == ".rtf")
					{
						richEditDocumentServer.LoadDocument(inputStream, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
					}
					else
					{
						richEditDocumentServer.LoadDocument(inputStream, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
					}
				}
				if (outputStream != null)
				{
					richEditDocumentServer.ExportToPdf(outputStream);
					outputStream.Position = 0L;
				}
				if (!string.IsNullOrEmpty(outputFile))
				{
					PdfExportOptions pdfExportOptions = new PdfExportOptions();
					pdfExportOptions.Compressed = false;
					pdfExportOptions.ImageQuality = PdfJpegImageQuality.Highest;
					using (FileStream stream = new FileStream(outputFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
					{
						richEditDocumentServer.ExportToPdf(stream, pdfExportOptions);
					}
				}
				richEditDocumentServer.Dispose();
				result = true;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}
	}
}
