using System.Collections.Generic;

namespace Inventec.Common.Integrate.EditorLoader
{
	public class ControlEditorADO
	{
		internal const int DEFAULT__POPUP_WIDTH = 300;

		internal const int DEFAULT__COLUMN_WIDTH = 100;

		internal const int DEFAULT__DROP_DOWN_ROW = 10;

		public string DisplayMember { get; set; }

		public string ValueMember { get; set; }

		public bool ShowHeader { get; set; }

		public bool ImmediatePopup { get; set; }

		public int DropDownRows { get; set; }

		public int PopupWidth { get; set; }

		public List<ColumnInfo> ColumnInfos { get; set; }

		public ControlEditorADO()
		{
		}

		public ControlEditorADO(string _DisplayMember, string _ValueMember, List<ColumnInfo> _ColumnInfos)
			: this(_DisplayMember, _ValueMember, _ColumnInfos, false, 300, 10)
		{
		}

		public ControlEditorADO(string _DisplayMember, string _ValueMember, List<ColumnInfo> _ColumnInfos, bool _ShowHeader)
			: this(_DisplayMember, _ValueMember, _ColumnInfos, _ShowHeader, 300, 10)
		{
		}

		public ControlEditorADO(string _DisplayMember, string _ValueMember, List<ColumnInfo> _ColumnInfos, bool _ShowHeader, int _PopupWidth)
			: this(_DisplayMember, _ValueMember, _ColumnInfos, _ShowHeader, _PopupWidth, 10)
		{
		}

		public ControlEditorADO(string _DisplayMember, string _ValueMember, List<ColumnInfo> _ColumnInfos, bool _ShowHeader, int _PopupWidth, int _DropDownRows)
		{
			DisplayMember = _DisplayMember;
			ValueMember = _ValueMember;
			ColumnInfos = _ColumnInfos;
			ShowHeader = _ShowHeader;
			PopupWidth = _PopupWidth;
			DropDownRows = _DropDownRows;
		}
	}
}
