using System;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;

namespace Inventec.Common.Integrate.EditorLoader
{
	public class ControlEditorLoader
	{
		public static void Load(object control, object dataSource, ControlEditorADO controlEditorADO)
		{
			try
			{
				if (control is LookUpEdit)
				{
					LoadDataToLookUpEdit((LookUpEdit)control, dataSource, controlEditorADO);
				}
				else if (control is GridLookUpEdit)
				{
					LoadDataToGridLookUpEdit((GridLookUpEdit)control, dataSource, controlEditorADO);
				}
				else if (control is RepositoryItemLookUpEdit)
				{
					LoadDataToLookUpEdit((RepositoryItemLookUpEdit)control, dataSource, controlEditorADO);
				}
				else if (control is RepositoryItemGridLookUpEdit)
				{
					LoadDataToGridLookUpEdit((RepositoryItemGridLookUpEdit)control, dataSource, controlEditorADO);
				}
			}
			catch (Exception)
			{
			}
		}

		private static void LoadDataToLookUpEdit(LookUpEdit cboEditor, object dataSource, ControlEditorADO controlEditorADO)
		{
			try
			{
				cboEditor.Properties.DataSource = dataSource;
				cboEditor.Properties.DisplayMember = controlEditorADO.DisplayMember;
				cboEditor.Properties.ValueMember = controlEditorADO.ValueMember;
				cboEditor.Properties.ForceInitialize();
				cboEditor.Properties.Columns.Clear();
				foreach (ColumnInfo columnInfo in controlEditorADO.ColumnInfos)
				{
					FormatType formatType = FormatType.None;
					ColumnInfo.FormatType formatType2 = columnInfo.formatType;
					if (true)
					{
						switch (columnInfo.formatType)
						{
						case ColumnInfo.FormatType.None:
							formatType = FormatType.None;
							break;
						case ColumnInfo.FormatType.Numeric:
							formatType = FormatType.Numeric;
							break;
						case ColumnInfo.FormatType.DateTime:
							formatType = FormatType.DateTime;
							break;
						case ColumnInfo.FormatType.Custom:
							formatType = FormatType.Custom;
							break;
						}
					}
					HorzAlignment alignment = HorzAlignment.Default;
					switch (columnInfo.horzAlignment)
					{
					case ColumnInfo.HorzAlignment.Default:
						alignment = HorzAlignment.Default;
						break;
					case ColumnInfo.HorzAlignment.Near:
						alignment = HorzAlignment.Near;
						break;
					case ColumnInfo.HorzAlignment.Center:
						alignment = HorzAlignment.Center;
						break;
					case ColumnInfo.HorzAlignment.Far:
						alignment = HorzAlignment.Far;
						break;
					}
					cboEditor.Properties.Columns.Add(new LookUpColumnInfo(columnInfo.fieldName, columnInfo.caption, columnInfo.width, formatType, columnInfo.formatString, columnInfo.visible, alignment));
				}
				cboEditor.Properties.ShowHeader = controlEditorADO.ShowHeader;
				cboEditor.Properties.ImmediatePopup = controlEditorADO.ImmediatePopup;
				cboEditor.Properties.DropDownRows = ((controlEditorADO.DropDownRows == 0) ? 10 : controlEditorADO.DropDownRows);
				cboEditor.Properties.PopupWidth = ((controlEditorADO.PopupWidth == 0) ? 300 : controlEditorADO.PopupWidth);
			}
			catch (Exception)
			{
			}
		}

		private static void LoadDataToGridLookUpEdit(GridLookUpEdit cboEditor, object dataSource, ControlEditorADO controlEditorADO)
		{
			try
			{
				cboEditor.Properties.DataSource = dataSource;
				cboEditor.Properties.DisplayMember = controlEditorADO.DisplayMember;
				cboEditor.Properties.ValueMember = controlEditorADO.ValueMember;
				cboEditor.Properties.TextEditStyle = TextEditStyles.Standard;
				cboEditor.Properties.PopupFilterMode = PopupFilterMode.Contains;
				cboEditor.Properties.ImmediatePopup = controlEditorADO.ImmediatePopup;
				cboEditor.ForceInitialize();
				cboEditor.Properties.View.Columns.Clear();
				foreach (ColumnInfo columnInfo in controlEditorADO.ColumnInfos)
				{
					HorzAlignment hAlignment = HorzAlignment.Default;
					switch (columnInfo.horzAlignment)
					{
					case ColumnInfo.HorzAlignment.Default:
						hAlignment = HorzAlignment.Default;
						break;
					case ColumnInfo.HorzAlignment.Near:
						hAlignment = HorzAlignment.Near;
						break;
					case ColumnInfo.HorzAlignment.Center:
						hAlignment = HorzAlignment.Center;
						break;
					case ColumnInfo.HorzAlignment.Far:
						hAlignment = HorzAlignment.Far;
						break;
					}
					GridColumn gridColumn = cboEditor.Properties.View.Columns.AddField(columnInfo.fieldName);
					gridColumn.Caption = columnInfo.caption;
					gridColumn.Visible = columnInfo.visible;
					gridColumn.VisibleIndex = columnInfo.VisibleIndex;
					gridColumn.Width = ((columnInfo.width == 0) ? 100 : columnInfo.width);
					gridColumn.AppearanceCell.TextOptions.HAlignment = hAlignment;
					gridColumn.OptionsColumn.FixedWidth = columnInfo.FixedWidth;
				}
				cboEditor.Properties.View.OptionsView.ColumnAutoWidth = false;
				cboEditor.Properties.View.OptionsView.ShowIndicator = false;
				cboEditor.Properties.View.OptionsView.ShowGroupPanel = false;
				cboEditor.Properties.PopupFormSize = new Size(controlEditorADO.PopupWidth + 20, 200);
				cboEditor.Properties.View.OptionsView.ShowColumnHeaders = controlEditorADO.ShowHeader;
			}
			catch (Exception)
			{
			}
		}

		private static void LoadDataToLookUpEdit(RepositoryItemLookUpEdit cboEditor, object dataSource, ControlEditorADO controlEditorADO)
		{
			try
			{
				cboEditor.DataSource = dataSource;
				cboEditor.DisplayMember = controlEditorADO.DisplayMember;
				cboEditor.ValueMember = controlEditorADO.ValueMember;
				cboEditor.ForceInitialize();
				cboEditor.Columns.Clear();
				foreach (ColumnInfo columnInfo in controlEditorADO.ColumnInfos)
				{
					FormatType formatType = FormatType.None;
					ColumnInfo.FormatType formatType2 = columnInfo.formatType;
					if (true)
					{
						switch (columnInfo.formatType)
						{
						case ColumnInfo.FormatType.None:
							formatType = FormatType.None;
							break;
						case ColumnInfo.FormatType.Numeric:
							formatType = FormatType.Numeric;
							break;
						case ColumnInfo.FormatType.DateTime:
							formatType = FormatType.DateTime;
							break;
						case ColumnInfo.FormatType.Custom:
							formatType = FormatType.Custom;
							break;
						}
					}
					HorzAlignment alignment = HorzAlignment.Default;
					switch (columnInfo.horzAlignment)
					{
					case ColumnInfo.HorzAlignment.Default:
						alignment = HorzAlignment.Default;
						break;
					case ColumnInfo.HorzAlignment.Near:
						alignment = HorzAlignment.Near;
						break;
					case ColumnInfo.HorzAlignment.Center:
						alignment = HorzAlignment.Center;
						break;
					case ColumnInfo.HorzAlignment.Far:
						alignment = HorzAlignment.Far;
						break;
					}
					cboEditor.Columns.Add(new LookUpColumnInfo(columnInfo.fieldName, columnInfo.caption, columnInfo.width, formatType, columnInfo.formatString, columnInfo.visible, alignment));
				}
				cboEditor.ShowHeader = controlEditorADO.ShowHeader;
				cboEditor.ImmediatePopup = controlEditorADO.ImmediatePopup;
				cboEditor.DropDownRows = ((controlEditorADO.DropDownRows == 0) ? 10 : controlEditorADO.DropDownRows);
				cboEditor.PopupWidth = ((controlEditorADO.PopupWidth == 0) ? 300 : controlEditorADO.PopupWidth);
			}
			catch (Exception)
			{
			}
		}

		private static void LoadDataToGridLookUpEdit(RepositoryItemGridLookUpEdit cboEditor, object dataSource, ControlEditorADO controlEditorADO)
		{
			try
			{
				cboEditor.DataSource = dataSource;
				cboEditor.DisplayMember = controlEditorADO.DisplayMember;
				cboEditor.ValueMember = controlEditorADO.ValueMember;
				cboEditor.TextEditStyle = TextEditStyles.Standard;
				cboEditor.PopupFilterMode = PopupFilterMode.Contains;
				cboEditor.ImmediatePopup = controlEditorADO.ImmediatePopup;
				cboEditor.View.Columns.Clear();
				foreach (ColumnInfo columnInfo in controlEditorADO.ColumnInfos)
				{
					HorzAlignment hAlignment = HorzAlignment.Default;
					switch (columnInfo.horzAlignment)
					{
					case ColumnInfo.HorzAlignment.Default:
						hAlignment = HorzAlignment.Default;
						break;
					case ColumnInfo.HorzAlignment.Near:
						hAlignment = HorzAlignment.Near;
						break;
					case ColumnInfo.HorzAlignment.Center:
						hAlignment = HorzAlignment.Center;
						break;
					case ColumnInfo.HorzAlignment.Far:
						hAlignment = HorzAlignment.Far;
						break;
					}
					GridColumn gridColumn = cboEditor.View.Columns.AddField(columnInfo.fieldName);
					FormatType formatType = FormatType.None;
					ColumnInfo.FormatType formatType2 = columnInfo.formatType;
					if (true)
					{
						switch (columnInfo.formatType)
						{
						case ColumnInfo.FormatType.None:
							formatType = FormatType.None;
							break;
						case ColumnInfo.FormatType.Numeric:
							formatType = FormatType.Numeric;
							break;
						case ColumnInfo.FormatType.DateTime:
							formatType = FormatType.DateTime;
							break;
						case ColumnInfo.FormatType.Custom:
							formatType = FormatType.Custom;
							break;
						}
					}
					gridColumn.DisplayFormat.FormatType = formatType;
					if (!string.IsNullOrEmpty(columnInfo.formatString))
					{
						gridColumn.DisplayFormat.FormatString = columnInfo.formatString;
					}
					gridColumn.Caption = columnInfo.caption;
					gridColumn.Visible = columnInfo.visible;
					gridColumn.VisibleIndex = columnInfo.VisibleIndex;
					gridColumn.Width = ((columnInfo.width == 0) ? 100 : columnInfo.width);
					gridColumn.AppearanceCell.TextOptions.HAlignment = hAlignment;
					gridColumn.OptionsColumn.FixedWidth = columnInfo.FixedWidth;
				}
				if (controlEditorADO.PopupWidth > 0)
				{
					cboEditor.PopupFormWidth = controlEditorADO.PopupWidth;
				}
				cboEditor.View.OptionsView.ColumnAutoWidth = true;
				cboEditor.View.OptionsView.ShowColumnHeaders = controlEditorADO.ShowHeader;
				cboEditor.View.OptionsView.ShowIndicator = false;
				cboEditor.View.OptionsView.ShowGroupPanel = false;
			}
			catch (Exception)
			{
			}
		}
	}
}
