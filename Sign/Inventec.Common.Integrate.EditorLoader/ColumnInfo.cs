namespace Inventec.Common.Integrate.EditorLoader
{
	public class ColumnInfo
	{
		public enum FormatType
		{
			None,
			Numeric,
			DateTime,
			Custom
		}

		public enum HorzAlignment
		{
			Default,
			Near,
			Center,
			Far
		}

		public bool visible = true;

		public bool FixedWidth = true;

		public string fieldName { get; set; }

		public string caption { get; set; }

		public int width { get; set; }

		public FormatType formatType { get; set; }

		public HorzAlignment horzAlignment { get; set; }

		public string formatString { get; set; }

		public int VisibleIndex { get; set; }

		public ColumnInfo()
		{
		}

		public ColumnInfo(string _fieldName, string _caption, int _width, int _VisibleIndex)
			: this(_fieldName, _caption, _width, _VisibleIndex, false)
		{
		}

		public ColumnInfo(string _fieldName, string _caption, int _width, int _VisibleIndex, bool _FixedWidth)
		{
			fieldName = _fieldName;
			caption = _caption;
			width = _width;
			VisibleIndex = _VisibleIndex;
			FixedWidth = _FixedWidth;
		}
	}
}
