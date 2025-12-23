using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace Inventec.Common.Integrate.CustomControl
{
	[UserRepositoryItem("RegisterCustomGridLookUpEdit")]
	internal class RepositoryItemCustomGridLookUpEdit : RepositoryItemGridLookUpEdit
	{
		public const string CustomGridLookUpEditName = "CustomGridLookUpEditWithFilterMultiColumn";

		[Browsable(false)]
		public override TextEditStyles TextEditStyle
		{
			get
			{
				return base.TextEditStyle;
			}
			set
			{
				base.TextEditStyle = value;
			}
		}

		public override string EditorTypeName
		{
			get
			{
				return "CustomGridLookUpEditWithFilterMultiColumn";
			}
		}

		static RepositoryItemCustomGridLookUpEdit()
		{
			RegisterCustomGridLookUpEdit();
		}

		public RepositoryItemCustomGridLookUpEdit()
		{
			TextEditStyle = TextEditStyles.Standard;
			base.AutoComplete = false;
		}

		public static void RegisterCustomGridLookUpEdit()
		{
			EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo("CustomGridLookUpEditWithFilterMultiColumn", typeof(CustomGridLookUpEditWithFilterMultiColumn), typeof(RepositoryItemCustomGridLookUpEdit), typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
		}

		protected override GridView CreateViewInstance()
		{
			return new CustomGridViewWithFilterMultiColumn();
		}

		protected override GridControl CreateGrid()
		{
			return new CustomGridControlWithFilterMultiColumn();
		}
	}
}
