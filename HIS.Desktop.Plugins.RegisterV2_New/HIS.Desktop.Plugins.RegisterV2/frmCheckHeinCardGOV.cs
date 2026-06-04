using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using His.Bhyt.InsuranceExpertise.LDO;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Plugins.RegisterV2.Resources;
using HIS.Desktop.Utility;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Desktop.Common.LanguageManager;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RegisterV2
{
	public class frmCheckHeinCardGOV : FormBase
	{
		private ResultHistoryLDO resultHistoryLDO = null;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private GridControl gridControlHistory;

		private GridView gridViewHistory;

		private LayoutControlItem layoutControlItem1;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private GridColumn gridColumn3;

		private GridColumn gridColumn4;

		private GridColumn gridColumn5;

		private GridColumn gridColumn6;

		private GridColumn gridColumn7;

		private GridColumn gridColumn8;

		private LabelControl labelControl1;

		private LabelControl lblMaKetQua;

		private LabelControl lblNgayDu5Nam;

		private LabelControl lblGiaTriTheDen;

		private LabelControl lblGiaTriTheTu;

		private LabelControl lblMaDKBD;

		private LabelControl lblCoQuanBHXH;

		private LabelControl lblMaKhuVuc;

		private LabelControl lblDiaChi;

		private LabelControl lblGioiTinh;

		private LabelControl lblHoTen;

		private LayoutControlItem layoutControlItem13;

		private LayoutControlItem layoutControlItem14;

		private LayoutControlItem layoutControlItem15;

		private LayoutControlItem layoutControlItem16;

		private LayoutControlItem layoutControlItem17;

		private LayoutControlItem layoutControlItem18;

		private LayoutControlItem layoutControlItem19;

		private LayoutControlItem layoutControlItem20;

		private LayoutControlItem layoutControlItem21;

		private LayoutControlItem layoutControlItem22;

		private LayoutControlItem layoutControlItem2;

		public frmCheckHeinCardGOV()
		{
			InitializeComponent();
		}

		public frmCheckHeinCardGOV(ResultHistoryLDO resultHistoryLDO)
		{
			InitializeComponent();
			try
			{
				this.resultHistoryLDO = resultHistoryLDO;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void CheckHeinCardGOV_Load(object sender, EventArgs e)
		{
			try
			{
				LoadInfo();
				LoadDataGridControl();
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
				ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.RegisterV2.Resources.Lang", typeof(frmCheckHeinCardGOV).Assembly);
				layoutControl1.Text = Get.Value("frmCheckHeinCardGOV.layoutControl1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn1.Caption = Get.Value("frmCheckHeinCardGOV.gridColumn1.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn2.Caption = Get.Value("frmCheckHeinCardGOV.gridColumn2.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn3.Caption = Get.Value("frmCheckHeinCardGOV.gridColumn3.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn4.Caption = Get.Value("frmCheckHeinCardGOV.gridColumn4.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn5.Caption = Get.Value("frmCheckHeinCardGOV.gridColumn5.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn6.Caption = Get.Value("frmCheckHeinCardGOV.gridColumn6.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn7.Caption = Get.Value("frmCheckHeinCardGOV.gridColumn7.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn8.Caption = Get.Value("frmCheckHeinCardGOV.gridColumn8.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem13.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem13.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem14.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem14.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem15.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem15.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem16.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem16.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem17.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem17.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem18.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem18.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem19.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem19.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem20.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem20.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem21.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem21.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem22.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem22.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem2.Text = Get.Value("frmCheckHeinCardGOV.layoutControlItem2.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				Text = Get.Value("frmCheckHeinCardGOV.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadInfo()
		{
			try
			{
				lblCoQuanBHXH.Text = resultHistoryLDO.cqBHXH;
				lblDiaChi.Text = resultHistoryLDO.diaChi;
				lblGiaTriTheDen.Text = resultHistoryLDO.gtTheDen;
				lblGiaTriTheTu.Text = resultHistoryLDO.gtTheTu;
				lblGioiTinh.Text = resultHistoryLDO.gioiTinh;
				lblHoTen.Text = resultHistoryLDO.hoTen;
				lblMaDKBD.Text = resultHistoryLDO.maDKBD;
				lblMaKhuVuc.Text = resultHistoryLDO.maKV;
				lblNgayDu5Nam.Text = resultHistoryLDO.ngayDu5Nam;
				lblMaKetQua.Text = resultHistoryLDO.maKetQua;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadDataGridControl()
		{
			try
			{
				gridControlHistory.BeginUpdate();
				gridControlHistory.DataSource = resultHistoryLDO.dsLichSuKCB2018;
				gridControlHistory.EndUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
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

		private void btnContinue_Click(object sender, EventArgs e)
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

		private void gridViewHistory_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			try
			{
				if (!e.IsGetData || e.Column.UnboundType == UnboundColumnType.Bound)
				{
					return;
				}
				ExamHistoryLDO data = (ExamHistoryLDO)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
				if (data == null)
				{
					return;
				}
				if (e.Column.FieldName == "tinhTrang_str")
				{
					if (data.tinhTrang == "1")
					{
						e.Value = "Ra viện";
					}
					else if (data.tinhTrang == "2")
					{
						e.Value = "Chuyển viện";
					}
					else if (data.tinhTrang == "3")
					{
						e.Value = "Trốn viện";
					}
					else if (data.tinhTrang == "4")
					{
						e.Value = "Xin ra viện";
					}
				}
				else if (e.Column.FieldName == "kqDieuTri_str")
				{
					if (data.kqDieuTri == "1")
					{
						e.Value = "Khỏi";
					}
					else if (data.kqDieuTri == "2")
					{
						e.Value = "Đỡ";
					}
					else if (data.kqDieuTri == "3")
					{
						e.Value = "Không thay đổi";
					}
					else if (data.kqDieuTri == "4")
					{
						e.Value = "Nặng hơn";
					}
					else if (data.kqDieuTri == "5")
					{
						e.Value = "Tử vong";
					}
				}
				else if (e.Column.FieldName == "cskcbbd_name")
				{
					HIS_MEDI_ORG hIS_MEDI_ORG = BackendDataWorker.Get<HIS_MEDI_ORG>().FirstOrDefault((HIS_MEDI_ORG o) => o.MEDI_ORG_CODE == data.maCSKCB);
					e.Value = ((hIS_MEDI_ORG != null) ? hIS_MEDI_ORG.MEDI_ORG_NAME : "");
				}
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
				resultHistoryLDO = null;
				gridViewHistory.CustomUnboundColumnData -= new CustomColumnDataEventHandler(gridViewHistory_CustomUnboundColumnData);
				base.Load -= new EventHandler(CheckHeinCardGOV_Load);
				gridViewHistory.GridControl.DataSource = null;
				gridControlHistory.DataSource = null;
				layoutControlItem2 = null;
				layoutControlItem22 = null;
				layoutControlItem21 = null;
				layoutControlItem20 = null;
				layoutControlItem19 = null;
				layoutControlItem18 = null;
				layoutControlItem17 = null;
				layoutControlItem16 = null;
				layoutControlItem15 = null;
				layoutControlItem14 = null;
				layoutControlItem13 = null;
				lblHoTen = null;
				lblGioiTinh = null;
				lblDiaChi = null;
				lblMaKhuVuc = null;
				lblCoQuanBHXH = null;
				lblMaDKBD = null;
				lblGiaTriTheTu = null;
				lblGiaTriTheDen = null;
				lblNgayDu5Nam = null;
				lblMaKetQua = null;
				labelControl1 = null;
				gridColumn8 = null;
				gridColumn7 = null;
				gridColumn6 = null;
				gridColumn5 = null;
				gridColumn4 = null;
				gridColumn3 = null;
				gridColumn2 = null;
				gridColumn1 = null;
				layoutControlItem1 = null;
				gridViewHistory = null;
				gridControlHistory = null;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HIS.Desktop.Plugins.RegisterV2.frmCheckHeinCardGOV));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.lblMaKetQua = new DevExpress.XtraEditors.LabelControl();
			this.lblNgayDu5Nam = new DevExpress.XtraEditors.LabelControl();
			this.lblGiaTriTheDen = new DevExpress.XtraEditors.LabelControl();
			this.lblGiaTriTheTu = new DevExpress.XtraEditors.LabelControl();
			this.lblMaDKBD = new DevExpress.XtraEditors.LabelControl();
			this.lblCoQuanBHXH = new DevExpress.XtraEditors.LabelControl();
			this.lblMaKhuVuc = new DevExpress.XtraEditors.LabelControl();
			this.lblDiaChi = new DevExpress.XtraEditors.LabelControl();
			this.lblGioiTinh = new DevExpress.XtraEditors.LabelControl();
			this.lblHoTen = new DevExpress.XtraEditors.LabelControl();
			this.gridControlHistory = new DevExpress.XtraGrid.GridControl();
			this.gridViewHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.gridControlHistory).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridViewHistory).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem13).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem14).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem15).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem16).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem17).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem18).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem19).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem20).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem21).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem22).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.labelControl1);
			this.layoutControl1.Controls.Add(this.lblMaKetQua);
			this.layoutControl1.Controls.Add(this.lblNgayDu5Nam);
			this.layoutControl1.Controls.Add(this.lblGiaTriTheDen);
			this.layoutControl1.Controls.Add(this.lblGiaTriTheTu);
			this.layoutControl1.Controls.Add(this.lblMaDKBD);
			this.layoutControl1.Controls.Add(this.lblCoQuanBHXH);
			this.layoutControl1.Controls.Add(this.lblMaKhuVuc);
			this.layoutControl1.Controls.Add(this.lblDiaChi);
			this.layoutControl1.Controls.Add(this.lblGioiTinh);
			this.layoutControl1.Controls.Add(this.lblHoTen);
			this.layoutControl1.Controls.Add(this.gridControlHistory);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(884, 401);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl1.Location = new System.Drawing.Point(187, 122);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(695, 20);
			this.labelControl1.StyleController = this.layoutControl1;
			this.labelControl1.TabIndex = 16;
			this.lblMaKetQua.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblMaKetQua.Location = new System.Drawing.Point(526, 98);
			this.lblMaKetQua.Name = "lblMaKetQua";
			this.lblMaKetQua.Size = new System.Drawing.Size(356, 20);
			this.lblMaKetQua.StyleController = this.layoutControl1;
			this.lblMaKetQua.TabIndex = 15;
			this.lblNgayDu5Nam.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblNgayDu5Nam.Location = new System.Drawing.Point(97, 98);
			this.lblNgayDu5Nam.Name = "lblNgayDu5Nam";
			this.lblNgayDu5Nam.Size = new System.Drawing.Size(330, 20);
			this.lblNgayDu5Nam.StyleController = this.layoutControl1;
			this.lblNgayDu5Nam.TabIndex = 14;
			this.lblGiaTriTheDen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblGiaTriTheDen.Location = new System.Drawing.Point(526, 74);
			this.lblGiaTriTheDen.Name = "lblGiaTriTheDen";
			this.lblGiaTriTheDen.Size = new System.Drawing.Size(356, 20);
			this.lblGiaTriTheDen.StyleController = this.layoutControl1;
			this.lblGiaTriTheDen.TabIndex = 13;
			this.lblGiaTriTheTu.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblGiaTriTheTu.Location = new System.Drawing.Point(97, 74);
			this.lblGiaTriTheTu.Name = "lblGiaTriTheTu";
			this.lblGiaTriTheTu.Size = new System.Drawing.Size(330, 20);
			this.lblGiaTriTheTu.StyleController = this.layoutControl1;
			this.lblGiaTriTheTu.TabIndex = 12;
			this.lblMaDKBD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblMaDKBD.Location = new System.Drawing.Point(526, 50);
			this.lblMaDKBD.Name = "lblMaDKBD";
			this.lblMaDKBD.Size = new System.Drawing.Size(356, 20);
			this.lblMaDKBD.StyleController = this.layoutControl1;
			this.lblMaDKBD.TabIndex = 11;
			this.lblCoQuanBHXH.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblCoQuanBHXH.Location = new System.Drawing.Point(97, 50);
			this.lblCoQuanBHXH.Name = "lblCoQuanBHXH";
			this.lblCoQuanBHXH.Size = new System.Drawing.Size(330, 20);
			this.lblCoQuanBHXH.StyleController = this.layoutControl1;
			this.lblCoQuanBHXH.TabIndex = 10;
			this.lblMaKhuVuc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblMaKhuVuc.Location = new System.Drawing.Point(526, 26);
			this.lblMaKhuVuc.Name = "lblMaKhuVuc";
			this.lblMaKhuVuc.Size = new System.Drawing.Size(356, 20);
			this.lblMaKhuVuc.StyleController = this.layoutControl1;
			this.lblMaKhuVuc.TabIndex = 9;
			this.lblDiaChi.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblDiaChi.Location = new System.Drawing.Point(97, 26);
			this.lblDiaChi.Name = "lblDiaChi";
			this.lblDiaChi.Size = new System.Drawing.Size(330, 20);
			this.lblDiaChi.StyleController = this.layoutControl1;
			this.lblDiaChi.TabIndex = 8;
			this.lblGioiTinh.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblGioiTinh.Location = new System.Drawing.Point(526, 2);
			this.lblGioiTinh.Name = "lblGioiTinh";
			this.lblGioiTinh.Size = new System.Drawing.Size(356, 20);
			this.lblGioiTinh.StyleController = this.layoutControl1;
			this.lblGioiTinh.TabIndex = 7;
			this.lblHoTen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblHoTen.Location = new System.Drawing.Point(97, 2);
			this.lblHoTen.Name = "lblHoTen";
			this.lblHoTen.Size = new System.Drawing.Size(330, 20);
			this.lblHoTen.StyleController = this.layoutControl1;
			this.lblHoTen.TabIndex = 6;
			this.gridControlHistory.Location = new System.Drawing.Point(2, 146);
			this.gridControlHistory.MainView = this.gridViewHistory;
			this.gridControlHistory.Name = "gridControlHistory";
			this.gridControlHistory.Size = new System.Drawing.Size(880, 253);
			this.gridControlHistory.TabIndex = 4;
			this.gridControlHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridViewHistory });
			this.gridViewHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[8] { this.gridColumn1, this.gridColumn2, this.gridColumn3, this.gridColumn4, this.gridColumn5, this.gridColumn6, this.gridColumn7, this.gridColumn8 });
			this.gridViewHistory.GridControl = this.gridControlHistory;
			this.gridViewHistory.Name = "gridViewHistory";
			this.gridViewHistory.OptionsView.ShowGroupPanel = false;
			this.gridViewHistory.OptionsView.ShowIndicator = false;
			this.gridViewHistory.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridViewHistory_CustomUnboundColumnData);
			this.gridColumn1.Caption = "Mã hồ sơ";
			this.gridColumn1.FieldName = "maHoSo";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 72;
			this.gridColumn2.Caption = "Mã CSKCBBĐ";
			this.gridColumn2.FieldName = "maCSKCB";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 88;
			this.gridColumn3.Caption = "Tên CSKCBBĐ";
			this.gridColumn3.FieldName = "cskcbbd_name";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn3.Width = 170;
			this.gridColumn4.Caption = "Từ ngày";
			this.gridColumn4.FieldName = "tuNgay";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 3;
			this.gridColumn4.Width = 76;
			this.gridColumn5.Caption = "Đến ngày";
			this.gridColumn5.FieldName = "denNgay";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 4;
			this.gridColumn5.Width = 74;
			this.gridColumn6.Caption = "Tên bệnh";
			this.gridColumn6.FieldName = "tenBenh";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 5;
			this.gridColumn6.Width = 164;
			this.gridColumn7.Caption = "Tình trạng";
			this.gridColumn7.FieldName = "tinhTrang_str";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 6;
			this.gridColumn7.Width = 86;
			this.gridColumn8.Caption = "Kết quả";
			this.gridColumn8.FieldName = "kqDieuTri_str";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn8.Width = 148;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[12]
			{
				this.layoutControlItem1, this.layoutControlItem13, this.layoutControlItem14, this.layoutControlItem15, this.layoutControlItem16, this.layoutControlItem17, this.layoutControlItem18, this.layoutControlItem19, this.layoutControlItem20, this.layoutControlItem21,
				this.layoutControlItem22, this.layoutControlItem2
			});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(884, 401);
			this.layoutControlItem1.Control = this.gridControlHistory;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 144);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(884, 257);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem13.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem13.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem13.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem13.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem13.Control = this.lblHoTen;
			this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem13.Name = "layoutControlItem13";
			this.layoutControlItem13.Size = new System.Drawing.Size(429, 24);
			this.layoutControlItem13.Text = "Họ tên:";
			this.layoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem13.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem13.TextToControlDistance = 5;
			this.layoutControlItem14.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem14.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem14.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem14.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem14.Control = this.lblGioiTinh;
			this.layoutControlItem14.Location = new System.Drawing.Point(429, 0);
			this.layoutControlItem14.Name = "layoutControlItem14";
			this.layoutControlItem14.Size = new System.Drawing.Size(455, 24);
			this.layoutControlItem14.Text = "Giới tính:";
			this.layoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem14.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem14.TextToControlDistance = 5;
			this.layoutControlItem15.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem15.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem15.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem15.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem15.Control = this.lblDiaChi;
			this.layoutControlItem15.Location = new System.Drawing.Point(0, 24);
			this.layoutControlItem15.Name = "layoutControlItem15";
			this.layoutControlItem15.Size = new System.Drawing.Size(429, 24);
			this.layoutControlItem15.Text = "Địa chỉ:";
			this.layoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem15.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem15.TextToControlDistance = 5;
			this.layoutControlItem16.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem16.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem16.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem16.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem16.Control = this.lblMaKhuVuc;
			this.layoutControlItem16.Location = new System.Drawing.Point(429, 24);
			this.layoutControlItem16.Name = "layoutControlItem16";
			this.layoutControlItem16.Size = new System.Drawing.Size(455, 24);
			this.layoutControlItem16.Text = "Mã khu vực:";
			this.layoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem16.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem16.TextToControlDistance = 5;
			this.layoutControlItem17.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem17.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem17.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem17.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem17.Control = this.lblCoQuanBHXH;
			this.layoutControlItem17.Location = new System.Drawing.Point(0, 48);
			this.layoutControlItem17.Name = "layoutControlItem17";
			this.layoutControlItem17.Size = new System.Drawing.Size(429, 24);
			this.layoutControlItem17.Text = "Cơ quan BHXH:";
			this.layoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem17.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem17.TextToControlDistance = 5;
			this.layoutControlItem18.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem18.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem18.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem18.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem18.Control = this.lblMaDKBD;
			this.layoutControlItem18.Location = new System.Drawing.Point(429, 48);
			this.layoutControlItem18.Name = "layoutControlItem18";
			this.layoutControlItem18.Size = new System.Drawing.Size(455, 24);
			this.layoutControlItem18.Text = "Mã ĐKBĐ:";
			this.layoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem18.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem18.TextToControlDistance = 5;
			this.layoutControlItem19.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem19.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem19.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem19.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem19.Control = this.lblGiaTriTheTu;
			this.layoutControlItem19.Location = new System.Drawing.Point(0, 72);
			this.layoutControlItem19.Name = "layoutControlItem19";
			this.layoutControlItem19.Size = new System.Drawing.Size(429, 24);
			this.layoutControlItem19.Text = "Giá trị thẻ từ:";
			this.layoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem19.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem19.TextToControlDistance = 5;
			this.layoutControlItem20.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem20.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem20.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem20.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem20.Control = this.lblGiaTriTheDen;
			this.layoutControlItem20.Location = new System.Drawing.Point(429, 72);
			this.layoutControlItem20.Name = "layoutControlItem20";
			this.layoutControlItem20.Size = new System.Drawing.Size(455, 24);
			this.layoutControlItem20.Text = "Giá trị thẻ đến:";
			this.layoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem20.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem20.TextToControlDistance = 5;
			this.layoutControlItem21.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem21.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem21.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem21.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem21.Control = this.lblNgayDu5Nam;
			this.layoutControlItem21.Location = new System.Drawing.Point(0, 96);
			this.layoutControlItem21.Name = "layoutControlItem21";
			this.layoutControlItem21.Size = new System.Drawing.Size(429, 24);
			this.layoutControlItem21.Text = "Ngày đủ 5 năm:";
			this.layoutControlItem21.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem21.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem21.TextToControlDistance = 5;
			this.layoutControlItem22.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
			this.layoutControlItem22.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem22.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem22.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem22.Control = this.lblMaKetQua;
			this.layoutControlItem22.Location = new System.Drawing.Point(429, 96);
			this.layoutControlItem22.Name = "layoutControlItem22";
			this.layoutControlItem22.Size = new System.Drawing.Size(455, 24);
			this.layoutControlItem22.Text = "Mã kết quả:";
			this.layoutControlItem22.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem22.TextSize = new System.Drawing.Size(90, 20);
			this.layoutControlItem22.TextToControlDistance = 5;
			this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10f, System.Drawing.FontStyle.Bold);
			this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
			this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem2.Control = this.labelControl1;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 120);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(884, 24);
			this.layoutControlItem2.Text = "Lịch sử khám chữa bệnh";
			this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(180, 20);
			this.layoutControlItem2.TextToControlDistance = 5;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(884, 401);
			base.Controls.Add(this.layoutControl1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "frmCheckHeinCardGOV";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lịch sử khám chữa bệnh";
			base.Load += new System.EventHandler(CheckHeinCardGOV_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.gridControlHistory).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridViewHistory).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem13).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem14).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem15).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem16).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem17).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem18).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem19).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem20).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem21).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem22).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			base.ResumeLayout(false);
		}
	}
}
