using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Inventec.Common.Integrate.CustomControl
{
	internal class CustomGridPainter : GridPainter
	{
		public new virtual CustomGridViewWithFilterMultiColumn View
		{
			get
			{
				return (CustomGridViewWithFilterMultiColumn)base.View;
			}
		}

		public CustomGridPainter(GridView view)
			: base(view)
		{
		}

		protected override void DrawRowCell(GridViewDrawArgs e, GridCellInfo cell)
		{
			cell.ViewInfo.MatchedStringUseContains = true;
			cell.ViewInfo.MatchedString = View.GetExtraFilterText;
			cell.State = GridRowCellState.Dirty;
			e.ViewInfo.UpdateCellAppearance(cell);
			base.DrawRowCell(e, cell);
		}
	}
}
