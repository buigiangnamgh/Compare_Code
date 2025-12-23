using System.Collections.Generic;
using System.Reflection;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace Inventec.Common.Integrate.CustomControl
{
	internal class CustomGridViewWithFilterMultiColumn : GridView, IGridLookUp
	{
		protected override string ViewName
		{
			get
			{
				return "CustomGridViewWithFilterMultiColumn";
			}
		}

		protected internal virtual string GetExtraFilterText
		{
			get
			{
				return ExtraFilterText;
			}
		}

		protected internal virtual void SetGridControlAccessMetod(GridControl newControl)
		{
			SetGridControl(newControl);
		}

		void IGridLookUp.Show(object editValue, string filterText)
		{
			FieldInfo field = typeof(GridView).GetField("firstMouseEnter", BindingFlags.Instance | BindingFlags.NonPublic);
			field.SetValue(this, true);
			if (base.LookUpOwner == null)
			{
				return;
			}
			if (Columns.Count == 0 && AllowAutoPopulateColumns)
			{
				PopulateColumns();
			}
			((IGridLookUp)this).SetDisplayFilter(filterText);
			if (base.LookUpOwner.TextEditStyle == TextEditStyles.DisableTextEditor && RowCount == 0 && ExtraFilterText != string.Empty)
			{
				((IGridLookUp)this).SetDisplayFilter(string.Empty);
			}
			int num = base.DataController.FindRowByValue(base.LookUpOwner.ValueMember, editValue, delegate(object args)
			{
				int num2 = (int)args;
				if (num2 >= 0)
				{
					base.FocusedRowHandle = num2;
					MakeRowVisible(base.FocusedRowHandle, false);
				}
				else
				{
					base.FocusedRowHandle = int.MinValue;
				}
			});
			if (num == -2147483638)
			{
				num = base.FocusedRowHandle;
			}
			if (!IsValidRowHandle(num))
			{
				num = 0;
			}
			BeginUpdate();
			try
			{
				base.TopRowIndex = 0;
				base.FocusedRowHandle = num;
			}
			finally
			{
				EndUpdate();
			}
			MakeRowVisible(base.FocusedRowHandle, false);
		}

		protected override string OnCreateLookupDisplayFilter(string text, string displayMember)
		{
			List<CriteriaOperator> list = new List<CriteriaOperator>();
			string[] array = text.Split(' ');
			string[] array2 = array;
			foreach (string autoFilterText in array2)
			{
				string value = LikeData.CreateContainsPattern(autoFilterText);
				List<CriteriaOperator> list2 = new List<CriteriaOperator>();
				foreach (GridColumn column in Columns)
				{
					if (column.ColumnType == typeof(string))
					{
						list2.Add(new BinaryOperator(column.FieldName, value, BinaryOperatorType.Like));
					}
				}
				list.Add(new GroupOperator(GroupOperatorType.Or, list2));
			}
			return new GroupOperator(GroupOperatorType.And, list).ToString();
		}

		protected override void OnApplyColumnsFilterComplete()
		{
			base.OnApplyColumnsFilterComplete();
			bool flag = true;
		}
	}
}
