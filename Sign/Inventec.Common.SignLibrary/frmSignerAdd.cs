using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using EMR.TDO;
using Inventec.Common.Integrate;
using Inventec.Common.Integrate.EditorLoader;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary
{
	public class frmSignerAdd : Form
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass39_0
		{
			public List<EMR_SIGN_ORDER> signOrders;
		}

		private List<SignTDO> listSign;

		private List<EMR_SIGNER> signers;

		private List<EMR_SIGN_TEMP> signTemplates;

		private Action<List<SignTDO>> actAfterSave;

		private Action<bool> actAfterIsSignParanelCheckchanged;

		internal const long stepNumOrder = 1L;

		private bool isSignParanel;

		private bool isEnablebbtnchkSignParanel;

		private bool isNoActionWhileSignParanelchanged;

		private string documentCode;

		private EMR_TREATMENT treatment;

		private EMR_SIGNER signer;

		private Action<bool> CreateEmr;

		private bool IsAddPatientSign = false;

		public GetDocument ReloadDocument;

		private Timer timerSign = new Timer();

		private IContainer components = null;

		private SimpleButton btnSave;

		private SimpleButton btnAddPatient;

		private SimpleButton btnAdd;

		private LabelControl labelControl1;

		private GridControl gridControl1;

		private GridView gridView1;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private GridColumn gridColumn3;

		private GridColumn gridColumn4;

		private RepositoryItemSpinEdit repositoryItemSpinEdit1;

		private GridColumn gridColumn5;

		private RepositoryItemButtonEdit repositoryItemButtonEdit1;

		private TextEdit txtLoginName;

		private GridLookUpEdit cboSigner;

		private GridView gridLookUpEdit1View;

		private CheckEdit chkISignParanel;

		private LabelControl labelControl2;

		private GridLookUpEdit cboSignTemplate;

		private GridView gridView2;

		private LabelControl labelControl3;

		private SimpleButton btnCreateEmr;

		public frmSignerAdd(List<SignTDO> listsign, Action<List<SignTDO>> actAfterSave, Action<bool> actAfterIsSignParanelCheckchanged, bool isSignParanel, bool isEnablebbtnchkSignParanel, string documentCode, EMR_TREATMENT _treatment, EMR_SIGNER _signer, bool IsAddPatientSign, Action<bool> CreateEmr)
			: this(listsign, actAfterSave, actAfterIsSignParanelCheckchanged, isSignParanel, isEnablebbtnchkSignParanel, documentCode, _treatment, _signer)
		{
			try
			{
				this.IsAddPatientSign = IsAddPatientSign;
				this.CreateEmr = CreateEmr;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public frmSignerAdd(List<SignTDO> listsign, Action<List<SignTDO>> actAfterSave, Action<bool> actAfterIsSignParanelCheckchanged, bool isSignParanel, bool isEnablebbtnchkSignParanel, string documentCode, EMR_TREATMENT _treatment, EMR_SIGNER _signer)
		{
			try
			{
				InitializeComponent();
				listSign = listsign;
				this.actAfterSave = actAfterSave;
				this.actAfterIsSignParanelCheckchanged = actAfterIsSignParanelCheckchanged;
				this.isSignParanel = isSignParanel;
				this.isEnablebbtnchkSignParanel = isEnablebbtnchkSignParanel;
				this.documentCode = documentCode;
				treatment = _treatment;
				signer = _signer;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void frmSignerAdd_Load(object sender, EventArgs e)
		{
			try
			{
				isNoActionWhileSignParanelchanged = true;
				chkISignParanel.Checked = isSignParanel;
				chkISignParanel.Enabled = isEnablebbtnchkSignParanel;
				isNoActionWhileSignParanelchanged = false;
				InitComboCommon(cboSigner, GetSigner(), "ID", "USERNAME", "USERNAME", 150, "TITLE", 150, "DEPARTMENT_NAME", 200, "", 0);
				InitComboCommon(cboSignTemplate, GetSignTemplate(), "ID", "SIGN_TEMP_NAME", "SIGN_TEMP_CODE");
				gridView1.BeginUpdate();
				gridView1.GridControl.DataSource = listSign;
				gridView1.EndUpdate();
				if (!string.IsNullOrEmpty(documentCode))
				{
					chkISignParanel.Enabled = false;
				}
				if (IsAddPatientSign && (listSign == null || listSign.Count == 0 || listSign.FirstOrDefault((SignTDO o) => o.PatientCode == treatment.PATIENT_CODE) == null))
				{
					btnAddPatient_Click(null, null);
					if (GlobalStore.EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION == "1")
					{
						CheckEdit checkEdit = chkISignParanel;
						bool flag = (chkISignParanel.Enabled = false);
						checkEdit.Checked = flag;
					}
				}
				timerSign.Interval = 500;
				timerSign.Tick += TimerSign_Tick;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void TimerSign_Tick(object sender, EventArgs e)
		{
			try
			{
				if (ReloadDocument != null)
				{
					btnCreateEmr.Enabled = string.IsNullOrEmpty(ReloadDocument());
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private List<EMR_SIGNER> GetSigner()
		{
			signers = (from o in new EmrSigner().Get()
				where o.IS_ACTIVE == 1
				orderby o.USERNAME, o.NUM_ORDER
				select o).ToList();
			return signers;
		}

		private List<EMR_SIGN_TEMP> GetSignTemplate()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			EmrSignTempFilter val = new EmrSignTempFilter();
			((FilterBase)val).IS_ACTIVE = (short)1;
			((FilterBase)val).ORDER_DIRECTION = "ASC";
			((FilterBase)val).ORDER_FIELD = "SIGN_TEMP_CODE";
			((FilterBase)val).CREATOR = signer.LOGINNAME;
			signTemplates = new EmrSignTemplate().Get(val);
			return signTemplates;
		}

		private EMR_SIGNER GetSignerById(long id)
		{
			return signers.FirstOrDefault((EMR_SIGNER o) => o.ID == id);
		}

		private void FocusShowpopup(LookUpEdit cboEditor, bool isSelectFirstRow)
		{
			try
			{
				cboEditor.Focus();
				cboEditor.ShowPopup();
				if (isSelectFirstRow)
				{
					PopupLoader.SelectFirstRowPopup(cboEditor);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FocusShowpopup(GridLookUpEdit cboEditor, bool isSelectFirstRow)
		{
			try
			{
				cboEditor.Focus();
				cboEditor.ShowPopup();
				if (isSelectFirstRow)
				{
					PopupLoader.SelectFirstRowPopup(cboEditor);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void InitComboCommon(Control cboEditor, object data, string valueMember, string displayMember, string displayMemberCode)
		{
			try
			{
				InitComboCommon(cboEditor, data, valueMember, displayMember, displayMember, 0, displayMemberCode, 0, "", 0, "", 0);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void InitComboCommon(Control cboEditor, object data, string valueMember, string displayMember, string col1Name, int col1Width, string col2Name, int col2Width, string col3Name, int col3Width, string col4Name, int col4Width)
		{
			try
			{
				int num = 0;
				int num2 = 1;
				List<ColumnInfo> list = new List<ColumnInfo>();
				if (!string.IsNullOrEmpty(col1Name))
				{
					list.Add(new ColumnInfo(col1Name, "", (col1Width > 0) ? col1Width : 250, num2, true));
					num += ((col1Width > 0) ? col1Width : 250);
					num2++;
				}
				if (!string.IsNullOrEmpty(col2Name))
				{
					list.Add(new ColumnInfo(col2Name, "", (col2Width > 0) ? col2Width : 100, num2, true));
					num += ((col2Width > 0) ? col2Width : 100);
					num2++;
				}
				if (!string.IsNullOrEmpty(col3Name))
				{
					list.Add(new ColumnInfo(col3Name, "", (col3Width > 0) ? col3Width : 100, num2, true));
					num += ((col3Width > 0) ? col3Width : 100);
					num2++;
				}
				if (!string.IsNullOrEmpty(col4Name))
				{
					list.Add(new ColumnInfo(col4Name, "", (col4Width > 0) ? col4Width : 100, num2, true));
					num += ((col4Width > 0) ? col4Width : 100);
					num2++;
				}
				ControlEditorADO controlEditorADO = new ControlEditorADO(displayMember, valueMember, list, false, num);
				ControlEditorLoader.Load(cboEditor, data, controlEditorADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private long GetMaxNumOrder()
		{
			long result = 1L;
			if (listSign != null && listSign.Count > 0)
			{
				result = listSign.Max((SignTDO o) => o.NumOrder) + 1;
			}
			return result;
		}

		private bool CheckExistsPatientSign()
		{
			bool flag = false;
			try
			{
				return listSign.Any((SignTDO o) => o.PatientCode == treatment.PATIENT_CODE);
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			try
			{
				if (cboSigner.EditValue == null)
				{
					MessageBox.Show("Chưa chọn người ký");
					FocusShowpopup(cboSigner, false);
					return;
				}
				SignTDO val = new SignTDO();
				EMR_SIGNER signerById = GetSignerById((long)cboSigner.EditValue);
				val.SignerId = signerById.ID;
				val.Loginname = signerById.LOGINNAME;
				val.Username = signerById.USERNAME;
				val.FullName = signerById.USERNAME;
				val.FirstName = signerById.USERNAME;
				val.NumOrder = GetMaxNumOrder();
				val.Title = signerById.TITLE;
				val.DepartmentCode = signerById.DEPARTMENT_CODE;
				val.DepartmentName = signerById.DEPARTMENT_NAME;
				val.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
				if (listSign == null)
				{
					listSign = new List<SignTDO>();
				}
				listSign.Add(val);
				gridView1.BeginUpdate();
				gridView1.GridControl.DataSource = listSign.OrderBy((SignTDO o) => o.NumOrder).ToList();
				gridView1.EndUpdate();
				cboSigner.EditValue = null;
				txtLoginName.Text = "";
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (actAfterSave != null)
				{
					if (listSign == null || listSign.Count <= 0 || listSign.Any((SignTDO o) => o.NumOrder > 0))
					{
						actAfterSave(listSign);
						Close();
					}
					else
					{
						MessageManager.Show("Danh sách người ký phải gán thứ tự ký");
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnAddPatient_Click(object sender, EventArgs e)
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			try
			{
				if (CheckExistsPatientSign())
				{
					MessageManager.Show(string.Format(MessageUitl.GetMessage("BenhNhanDaCoTrongLuongKy"), treatment.PATIENT_CODE));
					return;
				}
				if (listSign == null)
				{
					listSign = new List<SignTDO>();
				}
				SignTDO val = new SignTDO();
				val.Username = treatment.VIR_PATIENT_NAME;
				if (GlobalStore.EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION == "1")
				{
					val.NumOrder = 1L;
					if (listSign != null && listSign.Count > 0)
					{
						listSign.ForEach(delegate(SignTDO o)
						{
							long numOrder = o.NumOrder;
							o.NumOrder = numOrder + 1;
						});
					}
				}
				else
				{
					val.NumOrder = GetMaxNumOrder();
				}
				val.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
				val.PatientCode = treatment.PATIENT_CODE;
				val.FirstName = treatment.FIRST_NAME;
				val.LastName = treatment.LAST_NAME;
				val.FullName = treatment.VIR_PATIENT_NAME;
				listSign.Add(val);
				gridView1.BeginUpdate();
				gridView1.GridControl.DataSource = listSign.OrderBy((SignTDO o) => o.NumOrder).ToList();
				gridView1.EndUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			try
			{
				SignTDO item = (SignTDO)gridView1.GetFocusedRow();
				if (MessageBox.Show("Bạn có muốn xóa dữ liệu không", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					listSign.Remove(item);
					gridView1.BeginUpdate();
					gridView1.GridControl.DataSource = ((listSign != null) ? listSign.OrderBy((SignTDO o) => o.NumOrder).ToList() : null);
					gridView1.EndUpdate();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
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
					SignTDO val = (SignTDO)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
					if (val != null && e.Column.FieldName == "UsernameDisplay")
					{
						e.Value = (string.IsNullOrEmpty(val.Loginname) ? (val.FullName + " (Bệnh nhân)") : val.Username);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void txtLoginName_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode != Keys.Return)
				{
					return;
				}
				string searchCode = (sender as TextEdit).Text.ToUpper();
				if (string.IsNullOrEmpty(searchCode))
				{
					cboSigner.EditValue = null;
					FocusShowpopup(cboSigner, true);
					return;
				}
				List<EMR_SIGNER> list = signers.Where((EMR_SIGNER o) => o.IS_ACTIVE == 1 && o.LOGINNAME.ToUpper().Contains(searchCode.ToUpper())).ToList();
				List<EMR_SIGNER> list2 = ((list == null || list.Count <= 0) ? null : ((list.Count == 1) ? list : list.Where((EMR_SIGNER o) => o.LOGINNAME.ToUpper() == searchCode.ToUpper()).ToList()));
				if (list2 != null && list2.Count == 1)
				{
					cboSigner.EditValue = list2[0].ID;
					txtLoginName.Text = list2[0].LOGINNAME;
					btnAdd.Focus();
					e.Handled = true;
				}
				else
				{
					cboSigner.EditValue = null;
					FocusShowpopup(cboSigner, true);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void cboSigner_Closed(object sender, ClosedEventArgs e)
		{
			try
			{
				if (e.CloseMode != PopupCloseMode.Normal)
				{
					return;
				}
				if (cboSigner.EditValue != null)
				{
					EMR_SIGNER val = signers.FirstOrDefault((EMR_SIGNER o) => o.IS_ACTIVE == 1 && o.ID == TypeConvertParse.ToInt64((cboSigner.EditValue ?? "").ToString()));
					if (val != null)
					{
						txtLoginName.Text = val.LOGINNAME;
					}
				}
				btnAdd.Focus();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void cboSigner_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					if (cboSigner.EditValue != null)
					{
						EMR_SIGNER val = signers.FirstOrDefault((EMR_SIGNER o) => o.IS_ACTIVE == 1 && o.ID == TypeConvertParse.ToInt64((cboSigner.EditValue ?? "").ToString()));
						if (val != null)
						{
							txtLoginName.Text = val.LOGINNAME;
							btnAdd.Focus();
						}
					}
				}
				else
				{
					cboSigner.ShowPopup();
					PopupLoader.SelectFirstRowPopup(cboSigner);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void chkISignParanel_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (!isNoActionWhileSignParanelchanged && actAfterIsSignParanelCheckchanged != null)
				{
					actAfterIsSignParanelCheckchanged(chkISignParanel.Checked);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void cboSignTemplate_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					if (cboSignTemplate.EditValue != null)
					{
						EMR_SIGN_TEMP val = signTemplates.FirstOrDefault((EMR_SIGN_TEMP o) => o.IS_ACTIVE == 1 && o.ID == TypeConvertParse.ToInt64((cboSignTemplate.EditValue ?? "").ToString()));
						if (val != null)
						{
							ProcessSelectSignTemp(val);
							btnAdd.Focus();
						}
					}
				}
				else
				{
					cboSigner.ShowPopup();
					PopupLoader.SelectFirstRowPopup(cboSigner);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessSelectSignTemp(EMR_SIGN_TEMP signTemp)
		{
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Expected O, but got Unknown
			_003C_003Ec__DisplayClass39_0 _003C_003Ec__DisplayClass39_ = new _003C_003Ec__DisplayClass39_0();
			gridControl1.DataSource = null;
			listSign = new List<SignTDO>();
			_003C_003Ec__DisplayClass39_.signOrders = new EmrSignOrder().GetByTemp(signTemp.ID);
			if (_003C_003Ec__DisplayClass39_.signOrders == null || _003C_003Ec__DisplayClass39_.signOrders.Count <= 0)
			{
				return;
			}
			foreach (EMR_SIGN_ORDER item in _003C_003Ec__DisplayClass39_.signOrders)
			{
				SignTDO val = new SignTDO();
				if (item.SIGNER_ID.HasValue && item.SIGNER_ID.Value > 0 && item.IS_PATIENT_SIGN != 1)
				{
					EMR_SIGNER signerById = GetSignerById(item.SIGNER_ID.Value);
					if (signerById == null)
					{
						LogSystem.Info("ProcessSelectSignTemp: khong tim thay signer theo thong tin signorder____" + LogUtil.TraceData(LogUtil.GetMemberName<EMR_SIGN_ORDER>((Expression<Func<EMR_SIGN_ORDER>>)(() => item)), (object)item));
						continue;
					}
					val.SignerId = signerById.ID;
					val.Loginname = signerById.LOGINNAME;
					val.Username = signerById.USERNAME;
					val.FullName = signerById.USERNAME;
					val.FirstName = signerById.USERNAME;
					val.Title = signerById.TITLE;
					val.DepartmentCode = signerById.DEPARTMENT_CODE;
					val.DepartmentName = signerById.DEPARTMENT_NAME;
				}
				else if (item.IS_PATIENT_SIGN.HasValue && item.IS_PATIENT_SIGN.Value == 1)
				{
					if (CheckExistsPatientSign())
					{
						LogSystem.Info("ProcessSelectSignTemp.CheckExistsPatientSign: da co thiet lap benh nhan ky__du lieu bi trung lap__can kiem tra lai____" + LogUtil.TraceData(LogUtil.GetMemberName<EMR_SIGN_ORDER>((Expression<Func<EMR_SIGN_ORDER>>)(() => item)), (object)item) + LogUtil.TraceData(LogUtil.GetMemberName<List<EMR_SIGN_ORDER>>(Expression.Lambda<Func<List<EMR_SIGN_ORDER>>>(Expression.Field(Expression.Constant(_003C_003Ec__DisplayClass39_, typeof(_003C_003Ec__DisplayClass39_0)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)_003C_003Ec__DisplayClass39_.signOrders));
						continue;
					}
					val.Username = treatment.VIR_PATIENT_NAME;
					val.PatientCode = treatment.PATIENT_CODE;
					val.FirstName = treatment.FIRST_NAME;
					val.LastName = treatment.LAST_NAME;
					val.FullName = treatment.VIR_PATIENT_NAME;
				}
				val.NumOrder = item.NUM_ORDER;
				val.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
				if (listSign == null)
				{
					listSign = new List<SignTDO>();
				}
				listSign.Add(val);
			}
			gridView1.BeginUpdate();
			gridView1.GridControl.DataSource = listSign.OrderBy((SignTDO o) => o.NumOrder).ToList();
			gridView1.EndUpdate();
		}

		private void cboSignTemplate_Closed(object sender, ClosedEventArgs e)
		{
			try
			{
				if (e.CloseMode != PopupCloseMode.Normal)
				{
					return;
				}
				if (cboSignTemplate.EditValue != null)
				{
					EMR_SIGN_TEMP val = signTemplates.FirstOrDefault((EMR_SIGN_TEMP o) => o.IS_ACTIVE == 1 && o.ID == TypeConvertParse.ToInt64((cboSignTemplate.EditValue ?? "").ToString()));
					if (val != null)
					{
						ProcessSelectSignTemp(val);
					}
				}
				btnAdd.Focus();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnCreateEmr_Click(object sender, EventArgs e)
		{
			try
			{
				if (listSign == null || listSign.Count <= 0 || listSign.Any((SignTDO o) => o.NumOrder > 0))
				{
					if (actAfterSave != null)
					{
						actAfterSave(listSign);
					}
					if (CreateEmr != null)
					{
						CreateEmr(true);
					}
					timerSign.Start();
				}
				else
				{
					MessageManager.Show("Danh sách người ký phải gán thứ tự ký");
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
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.btnAddPatient = new DevExpress.XtraEditors.SimpleButton();
			this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.txtLoginName = new DevExpress.XtraEditors.TextEdit();
			this.cboSigner = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.chkISignParanel = new DevExpress.XtraEditors.CheckEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.cboSignTemplate = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.btnCreateEmr = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtLoginName.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cboSigner.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit1View).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkISignParanel.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cboSignTemplate.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView2).BeginInit();
			base.SuspendLayout();
			this.btnSave.Location = new System.Drawing.Point(530, 312);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(71, 23);
			this.btnSave.TabIndex = 9;
			this.btnSave.Text = "Cập nhật";
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.btnAddPatient.Location = new System.Drawing.Point(464, 48);
			this.btnAddPatient.Name = "btnAddPatient";
			this.btnAddPatient.Size = new System.Drawing.Size(97, 23);
			this.btnAddPatient.TabIndex = 7;
			this.btnAddPatient.Text = "Thêm BN ký";
			this.btnAddPatient.Click += new System.EventHandler(btnAddPatient_Click);
			this.btnAdd.Location = new System.Drawing.Point(349, 48);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(87, 23);
			this.btnAdd.TabIndex = 8;
			this.btnAdd.Text = "Thêm người ký";
			this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
			this.labelControl1.Location = new System.Drawing.Point(47, 52);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(73, 13);
			this.labelControl1.TabIndex = 6;
			this.labelControl1.Text = "Chọn người ký:";
			this.gridControl1.Location = new System.Drawing.Point(12, 86);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[2] { this.repositoryItemButtonEdit1, this.repositoryItemSpinEdit1 });
			this.gridControl1.Size = new System.Drawing.Size(682, 220);
			this.gridControl1.TabIndex = 5;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[5] { this.gridColumn1, this.gridColumn2, this.gridColumn3, this.gridColumn4, this.gridColumn5 });
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.OptionsView.ShowIndicator = false;
			this.gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
			this.gridColumn1.Caption = "Người ký";
			this.gridColumn1.FieldName = "UsernameDisplay";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 115;
			this.gridColumn2.Caption = "Chức danh";
			this.gridColumn2.FieldName = "Title";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 154;
			this.gridColumn3.Caption = "Đơn vị";
			this.gridColumn3.FieldName = "DepartmentName";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn3.Width = 120;
			this.gridColumn4.Caption = "Thứ tự ký";
			this.gridColumn4.ColumnEdit = this.repositoryItemSpinEdit1;
			this.gridColumn4.FieldName = "NumOrder";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 3;
			this.gridColumn4.Width = 55;
			this.repositoryItemSpinEdit1.AutoHeight = false;
			this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
			this.gridColumn5.Caption = "Xóa";
			this.gridColumn5.ColumnEdit = this.repositoryItemButtonEdit1;
			this.gridColumn5.FieldName = "DEL";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.gridColumn5.OptionsColumn.ShowCaption = false;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 4;
			this.gridColumn5.Width = 29;
			this.repositoryItemButtonEdit1.AutoHeight = false;
			this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
			});
			this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
			this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
			this.txtLoginName.Location = new System.Drawing.Point(129, 50);
			this.txtLoginName.Name = "txtLoginName";
			this.txtLoginName.Size = new System.Drawing.Size(54, 20);
			this.txtLoginName.TabIndex = 12;
			this.txtLoginName.KeyDown += new System.Windows.Forms.KeyEventHandler(txtLoginName_KeyDown);
			this.cboSigner.Location = new System.Drawing.Point(183, 50);
			this.cboSigner.Name = "cboSigner";
			this.cboSigner.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.cboSigner.Properties.NullText = "";
			this.cboSigner.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
			this.cboSigner.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			this.cboSigner.Properties.View = this.gridLookUpEdit1View;
			this.cboSigner.Size = new System.Drawing.Size(160, 20);
			this.cboSigner.TabIndex = 13;
			this.cboSigner.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(cboSigner_Closed);
			this.cboSigner.KeyUp += new System.Windows.Forms.KeyEventHandler(cboSigner_KeyUp);
			this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
			this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			this.chkISignParanel.Location = new System.Drawing.Point(470, 314);
			this.chkISignParanel.Name = "chkISignParanel";
			this.chkISignParanel.Properties.Caption = "";
			this.chkISignParanel.Size = new System.Drawing.Size(46, 19);
			this.chkISignParanel.TabIndex = 15;
			this.chkISignParanel.CheckedChanged += new System.EventHandler(chkISignParanel_CheckedChanged);
			this.labelControl2.Location = new System.Drawing.Point(390, 317);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(68, 13);
			this.labelControl2.TabIndex = 14;
			this.labelControl2.Text = "Ký song song:";
			this.cboSignTemplate.Location = new System.Drawing.Point(129, 12);
			this.cboSignTemplate.Name = "cboSignTemplate";
			this.cboSignTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.cboSignTemplate.Properties.NullText = "";
			this.cboSignTemplate.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
			this.cboSignTemplate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			this.cboSignTemplate.Properties.View = this.gridView2;
			this.cboSignTemplate.Size = new System.Drawing.Size(214, 20);
			this.cboSignTemplate.TabIndex = 16;
			this.cboSignTemplate.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(cboSignTemplate_Closed);
			this.cboSignTemplate.KeyUp += new System.Windows.Forms.KeyEventHandler(cboSignTemplate_KeyUp);
			this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			this.labelControl3.Location = new System.Drawing.Point(40, 14);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(80, 13);
			this.labelControl3.TabIndex = 6;
			this.labelControl3.Text = "Mẫu thiết lập ký:";
			this.btnCreateEmr.Location = new System.Drawing.Point(607, 312);
			this.btnCreateEmr.Name = "btnCreateEmr";
			this.btnCreateEmr.Size = new System.Drawing.Size(87, 23);
			this.btnCreateEmr.TabIndex = 17;
			this.btnCreateEmr.Text = "Tạo văn bản";
			this.btnCreateEmr.Click += new System.EventHandler(btnCreateEmr_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(706, 346);
			base.Controls.Add(this.btnCreateEmr);
			base.Controls.Add(this.cboSignTemplate);
			base.Controls.Add(this.chkISignParanel);
			base.Controls.Add(this.labelControl2);
			base.Controls.Add(this.txtLoginName);
			base.Controls.Add(this.cboSigner);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.btnAddPatient);
			base.Controls.Add(this.btnAdd);
			base.Controls.Add(this.labelControl3);
			base.Controls.Add(this.labelControl1);
			base.Controls.Add(this.gridControl1);
			base.MaximizeBox = false;
			base.Name = "frmSignerAdd";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tạo luồng ký";
			base.Load += new System.EventHandler(frmSignerAdd_Load);
			((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemButtonEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtLoginName.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cboSigner.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit1View).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkISignParanel.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cboSignTemplate.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView2).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
