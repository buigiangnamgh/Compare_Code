using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACS.EFMODEL.DataModels;
using AutoMapper;
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
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Base;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Config;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using HIS.Desktop.Utilities.Extensions;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Common.TypeConvert;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.CustomControl.CustomGrid;
using Microsoft.CSharp.RuntimeBinder;
using MOS.EFMODEL.DataModels;
using MOS.Filter;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute
{
	public class UCEkipUser : UserControlBase
	{
		[CompilerGenerated]
		private static class _003C_003Eo__24
		{
            public static CallSite<Func<CallSite, BackendAdapter, string, HIS.Desktop.ApiConsumer.ApiConsumers, object, CommonParam, object>> _003C_003Ep__0;

			public static CallSite<Func<CallSite, object, List<HIS_EXECUTE_ROLE>>> _003C_003Ep__1;
		}

		[CompilerGenerated]
		private sealed class _003CComboExecuteRole_003Ed__24 : IAsyncStateMachine
		{
			private static class _003C_003Eo__24
			{
				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__0;

				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__1;

				public static CallSite<Func<CallSite, object, bool>> _003C_003Ep__2;

				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__3;
			}

			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public UCEkipUser _003C_003E4__this;

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
							if (UCEkipUser._003C_003Eo__24._003C_003Ep__1 == null)
							{
								UCEkipUser._003C_003Eo__24._003C_003Ep__1 = CallSite<Func<CallSite, object, List<HIS_EXECUTE_ROLE>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(List<HIS_EXECUTE_ROLE>), typeof(UCEkipUser)));
							}
							_003C_003Es__6 = UCEkipUser._003C_003Eo__24._003C_003Ep__1.Target;
							_003C_003Es__7 = UCEkipUser._003C_003Eo__24._003C_003Ep__1;
							val = new BackendAdapter(_003CparamCommon_003E5__4).GetAsync<List<HIS_EXECUTE_ROLE>>("api/HisExecuteRole/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (dynamic)_003Cfilter_003E5__5, _003CparamCommon_003E5__4).GetAwaiter();
							if (!(bool)val.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = val;
								ICriticalNotifyCompletion awaiter = val as ICriticalNotifyCompletion;
								_003CComboExecuteRole_003Ed__24 stateMachine = this;
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

		private long? DepartmentId;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private GridControl grdControlInformationSurg;

		private GridView grdViewInformationSurg;

		private LayoutControlItem lciInformationSurg;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private GridColumn gridColumn3;

		private GridColumn gridColumn4;

		private GridColumn gridColumn5;

		private RepositoryItemButtonEdit btnAdd;

		private RepositoryItemTextEdit txtLogin;

		private RepositoryItemButtonEdit btnDelete;

		private RepositoryItemGridLookUpEdit repositoryItemGridLookUpEditUsername;

		private GridView repositoryItemGridLookUpEdit1View;

		private RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit1;

		private GridView repositoryItemSearchLookUpEdit1View;

		private HIS.Desktop.Utilities.Extensions.RepositoryItemCustomGridLookUpEdit GridLookUpEdit_Department;

		private HIS.Desktop.Utilities.Extensions.CustomGridView repositoryItemCustomGridLookUpEdit1View;

		private HIS.Desktop.Utilities.Extensions.RepositoryItemCustomGridLookUpEdit GridLookupEdit_UserName;

		private HIS.Desktop.Utilities.Extensions.CustomGridView repositoryItemCustomGridLookUpEdit2View;

		private RepositoryItemLookUpEdit cboPosition;

		private GridColumn gridColumn6;

		private List<AcsUserADO> AcsUserADOList { get; set; }

		private List<HIS_DEPARTMENT> departmentClinic { get; set; }

		private List<HIS_EXECUTE_ROLE_USER> executeRoleUsers { get; set; }

		public UCEkipUser()
		{
			InitializeComponent();
			SetCaptionByLanguageKey();
			AcsUserADOList = ProcessAcsUser();
			LoadDataToComboDepartment();
			ComboAcsUser();
			ComboExecuteRole();
			LoadExecuteRoleUser();
		}

		public void FillDataToGrid(List<HisEkipUserADO> lst)
		{
			try
			{
				if (lst != null && lst.Count > 0)
				{
					int index = 0;
					lst.ForEach(delegate(HisEkipUserADO o)
					{
						o.IsMinus = true;
						if (index == 0)
						{
							o.IsPlus = true;
						}
						else
						{
							o.IsPlus = false;
						}
						index++;
					});
				}
				grdControlInformationSurg.DataSource = new List<HisEkipUserADO>();
				grdControlInformationSurg.DataSource = lst;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void FillDataToGridDepartment()
		{
			try
			{
				if (!DepartmentId.HasValue || !(DepartmentId > 0))
				{
					return;
				}
				List<HisEkipUserADO> list = (List<HisEkipUserADO>)grdControlInformationSurg.DataSource;
				if (list != null && list.Count > 0)
				{
					Parallel.ForEach(list.Where((HisEkipUserADO f) => ((V_HIS_EKIP_USER)f).ID >= 0), delegate(HisEkipUserADO l)
					{
						((V_HIS_EKIP_USER)l).DEPARTMENT_ID = DepartmentId;
					});
				}
				grdViewInformationSurg.BeginDataUpdate();
				list.ForEach(delegate(HisEkipUserADO o)
				{
					o.Action = 2;
				});
				list.LastOrDefault().Action = 1;
				FillDataToGrid(list);
				grdViewInformationSurg.EndDataUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void FillDataToInformationSurgFromSereServLast(List<HisEkipUserADO> ekipUserAdos, HIS_SERE_SERV sereServLast)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				CommonParam val = new CommonParam();
				HisEkipUserViewFilter val2 = new HisEkipUserViewFilter();
				val2.EKIP_ID = sereServLast.EKIP_ID;
				((FilterBase)val2).ORDER_DIRECTION = "ASC";
				((FilterBase)val2).ORDER_FIELD = "ID";
				List<V_HIS_EKIP_USER> source = ((AdapterBase)new BackendAdapter(val)).Get<List<V_HIS_EKIP_USER>>("api/HisEkipUser/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val2, val);
				source = source.Where((V_HIS_EKIP_USER o) => o.IS_ACTIVE == 1).ToList();
				if (source.Count <= 0)
				{
					return;
				}
				foreach (V_HIS_EKIP_USER item in source)
				{
					HIS_EXECUTE_ROLE val3 = BackendDataWorker.Get<HIS_EXECUTE_ROLE>().FirstOrDefault((HIS_EXECUTE_ROLE p) => p.ID == item.EXECUTE_ROLE_ID && p.IS_ACTIVE == 1);
					if (val3 != null && val3.ID != 0)
					{
						Mapper.CreateMap<V_HIS_EKIP_USER, HisEkipUserADO>();
						HisEkipUserADO val4 = Mapper.Map<V_HIS_EKIP_USER, HisEkipUserADO>(item);
						SetDepartment(val4);
						ekipUserAdos.Add(val4);
					}
				}
				FillDataToGrid(ekipUserAdos);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void FillDataToInformationSurgFromServiceReqEkipPlan(List<HisEkipUserADO> ekipUserAdos, V_HIS_SERVICE_REQ serviceReq)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				CommonParam val = new CommonParam();
				HisEkipPlanUserViewFilter val2 = new HisEkipPlanUserViewFilter();
				val2.EKIP_PLAN_ID = serviceReq.EKIP_PLAN_ID;
				List<V_HIS_EKIP_PLAN_USER> list = ((AdapterBase)new BackendAdapter(val)).Get<List<V_HIS_EKIP_PLAN_USER>>("api/HisEkipPlanUser/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val2, val);
				if (list.Count <= 0)
				{
					return;
				}
				foreach (V_HIS_EKIP_PLAN_USER item in list)
				{
					HIS_EXECUTE_ROLE val3 = BackendDataWorker.Get<HIS_EXECUTE_ROLE>().FirstOrDefault((HIS_EXECUTE_ROLE p) => p.ID == item.EXECUTE_ROLE_ID && p.IS_ACTIVE == 1);
					if (val3 != null && val3.ID != 0)
					{
						Mapper.CreateMap<V_HIS_EKIP_PLAN_USER, HisEkipUserADO>();
						HisEkipUserADO val4 = Mapper.Map<V_HIS_EKIP_PLAN_USER, HisEkipUserADO>(item);
						((V_HIS_EKIP_USER)val4).DEPARTMENT_ID = null;
						SetDepartment(val4);
						ekipUserAdos.Add(val4);
					}
				}
				FillDataToGrid(ekipUserAdos);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void FillDataToInformationSurg(bool? isClick = null)
		{
			//IL_019a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a1: Expected O, but got Unknown
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c8: Expected O, but got Unknown
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Expected O, but got Unknown
			try
			{
				List<HisEkipUserADO> list = grdControlInformationSurg.DataSource as List<HisEkipUserADO>;
				if (grdControlInformationSurg != null && grdControlInformationSurg.DataSource != null && list != null && list.Count > 0)
				{
					return;
				}
				LogSystem.Warn("FillDataToInformationSurg_____________1");
				List<HisEkipUserADO> source = new List<HisEkipUserADO>();
				string text = HisConfigs.Get<string>("HIS.DESKTOP.PLUGINS.SURGSERVICEREQEXECUTE.EXECUTE_ROLE_DEFAULT");
				List<HIS_EXECUTE_ROLE> source2 = BackendDataWorker.Get<HIS_EXECUTE_ROLE>();
				source = source.Where((HisEkipUserADO o) => ((V_HIS_EKIP_USER)o).IS_ACTIVE == 1).ToList();
				if (!string.IsNullOrEmpty(text))
				{
					string[] collection = text.Split(',');
					List<string> list2 = new List<string>(collection);
					foreach (string item in list2)
					{
						HIS_EXECUTE_ROLE executeRoleCheck = source2.FirstOrDefault((HIS_EXECUTE_ROLE o) => o.EXECUTE_ROLE_CODE == item);
						if (executeRoleCheck != null)
						{
							HIS_EXECUTE_ROLE val = BackendDataWorker.Get<HIS_EXECUTE_ROLE>().FirstOrDefault((HIS_EXECUTE_ROLE p) => p.ID == executeRoleCheck.ID && p.IS_ACTIVE == 1);
							if (val != null && val.ID != 0)
							{
								HisEkipUserADO val2 = new HisEkipUserADO();
								((V_HIS_EKIP_USER)val2).EXECUTE_ROLE_ID = executeRoleCheck.ID;
								SetDepartment(val2);
								source.Add(val2);
							}
						}
					}
				}
				else
				{
					HisEkipUserADO item2 = new HisEkipUserADO();
					source.Add(item2);
				}
				if (source == null || source.Count == 0)
				{
					HisEkipUserADO item3 = new HisEkipUserADO();
					source.Add(item3);
				}
				LogSystem.Warn("FillDataToInformationSurg_____________2");
				if (!isClick.HasValue || !isClick.Value)
				{
					FillDataToGrid(source);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public List<HisEkipUserADO> GetDataSource()
		{
			try
			{
				return grdControlInformationSurg.DataSource as List<HisEkipUserADO>;
			}
			catch (Exception)
			{
				return new List<HisEkipUserADO>();
			}
		}

		public void LoadGridEkipUserFromTemp(long ekipTempId)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_0159: Expected O, but got Unknown
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
				List<ACS_USER> source = (from o in BackendDataWorker.Get<ACS_USER>()
					where loginNames.Exists((string p) => p == o.LOGINNAME)
					select o).ToList();
				List<string> list3 = (from i in source
					where i.IS_ACTIVE == 1
					select i.LOGINNAME).ToList();
				foreach (HIS_EKIP_TEMP_USER ekipTempUser in list)
				{
					HIS_EXECUTE_ROLE val3 = BackendDataWorker.Get<HIS_EXECUTE_ROLE>().FirstOrDefault((HIS_EXECUTE_ROLE p) => p.ID == ekipTempUser.EXECUTE_ROLE_ID && p.IS_ACTIVE == 1);
					if (val3 != null && val3.ID != 0)
					{
						HisEkipUserADO val4 = new HisEkipUserADO();
						((V_HIS_EKIP_USER)val4).EXECUTE_ROLE_ID = ekipTempUser.EXECUTE_ROLE_ID;
						((V_HIS_EKIP_USER)val4).LOGINNAME = ekipTempUser.LOGINNAME;
						((V_HIS_EKIP_USER)val4).USERNAME = ekipTempUser.USERNAME;
						((V_HIS_EKIP_USER)val4).DEPARTMENT_ID = ekipTempUser.DEPARTMENT_ID;
						if (list3.Contains(ekipTempUser.LOGINNAME))
						{
							SetDepartment(val4);
							list2.Add(val4);
						}
					}
				}
				int index = 0;
				list2.ForEach(delegate(HisEkipUserADO o)
				{
					o.IsMinus = true;
					if (index == 0)
					{
						o.IsPlus = true;
					}
					else
					{
						o.IsPlus = false;
					}
					index++;
				});
				grdControlInformationSurg.DataSource = list2;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void SetColorTitle(bool isRed)
		{
			try
			{
				if (!isRed)
				{
					lciInformationSurg.AppearanceItemCaption.ForeColor = Color.Black;
				}
				else
				{
					lciInformationSurg.AppearanceItemCaption.ForeColor = Color.Maroon;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void SetEnableControl(bool enable)
		{
			try
			{
				grdViewInformationSurg.OptionsBehavior.ReadOnly = enable;
				grdViewInformationSurg.OptionsCustomization.AllowFilter = !enable;
				grdViewInformationSurg.OptionsCustomization.AllowSort = !enable;
				grdViewInformationSurg.OptionsBehavior.Editable = !enable;
				btnAdd.ReadOnly = enable;
				btnDelete.ReadOnly = enable;
				grdViewInformationSurg.RefreshData();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void EnableControl(bool enable)
		{
			try
			{
				lciInformationSurg.Enabled = enable;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
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
                    datas = await new Inventec.Common.Adapter.BackendAdapter(paramCommon).GetAsync<List<HIS_EXECUTE_ROLE>>("api/HisExecuteRole/Get", ApiConsumers.MosConsumer, filter, paramCommon);
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

		private void grdViewInformationSurg_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			try
			{
				HisEkipUserADO department = (HisEkipUserADO)grdViewInformationSurg.GetFocusedRow();
				if (e.Column.FieldName == "LOGINNAME")
				{
					SetDepartment(department);
					grdControlInformationSurg.RefreshDataSource();
				}
				else if (e.Column.FieldName == "EXECUTE_ROLE_ID")
				{
					int visibleIndex = grdViewInformationSurg.FocusedColumn.VisibleIndex;
					int index = visibleIndex + 1;
					grdViewInformationSurg.FocusedColumn = grdViewInformationSurg.VisibleColumns[index];
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void grdViewInformationSurg_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			try
			{
				if (e.Column.FieldName == "BtnAdd")
				{
					int num = System.Convert.ToInt32(e.RowHandle);
					if (Parse.ToBoolean((grdViewInformationSurg.GetRowCellValue(e.RowHandle, "IsPlus") ?? "").ToString()))
					{
						e.RepositoryItem = btnAdd;
					}
				}
				else if (e.Column.FieldName == "BtnDelete")
				{
					int num2 = System.Convert.ToInt32(e.RowHandle);
					if (Parse.ToBoolean((grdViewInformationSurg.GetRowCellValue(e.RowHandle, "IsMinus") ?? "").ToString()))
					{
						e.RepositoryItem = btnDelete;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void grdViewInformationSurg_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
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

		private void grdViewInformationSurg_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
		{
			try
			{
				if (e.FocusedColumn.FieldName == "LOGINNAME")
				{
					grdViewInformationSurg.ShowEditor();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void grdViewInformationSurg_ShownEditor(object sender, EventArgs e)
		{
			try
			{
				GridView gridView = sender as GridView;
				object focusedRow = gridView.GetFocusedRow();
				HisEkipUserADO data = (HisEkipUserADO)((focusedRow is HisEkipUserADO) ? focusedRow : null);
				if (gridView.FocusedColumn.FieldName == "LOGINNAME" && gridView.ActiveEditor is GridLookUpEdit)
				{
					GridLookUpEdit editor = gridView.ActiveEditor as GridLookUpEdit;
					LoadDataToUser(data, editor);
					grdViewInformationSurg.RefreshData();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadDataToUser(HisEkipUserADO data, GridLookUpEdit editor)
		{
			try
			{
				List<string> list = new List<string>();
				if (data != null && ((V_HIS_EKIP_USER)data).EXECUTE_ROLE_ID > 0)
				{
					List<HIS_EXECUTE_ROLE_USER> list2 = ((executeRoleUsers != null) ? executeRoleUsers.Where((HIS_EXECUTE_ROLE_USER o) => o.EXECUTE_ROLE_ID == ((V_HIS_EKIP_USER)data).EXECUTE_ROLE_ID).ToList() : null);
					if (list2 != null && list2.Count > 0)
					{
						list = list2.Select((HIS_EXECUTE_ROLE_USER o) => o.LOGINNAME).Distinct().ToList();
					}
					if (((V_HIS_EKIP_USER)data).LOGINNAME != null)
					{
						if (HisConfigCFG.SURG_SERVICE_REQ_EXECUTE_ROLE_USER_OPTION == "1")
						{
							if (list.Contains(((V_HIS_EKIP_USER)data).LOGINNAME))
							{
								editor.EditValue = ((V_HIS_EKIP_USER)data).LOGINNAME;
							}
							else
							{
								editor.EditValue = null;
								((V_HIS_EKIP_USER)data).LOGINNAME = null;
							}
						}
						else
						{
							editor.EditValue = ((V_HIS_EKIP_USER)data).LOGINNAME;
						}
					}
				}
				ComboAcsUser(editor, list);
				SetDepartment(data);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void SetDepartmentID(long? id)
		{
			try
			{
				DepartmentId = id;
				long? departmentId = DepartmentId;
				LogSystem.Warn("SetDepartmentID_____________" + departmentId);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDepartment(HisEkipUserADO data)
		{
			try
			{
				if (data == null)
				{
					return;
				}
				if (((V_HIS_EKIP_USER)data).DEPARTMENT_ID.HasValue && ((V_HIS_EKIP_USER)data).DEPARTMENT_ID.Value > 0)
				{
					LogSystem.Warn("data.DEPARTMENT_ID.HasValue_____________" + ((V_HIS_EKIP_USER)data).DEPARTMENT_ID);
					return;
				}
				long? departmentId = DepartmentId;
				LogSystem.Warn("SetDepartment_____________" + departmentId);
				if (DepartmentId.HasValue && DepartmentId > 0)
				{
					((V_HIS_EKIP_USER)data).DEPARTMENT_ID = DepartmentId;
					return;
				}
				((V_HIS_EKIP_USER)data).DEPARTMENT_ID = null;
				((V_HIS_EKIP_USER)data).DEPARTMENT_NAME = "";
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ComboAcsUser(GridLookUpEdit cbo, List<string> loginNames)
		{
			try
			{
				List<AcsUserADO> list = new List<AcsUserADO>();
				list = ((loginNames != null && loginNames.Count > 0) ? AcsUserADOList.Where((AcsUserADO o) => loginNames.Contains(((ACS_USER)o).LOGINNAME) && ((ACS_USER)o).IS_ACTIVE == 1).ToList() : ((!(HisConfigCFG.SURG_SERVICE_REQ_EXECUTE_ROLE_USER_OPTION == "1")) ? AcsUserADOList.Where((AcsUserADO o) => ((ACS_USER)o).IS_ACTIVE == 1).ToList() : null));
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
				GridColumn gridColumn3 = cbo.Properties.View.Columns.AddField("DOB");
				gridColumn3.Caption = "Ngày sinh";
				gridColumn3.Visible = true;
				gridColumn3.VisibleIndex = 3;
				gridColumn3.Width = 100;
				GridColumn gridColumn4 = cbo.Properties.View.Columns.AddField("DIPLOMA");
				gridColumn4.Caption = "CCHN";
				gridColumn4.Visible = true;
				gridColumn4.VisibleIndex = 4;
				gridColumn4.Width = 100;
				GridColumn gridColumn5 = cbo.Properties.View.Columns.AddField("DEPARTMENT_NAME");
				gridColumn5.Caption = "Tên khoa";
				gridColumn5.Visible = true;
				gridColumn5.VisibleIndex = 5;
				gridColumn5.Width = 200;
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
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Expected O, but got Unknown
			//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Expected O, but got Unknown
			//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0202: Expected O, but got Unknown
			List<AcsUserADO> list = null;
			try
			{
				List<ACS_USER> list2 = null;
				List<V_HIS_EMPLOYEE> list3 = null;
				CommonParam val = new CommonParam();
				dynamic val2 = new ExpandoObject();
				list2 = new BackendAdapter(new CommonParam()).Get<List<ACS_USER>>("api/AcsUser/Get", ApiConsumers.AcsConsumer, val2, val);
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
					if (item.LOGINNAME == "anhnp")
					{
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<ACS_USER>((Expression<Func<ACS_USER>>)(() => item)), (object)item));
					}
					AcsUserADO acsUserADO = new AcsUserADO();
					((ACS_USER)acsUserADO).ID = item.ID;
					((ACS_USER)acsUserADO).LOGINNAME = item.LOGINNAME;
					((ACS_USER)acsUserADO).USERNAME = item.USERNAME;
					((ACS_USER)acsUserADO).MOBILE = item.MOBILE;
					((ACS_USER)acsUserADO).PASSWORD = item.PASSWORD;
					V_HIS_EMPLOYEE check = list3.FirstOrDefault((V_HIS_EMPLOYEE o) => o.LOGINNAME == item.LOGINNAME && o.IS_ACTIVE == 1);
					if (check != null)
					{
						((ACS_USER)acsUserADO).IS_ACTIVE = check.IS_ACTIVE;
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

		private async Task ComboAcsUser()
		{
			try
			{
				ControlEditorADO controlEditorADO = new ControlEditorADO("USERNAME", "LOGINNAME", new List<ColumnInfo>
				{
					new ColumnInfo("LOGINNAME", "", 150, 1),
					new ColumnInfo("USERNAME", "", 250, 2),
					new ColumnInfo("DOB", "", 100, 3),
					new ColumnInfo("DIPLOMA", "", 100, 4),
					new ColumnInfo("DEPARTMENT_NAME", "", 200, 5)
				}, false, 800);
				ControlEditorLoader.Load((object)GridLookupEdit_UserName, (object)AcsUserADOList, controlEditorADO);
				repositoryItemSearchLookUpEdit1.DataSource = AcsUserADOList;
				repositoryItemSearchLookUpEdit1.DisplayMember = "USERNAME";
				repositoryItemSearchLookUpEdit1.ValueMember = "LOGINNAME";
				repositoryItemSearchLookUpEdit1.TextEditStyle = TextEditStyles.Standard;
				repositoryItemSearchLookUpEdit1.PopupFilterMode = PopupFilterMode.Contains;
				repositoryItemSearchLookUpEdit1.ImmediatePopup = true;
				repositoryItemSearchLookUpEdit1.View.Columns.Clear();
				GridColumn aColumnCode = repositoryItemSearchLookUpEdit1.View.Columns.AddField("LOGINNAME");
				aColumnCode.Caption = "Mã";
				aColumnCode.Visible = true;
				aColumnCode.VisibleIndex = 1;
				aColumnCode.Width = 100;
				GridColumn aColumnName = repositoryItemSearchLookUpEdit1.View.Columns.AddField("USERNAME");
				aColumnName.Caption = "Tên";
				aColumnName.Visible = true;
				aColumnName.VisibleIndex = 2;
				aColumnName.Width = 200;
				GridColumn aColumnDOB = repositoryItemSearchLookUpEdit1.View.Columns.AddField("DOB");
				aColumnDOB.Caption = "DOB";
				aColumnDOB.Visible = true;
				aColumnDOB.VisibleIndex = 3;
				aColumnDOB.Width = 100;
				repositoryItemSearchLookUpEdit1.View.Columns.AddField("DIPLOMA");
				aColumnDOB.Caption = "CCHN";
				aColumnDOB.Visible = true;
				aColumnDOB.VisibleIndex = 4;
				aColumnDOB.Width = 100;
				GridColumn aColumnDepartment = repositoryItemSearchLookUpEdit1.View.Columns.AddField("DEPARTMENT_NAME");
				aColumnDepartment.Caption = "Khoa";
				aColumnDepartment.Visible = true;
				aColumnDepartment.VisibleIndex = 5;
				aColumnDepartment.Width = 200;
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Warn(ex2);
			}
		}

		private void LoadDataToComboDepartment()
		{
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			try
			{
				departmentClinic = (from o in BackendDataWorker.Get<HIS_DEPARTMENT>()
					where o.IS_CLINICAL == 1 && o.IS_ACTIVE == 1
					select o).ToList();
				List<ColumnInfo> list = new List<ColumnInfo>();
				list.Add(new ColumnInfo("DEPARTMENT_CODE", "", 150, 1));
				list.Add(new ColumnInfo("DEPARTMENT_NAME", "", 250, 2));
				ControlEditorADO val = new ControlEditorADO("DEPARTMENT_NAME", "ID", list, false, 400);
				val.ImmediatePopup = true;
				ControlEditorLoader.Load((object)GridLookUpEdit_Department, (object)departmentClinic, val);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadExecuteRoleUser()
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			try
			{
				LogSystem.Error("---S");
				HisExecuteRoleUserFilter val = new HisExecuteRoleUserFilter();
				executeRoleUsers = BackendDataWorker.Get<HIS_EXECUTE_ROLE_USER>();
				LogSystem.Error("---F");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnAdd_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			try
			{
				List<HisEkipUserADO> list = new List<HisEkipUserADO>();
				List<HisEkipUserADO> list2 = grdControlInformationSurg.DataSource as List<HisEkipUserADO>;
				HisEkipUserADO item = new HisEkipUserADO();
				list2.Add(item);
				grdControlInformationSurg.DataSource = null;
				int index = 0;
				list2.ForEach(delegate(HisEkipUserADO o)
				{
					o.IsMinus = true;
					if (index == 0)
					{
						o.IsPlus = true;
					}
					else
					{
						o.IsPlus = false;
					}
					index++;
				});
				grdControlInformationSurg.DataSource = list2;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			try
			{
				CommonParam val = new CommonParam();
				List<HisEkipUserADO> list = grdControlInformationSurg.DataSource as List<HisEkipUserADO>;
				HisEkipUserADO val2 = (HisEkipUserADO)grdViewInformationSurg.GetFocusedRow();
				if (val2 == null)
				{
					return;
				}
				if (list.Count > 1)
				{
					list.Remove(val2);
				}
				else if (list.Count == 1)
				{
					list = new List<HisEkipUserADO>();
					list.Add(new HisEkipUserADO());
				}
				grdControlInformationSurg.DataSource = null;
				int index = 0;
				list.ForEach(delegate(HisEkipUserADO o)
				{
					o.IsMinus = true;
					if (index == 0)
					{
						o.IsPlus = true;
					}
					else
					{
						o.IsPlus = false;
					}
					index++;
				});
				grdControlInformationSurg.DataSource = list;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void GridLookupEdit_UserName_Closed(object sender, ClosedEventArgs e)
		{
			try
			{
				grdViewInformationSurg.FocusedRowHandle = -2147483647;
				grdViewInformationSurg.FocusedColumn = grdViewInformationSurg.VisibleColumns[2];
				object loginName = grdViewInformationSurg.GetFocusedRowCellValue("LOGINNAME");
				List<AcsUserADO> data = AcsUserADOList.Where((AcsUserADO o) => ((ACS_USER)o).LOGINNAME.Equals(loginName)).ToList();
				if (data != null && data.Count > 0)
				{
					if (!string.IsNullOrEmpty(data.FirstOrDefault().DEPARTMENT_NAME))
					{
						List<HIS_DEPARTMENT> list = departmentClinic.Where((HIS_DEPARTMENT o) => o.DEPARTMENT_NAME.Equals(data.FirstOrDefault().DEPARTMENT_NAME)).ToList();
						if (list != null && list.Count > 0)
						{
							grdViewInformationSurg.SetFocusedRowCellValue("DEPARTMENT_ID", list.FirstOrDefault().ID);
						}
						else
						{
							grdViewInformationSurg.SetFocusedRowCellValue("DEPARTMENT_ID", null);
						}
					}
				}
				else
				{
					grdViewInformationSurg.SetFocusedRowCellValue("DEPARTMENT_ID", null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void grdControlInformationSurg_ProcessGridKey(object sender, KeyEventArgs e)
		{
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			if (e.KeyCode != Keys.Return || grdViewInformationSurg.FocusedColumn.VisibleIndex == 0)
			{
				return;
			}
			if (grdViewInformationSurg.FocusedColumn.VisibleIndex == grdViewInformationSurg.VisibleColumns.Count - 1)
			{
				HisEkipUserADO val = (HisEkipUserADO)grdViewInformationSurg.GetFocusedRow();
				if (val.IsPlus)
				{
					btnAdd_ButtonClick(null, null);
					grdViewInformationSurg.FocusedRowHandle = grdViewInformationSurg.DataRowCount - 1;
					int visibleIndex = grdViewInformationSurg.FocusedColumn.VisibleIndex;
					int num = visibleIndex + 1;
					if (num == grdViewInformationSurg.VisibleColumns.Count)
					{
						num = 0;
					}
					grdViewInformationSurg.FocusedColumn = grdViewInformationSurg.VisibleColumns[num];
				}
			}
			else if (grdViewInformationSurg.FocusedColumn.VisibleIndex == grdViewInformationSurg.VisibleColumns.Count - 2)
			{
				btnDelete_ButtonClick(null, null);
			}
			else
			{
				int visibleIndex2 = grdViewInformationSurg.FocusedColumn.VisibleIndex;
				int num2 = visibleIndex2 + 1;
				if (num2 == grdViewInformationSurg.VisibleColumns.Count)
				{
					num2 = 0;
				}
				grdViewInformationSurg.FocusedColumn = grdViewInformationSurg.VisibleColumns[num2];
			}
		}

		private void SetCaptionByLanguageKey()
		{
			try
			{
				ResourceLanguageManager.LanguageResource__UCEkipUser = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(UCEkipUser).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("UCEkipUser.layoutControl1.Text", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				gridColumn1.Caption = Inventec.Common.Resource.Get.Value("UCEkipUser.gridColumn1.Caption", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				cboPosition.NullText = Inventec.Common.Resource.Get.Value("UCEkipUser.cboPosition.NullText", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				gridColumn2.Caption = Inventec.Common.Resource.Get.Value("UCEkipUser.gridColumn2.Caption", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				((RepositoryItem)(object)GridLookupEdit_UserName).NullText = Inventec.Common.Resource.Get.Value("UCEkipUser.GridLookupEdit_UserName.NullText", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				gridColumn3.Caption = Inventec.Common.Resource.Get.Value("UCEkipUser.gridColumn3.Caption", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				((RepositoryItem)(object)GridLookUpEdit_Department).NullText = Inventec.Common.Resource.Get.Value("UCEkipUser.GridLookUpEdit_Department.NullText", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				gridColumn4.Caption = Inventec.Common.Resource.Get.Value("UCEkipUser.gridColumn4.Caption", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				gridColumn5.Caption = Inventec.Common.Resource.Get.Value("UCEkipUser.gridColumn5.Caption", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				repositoryItemGridLookUpEditUsername.NullText = Inventec.Common.Resource.Get.Value("UCEkipUser.repositoryItemGridLookUpEditUsername.NullText", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				repositoryItemSearchLookUpEdit1.NullText = Inventec.Common.Resource.Get.Value("UCEkipUser.repositoryItemSearchLookUpEdit1.NullText", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
				lciInformationSurg.Text = Inventec.Common.Resource.Get.Value("UCEkipUser.lciInformationSurg.Text", ResourceLanguageManager.LanguageResource__UCEkipUser, LanguageManager.GetCulture());
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void cboPosition_Closed(object sender, ClosedEventArgs e)
		{
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			try
			{
				LookUpEdit lookUpEdit = sender as LookUpEdit;
				if (lookUpEdit != null && lookUpEdit.EditValue != null && (lookUpEdit.EditValue ?? ((object)0)).ToString() != (lookUpEdit.OldEditValue ?? ((object)0)).ToString())
				{
					GridView gridView = grdViewInformationSurg;
					HisEkipUserADO val = (HisEkipUserADO)grdViewInformationSurg.GetFocusedRow();
					((V_HIS_EKIP_USER)val).EXECUTE_ROLE_ID = System.Convert.ToInt64(lookUpEdit.EditValue);
					GridLookUpEdit gridLookUpEdit = gridView.ActiveEditor as GridLookUpEdit;
					if (gridLookUpEdit != null)
					{
						LoadDataToUser(val, gridLookUpEdit);
					}
					else
					{
						GridLookUpEdit editor = new GridLookUpEdit();
						LoadDataToUser(val, editor);
					}
					if (((V_HIS_EKIP_USER)val).LOGINNAME == null && HisConfigCFG.SURG_SERVICE_REQ_EXECUTE_ROLE_USER_OPTION == "1")
					{
						gridView.SetRowCellValue(gridView.FocusedRowHandle, gridView.Columns["LOGINNAME"], null);
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
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			layoutControl1 = new LayoutControl();
			grdControlInformationSurg = new GridControl();
			grdViewInformationSurg = new GridView();
			gridColumn1 = new GridColumn();
			cboPosition = new RepositoryItemLookUpEdit();
			gridColumn2 = new GridColumn();
			GridLookupEdit_UserName = new HIS.Desktop.Utilities.Extensions.RepositoryItemCustomGridLookUpEdit();
			repositoryItemCustomGridLookUpEdit2View = new HIS.Desktop.Utilities.Extensions.CustomGridView();
			gridColumn3 = new GridColumn();
			GridLookUpEdit_Department = new HIS.Desktop.Utilities.Extensions.RepositoryItemCustomGridLookUpEdit();
			repositoryItemCustomGridLookUpEdit1View = new HIS.Desktop.Utilities.Extensions.CustomGridView();
			gridColumn4 = new GridColumn();
			gridColumn6 = new GridColumn();
			gridColumn5 = new GridColumn();
			btnAdd = new RepositoryItemButtonEdit();
			txtLogin = new RepositoryItemTextEdit();
			btnDelete = new RepositoryItemButtonEdit();
			repositoryItemGridLookUpEditUsername = new RepositoryItemGridLookUpEdit();
			repositoryItemGridLookUpEdit1View = new GridView();
			repositoryItemSearchLookUpEdit1 = new RepositoryItemSearchLookUpEdit();
			repositoryItemSearchLookUpEdit1View = new GridView();
			layoutControlGroup1 = new LayoutControlGroup();
			lciInformationSurg = new LayoutControlItem();
			((ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((ISupportInitialize)grdControlInformationSurg).BeginInit();
			((ISupportInitialize)grdViewInformationSurg).BeginInit();
			((ISupportInitialize)cboPosition).BeginInit();
			((ISupportInitialize)GridLookupEdit_UserName).BeginInit();
			((ISupportInitialize)repositoryItemCustomGridLookUpEdit2View).BeginInit();
			((ISupportInitialize)GridLookUpEdit_Department).BeginInit();
			((ISupportInitialize)repositoryItemCustomGridLookUpEdit1View).BeginInit();
			((ISupportInitialize)btnAdd).BeginInit();
			((ISupportInitialize)txtLogin).BeginInit();
			((ISupportInitialize)btnDelete).BeginInit();
			((ISupportInitialize)repositoryItemGridLookUpEditUsername).BeginInit();
			((ISupportInitialize)repositoryItemGridLookUpEdit1View).BeginInit();
			((ISupportInitialize)repositoryItemSearchLookUpEdit1).BeginInit();
			((ISupportInitialize)repositoryItemSearchLookUpEdit1View).BeginInit();
			((ISupportInitialize)layoutControlGroup1).BeginInit();
			((ISupportInitialize)lciInformationSurg).BeginInit();
			((Control)this).SuspendLayout();
			layoutControl1.Controls.Add(grdControlInformationSurg);
			layoutControl1.Dock = DockStyle.Fill;
			layoutControl1.Location = new Point(0, 0);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new Size(742, 264);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			grdControlInformationSurg.Location = new Point(2, 2);
			grdControlInformationSurg.MainView = grdViewInformationSurg;
			grdControlInformationSurg.Name = "grdControlInformationSurg";
			grdControlInformationSurg.RepositoryItems.AddRange(new RepositoryItem[8]
			{
				btnAdd,
				txtLogin,
				btnDelete,
				repositoryItemGridLookUpEditUsername,
				repositoryItemSearchLookUpEdit1,
				(RepositoryItem)(object)GridLookUpEdit_Department,
				(RepositoryItem)(object)GridLookupEdit_UserName,
				cboPosition
			});
			grdControlInformationSurg.Size = new Size(738, 260);
			grdControlInformationSurg.TabIndex = 4;
			grdControlInformationSurg.ViewCollection.AddRange(new BaseView[1] { grdViewInformationSurg });
			grdControlInformationSurg.ProcessGridKey += grdControlInformationSurg_ProcessGridKey;
			grdViewInformationSurg.Columns.AddRange(new GridColumn[6] { gridColumn1, gridColumn2, gridColumn3, gridColumn4, gridColumn6, gridColumn5 });
			grdViewInformationSurg.GridControl = grdControlInformationSurg;
			grdViewInformationSurg.Name = "grdViewInformationSurg";
			grdViewInformationSurg.OptionsView.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
			grdViewInformationSurg.OptionsView.ShowGroupPanel = false;
			grdViewInformationSurg.OptionsView.ShowIndicator = false;
			grdViewInformationSurg.CustomRowCellEdit += grdViewInformationSurg_CustomRowCellEdit;
			grdViewInformationSurg.ShownEditor += grdViewInformationSurg_ShownEditor;
			grdViewInformationSurg.FocusedColumnChanged += grdViewInformationSurg_FocusedColumnChanged;
			grdViewInformationSurg.CellValueChanged += grdViewInformationSurg_CellValueChanged;
			grdViewInformationSurg.CustomUnboundColumnData += grdViewInformationSurg_CustomUnboundColumnData;
			gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
			gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumn1.Caption = "Vai trò";
			gridColumn1.ColumnEdit = cboPosition;
			gridColumn1.FieldName = "EXECUTE_ROLE_ID";
			gridColumn1.Name = "gridColumn1";
			gridColumn1.Visible = true;
			gridColumn1.VisibleIndex = 0;
			gridColumn1.Width = 209;
			cboPosition.AutoHeight = false;
			cboPosition.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			cboPosition.Name = "cboPosition";
			cboPosition.NullText = "";
			cboPosition.TextEditStyle = TextEditStyles.Standard;
			cboPosition.Closed += cboPosition_Closed;
			gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
			gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumn2.Caption = "Họ tên";
			gridColumn2.ColumnEdit = (RepositoryItem)(object)GridLookupEdit_UserName;
			gridColumn2.FieldName = "LOGINNAME";
			gridColumn2.Name = "gridColumn2";
			gridColumn2.Visible = true;
			gridColumn2.VisibleIndex = 1;
			gridColumn2.Width = 209;
			((RepositoryItemTextEdit)(object)GridLookupEdit_UserName).AllowNullInput = DefaultBoolean.True;
			((RepositoryItemGridLookUpEdit)(object)GridLookupEdit_UserName).AutoComplete = false;
			((RepositoryItem)(object)GridLookupEdit_UserName).AutoHeight = false;
			((RepositoryItemButtonEdit)(object)GridLookupEdit_UserName).Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			((RepositoryItem)(object)GridLookupEdit_UserName).Name = "GridLookupEdit_UserName";
			((RepositoryItem)(object)GridLookupEdit_UserName).NullText = "";
			((RepositoryItemButtonEdit)(object)GridLookupEdit_UserName).TextEditStyle = TextEditStyles.Standard;
			((RepositoryItemGridLookUpEditBase)(object)GridLookupEdit_UserName).View = (GridView)(object)repositoryItemCustomGridLookUpEdit2View;
			((RepositoryItemPopupBase)(object)GridLookupEdit_UserName).Closed += GridLookupEdit_UserName_Closed;
			((GridView)(object)repositoryItemCustomGridLookUpEdit2View).FocusRectStyle = DrawFocusRectStyle.RowFocus;
			((BaseView)(object)repositoryItemCustomGridLookUpEdit2View).Name = "repositoryItemCustomGridLookUpEdit2View";
			((GridView)(object)repositoryItemCustomGridLookUpEdit2View).OptionsSelection.EnableAppearanceFocusedCell = false;
			((GridView)(object)repositoryItemCustomGridLookUpEdit2View).OptionsView.ShowGroupPanel = false;
			gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
			gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			gridColumn3.Caption = "Khoa";
			gridColumn3.ColumnEdit = (RepositoryItem)(object)GridLookUpEdit_Department;
			gridColumn3.FieldName = "DEPARTMENT_ID";
			gridColumn3.Name = "gridColumn3";
			gridColumn3.Visible = true;
			gridColumn3.VisibleIndex = 2;
			gridColumn3.Width = 182;
			((RepositoryItemTextEdit)(object)GridLookUpEdit_Department).AllowNullInput = DefaultBoolean.True;
			((RepositoryItemGridLookUpEdit)(object)GridLookUpEdit_Department).AutoComplete = false;
			((RepositoryItem)(object)GridLookUpEdit_Department).AutoHeight = false;
			((RepositoryItemButtonEdit)(object)GridLookUpEdit_Department).Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			((RepositoryItem)(object)GridLookUpEdit_Department).Name = "GridLookUpEdit_Department";
			((RepositoryItem)(object)GridLookUpEdit_Department).NullText = "";
			((RepositoryItemButtonEdit)(object)GridLookUpEdit_Department).TextEditStyle = TextEditStyles.Standard;
			((RepositoryItemGridLookUpEditBase)(object)GridLookUpEdit_Department).View = (GridView)(object)repositoryItemCustomGridLookUpEdit1View;
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).FocusRectStyle = DrawFocusRectStyle.RowFocus;
			((BaseView)(object)repositoryItemCustomGridLookUpEdit1View).Name = "repositoryItemCustomGridLookUpEdit1View";
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).OptionsSelection.EnableAppearanceFocusedCell = false;
			((GridView)(object)repositoryItemCustomGridLookUpEdit1View).OptionsView.ShowGroupPanel = false;
			gridColumn4.Caption = "gridColumn4";
			gridColumn4.FieldName = "BtnDelete";
			gridColumn4.MaxWidth = 25;
			gridColumn4.MinWidth = 25;
			gridColumn4.Name = "gridColumn4";
			gridColumn4.OptionsColumn.ShowCaption = false;
			gridColumn4.Visible = true;
			gridColumn4.VisibleIndex = 3;
			gridColumn4.Width = 25;
			gridColumn6.Caption = "gridColumn6";
			gridColumn6.FieldName = "BtnAdd";
			gridColumn6.MaxWidth = 25;
			gridColumn6.MinWidth = 25;
			gridColumn6.Name = "gridColumn6";
			gridColumn6.OptionsColumn.ShowCaption = false;
			gridColumn6.Visible = true;
			gridColumn6.VisibleIndex = 4;
			gridColumn6.Width = 25;
			gridColumn5.Caption = "gridColumn5";
			gridColumn5.FieldName = "LOGINNAME";
			gridColumn5.Name = "gridColumn5";
			btnAdd.AutoHeight = false;
			btnAdd.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Plus)
			});
			btnAdd.Name = "btnAdd";
			btnAdd.TextEditStyle = TextEditStyles.HideTextEditor;
			btnAdd.ButtonClick += btnAdd_ButtonClick;
			txtLogin.AutoHeight = false;
			txtLogin.Name = "txtLogin";
			btnDelete.AutoHeight = false;
			btnDelete.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Minus)
			});
			btnDelete.Name = "btnDelete";
			btnDelete.TextEditStyle = TextEditStyles.HideTextEditor;
			btnDelete.ButtonClick += btnDelete_ButtonClick;
			repositoryItemGridLookUpEditUsername.AutoHeight = false;
			repositoryItemGridLookUpEditUsername.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			repositoryItemGridLookUpEditUsername.Name = "repositoryItemGridLookUpEditUsername";
			repositoryItemGridLookUpEditUsername.NullText = "";
			repositoryItemGridLookUpEditUsername.View = repositoryItemGridLookUpEdit1View;
			repositoryItemGridLookUpEdit1View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
			repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
			repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			repositoryItemSearchLookUpEdit1.AutoHeight = false;
			repositoryItemSearchLookUpEdit1.Buttons.AddRange(new EditorButton[1]
			{
				new EditorButton(ButtonPredefines.Combo)
			});
			repositoryItemSearchLookUpEdit1.Name = "repositoryItemSearchLookUpEdit1";
			repositoryItemSearchLookUpEdit1.NullText = "";
			repositoryItemSearchLookUpEdit1.View = repositoryItemSearchLookUpEdit1View;
			repositoryItemSearchLookUpEdit1View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
			repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
			repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.False;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Items.AddRange(new BaseLayoutItem[1] { lciInformationSurg });
			layoutControlGroup1.Location = new Point(0, 0);
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Size = new Size(742, 264);
			layoutControlGroup1.TextVisible = false;
			lciInformationSurg.AppearanceItemCaption.ForeColor = Color.FromArgb(192, 0, 0);
			lciInformationSurg.AppearanceItemCaption.Options.UseForeColor = true;
			lciInformationSurg.AppearanceItemCaption.Options.UseTextOptions = true;
			lciInformationSurg.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
			lciInformationSurg.Control = grdControlInformationSurg;
			lciInformationSurg.Location = new Point(0, 0);
			lciInformationSurg.Name = "lciInformationSurg";
			lciInformationSurg.Size = new Size(742, 264);
			lciInformationSurg.Text = "Kíp thực hiện:";
			lciInformationSurg.TextAlignMode = TextAlignModeItem.CustomSize;
			lciInformationSurg.TextSize = new Size(0, 0);
			lciInformationSurg.TextToControlDistance = 0;
			lciInformationSurg.TextVisible = false;
			((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
			((ContainerControl)this).AutoScaleMode = AutoScaleMode.Font;
			((Control)this).Controls.Add(layoutControl1);
			((Control)this).Name = "UCEkipUser";
			((Control)this).Size = new Size(742, 264);
			((ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			((ISupportInitialize)grdControlInformationSurg).EndInit();
			((ISupportInitialize)grdViewInformationSurg).EndInit();
			((ISupportInitialize)cboPosition).EndInit();
			((ISupportInitialize)GridLookupEdit_UserName).EndInit();
			((ISupportInitialize)repositoryItemCustomGridLookUpEdit2View).EndInit();
			((ISupportInitialize)GridLookUpEdit_Department).EndInit();
			((ISupportInitialize)repositoryItemCustomGridLookUpEdit1View).EndInit();
			((ISupportInitialize)btnAdd).EndInit();
			((ISupportInitialize)txtLogin).EndInit();
			((ISupportInitialize)btnDelete).EndInit();
			((ISupportInitialize)repositoryItemGridLookUpEditUsername).EndInit();
			((ISupportInitialize)repositoryItemGridLookUpEdit1View).EndInit();
			((ISupportInitialize)repositoryItemSearchLookUpEdit1).EndInit();
			((ISupportInitialize)repositoryItemSearchLookUpEdit1View).EndInit();
			((ISupportInitialize)layoutControlGroup1).EndInit();
			((ISupportInitialize)lciInformationSurg).EndInit();
			((Control)this).ResumeLayout(false);
		}
	}
}
