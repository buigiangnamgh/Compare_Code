using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;

namespace Inventec.Common.Integrate.CustomControl
{
	internal class CustomGridControlWithFilterMultiColumn : GridControl
	{
		protected override void RegisterAvailableViewsCore(InfoCollection collection)
		{
			base.RegisterAvailableViewsCore(collection);
			collection.Add(new CustomGridInfoRegistrator());
		}

		protected override BaseView CreateDefaultView()
		{
			return CreateView("CustomGridViewWithFilterMultiColumn");
		}
	}
}
