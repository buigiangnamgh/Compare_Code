using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.ADO;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Config;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule;
using Inventec.Common.Adapter;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Common.Mapper;
using Inventec.Core;
using Inventec.Desktop.Common.Controls.ValidationRule;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Modules;
using Inventec.UC.Login.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.FormSurgAssignAndCopy
{
	public class frmSurgAssignAndCopy : Form
	{
		private Inventec.Desktop.Common.Modules.Module moduleData;

		private V_HIS_SERVICE_REQ serviceReq;

		private V_HIS_TREATMENT treatment;

		internal List<long> intructionTimeSelecteds = new List<long>();

		internal List<DateTime?> intructionTimeSelected = new List<DateTime?>();

		internal List<DateTime?> useTimeSelected = new List<DateTime?>();

		private DateTime timeSelested;

		public bool validNgayYLenh;

		private bool isInitUcDate;

		private List<V_HIS_SERVICE> lstService;

		private List<HIS_EKIP_USER> ekipUsers = new List<HIS_EKIP_USER>();

		private UCEkipUser ucEkip;

		private long BEGINTIME;

		private long ENDTIME;

		private List<HisEkipUserADO> ekipAdo;

		private List<DateTime> selectedDates = new List<DateTime>();

		private IContainer components = null;

		private LayoutControl layoutControlRoot;

		private LayoutControlGroup Root;

		private SimpleButton btnSelect;

		private CalendarControl calendarInstructionDate;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private LayoutControlItem lciCalendarInstructionDate;

		private LayoutControlItem layoutControlItem6;

		private DXValidationProvider dxValidationProvider1;

		private DXErrorProvider dxErrorProvider1;

		private TimeSpanEdit timeInstructionTime;

		private DXValidationProvider dxValidationProvider2;

		private ButtonEdit txtNgayYLenh;

		private ButtonEdit txtNgayDuTruTime;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem1;

		private Label lblThongBao;

		private LayoutControlItem layoutControlItem5;

		internal V_HIS_SERE_SERV_5 sereServ { get; set; }

		public frmSurgAssignAndCopy()
		{
			InitializeComponent();
		}

		public frmSurgAssignAndCopy(Inventec.Desktop.Common.Modules.Module moduleData, V_HIS_SERVICE_REQ serviceReq, V_HIS_TREATMENT treatment, V_HIS_SERE_SERV_5 sereServ, long beginTime, long endTime, List<HisEkipUserADO> hisEkipUserADOs)
		{
			InitializeComponent();
			try
			{
				SetIcon();
				this.moduleData = moduleData;
				this.serviceReq = serviceReq;
				this.treatment = treatment;
				this.sereServ = sereServ;
				BEGINTIME = beginTime;
				ENDTIME = endTime;
				ekipAdo = hisEkipUserADOs;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetIcon()
		{
			try
			{
				base.Icon = Icon.ExtractAssociatedIcon(Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void frmSurgAssignAndCopy_Load(object sender, EventArgs e)
		{
			try
			{
				SetDefaultControlProperties();
				LoadServiceFromRam();
				SetDefaultValues();
				ValidateControls();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ValidateControls()
		{
			try
			{
				ValidationSingleControl(timeInstructionTime);
				ValidationInstructionDate();
				ValidTimeSpan();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ValidTimeSpan()
		{
			try
			{
				ValidTimeSpan validTimeSpan = new ValidTimeSpan();
				validTimeSpan.inTime = treatment.IN_TIME;
				validTimeSpan.outTime = treatment.OUT_TIME;
				validTimeSpan.timeSpanEdit = timeInstructionTime;
				validTimeSpan.calendarControl = calendarInstructionDate;
				validTimeSpan.lciCa = lciCalendarInstructionDate;
				dxValidationProvider2.SetValidationRule(timeInstructionTime, validTimeSpan);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ValidationInstructionDate()
		{
			try
			{
				InstructionDateFromValidationRule instructionDateFromValidationRule = new InstructionDateFromValidationRule();
				instructionDateFromValidationRule.isRequired = true;
				instructionDateFromValidationRule.inTime = treatment.IN_TIME;
				instructionDateFromValidationRule.outTime = treatment.OUT_TIME;
				instructionDateFromValidationRule.timeSpan = timeInstructionTime;
				InstructionDateCalendarValidationRule instructionDateCalendarValidationRule = new InstructionDateCalendarValidationRule();
				instructionDateCalendarValidationRule.isRequired = true;
				instructionDateCalendarValidationRule.calendarControl = calendarInstructionDate;
				instructionDateCalendarValidationRule.lci = lciCalendarInstructionDate;
				instructionDateCalendarValidationRule.inTime = treatment.IN_TIME;
				instructionDateCalendarValidationRule.outTime = treatment.OUT_TIME;
				instructionDateCalendarValidationRule.timeSpan = timeInstructionTime;
				dxValidationProvider1.SetValidationRule(calendarInstructionDate, instructionDateCalendarValidationRule);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ValidationSingleControl(BaseEdit control)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			try
			{
				ControlEditValidationRule val = new ControlEditValidationRule();
				val.editor = control;
				((ValidationRuleBase)(object)val).ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage((LibraryMessage.Message.Enum)49);
				((ValidationRuleBase)(object)val).ErrorType = ErrorType.Warning;
				dxValidationProvider1.SetValidationRule(control, (ValidationRuleBase)(object)val);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDefaultControlProperties()
		{
			try
			{
				lciCalendarInstructionDate.Visibility = LayoutVisibility.Never;
				layoutControlRoot.MinimumSize = new Size(layoutControlRoot.Width, 200);
				layoutControlRoot.AutoSize = true;
				layoutControlRoot.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				AutoSize = true;
				base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetDefaultValues()
		{
			try
			{
				timeInstructionTime.EditValue = null;
				calendarInstructionDate.EditValue = null;
				if (serviceReq != null && serviceReq.ID > 0)
				{
					DateTime? dateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME);
					if (dateTime.HasValue)
					{
						LogSystem.Debug(LogUtil.TraceData("instructionTime", (object)dateTime));
						LogSystem.Debug(LogUtil.TraceData("instructionTime.Value.TimeOfDay", (object)dateTime.Value.TimeOfDay));
						timeInstructionTime.TimeSpan = dateTime.Value.TimeOfDay;
						calendarInstructionDate.DateTime = dateTime.Value.Date;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void chkNgayLienTiep_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Expected O, but got Unknown
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				if (string.IsNullOrWhiteSpace(txtNgayYLenh.Text))
				{
					dxErrorProvider1.SetError(txtNgayYLenh, "Ngày y lệnh không được bỏ trống!", ErrorType.Warning);
					return;
				}
				dxErrorProvider1.SetError(txtNgayYLenh, string.Empty);
				bool flag = false;
				CommonParam val = new CommonParam();
				if (serviceReq != null && serviceReq.ID > 0)
				{
					bool flag2 = dxValidationProvider1.Validate();
					if (!flag2 || !flag2 || !dxValidationProvider2.Validate())
					{
						return;
					}
					SurgAssignAndCopySDO sdo = new SurgAssignAndCopySDO();
					if (SetSurgAssignAndCopySDO(ref sdo))
					{
						WaitingManager.Show();
						LogSystem.Debug(LogUtil.TraceData("SurgAssignAndCopySDO", (object)sdo));
						if (((AdapterBase)new BackendAdapter(val)).Post<bool>("api/HisServiceReq/SurgAssignAndCopy", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)sdo, val))
						{
							flag = true;
						}
						MessageManager.Show((Form)this, val, (bool?)flag);
					}
				}
				if (flag)
				{
					Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void LoadServiceFromRam()
		{
			try
			{
				lstService = BackendDataWorker.Get<V_HIS_SERVICE>();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private bool SetSurgAssignAndCopySDO(ref SurgAssignAndCopySDO sdo)
		{
			//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Expected O, but got Unknown
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d7: Expected O, but got Unknown
			//IL_074f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0756: Expected O, but got Unknown
			//IL_0763: Unknown result type (might be due to invalid IL or missing references)
			//IL_076a: Expected O, but got Unknown
			//IL_076c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0776: Expected O, but got Unknown
			//IL_028b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0292: Expected O, but got Unknown
			//IL_084f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0856: Expected O, but got Unknown
			//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d2: Expected O, but got Unknown
			//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f9: Expected O, but got Unknown
			//IL_0612: Unknown result type (might be due to invalid IL or missing references)
			//IL_0619: Expected O, but got Unknown
			//IL_0619: Unknown result type (might be due to invalid IL or missing references)
			//IL_0620: Expected O, but got Unknown
			//IL_0656: Unknown result type (might be due to invalid IL or missing references)
			//IL_095c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0961: Unknown result type (might be due to invalid IL or missing references)
			//IL_096f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0982: Expected O, but got Unknown
			//IL_0994: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0504: Unknown result type (might be due to invalid IL or missing references)
			//IL_050d: Unknown result type (might be due to invalid IL or missing references)
			//IL_051b: Expected O, but got Unknown
			//IL_051e: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				V_HIS_SERVICE val = lstService.FirstOrDefault((V_HIS_SERVICE o) => o.ID == sereServ.SERVICE_ID);
				if (val.ALLOW_SIMULTANEITY != 1)
				{
					sdo.ServiceReqId = serviceReq.ID;
					sdo.InstructionTimes = new List<long>();
					sdo.Usetimes = new List<long>();
					string time = (DateTime.Today.Date + timeInstructionTime.TimeSpan).ToString("HHmm") + "00";
					List<DateTime> dateListFromTextBox = GetDateListFromTextBox(txtNgayYLenh.Text);
					List<DateTime> dateListFromTextBox2 = GetDateListFromTextBox(txtNgayDuTruTime.Text);
					sdo.InstructionTimes = dateListFromTextBox.Select((DateTime date) => System.Convert.ToInt64(date.ToString("yyyyMMdd") + time)).ToList();
					sdo.Usetimes = dateListFromTextBox2.Select((DateTime date) => System.Convert.ToInt64(date.ToString("yyyyMMdd") + time)).ToList();
					List<long> list = (sdo.InstructionTimes ?? new List<long>()).Union(sdo.Usetimes ?? new List<long>()).ToList();
					LogSystem.Info("kiem tra key ASSIGN_SERVICE_SIMULTANEITY_OPTION " + HisConfigKeys.ASSIGN_SERVICE_SIMULTANEITY_OPTION + " ");
					LogSystem.Info("kiem tra key CHECK_SIMULTANEITY_OPTION " + HisConfigKeys.CHECK_SIMULTANEITY_OPTION + " ");
					if (HisConfigKeys.ASSIGN_SERVICE_SIMULTANEITY_OPTION == "1" || HisConfigKeys.ASSIGN_SERVICE_SIMULTANEITY_OPTION == "2")
					{
						HisSereServCheckExecuteTimesSDO val2 = new HisSereServCheckExecuteTimesSDO();
						CommonParam val3 = new CommonParam();
						string loginName = ClientTokenManagerStore.ClientTokenManager.GetLoginName();
						LogSystem.Info("Login " + loginName);
						LogSystem.Debug(LogUtil.TraceData("Login", (object)loginName));
						val2.TreatmentId = serviceReq.TREATMENT_ID;
						LogSystem.Info(string.Format("inputSDO.TreatmentId {0}, {1}", val2.TreatmentId, serviceReq.TREATMENT_ID));
						List<string> list2 = new List<string> { loginName };
						List<HisEkipUserADO> list3 = ekipAdo;
						if (list3 != null && list3.Count() > 0)
						{
							foreach (HisEkipUserADO item in list3)
							{
								HIS_EKIP_USER val4 = new HIS_EKIP_USER();
								DataObjectMapper.Map<HIS_EKIP_USER>((object)val4, (object)item);
								if (val4 != null && val4.EXECUTE_ROLE_ID != 0)
								{
									ekipUsers.Add(val4);
								}
							}
						}
						LogSystem.Debug(LogUtil.TraceData("ekipUsers", (object)ekipUsers));
						List<string> list4 = ekipUsers.Select((HIS_EKIP_USER o) => o.LOGINNAME).Distinct().ToList();
						LogSystem.Info(string.Format("lstLogin {0}", list4));
						List<string> list5 = new List<string>();
						foreach (string item2 in list4)
						{
							if (item2 != null)
							{
								list5.Add(item2);
							}
						}
						if (list5.Count == 0)
						{
							list5.Add(loginName);
						}
						val2.Loginnames = list5;
						LogSystem.Info(string.Format("inputSDO.Loginnames {0}", val2.Loginnames));
						string text = "";
						LogSystem.Info("lay api 1 :/api/HisSereServ/CheckExecuteTimes");
						foreach (long item3 in list)
						{
							CommonParam val5 = new CommonParam();
							DateTime? dateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item3);
							DateTime? dateTime2 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(BEGINTIME);
							string text2 = (dateTime.HasValue ? dateTime.GetValueOrDefault().ToString("yyyyMMdd") : null) ?? "00000000";
							string text3 = (dateTime2.HasValue ? dateTime2.GetValueOrDefault().ToString("HHmmss") : null) ?? "000000";
							string s = text2 + text3;
							long beginTime = long.Parse(s);
							DateTime? dateTime3 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item3);
							DateTime? dateTime4 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ENDTIME);
							string text4 = (dateTime3.HasValue ? dateTime3.GetValueOrDefault().ToString("yyyyMMdd") : null) ?? "00000000";
							string text5 = (dateTime4.HasValue ? dateTime4.GetValueOrDefault().ToString("HHmmss") : null) ?? "000000";
							string s2 = text4 + text5;
							long endTime = long.Parse(s2);
							val2.ExecuteTime = new ExecuteTime
							{
								BeginTime = beginTime,
								EndTime = endTime
							};
							bool flag = ((AdapterBase)new BackendAdapter(val5)).Post<bool>("/api/HisSereServ/CheckExecuteTimes", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val2, val5);
							LogSystem.Debug(LogUtil.TraceData("/api/HisSereServ/CheckExecuteTimes", (object)val2));
							if (flag)
							{
								continue;
							}
							if (HisConfigKeys.ASSIGN_SERVICE_SIMULTANEITY_OPTION == "1")
							{
								XtraMessageBox.Show(val5.GetMessage(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return false;
							}
							if (HisConfigKeys.ASSIGN_SERVICE_SIMULTANEITY_OPTION == "2")
							{
								DialogResult dialogResult = XtraMessageBox.Show(val5.GetMessage(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
								if (dialogResult == DialogResult.No)
								{
									return false;
								}
							}
						}
						LogSystem.Info("lay api 2 :/api/HisServiceReq/CheckSereTimes");
						foreach (long instructionTime in sdo.InstructionTimes)
						{
							CommonParam val6 = new CommonParam();
							HisServiceReqCheckSereTimesSDO val7 = new HisServiceReqCheckSereTimesSDO();
							val7.SereTimes = new List<long> { instructionTime };
							val7.TreatmentId = serviceReq.TREATMENT_ID;
							val7.Loginnames = list5;
							bool flag2 = ((AdapterBase)new BackendAdapter(val6)).Post<bool>("/api/HisServiceReq/CheckSereTimes", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val7, val6);
							LogSystem.Debug(LogUtil.TraceData("/api/HisServiceReq/CheckSereTimes", (object)val7));
							if (flag2)
							{
								continue;
							}
							if (HisConfigKeys.ASSIGN_SERVICE_SIMULTANEITY_OPTION == "1")
							{
								XtraMessageBox.Show(val6.GetMessage(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return false;
							}
							if (HisConfigKeys.ASSIGN_SERVICE_SIMULTANEITY_OPTION == "2")
							{
								DialogResult dialogResult2 = XtraMessageBox.Show(val6.GetMessage(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
								if (dialogResult2 == DialogResult.No)
								{
									return false;
								}
							}
						}
					}
					if (HisConfigKeys.CHECK_SIMULTANEITY_OPTION == "1" || HisConfigKeys.CHECK_SIMULTANEITY_OPTION == "2")
					{
						HisSurgServiceReqUpdateListSDO val8 = new HisSurgServiceReqUpdateListSDO();
						val8.SurgUpdateSDOs = new List<SurgUpdateSDO>();
						SurgUpdateSDO val9 = new SurgUpdateSDO();
						val9.SereServExt = new HIS_SERE_SERV_EXT();
						val9.SereServId = sereServ.ID;
						string loginName2 = ClientTokenManagerStore.ClientTokenManager.GetLoginName();
						List<HisEkipUserADO> list6 = ekipAdo;
						if (list6 != null && list6.Count() > 0)
						{
							foreach (HisEkipUserADO item4 in list6)
							{
								HIS_EKIP_USER val10 = new HIS_EKIP_USER();
								DataObjectMapper.Map<HIS_EKIP_USER>((object)val10, (object)item4);
								if (val10 != null && val10.EXECUTE_ROLE_ID != 0)
								{
									ekipUsers.Add(val10);
								}
							}
						}
						val9.EkipUsers = ekipUsers;
						LogSystem.Info("lay api 3 :api/HisServiceReq/CheckSurgSimultaneily");
						foreach (long item5 in list)
						{
							CommonParam val11 = new CommonParam();
							DateTime? dateTime5 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item5);
							DateTime? dateTime6 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(BEGINTIME);
							string text6 = (dateTime5.HasValue ? dateTime5.GetValueOrDefault().ToString("yyyyMMdd") : null) ?? "00000000";
							string text7 = (dateTime6.HasValue ? dateTime6.GetValueOrDefault().ToString("HHmmss") : null) ?? "000000";
							string s3 = text6 + text7;
							long value = long.Parse(s3);
							DateTime? dateTime7 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item5);
							DateTime? dateTime8 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ENDTIME);
							string text8 = (dateTime7.HasValue ? dateTime7.GetValueOrDefault().ToString("yyyyMMdd") : null) ?? "00000000";
							string text9 = (dateTime8.HasValue ? dateTime8.GetValueOrDefault().ToString("HHmmss") : null) ?? "000000";
							string s4 = text8 + text9;
							long value2 = long.Parse(s4);
							val9.SereServExt = new HIS_SERE_SERV_EXT
							{
								BEGIN_TIME = value,
								END_TIME = value2
							};
							val8.SurgUpdateSDOs.Add(val9);
							bool flag3 = ((AdapterBase)new BackendAdapter(val11)).Post<bool>("api/HisServiceReq/CheckSurgSimultaneily", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val8, val11);
							LogSystem.Debug(LogUtil.TraceData("api/HisServiceReq/CheckSurgSimultaneily", (object)val8));
							LogSystem.Debug(LogUtil.TraceData("api/HisServiceReq/CheckSurgSimultaneily", (object)flag3.ToString()));
							if (flag3)
							{
								continue;
							}
							if (HisConfigKeys.CHECK_SIMULTANEITY_OPTION == "1")
							{
								XtraMessageBox.Show(val11.GetMessage(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return false;
							}
							if (HisConfigKeys.CHECK_SIMULTANEITY_OPTION == "2")
							{
								DialogResult dialogResult3 = XtraMessageBox.Show(val11.GetMessage(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
								if (dialogResult3 == DialogResult.No)
								{
									return false;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return false;
			}
			return true;
		}

		private List<DateTime> GetDateListFromTextBox(string text)
		{
			List<DateTime> list = new List<DateTime>();
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(';');
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					DateTime result;
					if (DateTime.TryParseExact(text2.Trim(), "dd/MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
					{
						result = new DateTime(DateTime.Now.Year, result.Month, result.Day);
						list.Add(result);
					}
				}
			}
			return list;
		}

		private void txtNgayYLenh_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				if (e.Button.Kind == ButtonPredefines.Glyph)
				{
					timeSelested = DateTime.Today.Add((TimeSpan)timeInstructionTime.EditValue);
					frmMultiIntructonTime frmMultiIntructonTime2 = new frmMultiIntructonTime(intructionTimeSelected, timeSelested, delegate(List<DateTime?> datas, DateTime time)
					{
						SelectMultiIntructionTime(datas, time, txtNgayYLenh, true);
					}, "Ngay y lệnh", moduleData, treatment);
					frmMultiIntructonTime2.ShowDialog();
				}
				else if (txtNgayYLenh.EditValue != null && e.Button.Kind == ButtonPredefines.Delete)
				{
					txtNgayYLenh.EditValue = null;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SelectMultiIntructionTime(List<DateTime?> datas, DateTime time, TextEdit targetTextBox, bool isIntructionTime)
		{
			try
			{
				if (datas != null && time != DateTime.MinValue)
				{
					string text = "";
					int num = 0;
					if (isIntructionTime)
					{
						intructionTimeSelected = datas;
						intructionTimeSelected = intructionTimeSelected.OrderBy((DateTime? o) => o.Value).ToList();
						foreach (DateTime? item in intructionTimeSelected)
						{
							if (item.HasValue && item.Value != DateTime.MinValue)
							{
								text = text + ((num == 0) ? "" : "; ") + item.Value.ToString("dd/MM");
								num++;
							}
						}
					}
					else
					{
						useTimeSelected = datas;
						useTimeSelected = useTimeSelected.OrderBy((DateTime? o) => o.Value).ToList();
						foreach (DateTime? item2 in useTimeSelected)
						{
							if (item2.HasValue && item2.Value != DateTime.MinValue)
							{
								text = text + ((num == 0) ? "" : "; ") + item2.Value.ToString("dd/MM");
								num++;
							}
						}
					}
					if (targetTextBox.Text != text)
					{
						isInitUcDate = true;
						timeSelested = time;
						timeInstructionTime.EditValue = timeSelested.ToString("HH:mm");
						targetTextBox.Text = text;
						isInitUcDate = false;
					}
				}
				else
				{
					if (datas != null || !(time != DateTime.MinValue))
					{
						return;
					}
					string text2 = "";
					int num2 = 0;
					if (isIntructionTime)
					{
						XtraMessageBox.Show("Thời gian y lệnh không được trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					useTimeSelected = null;
					useTimeSelected = useTimeSelected.OrderBy((DateTime? o) => o.Value).ToList();
					foreach (DateTime? item3 in useTimeSelected)
					{
						if (item3.HasValue && item3.Value != DateTime.MinValue)
						{
							text2 = text2 + ((num2 == 0) ? "" : "; ") + item3.Value.ToString("dd/MM");
							num2++;
						}
					}
					if (targetTextBox.Text != text2)
					{
						isInitUcDate = true;
						timeSelested = time;
						timeInstructionTime.EditValue = timeSelested.ToString("HH:mm");
						targetTextBox.Text = text2;
						isInitUcDate = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void txtNgayDuTruTime_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (e.Button.Kind == ButtonPredefines.Glyph)
			{
				timeSelested = DateTime.Today.Add((TimeSpan)timeInstructionTime.EditValue);
				frmMultiIntructonTime frmMultiIntructonTime2 = new frmMultiIntructonTime(useTimeSelected, timeSelested, delegate(List<DateTime?> datas, DateTime time)
				{
					SelectMultiIntructionTime(datas, time, txtNgayDuTruTime, false);
				}, "Ngay dự trù", moduleData, treatment);
				frmMultiIntructonTime2.ShowDialog();
			}
			else if (txtNgayDuTruTime.EditValue != null && e.Button.Kind == ButtonPredefines.Delete)
			{
				txtNgayDuTruTime.EditValue = null;
			}
		}

		private void txtNgayYLenh_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtNgayYLenh.Text))
			{
				dxErrorProvider1.SetError(txtNgayYLenh, string.Empty);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HIS.Desktop.Plugins.SurgServiceReqExecute.FormSurgAssignAndCopy.frmSurgAssignAndCopy));
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
			this.layoutControlRoot = new DevExpress.XtraLayout.LayoutControl();
			this.lblThongBao = new System.Windows.Forms.Label();
			this.txtNgayYLenh = new DevExpress.XtraEditors.ButtonEdit();
			this.txtNgayDuTruTime = new DevExpress.XtraEditors.ButtonEdit();
			this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
			this.calendarInstructionDate = new DevExpress.XtraEditors.Controls.CalendarControl();
			this.timeInstructionTime = new DevExpress.XtraEditors.TimeSpanEdit();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lciCalendarInstructionDate = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
			this.dxValidationProvider2 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			((System.ComponentModel.ISupportInitialize)this.layoutControlRoot).BeginInit();
			this.layoutControlRoot.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.txtNgayYLenh.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtNgayDuTruTime.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.calendarInstructionDate.CalendarTimeProperties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.timeInstructionTime.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciCalendarInstructionDate).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dxErrorProvider1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider2).BeginInit();
			base.SuspendLayout();
			this.layoutControlRoot.Controls.Add(this.lblThongBao);
			this.layoutControlRoot.Controls.Add(this.txtNgayYLenh);
			this.layoutControlRoot.Controls.Add(this.txtNgayDuTruTime);
			this.layoutControlRoot.Controls.Add(this.btnSelect);
			this.layoutControlRoot.Controls.Add(this.calendarInstructionDate);
			this.layoutControlRoot.Controls.Add(this.timeInstructionTime);
			this.layoutControlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControlRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlRoot.Name = "layoutControlRoot";
			this.layoutControlRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(466, 153, 250, 350);
			this.layoutControlRoot.Root = this.Root;
			this.layoutControlRoot.Size = new System.Drawing.Size(383, 414);
			this.layoutControlRoot.TabIndex = 4;
			this.layoutControlRoot.Text = "layoutControl1";
			this.lblThongBao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.lblThongBao.ForeColor = System.Drawing.Color.Maroon;
			this.lblThongBao.Location = new System.Drawing.Point(12, 345);
			this.lblThongBao.Name = "lblThongBao";
			this.lblThongBao.Size = new System.Drawing.Size(359, 57);
			this.lblThongBao.TabIndex = 14;
			this.lblThongBao.Text = "Chức năng này sao chép toàn bộ nội dung xử lý của dich vụ hiện tại cho y lệnh mới được chỉ định, Do đó thời gian xử lý sẽ lâu. Vui lòng chờ.";
			this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.txtNgayYLenh.Location = new System.Drawing.Point(127, 36);
			this.txtNgayYLenh.Name = "txtNgayYLenh";
			this.txtNgayYLenh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[2]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("txtNgayYLenh.Properties.Buttons"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, true),
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("txtNgayYLenh.Properties.Buttons1"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, true)
			});
			this.txtNgayYLenh.Size = new System.Drawing.Size(244, 22);
			this.txtNgayYLenh.StyleController = this.layoutControlRoot;
			this.txtNgayYLenh.TabIndex = 12;
			this.txtNgayYLenh.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(txtNgayYLenh_ButtonClick);
			this.txtNgayYLenh.TextChanged += new System.EventHandler(txtNgayYLenh_TextChanged);
			this.txtNgayDuTruTime.Location = new System.Drawing.Point(127, 62);
			this.txtNgayDuTruTime.Name = "txtNgayDuTruTime";
			this.txtNgayDuTruTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[2]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("txtNgayDuTruTime.Properties.Buttons"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance3, appearanceHovered3, appearancePressed3, appearanceDisabled3, "", null, null, true),
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("txtNgayDuTruTime.Properties.Buttons1"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance4, appearanceHovered4, appearancePressed4, appearanceDisabled4, "", null, null, true)
			});
			this.txtNgayDuTruTime.Size = new System.Drawing.Size(244, 22);
			this.txtNgayDuTruTime.StyleController = this.layoutControlRoot;
			this.txtNgayDuTruTime.TabIndex = 11;
			this.txtNgayDuTruTime.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(txtNgayDuTruTime_ButtonClick);
			this.btnSelect.Location = new System.Drawing.Point(264, 319);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(107, 22);
			this.btnSelect.StyleController = this.layoutControlRoot;
			this.btnSelect.TabIndex = 9;
			this.btnSelect.Text = "Chọn";
			this.btnSelect.Click += new System.EventHandler(btnSelect_Click);
			this.calendarInstructionDate.AutoSize = false;
			this.calendarInstructionDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.calendarInstructionDate.Location = new System.Drawing.Point(127, 88);
			this.calendarInstructionDate.Name = "calendarInstructionDate";
			this.calendarInstructionDate.Size = new System.Drawing.Size(244, 227);
			this.calendarInstructionDate.StyleController = this.layoutControlRoot;
			this.calendarInstructionDate.TabIndex = 8;
			this.timeInstructionTime.EditValue = System.TimeSpan.Parse("738363.00:00:00");
			this.timeInstructionTime.Location = new System.Drawing.Point(127, 12);
			this.timeInstructionTime.Name = "timeInstructionTime";
			this.timeInstructionTime.Properties.AllowEditDays = false;
			this.timeInstructionTime.Properties.AllowEditSeconds = false;
			this.timeInstructionTime.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.timeInstructionTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.timeInstructionTime.Properties.DisplayFormat.FormatString = "HH:mm";
			this.timeInstructionTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.timeInstructionTime.Properties.Mask.EditMask = "HH:mm";
			this.timeInstructionTime.Properties.Mask.UseMaskAsDisplayFormat = true;
			this.timeInstructionTime.Size = new System.Drawing.Size(244, 20);
			this.timeInstructionTime.StyleController = this.layoutControlRoot;
			this.timeInstructionTime.TabIndex = 5;
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[7] { this.layoutControlItem2, this.emptySpaceItem1, this.lciCalendarInstructionDate, this.layoutControlItem6, this.layoutControlItem3, this.layoutControlItem1, this.layoutControlItem5 });
			this.Root.Location = new System.Drawing.Point(0, 0);
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(383, 414);
			this.Root.TextVisible = false;
			this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
			this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem2.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.layoutControlItem2.Control = this.timeInstructionTime;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(363, 24);
			this.layoutControlItem2.Text = "Giờ y lệnh:";
			this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(110, 20);
			this.layoutControlItem2.TextToControlDistance = 5;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 307);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(252, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.lciCalendarInstructionDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
			this.lciCalendarInstructionDate.AppearanceItemCaption.Options.UseForeColor = true;
			this.lciCalendarInstructionDate.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciCalendarInstructionDate.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciCalendarInstructionDate.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.lciCalendarInstructionDate.Control = this.calendarInstructionDate;
			this.lciCalendarInstructionDate.Location = new System.Drawing.Point(0, 76);
			this.lciCalendarInstructionDate.Name = "lciCalendarInstructionDate";
			this.lciCalendarInstructionDate.Size = new System.Drawing.Size(363, 231);
			this.lciCalendarInstructionDate.Text = "Ngày y lệnh:";
			this.lciCalendarInstructionDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.lciCalendarInstructionDate.TextSize = new System.Drawing.Size(110, 20);
			this.lciCalendarInstructionDate.TextToControlDistance = 5;
			this.layoutControlItem6.Control = this.btnSelect;
			this.layoutControlItem6.Location = new System.Drawing.Point(252, 307);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Size = new System.Drawing.Size(111, 26);
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem3.Control = this.txtNgayDuTruTime;
			this.layoutControlItem3.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 50);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(363, 26);
			this.layoutControlItem3.Text = "Ngày dự trù:";
			this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(110, 20);
			this.layoutControlItem3.TextToControlDistance = 5;
			this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkRed;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem1.Control = this.txtNgayYLenh;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(363, 26);
			this.layoutControlItem1.Text = "Ngày y lệnh:";
			this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem1.TextSize = new System.Drawing.Size(110, 20);
			this.layoutControlItem1.TextToControlDistance = 5;
			this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.layoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
			this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem5.Control = this.lblThongBao;
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 333);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(363, 61);
			this.layoutControlItem5.Text = "Thông báo:";
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			this.dxErrorProvider1.ContainerControl = this;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(383, 414);
			base.Controls.Add(this.layoutControlRoot);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSurgAssignAndCopy";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thời gian chỉ định";
			base.Load += new System.EventHandler(frmSurgAssignAndCopy_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControlRoot).EndInit();
			this.layoutControlRoot.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.txtNgayYLenh.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtNgayDuTruTime.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.calendarInstructionDate.CalendarTimeProperties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.timeInstructionTime.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Root).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciCalendarInstructionDate).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dxErrorProvider1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider2).EndInit();
			base.ResumeLayout(false);
		}
	}
}
