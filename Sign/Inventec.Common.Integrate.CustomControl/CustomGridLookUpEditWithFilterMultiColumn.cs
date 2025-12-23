using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace Inventec.Common.Integrate.CustomControl
{
	internal class CustomGridLookUpEditWithFilterMultiColumn : GridLookUpEdit
	{
		public override string EditorTypeName
		{
			get
			{
				return "CustomGridLookUpEditWithFilterMultiColumn";
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new RepositoryItemCustomGridLookUpEdit Properties
		{
			get
			{
				return base.Properties as RepositoryItemCustomGridLookUpEdit;
			}
		}

		protected override bool IsAutoComplete
		{
			get
			{
				return true;
			}
		}

		static CustomGridLookUpEditWithFilterMultiColumn()
		{
			RepositoryItemCustomGridLookUpEdit.RegisterCustomGridLookUpEdit();
		}

		protected override PopupBaseForm CreatePopupForm()
		{
			return new MyGridLookUpPopupForm(this);
		}

		public override bool IsNeededKey(KeyEventArgs e)
		{
			return Properties.IsNeededKey(e.KeyData);
		}
	}
}
