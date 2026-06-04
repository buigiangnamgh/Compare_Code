using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Plugins.RegisterV2.ADO;
using HIS.Desktop.Plugins.RegisterV2.Resources;
using HIS.Desktop.Utility;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RegisterV2
{
	public class frmServiceReqChoice : FormBase
	{
		private List<V_HIS_SERVICE_REQ> listService = null;

		private List<V_HIS_SERE_SERV> listSereServ = null;

		private List<ServiceReqADO> listData = null;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private GridControl gridControl1;

		private GridView gridView1;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private GridColumn gridColumn3;

		private LayoutControlItem layoutControlItem1;

		private SimpleButton simpleButton1;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private BarManager barManager1;

		private Bar bar1;

		private BarButtonItem bbntClose;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private RepositoryItemMemoEdit repositoryItemMemoEdit1;

		private GridColumn gridColumn4;

		public frmServiceReqChoice(List<V_HIS_SERE_SERV> sereServ, List<V_HIS_SERVICE_REQ> listServiceReqs)
		{
			InitializeComponent();
			try
			{
				if (listService == null)
				{
					listService = new List<V_HIS_SERVICE_REQ>();
				}
				listService = listServiceReqs;
				if (listSereServ == null)
				{
					listSereServ = new List<V_HIS_SERE_SERV>();
				}
				listSereServ = sereServ;
				if (listData == null)
				{
					listData = new List<ServiceReqADO>();
				}
				SetCaptionByLanguageKey();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetCaptionByLanguageKey()
		{
			try
			{
				ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.RegisterV2.Resources.Lang", typeof(frmServiceReqChoice).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.layoutControl1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				simpleButton1.Text = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.simpleButton1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn1.Caption = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.gridColumn1.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn2.Caption = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.gridColumn2.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn3.Caption = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.gridColumn3.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				bar1.Text = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.bar1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				bbntClose.Caption = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.bbntClose.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn4.Caption = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.gridColumn4.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				Text = Inventec.Common.Resource.Get.Value("frmServiceReqChoice.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void PopupPatientInformation_Load(object sender, EventArgs e)
		{
			try
			{
				WaitingManager.Show();
				SetIconFrm();
				SetData();
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				WaitingManager.Hide();
			}
		}

		private void SetIconFrm()
		{
			try
			{
				string filePath = Path.Combine(ApplicationStoreLocation.ApplicationStartupPath, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
				base.Icon = Icon.ExtractAssociatedIcon(filePath);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetData()
		{
			try
			{
				foreach (V_HIS_SERVICE_REQ item in listService)
				{
					V_HIS_SERE_SERV v_HIS_SERE_SERV = listSereServ.SingleOrDefault((V_HIS_SERE_SERV x) => x.SERVICE_REQ_ID == item.ID);
					ServiceReqADO serviceReqADO = new ServiceReqADO();
					serviceReqADO.NUMBER_ORDER = item.NUM_ORDER;
					serviceReqADO.SERVICE_NAME = v_HIS_SERE_SERV.TDL_SERVICE_NAME;
					serviceReqADO.EXCUTE_ROOM_NAME = item.EXECUTE_ROOM_NAME;
					serviceReqADO.INTRUCTION_DATE = Inventec.Common.DateTime.Convert.TimeNumberToDateString(item.INTRUCTION_TIME);
					serviceReqADO.INTRUCTION_TIME = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(item.INTRUCTION_TIME);
					listData.Add(serviceReqADO);
				}
				gridControl1.DataSource = listData;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbntClose_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				simpleButton1_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			try
			{
				Close();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public override void ProcessDisposeModuleDataAfterClose()
		{
			try
			{
				listData = null;
				listSereServ = null;
				listService = null;
				simpleButton1.Click -= new EventHandler(simpleButton1_Click);
				bbntClose.ItemClick -= new ItemClickEventHandler(bbntClose_ItemClick);
				base.Load -= new EventHandler(PopupPatientInformation_Load);
				gridView1.GridControl.DataSource = null;
				gridControl1.DataSource = null;
				gridColumn4 = null;
				repositoryItemMemoEdit1 = null;
				barDockControlRight = null;
				barDockControlLeft = null;
				barDockControlBottom = null;
				barDockControlTop = null;
				bbntClose = null;
				bar1 = null;
				barManager1 = null;
				emptySpaceItem1 = null;
				layoutControlItem2 = null;
				simpleButton1 = null;
				layoutControlItem1 = null;
				gridColumn3 = null;
				gridColumn2 = null;
				gridColumn1 = null;
				gridView1 = null;
				gridControl1 = null;
				layoutControlGroup1 = null;
				layoutControl1 = null;
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
			this.components = new System.ComponentModel.Container();
			DevExpress.XtraGrid.GridLevelNode gridLevelNode = new DevExpress.XtraGrid.GridLevelNode();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HIS.Desktop.Plugins.RegisterV2.frmServiceReqChoice));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bbntClose = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemMemoEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.simpleButton1);
			this.layoutControl1.Controls.Add(this.gridControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 29);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(456, 128, 250, 350);
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1114, 432);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.simpleButton1.Location = new System.Drawing.Point(996, 408);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(116, 22);
			this.simpleButton1.StyleController = this.layoutControl1;
			this.simpleButton1.TabIndex = 5;
			this.simpleButton1.Text = "Đóng (Ctrl S)";
			this.simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
			this.gridControl1.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
			gridLevelNode.RelationName = "Level1";
			this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[1] { gridLevelNode });
			this.gridControl1.Location = new System.Drawing.Point(2, 2);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[1] { this.repositoryItemMemoEdit1 });
			this.gridControl1.Size = new System.Drawing.Size(1110, 402);
			this.gridControl1.TabIndex = 4;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.25f);
			this.gridView1.Appearance.Row.Options.UseFont = true;
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[4] { this.gridColumn1, this.gridColumn4, this.gridColumn2, this.gridColumn3 });
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
			this.gridView1.OptionsView.RowAutoHeight = true;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.OptionsView.ShowIndicator = false;
			this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 14f);
			this.gridColumn1.AppearanceCell.Options.UseFont = true;
			this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
			this.gridColumn1.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
			this.gridColumn1.AppearanceHeader.Options.UseFont = true;
			this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.Caption = "STT";
			this.gridColumn1.ColumnEdit = this.repositoryItemMemoEdit1;
			this.gridColumn1.FieldName = "NUMBER_ORDER";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 42;
			this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
			this.repositoryItemMemoEdit1.ReadOnly = true;
			this.repositoryItemMemoEdit1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 14f);
			this.gridColumn2.AppearanceCell.Options.UseFont = true;
			this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumn2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
			this.gridColumn2.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
			this.gridColumn2.AppearanceHeader.Options.UseFont = true;
			this.gridColumn2.Caption = "Phòng khám";
			this.gridColumn2.ColumnEdit = this.repositoryItemMemoEdit1;
			this.gridColumn2.FieldName = "EXCUTE_ROOM_NAME";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 2;
			this.gridColumn2.Width = 203;
			this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12f);
			this.gridColumn3.AppearanceCell.Options.UseFont = true;
			this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold);
			this.gridColumn3.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
			this.gridColumn3.AppearanceHeader.Options.UseFont = true;
			this.gridColumn3.Caption = "Dịch vụ";
			this.gridColumn3.ColumnEdit = this.repositoryItemMemoEdit1;
			this.gridColumn3.FieldName = "SERVICE_NAME";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.Width = 330;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3] { this.layoutControlItem1, this.layoutControlItem2, this.emptySpaceItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1114, 432);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.gridControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1114, 406);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem2.Control = this.simpleButton1;
			this.layoutControlItem2.Location = new System.Drawing.Point(994, 406);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(120, 26);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 406);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(994, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[1] { this.bbntClose });
			this.barManager1.MaxItemId = 1;
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.bbntClose)
			});
			this.bar1.Text = "Tools";
			this.bar1.Visible = false;
			this.bbntClose.Caption = "Close(Ctrl S)";
			this.bbntClose.Id = 0;
			this.bbntClose.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
			this.bbntClose.Name = "bbntClose";
			this.bbntClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbntClose_ItemClick);
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(1114, 29);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 461);
			this.barDockControlBottom.Size = new System.Drawing.Size(1114, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 432);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(1114, 29);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 432);
			this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 14f);
			this.gridColumn4.AppearanceCell.Options.UseFont = true;
			this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
			this.gridColumn4.AppearanceHeader.Options.UseFont = true;
			this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn4.Caption = "Ngày khám";
			this.gridColumn4.FieldName = "INTRUCTION_DATE";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 1;
			this.gridColumn4.Width = 137;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1114, 461);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "frmServiceReqChoice";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Thông tin đăng ký dịch vụ";
			base.Load += new System.EventHandler(PopupPatientInformation_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemMemoEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
