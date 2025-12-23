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
using Inventec.Common.SignLibrary.LibraryMessage;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Inventec.Common.SignLibrary
{
	internal class WaterMarkProcess
	{
		internal static void ProcessInsertWaterMark(PdfReader readerWorking, string outPathFile, V_EMR_DOCUMENT document, List<EMR_SIGN> signAlls, bool hasSignInformationPage, ref RichEditControl txtSignDescriptionList, DisplayConfig displayConfig)
		{
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Expected O, but got Unknown
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			try
			{
				LogSystem.Info(string.Format("[ProcessInsertWaterMark] Bắt đầu xử lý file: {0}", outPathFile));
				LogSystem.Info(string.Format("[ProcessInsertWaterMark] Font chữ ký (SizeFont) = {0}", displayConfig.SizeFont));
				int numberOfPages = readerWorking.NumberOfPages;
				LogSystem.Info(string.Format("[ProcessInsertWaterMark] Tổng số trang PDF = {0}", numberOfPages));
				int defaultFontSize = 18;
				int wkFontSize = 16;
				using (FileStream fileStream = File.Open(outPathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					PdfStamper val = new PdfStamper(readerWorking, (Stream)fileStream);
					try
					{
						PdfLayer layer = new PdfLayer("watermarkPdfLayer", val.Writer);
						List<EMR_SIGN> list = ((signAlls != null && signAlls.Count > 0) ? signAlls.Where(delegate(EMR_SIGN o)
						{
							int result;
							if (o.IS_SIGN_ELECTRONIC == 1)
							{
								decimal? cOOR_X_RECTANGLE = o.COOR_X_RECTANGLE;
								if ((cOOR_X_RECTANGLE.GetValueOrDefault() > default(decimal)) & cOOR_X_RECTANGLE.HasValue)
								{
									cOOR_X_RECTANGLE = o.COOR_Y_RECTANGLE;
									result = (((cOOR_X_RECTANGLE.GetValueOrDefault() > default(decimal)) & cOOR_X_RECTANGLE.HasValue) ? 1 : 0);
									goto IL_007f;
								}
							}
							result = 0;
							goto IL_007f;
							IL_007f:
							return (byte)result != 0;
						}).ToList() : null);
						if (list != null && list.Count > 0)
						{
							LogSystem.Info(string.Format("[ProcessInsertWaterMark] Tổng số chữ ký điện tử cần xử lý: {0}", list.Count));
							using (List<EMR_SIGN>.Enumerator enumerator = list.GetEnumerator())
							{
								PdfContentByte overContent;
								for (; enumerator.MoveNext(); overContent.EndText())
								{
									EMR_SIGN current = enumerator.Current;
									int num = (int)(current.PAGE_NUMBER ?? 1);
									LogSystem.Info(string.Format("[ProcessInsertWaterMark] → Đang xử lý chữ ký trên trang {0}", num));
									overContent = val.GetOverContent(num);
									overContent.SetColorFill(BaseColor.BLACK);
									overContent.SetFontAndSize(Utils.GetBaseFont(), (float)displayConfig.SizeFont);
									overContent.BeginText();
									if (current.SIGN_IMAGE != null && current.SIGN_IMAGE.Count() > 0)
									{
										LogSystem.Info(string.Format("[ProcessInsertWaterMark] Có ảnh chữ ký, toạ độ: X={0}, Y={1}", current.COOR_X_RECTANGLE, current.COOR_Y_RECTANGLE));
										Image instance = Image.GetInstance(current.SIGN_IMAGE);
										instance.ScalePercent(80f);
										float num2 = 40f;
										instance.SetAbsolutePosition((float)current.COOR_X_RECTANGLE.Value, (float)current.COOR_Y_RECTANGLE.Value - num2);
										instance.WidthPercentage = SharedUtils.CalculateWidthPercent(displayConfig.WidthRectangle, displayConfig.HeightRectangle, instance, displayConfig.SignaltureImageWidth, 100f, SignPdfAsynchronous.ProcessHeightPlus(100f, displayConfig));
										string arg = ((!string.IsNullOrEmpty(current.RELATION_PEOPLE_NAME)) ? string.Format("{0}({1})", current.RELATION_PEOPLE_NAME, current.RELATION_NAME) : current.VIR_PATIENT_NAME);
										long? pATIENT_SIGNATURE_DISPLAY_TYPE = document.PATIENT_SIGNATURE_DISPLAY_TYPE;
										long? num3 = pATIENT_SIGNATURE_DISPLAY_TYPE;
										if (num3.HasValue)
										{
											long valueOrDefault = num3.GetValueOrDefault();
											if ((ulong)valueOrDefault <= 2uL)
											{
												switch (valueOrDefault)
												{
												case 1L:
													overContent.ShowTextAligned(1, string.Format(MessageUitl.GetMessage("ChuKyDienTuBenhNhanDaKy"), arg, ""), (float)current.COOR_X_RECTANGLE.Value + ((Rectangle)instance).Width / 4f, (float)current.COOR_Y_RECTANGLE.Value - ((Rectangle)instance).Height / 2f - 20f, 0f);
													continue;
												case 2L:
													overContent.AddImage(instance);
													continue;
												case 0L:
													continue;
												}
											}
										}
										overContent.AddImage(instance);
										overContent.ShowTextAligned(1, string.Format(MessageUitl.GetMessage("ChuKyDienTuBenhNhanDaKy"), arg, ""), (float)current.COOR_X_RECTANGLE.Value + ((Rectangle)instance).Width / 4f, (float)current.COOR_Y_RECTANGLE.Value - ((Rectangle)instance).Height / 2f - 20f, 0f);
									}
									else if (current != null && current.COOR_X_RECTANGLE.HasValue && current.COOR_Y_RECTANGLE.HasValue && current.IS_SIGN_BOARD != 1 && current.IS_SIGN_ELECTRONIC + (hasSignInformationPage ? 1 : 0) == num)
									{
										LogSystem.Info(string.Format("[ProcessInsertWaterMark] Không có ảnh, hiển thị chữ ký text tại X={0}, Y={1}", current.COOR_X_RECTANGLE, current.COOR_Y_RECTANGLE));
										string arg2 = ((!string.IsNullOrEmpty(current.RELATION_PEOPLE_NAME)) ? string.Format("{0}({1})", current.RELATION_PEOPLE_NAME, current.RELATION_NAME) : current.VIR_PATIENT_NAME);
										overContent.ShowTextAligned(1, string.Format(MessageUitl.GetMessage("ChuKyDienTuBenhNhanDaKy"), arg2, ""), (float)current.COOR_X_RECTANGLE.Value, (float)current.COOR_Y_RECTANGLE.Value, 0f);
									}
								}
							}
						}
						for (int num4 = 1; num4 <= numberOfPages; num4++)
						{
							Rectangle pageSize = readerWorking.GetPageSize(num4);
							PdfContentByte underContent = val.GetUnderContent(num4);
							ProcessPdfContentByteInsertWaterMark(num4, underContent, ref layer, ref txtSignDescriptionList, defaultFontSize, wkFontSize, pageSize, readerWorking, signAlls, document);
							PdfContentByte overContent2 = val.GetOverContent(num4);
							ProcessPdfContentByteInsertWaterMark(num4, overContent2, ref layer, ref txtSignDescriptionList, defaultFontSize, wkFontSize, pageSize, readerWorking, signAlls, document);
						}
					}
					finally
					{
						if (val != null)
						{
							((IDisposable)val).Dispose();
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
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			try
			{
				cb.BeginLayer((IPdfOCG)(object)layer);
				cb.SetFontAndSize(SharedUtils.GetBaseFont(), (float)defaultFontSize);
				PdfGState gState = new PdfGState
				{
					FillOpacity = 0.1f,
					StrokeOpacity = 0.3f
				};
				cb.SetGState(gState);
				Rectangle pageSizeWithRotation = readerWorking.GetPageSizeWithRotation(i);
				Rectangle val = rec ?? pageSizeWithRotation;
				float num = (val.Right + val.Left) / 2f;
				float num2 = (val.Bottom + val.Top) / 2f;
				float num3 = val.Height / 8f + 10f;
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
							foreach (string arg in array2)
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
				if (GlobalStore.PrintUsingWaterMark == "1" || GlobalStore.PrintUsingWaterMark == "2")
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
						cb.SetFontAndSize(Utils.GetBaseFont(), (float)wkFontSize);
						if (flag2)
						{
							cb.SetColorFill(BaseColor.DARK_GRAY);
							cb.ShowTextAligned(1, text, num4 * (float)(1 - num5) + num, 2f * num3, 45f);
						}
					}
					else
					{
						cb.SetColorFill(BaseColor.RED);
						cb.SetFontAndSize(SharedUtils.GetBaseFont(), (float)defaultFontSize);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, num3, 45f);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 2f * num3, 45f);
					}
					cb.SetFontAndSize(SharedUtils.GetBaseFont(), (float)defaultFontSize);
					cb.SetColorFill(BaseColor.RED);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 3f * num3, 45f);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 4f * num3, 45f);
					if (flag || flag2)
					{
						cb.SetFontAndSize(Utils.GetBaseFont(), (float)wkFontSize);
						if (flag2)
						{
							cb.SetColorFill(BaseColor.DARK_GRAY);
							cb.ShowTextAligned(1, text, num4 * (float)(1 - num5) + num, 6f * num3, 45f);
						}
					}
					else
					{
						cb.SetColorFill(BaseColor.RED);
						cb.SetFontAndSize(SharedUtils.GetBaseFont(), (float)defaultFontSize);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 5f * num3, 45f);
						cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 6f * num3, 45f);
					}
					cb.SetFontAndSize(SharedUtils.GetBaseFont(), (float)defaultFontSize);
					cb.SetColorFill(BaseColor.RED);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 7f * num3, 45f);
					cb.ShowTextAligned(1, text4, num4 * (float)(1 - num5) + num, 8f * num3, 45f);
					if (flag || flag2)
					{
						cb.SetFontAndSize(Utils.GetBaseFont(), (float)wkFontSize);
						if (flag2)
						{
							cb.SetColorFill(BaseColor.DARK_GRAY);
							cb.ShowTextAligned(1, text, num4 * (float)(1 - num5) + num, 10f * num3, 45f);
						}
					}
					else
					{
						cb.SetColorFill(BaseColor.RED);
						cb.SetFontAndSize(SharedUtils.GetBaseFont(), (float)defaultFontSize);
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
				EMR_CONFIG val = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
				if (val != null)
				{
					try
					{
						string text = ((!string.IsNullOrEmpty(val.VALUE)) ? val.VALUE : val.DEFAULT_VALUE);
						string[] array = text.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Count() > 0)
						{
							string[] array2 = array;
							foreach (string text2 in array2)
							{
								if (string.IsNullOrEmpty(text2))
								{
									continue;
								}
								string[] array3 = text2.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
								if (array3 == null || array3.Count() <= 1 || string.IsNullOrEmpty(array3[1]))
								{
									continue;
								}
								string text3 = array3[0].ToLower();
								string text4 = text3;
								string text5 = text4;
								if (!(text5 == "w"))
								{
									if (text5 == "h")
									{
										int item = TypeConvertParse.ToInt32(array3[1]);
										list.Add(item);
									}
								}
								else
								{
									int item2 = TypeConvertParse.ToInt32(array3[1]);
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
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Expected O, but got Unknown
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
				PdfConcatenate val = new PdfConcatenate((Stream)fileStream);
				readerWorking.SelectPages((ICollection<int>)list);
				val.AddPages(readerWorking);
				try
				{
					fileStream.Close();
				}
				catch
				{
				}
				try
				{
					val.Close();
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
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Expected O, but got Unknown
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			try
			{
				PdfReader val = new PdfReader(inPathFile);
				int numberOfPages = val.NumberOfPages;
				using (FileStream fileStream = File.Open(outPathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					PdfStamper val2 = new PdfStamper(val, (Stream)fileStream);
					try
					{
						PdfLayer layer = new PdfLayer("watermarkPdfLayer", val2.Writer);
						for (int i = 1; i <= numberOfPages; i++)
						{
							Rectangle pageSize = val.GetPageSize(i);
							PdfContentByte underContent = val2.GetUnderContent(i);
							ProcessPdfContentByte(i, underContent, ref layer, document, val, pageSize);
							PdfContentByte overContent = val2.GetOverContent(i);
							ProcessPdfContentByte(i, overContent, ref layer, document, val, pageSize);
						}
					}
					finally
					{
						if (val2 != null)
						{
							((IDisposable)val2).Dispose();
						}
					}
				}
				val.Close();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private static void ProcessPdfContentByte(int i, PdfContentByte cb, ref PdfLayer layer, V_EMR_DOCUMENT document, PdfReader readerWorking, Rectangle rec)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			try
			{
				cb.BeginLayer((IPdfOCG)(object)layer);
				cb.SetFontAndSize(SharedUtils.GetBaseFont(), 18f);
				PdfGState gState = new PdfGState
				{
					FillOpacity = 0.1f,
					StrokeOpacity = 0.3f
				};
				cb.SetGState(gState);
				Rectangle pageSizeWithRotation = readerWorking.GetPageSizeWithRotation(i);
				Rectangle val = rec ?? pageSizeWithRotation;
				float num = (val.Right + val.Left) / 2f;
				float num2 = (val.Bottom + val.Top) / 2f;
				float num3 = (val.Bottom + val.Top) / 8f + 10f;
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
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Expected O, but got Unknown
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0121: Expected O, but got Unknown
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c6: Expected O, but got Unknown
			try
			{
				PdfReader val = new PdfReader(inPathFile);
				int numberOfPages = val.NumberOfPages;
				int signedCount = Utils.GetSignedCount(val);
				List<SignPositionADO> list = new List<SignPositionADO>();
				ComponentInfo.SetLicense(GlobalStore.GemBoxPdf__LicKey);
				PdfDocument val2 = PdfDocument.Load(inPathFile);
				try
				{
					int num = 1;
					foreach (PdfPage page in val2.Pages)
					{
						foreach (PdfTextContent item in (from element in page.Content.Elements.All()
							where (int)element.ElementType == 0
							select element).Cast<PdfTextContent>())
						{
							string text = ((object)item).ToString();
							PdfFont font = ((PdfVisualContentElement)item).Format.Text.Font;
							PdfColor color = ((PdfVisualContentElement)item).Format.Fill.Color;
							PdfPoint location = item.Location;
							if (text.Contains("<SINGLE_KEY__COMMENT_SIGN__"))
							{
								Rectangle reactanle = new Rectangle((float)((PdfPoint)(ref location)).X, (float)((PdfPoint)(ref location)).Y, (float)((PdfPoint)(ref location)).X + 2f, (float)((PdfPoint)(ref location)).Y + 2f);
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
				finally
				{
					if (val2 != null)
					{
						((IDisposable)val2).Dispose();
					}
				}
				if (list == null || list.Count <= 0)
				{
					return;
				}
				using (FileStream fileStream = File.Open(inPathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					PdfStamper val3 = new PdfStamper(val, (Stream)fileStream);
					try
					{
						for (int num2 = 1; num2 <= numberOfPages; num2++)
						{
							Rectangle pageSize = val.GetPageSize(num2);
							PdfContentByte overContent = val3.GetOverContent(num2);
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
					finally
					{
						if (val3 != null)
						{
							((IDisposable)val3).Dispose();
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
				EMR_CONFIG val = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.EMR_DOCUMENT.WARTERMARK.VALUE_OPTION").FirstOrDefault();
				if (val != null)
				{
					result = ((!string.IsNullOrEmpty(val.VALUE)) ? val.VALUE : ((!string.IsNullOrEmpty(val.DEFAULT_VALUE)) ? val.DEFAULT_VALUE : ""));
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
