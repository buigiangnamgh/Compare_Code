using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using Inventec.Common.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary.Integrate
{
	public class MessageManager
	{
		public const int DefaultFontSize = 12;

		public static int AutoFormDelay = 1000;

		public static void Show(CommonParam param, bool? success)
		{
			try
			{
				if (success.HasValue)
				{
					SetResultParam(param, success.Value);
				}
				string messageAlert = GetMessageAlert(param);
				if (!string.IsNullOrEmpty(messageAlert))
				{
					XtraMessageBox.Show(messageAlert, MessageUitl.GetMessage("ThongBao"), DefaultBoolean.True);
				}
			}
			catch (Exception)
			{
			}
		}

		public static void Show(string message)
		{
			try
			{
				XtraMessageBox.Show(message, MessageUitl.GetMessage("ThongBao"), DefaultBoolean.True);
			}
			catch (Exception)
			{
			}
		}

		public static void Show(Form owner, CommonParam param, bool? success)
		{
			try
			{
				bool flag = false;
				if (success.HasValue)
				{
					if (success.Value && (param.Messages == null || param.Messages.Count == 0))
					{
						flag = true;
					}
					SetResultParam(param, success.Value);
				}
				string messageAlert = GetMessageAlert(param);
				if (flag)
				{
					if (!string.IsNullOrEmpty(messageAlert))
					{
						ShowAlert(owner, "", messageAlert);
					}
				}
				else if (!string.IsNullOrEmpty(messageAlert))
				{
					XtraMessageBox.Show(messageAlert, MessageUitl.GetMessage("ThongBao"), DefaultBoolean.True);
				}
			}
			catch (Exception)
			{
			}
		}

		public static void Show(Form owner, CommonParam param, bool? success, AlertFormLocation formLocation)
		{
			try
			{
				bool flag = false;
				if (success.HasValue)
				{
					if (success.Value && (param.Messages == null || param.Messages.Count == 0))
					{
						flag = true;
					}
					SetResultParam(param, success.Value);
				}
				string messageAlert = GetMessageAlert(param);
				if (flag)
				{
					if (!string.IsNullOrEmpty(messageAlert))
					{
						ShowAlert(owner, "", messageAlert, AutoFormDelay, formLocation);
					}
				}
				else if (!string.IsNullOrEmpty(messageAlert))
				{
					XtraMessageBox.Show(messageAlert, MessageUitl.GetMessage("ThongBao"), DefaultBoolean.True);
				}
			}
			catch (Exception)
			{
			}
		}

		public static void ShowAlert(Form owner, CommonParam param, bool? success)
		{
			try
			{
				if (success.HasValue)
				{
					SetResultParam(param, success.Value);
				}
				string messageAlert = GetMessageAlert(param);
				if (!string.IsNullOrEmpty(messageAlert))
				{
					ShowAlert(owner, "", messageAlert, AutoFormDelay);
				}
			}
			catch (Exception)
			{
			}
		}

		public static void ShowAlert(Form owner, string caption, string message)
		{
			try
			{
				ShowAlert(owner, caption, message, AutoFormDelay);
			}
			catch (Exception)
			{
			}
		}

		public static void ShowAlert(Form owner, CommonParam param, bool? success, int autoFormDelay)
		{
			try
			{
				if (success.HasValue)
				{
					SetResultParam(param, success.Value);
				}
				string messageAlert = GetMessageAlert(param);
				if (!string.IsNullOrEmpty(messageAlert))
				{
					AlertControl alertControl = new AlertControl();
					alertControl.ShowPinButton = true;
					alertControl.ShowCloseButton = true;
					alertControl.AppearanceCaption.TextOptions.HAlignment = HorzAlignment.Center;
					alertControl.AppearanceText.TextOptions.HAlignment = HorzAlignment.Center;
					alertControl.AppearanceText.TextOptions.WordWrap = WordWrap.Wrap;
					alertControl.AutoFormDelay = autoFormDelay;
					alertControl.FormLocation = AlertFormLocation.BottomLeft;
					alertControl.AppearanceCaption.ForeColor = Color.Green;
					alertControl.AppearanceCaption.Font = new Font(alertControl.AppearanceCaption.Font.FontFamily, 12f, FontStyle.Bold);
					alertControl.Show(owner, messageAlert, "");
				}
			}
			catch (Exception)
			{
			}
		}

		public static void ShowAlert(Form owner, string caption, string message, int autoFormDelay)
		{
			try
			{
				ShowAlert(owner, caption, message, autoFormDelay, AlertFormLocation.TopRight);
			}
			catch (Exception)
			{
			}
		}

		public static void ShowAlert(Form owner, string caption, string message, int autoFormDelay, AlertFormLocation formLocation)
		{
			bool value = true;
			try
			{
				ShowAlert(owner, caption, message, autoFormDelay, formLocation, 12, Color.Green, FontStyle.Bold, value);
			}
			catch (Exception)
			{
			}
		}

		public static void ShowAlert(Form owner, string caption, string message, int autoFormDelay, AlertFormLocation formLocation, int fontSize, Color color, FontStyle fontStyle, bool? isShowScreenCenter)
		{
			try
			{
				AlertControl alertControl = new AlertControl();
				alertControl.ShowPinButton = true;
				alertControl.ShowCloseButton = true;
				alertControl.AppearanceCaption.TextOptions.HAlignment = HorzAlignment.Center;
				alertControl.AppearanceText.TextOptions.HAlignment = HorzAlignment.Center;
				alertControl.AppearanceText.TextOptions.WordWrap = WordWrap.Wrap;
				alertControl.AppearanceCaption.ForeColor = color;
				alertControl.AppearanceCaption.Font = new Font(alertControl.AppearanceCaption.Font.FontFamily, fontSize, fontStyle);
				alertControl.AutoFormDelay = autoFormDelay;
				if (isShowScreenCenter.Value)
				{
					alertControl.BeforeFormShow += beforeShowAlert;
				}
				else
				{
					bool flag = true;
					alertControl.FormLocation = formLocation;
				}
				alertControl.Show(owner, message, "");
			}
			catch (Exception)
			{
			}
		}

		private static void ShowAlert(Form owner, string caption, string message, int autoFormDelay, AlertFormControlBoxPosition formPosition, int fontSize, Color color, FontStyle fontStyle)
		{
			try
			{
				AlertControl alertControl = new AlertControl();
				alertControl.ShowPinButton = true;
				alertControl.ShowCloseButton = true;
				alertControl.AppearanceCaption.TextOptions.HAlignment = HorzAlignment.Center;
				alertControl.AppearanceText.TextOptions.HAlignment = HorzAlignment.Center;
				alertControl.AppearanceText.TextOptions.WordWrap = WordWrap.Wrap;
				alertControl.AppearanceCaption.ForeColor = color;
				alertControl.AppearanceCaption.Font = new Font(alertControl.AppearanceCaption.Font.FontFamily, fontSize, fontStyle);
				alertControl.AutoFormDelay = autoFormDelay;
				alertControl.ControlBoxPosition = formPosition;
				alertControl.Show(owner, message, "");
			}
			catch (Exception)
			{
			}
		}

		private static void beforeShowAlert(object sender, AlertFormEventArgs e)
		{
			AlertControl alertControl = sender as AlertControl;
			Point location = e.Location;
			Rectangle workingArea = Screen.GetWorkingArea(Screen.PrimaryScreen.Bounds);
			location.X = (workingArea.Width - e.AlertForm.Width) / 2;
			location.Y = (workingArea.Height - e.AlertForm.Height) / 2;
			e.Location = location;
		}

		private static string GetMessageAlert(CommonParam param)
		{
			string text = "";
			try
			{
				if (param.Messages != null && param.Messages.Count > 0)
				{
					text += param.GetMessage();
				}
				if (param.BugCodes != null && param.BugCodes.Count > 0)
				{
					text = text + "\r\nMã sự cố: " + param.GetBugCode();
				}
			}
			catch (Exception)
			{
			}
			return text;
		}

		private static void SetResultParam(CommonParam param, bool success)
		{
			try
			{
				if (success)
				{
					param.Messages.Insert(0, "Xử lý thành công");
				}
				else
				{
					param.Messages.Insert(0, "Xử lý thất bại");
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
