using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using Inventec.Common.Logging;

namespace Inventec.Common.Integrate
{
	public class WaitingManager
	{
		private static int pendingTime = int.Parse(ConfigurationManager.AppSettings.Get("Inventec.Desktop.Common.Message.PendingTime") ?? "2000");

		private static object IsLock = new object();

		private static SplashScreenManager CurrentSplashScreenManager;

		private static bool IsStop { get; set; }

		private static bool IsShow { get; set; }

		public static void Show()
		{
			try
			{
				if (CurrentSplashScreenManager != null)
				{
				}
				IsShow = true;
				IsStop = false;
				Thread thread = new Thread(CountTime);
				try
				{
					thread.Start();
				}
				catch (Exception)
				{
					thread.Abort();
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Debug(ex2);
			}
		}

		private static void CountTime()
		{
			try
			{
				Thread.Sleep(pendingTime);
				if (!IsStop && IsShow)
				{
					ShowWaitForm(null);
				}
				else
				{
					CloseForm();
				}
			}
			catch (Exception ex)
			{
				CloseForm();
				LogSystem.Error(ex);
			}
		}

		public static void Show(int frameCount)
		{
			try
			{
				CloseForm();
				ShowWaitForm(null);
			}
			catch (Exception ex)
			{
				LogSystem.Debug(ex);
			}
		}

		public static void Show(Form formParent)
		{
			try
			{
				CloseForm();
				ShowWaitForm(formParent);
			}
			catch (Exception ex)
			{
				CloseForm();
				LogSystem.Debug(ex);
			}
		}

		private static void ShowWaitForm(Form formParent)
		{
			try
			{
				if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
				{
					CloseForm();
				}
				if (CurrentSplashScreenManager == null)
				{
					CurrentSplashScreenManager = new SplashScreenManager(null, typeof(frmWaitForm), true, true, false);
				}
				lock (CurrentSplashScreenManager)
				{
					if (CurrentSplashScreenManager.IsSplashFormVisible)
					{
						CurrentSplashScreenManager.CloseWaitForm();
					}
					CurrentSplashScreenManager.ShowWaitForm();
					IsShow = true;
				}
			}
			catch (Exception ex)
			{
				CloseForm();
				LogSystem.Warn(ex);
			}
		}

		public static void Hide()
		{
			try
			{
				CloseForm();
				IsStop = true;
			}
			catch (Exception ex)
			{
				CloseForm();
				LogSystem.Debug(ex);
			}
		}

		public static void CloseForm()
		{
			try
			{
				if (CurrentSplashScreenManager != null && CurrentSplashScreenManager.IsSplashFormVisible)
				{
					CurrentSplashScreenManager.CloseWaitForm();
				}
				IsShow = false;
				Thread thread = new Thread(CheckSplashScreenManager);
				try
				{
					thread.Start();
				}
				catch (Exception)
				{
					thread.Abort();
				}
			}
			catch (Exception)
			{
				CloseFormByName("frmWaitForm");
			}
		}

		private static void CheckSplashScreenManager()
		{
			try
			{
				Thread.Sleep(pendingTime * 2);
				if (CurrentSplashScreenManager != null && CurrentSplashScreenManager.IsSplashFormVisible)
				{
					CurrentSplashScreenManager.CloseWaitForm();
				}
			}
			catch (Exception ex)
			{
				if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
				{
					SplashScreenManager.CloseDefaultSplashScreen();
				}
				LogSystem.Error(ex);
			}
		}

		private static void CloseFormByName(string formName)
		{
			try
			{
				foreach (Form openForm in Application.OpenForms)
				{
					if (openForm.Name == formName)
					{
						openForm.Close();
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
