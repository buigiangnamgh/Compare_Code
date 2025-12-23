using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignFile;
using Inventec.Common.SignLibrary.ADO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WinForms;

namespace Inventec.Common.SignLibrary
{
	public class Utils
	{
		public static byte[] SignPadImageData;

		public static PdfReader GetTempReader(Rectangle pageSize = null)
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Expected O, but got Unknown
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Expected O, but got Unknown
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Expected O, but got Unknown
			MemoryStream memoryStream = new MemoryStream();
			if (pageSize != null)
			{
				Document val = new Document(pageSize);
				try
				{
					PdfWriter.GetInstance(val, (Stream)memoryStream);
					val.Open();
					val.Add((IElement)new Phrase(" "));
				}
				finally
				{
					if (val != null)
					{
						((IDisposable)val).Dispose();
					}
				}
			}
			else
			{
				Document val2 = new Document();
				try
				{
					PdfWriter.GetInstance(val2, (Stream)memoryStream);
					val2.Open();
					val2.Add((IElement)new Phrase(" "));
				}
				finally
				{
					if (val2 != null)
					{
						((IDisposable)val2).Dispose();
					}
				}
			}
			return new PdfReader(memoryStream.ToArray());
		}

		public static void ProcessClearAllFileInTempFolder()
		{
			try
			{
				string text = ParentTempFolder();
				string text2 = GenerateTempFolderWithinByDate();
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				if (Directory.Exists(text))
				{
					FileInfo[] files = directoryInfo.GetFiles();
					foreach (FileInfo fileInfo in files)
					{
						try
						{
							File.SetAttributes(fileInfo.FullName, FileAttributes.Normal);
							fileInfo.Delete();
						}
						catch (Exception ex)
						{
							LogSystem.Warn(ex);
						}
					}
					DirectoryInfo[] directories = directoryInfo.GetDirectories("*", SearchOption.TopDirectoryOnly);
					for (int j = 0; j < directories.Length; j++)
					{
						DirectoryInfo dir = directories[j];
						try
						{
							if (!(dir.FullName != text2))
							{
								continue;
							}
							FileInfo[] files2 = dir.GetFiles();
							for (int k = 0; k < files2.Length; k++)
							{
								FileInfo file = files2[k];
								try
								{
									File.SetAttributes(file.FullName, FileAttributes.Normal);
									file.Delete();
								}
								catch (Exception ex2)
								{
									LogSystem.Warn("Xóa file theo đường dẫn thất bại____" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => file.FullName)), (object)file.FullName));
									LogSystem.Warn(ex2);
								}
							}
							try
							{
								dir.Delete();
							}
							catch (Exception ex3)
							{
								LogSystem.Warn("Xóa cả folder và các file bên trong theo đường dẫn thất bại____" + LogUtil.TraceData(LogUtil.GetMemberName<DirectoryInfo>((Expression<Func<DirectoryInfo>>)(() => dir)), (object)dir));
								LogSystem.Warn(ex3);
							}
						}
						catch (Exception ex4)
						{
							LogSystem.Warn(ex4);
						}
					}
				}
				else
				{
					LogSystem.Debug("ProcessClearAllFileInTempFolder: no clear file in folder,  path " + text + " not exists");
				}
			}
			catch (Exception ex5)
			{
				LogSystem.Warn(ex5);
			}
		}

		public static string GenerateTempFileWithin()
		{
			try
			{
				string path = GenerateTempFolderWithin();
				return Path.Combine(path, Guid.NewGuid().ToString() + ".pdf");
			}
			catch (IOException ex)
			{
				LogSystem.Warn("Error create temp file: " + ex.Message);
				return "";
			}
		}

		public static string GenerateTempFileWithin(string extention)
		{
			try
			{
				string path = GenerateTempFolderWithin();
				return Path.Combine(path, Guid.NewGuid().ToString() + extention);
			}
			catch (IOException ex)
			{
				LogSystem.Warn("Error create temp file: " + ex.Message);
				return "";
			}
		}

		public static string GenerateTempFolderWithin()
		{
			try
			{
				string text = GenerateTempFolderWithinByDate();
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				return text;
			}
			catch (IOException ex)
			{
				LogSystem.Warn("Error create temp file: " + ex.Message);
				return "";
			}
		}

		public static string GenerateTempFolderWithinByDate()
		{
			try
			{
				string text = Path.Combine(ParentTempFolder(), DateTime.Now.ToString("ddMMyyyy"));
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				return text;
			}
			catch (IOException ex)
			{
				LogSystem.Warn("Error create temp file: " + ex.Message);
				return "";
			}
		}

		public static string SignatureFolder()
		{
			try
			{
				return Path.Combine(Path.Combine(Application.StartupPath, "Img"), "Signature");
			}
			catch (IOException ex)
			{
				LogSystem.Warn("Error create temp file: " + ex.Message);
				return Application.StartupPath;
			}
		}

		public static string ParentTempFolder()
		{
			try
			{
				return Path.Combine(Application.StartupPath, "temp");
			}
			catch (IOException ex)
			{
				LogSystem.Warn("Error create temp file: " + ex.Message);
				return Application.StartupPath;
			}
		}

		public static string AppFilePathSignService()
		{
			try
			{
				return Path.Combine(Path.Combine(Path.Combine(Application.StartupPath, "Integrate"), "EMR.SignProcessor"), "EMR.SignProcessor.exe");
			}
			catch (IOException ex)
			{
				LogSystem.Warn("Error create temp file: " + ex.Message);
				return "";
			}
		}

		internal static string Base64Decode(string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(bytes);
		}

		internal static string Base64Encode(string dataEncode)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(dataEncode));
		}

		public static string GetFullPathFile(string filename)
		{
			return Path.Combine(GenerateTempFolderWithin(), filename);
		}

		internal static BaseFont GetBaseFont()
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "tahoma.ttf");
			return BaseFont.CreateFont(text, "Identity-H", false);
		}

		public static byte[] StreamToByte(Stream input)
		{
			byte[] array = new byte[16384];
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int count;
				while ((count = input.Read(array, 0, array.Length)) > 0)
				{
					memoryStream.Write(array, 0, count);
				}
				return memoryStream.ToArray();
			}
		}

		public static byte[] FileToByte(string input)
		{
			return File.ReadAllBytes(input);
		}

		public static string FileToBase64String(string input)
		{
			return Convert.ToBase64String(File.ReadAllBytes(input));
		}

		public static void ByteToFile(byte[] arrInFile, string saveFile)
		{
			try
			{
				File.WriteAllBytes(saveFile, arrInFile);
			}
			catch (Exception ex)
			{
				LogSystem.Warn("File gui sang khong phai dinh dang file pdf, he thong core ky phai convert sang pdf, nhung khong convert duoc, co the do khong co quyen do folder dang chay khong duoc gan quyen doc ghi, can kiem tra lai");
				LogSystem.Warn(ex);
				MessageBox.Show("Folder đang chạy không được cấp quyền đọc ghi, vui lòng kiểm tra lại");
			}
		}

		internal static byte[] ImageToByte(Bitmap img)
		{
			try
			{
				ImageConverter imageConverter = new ImageConverter();
				return (byte[])imageConverter.ConvertTo(img, typeof(byte[]));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return null;
		}

		public static void ProcessFileInput(string inputFile, string ext, ref string inputFileWork, string documentTypeCode = "")
		{
			if (ext == ".pdf" || ext == ".xml" || ext == ".json")
			{
				inputFileWork = inputFile;
				return;
			}
			switch (ext)
			{
			case ".xlsx":
				inputFileWork = GenerateTempFileWithin();
				FileConvert.ExcelToPdf(null, inputFile, null, inputFileWork);
				break;
			case ".xls":
				if (!string.IsNullOrEmpty(documentTypeCode) && documentTypeCode == "07")
				{
					inputFileWork = GenerateTempFileWithin();
					if (!FileConvert.ExcelToPdf__Old(ext, null, inputFile, null, inputFileWork))
					{
						LogSystem.Info("ExcelToPdf__Old fail");
					}
					try
					{
						if (File.Exists(inputFile))
						{
							File.Delete(inputFile);
						}
						break;
					}
					catch
					{
						break;
					}
				}
				if (!FileConvert.ExportExcelToPdfUsingApose(inputFile, inputFileWork))
				{
					LogSystem.Info("ExportExcelToPdfUsingApose fail");
					break;
				}
				try
				{
					if (File.Exists(inputFile))
					{
						File.Delete(inputFile);
					}
					break;
				}
				catch
				{
					break;
				}
			case ".rdlc":
				inputFileWork = ConvertRdlcToPdf(inputFile);
				break;
			default:
				inputFileWork = GenerateTempFileWithin();
				FileConvert.DocToPdf(null, inputFile, null, inputFileWork);
				break;
			}
		}

		public static void ProcessFileInput(byte[] inputByte, string ext, ref string inputFileWork, string documentTypeCode = "")
		{
			inputFileWork = GenerateTempFileWithin();
			if (ext == ".pdf" || ext == ".xml" || ext == ".json")
			{
				File.WriteAllBytes(inputFileWork, inputByte);
				return;
			}
			if (ext == ".xlsx")
			{
				using (MemoryStream memoryStream = new MemoryStream(inputByte))
				{
					memoryStream.Position = 0L;
					FileConvert.ExcelToPdf(memoryStream, "", null, inputFileWork);
					DisposeStream(memoryStream);
					return;
				}
			}
			if (ext == ".xls")
			{
				if (!string.IsNullOrEmpty(documentTypeCode) && documentTypeCode == "07")
				{
					using (MemoryStream memoryStream2 = new MemoryStream(inputByte))
					{
						memoryStream2.Position = 0L;
						if (!FileConvert.ExcelToPdf__Old(ext, memoryStream2, "", null, inputFileWork))
						{
							LogSystem.Info("ExcelToPdf__Old fail");
						}
						DisposeStream(memoryStream2);
						return;
					}
				}
				using (MemoryStream memoryStream3 = new MemoryStream(inputByte))
				{
					memoryStream3.Position = 0L;
					if (!FileConvert.ExportExcelToPdfUsingApose(memoryStream3, inputFileWork))
					{
						LogSystem.Info("convertExcelToPdf__Old fail");
					}
					DisposeStream(memoryStream3);
					return;
				}
			}
			using (MemoryStream memoryStream4 = new MemoryStream(inputByte))
			{
				memoryStream4.Position = 0L;
				FileConvert.DocToPdf(memoryStream4, "", null, inputFileWork, ext);
				DisposeStream(memoryStream4);
			}
		}

		public static void DisposeStream(Stream stream)
		{
			try
			{
				if (stream != null)
				{
					try
					{
						stream.Close();
						stream.Dispose();
					}
					catch
					{
					}
				}
				stream = null;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public static string GetExtByFileType(FileType fileType)
		{
			string text = "";
			switch (fileType)
			{
			case FileType.Pdf:
				return ".pdf";
			case FileType.Xls:
				return ".xls";
			case FileType.Xlsx:
				return ".xlsx";
			case FileType.Rdlc:
				return ".rdlc";
			case FileType.Doc:
				return ".doc";
			case FileType.Docx:
				return ".docx";
			case FileType.Html:
				return ".html";
			case FileType.Rtf:
				return ".rtf";
			case FileType.Xml:
				return ".xml";
			case FileType.Json:
				return ".json";
			default:
				return ".pdf";
			}
		}

		internal static string ConvertRdlcToPdf(string rdlcFile)
		{
			string text = GenerateTempFileWithin();
			string mimeType = string.Empty;
			string encoding = string.Empty;
			string empty = string.Empty;
			ReportViewer reportViewer = new ReportViewer();
			string fileNameExtension = "";
			reportViewer.LocalReport.ReportPath = rdlcFile;
			string[] streams;
			Warning[] warnings;
			byte[] array = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
			using (FileStream fileStream = new FileStream(text, FileMode.Create))
			{
				fileStream.Write(array, 0, array.Length);
			}
			return text;
		}

		internal static void AddTextAnnotation(string filePath, string contents, int pageNum, double x, double y, int width, int height)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Expected O, but got Unknown
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Expected O, but got Unknown
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Expected O, but got Unknown
			//IL_0138: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Expected O, but got Unknown
			PdfReader val = null;
			PdfStamper val2 = null;
			try
			{
				using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
				{
					val = new PdfReader((Stream)fileStream);
				}
				using (FileStream fileStream2 = new FileStream(filePath, FileMode.Create))
				{
					val2 = new PdfStamper(val, (Stream)fileStream2, '\0', true);
					Rectangle val3 = new Rectangle((float)x, (float)y, (float)x + (float)width, (float)y + (float)height);
					TextField val4 = new TextField(val2.Writer, val3, (string)null);
					((BaseField)val4).Text = contents;
					((BaseField)val4).FontSize = 8f;
					((BaseField)val4).TextColor = BaseColor.DARK_GRAY;
					((BaseField)val4).BackgroundColor = new BaseColor(Color.LightGoldenrodYellow);
					((BaseField)val4).BorderColor = new BaseColor(Color.BurlyWood);
					((BaseField)val4).Options = 4096;
					val4.SetExtraMargin(2f, 2f);
					((BaseField)val4).Alignment = 4;
					PdfAppearance appearance = val4.GetAppearance();
					PdfAnnotation val5 = PdfAnnotation.CreateFreeText(val2.Writer, val3, (string)null, new PdfContentByte((PdfWriter)null));
					val5.SetAppearance(PdfName.N, (PdfTemplate)(object)appearance);
					val5.Flags = 196;
					((PdfDictionary)val5).Put(PdfName.NM, (PdfObject)new PdfString(Guid.NewGuid().ToString()));
					((PdfDictionary)val5).Put(PdfName.CONTENTS, (PdfObject)new PdfString(contents));
					val2.AddAnnotation(val5, pageNum);
					val2.Close();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Could not add signature image to PDF with error: " + ex.Message);
			}
		}

		internal static List<SignPositionADO> GetPdfSignPosition(PdfReader reader)
		{
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Expected O, but got Unknown
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ae: Expected O, but got Unknown
			//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d2: Expected O, but got Unknown
			List<SignPositionADO> list = new List<SignPositionADO>();
			try
			{
				for (int i = 1; i <= reader.NumberOfPages; i++)
				{
					PdfDictionary pageN = reader.GetPageN(i);
					PdfArray asArray = pageN.GetAsArray(PdfName.ANNOTS);
					LogSystem.Debug("annots.Size=" + ((asArray != null) ? asArray.Size : 0));
					if (asArray == null || asArray.Size == 0)
					{
						continue;
					}
					for (int j = 0; j < asArray.Size; j++)
					{
						PdfDictionary asDict = asArray.GetAsDict(j);
						PdfName val = ((asDict != null) ? asDict.GetAsName(PdfName.SUBTYPE) : null);
						if (val != null && ((object)val).Equals((object)PdfName.TEXT))
						{
							string text = ((object)asDict.GetAsString(PdfName.CONTENTS)).ToString();
							PdfArray asArray2 = asDict.GetAsArray(PdfName.RECT);
							Rectangle reactanle = new Rectangle(asArray2.GetAsNumber(0).FloatValue, asArray2.GetAsNumber(1).FloatValue, asArray2.GetAsNumber(2).FloatValue, asArray2.GetAsNumber(3).FloatValue);
							SignPositionADO item = new SignPositionADO
							{
								PageNUm = i,
								Reactanle = reactanle,
								Text = text
							};
							list.Add(item);
						}
						else if (val != null && ((object)val).Equals((object)PdfName.SQUARE))
						{
							string text2 = ((object)asDict.GetAsString(PdfName.CONTENTS)).ToString();
							PdfArray asArray3 = asDict.GetAsArray(PdfName.RECT);
							Rectangle reactanle2 = new Rectangle(asArray3.GetAsNumber(0).FloatValue, asArray3.GetAsNumber(1).FloatValue, asArray3.GetAsNumber(2).FloatValue, asArray3.GetAsNumber(3).FloatValue);
							string[] array = text2.Split(new string[2] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
							string text3 = "";
							if (array.Length == 1)
							{
								text3 = array[0];
							}
							else if (array.Length > 1)
							{
								text3 = array[array.Length - 1];
							}
							SignPositionADO item2 = new SignPositionADO
							{
								PageNUm = i,
								Reactanle = reactanle2,
								Text = text3
							};
							list.Add(item2);
						}
						else if (val != null && ((object)val).Equals((object)PdfName.FREETEXT))
						{
							PdfString asString = asDict.GetAsString(PdfName.CONTENTS);
							string text4 = ((asString != null) ? asString.ToUnicodeString() : "");
							string text5 = ((asString != null) ? ((object)asString).ToString() : "");
							PdfArray asArray4 = asDict.GetAsArray(PdfName.RECT);
							Rectangle reactanle3 = new Rectangle(asArray4.GetAsNumber(0).FloatValue, asArray4.GetAsNumber(1).FloatValue, asArray4.GetAsNumber(2).FloatValue, asArray4.GetAsNumber(3).FloatValue);
							SignPositionADO item3 = new SignPositionADO
							{
								PageNUm = i,
								Reactanle = reactanle3,
								Text = ((!string.IsNullOrEmpty(text4)) ? text4 : text5)
							};
							list.Add(item3);
						}
					}
				}
				list = (from o in list
					where o.Text.StartsWith("$")
					orderby o.Text
					select o).ToList();
				foreach (SignPositionADO item4 in list)
				{
					string[] array2 = item4.Text.Split(new string[1] { "__" }, StringSplitOptions.RemoveEmptyEntries);
					if (array2 == null || array2.Count() <= 1)
					{
						continue;
					}
					string[] array3 = array2[1].Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
					if (array3 == null || array3.Count() <= 0)
					{
						continue;
					}
					string[] array4 = array3;
					foreach (string text6 in array4)
					{
						string[] array5 = text6.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
						if (array5 != null && array5.Count() > 1)
						{
							if (array5[0] == "d")
							{
								item4.TypeDisplay = TypeConvertParse.ToInt32(array5[1]);
							}
							else if (array5[0] == "p")
							{
								item4.TextPosition = (Constans.TEXT_POSITON)TypeConvertParse.ToInt32(array5[1]);
							}
							else if (array5[0] == "f")
							{
								item4.SizeFont = TypeConvertParse.ToInt32(array5[1]);
							}
							else if (array5[0] == "w")
							{
								item4.WidthRectangle = TypeConvertParse.ToInt32(array5[1]);
							}
							else if (array5[0] == "h")
							{
								item4.HeightRectangle = TypeConvertParse.ToInt32(array5[1]);
							}
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

		internal static List<SignPositionADO> GetPdfPatientSignPosition(PdfReader reader)
		{
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Expected O, but got Unknown
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ae: Expected O, but got Unknown
			//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d2: Expected O, but got Unknown
			//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ab: Expected O, but got Unknown
			List<SignPositionADO> list = new List<SignPositionADO>();
			try
			{
				for (int i = 1; i <= reader.NumberOfPages; i++)
				{
					PdfDictionary pageN = reader.GetPageN(i);
					PdfArray asArray = pageN.GetAsArray(PdfName.ANNOTS);
					LogSystem.Debug("annots.Size=" + ((asArray != null) ? asArray.Size : 0));
					if (asArray == null || asArray.Size == 0)
					{
						continue;
					}
					for (int j = 0; j < asArray.Size; j++)
					{
						PdfDictionary asDict = asArray.GetAsDict(j);
						PdfName val = ((asDict != null) ? asDict.GetAsName(PdfName.SUBTYPE) : null);
						if (val != null && ((object)val).Equals((object)PdfName.TEXT))
						{
							string text = ((object)asDict.GetAsString(PdfName.CONTENTS)).ToString();
							PdfArray asArray2 = asDict.GetAsArray(PdfName.RECT);
							Rectangle reactanle = new Rectangle(asArray2.GetAsNumber(0).FloatValue, asArray2.GetAsNumber(1).FloatValue, asArray2.GetAsNumber(2).FloatValue, asArray2.GetAsNumber(3).FloatValue);
							SignPositionADO item = new SignPositionADO
							{
								PageNUm = i,
								Reactanle = reactanle,
								Text = text
							};
							list.Add(item);
						}
						else if (val != null && ((object)val).Equals((object)PdfName.SQUARE))
						{
							string text2 = ((object)asDict.GetAsString(PdfName.CONTENTS)).ToString();
							PdfArray asArray3 = asDict.GetAsArray(PdfName.RECT);
							Rectangle reactanle2 = new Rectangle(asArray3.GetAsNumber(0).FloatValue, asArray3.GetAsNumber(1).FloatValue, asArray3.GetAsNumber(2).FloatValue, asArray3.GetAsNumber(3).FloatValue);
							string[] array = text2.Split(new string[2] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
							string text3 = "";
							if (array.Length == 1)
							{
								text3 = array[0];
							}
							else if (array.Length > 1)
							{
								text3 = array[array.Length - 1];
							}
							SignPositionADO item2 = new SignPositionADO
							{
								PageNUm = i,
								Reactanle = reactanle2,
								Text = text3
							};
							list.Add(item2);
						}
						else if (val != null && ((object)val).Equals((object)PdfName.FREETEXT))
						{
							PdfString asString = asDict.GetAsString(PdfName.CONTENTS);
							string text4 = ((asString != null) ? asString.ToUnicodeString() : "");
							string text5 = ((asString != null) ? ((object)asString).ToString() : "");
							PdfArray asArray4 = asDict.GetAsArray(PdfName.RECT);
							Rectangle reactanle3 = new Rectangle(asArray4.GetAsNumber(0).FloatValue, asArray4.GetAsNumber(1).FloatValue, asArray4.GetAsNumber(2).FloatValue, asArray4.GetAsNumber(3).FloatValue);
							SignPositionADO item3 = new SignPositionADO
							{
								PageNUm = i,
								Reactanle = reactanle3,
								Text = ((!string.IsNullOrEmpty(text4)) ? text4 : text5)
							};
							list.Add(item3);
						}
						else if (val != null && ((object)val).Equals((object)PdfName.WIDGET))
						{
							PdfString asString2 = asDict.GetAsString(PdfName.CONTENTS);
							string text6 = ((asString2 != null) ? asString2.ToUnicodeString() : "");
							string text7 = ((asString2 != null) ? ((object)asString2).ToString() : "");
							PdfArray asArray5 = asDict.GetAsArray(PdfName.RECT);
							Rectangle reactanle4 = new Rectangle(asArray5.GetAsNumber(0).FloatValue, asArray5.GetAsNumber(1).FloatValue, asArray5.GetAsNumber(2).FloatValue, asArray5.GetAsNumber(3).FloatValue);
							SignPositionADO item4 = new SignPositionADO
							{
								PageNUm = i,
								Reactanle = reactanle4,
								Text = ((!string.IsNullOrEmpty(text6)) ? text6 : text7)
							};
							list.Add(item4);
						}
					}
				}
				list = list.Where((SignPositionADO o) => o.Text.Equals("#@!@#PATIENT")).ToList();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return list;
		}

		internal static int GetSignedCount(PdfReader reader)
		{
			AcroFields acroFields = reader.AcroFields;
			List<string> signatureNames = acroFields.GetSignatureNames();
			if (signatureNames == null || signatureNames.Count == 0)
			{
				return 0;
			}
			return signatureNames.Count;
		}

		public static string TimeNumberToTimeString(long time)
		{
			string result = null;
			try
			{
				string text = time.ToString();
				if (text != null && text.Length >= 14)
				{
					result = new StringBuilder().Append(text.Substring(6, 2)).Append("/").Append(text.Substring(4, 2))
						.Append("/")
						.Append(text.Substring(0, 4))
						.Append(" ")
						.Append(text.Substring(8, 2))
						.Append(":")
						.Append(text.Substring(10, 2))
						.Append(":")
						.Append(text.Substring(12, 2))
						.ToString();
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static long? GetTimeNow()
		{
			long? num = null;
			try
			{
				return long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
			}
			catch (Exception)
			{
				num = null;
			}
			return num;
		}
	}
}
