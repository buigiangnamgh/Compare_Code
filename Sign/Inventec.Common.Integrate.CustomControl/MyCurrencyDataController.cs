using DevExpress.Data;
using DevExpress.XtraEditors.DXErrorProvider;

namespace Inventec.Common.Integrate.CustomControl
{
	internal class MyCurrencyDataController : CurrencyDataController
	{
		private readonly MyGridView _view;

		public MyCurrencyDataController(MyGridView view)
		{
			_view = view;
		}

		public override ErrorInfo GetErrorInfo(int controllerRow)
		{
			ErrorInfo errorInfo = base.GetErrorInfo(controllerRow);
			_view.FillRowError(controllerRow, errorInfo);
			return errorInfo;
		}

		public override ErrorInfo GetErrorInfo(int controllerRow, int column)
		{
			ErrorInfo errorInfo = base.GetErrorInfo(controllerRow, column);
			if (column < 0 || column >= base.Columns.Count)
			{
				return errorInfo;
			}
			DataColumnInfo dataColumnInfo = base.Columns[column];
			_view.FillRowColumnError(controllerRow, dataColumnInfo.Name, errorInfo);
			return errorInfo;
		}
	}
}
