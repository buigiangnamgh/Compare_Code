using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using HIS.Desktop.Utilities.Extensions;

namespace MultiColumnFilterTest
{
	[UserRepositoryItem("RegisterCustomGridLookUpEditNew")]
	public class RepositoryItemCustomGridLookUpEditNew : RepositoryItemGridLookUpEdit
	{
		public const string CustomGridLookUpEditNewName = "CustomGridLookUpEditNew";

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
				return "CustomGridLookUpEditNew";
			}
		}

		static RepositoryItemCustomGridLookUpEditNew()
		{
			RegisterCustomGridLookUpEditNew();
		}

		public RepositoryItemCustomGridLookUpEditNew()
		{
			TextEditStyle = TextEditStyles.Standard;
			base.AutoComplete = false;
		}

		public static void RegisterCustomGridLookUpEditNew()
		{
			EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo("CustomGridLookUpEditNew", typeof(CustomGridLookUpEditNew), typeof(RepositoryItemCustomGridLookUpEditNew), typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
		}

		protected override GridView CreateViewInstance()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			return (GridView)new CustomGridView();
		}

		protected override GridControl CreateGrid()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			return (GridControl)new CustomGridControl();
		}
	}
}
