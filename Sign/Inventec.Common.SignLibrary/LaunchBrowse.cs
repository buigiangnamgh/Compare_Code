using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Inventec.Common.Logging;
using Microsoft.Win32;

namespace Inventec.Common.SignLibrary
{
	internal class LaunchBrowse
	{
		internal LaunchBrowse()
		{
		}

		internal void Launch(string urlSite)
		{
			try
			{
				string browserPath = GetPathToDefaultBrowser();
				Process.Start(browserPath, urlSite);
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => browserPath), browserPath));
			}
			catch
			{
				Process.Start("cmd", "/C start " + urlSite);
			}
		}

		private string GetPathToDefaultBrowser()
		{
			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\http\\UserChoice", false))
			{
				string text = registryKey.GetValue("ProgId").ToString();
				using (RegistryKey registryKey2 = Registry.ClassesRoot.OpenSubKey(text + "\\shell\\open\\command", false))
				{
					string input = (string)registryKey2.GetValue("");
					Regex regex = new Regex("(?<=\").*?(?=\")");
					Match match = regex.Match(input);
					return match.Success ? match.Value : "";
				}
			}
		}

		internal void LaunchChrome(string apiDomain)
		{
			string text = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
			bool flag = File.Exists(text);
			if (!flag)
			{
				text = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
				flag = File.Exists(text);
			}
			if (!flag)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Chrome browser|chrome.exe";
				openFileDialog.CheckPathExists = true;
				openFileDialog.Title = "Hãy chọn trình duyệt chrome";
				using (OpenFileDialog openFileDialog2 = openFileDialog)
				{
					if (openFileDialog2.ShowDialog() == DialogResult.OK)
					{
						text = openFileDialog2.FileName;
						flag = true;
					}
				}
			}
			if (flag)
			{
				Process.Start(text, apiDomain);
			}
		}

		private string GetSecretKey(string maThe)
		{
			string text = DateTime.Now.Ticks.ToString();
			SHA256 sHA = SHA256.Create();
			string text2 = "VietSens_Siten_2019";
			string text3 = new Random().Next().ToString();
			return text + "." + text3 + "." + Convert.ToBase64String(sHA.ComputeHash(Encoding.UTF8.GetBytes(maThe + "." + text + "." + text3 + "." + text2)));
		}
	}
}
