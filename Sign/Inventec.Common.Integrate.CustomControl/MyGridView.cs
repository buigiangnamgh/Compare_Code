using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Inventec.Common.Integrate.CustomControl
{
	public class MyGridView : GridView
	{
		internal static readonly string ViewNameValue = typeof(MyGridView).Name;

		protected override string ViewName
		{
			get
			{
				return ViewNameValue;
			}
		}

		public event EventHandler<RowErrorEventArgs> CustomRowError;

		public event EventHandler<RowColumnErrorEventArgs> CustomRowColumnError;

		public MyGridView(GridControl ownerGrid)
			: base(ownerGrid)
		{
			base.CustomUnboundColumnData += MyGridView_CustomUnboundColumnData;
		}

		public MyGridView()
		{
			base.CustomUnboundColumnData += MyGridView_CustomUnboundColumnData;
		}

		protected override BaseGridController CreateDataController()
		{
			if (requireDataControllerType == DataControllerType.AsyncServerMode)
			{
				return new AsyncServerModeDataController();
			}
			if (requireDataControllerType == DataControllerType.ServerMode)
			{
				return new ServerModeDataController();
			}
			if (requireDataControllerType == DataControllerType.RegularNoCurrencyManager)
			{
				return new MyGridDataController(this);
			}
			return new MyCurrencyDataController(this);
		}

		protected internal virtual void FillRowError(int handle, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo errorInfo)
		{
			EventHandler<RowErrorEventArgs> eventHandler = this.CustomRowError;
			if (eventHandler != null)
			{
				eventHandler(this, new RowErrorEventArgs(errorInfo, handle));
			}
		}

		protected internal virtual void FillRowColumnError(int handle, string column, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo errorInfo)
		{
			EventHandler<RowColumnErrorEventArgs> eventHandler = this.CustomRowColumnError;
			if (eventHandler != null)
			{
				eventHandler(this, new RowColumnErrorEventArgs(errorInfo, handle, column));
			}
		}

		private void MyGridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(e.Column.FieldName) || !e.Column.FieldName.Contains("Unb"))
				{
					return;
				}
				string text = e.Column.FieldName.Substring(0, e.Column.FieldName.IndexOf("Unb"));
				object row = e.Row;
				Type type = row.GetType();
				PropertyInfo[] properties = type.GetProperties();
				PropertyInfo[] array = properties;
				foreach (PropertyInfo propertyInfo in array)
				{
					if (propertyInfo.Name == text && propertyInfo.Name != "IsChecked")
					{
						object value = propertyInfo.GetValue(row);
						if (value != null)
						{
							string text2 = RemoveDiacritics(value.ToString(), true);
							e.Value = ((value != null) ? value.ToString() : null) + text2;
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		protected override void RefreshVisibleColumnsList()
		{
			base.RefreshVisibleColumnsList();
			foreach (GridColumn visibleColumn in VisibleColumns)
			{
				string fieldName = visibleColumn.FieldName + "Unb";
				GridColumn gridColumn2 = Columns.ColumnByFieldName(fieldName);
				if (gridColumn2 == null)
				{
					GridColumn gridColumn3 = Columns.AddField(visibleColumn.FieldName + "Unb");
					gridColumn3.UnboundType = UnboundColumnType.String;
					visibleColumn.FieldNameSortGroup = gridColumn3.FieldName;
					visibleColumn.OptionsFilter.FilterBySortField = DefaultBoolean.True;
				}
			}
		}

		public static IEnumerable<char> RemoveDiacriticsEnum(string src, bool compatNorm, Func<char, char> customFolding)
		{
			string text = src.Normalize(compatNorm ? NormalizationForm.FormKD : NormalizationForm.FormD);
			foreach (char c in text)
			{
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				UnicodeCategory unicodeCategory2 = unicodeCategory;
				if ((uint)(unicodeCategory2 - 5) > 2u)
				{
					yield return customFolding(c);
				}
			}
		}

		public static IEnumerable<char> RemoveDiacriticsEnum(string src, bool compatNorm)
		{
			return RemoveDiacritics(src, compatNorm, (char c) => c);
		}

		public static string RemoveDiacritics(string src, bool compatNorm, Func<char, char> customFolding)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char item in RemoveDiacriticsEnum(src, compatNorm, customFolding))
			{
				stringBuilder.Append(item);
			}
			return stringBuilder.ToString().Replace('Đ', 'D').Replace('đ', 'd');
		}

		public static string RemoveDiacritics(string src, bool compatNorm)
		{
			return RemoveDiacritics(src, compatNorm, (char c) => c);
		}

		protected override ColumnFilterInfo CreateFilterRowInfo(GridColumn column, object _value)
		{
			string text = ((_value == null) ? null : _value.ToString());
			if (_value == null || text == string.Empty)
			{
				return ColumnFilterInfo.Empty;
			}
			text = RemoveDiacritics(text, true);
			AutoFilterCondition condition = ResolveAutoFilterCondition(column);
			CriteriaOperator filter = CreateAutoFilterCriterion(column, condition, _value, text);
			return new ColumnFilterInfo(ColumnFilterType.AutoFilter, _value, filter, string.Empty);
		}
	}
}
