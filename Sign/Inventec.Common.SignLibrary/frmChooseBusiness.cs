using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using EMR.EFMODEL.DataModels;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary
{
	public class frmChooseBusiness : Form
	{
		private Action<bool> IsReloadEmrBusiness;

		private Action<EMR_BUSINESS> actChoose;

		private List<EMR_BUSINESS> emrBusiness;

		private string currentBusinessCode;

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

		private SimpleButton btnAdd;

		private LayoutControlItem layoutControlItem3;

		private BarManager barManager1;

		private Bar bar1;

		private BarButtonItem barButtonItem1;

		private BarButtonItem barButtonItem2;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private BarButtonItem barButtonItem3;

		private SimpleButton btnSearch;

		private TextEdit txtSearch;

		private LayoutControlItem layoutControlItem4;

		private LayoutControlItem layoutControlItem5;

		public frmChooseBusiness(Action<EMR_BUSINESS> _actChoose, List<EMR_BUSINESS> _emrBusiness, string _currentBusinessCode, Action<bool> IsReloadEmrBusiness)
		{
			InitializeComponent();
			this.IsReloadEmrBusiness = IsReloadEmrBusiness;
			actChoose = _actChoose;
			emrBusiness = _emrBusiness;
			currentBusinessCode = _currentBusinessCode;
		}

		private void frmChooseBusiness_Load(object sender, EventArgs e)
		{
			try
			{
				int focusedRowHandle = 0;
				gridControl1.DataSource = emrBusiness;
				if (!string.IsNullOrEmpty(currentBusinessCode) && emrBusiness != null && emrBusiness.Count > 0)
				{
					for (int i = 0; i < emrBusiness.Count; i++)
					{
						if (emrBusiness[i].BUSINESS_CODE == currentBusinessCode)
						{
							focusedRowHandle = i;
							break;
						}
					}
				}
				if (emrBusiness == null || emrBusiness.Count == 0)
				{
					btnChoose.Enabled = false;
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
			try
			{
				EMR_BUSINESS eMR_BUSINESS = (EMR_BUSINESS)gridView1.GetFocusedRow();
				if (eMR_BUSINESS != null)
				{
					actChoose(eMR_BUSINESS);
					Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (emrBusiness == null)
				{
					emrBusiness = new List<EMR_BUSINESS>();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (btnChoose.Enabled)
			{
				btnChoose_Click(null, null);
			}
		}

		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			btnAdd_Click(null, null);
		}

		private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
		{
			btnSearch_Click(null, null);
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				txtSearch.Text = txtSearch.Text.Trim();
				if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
				{
					IEnumerable<EMR_BUSINESS> dataSource = emrBusiness.Where((EMR_BUSINESS o) => o.BUSINESS_CODE.ToLower().Contains(txtSearch.Text.Trim().ToLower()) || o.BUSINESS_NAME.ToLower().Contains(txtSearch.Text.Trim().ToLower()));
					gridControl1.DataSource = dataSource;
				}
				else
				{
					gridControl1.DataSource = emrBusiness;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					btnSearch_Click(null, null);
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
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
			this.btnChoose = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.barManager1 = new DevExpress.XtraBars.BarManager();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			this.txtSearch = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtSearch.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
			base.SuspendLayout();
			this.gridControl1.Location = new System.Drawing.Point(12, 38);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(479, 144);
			this.gridControl1.TabIndex = 0;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.gridControl1.DoubleClick += new System.EventHandler(gridControl1_DoubleClick);
			this.gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
			this.gridView1.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.gridView1.Appearance.HideSelectionRow.Options.UseFont = true;
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[2] { this.gridColumn1, this.gridColumn2 });
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.OptionsView.ShowIndicator = false;
			this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(gridView1_KeyDown);
			this.gridColumn1.Caption = "Mã";
			this.gridColumn1.FieldName = "BUSINESS_CODE";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 96;
			this.gridColumn2.Caption = "Tên";
			this.gridColumn2.FieldName = "BUSINESS_NAME";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 352;
			this.layoutControl1.Controls.Add(this.btnSearch);
			this.layoutControl1.Controls.Add(this.txtSearch);
			this.layoutControl1.Controls.Add(this.btnAdd);
			this.layoutControl1.Controls.Add(this.btnChoose);
			this.layoutControl1.Controls.Add(this.gridControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 29);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(503, 220);
			this.layoutControl1.TabIndex = 1;
			this.layoutControl1.Text = "layoutControl1";
			this.btnAdd.Location = new System.Drawing.Point(302, 186);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(95, 22);
			this.btnAdd.StyleController = this.layoutControl1;
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "Thêm (Ctrl N)";
			this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
			this.btnChoose.Location = new System.Drawing.Point(401, 186);
			this.btnChoose.Name = "btnChoose";
			this.btnChoose.Size = new System.Drawing.Size(90, 22);
			this.btnChoose.StyleController = this.layoutControl1;
			this.btnChoose.TabIndex = 4;
			this.btnChoose.Text = "Chọn (Ctrl S)";
			this.btnChoose.Click += new System.EventHandler(btnChoose_Click);
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[6] { this.layoutControlItem1, this.layoutControlItem2, this.emptySpaceItem1, this.layoutControlItem3, this.layoutControlItem4, this.layoutControlItem5 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(503, 220);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.gridControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(483, 148);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem2.Control = this.btnChoose;
			this.layoutControlItem2.Location = new System.Drawing.Point(389, 174);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(94, 26);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 174);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(290, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.Control = this.btnAdd;
			this.layoutControlItem3.Location = new System.Drawing.Point(290, 174);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(99, 26);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[3] { this.barButtonItem1, this.barButtonItem2, this.barButtonItem3 });
			this.barManager1.MaxItemId = 3;
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
				new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3)
			});
			this.bar1.Text = "Tools";
			this.bar1.Visible = false;
			this.barButtonItem1.Caption = "barButtonItem1";
			this.barButtonItem1.Id = 0;
			this.barButtonItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.N | System.Windows.Forms.Keys.Control);
			this.barButtonItem1.Name = "barButtonItem1";
			this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem1_ItemClick);
			this.barButtonItem2.Caption = "barButtonItem2";
			this.barButtonItem2.Id = 1;
			this.barButtonItem2.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
			this.barButtonItem2.Name = "barButtonItem2";
			this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem2_ItemClick);
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(503, 29);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 249);
			this.barDockControlBottom.Size = new System.Drawing.Size(503, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 220);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(503, 29);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 220);
			this.barButtonItem3.Caption = "barButtonItem3";
			this.barButtonItem3.Id = 2;
			this.barButtonItem3.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Control);
			this.barButtonItem3.Name = "barButtonItem3";
			this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick);
			this.txtSearch.Location = new System.Drawing.Point(12, 12);
			this.txtSearch.MenuManager = this.barManager1;
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Properties.NullValuePrompt = "Từ khóa tìm kiếm";
			this.txtSearch.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtSearch.Properties.ShowNullValuePromptWhenFocused = true;
			this.txtSearch.Size = new System.Drawing.Size(373, 20);
			this.txtSearch.StyleController = this.layoutControl1;
			this.txtSearch.TabIndex = 6;
			this.txtSearch.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(txtSearch_PreviewKeyDown);
			this.layoutControlItem4.Control = this.txtSearch;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(377, 26);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			this.btnSearch.Location = new System.Drawing.Point(389, 12);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(102, 22);
			this.btnSearch.StyleController = this.layoutControl1;
			this.btnSearch.TabIndex = 7;
			this.btnSearch.Text = "Tìm kiếm (Ctrl F)";
			this.btnSearch.Click += new System.EventHandler(btnSearch_Click);
			this.layoutControlItem5.Control = this.btnSearch;
			this.layoutControlItem5.Location = new System.Drawing.Point(377, 0);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(106, 26);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(503, 249);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmChooseBusiness";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chọn nghiệp vụ ký";
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
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtSearch.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
