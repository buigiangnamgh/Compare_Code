using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using EMR.EFMODEL.DataModels;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary
{
	public class frmChooseDocumentDependent : Form
	{
		private Action<V_EMR_DOCUMENT> actChoose;

		private List<V_EMR_DOCUMENT> emrDocument;

		private string currentDocumentCode;

		private IContainer components = null;

		private GridControl gridControl1;

		private GridView gridView1;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private LayoutControl layoutControl1;

		private SimpleButton btnChoose;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private LabelControl labelControl1;

		private LayoutControlItem layoutControlItem3;

		private GridColumn gridColumn3;

		private GridColumn gridColumn4;

		private GridColumn gridColumn5;

		public frmChooseDocumentDependent(Action<V_EMR_DOCUMENT> _actChoose, List<V_EMR_DOCUMENT> _emrDocument, string _currentDocumentCode)
		{
			InitializeComponent();
			actChoose = _actChoose;
			emrDocument = _emrDocument;
			currentDocumentCode = _currentDocumentCode;
		}

		private void frmChooseBusiness_Load(object sender, EventArgs e)
		{
			try
			{
				int focusedRowHandle = 0;
				gridControl1.DataSource = emrDocument;
				if (!string.IsNullOrEmpty(currentDocumentCode) && emrDocument != null && emrDocument.Count > 0)
				{
					for (int i = 0; i < emrDocument.Count; i++)
					{
						if (emrDocument[i].DOCUMENT_CODE == currentDocumentCode)
						{
							focusedRowHandle = i;
							break;
						}
					}
				}
				gridView1.FocusedRowHandle = focusedRowHandle;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void gridView1_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Space)
				{
					if (gridView1.IsEditing)
					{
						gridView1.CloseEditor();
					}
					if (gridView1.FocusedRowModified)
					{
						gridView1.UpdateCurrentRow();
					}
					btnChoose_Click(null, null);
				}
				else if (e.KeyCode == Keys.Return)
				{
					btnChoose_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void gridControl1_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				btnChoose_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnChoose_Click(object sender, EventArgs e)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			try
			{
				V_EMR_DOCUMENT val = (V_EMR_DOCUMENT)gridView1.GetFocusedRow();
				if (val != null)
				{
					actChoose(val);
					Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			try
			{
				if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound)
				{
					V_EMR_DOCUMENT val = (V_EMR_DOCUMENT)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
					if (val != null && e.Column.FieldName == "CREAT_TIME_DISPLAY")
					{
						e.Value = DateTimeConvert.TimeNumberToTimeString(val.CREATE_TIME.GetValueOrDefault());
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
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
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.btnChoose = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			base.SuspendLayout();
			this.gridControl1.Location = new System.Drawing.Point(12, 29);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(787, 182);
			this.gridControl1.TabIndex = 0;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.gridControl1.DoubleClick += new System.EventHandler(gridControl1_DoubleClick);
			this.gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
			this.gridView1.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.gridView1.Appearance.HideSelectionRow.Options.UseFont = true;
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[5] { this.gridColumn1, this.gridColumn2, this.gridColumn3, this.gridColumn4, this.gridColumn5 });
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.OptionsView.ShowIndicator = false;
			this.gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
			this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(gridView1_KeyDown);
			this.gridColumn1.Caption = "Mã văn bản";
			this.gridColumn1.FieldName = "DOCUMENT_CODE";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 96;
			this.gridColumn2.Caption = "Tên văn bản";
			this.gridColumn2.FieldName = "DOCUMENT_NAME";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 352;
			this.gridColumn3.Caption = " Loại văn bản";
			this.gridColumn3.FieldName = "DOCUMENT_TYPE_NAME";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn4.Caption = "Người tạo";
			this.gridColumn4.FieldName = "CREATOR";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 3;
			this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn5.Caption = "Thời gian tạo";
			this.gridColumn5.FieldName = "CREAT_TIME_DISPLAY";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 4;
			this.gridColumn5.Width = 99;
			this.layoutControl1.Controls.Add(this.labelControl1);
			this.layoutControl1.Controls.Add(this.btnChoose);
			this.layoutControl1.Controls.Add(this.gridControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(811, 249);
			this.layoutControl1.TabIndex = 1;
			this.layoutControl1.Text = "layoutControl1";
			this.labelControl1.Location = new System.Drawing.Point(12, 12);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(233, 13);
			this.labelControl1.StyleController = this.layoutControl1;
			this.labelControl1.TabIndex = 5;
			this.labelControl1.Text = "Vui lòng chọn văn bản phụ thuộc để thực hiện ký";
			this.btnChoose.Location = new System.Drawing.Point(376, 215);
			this.btnChoose.Name = "btnChoose";
			this.btnChoose.Size = new System.Drawing.Size(423, 22);
			this.btnChoose.StyleController = this.layoutControl1;
			this.btnChoose.TabIndex = 4;
			this.btnChoose.Text = "Chọn (Ctrl A)";
			this.btnChoose.Click += new System.EventHandler(btnChoose_Click);
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[4] { this.layoutControlItem1, this.layoutControlItem2, this.emptySpaceItem1, this.layoutControlItem3 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(811, 249);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.gridControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 17);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(791, 186);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem2.Control = this.btnChoose;
			this.layoutControlItem2.Location = new System.Drawing.Point(364, 203);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(427, 26);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 203);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(364, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.Control = this.labelControl1;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(791, 17);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(811, 249);
			base.Controls.Add(this.layoutControl1);
			base.Name = "frmChooseDocumentDependent";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ký văn bản phụ thuộc";
			base.Load += new System.EventHandler(frmChooseBusiness_Load);
			((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			base.ResumeLayout(false);
		}
	}
}
