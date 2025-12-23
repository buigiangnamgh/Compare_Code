using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using DevExpress.Pdf;
using DevExpress.XtraPdfViewer;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.License;

namespace Inventec.Common.SignLibrary
{
	internal class PrintLibProcess
	{
		private static PageSettings currentPageSettings;

		private static PdfPrinterSettings pdfPrinterSettings;

		private static PrinterSettings printerSettings;

		private static int Width_;

		private static int Height_;

		private static int printNumberCopies;

		internal static bool SimplePrint(string inputFile, int copyCount = 1, string printerName = "", PaperSize paperSize = null)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Expected O, but got Unknown
			bool result = false;
			try
			{
				LogSystem.Info("SimplePrint.1");
				LicenceProcess.SetLicenseForAspose();
				PdfViewer val = new PdfViewer();
				val.BindPdf(inputFile);
				val.AutoResize = true;
				val.AutoRotate = true;
				val.PrintPageDialog = true;
				PrinterSettings printerSettings = new PrinterSettings();
				PageSettings pageSettings = new PageSettings();
				PrintDocument printDocument = new PrintDocument();
				Document val2 = new Document(inputFile);
				PrintDialog printDialog = new PrintDialog();
				printDialog.AllowSomePages = true;
				printDialog.PrinterSettings.MinimumPage = 1;
				printDialog.PrinterSettings.MaximumPage = val.PageCount;
				printDialog.PrinterSettings.FromPage = 1;
				printDialog.PrinterSettings.ToPage = val.PageCount;
				printDialog.PrinterSettings.Copies = (short)((copyCount <= 0) ? 1 : ((short)copyCount));
				if (!string.IsNullOrEmpty(printerName))
				{
					printDialog.PrinterSettings.PrinterName = printerName;
				}
				PageCollection pages = val2.Pages;
				Page val3 = pages[1];
				PdfPageEditor val4 = new PdfPageEditor();
				((Facade)val4).BindPdf(inputFile);
				int num = (int)Math.Round(val3.Rect.Width * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				int num2 = (int)Math.Round(val3.Rect.Height * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				if (paperSize != null)
				{
					if (string.IsNullOrEmpty(paperSize.PaperName))
					{
						paperSize.PaperName = paperSize.Kind.ToString();
					}
					printDialog.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
				}
				else if ((num < num2 && num <= 585 && num2 <= 827) || (num > num2 && num <= 827 && num2 <= 585))
				{
					PaperSize paperSize2 = new PaperSize();
					paperSize2.RawKind = 11;
					printDialog.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize(paperSize2.Kind.ToString(), num, num2);
					printDialog.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = paperSize2.RawKind;
					if (val4.GetPageSize(1).IsLandscape)
					{
						val4.Alignment = AlignmentType.Center;
						val.AutoRotate = false;
						val.AutoResize = false;
					}
				}
				if (printDialog.ShowDialog() == DialogResult.OK)
				{
					printerSettings = printDialog.PrinterSettings;
					if (val4.GetPageSize(1).IsLandscape)
					{
						pageSettings.Landscape = true;
					}
					if (printerSettings.DefaultPageSettings.PaperSize != null && printerSettings.DefaultPageSettings.PaperSize.RawKind > 0)
					{
						if (string.IsNullOrEmpty(printerSettings.DefaultPageSettings.PaperSize.PaperName))
						{
							printerSettings.DefaultPageSettings.PaperSize.PaperName = printerSettings.DefaultPageSettings.PaperSize.Kind.ToString();
						}
						pageSettings.PaperSize = printerSettings.DefaultPageSettings.PaperSize;
					}
					else if ((num < num2 && num <= 585 && num2 <= 827) || (num > num2 && num <= 827 && num2 <= 585))
					{
						PaperSize paperSize3 = new PaperSize();
						paperSize3.RawKind = 11;
						pageSettings.PaperSize = new PaperSize(paperSize3.Kind.ToString(), num, num2);
						pageSettings.PaperSize.RawKind = paperSize3.RawKind;
						if (val4.GetPageSize(1).IsLandscape)
						{
							val4.Alignment = AlignmentType.Center;
							val.AutoRotate = false;
							val.AutoResize = false;
						}
					}
					else
					{
						pageSettings.PaperSize = new PaperSize("Custom", num, num2);
					}
					LogSystem.Info(LogUtil.TraceData("pageEditor.GetPageSize(1)", (object)val4.GetPageSize(1)));
					pageSettings.Margins = new Margins(0, 0, 0, 0);
					printerSettings.DefaultPageSettings.PaperSize = pageSettings.PaperSize;
					val.PrintDocumentWithSettings(pageSettings, printerSettings);
					if (val.PrintStatus != null)
					{
						if (val.PrintStatus is Exception)
						{
							Exception ex = val.PrintStatus as Exception;
							LogSystem.Warn("In văn bản lỗi.", ex);
						}
					}
					else
					{
						Console.WriteLine("printing completed without any issue..");
						LogSystem.Debug("printing completed without any issue..");
						result = true;
					}
				}
				val.Close();
				LogSystem.Info("SimplePrint.2");
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
			return result;
		}

		internal static bool ExecutePrintNowJob(string inputFile, int copyCount, string printerName = "", PaperSize paperSize = null, bool isPrintPageDialog = true)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Expected O, but got Unknown
			bool result = false;
			try
			{
				LogSystem.Info("ExecutePrintNowJob.1");
				LicenceProcess.SetLicenseForAspose();
				PdfViewer val = new PdfViewer();
				val.BindPdf(inputFile);
				val.AutoResize = true;
				val.AutoRotate = true;
				val.PrintPageDialog = isPrintPageDialog;
				PrinterSettings printerSettings = new PrinterSettings();
				PageSettings pageSettings = new PageSettings();
				PrintDocument printDocument = new PrintDocument();
				Document val2 = new Document(inputFile);
				PageCollection pages = val2.Pages;
				Page val3 = pages[1];
				PdfPageEditor val4 = new PdfPageEditor();
				((Facade)val4).BindPdf(inputFile);
				int num = (int)Math.Round(val3.Rect.Width * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				int num2 = (int)Math.Round(val3.Rect.Height * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				if (val4.GetPageSize(1).IsLandscape)
				{
					pageSettings.Landscape = true;
				}
				if (paperSize != null && paperSize.RawKind > 0)
				{
					if (string.IsNullOrEmpty(paperSize.PaperName))
					{
						paperSize.PaperName = paperSize.Kind.ToString();
					}
					pageSettings.PaperSize = paperSize;
				}
				else if ((num < num2 && num <= 585 && num2 <= 827) || (num > num2 && num <= 827 && num2 <= 585))
				{
					PaperSize paperSize2 = new PaperSize();
					paperSize2.RawKind = 11;
					pageSettings.PaperSize = new PaperSize(paperSize2.Kind.ToString(), num, num2);
					pageSettings.PaperSize.RawKind = paperSize2.RawKind;
					if (val4.GetPageSize(1).IsLandscape)
					{
						val4.Alignment = AlignmentType.Center;
						val.AutoRotate = false;
						val.AutoResize = false;
					}
				}
				else
				{
					pageSettings.PaperSize = new PaperSize("Custom", num, num2);
				}
				pageSettings.Margins = new Margins(0, 0, 0, 0);
				printerSettings.DefaultPageSettings.PaperSize = pageSettings.PaperSize;
				printerSettings.Copies = (short)((copyCount <= 0) ? 1 : ((short)copyCount));
				val.PrintDocumentWithSettings(pageSettings, printerSettings);
				if (val.PrintStatus != null)
				{
					if (val.PrintStatus is Exception)
					{
						Exception ex = val.PrintStatus as Exception;
						LogSystem.Warn("In văn bản lỗi.", ex);
					}
				}
				else
				{
					Console.WriteLine("printing completed without any issue..");
					LogSystem.Debug("printing completed without any issue..");
					result = true;
				}
				val.Close();
				LogSystem.Info("ExecutePrintNowJob.2");
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
			return result;
		}

		internal static bool SimplePrintDevLib(string inputFile, int copyCount = 1, string printerName = "", PaperSize paperSize = null)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Expected O, but got Unknown
			bool result = false;
			try
			{
				LogSystem.Info("SimplePrintDevLib.1");
				LicenceProcess.SetLicenseForAspose();
				PdfViewer val = new PdfViewer();
				val.BindPdf(inputFile);
				val.AutoResize = true;
				val.AutoRotate = true;
				val.PrintPageDialog = true;
				PrinterSettings printerSettings = new PrinterSettings();
				PageSettings pageSettings = new PageSettings();
				PrintDocument printDocument = new PrintDocument();
				Document val2 = new Document(inputFile);
				PrintDialog printDialog = new PrintDialog();
				printDialog.AllowSomePages = true;
				printDialog.PrinterSettings.MinimumPage = 1;
				printDialog.PrinterSettings.MaximumPage = val.PageCount;
				printDialog.PrinterSettings.FromPage = 1;
				printDialog.PrinterSettings.ToPage = val.PageCount;
				printDialog.PrinterSettings.Copies = (short)((copyCount <= 0) ? 1 : ((short)copyCount));
				if (!string.IsNullOrEmpty(printerName))
				{
					printDialog.PrinterSettings.PrinterName = printerName;
				}
				PageCollection pages = val2.Pages;
				Page val3 = pages[1];
				PdfPageEditor val4 = new PdfPageEditor();
				((Facade)val4).BindPdf(inputFile);
				int num = (int)Math.Round(val3.Rect.Width * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				int num2 = (int)Math.Round(val3.Rect.Height * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				if (paperSize != null)
				{
					if (string.IsNullOrEmpty(paperSize.PaperName))
					{
						paperSize.PaperName = paperSize.Kind.ToString();
					}
					printDialog.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
				}
				else if ((num < num2 && num <= 585 && num2 <= 827) || (num > num2 && num <= 827 && num2 <= 585))
				{
					PaperSize paperSize2 = new PaperSize();
					paperSize2.RawKind = 11;
					printDialog.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize(paperSize2.Kind.ToString(), num, num2);
					printDialog.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = paperSize2.RawKind;
					if (val4.GetPageSize(1).IsLandscape)
					{
						val4.Alignment = AlignmentType.Center;
						val.AutoRotate = false;
						val.AutoResize = false;
					}
				}
				if (printDialog.ShowDialog() == DialogResult.OK)
				{
					printerSettings = printDialog.PrinterSettings;
					if (val4.GetPageSize(1).IsLandscape)
					{
						pageSettings.Landscape = true;
					}
					if (printerSettings.DefaultPageSettings.PaperSize != null && printerSettings.DefaultPageSettings.PaperSize.RawKind > 0)
					{
						if (string.IsNullOrEmpty(printerSettings.DefaultPageSettings.PaperSize.PaperName))
						{
							printerSettings.DefaultPageSettings.PaperSize.PaperName = printerSettings.DefaultPageSettings.PaperSize.Kind.ToString();
						}
						pageSettings.PaperSize = printerSettings.DefaultPageSettings.PaperSize;
					}
					else if ((num < num2 && num <= 585 && num2 <= 827) || (num > num2 && num <= 827 && num2 <= 585))
					{
						PaperSize paperSize3 = new PaperSize();
						paperSize3.RawKind = 11;
						pageSettings.PaperSize = new PaperSize(paperSize3.Kind.ToString(), num, num2);
						pageSettings.PaperSize.RawKind = paperSize3.RawKind;
						if (val4.GetPageSize(1).IsLandscape)
						{
							val4.Alignment = AlignmentType.Center;
							val.AutoRotate = false;
							val.AutoResize = false;
						}
					}
					else
					{
						pageSettings.PaperSize = new PaperSize("Custom", num, num2);
					}
					pageSettings.Margins = new Margins(0, 0, 0, 0);
					printerSettings.DefaultPageSettings.PaperSize = pageSettings.PaperSize;
					PdfViewer pdfViewer = new PdfViewer();
					pdfViewer.Name = "pdfViewer1";
					pdfViewer.DetachStreamAfterLoadComplete = true;
					pdfViewer.LoadDocument(inputFile);
					printerSettings.PrintToFile = true;
					PdfPrinterSettings pdfPrinterSettings = new PdfPrinterSettings(printerSettings);
					pdfPrinterSettings.ScaleMode = PdfPrintScaleMode.Fit;
					pdfPrinterSettings.PageOrientation = ((!pageSettings.Landscape) ? PdfPrintPageOrientation.Portrait : PdfPrintPageOrientation.Landscape);
					pdfViewer.QueryPageSettings += OnQueryPageSettings;
					pdfViewer.Print(pdfPrinterSettings);
					pdfViewer.QueryPageSettings -= OnQueryPageSettings;
					result = true;
				}
				val.Close();
				LogSystem.Info("SimplePrintDevLib.2");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		internal static bool SimplePrintNowDevLib(string inputFile, int copyCount = 1, string printerName = "", PaperSize paperSize = null)
		{
			bool result = false;
			try
			{
				LogSystem.Info("SimplePrintNowDevLib.1");
				currentPageSettings = PdfDocumentProcess.GetPaperSize(inputFile);
				PdfViewer pdfViewer = new PdfViewer();
				pdfViewer.Name = "pdfViewer1";
				pdfViewer.PageSetupDialogShowing += pdfViewer1_PageSetupDialogShowing;
				pdfViewer.DetachStreamAfterLoadComplete = true;
				pdfViewer.LoadDocument(inputFile);
				SizeF pageSize = pdfViewer.GetPageSize(1);
				printerSettings = new PrinterSettings();
				printerSettings.Copies = (short)copyCount;
				printNumberCopies = copyCount;
				if (!string.IsNullOrEmpty(printerName))
				{
					printerSettings.PrinterName = printerName;
				}
				if (paperSize != null)
				{
					printerSettings.DefaultPageSettings.PaperSize = paperSize;
				}
				else if ((Width_ == 582 && Height_ == 826) || ((int)(pageSize.Width * 100f) == 582 && (int)(pageSize.Height * 100f) == 826) || (Width_ == 826 && Height_ == 582) || ((int)(pageSize.Width * 100f) == 826 && (int)(pageSize.Height * 100f) == 582))
				{
					IEnumerable<PaperSize> source = printerSettings.PaperSizes.Cast<PaperSize>();
					PaperSize paperSize2 = source.FirstOrDefault((PaperSize size) => size.Kind == PaperKind.A5);
					printerSettings.DefaultPageSettings.PaperSize = paperSize2;
				}
				else if ((Width_ == 826 && Height_ == 1169) || (Width_ == 1169 && Height_ == 826) || ((int)(pageSize.Width * 100f) == 826 && (int)(pageSize.Height * 100f) == 1169) || ((int)(pageSize.Width * 100f) == 1169 && (int)(pageSize.Height * 100f) == 826))
				{
					IEnumerable<PaperSize> source2 = printerSettings.PaperSizes.Cast<PaperSize>();
					PaperSize paperSize3 = source2.FirstOrDefault((PaperSize size) => size.Kind == PaperKind.A4);
					printerSettings.DefaultPageSettings.PaperSize = paperSize3;
				}
				else
				{
					printerSettings.DefaultPageSettings.PaperSize = currentPageSettings.PaperSize;
				}
				pdfPrinterSettings = new PdfPrinterSettings(printerSettings);
				pdfPrinterSettings.PageOrientation = ((!currentPageSettings.Landscape) ? PdfPrintPageOrientation.Portrait : PdfPrintPageOrientation.Landscape);
				pdfPrinterSettings.ScaleMode = PdfPrintScaleMode.ActualSize;
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => inputFile)), (object)inputFile));
				pdfViewer.QueryPageSettings += OnQueryPageSettings;
				pdfViewer.Print(pdfPrinterSettings);
				pdfViewer.QueryPageSettings -= OnQueryPageSettings;
				LogSystem.Info("SimplePrintNowDevLib.2");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private static void pdfViewer1_PageSetupDialogShowing(object sender, PdfPageSetupDialogShowingEventArgs e)
		{
			try
			{
				e.FormStartPosition = FormStartPosition.CenterScreen;
				int width = 600;
				int height = 400;
				if (Screen.PrimaryScreen != null)
				{
					width = ((Screen.PrimaryScreen.WorkingArea.Width > 400) ? (Screen.PrimaryScreen.WorkingArea.Width - 400) : 100);
					height = ((Screen.PrimaryScreen.WorkingArea.Height > 100) ? (Screen.PrimaryScreen.WorkingArea.Height - 100) : 50);
				}
				e.FormSize = new Size(width, height);
				if (printNumberCopies > 1)
				{
					e.PrinterSettings.Settings.Copies = (short)printNumberCopies;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private static void OnQueryPageSettings(object sender, PdfQueryPageSettingsEventArgs e)
		{
			try
			{
				Width_ = (int)e.PageSize.Width;
				Height_ = (int)e.PageSize.Height;
				if (currentPageSettings == null)
				{
					currentPageSettings = new PageSettings();
				}
				if (currentPageSettings.PaperSize == null)
				{
					currentPageSettings.PaperSize = new PaperSize("Custom", Width_, Height_);
				}
				currentPageSettings.PaperSize.Width = Width_;
				currentPageSettings.PaperSize.Height = Height_;
				if (printerSettings == null)
				{
					printerSettings = new PrinterSettings();
				}
				printerSettings.DefaultPageSettings.PaperSize = currentPageSettings.PaperSize;
				pdfPrinterSettings = new PdfPrinterSettings(printerSettings);
				pdfPrinterSettings.PageOrientation = ((!currentPageSettings.Landscape) ? PdfPrintPageOrientation.Portrait : PdfPrintPageOrientation.Landscape);
				pdfPrinterSettings.ScaleMode = PdfPrintScaleMode.ActualSize;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal static bool ExecutePrintNowJobDevLib(string inputFile, int copyCount, string printerName = "", PaperSize paperSize = null, bool isPrintPageDialog = true)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Expected O, but got Unknown
			bool result = false;
			try
			{
				LogSystem.Info("ExecutePrintNowJobDevLib.1");
				LicenceProcess.SetLicenseForAspose();
				PdfViewer val = new PdfViewer();
				val.BindPdf(inputFile);
				val.AutoResize = true;
				val.AutoRotate = true;
				val.PrintPageDialog = isPrintPageDialog;
				PrinterSettings printerSettings = new PrinterSettings();
				PageSettings pageSettings = new PageSettings();
				PrintDocument printDocument = new PrintDocument();
				Document val2 = new Document(inputFile);
				PageCollection pages = val2.Pages;
				Page val3 = pages[1];
				PdfPageEditor val4 = new PdfPageEditor();
				((Facade)val4).BindPdf(inputFile);
				int num = (int)Math.Round(val3.Rect.Width * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				int num2 = (int)Math.Round(val3.Rect.Height * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				if (val4.GetPageSize(1).IsLandscape)
				{
					pageSettings.Landscape = true;
				}
				if (paperSize != null && paperSize.RawKind > 0)
				{
					if (string.IsNullOrEmpty(paperSize.PaperName))
					{
						paperSize.PaperName = paperSize.Kind.ToString();
					}
					pageSettings.PaperSize = paperSize;
				}
				else if ((num < num2 && num <= 585 && num2 <= 827) || (num > num2 && num <= 827 && num2 <= 585))
				{
					PaperSize paperSize2 = new PaperSize();
					paperSize2.RawKind = 11;
					pageSettings.PaperSize = new PaperSize(paperSize2.Kind.ToString(), num, num2);
					pageSettings.PaperSize.RawKind = paperSize2.RawKind;
					if (val4.GetPageSize(1).IsLandscape)
					{
						val4.Alignment = AlignmentType.Center;
						val.AutoRotate = false;
						val.AutoResize = false;
					}
				}
				else
				{
					pageSettings.PaperSize = new PaperSize("Custom", num, num2);
				}
				pageSettings.Margins = new Margins(0, 0, 0, 0);
				printerSettings.DefaultPageSettings.PaperSize = pageSettings.PaperSize;
				printerSettings.Copies = (short)((copyCount <= 0) ? 1 : ((short)copyCount));
				PdfViewer pdfViewer = new PdfViewer();
				pdfViewer.Name = "pdfViewer1";
				pdfViewer.DetachStreamAfterLoadComplete = true;
				pdfViewer.LoadDocument(inputFile);
				PdfPrinterSettings pdfPrinterSettings = new PdfPrinterSettings(printerSettings);
				pdfPrinterSettings.PageOrientation = PdfPrintPageOrientation.Auto;
				if (pageSettings.Landscape)
				{
					pdfPrinterSettings.PageOrientation = PdfPrintPageOrientation.Landscape;
				}
				pdfPrinterSettings.ScaleMode = PdfPrintScaleMode.Fit;
				pdfViewer.Print(pdfPrinterSettings);
				result = true;
				val.Close();
				LogSystem.Info("ExecutePrintNowJobDevLib.2");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		internal static bool ValidExistsExecutePrintCallExeService()
		{
			bool valid = false;
			try
			{
				string exeServiceFileName = Path.Combine(Application.StartupPath, "Integrate\\PrintService\\HPS.ClientLibrary.exe");
				valid = !string.IsNullOrEmpty(exeServiceFileName) && File.Exists(exeServiceFileName);
				LogSystem.Info("ValidExistsExecutePrintCallExeService" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => valid)), (object)valid) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => exeServiceFileName)), (object)exeServiceFileName));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return valid;
		}

		internal static bool ExecutePrintCallExeService(string inputFile, int copyCount, string printerName = "", PaperSize paperSize = null, bool isPrintPageDialog = true)
		{
			bool result = false;
			try
			{
				LogSystem.Info("ExecutePrintCallExeService.1");
				string text = "";
				text = text + "|InputFile|" + inputFile;
				text += "|ApplicationCode|HIS";
				text = text + "|PrinterName|" + printerName;
				text = text + "|CopyCount|" + copyCount;
				LogSystem.Info("cmdLn = " + text + "____FileName = " + Application.StartupPath + "\\Integrate\\PrintService\\HPS.ClientLibrary.exe");
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				processStartInfo.FileName = Application.StartupPath + "\\Integrate\\PrintService\\HPS.ClientLibrary.exe";
				processStartInfo.Arguments = "\"" + text + "\"";
				Process.Start(processStartInfo);
				LogSystem.Info("ExecutePrintCallExeService.2");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}
	}
}
