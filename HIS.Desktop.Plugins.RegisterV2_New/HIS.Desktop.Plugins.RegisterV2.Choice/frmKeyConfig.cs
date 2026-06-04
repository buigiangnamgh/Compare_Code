using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Plugins.RegisterV2.ADO;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.RegisterV2.Choice
{
	public class frmKeyConfig : Form
	{
		private Action<string> textKey;

		private Dictionary<string, string> dic = new Dictionary<string, string>
		{
			{ "<#NUM_ORDER_STR;>", "Số thứ tự tiếp đón(NUM_ORDER) chuyển từ dạng số sang dạng chữ và đọc từng chữ" },
			{ "<#NUM_ORDER;>", "Số thứ tự tiếp đón(NUM_ORDER) đọc từng số." },
			{ "<#GATE_NAME;>", "Tên cổng" },
			{ "<#REGISTER_GATE_CODE;>", "Mã dãy" },
			{ "<#REGISTER_GATE_NAME;>", "Tên dãy" }
		};

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private GridControl gridControl1;

		private GridView gridView1;

		private LayoutControlItem layoutControlItem1;

		private GridColumn gc1;

		private GridColumn gc2;

		public frmKeyConfig(Action<string> textKey)
		{
			InitializeComponent();
			base.Icon = Icon.ExtractAssociatedIcon(Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
			try
			{
				this.textKey = textKey;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void frmKeyConfig_Load(object sender, EventArgs e)
		{
			try
			{
				KeyADO keyADO = new KeyADO();
				keyADO.lstKeyADO = new List<KeyADO>();
				foreach (KeyValuePair<string, string> item in dic)
				{
					keyADO.lstKeyADO.Add(new KeyADO
					{
						Key = item.Key,
						Details = item.Value
					});
				}
				gridControl1.DataSource = null;
				gridControl1.DataSource = keyADO.lstKeyADO;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void gridView1_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				KeyADO keyADO = (KeyADO)gridView1.GetFocusedRow();
				if (keyADO != null)
				{
					textKey(keyADO.Key);
					Close();
				}
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
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gc1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gc2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.gridControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(555, 135);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.gridControl1.Location = new System.Drawing.Point(2, 2);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(551, 131);
			this.gridControl1.TabIndex = 4;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[2] { this.gc1, this.gc2 });
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.OptionsView.ShowIndicator = false;
			this.gridView1.DoubleClick += new System.EventHandler(gridView1_DoubleClick);
			this.gc1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.gc1.AppearanceCell.Options.UseFont = true;
			this.gc1.Caption = "Key";
			this.gc1.FieldName = "Key";
			this.gc1.Name = "gc1";
			this.gc1.OptionsColumn.ReadOnly = true;
			this.gc1.Visible = true;
			this.gc1.VisibleIndex = 0;
			this.gc1.Width = 180;
			this.gc2.Caption = "Mô tả";
			this.gc2.FieldName = "Details";
			this.gc2.Name = "gc2";
			this.gc2.OptionsColumn.AllowEdit = false;
			this.gc2.OptionsColumn.ReadOnly = true;
			this.gc2.Visible = true;
			this.gc2.VisibleIndex = 1;
			this.gc2.Width = 369;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[1] { this.layoutControlItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(555, 135);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.gridControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(555, 135);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(555, 135);
			base.Controls.Add(this.layoutControl1);
			base.Name = "frmKeyConfig";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Key";
			base.Load += new System.EventHandler(frmKeyConfig_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
