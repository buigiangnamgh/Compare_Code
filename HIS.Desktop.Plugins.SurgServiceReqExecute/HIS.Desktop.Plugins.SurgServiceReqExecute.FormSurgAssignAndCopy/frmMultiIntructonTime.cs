using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Config;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using Inventec.Common.Adapter;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Modules;
using MOS.EFMODEL.DataModels;
using MOS.Filter;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.FormSurgAssignAndCopy
{
	public class frmMultiIntructonTime : Form
	{
		internal static bool IsCheckDepartmentInTimeWhenPresOrAssign;

		private List<HIS_DEPARTMENT_TRAN> ListDepartmentTranCheckTime = null;

		private List<HIS_CO_TREATMENT> ListCoTreatmentCheckTime = null;

		private V_HIS_TREATMENT treatment;

		private DelegateSelectMultiDate delegateSelectData;

		private List<DateTime?> oldDatas;

		private Inventec.Desktop.Common.Modules.Module moduleData;

		private DateTime timeSelested;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private LabelControl lblTimeInput;

		private TimeSpanEdit timeIntruction;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private CalendarControl calendarIntructionTime;

		private LabelControl lblCalendaInput;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem4;

		private SimpleButton btnChoose;

		private LayoutControlItem layoutControlItem5;

		private EmptySpaceItem emptySpaceItem1;

		public string CallerButton { get; set; }

		public frmMultiIntructonTime()
		{
			InitializeComponent();
			SetCaptionByLanguageKey();
		}

		public frmMultiIntructonTime(List<DateTime?> datas, DateTime time, DelegateSelectMultiDate selectData, string caller, Inventec.Desktop.Common.Modules.Module moduleData, V_HIS_TREATMENT treatment)
		{
			try
			{
				InitializeComponent();
				HisConfigCFG.LoadConfig();
				delegateSelectData = selectData;
				oldDatas = datas;
				timeSelested = time;
				CallerButton = caller;
				this.moduleData = moduleData;
				this.treatment = treatment;
				SetCaptionByLanguageKeyNew();
				string filePath = Path.Combine(ApplicationStoreLocation.ApplicationStartupPath, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
				base.Icon = Icon.ExtractAssociatedIcon(filePath);
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
				ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(frmMultiIntructonTime).Assembly);
				if (CallerButton == "Ngay y lệnh")
				{
					layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.layoutControl1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
					btnChoose.Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.btnChoose.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
					lblCalendaInput.Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.lblCalendaInput.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
					lblTimeInput.Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.lblTimeInput.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
					Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				}
				else if (CallerButton == "Ngay dự trù")
				{
					layoutControl1.Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.layoutControl1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
					btnChoose.Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.btnChoose.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
					lblCalendaInput.Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.lblCalendaInputDT.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
					lblTimeInput.Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTime.lblTimeInputDT.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
					Text = Inventec.Common.Resource.Get.Value("frmMultiIntructonTimeDT.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				}
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
				Text = Inventec.Common.Resource.Get.Value("AssignService.lciTimeAssign.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				lblTimeInput.Text = Inventec.Common.Resource.Get.Value("FormMultiChooseDate__CaptionTimeInput", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				lblCalendaInput.Text = Inventec.Common.Resource.Get.Value("FormMultiChooseDate__CaptionCalendaInput", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnChoose.Text = Inventec.Common.Resource.Get.Value("FormMultiChooseDate__CaptionBtnChoose", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void frmMultiIntructonTime1_Load(object sender, EventArgs e)
		{
			try
			{
				if (oldDatas != null && oldDatas.Count > 0)
				{
					foreach (DateTime? oldData in oldDatas)
					{
						if (oldData.HasValue && oldData.Value != DateTime.MinValue)
						{
							calendarIntructionTime.AddSelection(oldData.Value);
						}
					}
				}
				if (timeSelested != DateTime.MinValue)
				{
					timeIntruction.EditValue = timeSelested.ToString("HH:mm");
				}
				else
				{
					timeIntruction.EditValue = DateTime.Now.ToString("HH:mm");
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnChooose_Click(object sender, EventArgs e)
		{
			try
			{
				WaitingManager.Show();
				bool flag = false;
				List<DateTime?> list = new List<DateTime?>();
				foreach (DateRange selectedRange in calendarIntructionTime.SelectedRanges)
				{
					if (selectedRange != null)
					{
						DateTime value = selectedRange.StartDate;
						while (value.Date < selectedRange.EndDate.Date)
						{
							flag = true;
							list.Add(value);
							value = value.AddDays(1.0);
						}
					}
				}
				WaitingManager.Hide();
				if (flag)
				{
					DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 2);
					DateTime dateTime2 = dateTime.Add(timeIntruction.TimeSpan);
					timeSelested = dateTime2;
					if (delegateSelectData != null)
					{
						delegateSelectData(list, timeSelested);
					}
					Close();
				}
				else
				{
					MessageManager.Show(ResourceMessage.ChuaChonNgayChiDinh);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnChoose_Click(object sender, EventArgs e)
		{
			try
			{
				WaitingManager.Show();
				bool flag = false;
				List<long> list = new List<long>();
				List<DateTime?> list2 = new List<DateTime?>();
				foreach (DateRange selectedRange in calendarIntructionTime.SelectedRanges)
				{
					if (selectedRange != null)
					{
						DateTime value = selectedRange.StartDate;
						while (value.Date < selectedRange.EndDate.Date)
						{
							flag = true;
							list2.Add(value);
							long item = long.Parse(value.Date.Add(timeIntruction.TimeSpan).ToString("yyyyMMddHHmmss"));
							list.Add(item);
							value = value.AddDays(1.0);
						}
					}
				}
				if (flag)
				{
					if (HisConfigCFG.IsCheckDepartmentInTimeWhenPresOrAssign)
					{
						WaitingManager.Hide();
						if (!CheckTimeInDepartment(list))
						{
							return;
						}
					}
					DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 2);
					DateTime dateTime2 = dateTime.Add(timeIntruction.TimeSpan);
					timeSelested = dateTime2;
					if (delegateSelectData != null)
					{
						delegateSelectData(list2, timeSelested);
					}
					WaitingManager.Hide();
					Close();
				}
				else
				{
					WaitingManager.Hide();
					MessageManager.Show(ResourceMessage.ChuaChonNgayChiDinh);
				}
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private bool CheckTimeInDepartment(List<long> listTime)
		{
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Expected O, but got Unknown
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Expected O, but got Unknown
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			bool result = true;
			try
			{
				V_HIS_ROOM currentWorkingRoom = null;
				currentWorkingRoom = BackendDataWorker.Get<V_HIS_ROOM>().First((V_HIS_ROOM o) => o.ID == moduleData.RoomId);
				CommonParam val = new CommonParam();
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<List<long>>((Expression<Func<List<long>>>)(() => listTime)), (object)listTime));
				HisDepartmentTranFilter val2 = new HisDepartmentTranFilter();
				val2.TREATMENT_ID = treatment.ID;
				ListDepartmentTranCheckTime = ((AdapterBase)new BackendAdapter(val)).Get<List<HIS_DEPARTMENT_TRAN>>("api/HisDepartmentTran/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val2, (CommonParam)null);
				List<HIS_DEPARTMENT_TRAN> list = null;
				if (ListDepartmentTranCheckTime != null && ListDepartmentTranCheckTime.Count > 0)
				{
					list = ListDepartmentTranCheckTime.Where((HIS_DEPARTMENT_TRAN o) => o.DEPARTMENT_ID == currentWorkingRoom.DEPARTMENT_ID && o.DEPARTMENT_IN_TIME.HasValue).ToList();
				}
				List<HIS_CO_TREATMENT> list2 = null;
				HisCoTreatmentFilter val3 = new HisCoTreatmentFilter();
				val3.TDL_TREATMENT_ID = treatment.ID;
				ListCoTreatmentCheckTime = ((AdapterBase)new BackendAdapter(val)).Get<List<HIS_CO_TREATMENT>>("api/HisCoTreatment/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val2, (CommonParam)null);
				if (ListCoTreatmentCheckTime != null && ListCoTreatmentCheckTime.Count > 0)
				{
					list2 = ListCoTreatmentCheckTime.Where((HIS_CO_TREATMENT o) => o.DEPARTMENT_ID == currentWorkingRoom.DEPARTMENT_ID && o.START_TIME.HasValue).ToList();
				}
				foreach (long item2 in listTime)
				{
					bool flag = false;
					List<string> list3 = new List<string>();
					if (list != null && list.Count > 0)
					{
						list = list.OrderBy((HIS_DEPARTMENT_TRAN o) => o.DEPARTMENT_IN_TIME.GetValueOrDefault()).ToList();
						long num = 0L;
						long num2 = 0L;
						foreach (HIS_DEPARTMENT_TRAN item in list)
						{
							num = item.DEPARTMENT_IN_TIME.GetValueOrDefault();
							num2 = long.MaxValue;
							HIS_DEPARTMENT_TRAN val4 = ListDepartmentTranCheckTime.FirstOrDefault((HIS_DEPARTMENT_TRAN o) => o.PREVIOUS_ID == item.ID);
							if (val4 != null)
							{
								num2 = val4.DEPARTMENT_IN_TIME ?? long.MaxValue;
							}
							flag = flag || (num <= item2 && item2 <= num2);
                            list3.Add(string.Format("từ {0}{1}", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(num), (num2 > 0 && num2 != long.MaxValue) ? (" đến " + Inventec.Common.DateTime.Convert.TimeNumberToTimeString(num2)) : ""));
						}
					}
					if (!flag && list3.Count > 0 && list2 != null && list2.Count > 0)
					{
						list3.Clear();
					}
					if (!flag && list2 != null && list2.Count > 0)
					{
						list2 = list2.OrderBy((HIS_CO_TREATMENT o) => o.START_TIME.GetValueOrDefault()).ToList();
						long num3 = 0L;
						long num4 = 0L;
						foreach (HIS_CO_TREATMENT item3 in list2)
						{
							num3 = item3.START_TIME.GetValueOrDefault();
							num4 = item3.FINISH_TIME ?? long.MaxValue;
							flag = flag || (num3 <= item2 && item2 <= num4);
							list3.Add(string.Format("từ {0}{1}", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(num3), (num4 > 0 && num4 != long.MaxValue) ? (" đến " + Inventec.Common.DateTime.Convert.TimeNumberToTimeString(num4)) : ""));
						}
					}
					if (!flag)
					{
						XtraMessageBox.Show("Thời gian y lệnh phải nằm trong thời gian bệnh nhân hiện diện tại khoa", "Thông báo");
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				result = false;
				LogSystem.Error(ex);
			}
			return result;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HIS.Desktop.Plugins.SurgServiceReqExecute.FormSurgAssignAndCopy.frmMultiIntructonTime));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnChoose = new DevExpress.XtraEditors.SimpleButton();
			this.calendarIntructionTime = new DevExpress.XtraEditors.Controls.CalendarControl();
			this.lblCalendaInput = new DevExpress.XtraEditors.LabelControl();
			this.lblTimeInput = new DevExpress.XtraEditors.LabelControl();
			this.timeIntruction = new DevExpress.XtraEditors.TimeSpanEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.calendarIntructionTime.CalendarTimeProperties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.timeIntruction.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.btnChoose);
			this.layoutControl1.Controls.Add(this.calendarIntructionTime);
			this.layoutControl1.Controls.Add(this.lblCalendaInput);
			this.layoutControl1.Controls.Add(this.lblTimeInput);
			this.layoutControl1.Controls.Add(this.timeIntruction);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(402, 125, 250, 350);
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(348, 326);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.btnChoose.Location = new System.Drawing.Point(250, 267);
			this.btnChoose.Name = "btnChoose";
			this.btnChoose.Size = new System.Drawing.Size(86, 22);
			this.btnChoose.StyleController = this.layoutControl1;
			this.btnChoose.TabIndex = 10;
			this.btnChoose.Text = "Chọn";
			this.btnChoose.Click += new System.EventHandler(btnChoose_Click);
			this.calendarIntructionTime.AutoSize = false;
			this.calendarIntructionTime.CalendarAppearance.DayCellHighlighted.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.calendarIntructionTime.CalendarAppearance.DayCellHighlighted.Options.UseFont = true;
			this.calendarIntructionTime.CalendarAppearance.DayCellSelected.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.calendarIntructionTime.CalendarAppearance.DayCellSelected.Options.UseFont = true;
			this.calendarIntructionTime.CalendarAppearance.DayCellSpecialSelected.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.calendarIntructionTime.CalendarAppearance.DayCellSpecialSelected.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
			this.calendarIntructionTime.CalendarAppearance.DayCellSpecialSelected.Options.UseFont = true;
			this.calendarIntructionTime.CalendarAppearance.DayCellSpecialSelected.Options.UseForeColor = true;
			this.calendarIntructionTime.CalendarTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.calendarIntructionTime.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.calendarIntructionTime.DateTime = new System.DateTime(0L);
			this.calendarIntructionTime.EditValue = null;
			this.calendarIntructionTime.Location = new System.Drawing.Point(103, 36);
			this.calendarIntructionTime.Name = "calendarIntructionTime";
			this.calendarIntructionTime.SelectionBehavior = DevExpress.XtraEditors.Controls.CalendarSelectionBehavior.OutlookStyle;
			this.calendarIntructionTime.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Multiple;
			this.calendarIntructionTime.Size = new System.Drawing.Size(233, 227);
			this.calendarIntructionTime.StyleController = this.layoutControl1;
			this.calendarIntructionTime.TabIndex = 9;
			this.lblCalendaInput.Appearance.ForeColor = System.Drawing.Color.Maroon;
			this.lblCalendaInput.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lblCalendaInput.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblCalendaInput.Location = new System.Drawing.Point(12, 36);
			this.lblCalendaInput.Name = "lblCalendaInput";
			this.lblCalendaInput.Size = new System.Drawing.Size(87, 13);
			this.lblCalendaInput.StyleController = this.layoutControl1;
			this.lblCalendaInput.TabIndex = 8;
			this.lblCalendaInput.Text = "Ngày y lệnh:";
			this.lblTimeInput.Appearance.ForeColor = System.Drawing.Color.Maroon;
			this.lblTimeInput.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lblTimeInput.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblTimeInput.Location = new System.Drawing.Point(12, 12);
			this.lblTimeInput.Name = "lblTimeInput";
			this.lblTimeInput.Size = new System.Drawing.Size(87, 13);
			this.lblTimeInput.StyleController = this.layoutControl1;
			this.lblTimeInput.TabIndex = 7;
			this.lblTimeInput.Text = "Giờ y lệnh:";
			this.timeIntruction.EditValue = System.TimeSpan.Parse("00:00:00");
			this.timeIntruction.Location = new System.Drawing.Point(103, 12);
			this.timeIntruction.Name = "timeIntruction";
			this.timeIntruction.Properties.AllowEditDays = false;
			this.timeIntruction.Properties.AllowEditSeconds = false;
			this.timeIntruction.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.timeIntruction.Properties.DisplayFormat.FormatString = "HH:mm";
			this.timeIntruction.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.timeIntruction.Properties.Mask.EditMask = "HH:mm";
			this.timeIntruction.Properties.Mask.UseMaskAsDisplayFormat = true;
			this.timeIntruction.Size = new System.Drawing.Size(233, 20);
			this.timeIntruction.StyleController = this.layoutControl1;
			this.timeIntruction.TabIndex = 6;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[6] { this.layoutControlItem1, this.layoutControlItem2, this.layoutControlItem3, this.layoutControlItem4, this.layoutControlItem5, this.emptySpaceItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Size = new System.Drawing.Size(348, 326);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.lblTimeInput;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(91, 24);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem2.Control = this.timeIntruction;
			this.layoutControlItem2.Location = new System.Drawing.Point(91, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(237, 24);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.layoutControlItem3.Control = this.calendarIntructionTime;
			this.layoutControlItem3.Location = new System.Drawing.Point(91, 24);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(237, 231);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			this.layoutControlItem4.Control = this.lblCalendaInput;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(91, 231);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			this.layoutControlItem5.Control = this.btnChoose;
			this.layoutControlItem5.Location = new System.Drawing.Point(238, 255);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(90, 51);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 255);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(238, 51);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(348, 326);
			base.Controls.Add(this.layoutControl1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmMultiIntructonTime";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chọn nhiều ngày y lệnh";
			base.Load += new System.EventHandler(frmMultiIntructonTime1_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.calendarIntructionTime.CalendarTimeProperties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.timeIntruction.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
