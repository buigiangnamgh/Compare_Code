using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Integrate;

namespace Inventec.Common.SignLibrary.SignBoard
{
	internal class SignBoardUseBehavior : BusinessBase, ISignBoard
	{
		private InputADO entity;

		private byte[] SignPadImageData;

		private string deviceSignPadName;

		private Timer timerCheckFileSign;

		internal SignBoardUseBehavior(CommonParam param, InputADO inputADOWorking)
		{
			entity = inputADOWorking;
			deviceSignPadName = ((inputADOWorking != null) ? inputADOWorking.DeviceSignPadName : null);
		}

		byte[] ISignBoard.Run()
		{
			try
			{
				if (!IsProcessOpen("Inventec.SignPadManager"))
				{
					string path = Path.Combine(Path.Combine(Application.StartupPath, "temp"), DateTime.Now.ToString("ddMMyyyy"), "STPadLibFile");
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
					processStartInfo.FileName = Application.StartupPath + "\\Inventec.SignPadManager.exe";
					Process.Start(processStartInfo);
				}
				while (IsProcessOpen("Inventec.SignPadManager"))
				{
				}
				LogSystem.Debug("ISignBoard.Run.2");
				string path2 = Path.Combine(Path.Combine(Application.StartupPath, "temp"), DateTime.Now.ToString("ddMMyyyy"), "STPadLibFile");
				DirectoryInfo dicInfo = new DirectoryInfo(path2);
				string[] fileImage = Directory.GetFiles(dicInfo.FullName, "*");
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => fileImage), fileImage) + LogUtil.TraceData(LogUtil.GetMemberName(() => dicInfo.FullName), dicInfo.FullName));
				if (fileImage != null && fileImage.Length != 0)
				{
					SignPadImageData = Utils.FileToByte(fileImage[0]);
					LogSystem.Debug("ISignBoard.Run.3");
				}
				else
				{
					LogSystem.Debug("ISignBoard.Run.4");
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
				string path = Path.Combine(Path.Combine(Application.StartupPath, "temp"), DateTime.Now.ToString("ddMMyyyy"), "STPadLibFile");
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
				Process[] array = processes;
				foreach (Process process in array)
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
