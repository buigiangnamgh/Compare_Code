using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HID.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Plugins.RegisterV2.ADO;
using HIS.Desktop.Utility;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RegisterV2.Choice
{
	public class frmPersonSelect : FormBase
	{
		private List<PersonADO> data;

		private SelectPerson dlgSelectPerson;

		private Dictionary<long, string> dicGender;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LabelControl lblDescription;

		private SimpleButton btnClose;

		private GridControl grdInformation;

		private GridView gridView1;

		private GridColumn grdChoose;

		private RepositoryItemCheckEdit repositoryItemCheckEdit1;

		private GridColumn grdCode;

		private GridColumn grdName;

		private GridColumn grdDate;

		private GridColumn grdGender;

		private GridColumn grdAddress;

		private RepositoryItemRadioGroup radianChoose;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControlItem lciPatientInformation;

		private LayoutControlItem layoutControlItem2;

		private LayoutControlItem layoutControlItem3;

		private GridColumn gridColumn5;

		private GridColumn gridColumn4;

		private GridColumn gridColumn3;

		private GridColumn gridColumn2;

		private GridColumn gridColumn1;

		private GridColumn gridColumn6;

		private GridColumn gridColumn16;

		private GridColumn gridColumn15;

		private GridColumn gridColumn14;

		private GridColumn gridColumn13;

		private GridColumn gridColumn12;

		private GridColumn gridColumn11;

		private GridColumn gridColumn10;

		private GridColumn gridColumn9;

		private GridColumn gridColumn8;

		private GridColumn gridColumn7;

		private GridColumn gridColumn17;

		private RepositoryItemCheckEdit repositoryItemCheckEdit2;

		public frmPersonSelect(List<HID_PERSON> paramData, SelectPerson selectPerson)
		{
			InitializeComponent();
			if (paramData != null && paramData.Count > 0)
			{
				data = paramData.Select((HID_PERSON m) => new PersonADO(m)).ToList();
			}
			dlgSelectPerson = selectPerson;
		}

		private void frmPersonSelect_Load(object sender, EventArgs e)
		{
			try
			{
				WaitingManager.Show();
				dicGender = new Dictionary<long, string>();
				List<HIS_GENDER> list = BackendDataWorker.Get<HIS_GENDER>();
				if (list != null)
				{
					foreach (HIS_GENDER item in list)
					{
						dicGender.Add(item.ID, item.GENDER_NAME);
					}
				}
				gridView1.BeginUpdate();
				gridView1.GridControl.DataSource = data;
				gridView1.EndUpdate();
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			try
			{
				if (!e.IsGetData || e.Column.UnboundType == UnboundColumnType.Bound)
				{
					return;
				}
				GridView gridView = sender as GridView;
				PersonADO personADO = (PersonADO)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
				if (e.Column.FieldName == "DOB_DISPLAY")
				{
					try
					{
						e.Value = Inventec.Common.DateTime.Convert.TimeNumberToDateString(personADO.DOB);
						return;
					}
					catch (Exception ex)
					{
						LogSystem.Warn("Loi set gia tri cho cot ngay tao CREATE_TIME", ex);
						return;
					}
				}
				if (e.Column.FieldName == "GENDER_NAME")
				{
					try
					{
						e.Value = dicGender[personADO.GENDER_ID];
						return;
					}
					catch (Exception ex2)
					{
						LogSystem.Warn("Loi set gia tri cho cot GENDER_NAME", ex2);
						return;
					}
				}
			}
			catch (Exception ex3)
			{
				LogSystem.Warn(ex3);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void gridView1_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					return;
				}
				GridView gridView = sender as GridView;
				GridHitInfo gridHitInfo = gridView.CalcHitInfo(e.Location);
				if (!gridHitInfo.InRowCell || !(gridHitInfo.Column.RealColumnEdit.GetType() == typeof(RepositoryItemCheckEdit)))
				{
					return;
				}
				gridView.FocusedRowHandle = gridHitInfo.RowHandle;
				gridView.FocusedColumn = gridHitInfo.Column;
				gridView.ShowEditor();
				CheckEdit checkEdit = gridView.ActiveEditor as CheckEdit;
				CheckEditViewInfo checkEditViewInfo = (CheckEditViewInfo)checkEdit.GetViewInfo();
				Rectangle glyphRect = checkEditViewInfo.CheckInfo.GlyphRect;
				GridViewInfo gridViewInfo = gridView.GetViewInfo() as GridViewInfo;
				Rectangle rectangle = new Rectangle(gridViewInfo.GetGridCellInfo(gridHitInfo).Bounds.X + glyphRect.X, gridViewInfo.GetGridCellInfo(gridHitInfo).Bounds.Y + glyphRect.Y, glyphRect.Width, glyphRect.Height);
				if (!rectangle.Contains(e.Location))
				{
					gridView.CloseEditor();
					if (!gridView.IsCellSelected(gridHitInfo.RowHandle, gridHitInfo.Column))
					{
						gridView.SelectCell(gridHitInfo.RowHandle, gridHitInfo.Column);
					}
					else
					{
						gridView.UnselectCell(gridHitInfo.RowHandle, gridHitInfo.Column);
					}
				}
				else
				{
					checkEdit.Checked = !checkEdit.Checked;
					gridView.CloseEditor();
				}
				(e as DXMouseEventArgs).Handled = true;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessSelectedPerson(ref PersonADO patient)
		{
			try
			{
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void grdInformation_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				PersonADO patient = (PersonADO)gridView1.GetFocusedRow();
				if (patient != null)
				{
					ProcessSelectedPerson(ref patient);
					if (dlgSelectPerson != null)
					{
						dlgSelectPerson(patient);
					}
					Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void grdInformation_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode != Keys.Return)
				{
					return;
				}
				PersonADO patient = (PersonADO)gridView1.GetFocusedRow();
				if (patient != null)
				{
					ProcessSelectedPerson(ref patient);
					if (dlgSelectPerson != null)
					{
						dlgSelectPerson(patient);
					}
					Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HIS.Desktop.Plugins.RegisterV2.Choice.frmPersonSelect));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.lblDescription = new DevExpress.XtraEditors.LabelControl();
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			this.grdInformation = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.grdChoose = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.grdCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.grdName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.grdDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.grdGender = new DevExpress.XtraGrid.Columns.GridColumn();
			this.grdAddress = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.radianChoose = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lciPatientInformation = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.grdInformation).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.radianChoose).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciPatientInformation).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.lblDescription);
			this.layoutControl1.Controls.Add(this.btnClose);
			this.layoutControl1.Controls.Add(this.grdInformation);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(456, 128, 250, 350);
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1114, 461);
			this.layoutControl1.TabIndex = 1;
			this.layoutControl1.Text = "layoutControl1";
			this.lblDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblDescription.Location = new System.Drawing.Point(2, 437);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(1035, 13);
			this.lblDescription.StyleController = this.layoutControl1;
			this.lblDescription.TabIndex = 7;
			this.lblDescription.Text = "Chọn hồ sơ sức khỏe cá nhân bằng cách bấm enter hoặc nháy đúp chuột vào dòng hồ sơ. Thêm hồ sơ mới chọn Bỏ qua";
			this.btnClose.Location = new System.Drawing.Point(1041, 437);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(71, 22);
			this.btnClose.StyleController = this.layoutControl1;
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "Bỏ qua";
			this.btnClose.Click += new System.EventHandler(btnClose_Click);
			this.grdInformation.Location = new System.Drawing.Point(2, 2);
			this.grdInformation.MainView = this.gridView1;
			this.grdInformation.Name = "grdInformation";
			this.grdInformation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[3] { this.radianChoose, this.repositoryItemCheckEdit1, this.repositoryItemCheckEdit2 });
			this.grdInformation.Size = new System.Drawing.Size(1110, 431);
			this.grdInformation.TabIndex = 4;
			this.grdInformation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.grdInformation.DoubleClick += new System.EventHandler(grdInformation_DoubleClick);
			this.grdInformation.KeyDown += new System.Windows.Forms.KeyEventHandler(grdInformation_KeyDown);
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[23]
			{
				this.grdChoose, this.grdCode, this.grdName, this.grdDate, this.grdGender, this.grdAddress, this.gridColumn17, this.gridColumn5, this.gridColumn4, this.gridColumn3,
				this.gridColumn6, this.gridColumn2, this.gridColumn1, this.gridColumn16, this.gridColumn15, this.gridColumn14, this.gridColumn13, this.gridColumn12, this.gridColumn11, this.gridColumn10,
				this.gridColumn9, this.gridColumn8, this.gridColumn7
			});
			this.gridView1.GridControl = this.grdInformation;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ColumnAutoWidth = false;
			this.gridView1.OptionsView.ShowDetailButtons = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.OptionsView.ShowIndicator = false;
			this.gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
			this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(gridView1_MouseDown);
			this.grdChoose.AppearanceCell.Options.UseTextOptions = true;
			this.grdChoose.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.grdChoose.Caption = "Chọn";
			this.grdChoose.ColumnEdit = this.repositoryItemCheckEdit1;
			this.grdChoose.Name = "grdChoose";
			this.grdChoose.OptionsColumn.ShowCaption = false;
			this.grdChoose.Width = 30;
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			this.grdCode.Caption = "Mã y tế";
			this.grdCode.FieldName = "PERSON_CODE";
			this.grdCode.Name = "grdCode";
			this.grdCode.OptionsColumn.AllowEdit = false;
			this.grdCode.Visible = true;
			this.grdCode.VisibleIndex = 0;
			this.grdCode.Width = 116;
			this.grdName.Caption = "Họ tên";
			this.grdName.FieldName = "VIR_PERSON_NAME";
			this.grdName.Name = "grdName";
			this.grdName.OptionsColumn.AllowEdit = false;
			this.grdName.Visible = true;
			this.grdName.VisibleIndex = 1;
			this.grdName.Width = 150;
			this.grdDate.AppearanceCell.Options.UseTextOptions = true;
			this.grdDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.grdDate.Caption = "Ngày sinh";
			this.grdDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.grdDate.FieldName = "DOB_DISPLAY";
			this.grdDate.Name = "grdDate";
			this.grdDate.OptionsColumn.AllowEdit = false;
			this.grdDate.Tag = new System.DateTime(2016, 10, 1, 11, 58, 9, 631);
			this.grdDate.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.grdDate.Visible = true;
			this.grdDate.VisibleIndex = 3;
			this.grdDate.Width = 120;
			this.grdGender.Caption = "Giới tính";
			this.grdGender.FieldName = "GENDER_NAME";
			this.grdGender.Name = "grdGender";
			this.grdGender.OptionsColumn.AllowEdit = false;
			this.grdGender.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.grdGender.Visible = true;
			this.grdGender.VisibleIndex = 4;
			this.grdGender.Width = 74;
			this.grdAddress.Caption = "Địa chỉ";
			this.grdAddress.FieldName = "VIR_ADDRESS";
			this.grdAddress.Name = "grdAddress";
			this.grdAddress.OptionsColumn.AllowEdit = false;
			this.grdAddress.Visible = true;
			this.grdAddress.VisibleIndex = 7;
			this.grdAddress.Width = 300;
			this.gridColumn17.Caption = "Có GKS";
			this.gridColumn17.ColumnEdit = this.repositoryItemCheckEdit2;
			this.gridColumn17.FieldName = "IS_HAS_NOT_DAY_DOB";
			this.gridColumn17.Name = "gridColumn17";
			this.gridColumn17.OptionsColumn.AllowEdit = false;
			this.gridColumn17.Visible = true;
			this.gridColumn17.VisibleIndex = 6;
			this.gridColumn17.Width = 47;
			this.repositoryItemCheckEdit2.AutoHeight = false;
			this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
			this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			this.repositoryItemCheckEdit2.ReadOnly = true;
			this.gridColumn5.Caption = "Tỉnh khai sinh";
			this.gridColumn5.FieldName = "BORN_PROVINCE_NAME";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 2;
			this.gridColumn4.Caption = "Họ tên bố";
			this.gridColumn4.FieldName = "FATHER_NAME";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 8;
			this.gridColumn3.Caption = "Họ tên mẹ";
			this.gridColumn3.FieldName = "MOTHER_NAME";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 9;
			this.gridColumn6.Caption = "Số căn cước/CMND";
			this.gridColumn6.FieldName = "CCCD_NUMBER_CMND_NUMBER";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.OptionsColumn.AllowEdit = false;
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 5;
			this.gridColumn6.Width = 104;
			this.gridColumn2.Caption = "Ngày cấp";
			this.gridColumn2.FieldName = "CCCD_DATE_CMND_DATE";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 10;
			this.gridColumn1.Caption = "Nới cấp";
			this.gridColumn1.FieldName = "CCCD_PLACE_CMND_PLACE";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 11;
			this.gridColumn16.Caption = "Số thẻ BHYT";
			this.gridColumn16.FieldName = "BHYT_NUMBER";
			this.gridColumn16.Name = "gridColumn16";
			this.gridColumn16.OptionsColumn.AllowEdit = false;
			this.gridColumn16.Visible = true;
			this.gridColumn16.VisibleIndex = 12;
			this.gridColumn15.Caption = "Nghề nghiệp";
			this.gridColumn15.FieldName = "CAREER_NAME";
			this.gridColumn15.Name = "gridColumn15";
			this.gridColumn15.OptionsColumn.AllowEdit = false;
			this.gridColumn15.Visible = true;
			this.gridColumn15.VisibleIndex = 14;
			this.gridColumn14.Caption = "Dân tộc";
			this.gridColumn14.FieldName = "ETHNIC_NAME";
			this.gridColumn14.Name = "gridColumn14";
			this.gridColumn14.OptionsColumn.AllowEdit = false;
			this.gridColumn14.Visible = true;
			this.gridColumn14.VisibleIndex = 15;
			this.gridColumn13.Caption = "Tôn giáo";
			this.gridColumn13.FieldName = "RELIGION_NAME";
			this.gridColumn13.Name = "gridColumn13";
			this.gridColumn13.OptionsColumn.AllowEdit = false;
			this.gridColumn13.Visible = true;
			this.gridColumn13.VisibleIndex = 16;
			this.gridColumn12.Caption = "Địa chỉ thường trú";
			this.gridColumn12.FieldName = "VIR_HT_ADDRESS";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.OptionsColumn.AllowEdit = false;
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 17;
			this.gridColumn11.Caption = "Người chăm sóc";
			this.gridColumn11.FieldName = "NCSC_NAME";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.OptionsColumn.AllowEdit = false;
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 18;
			this.gridColumn10.Caption = "Quan hệ";
			this.gridColumn10.FieldName = "NCSC_RELATION";
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.OptionsColumn.AllowEdit = false;
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 19;
			this.gridColumn9.Caption = "Số căn cước/CMND người chăm sóc";
			this.gridColumn9.FieldName = "NCSC_CMND_CCCD_NUMBER";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 20;
			this.gridColumn8.Caption = "Điện thoại người chăm sóc";
			this.gridColumn8.FieldName = "NCSC_MOBILE";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.OptionsColumn.AllowEdit = false;
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 21;
			this.gridColumn7.Caption = "Địa chỉ người chăm sóc";
			this.gridColumn7.FieldName = "NCSC_ADDRESS";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 13;
			this.radianChoose.Name = "radianChoose";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3] { this.lciPatientInformation, this.layoutControlItem2, this.layoutControlItem3 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1114, 461);
			this.layoutControlGroup1.TextVisible = false;
			this.lciPatientInformation.Control = this.grdInformation;
			this.lciPatientInformation.Location = new System.Drawing.Point(0, 0);
			this.lciPatientInformation.Name = "lciPatientInformation";
			this.lciPatientInformation.Size = new System.Drawing.Size(1114, 435);
			this.lciPatientInformation.TextSize = new System.Drawing.Size(0, 0);
			this.lciPatientInformation.TextVisible = false;
			this.layoutControlItem2.Control = this.btnClose;
			this.layoutControlItem2.Location = new System.Drawing.Point(1039, 435);
			this.layoutControlItem2.MaxSize = new System.Drawing.Size(75, 26);
			this.layoutControlItem2.MinSize = new System.Drawing.Size(70, 26);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(75, 26);
			this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.layoutControlItem3.Control = this.lblDescription;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 435);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(1039, 26);
			this.layoutControlItem3.Text = resources.GetString("layoutControlItem3.Text");
			this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextToControlDistance = 0;
			this.layoutControlItem3.TextVisible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1114, 461);
			base.Controls.Add(this.layoutControl1);
			base.Name = "frmPersonSelect";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chọn hồ sơ sức khỏe cá nhân";
			base.Load += new System.EventHandler(frmPersonSelect_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.grdInformation).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.radianChoose).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciPatientInformation).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			base.ResumeLayout(false);
		}
	}
}
