using System;
using System.Collections;
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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.Library.CacheClient;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Base;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using HIS.Desktop.Utilities.Extensions;
using HIS.Desktop.Utility;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.LocalStorage.Location;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Modules;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.PtttMethod
{
	public class FormPtttMethod : FormBase
	{
		private Action<List<PtttMethodADO>> ChooseData;

		private List<PtttMethodADO> DataSource;

		private V_HIS_SERE_SERV_5 CurrentSereServ;

		private List<PtttMethodADO> SelectedData = new List<PtttMethodADO>();

		private ControlStateWorker controlStateWorker = new ControlStateWorker();

		private List<ControlStateRDO> currentControlStateRDO = new List<ControlStateRDO>();

		private string MODULELINK = "FormEkipUser";

		private PtttMethodADO currentPtttEkip;

		private Inventec.Desktop.Common.Modules.Module CurrentModuleData;

		private List<HIS_PTTT_GROUP> lstGroup;

		private List<PtttMethodADO> lstSelect = new List<PtttMethodADO>();

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private SimpleButton btnChoose;

		private GridControl gridControlPtttMethod;

		private GridView gridViewPtttMethod;

		private SimpleButton btnSearch;

		private TextEdit txtKeyword;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private LayoutControlItem layoutControlItem4;

		private GridColumn gc_MethodCode;

		private GridColumn gc_MethodName;

		private GridColumn gc_amount;

		private RepositoryItemSpinEdit repositoryItemSpinEdit2;

		private BarManager barManager1;

		private Bar bar1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private BarButtonItem barBtnSearch;

		private GridColumn gc_PTTT;

		private RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit_PTTT;

		private GridView repositoryItemGridLookUpEdit1View;

		private RepositoryItemCustomGridLookUpEdit GridLU_PTTT;

		private CustomGridView repositoryItemCustomGridLookUpEdit1View;

		private GridColumn gc_icon;

		private RepositoryItemButtonEdit BbtnUser_PTTT;

		private GridColumn gridColumn1;

		private RepositoryItemButtonEdit BbtnUnCheck;

		private RepositoryItemButtonEdit BbtnCheck;

		private PanelControl panelControl1;

		private LayoutControlItem layoutControlItem5;

		private PictureEdit pictureEdit1;

		private ImageCollection imageCollection1;

		private SimpleButton btnAdd;

		private BarButtonItem bbtnAdd;

		private LayoutControlItem layoutControlItem3;

		public FormPtttMethod(Action<List<PtttMethodADO>> chooseData, V_HIS_SERE_SERV_5 sereServ, List<PtttMethodADO> oldSelect, Inventec.Desktop.Common.Modules.Module moduleData)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			InitializeComponent();
			ChooseData = chooseData;
			CurrentSereServ = sereServ;
			CurrentModuleData = moduleData;
			if (oldSelect != null && oldSelect.Count > 0)
			{
				SelectedData.AddRange(oldSelect);
			}
		}

		private void FormPtttMethod_Load(object sender, EventArgs e)
		{
			try
			{
				((Form)this).Icon = Icon.ExtractAssociatedIcon(Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
				SetCaptionByLanguageKey();
				pictureEdit1.Image = imageCollection1.Images[1];
				lstGroup = BackendDataWorker.Get<HIS_PTTT_GROUP>();
				ComboPTMethod();
				CreateDataSource();
				FillDataToGrid();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void FillDataToGrid()
		{
			try
			{
				DataSource = lstSelect;
				string keyword = txtKeyword.Text.Trim();
				keyword = keyword.Trim().ToLower();
				if (!string.IsNullOrEmpty(keyword))
				{
					DataSource = DataSource.Where((PtttMethodADO o) => o.PTTT_METHOD_CODE.ToLower().Contains(keyword) || o.PTTT_METHOD_NAME.ToLower().Contains(keyword)).ToList();
				}
				DataSource = DataSource.OrderBy((PtttMethodADO o) => o.PTTT_METHOD_CODE).ToList();
				gridControlPtttMethod.BeginUpdate();
				gridControlPtttMethod.DataSource = DataSource;
				gridControlPtttMethod.EndUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void CreateDataSource()
		{
			try
			{
				DataSource = new List<PtttMethodADO>();
				List<HIS_PTTT_METHOD> list = BackendDataWorker.Get<HIS_PTTT_METHOD>();
				foreach (HIS_PTTT_METHOD item in list)
				{
					if (item.IS_ACTIVE != 1)
					{
						continue;
					}
					PtttMethodADO ptttMethodADO = new PtttMethodADO();
					ptttMethodADO.ID = item.ID;
					ptttMethodADO.PTTT_METHOD_CODE = item.PTTT_METHOD_CODE;
					ptttMethodADO.PTTT_METHOD_NAME = item.PTTT_METHOD_NAME;
					ptttMethodADO.PTTT_GROUP_ID = item.PTTT_GROUP_ID;
					ptttMethodADO.SERE_SERV_ID = CurrentSereServ.ID;
					if (SelectedData != null && SelectedData.Count > 0)
					{
						PtttMethodADO old = SelectedData.FirstOrDefault((PtttMethodADO o) => o.ID == item.ID);
						if (old != null)
						{
							ptttMethodADO.IS_SELECTION = true;
							ptttMethodADO.PTTT_GROUP_ID = old.PTTT_GROUP_ID;
							ptttMethodADO.EkipUsersADO = old.EkipUsersADO;
							ptttMethodADO.AMOUNT = old.AMOUNT;
							ptttMethodADO.EKIP_ID = old.EKIP_ID;
							ptttMethodADO.SERVICE_REQ_ID = old.SERVICE_REQ_ID;
							if (!ptttMethodADO.PTTT_GROUP_ID.HasValue)
							{
								ptttMethodADO.PTTT_GROUP_NAME = old.PTTT_GROUP_NAME;
							}
							else
							{
								ptttMethodADO.PTTT_GROUP_NAME = (old.PTTT_GROUP_ID.HasValue ? lstGroup.Where((HIS_PTTT_GROUP o) => o.ID == old.PTTT_GROUP_ID).FirstOrDefault().PTTT_GROUP_NAME : "");
							}
						}
					}
					DataSource.Add(ptttMethodADO);
				}
				lstSelect = DataSource;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtKeyword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					FillDataToGrid();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				FillDataToGrid();
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
				if (lstSelect == null || lstSelect.Count <= 0)
				{
					return;
				}
				List<PtttMethodADO> list = lstSelect.Where((PtttMethodADO o) => o.IS_SELECTION).ToList();
				if (((list != null && list.Count > 0) || XtraMessageBox.Show(ResourceMessage.BanChuaChonPhuongPhapNao, ResourceMessage.ThongBao, MessageBoxButtons.OKCancel) == DialogResult.OK) && ChooseData != null)
				{
					list.ForEach(delegate(PtttMethodADO o)
					{
						o.SERVICE_REQ_ID = CurrentSereServ.SERVICE_REQ_ID.GetValueOrDefault();
					});
					ChooseData(list);
					((Form)this).Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void barBtnSearch_ItemClick(object sender, ItemClickEventArgs e)
		{
			btnSearch_Click(null, null);
		}

		private void gridViewPtttMethod_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			try
			{
				GridView gridView = sender as GridView;
				PtttMethodADO ptttMethodADO = null;
				if (e.RowHandle > -1)
				{
					int dataSourceRowIndex = gridViewPtttMethod.GetDataSourceRowIndex(e.RowHandle);
					ptttMethodADO = (PtttMethodADO)((IList)((BaseView)sender).DataSource)[dataSourceRowIndex];
				}
				if (e.RowHandle < 0 || ptttMethodADO == null)
				{
					return;
				}
				if (e.Column.FieldName == "PTTT_GROUP_NAME")
				{
					if (ptttMethodADO.IS_SELECTION && !ptttMethodADO.PTTT_GROUP_ID.HasValue)
					{
						e.RepositoryItem = (RepositoryItem)(object)GridLU_PTTT;
					}
					else
					{
						e.RepositoryItem = null;
					}
				}
				else if (e.Column.FieldName == "IS_SELECTION")
				{
					if (ptttMethodADO.IS_SELECTION)
					{
						e.RepositoryItem = BbtnCheck;
					}
					else
					{
						e.RepositoryItem = BbtnUnCheck;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void gridViewPtttMethod_ShowingEditor(object sender, CancelEventArgs e)
		{
			try
			{
				GridView gridView = sender as GridView;
				if (gridView.FocusedColumn.FieldName == "PTTT_GROUP_NAME" && gridView.ActiveEditor is GridLookUpEdit)
				{
					GridLookUpEdit cbo = gridView.ActiveEditor as GridLookUpEdit;
					ComboPTTT(cbo);
					gridViewPtttMethod.RefreshData();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ComboPTTT(GridLookUpEdit cbo)
		{
			try
			{
				List<HIS_PTTT_GROUP> dataSource = BackendDataWorker.Get<HIS_PTTT_GROUP>();
				cbo.Properties.DataSource = dataSource;
				cbo.Properties.DisplayMember = "PTTT_GROUP_NAME";
				cbo.Properties.ValueMember = "ID";
				cbo.Properties.TextEditStyle = TextEditStyles.Standard;
				cbo.Properties.PopupFilterMode = PopupFilterMode.Contains;
				cbo.Properties.ImmediatePopup = true;
				cbo.ForceInitialize();
				cbo.Properties.View.Columns.Clear();
				GridColumn gridColumn = cbo.Properties.View.Columns.AddField("PTTT_GROUP_CODE");
				gridColumn.Caption = "Mã";
				gridColumn.Visible = true;
				gridColumn.VisibleIndex = 1;
				gridColumn.Width = 50;
				GridColumn gridColumn2 = cbo.Properties.View.Columns.AddField("PTTT_GROUP_NAME");
				gridColumn2.Caption = "Tên";
				gridColumn2.Visible = true;
				gridColumn2.VisibleIndex = 2;
				gridColumn2.Width = 150;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ComboPTMethod()
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			try
			{
				List<HIS_PTTT_GROUP> list = BackendDataWorker.Get<HIS_PTTT_GROUP>();
				List<ColumnInfo> list2 = new List<ColumnInfo>();
				list2.Add(new ColumnInfo("PTTT_GROUP_CODE", "Mã", 50, 1));
				list2.Add(new ColumnInfo("PTTT_GROUP_NAME", "Tên", 100, 2));
				ControlEditorADO val = new ControlEditorADO("PTTT_GROUP_NAME", "ID", list2, true, 150);
				ControlEditorLoader.Load((object)GridLU_PTTT, (object)list, val);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void BbtnUser_PTTT_Click(object sender, EventArgs e)
		{
			try
			{
				PtttMethodADO ptttMethodADO = (PtttMethodADO)gridViewPtttMethod.GetFocusedRow();
				if (ptttMethodADO.IS_SELECTION)
				{
					currentPtttEkip = ptttMethodADO;
					FormEkipUser formEkipUser = new FormEkipUser(SetEkipUser, ptttMethodADO, CurrentModuleData);
					((Form)(object)formEkipUser).ShowDialog();
				}
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private void SetEkipUser(EkipUsersADO data)
		{
			try
			{
				List<PtttMethodADO> list = gridControlPtttMethod.DataSource as List<PtttMethodADO>;
				int focusedRowHandle = 0;
				for (int i = 0; i < list.Count; i++)
				{
					PtttMethodADO ptttMethodADO = list[i];
					if (ptttMethodADO.ID == currentPtttEkip.ID)
					{
						focusedRowHandle = i;
						ptttMethodADO.EkipUsersADO = data;
					}
				}
				gridControlPtttMethod.DataSource = new List<PtttMethodADO>();
				gridControlPtttMethod.DataSource = list;
				gridViewPtttMethod.FocusedRowHandle = focusedRowHandle;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void BbtnCheck_Click(object sender, EventArgs e)
		{
			try
			{
				PtttMethodADO ptttMethodADO = (PtttMethodADO)gridViewPtttMethod.GetFocusedRow();
				List<PtttMethodADO> list = gridControlPtttMethod.DataSource as List<PtttMethodADO>;
				int focusedRowHandle = 0;
				for (int i = 0; i < list.Count; i++)
				{
					PtttMethodADO ptttMethodADO2 = list[i];
					if (ptttMethodADO2.ID == ptttMethodADO.ID)
					{
						focusedRowHandle = i;
						ptttMethodADO2.IS_SELECTION = false;
						ptttMethodADO2.PTTT_GROUP_NAME = null;
						ptttMethodADO2.AMOUNT = null;
						ptttMethodADO2.EkipUsersADO = null;
						break;
					}
				}
				gridControlPtttMethod.DataSource = new List<PtttMethodADO>();
				gridControlPtttMethod.DataSource = list;
				gridViewPtttMethod.FocusedRowHandle = focusedRowHandle;
				foreach (PtttMethodADO item in lstSelect)
				{
					if (item.ID == ptttMethodADO.ID)
					{
						item.IS_SELECTION = false;
						item.PTTT_GROUP_NAME = null;
						item.AMOUNT = null;
						item.EkipUsersADO = null;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void BbtnUnCheck_Click(object sender, EventArgs e)
		{
			try
			{
				List<HIS_PTTT_GROUP> source = BackendDataWorker.Get<HIS_PTTT_GROUP>();
				PtttMethodADO ptttMethodADO = (PtttMethodADO)gridViewPtttMethod.GetFocusedRow();
				List<PtttMethodADO> list = gridControlPtttMethod.DataSource as List<PtttMethodADO>;
				int focusedRowHandle = 0;
				for (int i = 0; i < list.Count; i++)
				{
					PtttMethodADO item = list[i];
					if (item.ID == ptttMethodADO.ID)
					{
						focusedRowHandle = i;
						item.IS_SELECTION = true;
						item.PTTT_GROUP_NAME = (item.PTTT_GROUP_ID.HasValue ? source.Where((HIS_PTTT_GROUP o) => o.ID == item.PTTT_GROUP_ID).FirstOrDefault().PTTT_GROUP_NAME : "");
						item.AMOUNT = 1;
						break;
					}
				}
				gridControlPtttMethod.DataSource = new List<PtttMethodADO>();
				gridControlPtttMethod.DataSource = list;
				gridViewPtttMethod.FocusedRowHandle = focusedRowHandle;
				foreach (PtttMethodADO item2 in lstSelect)
				{
					if (item2.ID == ptttMethodADO.ID)
					{
						item2.IS_SELECTION = true;
						item2.PTTT_GROUP_NAME = (item2.PTTT_GROUP_ID.HasValue ? source.Where((HIS_PTTT_GROUP o) => o.ID == item2.PTTT_GROUP_ID).FirstOrDefault().PTTT_GROUP_NAME : "");
						item2.AMOUNT = 1;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void pictureEdit1_Click(object sender, EventArgs e)
		{
			try
			{
				List<PtttMethodADO> list = gridControlPtttMethod.DataSource as List<PtttMethodADO>;
				if (pictureEdit1.Image == imageCollection1.Images[1])
				{
					pictureEdit1.Image = imageCollection1.Images[0];
					foreach (PtttMethodADO item in list)
					{
						item.IS_SELECTION = true;
						item.PTTT_GROUP_NAME = (item.PTTT_GROUP_ID.HasValue ? lstGroup.Where((HIS_PTTT_GROUP o) => o.ID == item.PTTT_GROUP_ID).FirstOrDefault().PTTT_GROUP_NAME : "");
						item.AMOUNT = 1;
					}
				}
				else
				{
					pictureEdit1.Image = imageCollection1.Images[1];
					foreach (PtttMethodADO item2 in list)
					{
						item2.IS_SELECTION = false;
						item2.PTTT_GROUP_NAME = null;
						item2.AMOUNT = null;
					}
				}
				gridControlPtttMethod.DataSource = new List<PtttMethodADO>();
				gridControlPtttMethod.DataSource = list;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetCaptionByLanguageKey()
		{
			try
			{
				ResourceLanguageManager.LanguageResource__FormPtttMethod = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(FormPtttMethod).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("FormPtttMethod.layoutControl1.Text", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				bar1.Text = Inventec.Common.Resource.Get.Value("FormPtttMethod.bar1.Text", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				barBtnSearch.Caption = Inventec.Common.Resource.Get.Value("FormPtttMethod.barBtnSearch.Caption", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				gc_icon.Caption = Inventec.Common.Resource.Get.Value("FormPtttMethod.gc_icon.Caption", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				gridColumn1.Caption = Inventec.Common.Resource.Get.Value("FormPtttMethod.gridColumn1.Caption", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				gc_MethodCode.Caption = Inventec.Common.Resource.Get.Value("FormPtttMethod.gc_MethodCode.Caption", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				gc_MethodName.Caption = Inventec.Common.Resource.Get.Value("FormPtttMethod.gc_MethodName.Caption", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				gc_PTTT.Caption = Inventec.Common.Resource.Get.Value("FormPtttMethod.gc_PTTT.Caption", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				gc_PTTT.ToolTip = Inventec.Common.Resource.Get.Value("FormPtttMethod.gc_PTTT.ToolTip", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				gc_amount.Caption = Inventec.Common.Resource.Get.Value("FormPtttMethod.gc_amount.Caption", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				repositoryItemGridLookUpEdit_PTTT.NullText = Inventec.Common.Resource.Get.Value("FormPtttMethod.repositoryItemGridLookUpEdit_PTTT.NullText", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				((RepositoryItem)(object)GridLU_PTTT).NullText = Inventec.Common.Resource.Get.Value("FormPtttMethod.GridLU_PTTT.NullText", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				btnChoose.Text = Inventec.Common.Resource.Get.Value("FormPtttMethod.btnChoose.Text", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				btnSearch.Text = Inventec.Common.Resource.Get.Value("FormPtttMethod.btnSearch.Text", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				txtKeyword.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("FormPtttMethod.txtKeyword.Properties.NullValuePrompt", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
				((Control)(object)this).Text = Inventec.Common.Resource.Get.Value("FormPtttMethod.Text", ResourceLanguageManager.LanguageResource__FormPtttMethod, LanguageManager.GetCulture());
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				Inventec.Desktop.Common.Modules.Module val = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.HisPtttMethod").FirstOrDefault();
				if (val == null)
				{
					throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.HisPtttMethod'");
				}
				if (val.IsPlugin && val.ExtensionInfo != null)
				{
					val.RoomId = CurrentModuleData.RoomId;
					val.RoomTypeId = CurrentModuleData.RoomTypeId;
					List<object> list = new List<object>();
					list.Add(val);
					if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()))
					{
						list.Add(txtKeyword.Text.Trim());
					}
					object pluginInstance = PluginInstance.GetPluginInstance(val, list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
				BackendDataWorker.Reset<HIS_PTTT_METHOD>();
				CreateDataSource();
				FillDataToGrid();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void txtKeyword_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				string text = (sender as TextEdit).Text;
				SearchClick(text);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SearchClick(string keyword)
		{
			try
			{
				List<PtttMethodADO> list = null;
				if (!string.IsNullOrEmpty(keyword.Trim()))
				{
					List<PtttMethodADO> list2 = new List<PtttMethodADO>();
					list2 = lstSelect.Where((PtttMethodADO o) => o.PTTT_METHOD_CODE.ToLower().Contains(keyword.Trim().ToLower()) || (o.PTTT_METHOD_NAME ?? "").ToString().ToLower().Contains(keyword.Trim().ToLower())).Distinct().ToList();
					list = list2;
				}
				else
				{
					list = lstSelect;
				}
				gridControlPtttMethod.BeginUpdate();
				gridControlPtttMethod.DataSource = list;
				gridControlPtttMethod.EndUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnAdd_ItemClick(object sender, ItemClickEventArgs e)
		{
			btnAdd_Click(null, null);
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
			//IL_0176: Unknown result type (might be due to invalid IL or missing references)
			//IL_0180: Expected O, but got Unknown
			//IL_0181: Unknown result type (might be due to invalid IL or missing references)
			//IL_018b: Expected O, but got Unknown
			ComponentResourceManager resources = new ComponentResourceManager(typeof(FormPtttMethod));
			SerializableAppearanceObject appearance = new SerializableAppearanceObject();
			SerializableAppearanceObject appearanceHovered = new SerializableAppearanceObject();
			SerializableAppearanceObject appearancePressed = new SerializableAppearanceObject();
			SerializableAppearanceObject appearanceDisabled = new SerializableAppearanceObject();
			SerializableAppearanceObject appearance2 = new SerializableAppearanceObject();
			SerializableAppearanceObject appearanceHovered2 = new SerializableAppearanceObject();
			SerializableAppearanceObject appearancePressed2 = new SerializableAppearanceObject();
			SerializableAppearanceObject appearanceDisabled2 = new SerializableAppearanceObject();
			SerializableAppearanceObject appearance3 = new SerializableAppearanceObject();
			SerializableAppearanceObject appearanceHovered3 = new SerializableAppearanceObject();
			SerializableAppearanceObject appearancePressed3 = new SerializableAppearanceObject();
			SerializableAppearanceObject appearanceDisabled3 = new SerializableAppearanceObject();
			repositoryItemSpinEdit2 = new RepositoryItemSpinEdit();
			layoutControl1 = new LayoutControl();
			btnAdd = new SimpleButton();
			panelControl1 = new PanelControl();
			pictureEdit1 = new PictureEdit();
			barManager1 = new BarManager();
			bar1 = new Bar();
			barBtnSearch = new BarButtonItem();
			bbtnAdd = new BarButtonItem();
			barDockControlTop = new BarDockControl();
			barDockControlBottom = new BarDockControl();
			barDockControlLeft = new BarDockControl();
			barDockControlRight = new BarDockControl();
			gridControlPtttMethod = new GridControl();
			gridViewPtttMethod = new GridView();
			gc_icon = new GridColumn();
			BbtnUser_PTTT = new RepositoryItemButtonEdit();
			gridColumn1 = new GridColumn();
			BbtnUnCheck = new RepositoryItemButtonEdit();
			gc_MethodCode = new GridColumn();
			gc_MethodName = new GridColumn();
			gc_PTTT = new GridColumn();
			gc_amount = new GridColumn();
			repositoryItemGridLookUpEdit_PTTT = new RepositoryItemGridLookUpEdit();
			repositoryItemGridLookUpEdit1View = new GridView();
			GridLU_PTTT = new RepositoryItemCustomGridLookUpEdit();
			repositoryItemCustomGridLookUpEdit1View = new CustomGridView();
			BbtnCheck = new RepositoryItemButtonEdit();
			btnChoose = new SimpleButton();
			btnSearch = new SimpleButton();
			txtKeyword = new TextEdit();
			layoutControlGroup1 = new LayoutControlGroup();
			layoutControlItem1 = new LayoutControlItem();
			layoutControlItem2 = new LayoutControlItem();
			emptySpaceItem1 = new EmptySpaceItem();
			layoutControlItem4 = new LayoutControlItem();
			layoutControlItem5 = new LayoutControlItem();
			layoutControlItem3 = new LayoutControlItem();
			imageCollection1 = new ImageCollection();
			((ISupportInitialize)repositoryItemSpinEdit2).BeginInit();
			((ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((ISupportInitialize)panelControl1).BeginInit();
			panelControl1.SuspendLayout();
			((ISupportInitialize)pictureEdit1.Properties).BeginInit();
			((ISupportInitialize)barManager1).BeginInit();
			((ISupportInitialize)gridControlPtttMethod).BeginInit();
			((ISupportInitialize)gridViewPtttMethod).BeginInit();
			((ISupportInitialize)BbtnUser_PTTT).BeginInit();
			((ISupportInitialize)BbtnUnCheck).BeginInit();
			((ISupportInitialize)repositoryItemGridLookUpEdit_PTTT).BeginInit();
			((ISupportInitialize)repositoryItemGridLookUpEdit1View).BeginInit();
			((ISupportInitialize)GridLU_PTTT).BeginInit();
			((ISupportInitialize)repositoryItemCustomGridLookUpEdit1View).BeginInit();
			((ISupportInitialize)BbtnCheck).BeginInit();
			((ISupportInitialize)txtKeyword.Properties).BeginInit();
			((ISupportInitialize)layoutControlGroup1).BeginInit();
			((ISupportInitialize)layoutControlItem1).BeginInit();
			((ISupportInitialize)layoutControlItem2).BeginInit();
			((ISupportInitialize)emptySpaceItem1).BeginInit();
			((ISupportInitialize)layoutControlItem4).BeginInit();
			((ISupportInitialize)layoutControlItem5).BeginInit();
			((ISupportInitialize)layoutControlItem3).BeginInit();
			((ISupportInitialize)imageCollection1).BeginInit();
			((Control)this).SuspendLayout();
			repositoryItemSpinEdit2.AutoHeight = false;
			repositoryItemSpinEdit2.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			repositoryItemSpinEdit2.MaxLength = 19;
			repositoryItemSpinEdit2.MaxValue = new decimal(new int[4] { -1981284353, -1966660860, 0, 0 });
			repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
			layoutControl1.Controls.Add(btnAdd);
			layoutControl1.Controls.Add(panelControl1);
			layoutControl1.Controls.Add(btnChoose);
			layoutControl1.Controls.Add(btnSearch);
			layoutControl1.Controls.Add(txtKeyword);
			layoutControl1.Dock = DockStyle.Fill;
			layoutControl1.Location = new Point(0, 29);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new Size(644, 432);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			btnAdd.Location = new Point(554, 2);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(88, 22);
			btnAdd.StyleController = layoutControl1;
			btnAdd.TabIndex = 9;
			btnAdd.Text = "Thêm (Ctrl N)";
			btnAdd.Click += btnAdd_Click;
			panelControl1.Controls.Add(pictureEdit1);
			panelControl1.Controls.Add(gridControlPtttMethod);
			panelControl1.Location = new Point(2, 28);
			panelControl1.Name = "panelControl1";
			panelControl1.Size = new Size(640, 376);
			panelControl1.TabIndex = 8;
			pictureEdit1.Location = new Point(10, 2);
			pictureEdit1.MenuManager = barManager1;
			pictureEdit1.Name = "pictureEdit1";
			pictureEdit1.Properties.ShowCameraMenuItem = CameraMenuItemVisibility.Auto;
			pictureEdit1.Size = new Size(20, 19);
			pictureEdit1.TabIndex = 7;
			pictureEdit1.Click += pictureEdit1_Click;
			barManager1.Bars.AddRange(new Bar[1] { bar1 });
			barManager1.DockControls.Add(barDockControlTop);
			barManager1.DockControls.Add(barDockControlBottom);
			barManager1.DockControls.Add(barDockControlLeft);
			barManager1.DockControls.Add(barDockControlRight);
			barManager1.Form = (Control)(object)this;
			barManager1.Items.AddRange(new BarItem[2] { barBtnSearch, bbtnAdd });
			barManager1.MaxItemId = 2;
			bar1.BarName = "Tools";
			bar1.DockCol = 0;
			bar1.DockRow = 0;
			bar1.DockStyle = BarDockStyle.Top;
			bar1.LinksPersistInfo.AddRange(new LinkPersistInfo[2]
			{
				new LinkPersistInfo(barBtnSearch),
				new LinkPersistInfo(bbtnAdd)
			});
			bar1.Text = "Tools";
			bar1.Visible = false;
			barBtnSearch.Caption = "Ctrl F";
			barBtnSearch.Id = 0;
			barBtnSearch.ItemShortcut = new BarShortcut(Keys.F | Keys.Control);
			barBtnSearch.Name = "barBtnSearch";
			barBtnSearch.ItemClick += barBtnSearch_ItemClick;
			bbtnAdd.Caption = "Ctrl N";
			bbtnAdd.Id = 1;
			bbtnAdd.ItemShortcut = new BarShortcut(Keys.N | Keys.Control);
			bbtnAdd.Name = "bbtnAdd";
			bbtnAdd.ItemClick += bbtnAdd_ItemClick;
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = DockStyle.Top;
			barDockControlTop.Location = new Point(0, 0);
			barDockControlTop.Size = new Size(644, 29);
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = DockStyle.Bottom;
			barDockControlBottom.Location = new Point(0, 461);
			barDockControlBottom.Size = new Size(644, 0);
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = DockStyle.Left;
			barDockControlLeft.Location = new Point(0, 29);
			barDockControlLeft.Size = new Size(0, 432);
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = DockStyle.Right;
			barDockControlRight.Location = new Point(644, 29);
			barDockControlRight.Size = new Size(0, 432);
			gridControlPtttMethod.Dock = DockStyle.Fill;
			gridControlPtttMethod.Location = new Point(2, 2);
			gridControlPtttMethod.MainView = gridViewPtttMethod;
			gridControlPtttMethod.Name = "gridControlPtttMethod";
			gridControlPtttMethod.RepositoryItems.AddRange(new RepositoryItem[5]
			{
				repositoryItemGridLookUpEdit_PTTT,
				(RepositoryItem)(object)GridLU_PTTT,
				BbtnUser_PTTT,
				BbtnCheck,
				BbtnUnCheck
			});
			gridControlPtttMethod.Size = new Size(636, 372);
			gridControlPtttMethod.TabIndex = 6;
			gridControlPtttMethod.ViewCollection.AddRange(new BaseView[1] { gridViewPtttMethod });
			gridViewPtttMethod.Columns.AddRange(new GridColumn[6] { gc_icon, gridColumn1, gc_MethodCode, gc_MethodName, gc_PTTT, gc_amount });
			gridViewPtttMethod.GridControl = gridControlPtttMethod;
			gridViewPtttMethod.Name = "gridViewPtttMethod";
			gridViewPtttMethod.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
			gridViewPtttMethod.OptionsView.ShowGroupPanel = false;
			gridViewPtttMethod.OptionsView.ShowIndicator = false;
			gridViewPtttMethod.CustomRowCellEdit += gridViewPtttMethod_CustomRowCellEdit;
			gridViewPtttMethod.ShowingEditor += gridViewPtttMethod_ShowingEditor;
			gc_icon.Caption = "gridColumn1";
			gc_icon.ColumnEdit = BbtnUser_PTTT;
			gc_icon.Name = "gc_icon";
			gc_icon.OptionsColumn.ShowCaption = false;
			gc_icon.Visible = true;
			gc_icon.VisibleIndex = 1;
			gc_icon.Width = 20;
			BbtnUser_PTTT.AutoHeight = false;
			BbtnUser_PTTT.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, ImageLocation.MiddleCenter, (Image)resources.GetObject("BbtnUser_PTTT.Buttons"), new KeyShortcut(Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "Kíp", null, null, true)
			});
			BbtnUser_PTTT.Name = "BbtnUser_PTTT";
			BbtnUser_PTTT.TextEditStyle = TextEditStyles.HideTextEditor;
			BbtnUser_PTTT.Click += BbtnUser_PTTT_Click;
			gridColumn1.Caption = "gridColumn1";
			gridColumn1.ColumnEdit = BbtnUnCheck;
			gridColumn1.FieldName = "IS_SELECTION";
			gridColumn1.Name = "gridColumn1";
			gridColumn1.OptionsColumn.ShowCaption = false;
			gridColumn1.Visible = true;
			gridColumn1.VisibleIndex = 0;
			gridColumn1.Width = 35;
			BbtnUnCheck.AutoHeight = false;
			BbtnUnCheck.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, ImageLocation.MiddleCenter, (Image)resources.GetObject("BbtnUnCheck.Buttons"), new KeyShortcut(Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, true)
			});
			BbtnUnCheck.Name = "BbtnUnCheck";
			BbtnUnCheck.TextEditStyle = TextEditStyles.HideTextEditor;
			BbtnUnCheck.Click += BbtnUnCheck_Click;
			gc_MethodCode.Caption = "Mã";
			gc_MethodCode.FieldName = "PTTT_METHOD_CODE";
			gc_MethodCode.Name = "gc_MethodCode";
			gc_MethodCode.OptionsColumn.AllowEdit = false;
			gc_MethodCode.Visible = true;
			gc_MethodCode.VisibleIndex = 2;
			gc_MethodCode.Width = 48;
			gc_MethodName.Caption = "Tên";
			gc_MethodName.FieldName = "PTTT_METHOD_NAME";
			gc_MethodName.Name = "gc_MethodName";
			gc_MethodName.OptionsColumn.AllowEdit = false;
			gc_MethodName.Visible = true;
			gc_MethodName.VisibleIndex = 3;
			gc_MethodName.Width = 298;
			gc_PTTT.Caption = "Loại PTTT";
			gc_PTTT.FieldName = "PTTT_GROUP_NAME";
			gc_PTTT.Name = "gc_PTTT";
			gc_PTTT.ToolTip = "Loại phẫu thuật thủ thuật";
			gc_PTTT.Visible = true;
			gc_PTTT.VisibleIndex = 4;
			gc_PTTT.Width = 138;
			gc_amount.Caption = "Số lượng";
			gc_amount.ColumnEdit = repositoryItemSpinEdit2;
			gc_amount.FieldName = "AMOUNT";
			gc_amount.Name = "gc_amount";
			gc_amount.Visible = true;
			gc_amount.VisibleIndex = 5;
			gc_amount.Width = 82;
			repositoryItemGridLookUpEdit_PTTT.AllowNullInput = DefaultBoolean.True;
			repositoryItemGridLookUpEdit_PTTT.AutoComplete = false;
			repositoryItemGridLookUpEdit_PTTT.AutoHeight = false;
			repositoryItemGridLookUpEdit_PTTT.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			repositoryItemGridLookUpEdit_PTTT.Name = "repositoryItemGridLookUpEdit_PTTT";
			repositoryItemGridLookUpEdit_PTTT.NullText = "";
			repositoryItemGridLookUpEdit_PTTT.TextEditStyle = TextEditStyles.Standard;
			repositoryItemGridLookUpEdit_PTTT.View = repositoryItemGridLookUpEdit1View;
			repositoryItemGridLookUpEdit1View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
			repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
			repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			((RepositoryItemTextEdit)(object)GridLU_PTTT).AllowNullInput = DefaultBoolean.True;
			((RepositoryItemGridLookUpEdit)(object)GridLU_PTTT).AutoComplete = false;
			((RepositoryItem)(object)GridLU_PTTT).AutoHeight = false;
			((RepositoryItemButtonEdit)(object)GridLU_PTTT).Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			((RepositoryItem)(object)GridLU_PTTT).Name = "GridLU_PTTT";
			((RepositoryItem)(object)GridLU_PTTT).NullText = "";
			((RepositoryItemButtonEdit)(object)GridLU_PTTT).TextEditStyle = TextEditStyles.Standard;
			((RepositoryItemGridLookUpEditBase)(object)GridLU_PTTT).View = (GridView)(object)repositoryItemCustomGridLookUpEdit1View;
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).FocusRectStyle = DrawFocusRectStyle.RowFocus;
			((BaseView)(object)repositoryItemCustomGridLookUpEdit1View).Name = "repositoryItemCustomGridLookUpEdit1View";
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).OptionsSelection.EnableAppearanceFocusedCell = false;
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).OptionsView.ShowGroupPanel = false;
			BbtnCheck.AutoHeight = false;
			BbtnCheck.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, ImageLocation.MiddleCenter, (Image)resources.GetObject("BbtnCheck.Buttons"), new KeyShortcut(Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, true)
			});
			BbtnCheck.Name = "BbtnCheck";
			BbtnCheck.TextEditStyle = TextEditStyles.HideTextEditor;
			BbtnCheck.Click += BbtnCheck_Click;
			btnChoose.Location = new Point(534, 408);
			btnChoose.Name = "btnChoose";
			btnChoose.Size = new Size(108, 22);
			btnChoose.StyleController = layoutControl1;
			btnChoose.TabIndex = 7;
			btnChoose.Text = "Chọn";
			btnChoose.Click += btnChoose_Click;
			btnSearch.Location = new Point(471, 2);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(79, 22);
			btnSearch.StyleController = layoutControl1;
			btnSearch.TabIndex = 5;
			btnSearch.Text = "Tìm (Ctrl F)";
			btnSearch.Click += btnSearch_Click;
			txtKeyword.Location = new Point(2, 2);
			txtKeyword.Name = "txtKeyword";
			txtKeyword.Properties.NullValuePrompt = "Từ khóa tìm kiếm";
			txtKeyword.Size = new Size(465, 20);
			txtKeyword.StyleController = layoutControl1;
			txtKeyword.TabIndex = 4;
			txtKeyword.EditValueChanged += txtKeyword_EditValueChanged;
			txtKeyword.PreviewKeyDown += txtKeyword_PreviewKeyDown;
			layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Items.AddRange(new BaseLayoutItem[6] { layoutControlItem1, layoutControlItem2, emptySpaceItem1, layoutControlItem4, layoutControlItem5, layoutControlItem3 });
			layoutControlGroup1.Location = new Point(0, 0);
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			layoutControlGroup1.Size = new Size(644, 432);
			layoutControlGroup1.TextVisible = false;
			layoutControlItem1.Control = txtKeyword;
			layoutControlItem1.Location = new Point(0, 0);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new Size(469, 26);
			layoutControlItem1.TextSize = new Size(0, 0);
			layoutControlItem1.TextVisible = false;
			layoutControlItem2.Control = btnSearch;
			layoutControlItem2.Location = new Point(469, 0);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Size = new Size(83, 26);
			layoutControlItem2.TextSize = new Size(0, 0);
			layoutControlItem2.TextVisible = false;
			emptySpaceItem1.AllowHotTrack = false;
			emptySpaceItem1.Location = new Point(0, 406);
			emptySpaceItem1.Name = "emptySpaceItem1";
			emptySpaceItem1.Size = new Size(532, 26);
			emptySpaceItem1.TextSize = new Size(0, 0);
			layoutControlItem4.Control = btnChoose;
			layoutControlItem4.Location = new Point(532, 406);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new Size(112, 26);
			layoutControlItem4.TextSize = new Size(0, 0);
			layoutControlItem4.TextVisible = false;
			layoutControlItem5.Control = panelControl1;
			layoutControlItem5.Location = new Point(0, 26);
			layoutControlItem5.Name = "layoutControlItem5";
			layoutControlItem5.Size = new Size(644, 380);
			layoutControlItem5.TextSize = new Size(0, 0);
			layoutControlItem5.TextVisible = false;
			layoutControlItem3.Control = btnAdd;
			layoutControlItem3.Location = new Point(552, 0);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new Size(92, 26);
			layoutControlItem3.TextSize = new Size(0, 0);
			layoutControlItem3.TextVisible = false;
			imageCollection1.ImageStream = (ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
			imageCollection1.Images.SetKeyName(0, "dau tích-01.jpg");
			imageCollection1.Images.SetKeyName(1, "dau tích-02.jpg");
			((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			((Form)this).ClientSize = new Size(644, 461);
			((Control)this).Controls.Add(layoutControl1);
			((Control)this).Controls.Add(barDockControlLeft);
			((Control)this).Controls.Add(barDockControlRight);
			((Control)this).Controls.Add(barDockControlBottom);
			((Control)this).Controls.Add(barDockControlTop);
			((Control)this).Name = "FormPtttMethod";
			((Form)this).StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)this).Text = "Phương pháp thực tế";
			((Form)this).Load += FormPtttMethod_Load;
			((Control)this).Controls.SetChildIndex(barDockControlTop, 0);
			((Control)this).Controls.SetChildIndex(barDockControlBottom, 0);
			((Control)this).Controls.SetChildIndex(barDockControlRight, 0);
			((Control)this).Controls.SetChildIndex(barDockControlLeft, 0);
			((Control)this).Controls.SetChildIndex(layoutControl1, 0);
			((ISupportInitialize)repositoryItemSpinEdit2).EndInit();
			((ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			((ISupportInitialize)panelControl1).EndInit();
			panelControl1.ResumeLayout(false);
			((ISupportInitialize)pictureEdit1.Properties).EndInit();
			((ISupportInitialize)barManager1).EndInit();
			((ISupportInitialize)gridControlPtttMethod).EndInit();
			((ISupportInitialize)gridViewPtttMethod).EndInit();
			((ISupportInitialize)BbtnUser_PTTT).EndInit();
			((ISupportInitialize)BbtnUnCheck).EndInit();
			((ISupportInitialize)repositoryItemGridLookUpEdit_PTTT).EndInit();
			((ISupportInitialize)repositoryItemGridLookUpEdit1View).EndInit();
			((ISupportInitialize)GridLU_PTTT).EndInit();
			((ISupportInitialize)repositoryItemCustomGridLookUpEdit1View).EndInit();
			((ISupportInitialize)BbtnCheck).EndInit();
			((ISupportInitialize)txtKeyword.Properties).EndInit();
			((ISupportInitialize)layoutControlGroup1).EndInit();
			((ISupportInitialize)layoutControlItem1).EndInit();
			((ISupportInitialize)layoutControlItem2).EndInit();
			((ISupportInitialize)emptySpaceItem1).EndInit();
			((ISupportInitialize)layoutControlItem4).EndInit();
			((ISupportInitialize)layoutControlItem5).EndInit();
			((ISupportInitialize)layoutControlItem3).EndInit();
			((ISupportInitialize)imageCollection1).EndInit();
			((Control)this).ResumeLayout(false);
			((Control)this).PerformLayout();
		}
	}
}
