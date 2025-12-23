using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using GemBox.Pdf;
using GemBox.Pdf.Content;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.License;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Inventec.Common.SignLibrary
{
	public class PdfDocumentProcess
	{
		internal static bool ReplaceTextWithGemBox(List<string> replaceKeys, string sourceFile)
		{
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Expected O, but got Unknown
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0175: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ad: Expected O, but got Unknown
			bool result = false;
			try
			{
				if (replaceKeys != null && replaceKeys.Count > 0 && File.Exists(sourceFile))
				{
					PdfDocument val = PdfDocument.Load(sourceFile);
					try
					{
						int num = 1;
						foreach (PdfPage page in val.Pages)
						{
							foreach (PdfTextContent item2 in (from element in page.Content.Elements.All()
								where (int)element.ElementType == 0
								select element).Cast<PdfTextContent>())
							{
								string item = ((object)item2).ToString();
								PdfFont font = ((PdfVisualContentElement)item2).Format.Text.Font;
								PdfColor color = ((PdfVisualContentElement)item2).Format.Fill.Color;
								PdfPoint location = item2.Location;
								if (replaceKeys.Contains(item))
								{
									string text = ((PdfPoint)(ref location)).X + ":" + ((PdfPoint)(ref location)).Y;
								}
							}
							num++;
						}
					}
					finally
					{
						if (val != null)
						{
							((IDisposable)val).Dispose();
						}
					}
					Document val2 = new Document(sourceFile);
					foreach (string replaceKey in replaceKeys)
					{
						TextFragmentAbsorber val3 = new TextFragmentAbsorber(replaceKey);
						val2.Pages.Accept(val3);
						TextFragmentCollection textFragments = val3.TextFragments;
						foreach (TextFragment item3 in textFragments)
						{
							TextFragment val4 = item3;
							val4.Text = "";
						}
					}
					val2.Save(sourceFile);
					result = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		internal static bool ReplaceText(List<string> replaceKeys, string sourceFile)
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			bool result = false;
			try
			{
				if (replaceKeys != null && replaceKeys.Count > 0 && File.Exists(sourceFile))
				{
					LicenceProcess.SetLicenseForAspose();
					Document val = new Document(sourceFile);
					foreach (string replaceKey in replaceKeys)
					{
						TextFragmentAbsorber val2 = new TextFragmentAbsorber(replaceKey);
						val.Pages.Accept(val2);
						TextFragmentCollection textFragments = val2.TextFragments;
						foreach (TextFragment item in textFragments)
						{
							TextFragment val3 = item;
							val3.Text = "";
						}
					}
					val.Save(sourceFile);
					result = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		internal static List<SignPositionADO> GetPositionBySearchKey(string sourceFile, string keySearch)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Expected O, but got Unknown
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Expected O, but got Unknown
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Expected O, but got Unknown
			List<SignPositionADO> list = new List<SignPositionADO>();
			try
			{
				Document val = new Document(sourceFile);
				TextFragmentAbsorber val2 = new TextFragmentAbsorber(keySearch);
				val.Pages.Accept(val2);
				TextFragmentCollection textFragments = val2.TextFragments;
				foreach (TextFragment item in textFragments)
				{
					TextFragment val3 = item;
					Rectangle reactanle = new Rectangle((float)val3.Rectangle.LLX, (float)val3.Rectangle.LLY, (float)val3.Rectangle.URX, (float)val3.Rectangle.URY);
					list.Add(new SignPositionADO
					{
						PageNUm = val3.Page.Number,
						Text = val3.Text,
						Reactanle = reactanle
					});
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return list;
		}

		internal static List<SignPositionADO> GetPositionBySearchKey(Stream sourceStream, string keySearch)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Expected O, but got Unknown
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Expected O, but got Unknown
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Expected O, but got Unknown
			List<SignPositionADO> list = new List<SignPositionADO>();
			try
			{
				Document val = new Document(sourceStream);
				TextFragmentAbsorber val2 = new TextFragmentAbsorber(keySearch);
				val.Pages.Accept(val2);
				TextFragmentCollection textFragments = val2.TextFragments;
				foreach (TextFragment item in textFragments)
				{
					TextFragment val3 = item;
					Rectangle reactanle = new Rectangle((float)val3.Rectangle.LLX, (float)val3.Rectangle.LLY, (float)val3.Rectangle.URX, (float)val3.Rectangle.URY);
					list.Add(new SignPositionADO
					{
						PageNUm = val3.Page.Number,
						Text = val3.Text,
						Reactanle = reactanle
					});
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return list;
		}

		internal static void InsertPages(Stream sourceFile, List<Stream> streamListJoin, string desFileJoined)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Expected O, but got Unknown
			List<int> list = new List<int>();
			Stream stream = File.Open(desFileJoined, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			PdfConcatenate val = new PdfConcatenate(stream);
			PdfReader val2 = null;
			if (sourceFile != null && sourceFile.Length > 0)
			{
				val2 = new PdfReader(sourceFile);
				for (int i = 0; i <= val2.NumberOfPages; i++)
				{
					list.Add(i);
				}
				val2.SelectPages((ICollection<int>)list);
				val.AddPages(val2);
			}
			if (streamListJoin != null && streamListJoin.Count > 0)
			{
				foreach (Stream item in streamListJoin)
				{
					PdfReader val3 = null;
					val3 = new PdfReader(item);
					list = new List<int>();
					for (int j = 0; j <= val3.NumberOfPages; j++)
					{
						list.Add(j);
					}
					val3.SelectPages((ICollection<int>)list);
					val.AddPages(val3);
					val3.Close();
				}
			}
			try
			{
				if (val2 != null)
				{
					val2.Close();
				}
			}
			catch
			{
			}
			try
			{
				sourceFile.Close();
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
		}

		internal static void InsertPage(Stream sourceFile, List<string> fileListJoin, string desFileJoined)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Expected O, but got Unknown
			//IL_0245: Unknown result type (might be due to invalid IL or missing references)
			//IL_024c: Expected O, but got Unknown
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Expected O, but got Unknown
			//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b0: Expected O, but got Unknown
			List<string> list = new List<string>();
			if (fileListJoin == null || fileListJoin.Count <= 0)
			{
				return;
			}
			PdfReader val = new PdfReader(sourceFile);
			int numberOfPages = val.NumberOfPages;
			Rectangle pageSizeWithRotation = val.GetPageSizeWithRotation(val.NumberOfPages);
			Rectangle val2 = new Rectangle(pageSizeWithRotation.Left, pageSizeWithRotation.Bottom, pageSizeWithRotation.Right, pageSizeWithRotation.Bottom + pageSizeWithRotation.Height, pageSizeWithRotation.Rotation);
			foreach (string item in fileListJoin)
			{
				int num = item.LastIndexOf(".");
				string text = item.Substring((num > 0) ? (num + 1) : num);
				if (text != "pdf")
				{
					MemoryStream file = FssFileDownload.GetFile(item);
					file.Position = 0L;
					string text2 = Utils.GenerateTempFileWithin();
					Stream stream = new FileStream(text2, FileMode.Create, FileAccess.Write);
					Document val3 = new Document(val2, 0f, 0f, 0f, 0f);
					PdfWriter instance = PdfWriter.GetInstance(val3, stream);
					val3.Open();
					((DocWriter)instance).Open();
					Image instance2 = Image.GetInstance((Stream)file);
					if (((Rectangle)instance2).Height > ((Rectangle)instance2).Width)
					{
						float num2 = 0f;
						num2 = pageSizeWithRotation.Height / ((Rectangle)instance2).Height;
						instance2.ScalePercent(num2 * 100f);
					}
					else
					{
						float num3 = 0f;
						num3 = pageSizeWithRotation.Width / ((Rectangle)instance2).Width;
						instance2.ScalePercent(num3 * 100f);
					}
					val3.Add((IElement)(object)instance2);
					val3.Close();
					((DocWriter)instance).Close();
					list.Add(text2);
				}
				else
				{
					MemoryStream file2 = FssFileDownload.GetFile(item);
					if (file2 != null && file2.Length > 0)
					{
						file2.Position = 0L;
						string text3 = Utils.GenerateTempFileWithin();
						Utils.ByteToFile(Utils.StreamToByte(file2), text3);
						list.Add(text3);
					}
					else
					{
						LogSystem.Error("Loi convert va luu tam file pdf tu server fss ve may tram____item=" + item);
					}
				}
			}
			Stream stream2 = File.Open(desFileJoined, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			PdfConcatenate val4 = new PdfConcatenate(stream2);
			List<int> list2 = new List<int>();
			for (int i = 0; i <= val.NumberOfPages; i++)
			{
				list2.Add(i);
			}
			val.SelectPages((ICollection<int>)list2);
			val4.AddPages(val);
			foreach (string item2 in list)
			{
				PdfReader val5 = null;
				val5 = new PdfReader(item2);
				list2 = new List<int>();
				for (int j = 0; j <= val5.NumberOfPages; j++)
				{
					list2.Add(j);
				}
				val5.SelectPages((ICollection<int>)list2);
				val4.AddPages(val5);
				val5.Close();
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
				sourceFile.Close();
			}
			catch
			{
			}
			try
			{
				val4.Close();
			}
			catch
			{
			}
			foreach (string item3 in list)
			{
				try
				{
					File.Delete(item3);
				}
				catch
				{
				}
			}
		}

		public static void InsertPageExt(List<string> fileListJoin, string desFileJoined)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			List<string> list = new List<string>();
			if (fileListJoin == null || fileListJoin.Count <= 0)
			{
				return;
			}
			List<int> list2 = new List<int>();
			Stream stream = File.Open(desFileJoined, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			PdfConcatenate val = new PdfConcatenate(stream);
			foreach (string item in fileListJoin)
			{
				PdfReader val2 = null;
				val2 = new PdfReader(item);
				list2 = new List<int>();
				for (int i = 0; i <= val2.NumberOfPages; i++)
				{
					list2.Add(i);
				}
				val2.SelectPages((ICollection<int>)list2);
				val.AddPages(val2);
				val2.Close();
			}
			try
			{
				val.Close();
			}
			catch
			{
			}
			foreach (string item2 in list)
			{
				try
				{
					File.Delete(item2);
				}
				catch
				{
				}
			}
		}

		public static void InsertPageExt(List<MemoryStream> streamListJoin, string desFileJoined)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			List<string> list = new List<string>();
			if (streamListJoin == null || streamListJoin.Count <= 0)
			{
				return;
			}
			List<int> list2 = new List<int>();
			Stream stream = File.Open(desFileJoined, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			PdfConcatenate val = new PdfConcatenate(stream);
			foreach (MemoryStream item in streamListJoin)
			{
				PdfReader val2 = null;
				val2 = new PdfReader((Stream)item);
				list2 = new List<int>();
				for (int i = 0; i <= val2.NumberOfPages; i++)
				{
					list2.Add(i);
				}
				val2.SelectPages((ICollection<int>)list2);
				val.AddPages(val2);
				val2.Close();
			}
			try
			{
				val.Close();
			}
			catch
			{
			}
			foreach (string item2 in list)
			{
				try
				{
					File.Delete(item2);
				}
				catch
				{
				}
			}
		}

		internal static List<SignPositionADO> GetPositionWithAutoAddAnnotationBySearchKey(string sourceFile, string outFile, string keySearch)
		{
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			List<SignPositionADO> list = new List<SignPositionADO>();
			try
			{
				PDFParser pDFParser = new PDFParser();
				List<string> list2 = pDFParser.ReadPdfFile(sourceFile, keySearch);
				if (list2 != null && list2.Count > 0)
				{
					foreach (string item in list2)
					{
						Document val = new Document(sourceFile);
						TextFragmentAbsorber val2 = new TextFragmentAbsorber(item);
						val.Pages.Accept(val2);
						TextFragmentCollection textFragments = val2.TextFragments;
						int num = 1;
						foreach (TextFragment item2 in textFragments)
						{
							TextFragment val3 = item2;
							Rectangle reactanle = new Rectangle((float)val3.Position.XIndent, (float)val3.Position.YIndent, (float)val3.Position.XIndent + 2f, (float)val3.Position.YIndent + 2f);
							num = val3.Page.Number;
							list.Add(new SignPositionADO
							{
								PageNUm = num,
								Text = val3.Text,
								Reactanle = reactanle
							});
							string[] array = val3.Text.Split(new string[1] { keySearch }, StringSplitOptions.RemoveEmptyEntries);
							string text = "";
							if (array.Length == 1)
							{
								text = array[0];
							}
							else if (array.Length > 1)
							{
								text = array[array.Length - 1];
							}
							text = text.Replace(">", "").Replace("}", "");
							Utils.AddTextAnnotation(outFile, text, num, val3.Position.XIndent, val3.Position.YIndent, 2, 2);
							num++;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return list;
		}

		internal static PageSettings GetPaperSize(string filePath)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Expected O, but got Unknown
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			PageSettings pageSettings = new PageSettings();
			try
			{
				Document val = new Document(filePath);
				pageSettings.PaperSize = new PaperSize();
				pageSettings.PaperSize.Width = (int)Math.Round(val.PageInfo.Width * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				pageSettings.PaperSize.Height = (int)Math.Round(val.PageInfo.Height * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				pageSettings.PaperSize.RawKind = 0;
				PdfPageEditor val2 = new PdfPageEditor();
				((Facade)val2).BindPdf(filePath);
				if (val2.GetPageSize(1).IsLandscape)
				{
					pageSettings.Landscape = val2.GetPageSize(1).IsLandscape;
				}
				LogSystem.Debug(LogUtil.TraceData("pSettings.Landscape", (object)pageSettings.Landscape) + LogUtil.TraceData("pdfDocument.PageInfo.Width", (object)val.PageInfo.Width) + LogUtil.TraceData("pdfDocument.PageInfo.Height", (object)val.PageInfo.Height) + LogUtil.TraceData("pSettings.PaperSize.Width", (object)pageSettings.PaperSize.Width) + LogUtil.TraceData("pSettings.PaperSize.Height", (object)pageSettings.PaperSize.Height));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return pageSettings;
		}

		public static void SplitOnePageToImageAndJoinToNewOnePdf(string sourceTempFilePath, float oginalHeight, ref string joinPdfFilePath, List<ImageOfPageDTO> imageFiles = null)
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Expected O, but got Unknown
			try
			{
				if (imageFiles == null || imageFiles.Count == 0)
				{
					imageFiles = ConvertPdfPageToListImage(sourceTempFilePath);
				}
				PdfReader val = new PdfReader(sourceTempFilePath);
				int numberOfPages = val.NumberOfPages;
				Rectangle pageSizeWithRotation = val.GetPageSizeWithRotation(val.NumberOfPages);
				Rectangle val2 = new Rectangle(pageSizeWithRotation.Left, pageSizeWithRotation.Bottom, pageSizeWithRotation.Right, pageSizeWithRotation.Bottom + oginalHeight, pageSizeWithRotation.Rotation);
				val2.BorderColor = pageSizeWithRotation.BorderColor;
				val2.BackgroundColor = pageSizeWithRotation.BackgroundColor;
				val2.Rotation = pageSizeWithRotation.Rotation;
				val2.Border = pageSizeWithRotation.Border;
				val2.BorderWidth = pageSizeWithRotation.BorderWidth;
				val2.BorderColor = pageSizeWithRotation.BorderColor;
				val2.BackgroundColor = pageSizeWithRotation.BackgroundColor;
				val2.BorderColorLeft = pageSizeWithRotation.BorderColorLeft;
				val2.BorderColorRight = pageSizeWithRotation.BorderColorRight;
				val2.BorderColorTop = pageSizeWithRotation.BorderColorTop;
				val2.BorderColorBottom = pageSizeWithRotation.BorderColorBottom;
				val2.BorderWidthLeft = pageSizeWithRotation.BorderWidthLeft;
				val2.BorderWidthRight = pageSizeWithRotation.BorderWidthRight;
				val2.BorderWidthTop = pageSizeWithRotation.BorderWidthTop;
				val2.BorderWidthBottom = pageSizeWithRotation.BorderWidthBottom;
				val2.UseVariableBorders = pageSizeWithRotation.UseVariableBorders;
				if (imageFiles != null && imageFiles.Count > 0)
				{
					joinPdfFilePath = Utils.GenerateTempFileWithin();
					PdfReader tempReader = Utils.GetTempReader(val2);
					using (FileStream fileStream = File.Open(joinPdfFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
					{
						PdfStamper val3 = new PdfStamper(tempReader, (Stream)fileStream);
						try
						{
							int num = 1;
							float num2 = 0f;
							foreach (ImageOfPageDTO imageFile in imageFiles)
							{
								float num3 = 0f;
								float num4 = 0f;
								if (num2 + imageFile.Height > oginalHeight)
								{
									num++;
									val3.InsertPage(num, val2);
									num2 = imageFile.Height;
									num3 = 0f;
									num4 = oginalHeight - num2;
								}
								else
								{
									num2 += imageFile.Height;
									num3 = 0f;
									num4 = oginalHeight - num2;
								}
								float num5 = 0f;
								Image val4 = ((!string.IsNullOrEmpty(imageFile.Path)) ? Image.GetInstance(imageFile.Path) : Image.GetInstance(imageFile.ImageContent));
								val4.SetAbsolutePosition(num3, num4);
								float val5 = pageSizeWithRotation.Width / ((Rectangle)val4).Width;
								float val6 = oginalHeight / ((Rectangle)val4).Height;
								float num6 = Math.Min(val5, val6);
								val4.ScalePercent(num6 * 100f);
								float num7 = ((Rectangle)val4).Width * num6;
								num3 = (pageSizeWithRotation.Width - num7) / 2f;
								val4.SetAbsolutePosition(num3, num4);
								val3.GetOverContent(num).AddImage(val4);
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
				val.Close();
				try
				{
					if (imageFiles == null || imageFiles.Count <= 0)
					{
						return;
					}
					int count = imageFiles.Count;
					for (int num8 = count - 1; num8 >= 0; num8--)
					{
						try
						{
							if (!string.IsNullOrEmpty(imageFiles[num8].Path))
							{
								File.Delete(imageFiles[num8].Path);
							}
						}
						catch
						{
						}
					}
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

		public static List<ImageOfPageDTO> ConvertPdfPageToListImage(string output_file)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			List<ImageOfPageDTO> list = new List<ImageOfPageDTO>();
			try
			{
				LicenceProcess.SetLicenseForAspose();
				Document val = new Document(output_file);
				for (int i = 1; i <= val.Pages.Count; i++)
				{
					string filename = string.Format("splitimage{0:d}{1}.jpg", i, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
					string fullPathFile = Utils.GetFullPathFile(filename);
					using (FileStream fileStream = new FileStream(fullPathFile, FileMode.Create))
					{
						Resolution val2 = new Resolution(300);
						JpegDevice val3 = new JpegDevice(val2, 100);
						((PageDevice)val3).Process(val.Pages[i], (Stream)fileStream);
						fileStream.Close();
						list.Add(new ImageOfPageDTO
						{
							Path = fullPathFile,
							PageNumber = val.Pages[i].Number,
							Width = (float)val.Pages[i].Rect.Width,
							Height = (float)val.Pages[i].Rect.Height
						});
					}
				}
			}
			catch (Exception ex)
			{
				list = new List<ImageOfPageDTO>();
				LogSystem.Warn(ex);
			}
			return list;
		}
	}
}
