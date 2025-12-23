using System;
using System.Collections;
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
			//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b2: Expected O, but got Unknown
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Expected O, but got Unknown
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Expected O, but got Unknown
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01af: Expected O, but got Unknown
			//IL_055f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0566: Expected O, but got Unknown
			//IL_038b: Unknown result type (might be due to invalid IL or missing references)
			//IL_039b: Expected O, but got Unknown
			//IL_0394: Unknown result type (might be due to invalid IL or missing references)
			//IL_0524: Unknown result type (might be due to invalid IL or missing references)
			//IL_052e: Expected O, but got Unknown
			//IL_049e: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a8: Expected O, but got Unknown
			//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f6: Expected O, but got Unknown
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
					Document val = new Document(stream);
					try
					{
						string text = Utils.GenerateTempFileWithin();
						val.Save(text);
						double height;
						if ((isSplitHeaderKey ?? true) && positionHeaders != null && positionHeaders.Count > 0)
						{
							Document val2 = new Document();
							try
							{
								val2.Pages.Add((from Page o in (IEnumerable)val.Pages
									where o.Number <= positionHeaders[0].PageNUm
									select o).ToArray());
								Page val3 = val2.Pages[positionHeaders[0].PageNUm];
								height = val3.GetPageRect(false).Height;
								num = val3.GetPageRect(false).Width;
								oginalHeight = val3.GetPageRect(false).Height;
								val3.CropBox = new Rectangle(0.0, (double)positionHeaders[0].Reactanle.Top, num, height);
								string text2 = Utils.GenerateTempFileWithin();
								val2.Save(splitFileHeaderStream);
								val2.Save(text2);
								splitFileHeaderStream.Position = 0L;
							}
							finally
							{
								if (val2 != null)
								{
									((IDisposable)val2).Dispose();
								}
							}
						}
						if (!(isSplitContentKey ?? true))
						{
							return;
						}
						string text3 = Utils.GenerateTempFileWithin();
						Page val4 = val.Pages[1];
						height = val4.GetPageRect(false).Height;
						num = val4.GetPageRect(false).Width;
						oginalHeight = val4.GetPageRect(false).Height;
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
							Document val5 = ((flag && File.Exists(text3)) ? new Document(text3) : new Document());
							try
							{
								if (num6 < 4)
								{
									val5.Pages.Add(array);
									num6 = 0;
									flag = false;
								}
								else
								{
									val5.Pages.Add(array.Skip(0).Take(3).ToArray());
									array = array.Skip(3).ToArray();
									num6 = array.Count();
									flag = true;
								}
								if (!flag2)
								{
									if (positionHeaders != null && positionHeaders.Count > 0 && positionHeaders[0].PageNUm < positionContents[0].PageNUm)
									{
										Page val6 = val5.Pages[positionHeaders[0].PageNUm];
										val6.CropBox = new Rectangle(0.0, 0.0, num, (double)positionHeaders[0].Reactanle.Top);
										Page val7 = val5.Pages[positionContents[0].PageNUm];
										val7.CropBox = new Rectangle(0.0, (double)positionContents[0].Reactanle.Top, num, height);
									}
									else
									{
										Page val8 = val5.Pages[positionContents[0].PageNUm];
										val8.CropBox = new Rectangle(num3, num4, num5, num2);
									}
									flag2 = true;
								}
								val5.Save(text3);
							}
							finally
							{
								if (val5 != null)
								{
									((IDisposable)val5).Dispose();
								}
							}
						}
						Document val9 = new Document(text3);
						try
						{
							val9.Save(splitFileContentStream);
							splitFileContentStream.Position = 0L;
						}
						finally
						{
							if (val9 != null)
							{
								((IDisposable)val9).Dispose();
							}
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
					finally
					{
						if (val != null)
						{
							((IDisposable)val).Dispose();
						}
					}
				}
				Document val10 = new Document(stream);
				try
				{
					Page val11 = val10.Pages[1];
					oginalHeight = val11.GetPageRect(false).Height;
				}
				finally
				{
					if (val10 != null)
					{
						((IDisposable)val10).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void splitIntoHalfPages(string sourceFile)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			PdfReader val = new PdfReader(sourceFile);
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				Document val2 = new Document();
				PdfCopy val3 = new PdfCopy(val2, (Stream)memoryStream);
				val2.Open();
				for (int i = 1; i <= val.NumberOfPages; i++)
				{
					float num = 1f;
					PdfDictionary pageN = val.GetPageN(i);
					Rectangle cropBox = val.GetCropBox(i);
					PdfArray val4 = new PdfArray(new float[4]
					{
						cropBox.GetLeft(num),
						cropBox.GetBottom(num),
						(cropBox.GetLeft(num) + cropBox.GetRight(num)) / 2f,
						cropBox.GetTop(num)
					});
					PdfArray val5 = new PdfArray(new float[4]
					{
						(cropBox.GetLeft(num) + cropBox.GetRight(num)) / 2f,
						cropBox.GetBottom(num),
						cropBox.GetRight(num),
						cropBox.GetTop(num)
					});
					PdfImportedPage importedPage = ((PdfWriter)val3).GetImportedPage(val, i);
					pageN.Put(PdfName.CROPBOX, (PdfObject)(object)val4);
					val3.AddPage(importedPage);
					pageN.Put(PdfName.CROPBOX, (PdfObject)(object)val5);
					val3.AddPage(importedPage);
				}
				val2.Close();
			}
			finally
			{
				val.Close();
			}
		}

		public void JoinPartialPdfFile(decimal oginalHeight, FileDataDTO headerData, List<FileDataDTO> contentDatas, ref string outputPdfFile)
		{
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Expected O, but got Unknown
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
				Document val = new Document();
				try
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
				finally
				{
					if (val != null)
					{
						((IDisposable)val).Dispose();
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
