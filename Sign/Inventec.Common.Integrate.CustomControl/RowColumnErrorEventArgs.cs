using DevExpress.XtraEditors.DXErrorProvider;

namespace Inventec.Common.Integrate.CustomControl
{
	public class RowColumnErrorEventArgs : RowErrorEventArgs
	{
		private readonly string _columnName;

		public string ColumnName
		{
			get
			{
				return _columnName;
			}
		}

		public RowColumnErrorEventArgs(ErrorInfo info, int rowHandle, string columnName)
			: base(info, rowHandle)
		{
			_columnName = columnName;
		}
	}
}
