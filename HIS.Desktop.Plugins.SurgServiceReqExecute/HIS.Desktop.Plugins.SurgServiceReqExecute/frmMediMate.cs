using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.Location;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute
{
	public class frmMediMate : Form
	{
		private List<V_HIS_SERE_SERV> ListData = new List<V_HIS_SERE_SERV>();

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private SimpleButton btnSave;

		private TextEdit txtContent;

		private GridControl gridControl1;

		private GridView gridView1;

		private TextEdit txtFind;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem4;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private GridColumn gridColumn3;

		private GridColumn gridColumn4;

		private long parentId { get; set; }

		private Action<string> ContentResult { get; set; }

		public frmMediMate(long parentId, Action<string> ContentResult)
		{
			InitializeComponent();
			try
			{
				SetIcon();
				this.ContentResult = ContentResult;
				this.parentId = parentId;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetIcon()
		{
			try
			{
				base.Icon = Icon.ExtractAssociatedIcon(Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void frmMediMate_Load(object sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				CommonParam val = new CommonParam();
				HisSereServViewFilter val2 = new HisSereServViewFilter();
				val2.PARENT_ID = parentId;
				val2.SERVICE_TYPE_IDs = new List<long> { 6L, 7L };
				ListData = ((AdapterBase)new BackendAdapter(val)).Get<List<V_HIS_SERE_SERV>>("api/HisSereServ/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val2, val);
				if (ListData != null && ListData.Count > 0)
				{
					ListData = (from o in ListData
						orderby o.TDL_SERVICE_TYPE_ID
						orderby o.TDL_SERVICE_NAME
						select o).ToList();
				}
				gridControl1.DataSource = ListData;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtFind_TextChanged(object sender, EventArgs e)
		{
			try
			{
				List<V_HIS_SERE_SERV> list = ListData;
				string key = txtFind.Text.Trim();
				if (!string.IsNullOrEmpty(key))
				{
					key = key.ToLower();
					list = list.Where((V_HIS_SERE_SERV o) => o.TDL_SERVICE_CODE.ToLower().Contains(key) || o.TDL_SERVICE_NAME.ToLower().Contains(key) || o.SERVICE_UNIT_NAME.ToLower().Contains(key)).ToList();
				}
				gridControl1.DataSource = null;
				gridControl1.DataSource = list;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void gridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				if (gridView1.SelectedRowsCount > 0)
				{
					List<string> list = new List<string>();
					for (int i = 0; i < gridView1.SelectedRowsCount; i++)
					{
						list.Add(((V_HIS_SERE_SERV)gridView1.GetRow(gridView1.GetSelectedRows()[i])).TDL_SERVICE_NAME);
					}
					txtContent.Text = string.Join("; ", list);
				}
				else
				{
					txtContent.Text = null;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (ContentResult != null)
				{
					ContentResult(txtContent.Text.Trim());
				}
				Close();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.txtContent = new DevExpress.XtraEditors.TextEdit();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.txtFind = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.txtContent.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtFind.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.btnSave);
			this.layoutControl1.Controls.Add(this.txtContent);
			this.layoutControl1.Controls.Add(this.gridControl1);
			this.layoutControl1.Controls.Add(this.txtFind);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(737, 372);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.btnSave.Location = new System.Drawing.Point(638, 348);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(97, 22);
			this.btnSave.StyleController = this.layoutControl1;
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Chọn";
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.txtContent.Location = new System.Drawing.Point(2, 348);
			this.txtContent.Name = "txtContent";
			this.txtContent.Size = new System.Drawing.Size(632, 20);
			this.txtContent.StyleController = this.layoutControl1;
			this.txtContent.TabIndex = 6;
			this.gridControl1.Location = new System.Drawing.Point(2, 26);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(733, 318);
			this.gridControl1.TabIndex = 5;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[4] { this.gridColumn1, this.gridColumn2, this.gridColumn3, this.gridColumn4 });
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
			this.gridView1.OptionsSelection.MultiSelect = true;
			this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.OptionsView.ShowIndicator = false;
			this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(gridView1_SelectionChanged);
			this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.Caption = "Mã";
			this.gridColumn1.FieldName = "TDL_SERVICE_CODE";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 1;
			this.gridColumn1.Width = 81;
			this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn2.Caption = "Tên thuốc/vật tư";
			this.gridColumn2.FieldName = "TDL_SERVICE_NAME";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 2;
			this.gridColumn2.Width = 406;
			this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn3.Caption = "Số lượng";
			this.gridColumn3.FieldName = "AMOUNT";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 3;
			this.gridColumn3.Width = 94;
			this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.Caption = "Đơn vị tính";
			this.gridColumn4.FieldName = "SERVICE_UNIT_NAME";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 4;
			this.gridColumn4.Width = 120;
			this.txtFind.Location = new System.Drawing.Point(2, 2);
			this.txtFind.Name = "txtFind";
			this.txtFind.Properties.NullValuePrompt = "Từ khóa tìm kiếm";
			this.txtFind.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtFind.Properties.ShowNullValuePromptWhenFocused = true;
			this.txtFind.Size = new System.Drawing.Size(733, 20);
			this.txtFind.StyleController = this.layoutControl1;
			this.txtFind.TabIndex = 4;
			this.txtFind.TextChanged += new System.EventHandler(txtFind_TextChanged);
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[4] { this.layoutControlItem1, this.layoutControlItem2, this.layoutControlItem3, this.layoutControlItem4 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(737, 372);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.txtFind;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(737, 24);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem2.Control = this.gridControl1;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(737, 322);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.layoutControlItem3.Control = this.txtContent;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 346);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(636, 26);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			this.layoutControlItem4.Control = this.btnSave;
			this.layoutControlItem4.Location = new System.Drawing.Point(636, 346);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(101, 26);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(737, 372);
			base.Controls.Add(this.layoutControl1);
			base.Name = "frmMediMate";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chọn thuốc/vật tư";
			base.Load += new System.EventHandler(frmMediMate_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.txtContent.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtFind.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
			base.ResumeLayout(false);
		}
	}
}
