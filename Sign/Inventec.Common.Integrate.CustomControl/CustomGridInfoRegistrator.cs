using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Inventec.Common.Integrate.CustomControl
{
	internal class CustomGridInfoRegistrator : GridInfoRegistrator
	{
		public override string ViewName
		{
			get
			{
				return "CustomGridViewWithFilterMultiColumn";
			}
		}

		public override BaseViewPainter CreatePainter(BaseView view)
		{
			return new CustomGridPainter(view as GridView);
		}

		public override BaseView CreateView(GridControl grid)
		{
			CustomGridViewWithFilterMultiColumn customGridViewWithFilterMultiColumn = new CustomGridViewWithFilterMultiColumn();
			customGridViewWithFilterMultiColumn.SetGridControlAccessMetod(grid);
			return customGridViewWithFilterMultiColumn;
		}
	}
}
