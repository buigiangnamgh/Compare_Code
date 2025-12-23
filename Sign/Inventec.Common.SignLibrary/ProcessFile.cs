using System;
using System.Collections;
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
			//IL_035b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0362: Expected O, but got Unknown
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Expected O, but got Unknown
			//IL_0308: Unknown result type (might be due to invalid IL or missing references)
			//IL_030f: Expected O, but got Unknown
			//IL_0237: Unknown result type (might be due to invalid IL or missing references)
			//IL_0247: Expected O, but got Unknown
			//IL_0240: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d8: Expected O, but got Unknown
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
					Document val = new Document(stream);
					try
					{
						string text = Utils.GenerateTempFileWithin();
						val.Save(text);
						string text2 = Utils.GenerateTempFileWithin();
						Page val2 = val.Pages[1];
						double height = val2.GetPageRect(false).Height;
						num = val2.GetPageRect(false).Width;
						oginalHeight = val2.GetPageRect(false).Height;
						oginalWidth = val2.GetPageRect(false).Width;
						bool flag = false;
						double num2 = 0.0;
						double num3;
						double num4;
						double num5;
						Page[] array;
						if (positionContents != null && positionContents.Count > 0)
						{
							num3 = 0.0;
							num4 = positionContents[0].Reactanle.Top;
							num5 = num;
							num2 = positionHeaders[0].Reactanle.Top;
							List<int> list = (from Page o in (IEnumerable)val.Pages
								select o.Number).ToList();
							array = (from Page o in (IEnumerable)val.Pages
								where o.Number <= positionContents[0].PageNUm
								select o).ToArray();
						}
						else
						{
							num3 = 0.0;
							num4 = 0.0;
							num5 = num;
							num2 = positionHeaders[0].Reactanle.Top;
							array = (from Page o in (IEnumerable)val.Pages
								where o.Number >= positionHeaders[0].PageNUm
								select o).ToArray();
						}
						bool flag2 = false;
						int num6 = array.Count();
						while (num6 > 0)
						{
							Document val3 = ((flag && File.Exists(text2)) ? new Document(text2) : new Document());
							try
							{
								if (num6 < 4)
								{
									val3.Pages.Add(array);
									num6 = 0;
									flag = false;
								}
								else
								{
									val3.Pages.Add(array.Skip(0).Take(3).ToArray());
									array = array.Skip(3).ToArray();
									num6 = array.Count();
									flag = true;
								}
								if (!flag2)
								{
									Page val4 = val3.Pages[1];
									val4.CropBox = new Rectangle(num3, num4, num5, num2);
									flag2 = true;
								}
								val3.Save(text2);
							}
							finally
							{
								if (val3 != null)
								{
									((IDisposable)val3).Dispose();
								}
							}
						}
						Document val5 = new Document(text2);
						try
						{
							val5.Save(splitFileContentStream);
							splitFileContentStream.Position = 0L;
						}
						finally
						{
							if (val5 != null)
							{
								((IDisposable)val5).Dispose();
							}
						}
						try
						{
							File.Delete(text2);
							return;
						}
						catch
						{
							return;
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
				Document val6 = new Document(stream);
				try
				{
					Page val7 = val6.Pages[1];
					oginalHeight = val7.GetPageRect(false).Height;
					oginalWidth = val7.GetPageRect(false).Width;
				}
				finally
				{
					if (val6 != null)
					{
						((IDisposable)val6).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal static List<ImageOfPageDTO> ConvertPdfToImage(string pdf_file)
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
				Document val = new Document(pdf_file);
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
