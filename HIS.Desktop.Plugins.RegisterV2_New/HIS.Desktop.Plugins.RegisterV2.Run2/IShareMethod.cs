using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
	internal interface IShareMethod
	{
		void FocusShowpopup(LookUpEdit cboEditor, bool isSelectFirstRow);

		void InitComboCommon(Control cboEditor, object data, string valueMember, string displayMember, string displayMemberCode);
	}
}
