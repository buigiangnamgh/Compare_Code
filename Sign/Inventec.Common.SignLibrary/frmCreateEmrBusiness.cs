using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using EMR.TDO;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Desktop.Common.Controls.ValidationRule;

namespace Inventec.Common.SignLibrary
{
	public class frmCreateEmrBusiness : Form
	{
		private int rowFocus;

		private int positionHandleControl;

		private Action<List<SignTDO>> actChoose;

		private Action<bool> CreateEmr;

		public GetDocument ReloadDocument;

		private System.Windows.Forms.Timer timerSign = new System.Windows.Forms.Timer();

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private SimpleButton btnSave;

		private TreeList treeList1;

		private SimpleButton btnLoginName;

		private TextEdit txtLoginName;

		private GridLookUpEdit cboLoginName;

		private GridView gridLookUpEdit3View;

		private GridLookUpEdit cboSample;

		private GridView gridLookUpEdit1View;

		private LayoutControlItem layoutControlItem2;

		private LayoutControlItem layoutControlItem5;

		private LayoutControlItem layoutControlItem4;

		private LayoutControlItem layoutControlItem7;

		private LayoutControlItem layoutControlItem8;

		private LayoutControlItem layoutControlItem9;

		private EmptySpaceItem emptySpaceItem1;

		private EmptySpaceItem emptySpaceItem2;

		private TreeListColumn treeListColumn1;

		private TreeListColumn treeListColumn2;

		private TreeListColumn treeListColumn3;

		private TreeListColumn treeListColumn4;

		private TreeListColumn treeListColumn5;

		private TreeListColumn treeListColumn6;

		private RepositoryItemButtonEdit repDown;

		private RepositoryItemButtonEdit repUp;

		private RepositoryItemButtonEdit repDelete;

		private DXValidationProvider dxValidationProvider1;

		private SimpleButton btnCreateEmr;

		private LayoutControlItem layoutControlItem1;

		private RepositoryItemButtonEdit repAdd;

		private RepositoryItemGridLookUpEdit repSigner;

		private GridView repositoryItemGridLookUpEdit1View;

		private RepositoryItemTextEdit repTextDisable;

		private EMR_BUSINESS currentBusiness { get; set; }

		private List<EMR_BUSINESS> emrBusiness { get; set; }

		private List<EMR_FLOW> emrFlowAll { get; set; }

		private List<EMR_FLOW> emrFlow { get; set; }

		private List<EMR_SIGNER> emrSigner { get; set; }

		private List<EMR_SIGNER_FLOW> emrSignerFlow { get; set; }

		private List<EmrBusinessADO> lstEmrBusiness { get; set; }

		private string currentBusinessCode { get; set; }

		private Action<bool> IsReloadEmrBusiness { get; set; }

		private string departmentCode { get; set; }

		private List<SignTDO> listSign { get; set; }

		public frmCreateEmrBusiness(string departmentCode, List<EMR_BUSINESS> emrBusiness, List<SignTDO> listSign, Action<List<SignTDO>> actChoose, string currentBusinessCode, Action<bool> CreateEmr)
		{
			InitializeComponent();
			try
			{
				this.departmentCode = departmentCode;
				this.actChoose = actChoose;
				this.emrBusiness = emrBusiness;
				this.currentBusinessCode = currentBusinessCode;
				this.listSign = listSign;
				this.CreateEmr = CreateEmr;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void frmCreateEmrBusiness_Load(object sender, EventArgs e)
		{
			try
			{
				CreatThreadLoadData();
				LoadComboSampleBusiness();
				LoadDefaultCombo();
				LoadGrid();
				timerSign.Interval = 500;
				timerSign.Tick += TimerSign_Tick;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void LoadGrid()
		{
			try
			{
				if (listSign == null || listSign.Count <= 0)
				{
					return;
				}
				List<IGrouping<long?, SignTDO>> list = (from o in listSign
					group o by o.FlowId).ToList();
				foreach (IGrouping<long?, SignTDO> item in list)
				{
					long? key = item.Key;
					if (key.GetValueOrDefault() <= 0 || !key.HasValue || !AddKey(item.Key.GetValueOrDefault()))
					{
						continue;
					}
					EMR_FLOW flow = emrFlow.FirstOrDefault((EMR_FLOW o) => o.ID == item.Key);
					int num = 1;
					List<string> signer = item.ToList()[0].UnSigners.Split(',').ToList();
					List<EMR_SIGNER> list2 = new List<EMR_SIGNER>();
					foreach (string em in signer)
					{
						List<EMR_SIGNER> source = emrSigner;
						Func<EMR_SIGNER, bool> predicate = (EMR_SIGNER o) => o.LOGINNAME == em;
						EMR_SIGNER eMR_SIGNER = source.FirstOrDefault(predicate);
						if (eMR_SIGNER != null)
						{
							list2.Add(eMR_SIGNER);
						}
					}
					list2.Sort((EMR_SIGNER x, EMR_SIGNER y) => signer.IndexOf(x.LOGINNAME).CompareTo(signer.IndexOf(y.LOGINNAME)));
					foreach (EMR_SIGNER item2 in list2)
					{
						AddChild(flow, item2, num);
						num++;
					}
					AddChild(flow, new EMR_SIGNER(), 32767, true);
				}
				LoadDataTree();
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

		private void LoadDefaultCombo()
		{
			try
			{
				LoadComboSigner(emrSigner);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void CreatThreadLoadData()
		{
			Thread thread = new Thread(LoadFlow);
			Thread thread2 = new Thread(LoadSigner);
			Thread thread3 = new Thread(LoadBusiness);
			Thread thread4 = new Thread(LoadSignerFlow);
			try
			{
				thread4.Start();
				thread.Start();
				thread2.Start();
				thread3.Start();
				thread4.Join();
				thread.Join();
				thread2.Join();
				thread3.Join();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				thread4.Abort();
				thread.Abort();
				thread2.Abort();
				thread3.Abort();
			}
		}

		private void LoadSignerFlow()
		{
			try
			{
				emrSignerFlow = new EmrSignerFlow().Get(new EmrSignerFlowFilter());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void LoadBusiness()
		{
			try
			{
				emrBusiness = emrBusiness.Where((EMR_BUSINESS o) => o.IS_ACTIVE == 1 && ((GlobalStore.EMR_EMR_BUSINESS_SHOW_ALL_TEMPLATES == "1" && o.CREATOR == GlobalStore.LoginName) || (GlobalStore.EMR_EMR_BUSINESS_SHOW_ALL_TEMPLATES != "1" && o.DEPARTMENT_CODE == null) || string.IsNullOrEmpty(departmentCode) || departmentCode == o.DEPARTMENT_CODE)).ToList();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void LoadSigner()
		{
			try
			{
				emrSigner = new EmrSigner().Get(new EmrSignerFilter
				{
					IS_ACTIVE = 1
				}).ToList();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void LoadFlow()
		{
			try
			{
				emrFlowAll = new EmrFlow().Get(new EmrFlowFilter()).ToList();
				if (emrFlowAll != null && emrFlowAll.Count > 0)
				{
					emrFlow = emrFlowAll.Where((EMR_FLOW o) => o.IS_ACTIVE == 1).ToList();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void LoadComboSampleBusiness()
		{
			try
			{
				List<ColumnInfo> list = new List<ColumnInfo>();
				list.Add(new ColumnInfo("BUSINESS_CODE", "", 100, 1));
				list.Add(new ColumnInfo("BUSINESS_NAME", "", 300, 2));
				ControlEditorADO controlEditorADO = new ControlEditorADO("BUSINESS_NAME", "ID", list, false, 400);
				ControlEditorLoader.Load(cboSample, emrBusiness, controlEditorADO);
				cboSample.Properties.ImmediatePopup = true;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadComboSigner(List<EMR_SIGNER> data)
		{
			try
			{
				List<ColumnInfo> list = new List<ColumnInfo>();
				list.Add(new ColumnInfo("LOGINNAME", "", 100, 1));
				list.Add(new ColumnInfo("USERNAME", "", 300, 2));
				ControlEditorADO controlEditorADO = new ControlEditorADO("USERNAME", "ID", list, false, 400);
				ControlEditorLoader.Load(cboLoginName, data, controlEditorADO);
				cboLoginName.Properties.ImmediatePopup = true;
				ControlEditorLoader.Load(repSigner, data, controlEditorADO);
				repSigner.ImmediatePopup = true;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void ValidationSingleControlWithMaxLength(Control control, bool isRequired, int? maxLength)
		{
			try
			{
				ControlMaxLengthValidationRule controlMaxLengthValidationRule = new ControlMaxLengthValidationRule();
				controlMaxLengthValidationRule.editor = control;
				controlMaxLengthValidationRule.maxLength = maxLength;
				controlMaxLengthValidationRule.IsRequired = isRequired;
				controlMaxLengthValidationRule.ErrorType = ErrorType.Warning;
				dxValidationProvider1.SetValidationRule(control, controlMaxLengthValidationRule);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void dxValidationProvider1_ValidationFailed(object sender, ValidationFailedEventArgs e)
		{
			try
			{
				BaseEdit baseEdit = e.InvalidControl as BaseEdit;
				if (baseEdit == null)
				{
					return;
				}
				BaseEditViewInfo baseEditViewInfo = baseEdit.GetViewInfo() as BaseEditViewInfo;
				if (baseEditViewInfo == null)
				{
					return;
				}
				if (positionHandleControl == -1)
				{
					positionHandleControl = baseEdit.TabIndex;
					if (baseEdit.Visible)
					{
						baseEdit.SelectAll();
						baseEdit.Focus();
					}
				}
				if (positionHandleControl > baseEdit.TabIndex)
				{
					positionHandleControl = baseEdit.TabIndex;
					if (baseEdit.Visible)
					{
						baseEdit.SelectAll();
						baseEdit.Focus();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void AddSignerFlow(List<EMR_SIGNER_FLOW> emrSignerIds)
		{
			try
			{
				if (!AddKey(emrSignerIds.First().FLOW_ID))
				{
					return;
				}
				List<EMR_SIGNER> list = emrSigner.Where((EMR_SIGNER o) => emrSignerIds.Select((EMR_SIGNER_FLOW p) => p.SIGNER_ID).ToList().Exists((long p) => p == o.ID)).ToList();
				EMR_FLOW flow = emrFlow.FirstOrDefault((EMR_FLOW o) => o.ID == emrSignerIds.First().FLOW_ID);
				int num = 1;
				foreach (EMR_SIGNER item in list)
				{
					AddChild(flow, item, num);
					num++;
				}
				AddChild(flow, new EMR_SIGNER(), 32767, true);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void cboSample_Closed(object sender, ClosedEventArgs e)
		{
			try
			{
				if (cboSample.EditValue == null)
				{
					return;
				}
				List<EMR_FLOW> list = (from o in emrFlowAll
					where o.BUSINESS_ID == long.Parse(cboSample.EditValue.ToString())
					orderby o.NUM_ORDER
					select o).ToList();
				lstEmrBusiness = new List<EmrBusinessADO>();
				LoadDataTree();
				foreach (EMR_FLOW flow in list)
				{
					List<EMR_SIGNER_FLOW> source = emrSignerFlow;
					Func<EMR_SIGNER_FLOW, bool> predicate = (EMR_SIGNER_FLOW o) => o.FLOW_ID == flow.ID;
					List<EMR_SIGNER_FLOW> list2 = source.Where(predicate).ToList();
					if (list2 != null && list2.Count > 0)
					{
						AddSignerFlow(list2);
						continue;
					}
					AddKey(flow.ID);
					AddChild(flow, new EMR_SIGNER(), 32767, true);
				}
				LoadDataTree();
				currentBusiness = emrBusiness.FirstOrDefault((EMR_BUSINESS o) => o.ID == long.Parse(cboSample.EditValue.ToString()));
				cboLoginName.EditValue = null;
				txtLoginName.Text = null;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void cboSample_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				if (e.Button.Kind == ButtonPredefines.Delete)
				{
					currentBusiness = null;
					cboSample.EditValue = null;
					LoadDefaultCombo();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void cboLoginName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (cboLoginName.EditValue != null)
				{
					btnLoginName.Enabled = true;
				}
				else
				{
					btnLoginName.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void cboLoginName_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				if (e.Button.Kind == ButtonPredefines.Delete)
				{
					cboLoginName.EditValue = null;
					txtLoginName.Text = null;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void txtLoginName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode != Keys.Return)
				{
					return;
				}
				if (!string.IsNullOrEmpty(txtLoginName.Text.Trim()))
				{
					List<EMR_SIGNER> list = cboLoginName.Properties.DataSource as List<EMR_SIGNER>;
					if (list != null && list.Count > 0)
					{
						EMR_SIGNER eMR_SIGNER = list.FirstOrDefault((EMR_SIGNER o) => o.LOGINNAME.Equals(txtLoginName.Text.Trim()));
						if (eMR_SIGNER != null)
						{
							cboLoginName.Focus();
							cboLoginName.EditValue = eMR_SIGNER.ID;
							return;
						}
					}
				}
				cboLoginName.Focus();
				cboLoginName.ShowPopup();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private bool AddKey(long flowId)
		{
			try
			{
				if (lstEmrBusiness == null)
				{
					lstEmrBusiness = new List<EmrBusinessADO>();
				}
				EmrBusinessADO emrBusinessADO = lstEmrBusiness.FirstOrDefault((EmrBusinessADO o) => o.FLOW_ID == flowId);
				if (emrBusinessADO != null)
				{
					MessageManager.Show(string.Format("Danh sách thiết lập ký đã có vai trò {0}.", emrBusinessADO.FLOW_NAME));
					return false;
				}
				EMR_FLOW eMR_FLOW = emrFlow.FirstOrDefault((EMR_FLOW o) => o.ID == flowId);
				EmrBusinessADO emrBusinessADO2 = new EmrBusinessADO();
				emrBusinessADO2.CONCRETE_ID__IN_SETY = eMR_FLOW.ID.ToString();
				emrBusinessADO2.USER_NAME = eMR_FLOW.FLOW_NAME;
				emrBusinessADO2.FLOW_ID = eMR_FLOW.ID;
				emrBusinessADO2.FLOW_CODE = eMR_FLOW.FLOW_CODE;
				emrBusinessADO2.FLOW_NAME = eMR_FLOW.FLOW_NAME;
				emrBusinessADO2.ROOM_CODE = eMR_FLOW.ROOM_CODE;
				emrBusinessADO2.ROOM_NAME = eMR_FLOW.ROOM_NAME;
				emrBusinessADO2.ROOM_TYPE_CODE = eMR_FLOW.ROOM_TYPE_CODE;
				if (lstEmrBusiness.Count == 0)
				{
					emrBusinessADO2.NUM_ORDER = 1L;
				}
				else
				{
					emrBusinessADO2.NUM_ORDER = lstEmrBusiness.LastOrDefault((EmrBusinessADO o) => string.IsNullOrEmpty(o.PARENT_ID__IN_SETY)).NUM_ORDER + 1;
				}
				lstEmrBusiness.Add(emrBusinessADO2);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return true;
		}

		private void AddChild(EMR_FLOW flow, EMR_SIGNER singer, int index, bool IsRowEmpty = false)
		{
			try
			{
				if (flow != null)
				{
					EmrBusinessADO emrBusinessADO = new EmrBusinessADO();
					emrBusinessADO.FLOW_ID = flow.ID;
					emrBusinessADO.PARENT_ID__IN_SETY = flow.ID.ToString();
					emrBusinessADO.CONCRETE_ID__IN_SETY = singer.ID + Guid.NewGuid().ToString();
					emrBusinessADO.USER_NAME = singer.USERNAME;
					emrBusinessADO.LOGINNAME = singer.LOGINNAME;
					emrBusinessADO.IS_LEAF = true;
					emrBusinessADO.SIGNER_ID = singer.ID;
					emrBusinessADO.TITLE = singer.TITLE;
					emrBusinessADO.DEPARTMENT_NAME = singer.DEPARTMENT_NAME;
					emrBusinessADO.NUM_ORDER = index;
					emrBusinessADO.IS_ROW_EMPTY = IsRowEmpty;
					lstEmrBusiness.Add(emrBusinessADO);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void UpdateChild(EmrBusinessADO ado, EMR_FLOW flow, EMR_SIGNER singer, int index, bool IsRowEmpty = false)
		{
			try
			{
				if (flow != null)
				{
					ado.PARENT_ID__IN_SETY = flow.ID.ToString();
					ado.CONCRETE_ID__IN_SETY = singer.ID + Guid.NewGuid().ToString();
					ado.USER_NAME = singer.USERNAME;
					ado.LOGINNAME = singer.LOGINNAME;
					ado.IS_LEAF = true;
					ado.SIGNER_ID = singer.ID;
					ado.TITLE = singer.TITLE;
					ado.DEPARTMENT_NAME = singer.DEPARTMENT_NAME;
					ado.NUM_ORDER = index;
					ado.IS_ROW_EMPTY = IsRowEmpty;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnLoginName_Click(object sender, EventArgs e)
		{
			try
			{
				if (lstEmrBusiness == null || lstEmrBusiness.Count <= 0)
				{
					return;
				}
				object dataRecordByNode = treeList1.GetDataRecordByNode(treeList1.FocusedNode);
				if (cboLoginName.EditValue == null || dataRecordByNode == null || !(dataRecordByNode is EmrBusinessADO))
				{
					return;
				}
				rowFocus = treeList1.GetVisibleIndexByNode(treeList1.FocusedNode);
				EMR_SIGNER singer = (cboLoginName.Properties.DataSource as List<EMR_SIGNER>).FirstOrDefault((EMR_SIGNER o) => o.ID == long.Parse(cboLoginName.EditValue.ToString()));
				EmrBusinessADO node = dataRecordByNode as EmrBusinessADO;
				EMR_FLOW eMR_FLOW = new EMR_FLOW();
				if (!node.IS_LEAF)
				{
					eMR_FLOW = emrFlow.FirstOrDefault((EMR_FLOW o) => o.ID == node.FLOW_ID);
					AddChild(eMR_FLOW, singer, rowFocus);
				}
				else
				{
					eMR_FLOW = emrFlow.FirstOrDefault((EMR_FLOW o) => o.ID == lstEmrBusiness.FirstOrDefault((EmrBusinessADO p) => p.CONCRETE_ID__IN_SETY == node.PARENT_ID__IN_SETY).FLOW_ID);
					AddChild(eMR_FLOW, singer, rowFocus);
				}
				LoadDataTree();
				treeList1.SetFocusedNode(treeList1.GetNodeByVisibleIndex(rowFocus));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadDataTree()
		{
			try
			{
				if (lstEmrBusiness != null && lstEmrBusiness.Count > 0)
				{
					lstEmrBusiness = (from o in lstEmrBusiness
						orderby !o.IS_LEAF, o.NUM_ORDER
						select o).ToList();
				}
				BindingList<EmrBusinessADO> dataSource = new BindingList<EmrBusinessADO>(lstEmrBusiness);
				treeList1.DataSource = dataSource;
				treeList1.ExpandAll();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
		{
			try
			{
				object dataRecordByNode = treeList1.GetDataRecordByNode(e.Node);
				if (dataRecordByNode != null && dataRecordByNode is EmrBusinessADO)
				{
					EmrBusinessADO emrBusinessADO = dataRecordByNode as EmrBusinessADO;
					if (emrBusinessADO != null && !emrBusinessADO.IS_LEAF)
					{
						e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void treeList1_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
		{
			try
			{
				object dataRecordByNode = treeList1.GetDataRecordByNode(e.Node);
				if (dataRecordByNode == null || !(dataRecordByNode is EmrBusinessADO))
				{
					return;
				}
				EmrBusinessADO emrBusinessADO = dataRecordByNode as EmrBusinessADO;
				if (emrBusinessADO != null && !emrBusinessADO.IS_LEAF)
				{
					if (e.Column.FieldName == "UP")
					{
						e.RepositoryItem = repUp;
					}
					else if (e.Column.FieldName == "DOWN")
					{
						e.RepositoryItem = repDown;
					}
					else if (e.Column.FieldName == "USER_NAME")
					{
						e.RepositoryItem = repTextDisable;
					}
				}
				else if (emrBusinessADO != null && emrBusinessADO.IS_LEAF)
				{
					if (e.Column.FieldName == "PLUS")
					{
						e.RepositoryItem = (emrBusinessADO.IS_ROW_EMPTY ? repAdd : repDelete);
					}
					else if (e.Column.FieldName == "USER_NAME")
					{
						e.RepositoryItem = (emrBusinessADO.IS_ROW_EMPTY ? repSigner : repTextDisable);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void repDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				object dataRecordByNode = treeList1.GetDataRecordByNode(treeList1.FocusedNode);
				if (dataRecordByNode == null || !(dataRecordByNode is EmrBusinessADO))
				{
					return;
				}
				EmrBusinessADO business = dataRecordByNode as EmrBusinessADO;
				lstEmrBusiness = lstEmrBusiness.Where((EmrBusinessADO o) => o.CONCRETE_ID__IN_SETY != business.CONCRETE_ID__IN_SETY).ToList();
				if (!business.IS_LEAF)
				{
					lstEmrBusiness = lstEmrBusiness.Where((EmrBusinessADO o) => o.PARENT_ID__IN_SETY != business.CONCRETE_ID__IN_SETY).ToList();
				}
				if (lstEmrBusiness != null && lstEmrBusiness.Count > 0)
				{
					int index = 1;
					lstEmrBusiness.ForEach(delegate(EmrBusinessADO o)
					{
						if (string.IsNullOrEmpty(o.PARENT_ID__IN_SETY))
						{
							o.NUM_ORDER = index++;
						}
					});
				}
				LoadDataTree();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void repDown_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				object dataRecordByNode = treeList1.GetDataRecordByNode(treeList1.FocusedNode);
				if (dataRecordByNode == null || !(dataRecordByNode is EmrBusinessADO))
				{
					return;
				}
				EmrBusinessADO business = dataRecordByNode as EmrBusinessADO;
				if (business.IS_LEAF)
				{
					return;
				}
				List<EmrBusinessADO> list = lstEmrBusiness.Where((EmrBusinessADO o) => string.IsNullOrEmpty(o.PARENT_ID__IN_SETY)).ToList();
				if (list.Count <= 1 || !(list.Last().CONCRETE_ID__IN_SETY != business.CONCRETE_ID__IN_SETY))
				{
					return;
				}
				bool nextItem = false;
				lstEmrBusiness.ForEach(delegate(EmrBusinessADO o)
				{
					if (!o.IS_LEAF && nextItem)
					{
						o.NUM_ORDER--;
						nextItem = false;
					}
					if (o.CONCRETE_ID__IN_SETY == business.CONCRETE_ID__IN_SETY)
					{
						o.NUM_ORDER++;
						nextItem = true;
					}
				});
				LoadDataTree();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void repUp_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				object dataRecordByNode = treeList1.GetDataRecordByNode(treeList1.FocusedNode);
				if (dataRecordByNode == null || !(dataRecordByNode is EmrBusinessADO))
				{
					return;
				}
				EmrBusinessADO emrBusinessADO = dataRecordByNode as EmrBusinessADO;
				if (emrBusinessADO.IS_LEAF)
				{
					return;
				}
				List<EmrBusinessADO> list = lstEmrBusiness.Where((EmrBusinessADO o) => string.IsNullOrEmpty(o.PARENT_ID__IN_SETY)).ToList();
				if (list.Count <= 1 || !(list.First().CONCRETE_ID__IN_SETY != emrBusinessADO.CONCRETE_ID__IN_SETY))
				{
					return;
				}
				for (int num = 0; num < lstEmrBusiness.Count; num++)
				{
					EmrBusinessADO item = lstEmrBusiness[num];
					if (!item.IS_LEAF && item.CONCRETE_ID__IN_SETY == emrBusinessADO.CONCRETE_ID__IN_SETY)
					{
						item.NUM_ORDER--;
						lstEmrBusiness.FirstOrDefault((EmrBusinessADO o) => !o.IS_LEAF && o.NUM_ORDER == item.NUM_ORDER).NUM_ORDER = item.NUM_ORDER + 1;
						break;
					}
				}
				LoadDataTree();
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
				positionHandleControl = -1;
				if (dxValidationProvider1.Validate())
				{
					CommonParam commonParam = new CommonParam();
					listSign = GetListSignTDO();
					if (listSign != null)
					{
						actChoose(listSign);
						Close();
					}
				}
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private List<SignTDO> GetListSignTDO()
		{
			List<SignTDO> list = new List<SignTDO>();
			try
			{
				if (lstEmrBusiness != null && lstEmrBusiness.Count > 0)
				{
					List<EmrBusinessADO> list2 = lstEmrBusiness.Where((EmrBusinessADO o) => !o.IS_LEAF).ToList();
					foreach (EmrBusinessADO item in list2)
					{
						SignTDO signTDO = new SignTDO();
						signTDO.FlowId = item.FLOW_ID;
						signTDO.NumOrder = item.NUM_ORDER;
						List<EmrBusinessADO> source = lstEmrBusiness;
						Func<EmrBusinessADO, bool> predicate = (EmrBusinessADO o) => o.PARENT_ID__IN_SETY == item.CONCRETE_ID__IN_SETY && !o.IS_ROW_EMPTY;
						List<EmrBusinessADO> list3 = source.Where(predicate).ToList();
						if (list3 != null && list3.Count > 0)
						{
							signTDO.UnSigners = string.Join(",", list3.Select((EmrBusinessADO o) => o.LOGINNAME).ToList());
						}
						list.Add(signTDO);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return list;
		}

		private void cboLoginName_Closed(object sender, ClosedEventArgs e)
		{
			try
			{
				if (cboLoginName.EditValue != null)
				{
					EMR_SIGNER eMR_SIGNER = (cboLoginName.Properties.DataSource as List<EMR_SIGNER>).FirstOrDefault((EMR_SIGNER o) => o.ID == long.Parse(cboLoginName.EditValue.ToString()));
					txtLoginName.Text = eMR_SIGNER.LOGINNAME;
				}
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
				listSign = GetListSignTDO();
				if (listSign == null || listSign.Count <= 0 || listSign.Any((SignTDO o) => o.NumOrder > 0))
				{
					if (actChoose != null)
					{
						actChoose(listSign);
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
				LogSystem.Warn(ex);
			}
		}

		private void repSigner_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void ClickAddNewRow(EMR_FLOW flow, EMR_SIGNER singer, int index, bool IsRowEmpty = false)
		{
			AddChild(flow, new EMR_SIGNER(), index, true);
			LoadDataTree();
			treeList1.SetFocusedNode(treeList1.FocusedNode);
		}

		private void repAdd_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				object dataRecordByNode = treeList1.GetDataRecordByNode(treeList1.FocusedNode);
				if (dataRecordByNode != null && dataRecordByNode is EmrBusinessADO)
				{
					EmrBusinessADO business = dataRecordByNode as EmrBusinessADO;
					EMR_FLOW flow = emrFlow.FirstOrDefault((EMR_FLOW o) => o.ID == business.FLOW_ID);
					ClickAddNewRow(flow, new EMR_SIGNER(), (int)business.NUM_ORDER + 1, true);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void repSigner_Closed(object sender, ClosedEventArgs e)
		{
			try
			{
				object dataRecordByNode = treeList1.GetDataRecordByNode(treeList1.FocusedNode);
				if (dataRecordByNode == null || !(dataRecordByNode is EmrBusinessADO))
				{
					return;
				}
				GridLookUpEdit cbo = sender as GridLookUpEdit;
				if (cbo.EditValue != null)
				{
					EMR_SIGNER singer = emrSigner.FirstOrDefault((EMR_SIGNER o) => o.ID.ToString() == cbo.EditValue.ToString());
					EmrBusinessADO business = dataRecordByNode as EmrBusinessADO;
					EMR_FLOW flow = emrFlow.FirstOrDefault((EMR_FLOW o) => o.ID == business.FLOW_ID);
					rowFocus = treeList1.GetVisibleIndexByNode(treeList1.FocusedNode);
					UpdateChild(business, flow, singer, (int)business.NUM_ORDER + 1);
					treeList1.RefreshNode(treeList1.FocusedNode);
					if (!lstEmrBusiness.Exists((EmrBusinessADO o) => (flow == null || o.FLOW_ID == flow.ID) && o.IS_ROW_EMPTY))
					{
						ClickAddNewRow(flow, new EMR_SIGNER(), (int)business.NUM_ORDER + 1, true);
					}
					LoadDataTree();
					treeList1.SetFocusedNode(treeList1.FocusedNode);
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventec.Common.SignLibrary.frmCreateEmrBusiness));
			DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearance3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceHovered3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearancePressed3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceDisabled3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearance4 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceHovered4 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearancePressed4 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceDisabled4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnCreateEmr = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.treeList1 = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repDown = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repUp = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repAdd = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repSigner = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
			this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.repTextDisable = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.btnLoginName = new DevExpress.XtraEditors.SimpleButton();
			this.txtLoginName = new DevExpress.XtraEditors.TextEdit();
			this.cboLoginName = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.cboSample = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.treeList1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repDelete).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repDown).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repUp).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repAdd).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repSigner).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemGridLookUpEdit1View).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repTextDisable).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtLoginName.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cboLoginName.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit3View).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cboSample.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit1View).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem9).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.btnCreateEmr);
			this.layoutControl1.Controls.Add(this.btnSave);
			this.layoutControl1.Controls.Add(this.treeList1);
			this.layoutControl1.Controls.Add(this.btnLoginName);
			this.layoutControl1.Controls.Add(this.txtLoginName);
			this.layoutControl1.Controls.Add(this.cboLoginName);
			this.layoutControl1.Controls.Add(this.cboSample);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(730, 322);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.btnCreateEmr.Location = new System.Drawing.Point(624, 298);
			this.btnCreateEmr.Name = "btnCreateEmr";
			this.btnCreateEmr.Size = new System.Drawing.Size(104, 22);
			this.btnCreateEmr.StyleController = this.layoutControl1;
			this.btnCreateEmr.TabIndex = 13;
			this.btnCreateEmr.Text = "Tạo văn bản";
			this.btnCreateEmr.Click += new System.EventHandler(btnCreateEmr_Click);
			this.btnSave.Location = new System.Drawing.Point(548, 298);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(72, 22);
			this.btnSave.StyleController = this.layoutControl1;
			this.btnSave.TabIndex = 12;
			this.btnSave.Text = "Chọn";
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[6] { this.treeListColumn1, this.treeListColumn2, this.treeListColumn3, this.treeListColumn4, this.treeListColumn5, this.treeListColumn6 });
			this.treeList1.Cursor = System.Windows.Forms.Cursors.Default;
			this.treeList1.KeyFieldName = "CONCRETE_ID__IN_SETY";
			this.treeList1.Location = new System.Drawing.Point(2, 52);
			this.treeList1.Name = "treeList1";
			this.treeList1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.False;
			this.treeList1.OptionsBehavior.AutoPopulateColumns = false;
			this.treeList1.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
			this.treeList1.OptionsView.ShowHorzLines = false;
			this.treeList1.OptionsView.ShowVertLines = false;
			this.treeList1.ParentFieldName = "PARENT_ID__IN_SETY";
			this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[6] { this.repDown, this.repUp, this.repDelete, this.repAdd, this.repSigner, this.repTextDisable });
			this.treeList1.Size = new System.Drawing.Size(726, 242);
			this.treeList1.TabIndex = 11;
			this.treeList1.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(treeList1_CustomNodeCellEdit);
			this.treeList1.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(treeList1_CustomDrawNodeCell);
			this.treeListColumn1.Caption = "Người ký";
			this.treeListColumn1.FieldName = "USER_NAME";
			this.treeListColumn1.MinWidth = 34;
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowAlways;
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 0;
			this.treeListColumn1.Width = 170;
			this.treeListColumn2.Caption = "Chức danh";
			this.treeListColumn2.FieldName = "TITLE";
			this.treeListColumn2.Name = "treeListColumn2";
			this.treeListColumn2.OptionsColumn.AllowEdit = false;
			this.treeListColumn2.Visible = true;
			this.treeListColumn2.VisibleIndex = 1;
			this.treeListColumn2.Width = 216;
			this.treeListColumn3.Caption = "Đơn vị";
			this.treeListColumn3.FieldName = "DEPARTMENT_NAME";
			this.treeListColumn3.Name = "treeListColumn3";
			this.treeListColumn3.OptionsColumn.AllowEdit = false;
			this.treeListColumn3.Visible = true;
			this.treeListColumn3.VisibleIndex = 2;
			this.treeListColumn3.Width = 247;
			this.treeListColumn4.Caption = " ";
			this.treeListColumn4.FieldName = "DOWN";
			this.treeListColumn4.MinWidth = 25;
			this.treeListColumn4.Name = "treeListColumn4";
			this.treeListColumn4.Visible = true;
			this.treeListColumn4.VisibleIndex = 3;
			this.treeListColumn4.Width = 25;
			this.treeListColumn5.Caption = " ";
			this.treeListColumn5.FieldName = "UP";
			this.treeListColumn5.MinWidth = 25;
			this.treeListColumn5.Name = "treeListColumn5";
			this.treeListColumn5.Visible = true;
			this.treeListColumn5.VisibleIndex = 4;
			this.treeListColumn5.Width = 25;
			this.treeListColumn6.Caption = " ";
			this.treeListColumn6.ColumnEdit = this.repDelete;
			this.treeListColumn6.FieldName = "PLUS";
			this.treeListColumn6.MinWidth = 25;
			this.treeListColumn6.Name = "treeListColumn6";
			this.treeListColumn6.Visible = true;
			this.treeListColumn6.VisibleIndex = 5;
			this.treeListColumn6.Width = 25;
			this.repDelete.AutoHeight = false;
			this.repDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("repDelete.Buttons"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, true)
			});
			this.repDelete.Name = "repDelete";
			this.repDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repDelete_ButtonClick);
			this.repDown.AutoHeight = false;
			this.repDown.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("repDown.Buttons"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, true)
			});
			this.repDown.Name = "repDown";
			this.repDown.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repDown.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repDown_ButtonClick);
			this.repUp.AutoHeight = false;
			this.repUp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("repUp.Buttons"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, true)
			});
			this.repUp.Name = "repUp";
			this.repUp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repUp.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repUp_ButtonClick);
			this.repAdd.AutoHeight = false;
			this.repAdd.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("repAdd.Buttons"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, "", null, null, true)
			});
			this.repAdd.Name = "repAdd";
			this.repAdd.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repAdd.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repAdd_ButtonClick);
			this.repSigner.AutoHeight = false;
			this.repSigner.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.repSigner.Name = "repSigner";
			this.repSigner.NullText = "";
			this.repSigner.View = this.repositoryItemGridLookUpEdit1View;
			this.repSigner.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(repSigner_Closed);
			this.repSigner.EditValueChanged += new System.EventHandler(repSigner_EditValueChanged);
			this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
			this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			this.repTextDisable.AutoHeight = false;
			this.repTextDisable.Name = "repTextDisable";
			this.repTextDisable.ReadOnly = true;
			this.btnLoginName.Enabled = false;
			this.btnLoginName.Location = new System.Drawing.Point(587, 26);
			this.btnLoginName.Name = "btnLoginName";
			this.btnLoginName.Size = new System.Drawing.Size(141, 22);
			this.btnLoginName.StyleController = this.layoutControl1;
			this.btnLoginName.TabIndex = 10;
			this.btnLoginName.Text = "Thêm người ký";
			this.btnLoginName.Click += new System.EventHandler(btnLoginName_Click);
			this.txtLoginName.Location = new System.Drawing.Point(107, 26);
			this.txtLoginName.Name = "txtLoginName";
			this.txtLoginName.Size = new System.Drawing.Size(167, 20);
			this.txtLoginName.StyleController = this.layoutControl1;
			this.txtLoginName.TabIndex = 8;
			this.txtLoginName.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(txtLoginName_PreviewKeyDown);
			this.cboLoginName.Location = new System.Drawing.Point(274, 26);
			this.cboLoginName.Name = "cboLoginName";
			this.cboLoginName.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.cboLoginName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[2]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
			});
			this.cboLoginName.Properties.NullText = "";
			this.cboLoginName.Properties.View = this.gridLookUpEdit3View;
			this.cboLoginName.Size = new System.Drawing.Size(309, 20);
			this.cboLoginName.StyleController = this.layoutControl1;
			this.cboLoginName.TabIndex = 7;
			this.cboLoginName.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(cboLoginName_Closed);
			this.cboLoginName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(cboLoginName_ButtonClick);
			this.cboLoginName.EditValueChanged += new System.EventHandler(cboLoginName_EditValueChanged);
			this.gridLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridLookUpEdit3View.Name = "gridLookUpEdit3View";
			this.gridLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridLookUpEdit3View.OptionsView.ShowGroupPanel = false;
			this.cboSample.Location = new System.Drawing.Point(107, 2);
			this.cboSample.Name = "cboSample";
			this.cboSample.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.cboSample.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[2]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
			});
			this.cboSample.Properties.NullText = "";
			this.cboSample.Properties.View = this.gridLookUpEdit1View;
			this.cboSample.Size = new System.Drawing.Size(205, 20);
			this.cboSample.StyleController = this.layoutControl1;
			this.cboSample.TabIndex = 5;
			this.cboSample.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(cboSample_Closed);
			this.cboSample.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(cboSample_ButtonClick);
			this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
			this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[9] { this.layoutControlItem2, this.layoutControlItem5, this.layoutControlItem4, this.layoutControlItem7, this.layoutControlItem8, this.layoutControlItem9, this.emptySpaceItem1, this.emptySpaceItem2, this.layoutControlItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(730, 322);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem2.Control = this.cboSample;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(314, 24);
			this.layoutControlItem2.Text = "Mẫu:";
			this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(100, 20);
			this.layoutControlItem2.TextToControlDistance = 5;
			this.layoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem5.Control = this.txtLoginName;
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
			this.layoutControlItem5.Size = new System.Drawing.Size(274, 26);
			this.layoutControlItem5.Text = "Chọn người ký:";
			this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem5.TextSize = new System.Drawing.Size(100, 20);
			this.layoutControlItem5.TextToControlDistance = 5;
			this.layoutControlItem4.Control = this.cboLoginName;
			this.layoutControlItem4.Location = new System.Drawing.Point(274, 24);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 2, 2);
			this.layoutControlItem4.Size = new System.Drawing.Size(311, 26);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			this.layoutControlItem7.Control = this.btnLoginName;
			this.layoutControlItem7.Location = new System.Drawing.Point(585, 24);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Size = new System.Drawing.Size(145, 26);
			this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem7.TextVisible = false;
			this.layoutControlItem8.Control = this.treeList1;
			this.layoutControlItem8.Location = new System.Drawing.Point(0, 50);
			this.layoutControlItem8.Name = "layoutControlItem8";
			this.layoutControlItem8.Size = new System.Drawing.Size(730, 246);
			this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem8.TextVisible = false;
			this.layoutControlItem9.Control = this.btnSave;
			this.layoutControlItem9.Location = new System.Drawing.Point(546, 296);
			this.layoutControlItem9.Name = "layoutControlItem9";
			this.layoutControlItem9.Size = new System.Drawing.Size(76, 26);
			this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem9.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 296);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(546, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(314, 0);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(416, 24);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.Control = this.btnCreateEmr;
			this.layoutControlItem1.Location = new System.Drawing.Point(622, 296);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(108, 26);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.dxValidationProvider1.ValidationFailed += new DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventHandler(dxValidationProvider1_ValidationFailed);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(730, 322);
			base.Controls.Add(this.layoutControl1);
			base.MaximizeBox = false;
			base.Name = "frmCreateEmrBusiness";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tạo nghiệp vụ ký";
			base.Load += new System.EventHandler(frmCreateEmrBusiness_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.treeList1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repDelete).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repDown).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repUp).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repAdd).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repSigner).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemGridLookUpEdit1View).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repTextDisable).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtLoginName.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cboLoginName.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit3View).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cboSample.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit1View).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem9).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
