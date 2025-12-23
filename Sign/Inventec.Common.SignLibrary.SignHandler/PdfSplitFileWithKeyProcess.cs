using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.License;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Inventec.Common.SignLibrary.SignHandler
{
	public class PdfSplitFileWithKeyProcess
	{
		internal void SplitPdfFileWithKey(Stream stream, ref Stream splitFileHeaderStream, ref Stream splitFileContentStream, ref double oginalHeight, bool? isSplitHeaderKey = true, bool? isSplitContentKey = true)
		{
			try
			{
				List<SignPositionADO> positionHeaders = ((isSplitHeaderKey ?? true) ? PdfDocumentProcess.GetPositionBySearchKey(stream, GlobalStore.SplitPdfHeaderKey) : null);
				stream.Position = 0L;
				List<SignPositionADO> positionContents = ((isSplitContentKey ?? true) ? PdfDocumentProcess.GetPositionBySearchKey(stream, GlobalStore.SplitPdfContentKey) : null);
				stream.Position = 0L;
				if ((positionHeaders != null && positionHeaders.Count > 0) || (positionContents != null && positionContents.Count > 0))
				{
					LicenceProcess.SetLicenseForAspose();
					double num = 0.0;
					using (Aspose.Pdf.Document document = new Aspose.Pdf.Document(stream))
					{
						string outputFileName = Utils.GenerateTempFileWithin();
						document.Save(outputFileName);
						double height;
						if ((isSplitHeaderKey ?? true) && positionHeaders != null && positionHeaders.Count > 0)
						{
							using (Aspose.Pdf.Document document2 = new Aspose.Pdf.Document())
							{
								document2.Pages.Add((from Page o in document.Pages
									where o.Number <= positionHeaders[0].PageNUm
									select o).ToArray());
								Page page = document2.Pages[positionHeaders[0].PageNUm];
								height = page.GetPageRect(false).Height;
								num = page.GetPageRect(false).Width;
								oginalHeight = page.GetPageRect(false).Height;
								page.CropBox = new Aspose.Pdf.Rectangle(0.0, positionHeaders[0].Reactanle.Top, num, height);
								string outputFileName2 = Utils.GenerateTempFileWithin();
								document2.Save(splitFileHeaderStream);
								document2.Save(outputFileName2);
								splitFileHeaderStream.Position = 0L;
							}
						}
						if ((!isSplitContentKey) ?? false)
						{
							return;
						}
						string text = Utils.GenerateTempFileWithin();
						Page page2 = document.Pages[1];
						height = page2.GetPageRect(false).Height;
						num = page2.GetPageRect(false).Width;
						oginalHeight = page2.GetPageRect(false).Height;
						bool flag = false;
						double num2 = 0.0;
						double llx;
						double lly;
						double urx;
						Page[] array;
						if (positionContents != null && positionContents.Count > 0)
						{
							llx = 0.0;
							lly = positionContents[0].Reactanle.Top;
							urx = num;
							num2 = positionHeaders[0].Reactanle.Top;
							List<int> list = (from Page o in document.Pages
								select o.Number).ToList();
							array = (from Page o in document.Pages
								where o.Number <= positionContents[0].PageNUm
								select o).ToArray();
						}
						else
						{
							llx = 0.0;
							lly = 0.0;
							urx = num;
							num2 = positionHeaders[0].Reactanle.Top;
							array = (from Page o in document.Pages
								where o.Number >= positionHeaders[0].PageNUm
								select o).ToArray();
						}
						bool flag2 = false;
						int num3 = array.Count();
						while (num3 > 0)
						{
							using (Aspose.Pdf.Document document3 = ((flag && File.Exists(text)) ? new Aspose.Pdf.Document(text) : new Aspose.Pdf.Document()))
							{
								if (num3 < 4)
								{
									document3.Pages.Add(array);
									num3 = 0;
									flag = false;
								}
								else
								{
									document3.Pages.Add(array.Skip(0).Take(3).ToArray());
									array = array.Skip(3).ToArray();
									num3 = array.Count();
									flag = true;
								}
								if (!flag2)
								{
									if (positionHeaders != null && positionHeaders.Count > 0 && positionHeaders[0].PageNUm < positionContents[0].PageNUm)
									{
										Page page3 = document3.Pages[positionHeaders[0].PageNUm];
										page3.CropBox = new Aspose.Pdf.Rectangle(0.0, 0.0, num, positionHeaders[0].Reactanle.Top);
										Page page4 = document3.Pages[positionContents[0].PageNUm];
										page4.CropBox = new Aspose.Pdf.Rectangle(0.0, positionContents[0].Reactanle.Top, num, height);
									}
									else
									{
										Page page5 = document3.Pages[positionContents[0].PageNUm];
										page5.CropBox = new Aspose.Pdf.Rectangle(llx, lly, urx, num2);
									}
									flag2 = true;
								}
								document3.Save(text);
							}
						}
						using (Aspose.Pdf.Document document4 = new Aspose.Pdf.Document(text))
						{
							document4.Save(splitFileContentStream);
							splitFileContentStream.Position = 0L;
						}
						try
						{
							return;
						}
						catch
						{
							return;
						}
					}
				}
				using (Aspose.Pdf.Document document5 = new Aspose.Pdf.Document(stream))
				{
					Page page6 = document5.Pages[1];
					oginalHeight = page6.GetPageRect(false).Height;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void splitIntoHalfPages(string sourceFile)
		{
			PdfReader pdfReader = new PdfReader(sourceFile);
			try
			{
				MemoryStream os = new MemoryStream();
				iTextSharp.text.Document document = new iTextSharp.text.Document();
				PdfCopy pdfCopy = new PdfCopy(document, os);
				document.Open();
				for (int i = 1; i <= pdfReader.NumberOfPages; i++)
				{
					float margin = 1f;
					PdfDictionary pageN = pdfReader.GetPageN(i);
					iTextSharp.text.Rectangle cropBox = pdfReader.GetCropBox(i);
					PdfArray value = new PdfArray(new float[4]
					{
						cropBox.GetLeft(margin),
						cropBox.GetBottom(margin),
						(cropBox.GetLeft(margin) + cropBox.GetRight(margin)) / 2f,
						cropBox.GetTop(margin)
					});
					PdfArray value2 = new PdfArray(new float[4]
					{
						(cropBox.GetLeft(margin) + cropBox.GetRight(margin)) / 2f,
						cropBox.GetBottom(margin),
						cropBox.GetRight(margin),
						cropBox.GetTop(margin)
					});
					PdfImportedPage importedPage = pdfCopy.GetImportedPage(pdfReader, i);
					pageN.Put(PdfName.CROPBOX, value);
					pdfCopy.AddPage(importedPage);
					pageN.Put(PdfName.CROPBOX, value2);
					pdfCopy.AddPage(importedPage);
				}
				document.Close();
			}
			finally
			{
				pdfReader.Close();
			}
		}

		public void JoinPartialPdfFile(decimal oginalHeight, FileDataDTO headerData, List<FileDataDTO> contentDatas, ref string outputPdfFile)
		{
			try
			{
				string text = Utils.GenerateTempFileWithin();
				string joinPdfFilePath = Utils.GenerateTempFileWithin();
				string text2 = Utils.GenerateTempFileWithin();
				string text3 = Utils.GenerateTempFileWithin();
				List<string> list = new List<string>();
				LicenceProcess.SetLicenseForAspose();
				Stream splitFileHeaderStream = new MemoryStream();
				Stream splitFileContentStream = new MemoryStream();
				double oginalHeight2 = 0.0;
				if (headerData != null && headerData.Stream != null && headerData.Stream.Length > 0)
				{
					SplitPdfFileWithKey(headerData.Stream, ref splitFileHeaderStream, ref splitFileContentStream, ref oginalHeight2, true, false);
				}
				List<Stream> list2 = new List<Stream>();
				using (new Aspose.Pdf.Document())
				{
					for (int i = 0; i < contentDatas.Count; i++)
					{
						Stream splitFileHeaderStream2 = new MemoryStream();
						Stream splitFileContentStream2 = new MemoryStream();
						SplitPdfFileWithKey(contentDatas[i].Stream, ref splitFileHeaderStream2, ref splitFileContentStream2, ref oginalHeight2, true, true);
						if (splitFileContentStream2 != null && splitFileContentStream2.Length > 0)
						{
							splitFileContentStream2.Position = 0L;
							list2.Add(splitFileContentStream2);
						}
						else
						{
							contentDatas[i].Stream.Position = 0L;
							list2.Add(contentDatas[i].Stream);
						}
					}
				}
				oginalHeight = (decimal)oginalHeight2;
				PdfDocumentProcess.InsertPages(splitFileHeaderStream, list2, text);
				PdfDocumentProcess.SplitOnePageToImageAndJoinToNewOnePdf(text, (float)oginalHeight, ref joinPdfFilePath);
				if (!string.IsNullOrEmpty(joinPdfFilePath) && File.Exists(joinPdfFilePath))
				{
					outputPdfFile = joinPdfFilePath;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
