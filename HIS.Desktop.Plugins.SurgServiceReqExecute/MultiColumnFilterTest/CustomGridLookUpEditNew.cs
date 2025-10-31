using System.ComponentModel;
using DevExpress.XtraEditors;

namespace MultiColumnFilterTest
{
	public class CustomGridLookUpEditNew : GridLookUpEdit
	{
		public override string EditorTypeName
		{
			get
			{
				return "CustomGridLookUpEditNew";
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new RepositoryItemCustomGridLookUpEditNew Properties
		{
			get
			{
				return base.Properties as RepositoryItemCustomGridLookUpEditNew;
			}
		}

		static CustomGridLookUpEditNew()
		{
			RepositoryItemCustomGridLookUpEditNew.RegisterCustomGridLookUpEditNew();
		}
	}
}
