using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.RegisterV2
{
	[KeyboardAction("PatientNew", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "PatientNew", KeyStroke = (XKeys.R | XKeys.Control))]
	[KeyboardAction("Save", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "Save", KeyStroke = (XKeys.S | XKeys.Control))]
	[KeyboardAction("SaveAndPrint", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "SaveAndPrint", KeyStroke = (XKeys.I | XKeys.Control))]
	[KeyboardAction("PrintKeyboard", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "PrintKeyboard", KeyStroke = (XKeys.P | XKeys.Control))]
	[KeyboardAction("AssignService", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "AssignService", KeyStroke = (XKeys.D | XKeys.Control))]
	[KeyboardAction("BillKeyboard", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "BillKeyboard", KeyStroke = (XKeys.B | XKeys.Control))]
	[KeyboardAction("Deposit", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "Deposit", KeyStroke = (XKeys.T | XKeys.Control))]
	[KeyboardAction("DepositRequest", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "DepositRequest", KeyStroke = (XKeys.T | XKeys.Control | XKeys.Shift))]
	[KeyboardAction("InBed", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "InBed", KeyStroke = (XKeys.G | XKeys.Control))]
	[KeyboardAction("New", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "New", KeyStroke = (XKeys.N | XKeys.Control))]
	[KeyboardAction("ClickF2", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF2", KeyStroke = XKeys.F2)]
	[KeyboardAction("ClickF3", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF3", KeyStroke = XKeys.F3)]
	[KeyboardAction("ClickF4", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF4", KeyStroke = XKeys.F4)]
	[KeyboardAction("ClickF5", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF5", KeyStroke = XKeys.F5)]
	[KeyboardAction("ClickF6", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF6", KeyStroke = XKeys.F6)]
	[KeyboardAction("ClickF7", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF7", KeyStroke = XKeys.F7)]
	[KeyboardAction("ClickF8", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF8", KeyStroke = XKeys.F8)]
	[KeyboardAction("ClickF9", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF9", KeyStroke = XKeys.F9)]
	[KeyboardAction("ClickF10", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF10", KeyStroke = XKeys.F10)]
	[KeyboardAction("ClickF11", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF11", KeyStroke = XKeys.F11)]
	[ExtensionOf(typeof(DesktopToolExtensionPoint))]
	public sealed class KeyboardWorker : Tool<IDesktopToolContext>
	{
		public override IActionSet Actions
		{
			get
			{
				return base.Actions;
			}
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
