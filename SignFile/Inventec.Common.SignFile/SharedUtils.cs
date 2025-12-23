using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Inventec.Common.Logging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.X509;

namespace Inventec.Common.SignFile
{
	public class SharedUtils
	{
		internal class CertManager
		{
			private static X509Certificate2 _certificate;

			internal static X509Certificate2 Certificate
			{
				get
				{
					if (_certificate == null)
					{
						string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "3dcb620b-e826-436e-a018-6826b0b4ed7f.pfx");
						LogSystem.Info(text);
						if (!File.Exists(text))
						{
							throw new FileNotFoundException("Certificate file not found: " + text);
						}
						byte[] rawData = File.ReadAllBytes(text);
						_certificate = new X509Certificate2(rawData, "@123", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
					}
					return _certificate;
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass27_0
		{
			public float imgTotalWidth;

			public float plusH;

			public float widthImagePercent;
		}

		public static string ConvertTVKhongDau(string str)
		{
			string[] array = new string[17]
			{
				"à", "á", "ạ", "ả", "ã", "â", "ầ", "ấ", "ậ", "ẩ",
				"ẫ", "ă", "ằ", "ắ", "ặ", "ẳ", "ẵ"
			};
			string[] array2 = new string[17]
			{
				"À", "Á", "Ạ", "Ả", "Ã", "Â", "Ầ", "Ấ", "Ậ", "Ẩ",
				"Ẫ", "Ă", "Ằ", "Ắ", "Ặ", "Ẳ", "Ẵ"
			};
			string[] array3 = new string[11]
			{
				"è", "é", "ẹ", "ẻ", "ẽ", "ê", "ề", "ế", "ệ", "ể",
				"ễ"
			};
			string[] array4 = new string[11]
			{
				"È", "É", "Ẹ", "Ẻ", "Ẽ", "Ê", "Ề", "Ế", "Ệ", "Ể",
				"Ễ"
			};
			string[] array5 = new string[5] { "ì", "í", "ị", "ỉ", "ĩ" };
			string[] array6 = new string[5] { "Ì", "Í", "Ị", "Ỉ", "Ĩ" };
			string[] array7 = new string[17]
			{
				"ò", "ó", "ọ", "ỏ", "õ", "ô", "ồ", "ố", "ộ", "ổ",
				"ỗ", "ơ", "ờ", "ớ", "ợ", "ở", "ỡ"
			};
			string[] array8 = new string[17]
			{
				"Ò", "Ó", "Ọ", "Ỏ", "Õ", "Ô", "Ồ", "Ố", "Ộ", "Ổ",
				"Ỗ", "Ơ", "Ờ", "Ớ", "Ợ", "Ở", "Ỡ"
			};
			string[] array9 = new string[11]
			{
				"ù", "ú", "ụ", "ủ", "ũ", "ư", "ừ", "ứ", "ự", "ử",
				"ữ"
			};
			string[] array10 = new string[11]
			{
				"Ù", "Ú", "Ụ", "Ủ", "Ũ", "Ư", "Ừ", "Ứ", "Ự", "Ử",
				"Ữ"
			};
			string[] array11 = new string[5] { "ỳ", "ý", "ỵ", "ỷ", "ỹ" };
			string[] array12 = new string[5] { "Ỳ", "Ý", "Ỵ", "Ỷ", "Ỹ" };
			str = str.Replace("đ", "d");
			str = str.Replace("Đ", "D");
			string[] array13 = array;
			string[] array14 = array13;
			foreach (string oldValue in array14)
			{
				str = str.Replace(oldValue, "a");
			}
			string[] array15 = array2;
			array14 = array15;
			foreach (string oldValue2 in array14)
			{
				str = str.Replace(oldValue2, "A");
			}
			string[] array16 = array3;
			array14 = array16;
			foreach (string oldValue3 in array14)
			{
				str = str.Replace(oldValue3, "e");
			}
			string[] array17 = array4;
			array14 = array17;
			foreach (string oldValue4 in array14)
			{
				str = str.Replace(oldValue4, "E");
			}
			string[] array18 = array5;
			array14 = array18;
			foreach (string oldValue5 in array14)
			{
				str = str.Replace(oldValue5, "i");
			}
			string[] array19 = array6;
			array14 = array19;
			foreach (string oldValue6 in array14)
			{
				str = str.Replace(oldValue6, "I");
			}
			string[] array20 = array7;
			array14 = array20;
			foreach (string oldValue7 in array14)
			{
				str = str.Replace(oldValue7, "o");
			}
			string[] array21 = array8;
			array14 = array21;
			foreach (string oldValue8 in array14)
			{
				str = str.Replace(oldValue8, "O");
			}
			string[] array22 = array9;
			array14 = array22;
			foreach (string oldValue9 in array14)
			{
				str = str.Replace(oldValue9, "u");
			}
			string[] array23 = array10;
			array14 = array23;
			foreach (string oldValue10 in array14)
			{
				str = str.Replace(oldValue10, "U");
			}
			string[] array24 = array11;
			array14 = array24;
			foreach (string oldValue11 in array14)
			{
				str = str.Replace(oldValue11, "y");
			}
			string[] array25 = array12;
			array14 = array25;
			foreach (string oldValue12 in array14)
			{
				str = str.Replace(oldValue12, "Y");
			}
			return str;
		}

		public static string GenerateTempFile()
		{
			try
			{
				string path = GenerateTempFolderWithin();
				return Path.Combine(path, Guid.NewGuid().ToString() + ".pdf");
			}
			catch (IOException ex)
			{
				Console.WriteLine("Error create temp file: " + ex.Message);
				return "";
			}
		}

		public static string GenerateTempFile(string ext)
		{
			try
			{
				string path = GenerateTempFolderWithin();
				return Path.Combine(path, Guid.NewGuid().ToString() + ((!string.IsNullOrEmpty(ext)) ? ext : ".pdf"));
			}
			catch (IOException ex)
			{
				Console.WriteLine("Error create temp file: " + ex.Message);
				return "";
			}
		}

		internal static string GenerateTempFolderWithin()
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

		internal static string GenerateTempFolderWithinByDate()
		{
			try
			{
				string text = Path.Combine(ParentTempFolder(), DateTime.Now.ToString("ddMMyyyy"));
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				try
				{
					string path = Path.Combine(ParentTempFolder(), DateTime.Now.AddDays(-1.0).ToString("ddMMyyyy"));
					if (Directory.Exists(path))
					{
						Directory.Delete(path, true);
					}
				}
				catch (Exception ex)
				{
					LogSystem.Warn("Error .Delete temp pre folder: " + ex.Message);
				}
				return text;
			}
			catch (IOException ex2)
			{
				LogSystem.Warn("Error create temp file: " + ex2.Message);
				return "";
			}
		}

		internal static string ParentTempFolder()
		{
			try
			{
				return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");
			}
			catch (IOException ex)
			{
				LogSystem.Warn("Error create temp file: " + ex.Message);
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		public static void CreatePdf(PdfReader[] readers, ref string outFilePath)
		{
			Document document = new Document();
			outFilePath = GenerateTempFile();
			PdfCopy pdfCopy = new PdfCopy(document, File.Open(outFilePath, FileMode.Create));
			pdfCopy.SetMergeFields();
			document.Open();
			PdfReader[] array = readers;
			foreach (PdfReader reader in array)
			{
				pdfCopy.AddDocument(reader);
			}
			document.Close();
			array = readers;
			foreach (PdfReader pdfReader in array)
			{
			}
		}

		public static void CreatePdf(string inFilePath, ref string outFilePath)
		{
			Document document = new Document();
			outFilePath = GenerateTempFile();
			PdfCopy pdfCopy = new PdfCopy(document, File.Open(outFilePath, FileMode.Create));
			pdfCopy.SetMergeFields();
			document.Open();
			PdfReader pdfReader = new PdfReader(inFilePath);
			pdfCopy.AddDocument(pdfReader);
			document.Close();
			pdfReader.Close();
		}

		public static bool SaveNewFileFromReader(PdfReader reader, ref string outFilePath, bool isCloseReader)
		{
			bool result = false;
			try
			{
				outFilePath = GenerateTempFile();
				using (FileStream os = File.Open(outFilePath, FileMode.Create))
				{
					PdfReader pdfReader = new PdfReader(reader);
					PdfConcatenate pdfConcatenate = new PdfConcatenate(os);
					List<int> list = new List<int>();
					for (int i = 0; i <= pdfReader.NumberOfPages; i++)
					{
						list.Add(i);
					}
					pdfReader.SelectPages(list);
					pdfConcatenate.AddPages(pdfReader);
					pdfReader.Close();
					pdfConcatenate.Close();
				}
				result = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error insertSignature: " + ex.Message);
				result = false;
			}
			finally
			{
				if (isCloseReader && reader != null)
				{
					reader.Close();
				}
			}
			return result;
		}

		public static bool SaveNewFileFromReader(string filename, ref string outFilePath)
		{
			bool result = false;
			try
			{
				outFilePath = GenerateTempFile();
				PdfReader pdfReader = new PdfReader(filename);
				List<int> list = new List<int>();
				for (int i = 0; i <= pdfReader.NumberOfPages; i++)
				{
					list.Add(i);
				}
				pdfReader.SelectPages(list);
				PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(outFilePath, FileMode.Create));
				pdfStamper.Close();
				pdfReader.Close();
				result = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error insertSignature: " + ex.Message);
				result = false;
			}
			finally
			{
			}
			return result;
		}

		public static bool SaveNewFileFromReader(byte[] bfile, ref string outFilePath, string ext = ".pdf")
		{
			bool result = false;
			try
			{
				outFilePath = GenerateTempFile(ext);
				if (ext == ".pdf")
				{
					using (FileStream os = File.Open(outFilePath, FileMode.Create, FileAccess.ReadWrite))
					{
						PdfConcatenate pdfConcatenate = new PdfConcatenate(os);
						PdfReader pdfReader = new PdfReader(bfile);
						List<int> list = new List<int>();
						for (int i = 0; i <= pdfReader.NumberOfPages; i++)
						{
							list.Add(i);
						}
						pdfReader.SelectPages(list);
						pdfConcatenate.AddPages(pdfReader);
						pdfReader.Close();
						pdfConcatenate.Close();
					}
				}
				else
				{
					ByteToFile(bfile, outFilePath);
				}
				result = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error insertSignature: " + ex.Message);
				result = false;
			}
			finally
			{
			}
			return result;
		}

		public static bool SaveNewFileFromReaderExt(byte[] bfile, ref string outFilePath, string ext = ".pdf")
		{
			bool result = false;
			try
			{
				outFilePath = GenerateTempFile(ext);
				if (ext == ".pdf")
				{
					using (FileStream os = File.Open(outFilePath, FileMode.Create, FileAccess.ReadWrite))
					{
						PdfConcatenate pdfConcatenate = new PdfConcatenate(os);
						PdfReader pdfReader = new PdfReader(bfile);
						List<int> list = new List<int>();
						for (int i = 0; i <= pdfReader.NumberOfPages; i++)
						{
							list.Add(i);
						}
						pdfReader.SelectPages(list);
						pdfConcatenate.AddPages(pdfReader);
						pdfReader.Close();
						pdfConcatenate.Close();
					}
				}
				else
				{
					ByteToFile(bfile, outFilePath);
				}
				result = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error insertSignature: " + ex.Message);
				result = false;
			}
			finally
			{
			}
			return result;
		}

		public static bool SaveNewFileFromReader(Stream sfile, ref string outFilePath)
		{
			bool result = false;
			try
			{
				outFilePath = GenerateTempFile();
				ByteToFile(StreamToByte(sfile), outFilePath);
				result = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error SaveNewFileFromReader: " + ex.Message);
				try
				{
					File.Delete(outFilePath);
				}
				catch
				{
				}
				result = false;
			}
			finally
			{
			}
			return result;
		}

		public static byte[] GetBytes(string str)
		{
			try
			{
				byte[] array = new byte[str.Length * 2];
				Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
				return array;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error get Byte from string: " + ex.Message);
				return null;
			}
		}

		public static string GetCN(Org.BouncyCastle.X509.X509Certificate cert)
		{
			try
			{
				return GetCNFromDN(GetSubject(cert));
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error getCN: " + ex.Message);
				return null;
			}
		}

		public static string GetCNFromDN(string dn)
		{
			try
			{
				char[] separator = new char[1] { ',' };
				string[] array = dn.Split(separator);
				string result = "";
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IndexOf("CN=") != -1)
					{
						char[] separator2 = new char[1] { '=' };
						result = array[i].Split(separator2)[1];
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error GetCNFromDN: " + ex.Message);
				return "";
			}
		}

		public static double GetCurrentMilli()
		{
			try
			{
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				return (long)(DateTime.UtcNow - dateTime).TotalMilliseconds;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error get current milli: " + ex.Message);
				return 0.0;
			}
		}

		public static string GetLocation(Org.BouncyCastle.X509.X509Certificate certificate)
		{
			try
			{
				return GetLocationFromDN(GetSubject(certificate));
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error getLocation: " + ex.Message);
				return null;
			}
		}

		public static string GetLocationFromDN(string dn)
		{
			try
			{
				char[] separator = new char[1] { ',' };
				string[] array = dn.Split(separator);
				string text = "";
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IndexOf("L=") != -1)
					{
						char[] separator2 = new char[1] { '=' };
						text = array[i].Split(separator2)[1];
					}
				}
				if (text != "")
				{
					return ConvertTVKhongDau(text);
				}
				return text;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error GetLocationFromDN: " + ex.Message);
				return "";
			}
		}

		public static string getSignName()
		{
			return GetCurrentMilli().ToString();
		}

		public static string GetSubject(Org.BouncyCastle.X509.X509Certificate certificate)
		{
			try
			{
				return certificate.SubjectDN.ToString();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error getSubject: " + ex.Message);
				return null;
			}
		}

		public static BaseFont GetBaseFont()
		{
			string name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "tahoma.ttf");
			return BaseFont.CreateFont(name, "Identity-H", false);
		}

		internal static SecureString GetSecurePin(string PinCode)
		{
			SecureString secureString = new SecureString();
			char[] array = PinCode.ToCharArray();
			char[] array2 = array;
			foreach (char c in array2)
			{
				secureString.AppendChar(c);
			}
			return secureString;
		}

		internal static string GetFileContentHash(string fileContent)
		{
			try
			{
				using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create())
				{
					byte[] array = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(fileContent));
					string text = BitConverter.ToString(array);
					return text.Replace("-", string.Empty);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return "";
		}

		internal static byte[] StreamToByte(Stream input)
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

		internal static byte[] FileToByte(string input)
		{
			return File.ReadAllBytes(input);
		}

		internal static void ByteToFile(byte[] arrInFile, string saveFile)
		{
			File.WriteAllBytes(saveFile, arrInFile);
		}

		internal static void UpdateimageCell(float widthRectangle, float heightRectangle, Image instance, float SignaltureImageWidth, float widthImagePercent, float plusH)
		{
			_003C_003Ec__DisplayClass27_0 CS_0024_003C_003E8__locals26 = new _003C_003Ec__DisplayClass27_0();
			CS_0024_003C_003E8__locals26.plusH = plusH;
			CS_0024_003C_003E8__locals26.widthImagePercent = widthImagePercent;
			CS_0024_003C_003E8__locals26.imgTotalWidth = 0f;
			if (instance != null)
			{
				float num = heightRectangle - CS_0024_003C_003E8__locals26.plusH;
				float num2 = ((num > 0f) ? num : heightRectangle);
				if (instance.Width > widthRectangle || instance.Height > heightRectangle || (instance.Height > num && num > 0f))
				{
					float weightImgRealPercentTH1 = ((instance.Width > widthRectangle) ? (widthRectangle / instance.Width) : 0f);
					float heightImgRealPercentTH1 = ((instance.Height > num2) ? (num2 / instance.Height) : 0f);
					if (weightImgRealPercentTH1 > 0f && heightImgRealPercentTH1 > 0f)
					{
						CS_0024_003C_003E8__locals26.imgTotalWidth = instance.Width * CS_0024_003C_003E8__locals26.widthImagePercent * ((weightImgRealPercentTH1 < heightImgRealPercentTH1) ? weightImgRealPercentTH1 : heightImgRealPercentTH1) / 100f;
					}
					else if (heightImgRealPercentTH1 > 0f)
					{
						CS_0024_003C_003E8__locals26.imgTotalWidth = instance.Width * CS_0024_003C_003E8__locals26.widthImagePercent * heightImgRealPercentTH1 / 100f;
					}
					else if (weightImgRealPercentTH1 > 0f)
					{
						CS_0024_003C_003E8__locals26.imgTotalWidth = instance.Width * CS_0024_003C_003E8__locals26.widthImagePercent * weightImgRealPercentTH1 / 100f;
					}
					LogSystem.Info("2__" + LogUtil.TraceData(LogUtil.GetMemberName(() => weightImgRealPercentTH1), weightImgRealPercentTH1) + LogUtil.TraceData(LogUtil.GetMemberName(() => heightImgRealPercentTH1), heightImgRealPercentTH1) + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals26.imgTotalWidth), CS_0024_003C_003E8__locals26.imgTotalWidth) + LogUtil.TraceData(LogUtil.GetMemberName(() => CS_0024_003C_003E8__locals26.plusH), CS_0024_003C_003E8__locals26.plusH));
				}
				else
				{
					float num3 = ((num > 0f) ? num : heightRectangle) / instance.Height;
					CS_0024_003C_003E8__locals26.imgTotalWidth = instance.Width * CS_0024_003C_003E8__locals26.widthImagePercent * num3 / 100f;
				}
			}
			else
			{
				CS_0024_003C_003E8__locals26.imgTotalWidth = widthRectangle * CS_0024_003C_003E8__locals26.widthImagePercent / 100f;
			}
		}

		public static float CalculateWidthPercent(float widthRectangle, float heightRectangle, Image instance, float SignaltureImageWidth, float widthImagePercent, float plusH)
		{
			float imgTotalWidth = instance.Width;
			if (instance != null)
			{
				float heightRecModPlus = heightRectangle - plusH;
				float num = ((heightRecModPlus > 0f) ? heightRecModPlus : heightRectangle);
				if (instance.Width > widthRectangle || instance.Height > heightRectangle || (instance.Height > heightRecModPlus && heightRecModPlus > 0f))
				{
					float weightImgRealPercentTH1 = ((instance.Width > widthRectangle) ? (widthRectangle / instance.Width) : 0f);
					float heightImgRealPercentTH1 = ((instance.Height > num) ? (num / instance.Height) : 0f);
					if (weightImgRealPercentTH1 > 0f && heightImgRealPercentTH1 > 0f)
					{
						imgTotalWidth = instance.Width * widthImagePercent * ((weightImgRealPercentTH1 < heightImgRealPercentTH1) ? weightImgRealPercentTH1 : heightImgRealPercentTH1) / 100f;
					}
					else if (heightImgRealPercentTH1 > 0f)
					{
						imgTotalWidth = instance.Width * widthImagePercent * heightImgRealPercentTH1 / 100f;
					}
					else if (weightImgRealPercentTH1 > 0f)
					{
						imgTotalWidth = instance.Width * widthImagePercent * weightImgRealPercentTH1 / 100f;
					}
					LogSystem.Info("2__" + LogUtil.TraceData(LogUtil.GetMemberName(() => weightImgRealPercentTH1), weightImgRealPercentTH1) + LogUtil.TraceData(LogUtil.GetMemberName(() => heightImgRealPercentTH1), heightImgRealPercentTH1) + LogUtil.TraceData(LogUtil.GetMemberName(() => widthRectangle), widthRectangle) + LogUtil.TraceData(LogUtil.GetMemberName(() => heightRectangle), heightRectangle) + LogUtil.TraceData(LogUtil.GetMemberName(() => imgTotalWidth), imgTotalWidth) + LogUtil.TraceData(LogUtil.GetMemberName(() => heightRecModPlus), heightRecModPlus) + LogUtil.TraceData("instance.Height", instance.Height) + LogUtil.TraceData("instance.Width", instance.Width) + LogUtil.TraceData(LogUtil.GetMemberName(() => plusH), plusH));
				}
				else
				{
					float newHeightImagePercent = ((heightRecModPlus > 0f) ? heightRecModPlus : heightRectangle) / instance.Height;
					imgTotalWidth = instance.Width * widthImagePercent * newHeightImagePercent / 100f;
					LogSystem.Info("3__" + LogUtil.TraceData(LogUtil.GetMemberName(() => newHeightImagePercent), newHeightImagePercent) + LogUtil.TraceData(LogUtil.GetMemberName(() => widthRectangle), widthRectangle) + LogUtil.TraceData(LogUtil.GetMemberName(() => heightRectangle), heightRectangle) + LogUtil.TraceData(LogUtil.GetMemberName(() => imgTotalWidth), imgTotalWidth) + LogUtil.TraceData(LogUtil.GetMemberName(() => heightRecModPlus), heightRecModPlus) + LogUtil.TraceData("instance.Height", instance.Height) + LogUtil.TraceData("instance.Width", instance.Width) + LogUtil.TraceData(LogUtil.GetMemberName(() => plusH), plusH));
				}
			}
			else
			{
				imgTotalWidth = widthRectangle * widthImagePercent / 100f;
				LogSystem.Info("4__" + LogUtil.TraceData(LogUtil.GetMemberName(() => widthRectangle), widthRectangle) + LogUtil.TraceData(LogUtil.GetMemberName(() => heightRectangle), heightRectangle) + LogUtil.TraceData(LogUtil.GetMemberName(() => imgTotalWidth), imgTotalWidth) + LogUtil.TraceData("instance.Height", instance.Height) + LogUtil.TraceData("instance.Width", instance.Width) + LogUtil.TraceData(LogUtil.GetMemberName(() => widthImagePercent), widthImagePercent));
			}
			float num2 = widthRectangle;
			float num3 = ((imgTotalWidth > instance.Width) ? instance.Width : imgTotalWidth);
			if (SignaltureImageWidth > 0f)
			{
				num2 = SignaltureImageWidth;
			}
			else if (widthRectangle >= 100f)
			{
				num2 = 100f;
				if (num3 < 140f)
				{
					float num4 = 0f;
					num4 = num2;
					num2 = num3;
					num3 = num4 * 2f;
				}
			}
			else
			{
				num2 = widthRectangle;
				if (num3 < widthRectangle)
				{
					float num5 = 0f;
					num5 = num2;
					num2 = num3;
					num3 = num5;
				}
			}
			return 100f * (num2 / num3);
		}
	}
}
