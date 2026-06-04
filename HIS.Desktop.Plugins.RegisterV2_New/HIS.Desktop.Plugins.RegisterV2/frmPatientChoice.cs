using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
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
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Plugins.RegisterV2.Resources;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.SDO;

namespace HIS.Desktop.Plugins.RegisterV2
{
	public class frmPatientChoice : FormBase
	{
		private UpdatePatientInfo updatePatientInfo;

		private List<HisPatientSDO> currentListPatient;

		private Dictionary<long, string> dicGender;

		private Dictionary<long, string> dicWorkPlace;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private SimpleButton btnClose;

		private GridControl grdInformation;

		private GridView gridView1;

		private GridColumn grdChoose;

		private GridColumn grdName;

		private GridColumn grdDate;

		private GridColumn grdGender;

		private GridColumn grdAddress;

		private RepositoryItemRadioGroup radianChoose;

		private LayoutControlItem lciPatientInformation;

		private LayoutControlItem layoutControlItem2;

		private RepositoryItemCheckEdit repositoryItemCheckEdit1;

		private GridColumn grdCode;

		private LabelControl lblDescription;

		private LayoutControlItem layoutControlItem3;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private GridColumn gridColumn3;

		public frmPatientChoice(List<HisPatientSDO> currentListPatient, UpdatePatientInfo updatePatientInfo)
		{
			this.currentListPatient = currentListPatient;
			this.updatePatientInfo = updatePatientInfo;
			InitializeComponent();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void PopupPatientInformation_Load(object sender, EventArgs e)
		{
			try
			{
				WaitingManager.Show();
				SetCaptionByLanguageKeyNew();
				dicGender = new Dictionary<long, string>();
				dicWorkPlace = new Dictionary<long, string>();
				List<HIS_GENDER> list = BackendDataWorker.Get<HIS_GENDER>();
				if (list != null)
				{
					foreach (HIS_GENDER item in list)
					{
						dicGender.Add(item.ID, item.GENDER_NAME);
					}
				}
				List<HIS_WORK_PLACE> list2 = BackendDataWorker.Get<HIS_WORK_PLACE>();
				if (list2 != null)
				{
					foreach (HIS_WORK_PLACE item2 in list2)
					{
						dicWorkPlace.Add(item2.ID, item2.WORK_PLACE_NAME);
					}
				}
				grdInformation.DataSource = currentListPatient;
				gridView1.FocusedRowHandle = 0;
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				WaitingManager.Hide();
			}
		}

		private void SetCaptionByLanguageKey()
		{
			try
			{
				ResourceLanguageManager.LanguagefrmPatientChoice = new ResourceManager("HIS.Desktop.Plugins.RegisterV2.Resources.Lang", typeof(frmPatientChoice).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.layoutControl1.Text", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				lblDescription.Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.lblDescription.Text", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				btnClose.Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.btnClose.Text", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				grdChoose.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdChoose.Caption", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				grdCode.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdCode.Caption", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				grdName.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdName.Caption", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				grdDate.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdDate.Caption", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				grdGender.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdGender.Caption", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				grdAddress.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdAddress.Caption", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				layoutControlItem3.Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.layoutControlItem3.Text", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
				Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.Text", ResourceLanguageManager.LanguagefrmPatientChoice, LanguageManager.GetCulture());
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetCaptionByLanguageKeyNew()
		{
			try
			{
				ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.RegisterV2.Resources.Lang", typeof(frmPatientChoice).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.layoutControl1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				lblDescription.Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.lblDescription.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnClose.Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.btnClose.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				grdChoose.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdChoose.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				grdCode.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdCode.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				grdName.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdName.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				grdDate.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdDate.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				grdGender.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdGender.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				grdAddress.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.grdAddress.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn1.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.gridColumn1.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn2.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.gridColumn2.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				gridColumn3.Caption = Inventec.Common.Resource.Get.Value("frmPatientChoice.gridColumn3.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem3.Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.layoutControlItem3.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				Text = Inventec.Common.Resource.Get.Value("frmPatientChoice.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
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
				HisPatientSDO hisPatientSDO = (HisPatientSDO)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
				if (e.Column.FieldName == "DOB_DISPLAY")
				{
					try
					{
						e.Value = Inventec.Common.DateTime.Convert.TimeNumberToDateString(hisPatientSDO.DOB);
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
						e.Value = (dicGender.ContainsKey(hisPatientSDO.GENDER_ID) ? dicGender[hisPatientSDO.GENDER_ID] : "");
						return;
					}
					catch (Exception ex2)
					{
						LogSystem.Warn("Loi set gia tri cho cot GENDER_NAME", ex2);
						return;
					}
				}
				if (!(e.Column.FieldName == "TDL_PATIENT_WORK_PLACE_NAME"))
				{
					return;
				}
				try
				{
					if (!hisPatientSDO.WORK_PLACE_ID.HasValue)
					{
						e.Value = hisPatientSDO.WORK_PLACE;
					}
					else if (hisPatientSDO.WORK_PLACE_ID.HasValue && hisPatientSDO.WORK_PLACE_ID.Value > 0)
					{
						e.Value = (dicWorkPlace.ContainsKey(hisPatientSDO.WORK_PLACE_ID.Value) ? dicWorkPlace[hisPatientSDO.WORK_PLACE_ID.Value] : "");
					}
				}
				catch (Exception ex3)
				{
					LogSystem.Warn("Loi set gia tri cho cot TDL_PATIENT_WORK_PLACE_NAME", ex3);
				}
			}
			catch (Exception ex4)
			{
				LogSystem.Warn(ex4);
			}
		}

		private void ProcessSelectedPatientSdo(ref HisPatientSDO patient)
		{
			try
			{
				CommonParam commonParam = new CommonParam();
				HisPatientWarningSDO hisPatientWarningSDO = new BackendAdapter(commonParam).Get<List<HisPatientWarningSDO>>("api/HisPatient/GetSdoAdvance", ApiConsumers.MosConsumer, patient.ID, new Action(SessionManager.ActionLostToken), commonParam).SingleOrDefault();
				if (hisPatientWarningSDO == null)
				{
					throw new ArgumentNullException("patientWarningSDO");
				}
				patient.PreviousPrescriptions = hisPatientWarningSDO.PreviousPrescriptions;
				patient.PreviousDebtTreatments = hisPatientWarningSDO.PreviousDebtTreatments;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			try
			{
				HisPatientSDO patient = (HisPatientSDO)gridView1.GetFocusedRow();
				if (patient != null)
				{
					ProcessSelectedPatientSdo(ref patient);
					updatePatientInfo(patient);
					Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void grdInformation_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					HisPatientSDO patient = (HisPatientSDO)gridView1.GetFocusedRow();
					if (patient != null)
					{
						ProcessSelectedPatientSdo(ref patient);
						updatePatientInfo(patient);
						Close();
					}
				}
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
				HisPatientSDO patient = (HisPatientSDO)gridView1.GetFocusedRow();
				if (patient != null)
				{
					ProcessSelectedPatientSdo(ref patient);
					updatePatientInfo(patient);
					Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
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

		public override void ProcessDisposeModuleDataAfterClose()
		{
			try
			{
				dicWorkPlace = null;
				dicGender = null;
				currentListPatient = null;
				updatePatientInfo = null;
				btnClose.Click -= new EventHandler(btnClose_Click);
				grdInformation.DoubleClick -= new EventHandler(grdInformation_DoubleClick);
				grdInformation.PreviewKeyDown -= new PreviewKeyDownEventHandler(grdInformation_PreviewKeyDown);
				gridView1.CustomUnboundColumnData -= new CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
				gridView1.MouseDown -= new MouseEventHandler(gridView1_MouseDown);
				base.Load -= new EventHandler(PopupPatientInformation_Load);
				gridView1.GridControl.DataSource = null;
				grdInformation.DataSource = null;
				gridColumn3 = null;
				gridColumn2 = null;
				gridColumn1 = null;
				layoutControlItem3 = null;
				lblDescription = null;
				grdCode = null;
				repositoryItemCheckEdit1 = null;
				layoutControlItem2 = null;
				lciPatientInformation = null;
				radianChoose = null;
				grdAddress = null;
				grdGender = null;
				grdDate = null;
				grdName = null;
				grdChoose = null;
				gridView1 = null;
				grdInformation = null;
				btnClose = null;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HIS.Desktop.Plugins.RegisterV2.frmPatientChoice));
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
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
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
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(456, 128, 250, 350);
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1485, 567);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.lblDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblDescription.Location = new System.Drawing.Point(3, 544);
			this.lblDescription.Margin = new System.Windows.Forms.Padding(4);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(1404, 17);
			this.lblDescription.StyleController = this.layoutControl1;
			this.lblDescription.TabIndex = 7;
			this.lblDescription.Text = "Chọn bệnh nhân bằng cách bấm enter hoặc nháy đúp chuột vào bệnh nhân. Thêm hồ sơ bệnh nhân mới chọn Bỏ qua";
			this.btnClose.Location = new System.Drawing.Point(1413, 544);
			this.btnClose.Margin = new System.Windows.Forms.Padding(4);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(69, 20);
			this.btnClose.StyleController = this.layoutControl1;
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "Bỏ qua";
			this.btnClose.Click += new System.EventHandler(btnClose_Click);
			this.grdInformation.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
			this.grdInformation.Location = new System.Drawing.Point(3, 3);
			this.grdInformation.MainView = this.gridView1;
			this.grdInformation.Margin = new System.Windows.Forms.Padding(4);
			this.grdInformation.Name = "grdInformation";
			this.grdInformation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[2] { this.radianChoose, this.repositoryItemCheckEdit1 });
			this.grdInformation.Size = new System.Drawing.Size(1479, 535);
			this.grdInformation.TabIndex = 4;
			this.grdInformation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.grdInformation.DoubleClick += new System.EventHandler(grdInformation_DoubleClick);
			this.grdInformation.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(grdInformation_PreviewKeyDown);
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[9] { this.grdChoose, this.grdCode, this.grdName, this.grdDate, this.grdGender, this.grdAddress, this.gridColumn1, this.gridColumn2, this.gridColumn3 });
			this.gridView1.GridControl = this.grdInformation;
			this.gridView1.Name = "gridView1";
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
			this.grdCode.Caption = "Mã bệnh nhân";
			this.grdCode.FieldName = "PATIENT_CODE";
			this.grdCode.Name = "grdCode";
			this.grdCode.OptionsColumn.AllowEdit = false;
			this.grdCode.Visible = true;
			this.grdCode.VisibleIndex = 0;
			this.grdCode.Width = 118;
			this.grdName.Caption = "Tên bệnh nhân";
			this.grdName.FieldName = "VIR_PATIENT_NAME";
			this.grdName.Name = "grdName";
			this.grdName.OptionsColumn.AllowEdit = false;
			this.grdName.Visible = true;
			this.grdName.VisibleIndex = 1;
			this.grdName.Width = 152;
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
			this.grdDate.VisibleIndex = 2;
			this.grdDate.Width = 122;
			this.grdGender.Caption = "Giới tính";
			this.grdGender.FieldName = "GENDER_NAME";
			this.grdGender.Name = "grdGender";
			this.grdGender.OptionsColumn.AllowEdit = false;
			this.grdGender.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.grdGender.Visible = true;
			this.grdGender.VisibleIndex = 3;
			this.grdGender.Width = 97;
			this.grdAddress.Caption = "Địa chỉ";
			this.grdAddress.FieldName = "VIR_ADDRESS";
			this.grdAddress.Name = "grdAddress";
			this.grdAddress.OptionsColumn.AllowEdit = false;
			this.grdAddress.Visible = true;
			this.grdAddress.VisibleIndex = 4;
			this.grdAddress.Width = 571;
			this.gridColumn1.Caption = "Thẻ BHYT";
			this.gridColumn1.FieldName = "TDL_HEIN_CARD_NUMBER";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 5;
			this.gridColumn1.Width = 140;
			this.gridColumn2.Caption = "Nơi làm việc";
			this.gridColumn2.FieldName = "TDL_PATIENT_WORK_PLACE_NAME";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 6;
			this.gridColumn2.Width = 149;
			this.gridColumn3.Caption = "Số điện thoại";
			this.gridColumn3.FieldName = "PHONE";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 7;
			this.gridColumn3.Width = 128;
			this.radianChoose.Name = "radianChoose";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3] { this.lciPatientInformation, this.layoutControlItem2, this.layoutControlItem3 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1485, 567);
			this.layoutControlGroup1.TextVisible = false;
			this.lciPatientInformation.Control = this.grdInformation;
			this.lciPatientInformation.Location = new System.Drawing.Point(0, 0);
			this.lciPatientInformation.Name = "lciPatientInformation";
			this.lciPatientInformation.Size = new System.Drawing.Size(1485, 541);
			this.lciPatientInformation.TextSize = new System.Drawing.Size(0, 0);
			this.lciPatientInformation.TextVisible = false;
			this.layoutControlItem2.Control = this.btnClose;
			this.layoutControlItem2.Location = new System.Drawing.Point(1410, 541);
			this.layoutControlItem2.MaxSize = new System.Drawing.Size(75, 26);
			this.layoutControlItem2.MinSize = new System.Drawing.Size(70, 26);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(75, 26);
			this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.layoutControlItem3.Control = this.lblDescription;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 541);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(1410, 26);
			this.layoutControlItem3.Text = resources.GetString("layoutControlItem3.Text");
			this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextToControlDistance = 0;
			this.layoutControlItem3.TextVisible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1485, 567);
			base.Controls.Add(this.layoutControl1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Margin = new System.Windows.Forms.Padding(4);
			base.MaximizeBox = false;
			base.Name = "frmPatientChoice";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chọn thông tin bệnh nhân";
			base.Load += new System.EventHandler(PopupPatientInformation_Load);
			base.Controls.SetChildIndex(this.layoutControl1, 0);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.grdInformation).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.radianChoose).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciPatientInformation).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
