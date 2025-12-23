using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using com.itextpdf.text.pdf.security;
using Inventec.Common.Logging;
using iTextSharp.text;
using iTextSharp.text.io;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Inventec.Common.SignFile
{
	public class SignPdfAsynchronous
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass9_0
		{
			public DisplayConfig displayConfig;

			public string strDate;

			public string displayText;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6
		{
			public _003C_003Ec__DisplayClass9_0 CS_0024_003C_003E8__locals75;

			public DisplayConfig displayConfig;

			public string strDate;
		}

		private const string CRYPT_ALG = "RSA";

		private static string hashAlgorithmCfg;

		internal static string HASH_ALG
		{
			get
			{
				if (hashAlgorithmCfg == null)
				{
					try
					{
						hashAlgorithmCfg = ConfigurationManager.AppSettings["Inventec.Common.SignFile.Hash_Algorithm"];
						if (string.IsNullOrWhiteSpace(hashAlgorithmCfg))
						{
							hashAlgorithmCfg = "SHA1";
						}
					}
					catch (Exception ex)
					{
						LogSystem.Error(ex);
						hashAlgorithmCfg = "SHA1";
					}
				}
				return hashAlgorithmCfg;
			}
		}

		public List<byte[]> CreateHash(string inFile, string tempFile, string fileName, X509Certificate[] chain, DisplayConfig displayConfig)
		{
			if (!EmptySignature(inFile, tempFile, fileName, displayConfig, chain[0]))
			{
				return null;
			}
			return PreSign(tempFile, fileName, chain, displayConfig);
		}

		internal static void ProcessFontSizeFit(DisplayConfig displayConfig)
		{
			int newSizeFont = 0;
			if (displayConfig.WidthRectangle > 0f)
			{
				if (displayConfig.WidthRectangle <= 30f && displayConfig.SizeFont >= 2)
				{
					newSizeFont = 1;
				}
				if (displayConfig.WidthRectangle <= 40f && displayConfig.SizeFont >= 3)
				{
					newSizeFont = 2;
				}
				else if (displayConfig.WidthRectangle <= 60f && displayConfig.SizeFont >= 4)
				{
					newSizeFont = 3;
				}
				else if (displayConfig.WidthRectangle <= 80f && displayConfig.SizeFont >= 5)
				{
					newSizeFont = 4;
				}
				else if (displayConfig.WidthRectangle <= 120f && displayConfig.SizeFont >= 6)
				{
					newSizeFont = 5;
				}
				else if (displayConfig.WidthRectangle <= 160f && displayConfig.SizeFont >= 7)
				{
					newSizeFont = 6;
				}
			}
			if (displayConfig.SizeFont != newSizeFont && newSizeFont > 0)
			{
				displayConfig.SizeFont = newSizeFont;
				LogSystem.Debug("Kiem tra SizeFont cua vung chu ky, neu do rong cua vung ky duoc cau hinh  khong phu hop voi SizeFont thi tu dong dieu chinh cho phu hop____" + LogUtil.TraceData("oldSizeFont", displayConfig.SizeFont) + LogUtil.TraceData(LogUtil.GetMemberName(() => newSizeFont), newSizeFont));
			}
		}

		public static float ProcessHeightPlus(float widthImagePercent, DisplayConfig displayConfig)
		{
			float result = 0f;
			if (widthImagePercent == 100f)
			{
				if (displayConfig.WidthRectangle <= 30f)
				{
					result = 5f;
				}
				result = ((displayConfig.WidthRectangle <= 40f) ? 5f : ((displayConfig.WidthRectangle <= 60f) ? 10f : ((displayConfig.WidthRectangle <= 80f) ? 15f : ((displayConfig.WidthRectangle <= 100f) ? 25f : ((!(displayConfig.WidthRectangle <= 120f)) ? 40f : 30f)))));
			}
			return result;
		}

		public static float ProcessHeightPlus(float widthImagePercent, float WidthRectangle)
		{
			float result = 0f;
			if (widthImagePercent == 100f)
			{
				if (WidthRectangle <= 30f)
				{
					result = 5f;
				}
				result = ((WidthRectangle <= 40f) ? 5f : ((WidthRectangle <= 60f) ? 10f : ((WidthRectangle <= 80f) ? 15f : ((WidthRectangle <= 100f) ? 25f : ((!(WidthRectangle <= 120f)) ? 40f : 30f)))));
			}
			return result;
		}

		public bool EmptySignature(string inFile, string outFile, string fieldName, DisplayConfig displayConfig, X509Certificate cert)
		{
			PdfReader pdfReader = null;
			FileStream fileStream = null;
			bool success;
			try
			{
				pdfReader = new PdfReader(inFile);
				int numberOfPages = pdfReader.NumberOfPages;
				int num = displayConfig.NumberPageSign;
				if (num < 1 || num > numberOfPages)
				{
					num = 1;
				}
				fileStream = new FileStream(outFile, FileMode.Create);
				bool IsRebuilt = pdfReader.IsRebuilt();
				LogSystem.Debug("EmptySignature: " + LogUtil.TraceData(LogUtil.GetMemberName(() => IsRebuilt), IsRebuilt));
				PdfSignatureAppearance pdfSignatureAppearance = null;
				if (IsRebuilt)
				{
					pdfReader.Catalog.Remove(PdfName.PERMS);
					pdfReader.RemoveUsageRights();
					pdfSignatureAppearance = PdfStamper.CreateSignature(pdfReader, fileStream, '\0', null).SignatureAppearance;
				}
				else
				{
					pdfSignatureAppearance = PdfStamper.CreateSignature(pdfReader, fileStream, '\0', null, true).SignatureAppearance;
				}
				DateTime signDate = displayConfig.SignDate;
				if ("".Equals(displayConfig.Contact))
				{
					displayConfig.Contact = SharedUtils.GetCN(cert);
				}
				pdfSignatureAppearance.Contact = displayConfig.Contact;
				pdfSignatureAppearance.SignDate = signDate;
				pdfSignatureAppearance.Reason = displayConfig.Reason;
				pdfSignatureAppearance.Location = displayConfig.Location;
				pdfSignatureAppearance.Certificate = cert;
				string strDate = string.Format(displayConfig.DateFormatstring, signDate);
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => displayConfig), displayConfig));
				if (displayConfig.IsDisplaySignature)
				{
					float num2 = displayConfig.CoorXRectangle - displayConfig.WidthRectangle / 2f;
					float num3 = displayConfig.CoorYRectangle - displayConfig.HeightRectangle / 2f;
					float widthRectangle = displayConfig.WidthRectangle;
					float heightRectangle = displayConfig.HeightRectangle;
					ProcessFontSizeFit(displayConfig);
					float num4 = 0f;
					float num5 = 0f;
					Image image = null;
					if (displayConfig.TypeDisplay == Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT)
					{
						if (!string.IsNullOrEmpty(displayConfig.PathImage) && File.Exists(displayConfig.PathImage))
						{
							image = Image.GetInstance(displayConfig.PathImage);
						}
						else if (displayConfig.BImage != null)
						{
							image = Image.GetInstance(displayConfig.BImage);
						}
					}
					else if (displayConfig.TypeDisplay == Constans.DISPLAY_IMAGE_STAMP)
					{
						if (!string.IsNullOrEmpty(displayConfig.PathImage) && File.Exists(displayConfig.PathImage))
						{
							image = Image.GetInstance(displayConfig.PathImage);
						}
						else if (displayConfig.BImage != null)
						{
							image = Image.GetInstance(displayConfig.BImage);
						}
					}
					if (displayConfig.SignType == Constans.SIGN_TYPE_CREATE_NEW_EMPTY_SIGNATURE_FIELD)
					{
						Rectangle pageRect = new Rectangle(num2, num3, num2 + widthRectangle + num4, num3 + heightRectangle + num5);
						pdfSignatureAppearance.SetVisibleSignature(pageRect, num, fieldName);
					}
					else
					{
						pdfSignatureAppearance.SetVisibleSignature(fieldName);
					}
					if (displayConfig.TypeDisplay == Constans.DISPLAY_IMAGE_STAMP)
					{
						float totalWidth = widthRectangle;
						PdfTemplate layer = pdfSignatureAppearance.GetLayer(2);
						float left = layer.BoundingBox.Left;
						float bottom = layer.BoundingBox.Bottom;
						float width = layer.BoundingBox.Width;
						float height = layer.BoundingBox.Height;
						ColumnText columnText = new ColumnText(layer);
						PdfPCell pdfPCell = new PdfPCell();
						if (image != null)
						{
							image.Alignment = 1;
							float plusH = ProcessHeightPlus(100f, displayConfig);
							image.WidthPercentage = SharedUtils.CalculateWidthPercent(widthRectangle, heightRectangle, image, displayConfig.SignaltureImageWidth, 100f, plusH);
							pdfPCell.AddElement(image);
							pdfPCell.HorizontalAlignment = 1;
							pdfPCell.VerticalAlignment = 5;
							pdfPCell.Border = 0;
							pdfPCell.MinimumHeight = heightRectangle;
							LogSystem.Info(LogUtil.TraceData("instance.WidthPercentage", image.WidthPercentage) + LogUtil.TraceData("instance.Width", image.Width) + LogUtil.TraceData("displayConfig.SignaltureImageWidth", displayConfig.SignaltureImageWidth));
						}
						PdfPTable pdfPTable = new PdfPTable(1);
						pdfPTable.TotalWidth = totalWidth;
						pdfPTable.LockedWidth = true;
						pdfPTable.AddCell(pdfPCell);
						columnText.AddElement(pdfPTable);
						columnText.SetSimpleColumn(left, bottom, width, height);
						columnText.Alignment = 1;
						columnText.Go();
					}
					else if (displayConfig.TypeDisplay == Constans.DISPLAY_IMAGE_STAMP_WITH_TEXT)
					{
						string displayText = GetDisplayText(displayConfig, strDate);
						float widthImagePercent = 0f;
						PdfPTable pdfPTable2 = null;
						if (displayConfig.TextPosition == Constans.TEXT_POSITON.x100)
						{
							pdfPTable2 = new PdfPTable(1);
							widthImagePercent = 100f;
						}
						else if (displayConfig.TextPosition == Constans.TEXT_POSITON.x25x75)
						{
							pdfPTable2 = new PdfPTable(new float[2] { 25f, 75f });
							widthImagePercent = 25f;
						}
						else if (displayConfig.TextPosition == Constans.TEXT_POSITON.x30x70)
						{
							pdfPTable2 = new PdfPTable(new float[2] { 30f, 70f });
							widthImagePercent = 30f;
						}
						else if (displayConfig.TextPosition == Constans.TEXT_POSITON.x40x60)
						{
							pdfPTable2 = new PdfPTable(new float[2] { 40f, 60f });
							widthImagePercent = 40f;
						}
						else if (displayConfig.TextPosition == Constans.TEXT_POSITON.x50x50)
						{
							pdfPTable2 = new PdfPTable(new float[2] { 50f, 50f });
							widthImagePercent = 50f;
						}
						else if (displayConfig.TextPosition == Constans.TEXT_POSITON.x60x40)
						{
							pdfPTable2 = new PdfPTable(new float[2] { 60f, 40f });
							widthImagePercent = 40f;
						}
						else if (displayConfig.TextPosition == Constans.TEXT_POSITON.x70x30)
						{
							pdfPTable2 = new PdfPTable(new float[2] { 70f, 30f });
							widthImagePercent = 30f;
						}
						else if (displayConfig.TextPosition == Constans.TEXT_POSITON.x75x25)
						{
							pdfPTable2 = new PdfPTable(new float[2] { 75f, 25f });
							widthImagePercent = 25f;
						}
						PdfTemplate layer2 = pdfSignatureAppearance.GetLayer(2);
						float left2 = layer2.BoundingBox.Left;
						float bottom2 = layer2.BoundingBox.Bottom;
						float width2 = layer2.BoundingBox.Width;
						float height2 = layer2.BoundingBox.Height;
						ColumnText columnText2 = new ColumnText(layer2);
						PdfPCell pdfPCell2 = new PdfPCell();
						if (image != null)
						{
							image.Alignment = 1;
							float plusH2 = ProcessHeightPlus(widthImagePercent, displayConfig);
							image.WidthPercentage = SharedUtils.CalculateWidthPercent(widthRectangle, heightRectangle, image, displayConfig.SignaltureImageWidth, widthImagePercent, plusH2);
							pdfPCell2.AddElement(image);
							pdfPCell2.HorizontalAlignment = 1;
							pdfPCell2.VerticalAlignment = 5;
							pdfPCell2.Border = 0;
							LogSystem.Info(LogUtil.TraceData("instance.WidthPercentage", image.WidthPercentage) + LogUtil.TraceData("instance.Width", image.Width) + LogUtil.TraceData("displayConfig.SignaltureImageWidth", displayConfig.SignaltureImageWidth));
						}
						PdfPCell textCell = SignPdfFile.GetTextCell(displayText, displayConfig);
						pdfPTable2.TotalWidth = widthRectangle;
						pdfPTable2.LockedWidth = true;
						if (displayConfig.TextPosition == Constans.TEXT_POSITON.x100 || displayConfig.TextPosition == Constans.TEXT_POSITON.x25x75 || displayConfig.TextPosition == Constans.TEXT_POSITON.x30x70 || displayConfig.TextPosition == Constans.TEXT_POSITON.x40x60 || displayConfig.TextPosition == Constans.TEXT_POSITON.x50x50)
						{
							pdfPTable2.AddCell(pdfPCell2);
							pdfPTable2.AddCell(textCell);
						}
						else
						{
							pdfPTable2.AddCell(textCell);
							pdfPTable2.AddCell(pdfPCell2);
						}
						PdfPTable pdfPTable3 = new PdfPTable(1);
						PdfPCell pdfPCell3 = new PdfPCell();
						pdfPCell3.AddElement(pdfPTable2);
						pdfPCell3.HorizontalAlignment = 1;
						pdfPCell3.VerticalAlignment = 5;
						pdfPCell3.Border = 0;
						pdfPCell3.MinimumHeight = heightRectangle;
						pdfPTable3.TotalWidth = widthRectangle;
						pdfPTable3.LockedWidth = true;
						pdfPTable3.AddCell(pdfPCell3);
						columnText2.AddElement(pdfPTable3);
						columnText2.SetSimpleColumn(left2, bottom2, width2, height2);
						columnText2.Alignment = 1;
						columnText2.Go();
						LogSystem.Debug(LogUtil.TraceData("instance.Width", (image != null) ? image.Width : 0f) + LogUtil.TraceData("instance.Height", (image != null) ? image.Height : 0f) + LogUtil.TraceData("widthRectangle", widthRectangle) + LogUtil.TraceData("heightRectangle", heightRectangle));
					}
					else if (displayConfig.TypeDisplay == Constans.DISPLAY_RECTANGLE_TEXT)
					{
						PdfTemplate layer3 = pdfSignatureAppearance.GetLayer(2);
						float left3 = layer3.BoundingBox.Left;
						float bottom3 = layer3.BoundingBox.Bottom;
						float width3 = layer3.BoundingBox.Width;
						float height3 = layer3.BoundingBox.Height;
						ColumnText columnText3 = new ColumnText(layer3);
						columnText3.SetSimpleColumn(left3, bottom3, width3, height3);
						columnText3.Alignment = 5;
						string displayText2 = GetDisplayText(displayConfig, strDate);
						PdfPCell textCell2 = SignPdfFile.GetTextCell(displayText2, displayConfig);
						textCell2.MinimumHeight = heightRectangle;
						PdfPTable pdfPTable4 = new PdfPTable(1);
						pdfPTable4.TotalWidth = widthRectangle;
						pdfPTable4.HorizontalAlignment = 1;
						pdfPTable4.LockedWidth = true;
						pdfPTable4.AddCell(textCell2);
						pdfPTable4.CompleteRow();
						columnText3.AddElement(pdfPTable4);
						columnText3.Go();
					}
					LogSystem.Info("EmptySignature____" + LogUtil.TraceData(LogUtil.GetMemberName(() => displayConfig.TypeDisplay), displayConfig.TypeDisplay));
				}
				else if (displayConfig.SignType == Constans.SIGN_TYPE_CREATE_NEW_EMPTY_SIGNATURE_FIELD)
				{
					pdfSignatureAppearance.SetVisibleSignature(new Rectangle(0f, 0f, 0f, 0f), 1, fieldName);
				}
				else
				{
					pdfSignatureAppearance.SetVisibleSignature(fieldName);
				}
				IExternalSignatureContainer externalSignatureContainer = new ExternalBlankSignatureContainer(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED);
				MakeSignature.SignExternalContainer(pdfSignatureAppearance, externalSignatureContainer, 8192);
				success = true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				success = false;
			}
			finally
			{
				if (pdfReader != null)
				{
					pdfReader.Close();
				}
				if (fileStream != null)
				{
					try
					{
						fileStream.Close();
						fileStream.Dispose();
					}
					catch (IOException ex2)
					{
						LogSystem.Warn("Error emptySignature: " + ex2.Message);
					}
				}
			}
			LogSystem.Debug("EmptySignature.____" + LogUtil.TraceData(LogUtil.GetMemberName(() => success), success));
			return success;
		}

		internal static string GetDisplayText(DisplayConfig displayConfig, string strDate)
		{
			_003C_003Ec__DisplayClass6 CS_0024_003C_003E8__locals80 = new _003C_003Ec__DisplayClass6();
			CS_0024_003C_003E8__locals80.displayConfig = displayConfig;
			CS_0024_003C_003E8__locals80.strDate = strDate;
			CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75 = new _003C_003Ec__DisplayClass9_0();
			CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig = CS_0024_003C_003E8__locals80.displayConfig;
			CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.strDate = CS_0024_003C_003E8__locals80.strDate;
			CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayText = string.Empty;
			try
			{
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals80.displayConfig), CS_0024_003C_003E8__locals80.displayConfig) + LogUtil.TraceData(LogUtil.GetMemberName(Expression.Lambda<Func<string>>(Expression.Field(Expression.Constant(CS_0024_003C_003E8__locals80), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), CS_0024_003C_003E8__locals80.strDate));
				if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.DisplayText != null && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.DisplayText.Length != 0)
				{
					CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayText = CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.DisplayText;
				}
				else
				{
					List<object> list = new List<object>();
					List<string> list2 = new List<string>();
					string text = "";
					string text2 = "";
					if (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Location) && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Location.Contains("|"))
					{
						string[] array = CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Location.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Length != 0)
						{
							text = array[0];
						}
						if (array != null && array.Length > 1)
						{
							text2 = array[1];
						}
					}
					else
					{
						text = CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Location;
						if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Titles != null && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Titles.Length != 0)
						{
							text2 = CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Titles[0];
						}
					}
					if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText == Constans.SIGN_TEXT_FORMAT_3__NO_DATE || CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText == Constans.SIGN_TEXT_FORMAT_USER)
					{
						list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Contact.Replace("\\", ""));
						list2.Add(Constans.SIGN_TEXT_FORMAT_USER.Replace("0", list2.Count.ToString()));
						if (!string.IsNullOrEmpty(text2))
						{
							list.Add(text2);
							list2.Add(Constans.SIGN_TEXT_FORMAT_TITLE.Replace("0", list2.Count.ToString()));
						}
						if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.HasValue && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.Value && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason))
						{
							list2.Add(Constans.SIGN_TEXT_FORMAT_REASON.Replace("0", list2.Count.ToString()));
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason);
						}
						CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText = string.Join("\r\n", list2);
					}
					else if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText == Constans.SIGN_TEXT_FORMAT_3__NO_TITLE)
					{
						list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Contact.Replace("\\", ""));
						list2.Add(Constans.SIGN_TEXT_FORMAT_USER.Replace("0", list2.Count.ToString()));
						list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.strDate);
						list2.Add(Constans.SIGN_TEXT_FORMAT_DATE.Replace("0", list2.Count.ToString()));
						if (!string.IsNullOrEmpty(text))
						{
							list.Add(text);
							list2.Add(Constans.SIGN_TEXT_FORMAT_PLACE.Replace("0", list2.Count.ToString()));
						}
						if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.HasValue && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.Value && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason))
						{
							list2.Add(Constans.SIGN_TEXT_FORMAT_REASON.Replace("0", list2.Count.ToString()));
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason);
						}
						CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText = string.Join("\r\n", list2);
					}
					else
					{
						if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText == "2")
						{
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Contact.Replace("\\", ""));
							list2.Add(Constans.SIGN_TEXT_FORMAT_USER.Replace("0", list2.Count.ToString()));
							if (!string.IsNullOrEmpty(text2))
							{
								list.Add(text2);
								list2.Add(Constans.SIGN_TEXT_FORMAT_TITLE.Replace("0", list2.Count.ToString()));
							}
							if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.HasValue && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.Value && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason))
							{
								list2.Add(Constans.SIGN_TEXT_FORMAT_REASON.Replace("0", list2.Count.ToString()));
								list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason);
							}
						}
						else if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText == "3")
						{
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Contact.Replace("\\", ""));
							list2.Add(Constans.SIGN_TEXT_FORMAT_USER.Replace("0", list2.Count.ToString()));
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.strDate);
							list2.Add(Constans.SIGN_TEXT_FORMAT_DATE.Replace("0", list2.Count.ToString()));
							if (!string.IsNullOrEmpty(text))
							{
								list.Add(text);
								list2.Add(Constans.SIGN_TEXT_FORMAT_PLACE.Replace("0", list2.Count.ToString()));
							}
							if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.HasValue && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.Value && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason))
							{
								list2.Add(Constans.SIGN_TEXT_FORMAT_REASON.Replace("0", list2.Count.ToString()));
								list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason);
							}
						}
						else if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText == "4")
						{
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Contact.Replace("\\", ""));
							list2.Add(Constans.SIGN_TEXT_FORMAT_USER.Replace("0", list2.Count.ToString()));
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.strDate);
							list2.Add(Constans.SIGN_TEXT_FORMAT_DATE.Replace("0", list2.Count.ToString()));
							if (!string.IsNullOrEmpty(text2))
							{
								list.Add(text2);
								list2.Add(Constans.SIGN_TEXT_FORMAT_TITLE.Replace("0", list2.Count.ToString()));
							}
							if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.HasValue && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.Value && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason))
							{
								list2.Add(Constans.SIGN_TEXT_FORMAT_REASON.Replace("0", list2.Count.ToString()));
								list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason);
							}
						}
						else if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText == "5")
						{
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Contact.Replace("\\", ""));
							list2.Add(Constans.SIGN_TEXT_FORMAT_USER.Replace("0", list2.Count.ToString()));
							if (!string.IsNullOrEmpty(text))
							{
								list.Add(text);
								list2.Add(Constans.SIGN_TEXT_FORMAT_PLACE.Replace("0", list2.Count.ToString()));
							}
							if (!string.IsNullOrEmpty(text2))
							{
								list.Add(text2);
								list2.Add(Constans.SIGN_TEXT_FORMAT_TITLE.Replace("0", list2.Count.ToString()));
							}
							if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.HasValue && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.Value && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason))
							{
								list2.Add(Constans.SIGN_TEXT_FORMAT_REASON.Replace("0", list2.Count.ToString()));
								list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason);
							}
						}
						else
						{
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Contact.Replace("\\", ""));
							list2.Add(Constans.SIGN_TEXT_FORMAT_USER.Replace("0", list2.Count.ToString()));
							list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.strDate);
							list2.Add(Constans.SIGN_TEXT_FORMAT_DATE.Replace("0", list2.Count.ToString()));
							if (!string.IsNullOrEmpty(text))
							{
								list.Add(text);
								list2.Add(Constans.SIGN_TEXT_FORMAT_PLACE.Replace("0", list2.Count.ToString()));
							}
							if (!string.IsNullOrEmpty(text2))
							{
								list.Add(text2);
								list2.Add(Constans.SIGN_TEXT_FORMAT_TITLE.Replace("0", list2.Count.ToString()));
							}
							if (CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.HasValue && CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.IsDisplaySignNote.Value && !string.IsNullOrEmpty(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason))
							{
								list2.Add(Constans.SIGN_TEXT_FORMAT_REASON.Replace("0", list2.Count.ToString()));
								list.Add(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.Reason);
							}
						}
						CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText = string.Join("\r\n", list2);
					}
					CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayText = string.Format(CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText, list.ToArray());
				}
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText), CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayConfig.FormatRectangleText) + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayText), CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayText));
			}
			catch (Exception ex)
			{
				CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayText = Constans.SIGN_TEXT_FORMAT_3_1;
				LogSystem.Warn("Error GetDisplayText: " + ex.Message);
			}
			return CS_0024_003C_003E8__locals80.CS_0024_003C_003E8__locals75.displayText;
		}

		public bool EmptySignatureTable(string inFile, string outFile, string fieldName, DisplayConfig displayConfig, X509Certificate cert)
		{
			PdfReader pdfReader = null;
			FileStream fileStream = null;
			bool result;
			try
			{
				pdfReader = new PdfReader(inFile);
				AcroFields acroFields = pdfReader.AcroFields;
				int num = 1;
				float[] array = new float[displayConfig.MaxPageSign];
				foreach (string signatureName in acroFields.GetSignatureNames())
				{
					IList<AcroFields.FieldPosition> fieldPositions = acroFields.GetFieldPositions(signatureName);
					int page = fieldPositions[0].page;
					if (page > num)
					{
						num = page;
					}
					float height = fieldPositions[0].position.Height;
					array[page] += height;
				}
				Rectangle pageSize = pdfReader.GetPageSize(num);
				float height2 = pageSize.Height;
				float marginRight = displayConfig.MarginRight;
				float num2 = pageSize.Width - displayConfig.MarginRight * 2f;
				fileStream = new FileStream(outFile, FileMode.Create);
				PdfSignatureAppearance signatureAppearance = PdfStamper.CreateSignature(pdfReader, fileStream, '\0', null, true).SignatureAppearance;
				if ("".Equals(displayConfig.Contact))
				{
					displayConfig.Contact = SharedUtils.GetCN(cert);
				}
				signatureAppearance.Contact = displayConfig.Contact;
				signatureAppearance.Reason = displayConfig.Reason;
				signatureAppearance.Location = displayConfig.Location;
				DateTime signDate = displayConfig.SignDate;
				signatureAppearance.SignDate = signDate;
				PdfPTable pdfPTable = new PdfPTable(displayConfig.WidthsPercen.Length);
				pdfPTable.SetWidths(displayConfig.WidthsPercen);
				pdfPTable.WidthPercentage = 100f;
				pdfPTable.TotalWidth = num2;
				for (int i = 0; i < displayConfig.TextArray.Length; i++)
				{
					Paragraph paragraph = new Paragraph(displayConfig.TextArray[i], SignPdfFile.GetFontByConfig(displayConfig));
					paragraph.Alignment = displayConfig.AlignmentArray[i];
					Paragraph paragraph2 = paragraph;
					PdfPCell pdfPCell = new PdfPCell();
					pdfPCell.AddElement(paragraph2);
					pdfPTable.AddCell(pdfPCell);
				}
				float totalHeight = pdfPTable.TotalHeight;
				float num3 = height2 - array[num] - displayConfig.MarginTop - totalHeight - displayConfig.HeightTitle;
				Rectangle rectangle = null;
				IExternalSignatureContainer externalSignatureContainer = null;
				if (num3 < displayConfig.MarginBottom)
				{
					if (num >= displayConfig.TotalPageSign)
					{
						rectangle = new Rectangle(0f, 0f, 0f, 0f);
						signatureAppearance.SetVisibleSignature(rectangle, num, fieldName);
						externalSignatureContainer = new ExternalBlankSignatureContainer(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED);
						MakeSignature.SignExternalContainer(signatureAppearance, externalSignatureContainer, 8192);
						pdfReader.Close();
						return true;
					}
					num++;
					num3 = height2 - displayConfig.MarginTop - totalHeight - displayConfig.HeightTitle;
				}
				rectangle = new Rectangle(marginRight, num3, marginRight + num2, num3 + totalHeight);
				signatureAppearance.SetVisibleSignature(rectangle, num, fieldName);
				PdfTemplate layer = signatureAppearance.GetLayer(0);
				float left = layer.BoundingBox.Left;
				float bottom = layer.BoundingBox.Bottom;
				float width = layer.BoundingBox.Width;
				float height3 = layer.BoundingBox.Height;
				ColumnText columnText = new ColumnText(signatureAppearance.GetLayer(2));
				columnText.SetSimpleColumn(left, bottom, left + width, bottom + height3);
				columnText.AddElement(pdfPTable);
				columnText.Go();
				externalSignatureContainer = new ExternalBlankSignatureContainer(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED);
				MakeSignature.SignExternalContainer(signatureAppearance, externalSignatureContainer, 8192);
				result = true;
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Error emptySignatureTable: " + ex.Message);
				result = false;
			}
			finally
			{
				if (pdfReader != null)
				{
					pdfReader.Close();
				}
				if (fileStream != null)
				{
					try
					{
						fileStream.Close();
					}
					catch (IOException ex2)
					{
						LogSystem.Warn("Error emptySignatureTable: " + ex2.Message);
					}
				}
			}
			return result;
		}

		public bool InsertSignature(string inFile, string outFile, string fieldName, byte[] hash, byte[] extSignature, X509Certificate[] chain, DateTime signDate, DisplayConfig displayConfig, TimestampConfig timestampConfig)
		{
			PdfReader pdfReader = null;
			FileStream fileStream = null;
			bool result;
			try
			{
				pdfReader = new PdfReader(inFile);
				fileStream = new FileStream(outFile, FileMode.Append);
				AcroFields acroFields = pdfReader.AcroFields;
				PdfDictionary signatureDictionary = acroFields.GetSignatureDictionary(fieldName);
				if (signatureDictionary == null)
				{
					LogSystem.Warn("No field");
					return false;
				}
				if (!acroFields.SignatureCoversWholeDocument(fieldName))
				{
					LogSystem.Warn("Not the last signature");
					return false;
				}
				PdfArray asArray = signatureDictionary.GetAsArray(PdfName.BYTERANGE);
				long[] array = asArray.AsLongArray();
				if (asArray.Size != 4 || array[0] != 0)
				{
					LogSystem.Warn("Single exclusion space supported");
					return false;
				}
				IRandomAccessSource randomAccessSource = pdfReader.SafeFile.CreateSourceView();
				string hashAlgorithm = ((displayConfig != null && !string.IsNullOrEmpty(displayConfig.HashAlgorithm)) ? displayConfig.HashAlgorithm : HASH_ALG);
				PdfPKCS7 pdfPKCS = new PdfPKCS7(null, chain, hashAlgorithm, false);
				pdfPKCS.SetExternalDigest(extSignature, null, "RSA");
				TSAClientBouncyCastle tSAClientBouncyCastle = null;
				if (timestampConfig.UseTimestamp)
				{
					tSAClientBouncyCastle = new TSAClientBouncyCastle(timestampConfig.TsaUrl, timestampConfig.TsaAcc, timestampConfig.TsaPass);
				}
				byte[] encodedPKCS = pdfPKCS.GetEncodedPKCS7(hash, signDate, tSAClientBouncyCastle, null, null, CryptoStandard.CMS);
				int num = (int)(array[2] - array[1]) - 2;
				if ((num & 1) != 0)
				{
					LogSystem.Warn("Gap is not a multiple of 2");
					return false;
				}
				num /= 2;
				if (num < encodedPKCS.Length)
				{
					LogSystem.Warn("Not enough space");
					return false;
				}
				StreamUtil.CopyBytes(randomAccessSource, 0L, array[1] + 1, fileStream);
				ByteBuffer byteBuffer = new ByteBuffer(num * 2);
				byte[] array2 = encodedPKCS;
				byte[] array3 = array2;
				foreach (byte b in array3)
				{
					byteBuffer.AppendHex(b);
				}
				int num2 = (num - encodedPKCS.Length) * 2;
				for (int j = 0; j < num2; j++)
				{
					byteBuffer.Append((byte)48);
				}
				byteBuffer.WriteTo(fileStream);
				StreamUtil.CopyBytes(randomAccessSource, array[2] - 1, array[3] + 1, fileStream);
				randomAccessSource.Close();
				((Stream)byteBuffer).Close();
				result = true;
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Error insertSignature: " + ex.Message);
				result = false;
			}
			finally
			{
				if (fileStream != null)
				{
					try
					{
						fileStream.Close();
						fileStream.Dispose();
					}
					catch (IOException ex2)
					{
						LogSystem.Warn("Error insertSignature: " + ex2.Message);
					}
				}
				if (pdfReader != null)
				{
					pdfReader.Close();
				}
			}
			return result;
		}

		public bool InsertSignature(string inFile, Stream outStream, string fieldName, byte[] hash, byte[] extSignature, X509Certificate[] chain, DateTime signDate, DisplayConfig displayConfig, TimestampConfig timestampConfig)
		{
			PdfReader pdfReader = null;
			bool result;
			try
			{
				pdfReader = new PdfReader(inFile);
				AcroFields acroFields = pdfReader.AcroFields;
				PdfDictionary signatureDictionary = acroFields.GetSignatureDictionary(fieldName);
				if (signatureDictionary == null)
				{
					LogSystem.Warn("No field");
					return false;
				}
				if (!acroFields.SignatureCoversWholeDocument(fieldName))
				{
					LogSystem.Warn("Not the last signature");
					return false;
				}
				PdfArray asArray = signatureDictionary.GetAsArray(PdfName.BYTERANGE);
				long[] array = asArray.AsLongArray();
				if (asArray.Size != 4 || array[0] != 0)
				{
					LogSystem.Warn("Single exclusion space supported");
					return false;
				}
				IRandomAccessSource randomAccessSource = pdfReader.SafeFile.CreateSourceView();
				string hashAlgorithm = ((displayConfig != null && !string.IsNullOrEmpty(displayConfig.HashAlgorithm)) ? displayConfig.HashAlgorithm : HASH_ALG);
				PdfPKCS7 pdfPKCS = new PdfPKCS7(null, chain, hashAlgorithm, false);
				pdfPKCS.SetExternalDigest(extSignature, null, "RSA");
				TSAClientBouncyCastle tSAClientBouncyCastle = null;
				if (timestampConfig.UseTimestamp)
				{
					tSAClientBouncyCastle = new TSAClientBouncyCastle(timestampConfig.TsaUrl, timestampConfig.TsaAcc, timestampConfig.TsaPass);
				}
				byte[] encodedPKCS = pdfPKCS.GetEncodedPKCS7(hash, signDate, tSAClientBouncyCastle, null, null, CryptoStandard.CMS);
				int num = (int)(array[2] - array[1]) - 2;
				if ((num & 1) != 0)
				{
					LogSystem.Warn("Gap is not a multiple of 2");
					return false;
				}
				num /= 2;
				if (num < encodedPKCS.Length)
				{
					LogSystem.Warn("Not enough space");
					return false;
				}
				StreamUtil.CopyBytes(randomAccessSource, 0L, array[1] + 1, outStream);
				ByteBuffer byteBuffer = new ByteBuffer(num * 2);
				byte[] array2 = encodedPKCS;
				byte[] array3 = array2;
				foreach (byte b in array3)
				{
					byteBuffer.AppendHex(b);
				}
				int num2 = (num - encodedPKCS.Length) * 2;
				for (int j = 0; j < num2; j++)
				{
					byteBuffer.Append((byte)48);
				}
				byteBuffer.WriteTo(outStream);
				StreamUtil.CopyBytes(randomAccessSource, array[2] - 1, array[3] + 1, outStream);
				randomAccessSource.Close();
				((Stream)byteBuffer).Close();
				result = true;
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Error insertSignature: " + ex.Message);
				result = false;
			}
			finally
			{
				if (pdfReader != null)
				{
					pdfReader.Close();
				}
			}
			return result;
		}

		public List<byte[]> PreSign(string inFile, string fieldName, X509Certificate[] chain, DisplayConfig displayConfig)
		{
			PdfReader pdfReader = null;
			List<byte[]> result;
			try
			{
				List<byte[]> list = new List<byte[]>();
				pdfReader = new PdfReader(inFile);
				PdfDictionary signatureDictionary = pdfReader.AcroFields.GetSignatureDictionary(fieldName);
				if (signatureDictionary == null)
				{
					Console.WriteLine("No field");
					return null;
				}
				PdfArray asArray = signatureDictionary.GetAsArray(PdfName.BYTERANGE);
				long[] array = asArray.AsLongArray();
				if (asArray.Size != 4 || array[0] != 0)
				{
					Console.WriteLine("Single exclusion space supported");
					return null;
				}
				IRandomAccessSource source = pdfReader.SafeFile.CreateSourceView();
				PdfPKCS7 pdfPKCS = new PdfPKCS7(null, chain, (!string.IsNullOrEmpty(displayConfig.HashAlgorithm)) ? displayConfig.HashAlgorithm : HASH_ALG, false);
				byte[] array2 = DigestAlgorithms.Digest(new RASInputStream(new RandomAccessSourceFactory().CreateRanged(source, array)), DigestUtilities.GetDigest((!string.IsNullOrEmpty(displayConfig.HashAlgorithm)) ? displayConfig.HashAlgorithm : HASH_ALG));
				byte[] authenticatedAttributeBytes = pdfPKCS.getAuthenticatedAttributeBytes(array2, displayConfig.SignDate, null, null, CryptoStandard.CMS);
				list.Add(authenticatedAttributeBytes);
				list.Add(array2);
				result = list;
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Error create hash: " + ex.Message);
				result = null;
			}
			finally
			{
				try
				{
					if (pdfReader != null)
					{
						pdfReader.Close();
					}
				}
				catch (Exception ex2)
				{
					LogSystem.Warn("Error create hash: " + ex2.Message);
				}
			}
			return result;
		}
	}
}
