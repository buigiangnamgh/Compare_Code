using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace Inventec.Common.Integrate.CustomControl
{
	internal class MyGridLookUpPopupForm : PopupGridLookUpEditForm
	{
		public MyGridLookUpPopupForm(GridLookUpEdit ownerEdit)
			: base(ownerEdit)
		{
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Tab)
			{
				base.OwnerEdit.EditValue = QueryResultValue();
				base.OwnerEdit.SendKey(e);
			}
			base.OnKeyDown(e);
		}
	}
}
