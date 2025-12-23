using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.License;

namespace Inventec.Common.SignLibrary
{
	internal class ProcessFile
	{
		internal void SplitPdfFileWithKey(Stream stream, ref Stream splitFileContentStream, ref double oginalHeight, ref double oginalWidth)
		{
			try
			{
				List<SignPositionADO> positionHeaders = PdfDocumentProcess.GetPositionBySearchKey(stream, "{SignLibrary.SplitPdfHeaderKey}");
				stream.Position = 0L;
				List<SignPositionADO> positionContents = PdfDocumentProcess.GetPositionBySearchKey(stream, "{SignLibrary.SplitPdfContentKey}");
				stream.Position = 0L;
				if ((positionHeaders != null && positionHeaders.Count > 0) || (positionContents != null && positionContents.Count > 0))
				{
					LicenceProcess.SetLicenseForAspose();
					double num = 0.0;
					using (Document document = new Document(stream))
					{
						string outputFileName = Utils.GenerateTempFileWithin();
						document.Save(outputFileName);
						string text = Utils.GenerateTempFileWithin();
						Page page = document.Pages[1];
						double height = page.GetPageRect(false).Height;
						num = page.GetPageRect(false).Width;
						oginalHeight = page.GetPageRect(false).Height;
						oginalWidth = page.GetPageRect(false).Width;
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
							using (Document document2 = ((flag && File.Exists(text)) ? new Document(text) : new Document()))
							{
								if (num3 < 4)
								{
									document2.Pages.Add(array);
									num3 = 0;
									flag = false;
								}
								else
								{
									document2.Pages.Add(array.Skip(0).Take(3).ToArray());
									array = array.Skip(3).ToArray();
									num3 = array.Count();
									flag = true;
								}
								if (!flag2)
								{
									Page page2 = document2.Pages[1];
									page2.CropBox = new Rectangle(llx, lly, urx, num2);
									flag2 = true;
								}
								document2.Save(text);
							}
						}
						using (Document document3 = new Document(text))
						{
							document3.Save(splitFileContentStream);
							splitFileContentStream.Position = 0L;
						}
						try
						{
							File.Delete(text);
							return;
						}
						catch
						{
							return;
						}
					}
				}
				using (Document document4 = new Document(stream))
				{
					Page page3 = document4.Pages[1];
					oginalHeight = page3.GetPageRect(false).Height;
					oginalWidth = page3.GetPageRect(false).Width;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal static List<ImageOfPageDTO> ConvertPdfToImage(string pdf_file)
		{
			List<ImageOfPageDTO> list = new List<ImageOfPageDTO>();
			try
			{
				LicenceProcess.SetLicenseForAspose();
				Document document = new Document(pdf_file);
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
