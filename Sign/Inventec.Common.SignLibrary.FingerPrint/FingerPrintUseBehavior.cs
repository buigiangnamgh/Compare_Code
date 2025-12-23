using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Windows.Forms;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Integrate;

namespace Inventec.Common.SignLibrary.FingerPrint
{
	internal class FingerPrintUseBehavior : BusinessBase, IFingerPrint
	{
		private InputADO entity;

		private byte[] SignPadImageData;

		private string deviceSignPadName;

		private Timer timerCheckFileSign;

		internal FingerPrintUseBehavior(CommonParam param, InputADO inputADOWorking)
		{
			entity = inputADOWorking;
			deviceSignPadName = ((inputADOWorking != null) ? inputADOWorking.DeviceSignPadName : null);
		}

		byte[] IFingerPrint.Run()
		{
			try
			{
				if (!IsProcessOpen("Inventec.FingerPrintManager"))
				{
					string path = Path.Combine(Path.Combine(Application.StartupPath, "temp"), DateTime.Now.ToString("ddMMyyyy"), "STFingerPrintFile");
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					string[] files = Directory.GetFiles(directoryInfo.FullName, "*");
					if (files != null && files.Length != 0)
					{
						try
						{
							directoryInfo.Delete(true);
						}
						catch (Exception ex)
						{
							LogSystem.Error(ex);
						}
					}
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.FileName = Application.StartupPath + "\\Integrate\\Inventec.FingerPrintManager\\Inventec.FingerPrintManager.exe";
					if (!string.IsNullOrEmpty(deviceSignPadName))
					{
						processStartInfo.Arguments = deviceSignPadName;
					}
					Process.Start(processStartInfo);
				}
				while (IsProcessOpen("Inventec.FingerPrintManager"))
				{
				}
				LogSystem.Debug("IFingerPrint.Run.2");
				string path2 = Path.Combine(Application.StartupPath + "\\Integrate\\Inventec.FingerPrintManager\\temp", DateTime.Now.ToString("ddMMyyyy"), "STFingerPrintFile");
				DirectoryInfo dicInfo = new DirectoryInfo(path2);
				string[] fileImage = Directory.GetFiles(dicInfo.FullName, "*");
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<string[]>((Expression<Func<string[]>>)(() => fileImage)), (object)fileImage) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => dicInfo.FullName)), (object)dicInfo.FullName));
				if (fileImage != null && fileImage.Length != 0)
				{
					SignPadImageData = Utils.FileToByte(fileImage[0]);
					LogSystem.Debug("IFingerPrint.Run.3");
				}
				else
				{
					LogSystem.Debug("IFingerPrint.Run.4");
				}
				return SignPadImageData;
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
				base.param.HasException = true;
				return null;
			}
		}

		private void CheckImageFile(object sender, EventArgs e)
		{
			try
			{
				string path = Path.Combine(Path.Combine(Application.StartupPath, "temp"), DateTime.Now.ToString("ddMMyyyy"), "STFingerPrintFile");
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				string[] files = Directory.GetFiles(directoryInfo.FullName, "*");
				if (files != null && files.Length != 0)
				{
					SignPadImageData = Utils.FileToByte(files[0]);
					timerCheckFileSign.Enabled = false;
					timerCheckFileSign.Stop();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		internal bool IsProcessOpen(string name)
		{
			try
			{
				Process[] processes = Process.GetProcesses();
				foreach (Process process in processes)
				{
					if (process.ProcessName == name || process.ProcessName == string.Format("{0}.exe", name) || process.ProcessName == string.Format("{0} (32 bit)", name) || process.ProcessName == string.Format("{0}.exe (32 bit)", name))
					{
						return true;
					}
				}
				return false;
			}
			catch (Exception ex)
			{
				LogSystem.Debug(string.Format("Xảy ra lỗi khi kiểm tra ứng dụng {0}.", name), ex);
			}
			return false;
		}

		private void ActSelectDevice(string deviceName)
		{
			try
			{
				deviceSignPadName = deviceName;
				if (entity != null && entity.ActSelectDevice != null)
				{
					entity.ActSelectDevice(deviceName);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void GetSignImageFIle(Bitmap bmpSignImage)
		{
			try
			{
				if (bmpSignImage != null)
				{
					SignPadImageData = Utils.ImageToByte(bmpSignImage);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
