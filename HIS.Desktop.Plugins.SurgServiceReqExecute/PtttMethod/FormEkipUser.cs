using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using HIS.Desktop.ADO;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Library.CacheClient;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Base;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using HIS.Desktop.Utilities.Extensions;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.DateTime;
using Inventec.Common.Integrate.EditorLoader;
using Inventec.Common.Logging;
using Inventec.Common.Mapper;
using Inventec.Common.Resource;
using Inventec.Common.TypeConvert;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.LocalStorage.Location;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Modules;
using Inventec.UC.Login.Base;
using Microsoft.CSharp.RuntimeBinder;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.PtttMethod
{
	public class FormEkipUser : FormBase
	{
		[CompilerGenerated]
		private static class _003C_003Eo__26
		{
            public static CallSite<Func<CallSite, BackendAdapter, string, Inventec.Common.WebApiClient.ApiConsumer, object, CommonParam, object>> _003C_003Ep__0;

			public static CallSite<Func<CallSite, object, List<HIS_EXECUTE_ROLE>>> _003C_003Ep__1;
		}

		[CompilerGenerated]
		private sealed class _003CComboExecuteRole_003Ed__26 : IAsyncStateMachine
		{
			private static class _003C_003Eo__26
			{
				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__0;

				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__1;

				public static CallSite<Func<CallSite, object, bool>> _003C_003Ep__2;

				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__3;
			}

			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public FormEkipUser _003C_003E4__this;

			private List<HIS_EXECUTE_ROLE> _003Cdatas_003E5__1;

			private List<ColumnInfo> _003CcolumnInfos_003E5__2;

			private ControlEditorADO _003CcontrolEditorADO_003E5__3;

			private CommonParam _003CparamCommon_003E5__4;

			private object _003Cfilter_003E5__5;

			private Func<CallSite, object, List<HIS_EXECUTE_ROLE>> _003C_003Es__6;

			private CallSite<Func<CallSite, object, List<HIS_EXECUTE_ROLE>>> _003C_003Es__7;

			private object _003C_003Es__8;

			private Exception _003Cex_003E5__9;

			private object _003C_003Eu__1;

			private void MoveNext()
			{
				//IL_003f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0049: Expected O, but got Unknown
				//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
				//IL_03de: Expected O, but got Unknown
				//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
				//IL_03ff: Expected O, but got Unknown
				//IL_0417: Unknown result type (might be due to invalid IL or missing references)
				//IL_0421: Expected O, but got Unknown
				//IL_016c: Unknown result type (might be due to invalid IL or missing references)
				//IL_018c: Expected O, but got Unknown
				int num = _003C_003E1__state;
				try
				{
					if (num != 0)
					{
					}
					try
					{
						dynamic val;
						if (num != 0)
						{
							_003Cdatas_003E5__1 = null;
							if (BackendDataWorker.IsExistsKey<HIS_EXECUTE_ROLE>())
							{
								_003Cdatas_003E5__1 = BackendDataWorker.Get<HIS_EXECUTE_ROLE>();
								goto IL_035d;
							}
							_003CparamCommon_003E5__4 = new CommonParam();
							_003Cfilter_003E5__5 = new ExpandoObject();
							if (FormEkipUser._003C_003Eo__26._003C_003Ep__1 == null)
							{
								FormEkipUser._003C_003Eo__26._003C_003Ep__1 = CallSite<Func<CallSite, object, List<HIS_EXECUTE_ROLE>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(List<HIS_EXECUTE_ROLE>), typeof(FormEkipUser)));
							}
							_003C_003Es__6 = FormEkipUser._003C_003Eo__26._003C_003Ep__1.Target;
							_003C_003Es__7 = FormEkipUser._003C_003Eo__26._003C_003Ep__1;
							val = new BackendAdapter(_003CparamCommon_003E5__4).GetAsync<List<HIS_EXECUTE_ROLE>>("api/HisExecuteRole/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (dynamic)_003Cfilter_003E5__5, _003CparamCommon_003E5__4).GetAwaiter();
							if (!(bool)val.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = val;
								ICriticalNotifyCompletion awaiter = val as ICriticalNotifyCompletion;
								_003CComboExecuteRole_003Ed__26 stateMachine = this;
								if (awaiter == null)
								{
									INotifyCompletion awaiter2 = (INotifyCompletion)(object)val;
									_003C_003Et__builder.AwaitOnCompleted(ref awaiter2, ref stateMachine);
									awaiter2 = null;
								}
								else
								{
									_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
								}
								awaiter = null;
								return;
							}
						}
						else
						{
							val = _003C_003Eu__1;
							_003C_003Eu__1 = null;
							num = (_003C_003E1__state = -1);
						}
						_003C_003Es__8 = val.GetResult();
						_003Cdatas_003E5__1 = _003C_003Es__6(_003C_003Es__7, _003C_003Es__8);
						_003C_003Es__6 = null;
						_003C_003Es__7 = null;
						_003C_003Es__8 = null;
						if (_003Cdatas_003E5__1 != null)
						{
							BackendDataWorker.UpdateToRam(typeof(HIS_EXECUTE_ROLE), (object)_003Cdatas_003E5__1, long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
						}
						_003CparamCommon_003E5__4 = null;
						_003Cfilter_003E5__5 = null;
						goto IL_035d;
						IL_035d:
						if (_003Cdatas_003E5__1 != null && _003Cdatas_003E5__1.Count > 0)
						{
							_003Cdatas_003E5__1 = _003Cdatas_003E5__1.Where((HIS_EXECUTE_ROLE p) => p.IS_ACTIVE == 1 && p.IS_DISABLE_IN_EKIP != 1).ToList();
						}
						_003CcolumnInfos_003E5__2 = new List<ColumnInfo>();
						_003CcolumnInfos_003E5__2.Add(new ColumnInfo("EXECUTE_ROLE_CODE", "", 150, 1));
						_003CcolumnInfos_003E5__2.Add(new ColumnInfo("EXECUTE_ROLE_NAME", "", 250, 2));
						_003CcontrolEditorADO_003E5__3 = new ControlEditorADO("EXECUTE_ROLE_NAME", "ID", _003CcolumnInfos_003E5__2, false, 250);
						_003CcontrolEditorADO_003E5__3.ImmediatePopup = true;
						ControlEditorLoader.Load((object)_003C_003E4__this.cboPosition, (object)_003Cdatas_003E5__1, _003CcontrolEditorADO_003E5__3);
						_003Cdatas_003E5__1 = null;
						_003CcolumnInfos_003E5__2 = null;
						_003CcontrolEditorADO_003E5__3 = null;
					}
					catch (Exception ex)
					{
						_003Cex_003E5__9 = ex;
						LogSystem.Warn(_003Cex_003E5__9);
					}
				}
				catch (Exception ex)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(ex);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private Action<EkipUsersADO> lstEkipUser;

		private Action<long> ekipId;

		private ControlStateWorker controlStateWorker = new ControlStateWorker();

		private List<ControlStateRDO> currentControlStateRDO = new List<ControlStateRDO>();

		private string MODULELINK = "FormEkipUser";

		private Inventec.Desktop.Common.Modules.Module CurrentModuleData;

		private long? idPttt;

		private List<HIS_EKIP_USER> lstEkip = new List<HIS_EKIP_USER>();

		private PtttMethodADO currentADO;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private GridControl gridControl1;

		private GridView gridView1;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private GridColumn gridColumn3;

		private RepositoryItemButtonEdit Bbtn_Add;

		private RepositoryItemButtonEdit Bbtn_Minus;

		private RepositoryItemCustomGridLookUpEdit GridLU_User;

		private CustomGridView repositoryItemCustomGridLookUpEdit1View;

		private LayoutControlItem layoutControlItem1;

		private GridLookUpEdit cboEkipTemp;

		private GridView gridLookUpEdit1View;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private RepositoryItemLookUpEdit cboPosition;

		private SimpleButton btnSave;

		private LayoutControlItem layoutControlItem3;

		private EmptySpaceItem emptySpaceItem2;

		private GridColumn gridColumn4;

		internal List<HIS_EKIP_TEMP> ekipTemps { get; set; }

		internal List<HIS_EXECUTE_ROLE_USER> executeRoleUsers { get; set; }

		internal List<AcsUserADO> AcsUserADOList { get; set; }

		public FormEkipUser(Action<EkipUsersADO> lst, PtttMethodADO ado, Inventec.Desktop.Common.Modules.Module module)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			((Form)this).Icon = Icon.ExtractAssociatedIcon(Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
			lstEkipUser = lst;
			currentADO = ado;
			CurrentModuleData = module;
			InitializeComponent();
		}

		private void FormEkipUser_Load(object sender, EventArgs e)
		{
			try
			{
				WaitingManager.Show();
				SetCaptionByLanguageKey();
				ComboEkipTemp(cboEkipTemp);
				ComboExecuteRole();
				AcsUserADOList = ProcessAcsUser();
				ComboAcsUser();
				LoadExecuteRoleUser();
				LoadDefaultGrid();
				InitControlState();
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void InitControlState()
		{
			try
			{
				if (currentADO.EkipUsersADO != null && currentADO.EkipUsersADO.listEkipUser.Count > 0)
				{
					currentADO.EkipUsersADO.listEkipUser.First().Action = 1;
					gridControl1.DataSource = null;
					gridControl1.DataSource = currentADO.EkipUsersADO.listEkipUser;
				}
				cboEkipTemp.EditValue = ((currentADO.EkipUsersADO != null && currentADO.EkipUsersADO.idEkip.HasValue && currentADO.EkipUsersADO.idEkip > 0) ? currentADO.EkipUsersADO.idEkip : new long?(0L));
				gridControl1.RefreshDataSource();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private List<AcsUserADO> ProcessAcsUser()
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Expected O, but got Unknown
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Expected O, but got Unknown
			List<AcsUserADO> list = null;
			try
			{
				List<ACS_USER> list2 = null;
				List<V_HIS_EMPLOYEE> list3 = null;
				CommonParam val = new CommonParam();
				dynamic val2 = new ExpandoObject();
				list2 = BackendDataWorker.Get<ACS_USER>();
				if (list2 != null)
				{
					BackendDataWorker.UpdateToRam(typeof(ACS_USER), (object)list2, long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
				}
				list3 = new BackendAdapter(new CommonParam()).Get<List<V_HIS_EMPLOYEE>>("api/HisEmployee/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, val2, val);
				if (list3 != null)
				{
					BackendDataWorker.UpdateToRam(typeof(V_HIS_EMPLOYEE), (object)list3, long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
				}
				List<HIS_DEPARTMENT> source = (from o in BackendDataWorker.Get<HIS_DEPARTMENT>()
					where o.IS_ACTIVE == 1 && o.IS_CLINICAL == 1
					select o).ToList();
				list = new List<AcsUserADO>();
				foreach (ACS_USER item in list2)
				{
					AcsUserADO acsUserADO = new AcsUserADO();
					((ACS_USER)acsUserADO).ID = item.ID;
					((ACS_USER)acsUserADO).LOGINNAME = item.LOGINNAME;
					((ACS_USER)acsUserADO).USERNAME = item.USERNAME;
					((ACS_USER)acsUserADO).MOBILE = item.MOBILE;
					((ACS_USER)acsUserADO).PASSWORD = item.PASSWORD;
					((ACS_USER)acsUserADO).IS_ACTIVE = item.IS_ACTIVE;
					V_HIS_EMPLOYEE check = list3.FirstOrDefault((V_HIS_EMPLOYEE o) => o.LOGINNAME == item.LOGINNAME);
					if (check != null)
					{
						acsUserADO.DOB = Inventec.Common.DateTime.Convert.TimeNumberToDateString(check.DOB.GetValueOrDefault());
						acsUserADO.DIPLOMA = check.DIPLOMA;
						HIS_DEPARTMENT val3 = source.FirstOrDefault((HIS_DEPARTMENT o) => o.ID == check.DEPARTMENT_ID);
						if (val3 != null)
						{
							acsUserADO.DEPARTMENT_NAME = val3.DEPARTMENT_NAME;
						}
					}
					list.Add(acsUserADO);
				}
				list = list.OrderBy((AcsUserADO o) => ((ACS_USER)o).USERNAME).ToList();
			}
			catch (Exception ex)
			{
				list = null;
				LogSystem.Warn(ex);
			}
			return list;
		}

		public async Task ComboEkipTemp(GridLookUpEdit cbo)
		{
			try
			{
				long DepartmentID = WorkPlace.WorkPlaceSDO.FirstOrDefault((WorkPlaceSDO o) => o.RoomId == CurrentModuleData.RoomId).DepartmentId;
				string logginName = ClientTokenManagerStore.ClientTokenManager.GetLoginName();
				CommonParam param = new CommonParam();
				HisEkipTempFilter filter = new HisEkipTempFilter();
				ekipTemps = await ((AdapterBase)new BackendAdapter(param)).GetAsync<List<HIS_EKIP_TEMP>>("/api/HisEkipTemp/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)filter, param);
				if (ekipTemps != null && ekipTemps.Count > 0)
				{
					ekipTemps = (from o in ekipTemps
						where (o.IS_PUBLIC == 1 || o.CREATOR == logginName || (o.IS_PUBLIC_IN_DEPARTMENT == 1 && o.DEPARTMENT_ID == DepartmentID)) && o.IS_ACTIVE == 1
						orderby o.CREATE_TIME descending
						select o).ToList();
				}
				ControlEditorADO controlEditorADO = new ControlEditorADO("EKIP_TEMP_NAME", "ID", new List<ColumnInfo>
				{
					new ColumnInfo("EKIP_TEMP_NAME", "", 250, 1)
				}, false, 250);
				ControlEditorLoader.Load((object)cbo, (object)ekipTemps, controlEditorADO);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Warn(ex2);
			}
		}

		private async Task ComboExecuteRole()
		{
            try
            {
                List<HIS_EXECUTE_ROLE> datas = null;
                if (BackendDataWorker.IsExistsKey<HIS_EXECUTE_ROLE>())
                {
                    datas = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<HIS_EXECUTE_ROLE>();
                }
                else
                {
                    CommonParam paramCommon = new CommonParam();
                    dynamic filter = new System.Dynamic.ExpandoObject();
                    datas = await new Inventec.Common.Adapter.BackendAdapter(paramCommon).GetAsync<List<HIS_EXECUTE_ROLE>>("api/HisExecuteRole/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, filter, paramCommon);
                    if (datas != null) BackendDataWorker.UpdateToRam(typeof(HIS_EXECUTE_ROLE), datas, long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
                }

                if (datas != null && datas.Count > 0)
                {
                    datas = datas.Where(p => p.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && p.IS_DISABLE_IN_EKIP != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).ToList();
                }

                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("EXECUTE_ROLE_CODE", "", 150, 1));
                columnInfos.Add(new ColumnInfo("EXECUTE_ROLE_NAME", "", 250, 2));
                ControlEditorADO controlEditorADO = new ControlEditorADO("EXECUTE_ROLE_NAME", "ID", columnInfos, false, 250);
                controlEditorADO.ImmediatePopup = true;
                ControlEditorLoader.Load(cboPosition, datas, controlEditorADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
		}

		private async Task ComboAcsUser()
		{
            try
            {
                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("LOGINNAME", "", 150, 1));
                columnInfos.Add(new ColumnInfo("USERNAME", "", 250, 2));
                columnInfos.Add(new ColumnInfo("DOB", "", 100, 3));
                columnInfos.Add(new ColumnInfo("DIPLOMA", "", 100, 4));
                columnInfos.Add(new ColumnInfo("DEPARTMENT_NAME", "", 200, 5));

                ControlEditorADO controlEditorADO = new ControlEditorADO("USERNAME", "LOGINNAME", columnInfos, true, 800);
                ControlEditorLoader.Load(GridLU_User, this.AcsUserADOList, controlEditorADO);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
		}

		private void LoadExecuteRoleUser()
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			try
			{
				LogSystem.Error("---S");
				HisExecuteRoleUserFilter val = new HisExecuteRoleUserFilter();
				executeRoleUsers = ((AdapterBase)new BackendAdapter(new CommonParam())).Get<List<HIS_EXECUTE_ROLE_USER>>("api/HisExecuteRoleUser/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val, new CommonParam());
				LogSystem.Error("---F");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			try
			{
				if (e.Column.FieldName == "BtnDelete")
				{
					int num = System.Convert.ToInt32(e.RowHandle);
					int num2 = Parse.ToInt32((gridView1.GetRowCellValue(e.RowHandle, "Action") ?? "").ToString());
					if (num2 == 1)
					{
						e.RepositoryItem = Bbtn_Add;
					}
					else
					{
						e.RepositoryItem = Bbtn_Minus;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void gridView1_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
		{
			try
			{
				if (e.FocusedColumn.FieldName == "LOGINNAME")
				{
					gridView1.ShowEditor();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void gridView1_ShownEditor(object sender, EventArgs e)
		{
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                HisEkipUserADO data = view.GetFocusedRow() as HisEkipUserADO;
                if (view.FocusedColumn.FieldName == "LOGINNAME" && view.ActiveEditor is GridLookUpEdit)
                {
                    GridLookUpEdit editor = view.ActiveEditor as GridLookUpEdit;
                    List<string> loginNames = new List<string>();
                    if (data != null && data.EXECUTE_ROLE_ID > 0)
                    {
                        if (data.LOGINNAME != null)
                            editor.EditValue = data.LOGINNAME;
                        var executeRoleUserTemps = executeRoleUsers != null ? executeRoleUsers.Where(o => o.EXECUTE_ROLE_ID == data.EXECUTE_ROLE_ID).ToList() : null;
                        if (executeRoleUserTemps != null && executeRoleUserTemps.Count > 0)
                        {
                            loginNames = executeRoleUserTemps.Select(o => o.LOGINNAME).Distinct().ToList();
                        }
                    }

                    ComboAcsUser(editor, loginNames);
                    gridView1.RefreshData();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
		}

		private void ComboAcsUser(GridLookUpEdit cbo, List<string> loginNames)
		{
			try
			{
				List<AcsUserADO> list = new List<AcsUserADO>();
				list = ((loginNames == null || loginNames.Count <= 0) ? AcsUserADOList.Where((AcsUserADO o) => ((ACS_USER)o).IS_ACTIVE == 1).ToList() : AcsUserADOList.Where((AcsUserADO o) => loginNames.Contains(((ACS_USER)o).LOGINNAME) && ((ACS_USER)o).IS_ACTIVE == 1).ToList());
				cbo.Properties.DataSource = list;
				cbo.Properties.DisplayMember = "USERNAME";
				cbo.Properties.ValueMember = "LOGINNAME";
				cbo.Properties.TextEditStyle = TextEditStyles.Standard;
				cbo.Properties.PopupFilterMode = PopupFilterMode.Contains;
				cbo.Properties.ImmediatePopup = true;
				cbo.ForceInitialize();
				cbo.Properties.View.Columns.Clear();
				GridColumn gridColumn = cbo.Properties.View.Columns.AddField("LOGINNAME");
				gridColumn.Caption = "Tài khoản";
				gridColumn.Visible = true;
				gridColumn.VisibleIndex = 1;
				gridColumn.Width = 100;
				GridColumn gridColumn2 = cbo.Properties.View.Columns.AddField("USERNAME");
				gridColumn2.Caption = "Họ tên";
				gridColumn2.Visible = true;
				gridColumn2.VisibleIndex = 2;
				gridColumn2.Width = 200;
				GridColumn gridColumn3 = cbo.Properties.View.Columns.AddField("DIPLOMA");
				gridColumn3.Caption = "CCHN";
				gridColumn3.Visible = true;
				gridColumn3.VisibleIndex = 3;
				gridColumn3.Width = 100;
				GridColumn gridColumn4 = cbo.Properties.View.Columns.AddField("DEPARTMENT_NAME");
				gridColumn4.Caption = "Tên khoa";
				gridColumn4.Visible = true;
				gridColumn4.VisibleIndex = 4;
				gridColumn4.Width = 200;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void Bbtn_Add_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			try
			{
				List<HisEkipUserADO> list = new List<HisEkipUserADO>();
				List<HisEkipUserADO> list2 = gridControl1.DataSource as List<HisEkipUserADO>;
				if (list2 == null || list2.Count < 1)
				{
					HisEkipUserADO item = new HisEkipUserADO();
					list.Add(item);
					list.ForEach(delegate(HisEkipUserADO o)
					{
						o.Action = 2;
					});
					list.LastOrDefault().Action = 1;
					gridControl1.DataSource = null;
					gridControl1.DataSource = list;
				}
				else
				{
					HisEkipUserADO item2 = new HisEkipUserADO();
					list2.Add(item2);
					list2.ForEach(delegate(HisEkipUserADO o)
					{
						o.Action = 2;
					});
					list2.LastOrDefault().Action = 1;
					gridControl1.DataSource = null;
					gridControl1.DataSource = list2;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void Bbtn_Minus_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			try
			{
				CommonParam val = new CommonParam();
				List<HisEkipUserADO> list = gridControl1.DataSource as List<HisEkipUserADO>;
				HisEkipUserADO val2 = (HisEkipUserADO)gridView1.GetFocusedRow();
				if (val2 != null && list.Count > 0)
				{
					list.Remove(val2);
					list.ForEach(delegate(HisEkipUserADO o)
					{
						o.Action = 2;
					});
					list.LastOrDefault().Action = 1;
					gridControl1.DataSource = null;
					gridControl1.DataSource = list;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadDefaultGrid()
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			try
			{
				List<HisEkipUserADO> list = new List<HisEkipUserADO>();
				HisEkipUserADO val = new HisEkipUserADO();
				val.Action = 1;
				list.Add(val);
				gridControl1.DataSource = null;
				gridControl1.DataSource = list;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			((FormBase)this).LogTheadInSessionInfo((Action)saveClick, "btnSave_Click");
		}

		private void saveClick()
		{
			try
			{
				WaitingManager.Show();
				List<HisEkipUserADO> list = gridControl1.DataSource as List<HisEkipUserADO>;
				if (list != null && list.Count > 0)
				{
					if (!ProcessEkipUser(list))
					{
						WaitingManager.Hide();
						XtraMessageBox.Show(ResourceMessage.DuLieuEkipTrung, ResourceMessage.ThongBao, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else if (CheckEkip())
					{
						WaitingManager.Show();
						EkipUsersADO ekipUsersADO = new EkipUsersADO();
						ekipUsersADO.idPtttMethod = currentADO.ID;
						ekipUsersADO.idEkip = ((cboEkipTemp.EditValue != null) ? int.Parse(cboEkipTemp.EditValue.ToString()) : 0);
						ekipUsersADO.listEkipUser = list.Where((HisEkipUserADO o) => !string.IsNullOrEmpty(((V_HIS_EKIP_USER)o).LOGINNAME) && ((V_HIS_EKIP_USER)o).EXECUTE_ROLE_ID > 0).ToList();
						lstEkipUser(ekipUsersADO);
						((Form)this).Close();
					}
				}
				else
				{
					((Form)this).Close();
				}
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private bool ProcessEkipUser(List<HisEkipUserADO> sereServPTTTADOs)
		{
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			bool result = true;
			try
			{
				List<HIS_EKIP_USER> list = new List<HIS_EKIP_USER>();
				if (sereServPTTTADOs != null && sereServPTTTADOs.Count > 0)
				{
					foreach (HisEkipUserADO sereServPTTTADO in sereServPTTTADOs)
					{
						HIS_EKIP_USER ekipUser = new HIS_EKIP_USER();
						DataObjectMapper.Map<HIS_EKIP_USER>((object)ekipUser, (object)sereServPTTTADO);
						ACS_USER val = BackendDataWorker.Get<ACS_USER>().SingleOrDefault((ACS_USER o) => o.LOGINNAME == ekipUser.LOGINNAME);
						if (val != null)
						{
							ekipUser.USERNAME = val.USERNAME;
						}
						list.Add(ekipUser);
					}
				}
				var enumerable = from x in list
					group x by new { x.LOGINNAME, x.EXECUTE_ROLE_ID };
				foreach (var item in enumerable)
				{
					if (item.Count() >= 2)
					{
						return false;
					}
				}
				lstEkip = list;
			}
			catch (Exception ex)
			{
				result = true;
				LogSystem.Warn(ex);
			}
			return result;
		}

		private bool CheckEkip()
		{
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Expected O, but got Unknown
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Expected O, but got Unknown
			bool result = true;
			try
			{
				WaitingManager.Hide();
				List<HIS_EKIP_USER> hasInvalid = lstEkip.Where((HIS_EKIP_USER o) => string.IsNullOrEmpty(o.LOGINNAME) || o.EXECUTE_ROLE_ID <= 0).Distinct().ToList();
				if (hasInvalid != null && hasInvalid.Count > 0)
				{
					List<HIS_EXECUTE_ROLE> list = null;
					if (BackendDataWorker.IsExistsKey<HIS_EXECUTE_ROLE>())
					{
						list = BackendDataWorker.Get<HIS_EXECUTE_ROLE>();
					}
					else
					{
						CommonParam val = new CommonParam();
						dynamic val2 = new ExpandoObject();
						list = new BackendAdapter(val).GetAsync<List<HIS_EXECUTE_ROLE>>("api/HisExecuteRole/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, val2, val);
						if (list != null)
						{
							BackendDataWorker.UpdateToRam(typeof(HIS_EXECUTE_ROLE), (object)list, long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
						}
					}
					List<HIS_EXECUTE_ROLE> list2 = list.Where((HIS_EXECUTE_ROLE o) => hasInvalid.Select((HIS_EKIP_USER p) => p.EXECUTE_ROLE_ID).Contains(o.ID)).ToList();
					if (list2 == null)
					{
						result = true;
					}
					string text = string.Format(ResourceMessage.BanChuaNhapThongTinTuongUngVoiCacVaiTRo, string.Join(",", list2.Select((HIS_EXECUTE_ROLE o) => o.EXECUTE_ROLE_NAME).ToArray()));
					if (MessageBox.Show(text, ResourceMessage.ThongBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						return false;
					}
					result = true;
				}
				List<string> list3 = new List<string>();
				List<IGrouping<string, HIS_EKIP_USER>> list4 = (from o in lstEkip
					where !string.IsNullOrWhiteSpace(o.LOGINNAME)
					group o by o.LOGINNAME).ToList();
				foreach (IGrouping<string, HIS_EKIP_USER> item in list4)
				{
					if (item.Count() <= 1)
					{
						continue;
					}
					List<HIS_EXECUTE_ROLE> source = (from o in BackendDataWorker.Get<HIS_EXECUTE_ROLE>()
						where item.Select((HIS_EKIP_USER s) => s.EXECUTE_ROLE_ID).Contains(o.ID)
						select o).ToList();
					list3.Add(string.Format(ResourceMessage.TaiKhoanDuocThietLapVoiCacVaiTro, item.Key, string.Join(",", source.Select((HIS_EXECUTE_ROLE s) => s.EXECUTE_ROLE_NAME))));
				}
				if (list3.Count > 0)
				{
					XtraMessageBox.Show(string.Join("\n", list3), ResourceMessage.ThongBao);
					return false;
				}
			}
			catch (Exception ex)
			{
				result = false;
				LogSystem.Warn(ex);
			}
			return result;
		}

		private void cboEkipTemp_Closed(object sender, ClosedEventArgs e)
		{
			try
			{
				if (e.CloseMode == PopupCloseMode.Normal && cboEkipTemp.EditValue != null)
				{
					HIS_EKIP_TEMP val = ekipTemps.FirstOrDefault((HIS_EKIP_TEMP o) => o.ID == Parse.ToInt64((cboEkipTemp.EditValue ?? ((object)0)).ToString()));
					if (val != null)
					{
						LoadGridEkipUserFromTemp(val.ID);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal void LoadGridEkipUserFromTemp(long ekipTempId)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Expected O, but got Unknown
			//IL_0168: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Expected O, but got Unknown
			try
			{
				CommonParam val = new CommonParam();
				HisEkipTempUserFilter val2 = new HisEkipTempUserFilter();
				val2.EKIP_TEMP_ID = ekipTempId;
				List<HIS_EKIP_TEMP_USER> list = ((AdapterBase)new BackendAdapter(val)).Get<List<HIS_EKIP_TEMP_USER>>("api/HisEkipTempUser/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val2, val);
				if (list == null || list.Count <= 0)
				{
					return;
				}
				List<HisEkipUserADO> list2 = new List<HisEkipUserADO>();
				List<string> loginNames = list.Select((HIS_EKIP_TEMP_USER o) => o.LOGINNAME).ToList();
				AcsUserFilter val3 = new AcsUserFilter();
				val3.LOGINNAMEs = loginNames;
				List<ACS_USER> source = (from o in BackendDataWorker.Get<ACS_USER>()
					where loginNames.Exists((string p) => p == o.LOGINNAME)
					select o).ToList();
				List<string> list3 = (from i in source
					where i.IS_ACTIVE == 1
					select i.LOGINNAME).ToList();
				foreach (HIS_EKIP_TEMP_USER ekipTempUser in list)
				{
					HIS_EXECUTE_ROLE val4 = BackendDataWorker.Get<HIS_EXECUTE_ROLE>().FirstOrDefault((HIS_EXECUTE_ROLE p) => p.ID == ekipTempUser.EXECUTE_ROLE_ID && p.IS_ACTIVE == 1);
					if (val4 != null && val4.ID != 0)
					{
						HisEkipUserADO val5 = new HisEkipUserADO();
						((V_HIS_EKIP_USER)val5).EXECUTE_ROLE_ID = ekipTempUser.EXECUTE_ROLE_ID;
						((V_HIS_EKIP_USER)val5).LOGINNAME = ekipTempUser.LOGINNAME;
						((V_HIS_EKIP_USER)val5).USERNAME = ekipTempUser.USERNAME;
						((V_HIS_EKIP_USER)val5).DEPARTMENT_ID = ekipTempUser.DEPARTMENT_ID;
						if (list2.Count == 0)
						{
							val5.Action = 1;
						}
						else
						{
							val5.Action = 2;
						}
						if (list3.Contains(ekipTempUser.LOGINNAME))
						{
							list2.Add(val5);
						}
					}
				}
				gridControl1.DataSource = list2;
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
				if (!e.IsGetData)
				{
					return;
				}
				GridView gridView = sender as GridView;
				if (!(e.Column.FieldName == "USERNAME"))
				{
					return;
				}
				try
				{
					string status = (gridView.GetRowCellValue(e.ListSourceRowIndex, "LOGINNAME") ?? "").ToString();
					ACS_USER val = BackendDataWorker.Get<ACS_USER>().SingleOrDefault((ACS_USER o) => o.LOGINNAME == status && o.IS_ACTIVE == 1);
					e.Value = val.USERNAME;
				}
				catch (Exception ex)
				{
					LogSystem.Warn("Loi hien thi gia tri cot USERNAME", ex);
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private void FormEkipUser_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private void SetCaptionByLanguageKey()
		{
			try
			{
				ResourceLanguageManager.LanguageResource__FormEkipUser = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(FormEkipUser).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("FormEkipUser.layoutControl1.Text", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				btnSave.Text = Inventec.Common.Resource.Get.Value("FormEkipUser.btnSave.Text", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				cboEkipTemp.Properties.NullText = Inventec.Common.Resource.Get.Value("FormEkipUser.cboEkipTemp.Properties.NullText", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				gridColumn1.Caption = Inventec.Common.Resource.Get.Value("FormEkipUser.gridColumn1.Caption", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				((RepositoryItem)(object)GridLU_User).NullText = Inventec.Common.Resource.Get.Value("FormEkipUser.GridLU_User.NullText", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				gridColumn2.Caption = Inventec.Common.Resource.Get.Value("FormEkipUser.gridColumn2.Caption", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				gridColumn3.Caption = Inventec.Common.Resource.Get.Value("FormEkipUser.gridColumn3.Caption", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				gridColumn4.Caption = Inventec.Common.Resource.Get.Value("FormEkipUser.gridColumn4.Caption", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				layoutControlItem2.Text = Inventec.Common.Resource.Get.Value("FormEkipUser.layoutControlItem2.Text", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
				((Control)(object)this).Text = Inventec.Common.Resource.Get.Value("FormEkipUser.Text", ResourceLanguageManager.LanguageResource__FormEkipUser, LanguageManager.GetCulture());
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
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			layoutControl1 = new LayoutControl();
			btnSave = new SimpleButton();
			cboEkipTemp = new GridLookUpEdit();
			gridLookUpEdit1View = new GridView();
			gridControl1 = new GridControl();
			gridView1 = new GridView();
			gridColumn1 = new GridColumn();
			GridLU_User = new RepositoryItemCustomGridLookUpEdit();
			repositoryItemCustomGridLookUpEdit1View = new CustomGridView();
			gridColumn2 = new GridColumn();
			cboPosition = new RepositoryItemLookUpEdit();
			gridColumn3 = new GridColumn();
			Bbtn_Add = new RepositoryItemButtonEdit();
			gridColumn4 = new GridColumn();
			Bbtn_Minus = new RepositoryItemButtonEdit();
			layoutControlGroup1 = new LayoutControlGroup();
			layoutControlItem1 = new LayoutControlItem();
			layoutControlItem2 = new LayoutControlItem();
			emptySpaceItem1 = new EmptySpaceItem();
			layoutControlItem3 = new LayoutControlItem();
			emptySpaceItem2 = new EmptySpaceItem();
			((ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((ISupportInitialize)cboEkipTemp.Properties).BeginInit();
			((ISupportInitialize)gridLookUpEdit1View).BeginInit();
			((ISupportInitialize)gridControl1).BeginInit();
			((ISupportInitialize)gridView1).BeginInit();
			((ISupportInitialize)GridLU_User).BeginInit();
			((ISupportInitialize)repositoryItemCustomGridLookUpEdit1View).BeginInit();
			((ISupportInitialize)cboPosition).BeginInit();
			((ISupportInitialize)Bbtn_Add).BeginInit();
			((ISupportInitialize)Bbtn_Minus).BeginInit();
			((ISupportInitialize)layoutControlGroup1).BeginInit();
			((ISupportInitialize)layoutControlItem1).BeginInit();
			((ISupportInitialize)layoutControlItem2).BeginInit();
			((ISupportInitialize)emptySpaceItem1).BeginInit();
			((ISupportInitialize)layoutControlItem3).BeginInit();
			((ISupportInitialize)emptySpaceItem2).BeginInit();
			((Control)this).SuspendLayout();
			layoutControl1.Controls.Add(btnSave);
			layoutControl1.Controls.Add(cboEkipTemp);
			layoutControl1.Controls.Add(gridControl1);
			layoutControl1.Dock = DockStyle.Fill;
			layoutControl1.Location = new Point(0, 0);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new Size(654, 196);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			btnSave.Location = new Point(530, 172);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(122, 22);
			btnSave.StyleController = layoutControl1;
			btnSave.TabIndex = 6;
			btnSave.Text = "Lưu";
			btnSave.Click += btnSave_Click;
			cboEkipTemp.Location = new Point(57, 2);
			cboEkipTemp.Name = "cboEkipTemp";
			cboEkipTemp.Properties.AllowNullInput = DefaultBoolean.True;
			cboEkipTemp.Properties.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			cboEkipTemp.Properties.NullText = "";
			cboEkipTemp.Properties.View = gridLookUpEdit1View;
			cboEkipTemp.Size = new Size(275, 20);
			cboEkipTemp.StyleController = layoutControl1;
			cboEkipTemp.TabIndex = 5;
			cboEkipTemp.Closed += cboEkipTemp_Closed;
			gridLookUpEdit1View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
			gridLookUpEdit1View.Name = "gridLookUpEdit1View";
			gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			gridControl1.Location = new Point(2, 26);
			gridControl1.MainView = gridView1;
			gridControl1.Name = "gridControl1";
			gridControl1.RepositoryItems.AddRange(new RepositoryItem[4]
			{
				Bbtn_Add,
				Bbtn_Minus,
				(RepositoryItem)(object)GridLU_User,
				cboPosition
			});
			gridControl1.Size = new Size(650, 142);
			gridControl1.TabIndex = 4;
			gridControl1.ViewCollection.AddRange(new BaseView[1] { gridView1 });
			gridView1.Columns.AddRange(new GridColumn[4] { gridColumn1, gridColumn2, gridColumn3, gridColumn4 });
			gridView1.GridControl = gridControl1;
			gridView1.Name = "gridView1";
			gridView1.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
			gridView1.OptionsView.ShowGroupPanel = false;
			gridView1.OptionsView.ShowIndicator = false;
			gridView1.CustomRowCellEdit += gridView1_CustomRowCellEdit;
			gridView1.ShownEditor += gridView1_ShownEditor;
			gridView1.FocusedColumnChanged += gridView1_FocusedColumnChanged;
			gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
			gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
			gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumn1.Caption = "Họ và tên";
			gridColumn1.ColumnEdit = (RepositoryItem)(object)GridLU_User;
			gridColumn1.FieldName = "LOGINNAME";
			gridColumn1.Name = "gridColumn1";
			gridColumn1.UnboundType = UnboundColumnType.Object;
			gridColumn1.Visible = true;
			gridColumn1.VisibleIndex = 1;
			gridColumn1.Width = 266;
			((RepositoryItemGridLookUpEdit)(object)GridLU_User).AutoComplete = false;
			((RepositoryItem)(object)GridLU_User).AutoHeight = false;
			((RepositoryItemButtonEdit)(object)GridLU_User).Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			((RepositoryItem)(object)GridLU_User).Name = "GridLU_User";
			((RepositoryItem)(object)GridLU_User).NullText = "";
			((RepositoryItemButtonEdit)(object)GridLU_User).TextEditStyle = TextEditStyles.Standard;
			((RepositoryItemGridLookUpEditBase)(object)GridLU_User).View = (GridView)(object)repositoryItemCustomGridLookUpEdit1View;
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).FocusRectStyle = DrawFocusRectStyle.RowFocus;
			((BaseView)(object)repositoryItemCustomGridLookUpEdit1View).Name = "repositoryItemCustomGridLookUpEdit1View";
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).OptionsSelection.EnableAppearanceFocusedCell = false;
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).OptionsView.ShowGroupPanel = false;
			gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
			gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumn2.Caption = "Vai trò";
			gridColumn2.ColumnEdit = cboPosition;
			gridColumn2.FieldName = "EXECUTE_ROLE_ID";
			gridColumn2.Name = "gridColumn2";
			gridColumn2.Visible = true;
			gridColumn2.VisibleIndex = 0;
			gridColumn2.Width = 331;
			cboPosition.AutoHeight = false;
			cboPosition.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			cboPosition.Name = "cboPosition";
			cboPosition.TextEditStyle = TextEditStyles.Standard;
			gridColumn3.Caption = "gridColumn3";
			gridColumn3.ColumnEdit = Bbtn_Add;
			gridColumn3.FieldName = "BtnDelete";
			gridColumn3.Name = "gridColumn3";
			gridColumn3.OptionsColumn.ShowCaption = false;
			gridColumn3.Visible = true;
			gridColumn3.VisibleIndex = 2;
			gridColumn3.Width = 51;
			Bbtn_Add.AutoHeight = false;
			Bbtn_Add.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Plus)
			});
			Bbtn_Add.Name = "Bbtn_Add";
			Bbtn_Add.TextEditStyle = TextEditStyles.HideTextEditor;
			Bbtn_Add.ButtonClick += Bbtn_Add_ButtonClick;
			gridColumn4.Caption = "gridColumn4";
			gridColumn4.FieldName = "LOGINNAME";
			gridColumn4.Name = "gridColumn4";
			Bbtn_Minus.AutoHeight = false;
			Bbtn_Minus.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Minus)
			});
			Bbtn_Minus.Name = "Bbtn_Minus";
			Bbtn_Minus.TextEditStyle = TextEditStyles.HideTextEditor;
			Bbtn_Minus.ButtonClick += Bbtn_Minus_ButtonClick;
			layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.False;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Items.AddRange(new BaseLayoutItem[5] { layoutControlItem1, layoutControlItem2, emptySpaceItem1, layoutControlItem3, emptySpaceItem2 });
			layoutControlGroup1.Location = new Point(0, 0);
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Size = new Size(654, 196);
			layoutControlGroup1.TextVisible = false;
			layoutControlItem1.Control = gridControl1;
			layoutControlItem1.Location = new Point(0, 24);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new Size(654, 146);
			layoutControlItem1.TextSize = new Size(0, 0);
			layoutControlItem1.TextVisible = false;
			layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
			layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
			layoutControlItem2.Control = cboEkipTemp;
			layoutControlItem2.Location = new Point(0, 0);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Size = new Size(334, 24);
			layoutControlItem2.Text = "Kíp mẫu:";
			layoutControlItem2.TextAlignMode = TextAlignModeItem.CustomSize;
			layoutControlItem2.TextSize = new Size(50, 20);
			layoutControlItem2.TextToControlDistance = 5;
			emptySpaceItem1.AllowHotTrack = false;
			emptySpaceItem1.Location = new Point(334, 0);
			emptySpaceItem1.Name = "emptySpaceItem1";
			emptySpaceItem1.Size = new Size(320, 24);
			emptySpaceItem1.TextSize = new Size(0, 0);
			layoutControlItem3.Control = btnSave;
			layoutControlItem3.Location = new Point(528, 170);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new Size(126, 26);
			layoutControlItem3.TextSize = new Size(0, 0);
			layoutControlItem3.TextVisible = false;
			emptySpaceItem2.AllowHotTrack = false;
			emptySpaceItem2.Location = new Point(0, 170);
			emptySpaceItem2.Name = "emptySpaceItem2";
			emptySpaceItem2.Size = new Size(528, 26);
			emptySpaceItem2.TextSize = new Size(0, 0);
			((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			((Form)this).ClientSize = new Size(654, 196);
			((Control)this).Controls.Add(layoutControl1);
			((Control)this).Name = "FormEkipUser";
			((Form)this).StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)this).Text = "Kíp thực hiện";
			((Form)this).FormClosed += FormEkipUser_FormClosed;
			((Form)this).Load += FormEkipUser_Load;
			((Control)this).Controls.SetChildIndex(layoutControl1, 0);
			((ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			((ISupportInitialize)cboEkipTemp.Properties).EndInit();
			((ISupportInitialize)gridLookUpEdit1View).EndInit();
			((ISupportInitialize)gridControl1).EndInit();
			((ISupportInitialize)gridView1).EndInit();
			((ISupportInitialize)GridLU_User).EndInit();
			((ISupportInitialize)repositoryItemCustomGridLookUpEdit1View).EndInit();
			((ISupportInitialize)cboPosition).EndInit();
			((ISupportInitialize)Bbtn_Add).EndInit();
			((ISupportInitialize)Bbtn_Minus).EndInit();
			((ISupportInitialize)layoutControlGroup1).EndInit();
			((ISupportInitialize)layoutControlItem1).EndInit();
			((ISupportInitialize)layoutControlItem2).EndInit();
			((ISupportInitialize)emptySpaceItem1).EndInit();
			((ISupportInitialize)layoutControlItem3).EndInit();
			((ISupportInitialize)emptySpaceItem2).EndInit();
			((Control)this).ResumeLayout(false);
			((Control)this).PerformLayout();
		}
	}
}
