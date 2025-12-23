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
			bool result = false;
			try
			{
				if (replaceKeys != null && replaceKeys.Count > 0 && File.Exists(sourceFile))
				{
					using (GemBox.Pdf.PdfDocument pdfDocument = GemBox.Pdf.PdfDocument.Load(sourceFile))
					{
						int num = 1;
						foreach (GemBox.Pdf.PdfPage page in pdfDocument.Pages)
						{
							foreach (PdfTextContent item2 in (from element in page.Content.Elements.All()
								where element.ElementType == PdfContentElementType.Text
								select element).Cast<PdfTextContent>())
							{
								string item = item2.ToString();
								GemBox.Pdf.Content.PdfFont font = item2.Format.Text.Font;
								PdfColor color = item2.Format.Fill.Color;
								PdfPoint location = item2.Location;
								if (replaceKeys.Contains(item))
								{
									string text = location.X + ":" + location.Y;
								}
							}
							num++;
						}
					}
					Aspose.Pdf.Document document = new Aspose.Pdf.Document(sourceFile);
					foreach (string replaceKey in replaceKeys)
					{
						TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(replaceKey);
						document.Pages.Accept(textFragmentAbsorber);
						TextFragmentCollection textFragments = textFragmentAbsorber.TextFragments;
						foreach (TextFragment item3 in textFragments)
						{
							item3.Text = "";
						}
					}
					document.Save(sourceFile);
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
			bool result = false;
			try
			{
				if (replaceKeys != null && replaceKeys.Count > 0 && File.Exists(sourceFile))
				{
					LicenceProcess.SetLicenseForAspose();
					Aspose.Pdf.Document document = new Aspose.Pdf.Document(sourceFile);
					foreach (string replaceKey in replaceKeys)
					{
						TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(replaceKey);
						document.Pages.Accept(textFragmentAbsorber);
						TextFragmentCollection textFragments = textFragmentAbsorber.TextFragments;
						foreach (TextFragment item in textFragments)
						{
							item.Text = "";
						}
					}
					document.Save(sourceFile);
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
			List<SignPositionADO> list = new List<SignPositionADO>();
			try
			{
				Aspose.Pdf.Document document = new Aspose.Pdf.Document(sourceFile);
				TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(keySearch);
				document.Pages.Accept(textFragmentAbsorber);
				TextFragmentCollection textFragments = textFragmentAbsorber.TextFragments;
				foreach (TextFragment item in textFragments)
				{
					iTextSharp.text.Rectangle reactanle = new iTextSharp.text.Rectangle((float)item.Rectangle.LLX, (float)item.Rectangle.LLY, (float)item.Rectangle.URX, (float)item.Rectangle.URY);
					list.Add(new SignPositionADO
					{
						PageNUm = item.Page.Number,
						Text = item.Text,
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
			List<SignPositionADO> list = new List<SignPositionADO>();
			try
			{
				Aspose.Pdf.Document document = new Aspose.Pdf.Document(sourceStream);
				TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(keySearch);
				document.Pages.Accept(textFragmentAbsorber);
				TextFragmentCollection textFragments = textFragmentAbsorber.TextFragments;
				foreach (TextFragment item in textFragments)
				{
					iTextSharp.text.Rectangle reactanle = new iTextSharp.text.Rectangle((float)item.Rectangle.LLX, (float)item.Rectangle.LLY, (float)item.Rectangle.URX, (float)item.Rectangle.URY);
					list.Add(new SignPositionADO
					{
						PageNUm = item.Page.Number,
						Text = item.Text,
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
			List<int> list = new List<int>();
			Stream os = File.Open(desFileJoined, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			PdfConcatenate pdfConcatenate = new PdfConcatenate(os);
			PdfReader pdfReader = null;
			if (sourceFile != null && sourceFile.Length > 0)
			{
				pdfReader = new PdfReader(sourceFile);
				for (int i = 0; i <= pdfReader.NumberOfPages; i++)
				{
					list.Add(i);
				}
				pdfReader.SelectPages(list);
				pdfConcatenate.AddPages(pdfReader);
			}
			if (streamListJoin != null && streamListJoin.Count > 0)
			{
				foreach (Stream item in streamListJoin)
				{
					PdfReader pdfReader2 = null;
					pdfReader2 = new PdfReader(item);
					list = new List<int>();
					for (int j = 0; j <= pdfReader2.NumberOfPages; j++)
					{
						list.Add(j);
					}
					pdfReader2.SelectPages(list);
					pdfConcatenate.AddPages(pdfReader2);
					pdfReader2.Close();
				}
			}
			try
			{
				if (pdfReader != null)
				{
					pdfReader.Close();
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
				pdfConcatenate.Close();
			}
			catch
			{
			}
		}

		internal static void InsertPage(Stream sourceFile, List<string> fileListJoin, string desFileJoined)
		{
			List<string> list = new List<string>();
			if (fileListJoin == null || fileListJoin.Count <= 0)
			{
				return;
			}
			PdfReader pdfReader = new PdfReader(sourceFile);
			int numberOfPages = pdfReader.NumberOfPages;
			iTextSharp.text.Rectangle pageSizeWithRotation = pdfReader.GetPageSizeWithRotation(pdfReader.NumberOfPages);
			iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(pageSizeWithRotation.Left, pageSizeWithRotation.Bottom, pageSizeWithRotation.Right, pageSizeWithRotation.Bottom + pageSizeWithRotation.Height, pageSizeWithRotation.Rotation);
			foreach (string item in fileListJoin)
			{
				int num = item.LastIndexOf(".");
				string text = item.Substring((num > 0) ? (num + 1) : num);
				if (text != "pdf")
				{
					MemoryStream file = FssFileDownload.GetFile(item);
					file.Position = 0L;
					string text2 = Utils.GenerateTempFileWithin();
					Stream os = new FileStream(text2, FileMode.Create, FileAccess.Write);
					iTextSharp.text.Document document = new iTextSharp.text.Document(pageSize, 0f, 0f, 0f, 0f);
					PdfWriter instance = PdfWriter.GetInstance(document, os);
					document.Open();
					instance.Open();
					iTextSharp.text.Image instance2 = iTextSharp.text.Image.GetInstance(file);
					if (instance2.Height > instance2.Width)
					{
						float num2 = 0f;
						num2 = pageSizeWithRotation.Height / instance2.Height;
						instance2.ScalePercent(num2 * 100f);
					}
					else
					{
						float num3 = 0f;
						num3 = pageSizeWithRotation.Width / instance2.Width;
						instance2.ScalePercent(num3 * 100f);
					}
					document.Add(instance2);
					document.Close();
					instance.Close();
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
			Stream os2 = File.Open(desFileJoined, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			PdfConcatenate pdfConcatenate = new PdfConcatenate(os2);
			List<int> list2 = new List<int>();
			for (int i = 0; i <= pdfReader.NumberOfPages; i++)
			{
				list2.Add(i);
			}
			pdfReader.SelectPages(list2);
			pdfConcatenate.AddPages(pdfReader);
			foreach (string item2 in list)
			{
				PdfReader pdfReader2 = null;
				pdfReader2 = new PdfReader(item2);
				list2 = new List<int>();
				for (int j = 0; j <= pdfReader2.NumberOfPages; j++)
				{
					list2.Add(j);
				}
				pdfReader2.SelectPages(list2);
				pdfConcatenate.AddPages(pdfReader2);
				pdfReader2.Close();
			}
			try
			{
				pdfReader.Close();
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
				pdfConcatenate.Close();
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
			List<string> list = new List<string>();
			if (fileListJoin == null || fileListJoin.Count <= 0)
			{
				return;
			}
			List<int> list2 = new List<int>();
			Stream os = File.Open(desFileJoined, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			PdfConcatenate pdfConcatenate = new PdfConcatenate(os);
			foreach (string item in fileListJoin)
			{
				PdfReader pdfReader = null;
				pdfReader = new PdfReader(item);
				list2 = new List<int>();
				for (int i = 0; i <= pdfReader.NumberOfPages; i++)
				{
					list2.Add(i);
				}
				pdfReader.SelectPages(list2);
				pdfConcatenate.AddPages(pdfReader);
				pdfReader.Close();
			}
			try
			{
				pdfConcatenate.Close();
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
			List<string> list = new List<string>();
			if (streamListJoin == null || streamListJoin.Count <= 0)
			{
				return;
			}
			List<int> list2 = new List<int>();
			Stream os = File.Open(desFileJoined, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			PdfConcatenate pdfConcatenate = new PdfConcatenate(os);
			foreach (MemoryStream item in streamListJoin)
			{
				PdfReader pdfReader = null;
				pdfReader = new PdfReader(item);
				list2 = new List<int>();
				for (int i = 0; i <= pdfReader.NumberOfPages; i++)
				{
					list2.Add(i);
				}
				pdfReader.SelectPages(list2);
				pdfConcatenate.AddPages(pdfReader);
				pdfReader.Close();
			}
			try
			{
				pdfConcatenate.Close();
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
			List<SignPositionADO> list = new List<SignPositionADO>();
			try
			{
				PDFParser pDFParser = new PDFParser();
				List<string> list2 = pDFParser.ReadPdfFile(sourceFile, keySearch);
				if (list2 != null && list2.Count > 0)
				{
					foreach (string item in list2)
					{
						Aspose.Pdf.Document document = new Aspose.Pdf.Document(sourceFile);
						TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(item);
						document.Pages.Accept(textFragmentAbsorber);
						TextFragmentCollection textFragments = textFragmentAbsorber.TextFragments;
						int num = 1;
						foreach (TextFragment item2 in textFragments)
						{
							iTextSharp.text.Rectangle reactanle = new iTextSharp.text.Rectangle((float)item2.Position.XIndent, (float)item2.Position.YIndent, (float)item2.Position.XIndent + 2f, (float)item2.Position.YIndent + 2f);
							num = item2.Page.Number;
							list.Add(new SignPositionADO
							{
								PageNUm = num,
								Text = item2.Text,
								Reactanle = reactanle
							});
							string[] array = item2.Text.Split(new string[1] { keySearch }, StringSplitOptions.RemoveEmptyEntries);
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
							Utils.AddTextAnnotation(outFile, text, num, item2.Position.XIndent, item2.Position.YIndent, 2, 2);
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
			PageSettings pageSettings = new PageSettings();
			try
			{
				Aspose.Pdf.Document document = new Aspose.Pdf.Document(filePath);
				pageSettings.PaperSize = new PaperSize();
				pageSettings.PaperSize.Width = (int)Math.Round(document.PageInfo.Width * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				pageSettings.PaperSize.Height = (int)Math.Round(document.PageInfo.Height * 100.0 / 72.0, 0, MidpointRounding.AwayFromZero);
				pageSettings.PaperSize.RawKind = 0;
				PdfPageEditor pdfPageEditor = new PdfPageEditor();
				pdfPageEditor.BindPdf(filePath);
				if (pdfPageEditor.GetPageSize(1).IsLandscape)
				{
					pageSettings.Landscape = pdfPageEditor.GetPageSize(1).IsLandscape;
				}
				LogSystem.Debug(LogUtil.TraceData("pSettings.Landscape", pageSettings.Landscape) + LogUtil.TraceData("pdfDocument.PageInfo.Width", document.PageInfo.Width) + LogUtil.TraceData("pdfDocument.PageInfo.Height", document.PageInfo.Height) + LogUtil.TraceData("pSettings.PaperSize.Width", pageSettings.PaperSize.Width) + LogUtil.TraceData("pSettings.PaperSize.Height", pageSettings.PaperSize.Height));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return pageSettings;
		}

		public static void SplitOnePageToImageAndJoinToNewOnePdf(string sourceTempFilePath, float oginalHeight, ref string joinPdfFilePath, List<ImageOfPageDTO> imageFiles = null)
		{
			try
			{
				if (imageFiles == null || imageFiles.Count == 0)
				{
					imageFiles = ConvertPdfPageToListImage(sourceTempFilePath);
				}
				PdfReader pdfReader = new PdfReader(sourceTempFilePath);
				int numberOfPages = pdfReader.NumberOfPages;
				iTextSharp.text.Rectangle pageSizeWithRotation = pdfReader.GetPageSizeWithRotation(pdfReader.NumberOfPages);
				iTextSharp.text.Rectangle rectangle = new iTextSharp.text.Rectangle(pageSizeWithRotation.Left, pageSizeWithRotation.Bottom, pageSizeWithRotation.Right, pageSizeWithRotation.Bottom + oginalHeight, pageSizeWithRotation.Rotation);
				rectangle.BorderColor = pageSizeWithRotation.BorderColor;
				rectangle.BackgroundColor = pageSizeWithRotation.BackgroundColor;
				rectangle.Rotation = pageSizeWithRotation.Rotation;
				rectangle.Border = pageSizeWithRotation.Border;
				rectangle.BorderWidth = pageSizeWithRotation.BorderWidth;
				rectangle.BorderColor = pageSizeWithRotation.BorderColor;
				rectangle.BackgroundColor = pageSizeWithRotation.BackgroundColor;
				rectangle.BorderColorLeft = pageSizeWithRotation.BorderColorLeft;
				rectangle.BorderColorRight = pageSizeWithRotation.BorderColorRight;
				rectangle.BorderColorTop = pageSizeWithRotation.BorderColorTop;
				rectangle.BorderColorBottom = pageSizeWithRotation.BorderColorBottom;
				rectangle.BorderWidthLeft = pageSizeWithRotation.BorderWidthLeft;
				rectangle.BorderWidthRight = pageSizeWithRotation.BorderWidthRight;
				rectangle.BorderWidthTop = pageSizeWithRotation.BorderWidthTop;
				rectangle.BorderWidthBottom = pageSizeWithRotation.BorderWidthBottom;
				rectangle.UseVariableBorders = pageSizeWithRotation.UseVariableBorders;
				if (imageFiles != null && imageFiles.Count > 0)
				{
					joinPdfFilePath = Utils.GenerateTempFileWithin();
					PdfReader tempReader = Utils.GetTempReader(rectangle);
					using (FileStream os = File.Open(joinPdfFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
					{
						using (PdfStamper pdfStamper = new PdfStamper(tempReader, os))
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
									pdfStamper.InsertPage(num, rectangle);
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
								iTextSharp.text.Image image = ((!string.IsNullOrEmpty(imageFile.Path)) ? iTextSharp.text.Image.GetInstance(imageFile.Path) : iTextSharp.text.Image.GetInstance(imageFile.ImageContent));
								image.SetAbsolutePosition(num3, num4);
								num5 = pageSizeWithRotation.Width / image.Width;
								image.ScalePercent(num5 * 100f);
								pdfStamper.GetOverContent(num).AddImage(image);
							}
						}
					}
				}
				pdfReader.Close();
				try
				{
					if (imageFiles == null || imageFiles.Count <= 0)
					{
						return;
					}
					int count = imageFiles.Count;
					for (int num6 = count - 1; num6 >= 0; num6--)
					{
						try
						{
							if (!string.IsNullOrEmpty(imageFiles[num6].Path))
							{
								File.Delete(imageFiles[num6].Path);
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
			List<ImageOfPageDTO> list = new List<ImageOfPageDTO>();
			try
			{
				LicenceProcess.SetLicenseForAspose();
				Aspose.Pdf.Document document = new Aspose.Pdf.Document(output_file);
				for (int i = 1; i <= document.Pages.Count; i++)
				{
					string filename = string.Format("splitimage{0:d}{1}.jpg", i, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
					string fullPathFile = Utils.GetFullPathFile(filename);
					using (FileStream fileStream = new FileStream(fullPathFile, FileMode.Create))
					{
						Resolution resolution = new Resolution(300);
						JpegDevice jpegDevice = new JpegDevice(resolution, 100);
						jpegDevice.Process(document.Pages[i], fileStream);
						fileStream.Close();
						list.Add(new ImageOfPageDTO
						{
							Path = fullPathFile,
							PageNumber = document.Pages[i].Number,
							Width = (float)document.Pages[i].Rect.Width,
							Height = (float)document.Pages[i].Rect.Height
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
