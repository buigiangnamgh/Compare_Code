using System;
using System.Collections.Generic;
using System.IO;
using Inventec.Common.Logging;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Inventec.Common.SignLibrary
{
	public class PDFParser
	{
		private static int _numberOfCharsToKeep = 15;

		public List<string> ReadPdfFile(string fileName, string searthText)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			List<string> list = new List<string>();
			if (File.Exists(fileName))
			{
				PdfReader val = new PdfReader(fileName);
				for (int i = 1; i <= val.NumberOfPages; i++)
				{
					ITextExtractionStrategy val2 = (ITextExtractionStrategy)new SimpleTextExtractionStrategy();
					string textFromPage = PdfTextExtractor.GetTextFromPage(val, i, val2);
					string text = "< S I N G L E _ K E Y _ _ C O M M E N T _ S I G N _ _";
					if (textFromPage.Contains(searthText))
					{
						string[] array = textFromPage.Split(' ');
						if (array == null || array.Length == 0)
						{
							continue;
						}
						string[] array2 = array;
						foreach (string text2 in array2)
						{
							if (!text2.Contains("<SINGLE_KEY__COMMENT_SIGN__"))
							{
								continue;
							}
							try
							{
								string text3 = text2.Substring(text2.IndexOf("<SINGLE_KEY__COMMENT_SIGN__"), text2.IndexOf(">") - text2.IndexOf("<SINGLE_KEY__COMMENT_SIGN__") + 1);
								if (!string.IsNullOrEmpty(text3))
								{
									list.Add(text3);
								}
							}
							catch (Exception ex)
							{
								LogSystem.Warn("Doc file pdf lay danh sach comment key xac dinh toa do vi tri ky tu dong that bai____" + text2, ex);
							}
						}
					}
					else
					{
						if (!textFromPage.Replace(" ", "").Contains(searthText))
						{
							continue;
						}
						string[] array3 = textFromPage.Replace(" ", "").Split('\n');
						if (array3 == null || array3.Length == 0)
						{
							continue;
						}
						string[] array4 = array3;
						foreach (string text4 in array4)
						{
							if (!text4.Contains(searthText))
							{
								continue;
							}
							try
							{
								string text5 = text4.Replace(searthText, "").Replace(" ", "").Replace(">", "")
									.Trim();
								if (!string.IsNullOrEmpty(text5) && text5.StartsWith("$"))
								{
									list.Add("<SINGLE_KEY__COMMENT_SIGN__" + text5 + ">");
								}
							}
							catch (Exception ex2)
							{
								LogSystem.Warn("Doc file pdf lay danh sach comment key xac dinh toa do vi tri ky tu dong that bai____" + text4, ex2);
							}
						}
					}
				}
				val.Close();
			}
			return list;
		}

		public List<string> ExtractText(string inFileName)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Expected O, but got Unknown
			List<string> list = new List<string>();
			try
			{
				PdfReader val = new PdfReader(inFileName);
				Console.Write("Processing: ");
				int num = 68;
				float num2 = (float)num / (float)val.NumberOfPages;
				int num3 = 0;
				float num4 = 0f;
				for (int i = 1; i <= val.NumberOfPages; i++)
				{
					list.Add(ExtractTextFromPDFBytes(val.GetPageContent(i)) + " ");
					if (num2 >= 1f)
					{
						for (int j = 0; j < (int)num2; j++)
						{
							Console.Write("#");
							num3++;
						}
						continue;
					}
					num4 += num2;
					if (num4 >= 1f)
					{
						for (int k = 0; k < (int)num4; k++)
						{
							Console.Write("#");
							num3++;
						}
						num4 = 0f;
					}
				}
				if (num3 < num)
				{
					for (int l = 0; l < num - num3; l++)
					{
						Console.Write("#");
					}
				}
				val.Close();
				return list;
			}
			catch
			{
				return null;
			}
			finally
			{
			}
		}

		private string ExtractTextFromPDFBytes(byte[] input)
		{
			if (input == null || input.Length == 0)
			{
				return "";
			}
			try
			{
				string text = "";
				bool flag = false;
				bool flag2 = false;
				int num = 0;
				char[] array = new char[_numberOfCharsToKeep];
				for (int i = 0; i < _numberOfCharsToKeep; i++)
				{
					array[i] = ' ';
				}
				for (int j = 0; j < input.Length; j++)
				{
					char c = (char)input[j];
					if (flag)
					{
						if (num == 0)
						{
							if (CheckToken(new string[2] { "TD", "Td" }, array))
							{
								text += "\n\r";
							}
							else if (CheckToken(new string[3] { "'", "T*", "\"" }, array))
							{
								text += "\n";
							}
							else if (CheckToken(new string[1] { "Tj" }, array))
							{
								text += " ";
							}
						}
						if (num == 0 && CheckToken(new string[1] { "ET" }, array))
						{
							flag = false;
							text += " ";
						}
						else if (c == '(' && num == 0 && !flag2)
						{
							num = 1;
						}
						else if (c == ')' && num == 1 && !flag2)
						{
							num = 0;
						}
						else if (num == 1)
						{
							if (c == '\\' && !flag2)
							{
								flag2 = true;
							}
							else
							{
								if ((c >= ' ' && c <= '~') || (c >= '\u0080' && c < 'ÿ'))
								{
									text += c;
								}
								flag2 = false;
							}
						}
					}
					for (int k = 0; k < _numberOfCharsToKeep - 1; k++)
					{
						array[k] = array[k + 1];
					}
					array[_numberOfCharsToKeep - 1] = c;
					if (!flag && CheckToken(new string[1] { "BT" }, array))
					{
						flag = true;
					}
				}
				return text;
			}
			catch
			{
				return "";
			}
		}

		private bool CheckToken(string[] tokens, char[] recent)
		{
			foreach (string text in tokens)
			{
				if (recent[_numberOfCharsToKeep - 3] == text[0] && recent[_numberOfCharsToKeep - 2] == text[1] && (recent[_numberOfCharsToKeep - 1] == ' ' || recent[_numberOfCharsToKeep - 1] == '\r' || recent[_numberOfCharsToKeep - 1] == '\n') && (recent[_numberOfCharsToKeep - 4] == ' ' || recent[_numberOfCharsToKeep - 4] == '\r' || recent[_numberOfCharsToKeep - 4] == '\n'))
				{
					return true;
				}
			}
			return false;
		}
	}
}
