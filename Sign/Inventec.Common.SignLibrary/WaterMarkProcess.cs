using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using EMR.EFMODEL.DataModels;
using GemBox.Pdf;
using GemBox.Pdf.Content;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignFile;
using Inventec.Common.SignLibrary.ADO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Inventec.Common.SignLibrary
{
	internal class WaterMarkProcess
	{
		internal static void ProcessInsertWaterMark(PdfReader readerWorking, string outPathFile, V_EMR_DOCUMENT document, List<EMR_SIGN> signAlls, bool hasSignInformationPage, ref RichEditControl txtSignDescriptionList)
		{
			try
			{
				int numberOfPages = readerWorking.NumberOfPages;
				int defaultFontSize = 18;
				int wkFontSize = 16;
				using (FileStream os = File.Open(outPathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					using (PdfStamper pdfStamper = new PdfStamper(readerWorking, os))
					{
						PdfLayer layer = new PdfLayer("watermarkPdfLayer", pdfStamper.Writer);
						for (int i = 1; i <= numberOfPages; i++)
						{
							Rectangle pageSize = readerWorking.GetPageSize(i);
							PdfContentByte underContent = pdfStamper.GetUnderContent(i);
							ProcessPdfContentByteInsertWaterMark(i, underContent, ref layer, ref txtSignDescriptionList, defaultFontSize, wkFontSize, pageSize, readerWorking, signAlls, document);
							PdfContentByte overContent = pdfStamper.GetOverContent(i);
							ProcessPdfContentByteInsertWaterMark(i, overContent, ref layer, ref txtSignDescriptionList, defaultFontSize, wkFontSize, pageSize, readerWorking, signAlls, document);
						}
					}
				}
				readerWorking.Close();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private static void ProcessPdfContentByteInsertWaterMark(int i, PdfContentByte cb, ref PdfLayer layer, ref RichEditControl txtSignDescriptionList, int defaultFontSize, int wkFontSize, Rectangle rec, PdfReader readerWorking, List<EMR_SIGN> signAlls, V_EMR_DOCUMENT document)
		{
			try
			{
				cb.BeginLayer(layer);
				cb.SetFontAndSize(SharedUtils.GetBaseFont(), defaultFontSize);
				PdfGState pdfGState = new PdfGState();
				pdfGState.FillOpacity = 0.1f;
				pdfGState.StrokeOpacity = 0.3f;
				PdfGState gState = pdfGState;
				cb.SetGState(gState);
				Rectangle pageSizeWithRotation = readerWorking.GetPageSizeWithRotation(i);
				Rectangle rectangle = rec ?? pageSizeWithRotation;
				float num = (rectangle.Right + rectangle.Left) / 2f;
				float num2 = (rectangle.Bottom + rectangle.Top) / 2f;
				float num3 = (rectangle.Bottom + rectangle.Top) / 8f + 10f;
				string format = "{0}       {1}       {2}       {3}       {4}       {5}       {6}";
				float num4 = 40f;
				int num5 = 1;
				cb.SetColorFill(BaseColor.BLACK);
				cb.BeginText();
				num5 = 1;
				bool flag = false;
				bool flag2 = false;
				string text = "";
				List<EMR_SIGN> list = ((signAlls != null && signAlls.Count > 0) ? (from o in signAlls
					where !string.IsNullOrEmpty(o.DESCRIPTION) && o.SIGN_TIME.HasValue
					orderby o.SIGN_TIME
					select o).ToList() : null);
				if (list != null && list.Count > 0)
				{
					flag = true;
					string text2 = "";
					int num6 = 0;
					int num7 = 0;
					foreach (EMR_SIGN item in list)
					{
						num6++;
						string[] array = item.DESCRIPTION.Split(new string[1] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Length > 1)
						{
							num7 = 0;
							string[] array2 = array;
							string[] array3 = array2;
							foreach (string arg in array3)
							{
								num7++;
								text2 += string.Format("{0}", arg);
								text2 += "<br>";
								text2 += string.Format("<p align=right><b>{0}</b></p>", item.USERNAME);
								if (num7 < array.Length || (num7 == array.Length && num6 < list.Count))
								{
									text2 += "<br>";
								}
							}
						}
						else
						{
							text2 += string.Format("{0}<br><p align=right><b>{1}</b></p>", item.DESCRIPTION, item.USERNAME);
							if (num6 < list.Count)
							{
								text2 += "<br>";
							}
						}
					}
					txtSignDescriptionList.HtmlText = text2;
				}
				List<EMR_SIGN> list2 = ((signAlls != null && signAlls.Count > 0) ? signAlls.Where((EMR_SIGN o) => o.REJECT_TIME.HasValue && !string.IsNullOrEmpty(o.REJECT_REASON)).ToList() : null);
				if (list2 != null && list2.Count > 0)
				{
					flag2 = true;
					foreach (EMR_SIGN item2 in list2)
					{
						text += string.Format("{0} đã từ chối ký, {1}, lý do: {2}    ", item2.USERNAME, DateTimeConvert.TimeNumberToSystemDateTime(item2.REJECT_TIME.GetValueOrDefault()).Value.ToString("dd/MM/yyyy HH:mm:ss"), item2.REJECT_REASON);
					}
					text = string.Format(format, text, text, text, text, text, text, text);
				}
				if (GlobalStore.PrintUsingWaterMark)
				{
					string text3 = string.Format("{0} - {1} - {2}", GlobalStore.LoginName, GlobalStore.UserName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
					string configWatermarkValueOption = GetConfigWatermarkValueOption();
					if (!string.IsNullOrWhiteSpace(configWatermarkValueOption) && document != null)
					{
						text3 = configWatermarkValueOption.Replace("<#DOCUMENT_CODE;>", document.DOCUMENT_CODE).Replace("<#DOCUMENT_NAME;>", document.DOCUMENT_NAME).Replace("<#TREATMENT_CODE;>", document.TREATMENT_CODE)
							.Replace("<#LOGINNAME;>", GlobalStore.LoginName)
							.Replace("<#USER_NAME;>", GlobalStore.UserName)
							.Replace("<#TIME_NOW;>", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
							.Replace("<#CREATE_TIME_STR;>", document.CREATE_TIME.HasValue ? Utils.TimeNumberToTimeString(document.CREATE_TIME.Value) : "");
					}
					string text4 = string.Format(format, text3, text3, text3, text3, text3, text3, text3);
					cb.SetFontAndSize(SharedUtils.GetBaseFont(), 18f);
					cb.SetColorFill(BaseColor.RED);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, -2f * num3, 45f);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 0f - num3, 45f);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 0f, 45f);
					if (flag || flag2)
					{
						cb.SetFontAndSize(Utils.GetBaseFont(), wkFontSize);
						if (flag2)
						{
							cb.SetColorFill(BaseColor.DARK_GRAY);
							cb.ShowTextAligned(1, text, num4 * (float)(1 - num5) + num, 2f * num3, 45f);
						}
					}
					else
					{
						cb.SetColorFill(BaseColor.RED);
						cb.SetFontAndSize(SharedUtils.GetBaseFont(), defaultFontSize);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, num3, 45f);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 2f * num3, 45f);
					}
					cb.SetFontAndSize(SharedUtils.GetBaseFont(), defaultFontSize);
					cb.SetColorFill(BaseColor.RED);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 3f * num3, 45f);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 4f * num3, 45f);
					if (flag || flag2)
					{
						cb.SetFontAndSize(Utils.GetBaseFont(), wkFontSize);
						if (flag2)
						{
							cb.SetColorFill(BaseColor.DARK_GRAY);
							cb.ShowTextAligned(1, text, num4 * (float)(1 - num5) + num, 6f * num3, 45f);
						}
					}
					else
					{
						cb.SetColorFill(BaseColor.RED);
						cb.SetFontAndSize(SharedUtils.GetBaseFont(), defaultFontSize);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 5f * num3, 45f);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 6f * num3, 45f);
					}
					cb.SetFontAndSize(SharedUtils.GetBaseFont(), defaultFontSize);
					cb.SetColorFill(BaseColor.RED);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 7f * num3, 45f);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 8f * num3, 45f);
					if (flag || flag2)
					{
						cb.SetFontAndSize(Utils.GetBaseFont(), wkFontSize);
						if (flag2)
						{
							cb.SetColorFill(BaseColor.DARK_GRAY);
							cb.ShowTextAligned(1, text, num4 * (float)(1 - num5) + num, 10f * num3, 45f);
						}
					}
					else
					{
						cb.SetColorFill(BaseColor.RED);
						cb.SetFontAndSize(SharedUtils.GetBaseFont(), defaultFontSize);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 9f * num3, 45f);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 10f * num3, 45f);
					}
				}
				num5++;
				cb.EndText();
				cb.EndLayer();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private static List<int> GetConfigImage()
		{
			List<int> list = new List<int>();
			try
			{
				List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
				IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGNATURE_APPEARANCE_OPTION");
				EMR_CONFIG eMR_CONFIG = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
				if (eMR_CONFIG != null)
				{
					try
					{
						string text = ((!string.IsNullOrEmpty(eMR_CONFIG.VALUE)) ? eMR_CONFIG.VALUE : eMR_CONFIG.DEFAULT_VALUE);
						string[] array = text.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Count() > 0)
						{
							string[] array2 = array;
							string[] array3 = array2;
							foreach (string text2 in array3)
							{
								if (string.IsNullOrEmpty(text2))
								{
									continue;
								}
								string[] array4 = text2.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
								if (array4 == null || array4.Count() <= 1 || string.IsNullOrEmpty(array4[1]))
								{
									continue;
								}
								string text3 = array4[0].ToLower();
								string text4 = text3;
								string text5 = text4;
								if (!(text5 == "w"))
								{
									if (text5 == "h")
									{
										int item = TypeConvertParse.ToInt32(array4[1]);
										list.Add(item);
									}
								}
								else
								{
									int item2 = TypeConvertParse.ToInt32(array4[1]);
									list.Add(item2);
								}
							}
						}
					}
					catch (Exception ex)
					{
						LogSystem.Warn(ex);
					}
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
			return list;
		}

		internal static void ProcessNoInsertWaterMark(PdfReader readerWorking, string outPathFile)
		{
			try
			{
				int numberOfPages = readerWorking.NumberOfPages;
				List<int> list = new List<int>();
				for (int i = 0; i <= readerWorking.NumberOfPages; i++)
				{
					list.Add(i);
				}
				if (string.IsNullOrEmpty(outPathFile))
				{
					outPathFile = Utils.GenerateTempFileWithin();
				}
				FileStream fileStream = File.Open(outPathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				PdfConcatenate pdfConcatenate = new PdfConcatenate(fileStream);
				readerWorking.SelectPages(list);
				pdfConcatenate.AddPages(readerWorking);
				try
				{
					fileStream.Close();
				}
				catch
				{
				}
				try
				{
					pdfConcatenate.Close();
				}
				catch
				{
				}
				try
				{
					readerWorking.Close();
				}
				catch
				{
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal static void ProcessInsertWaterMark(string inPathFile, string outPathFile, V_EMR_DOCUMENT document)
		{
			try
			{
				PdfReader pdfReader = new PdfReader(inPathFile);
				int numberOfPages = pdfReader.NumberOfPages;
				using (FileStream os = File.Open(outPathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					using (PdfStamper pdfStamper = new PdfStamper(pdfReader, os))
					{
						PdfLayer layer = new PdfLayer("watermarkPdfLayer", pdfStamper.Writer);
						for (int i = 1; i <= numberOfPages; i++)
						{
							Rectangle pageSize = pdfReader.GetPageSize(i);
							PdfContentByte underContent = pdfStamper.GetUnderContent(i);
							ProcessPdfContentByte(i, underContent, ref layer, document, pdfReader, pageSize);
							PdfContentByte overContent = pdfStamper.GetOverContent(i);
							ProcessPdfContentByte(i, overContent, ref layer, document, pdfReader, pageSize);
						}
					}
				}
				pdfReader.Close();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private static void ProcessPdfContentByte(int i, PdfContentByte cb, ref PdfLayer layer, V_EMR_DOCUMENT document, PdfReader readerWorking, Rectangle rec)
		{
			try
			{
				cb.BeginLayer(layer);
				cb.SetFontAndSize(SharedUtils.GetBaseFont(), 18f);
				PdfGState pdfGState = new PdfGState();
				pdfGState.FillOpacity = 0.1f;
				pdfGState.StrokeOpacity = 0.3f;
				PdfGState gState = pdfGState;
				cb.SetGState(gState);
				Rectangle pageSizeWithRotation = readerWorking.GetPageSizeWithRotation(i);
				Rectangle rectangle = rec ?? pageSizeWithRotation;
				float num = (rectangle.Right + rectangle.Left) / 2f;
				float num2 = (rectangle.Bottom + rectangle.Top) / 2f;
				float num3 = (rectangle.Bottom + rectangle.Top) / 8f + 10f;
				string format = "{0}       {1}       {2}       {3}       {4}       {5}       {6}";
				float num4 = 40f;
				int num5 = 1;
				cb.SetColorFill(BaseColor.RED);
				cb.BeginText();
				num5 = 1;
				string text = string.Format("{0} - {1} - {2}", GlobalStore.LoginName, GlobalStore.UserName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
				string configWatermarkValueOption = GetConfigWatermarkValueOption();
				if (!string.IsNullOrWhiteSpace(configWatermarkValueOption) && document != null)
				{
					text = configWatermarkValueOption.Replace("<#DOCUMENT_CODE;>", document.DOCUMENT_CODE).Replace("<#DOCUMENT_NAME;>", document.DOCUMENT_NAME).Replace("<#TREATMENT_CODE;>", document.TREATMENT_CODE)
						.Replace("<#LOGINNAME;>", GlobalStore.LoginName)
						.Replace("<#USER_NAME;>", GlobalStore.UserName)
						.Replace("<#TIME_NOW;>", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
						.Replace("<#CREATE_TIME_STR;>", document.CREATE_TIME.HasValue ? Utils.TimeNumberToTimeString(document.CREATE_TIME.Value) : "");
				}
				string text2 = string.Format(format, text, text, text, text, text, text, text);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, -2f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 0f - num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 0f, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 2f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 3f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 4f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 5f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 6f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 7f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 8f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 9f * num3, 45f);
				cb.ShowTextAligned(1, text2, num4 * (float)(1 - num5) + num, 10f * num3, 45f);
				num5++;
				cb.EndText();
				cb.EndLayer();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal static void ProcessInsertWaterMarkOld(string inPathFile, ImageList imageList1)
		{
			try
			{
				PdfReader pdfReader = new PdfReader(inPathFile);
				int numberOfPages = pdfReader.NumberOfPages;
				int signedCount = Utils.GetSignedCount(pdfReader);
				List<SignPositionADO> list = new List<SignPositionADO>();
				ComponentInfo.SetLicense(GlobalStore.GemBoxPdf__LicKey);
				using (GemBox.Pdf.PdfDocument pdfDocument = GemBox.Pdf.PdfDocument.Load(inPathFile))
				{
					int num = 1;
					foreach (GemBox.Pdf.PdfPage page in pdfDocument.Pages)
					{
						foreach (PdfTextContent item in (from element in page.Content.Elements.All()
							where element.ElementType == PdfContentElementType.Text
							select element).Cast<PdfTextContent>())
						{
							string text = item.ToString();
							GemBox.Pdf.Content.PdfFont font = item.Format.Text.Font;
							PdfColor color = item.Format.Fill.Color;
							PdfPoint location = item.Location;
							if (text.Contains("<SINGLE_KEY__COMMENT_SIGN__"))
							{
								Rectangle reactanle = new Rectangle((float)location.X, (float)location.Y, (float)location.X + 2f, (float)location.Y + 2f);
								list.Add(new SignPositionADO
								{
									PageNUm = num,
									Text = text,
									Reactanle = reactanle
								});
							}
						}
						num++;
					}
				}
				if (list == null || list.Count <= 0)
				{
					return;
				}
				using (FileStream os = File.Open(inPathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					using (PdfStamper pdfStamper = new PdfStamper(pdfReader, os))
					{
						for (int num2 = 1; num2 <= numberOfPages; num2++)
						{
							Rectangle pageSize = pdfReader.GetPageSize(num2);
							PdfContentByte overContent = pdfStamper.GetOverContent(num2);
							List<SignPositionADO> list2 = list.Skip(signedCount).ToList();
							foreach (SignPositionADO item2 in list2)
							{
								Image instance = Image.GetInstance(imageList1.Images[1], BaseColor.YELLOW);
								instance.ScaleToFit(10f, 10f);
								instance.SetAbsolutePosition(item2.Reactanle.Left, item2.Reactanle.Bottom);
								overContent.AddImage(instance);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private static string GetConfigWatermarkValueOption()
		{
			string result = null;
			try
			{
				List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
				EMR_CONFIG eMR_CONFIG = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_DOCUMENT.WARTERMARK.VALUE_OPTION").FirstOrDefault();
				if (eMR_CONFIG != null)
				{
					result = ((!string.IsNullOrEmpty(eMR_CONFIG.VALUE)) ? eMR_CONFIG.VALUE : ((!string.IsNullOrEmpty(eMR_CONFIG.DEFAULT_VALUE)) ? eMR_CONFIG.DEFAULT_VALUE : ""));
				}
			}
			catch (Exception ex)
			{
				result = null;
				LogSystem.Error(ex);
			}
			return result;
		}
	}
}
