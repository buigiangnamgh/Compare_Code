using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CARD.WCF.Service.TapCardService;
using CPA.WCFClient.CallPatientClient;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using His.Bhyt.InsuranceExpertise.LDO;
using HIS.Desktop.ADO;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Common;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.DelegateRegister;
using HIS.Desktop.Library.CacheClient;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.ConfigSystem;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.ModuleExt;
using HIS.Desktop.Plugins.Library.CheckHeinGOV;
using HIS.Desktop.Plugins.Library.EmrGenerate;
using HIS.Desktop.Plugins.Library.MedicalExpenseGuarantee;
using HIS.Desktop.Plugins.Library.MedicalExpenseGuarantee.ADO;
using HIS.Desktop.Plugins.Library.PrintServiceReq;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using HIS.Desktop.Plugins.RegisterV2.Choice;
using HIS.Desktop.Plugins.RegisterV2.Process;
using HIS.Desktop.Plugins.RegisterV2.Register;
using HIS.Desktop.Plugins.RegisterV2.Resources;
using HIS.Desktop.Plugins.RegisterV2.RunV3;
using HIS.Desktop.Utility;
using HIS.UC.AddressCombo;
using HIS.UC.AddressCombo.ADO;
using HIS.UC.KskContract;
using HIS.UC.KskContract.ADO;
using HIS.UC.PlusInfo;
using HIS.UC.PlusInfo.ADO;
using HIS.UC.ServiceRoom;
using HIS.UC.UCCheckTT;
using His.UC.UCHein;
using His.UC.UCHein.Data;
using HIS.UC.UCHeniInfo;
using HIS.UC.UCHeniInfo.Data;
using HIS.UC.UCImageInfo;
using HIS.UC.UCImageInfo.ADO;
using HIS.UC.UCImageInfo.Base;
using HIS.UC.UCOtherServiceReqInfo;
using HIS.UC.UCOtherServiceReqInfo.ADO;
using HIS.UC.UCPatientRaw;
using HIS.UC.UCPatientRaw.ADO;
using HIS.UC.UCPatientRaw.Base;
using HIS.UC.UCRelativeInfo;
using HIS.UC.UCRelativeInfo.ADO;
using HIS.UC.UCServiceRoomInfo;
using HIS.UC.UCServiceRoomInfo.ADO;
using HIS.UC.UCTransPati.ADO;
using HIS.UC.WorkPlace;
using Inventec.Common.Adapter;
using Inventec.Common.Address;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Common.Controls.PopupLoader;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Common.Mapper;
using Inventec.Common.QrCodeBHYT;
using Inventec.Common.Resource;
using Inventec.Common.RichEditor;
using Inventec.Common.RichEditor.Base;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.String;
using Inventec.Common.TypeConvert;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.LibraryMessage;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Modules;
using Inventec.Speech;
using Inventec.UC.Login.Base;
using Microsoft.CSharp.RuntimeBinder;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Bhyt.HeinLiveArea;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.SDO;
using MPS;
using MPS.Processor.Mps000111.PDO;
using MPS.Processor.Mps000178.PDO;
using MPS.Processor.Mps000309.PDO;
using MPS.Processor.Mps000358.PDO;
using MPS.Processor.Mps000420.PDO;
using MPS.ProcessorBase;
using MPS.ProcessorBase.Core;
using SDA.EFMODEL.DataModels;
using SDA.SDO;

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
	public class UCRegister : UserControlBase
	{
		private enum PrintType
		{
			InDvKham = 0,
			InTheBenhNhan = 1,
			InPhieuYeuCauKham = 2,
			BangKiemTruocTiemChung = 3,
			InBienLaiHoaDon = 4
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass455_0
		{
			public List<long> roomIds;

			internal bool _003CSetDefaultCashierRoom_003Eb__0(HIS_CASHIER_ROOM o)
			{
				return roomIds.Contains(o.ROOM_ID);
			}
		}

		[CompilerGenerated]
		private static class _003C_003Eo__455
		{
			public static CallSite<Func<CallSite, BackendAdapter, string, Inventec.Common.WebApiClient.ApiConsumer, object, CommonParam, object>> _003C_003Ep__0;

			public static CallSite<Func<CallSite, object, List<HIS_CASHIER_ROOM>>> _003C_003Ep__1;
		}

		[CompilerGenerated]
		private sealed class _003CSetDefaultCashierRoom_003Ed__455 : IAsyncStateMachine
		{
			private static class _003C_003Eo__455
			{
				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__0;

				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__1;

				public static CallSite<Func<CallSite, object, bool>> _003C_003Ep__2;

				public static CallSite<Func<CallSite, object, object>> _003C_003Ep__3;
			}

			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public UCRegister _003C_003E4__this;

			private _003C_003Ec__DisplayClass455_0 _003C_003E8__1;

			private List<HIS_CASHIER_ROOM> _003ClistCashier_003E5__2;

			private CommonParam _003CparamCommon_003E5__3;

			private object _003Cfilter_003E5__4;

			private Func<CallSite, object, List<HIS_CASHIER_ROOM>> _003C_003Es__5;

			private CallSite<Func<CallSite, object, List<HIS_CASHIER_ROOM>>> _003C_003Es__6;

			private object _003C_003Es__7;

			private Exception _003Cex_003E5__8;

			private object _003C_003Eu__1;

			private void MoveNext()
			{
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
							_003C_003E8__1 = new _003C_003Ec__DisplayClass455_0();
							_003ClistCashier_003E5__2 = new List<HIS_CASHIER_ROOM>();
							if (BackendDataWorker.IsExistsKey<HIS_CASHIER_ROOM>())
							{
								_003ClistCashier_003E5__2 = BackendDataWorker.Get<HIS_CASHIER_ROOM>();
								goto IL_036c;
							}
							_003CparamCommon_003E5__3 = new CommonParam();
							_003Cfilter_003E5__4 = new ExpandoObject();
							if (UCRegister._003C_003Eo__455._003C_003Ep__1 == null)
							{
								UCRegister._003C_003Eo__455._003C_003Ep__1 = CallSite<Func<CallSite, object, List<HIS_CASHIER_ROOM>>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(List<HIS_CASHIER_ROOM>), typeof(UCRegister)));
							}
							_003C_003Es__5 = UCRegister._003C_003Eo__455._003C_003Ep__1.Target;
							_003C_003Es__6 = UCRegister._003C_003Eo__455._003C_003Ep__1;
							val = new BackendAdapter(_003CparamCommon_003E5__3).GetAsync<List<HIS_CASHIER_ROOM>>("api/HisCashierRoom/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (dynamic)_003Cfilter_003E5__4, _003CparamCommon_003E5__3).GetAwaiter();
							if (!(bool)val.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = val;
								ICriticalNotifyCompletion awaiter = val as ICriticalNotifyCompletion;
								_003CSetDefaultCashierRoom_003Ed__455 stateMachine = this;
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
						_003C_003Es__7 = val.GetResult();
						_003ClistCashier_003E5__2 = _003C_003Es__5(_003C_003Es__6, _003C_003Es__7);
						_003C_003Es__5 = null;
						_003C_003Es__6 = null;
						_003C_003Es__7 = null;
						if (_003ClistCashier_003E5__2 != null)
						{
							BackendDataWorker.UpdateToRam(typeof(HIS_CASHIER_ROOM), _003ClistCashier_003E5__2, long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
						}
						_003CparamCommon_003E5__3 = null;
						_003Cfilter_003E5__4 = null;
						goto IL_036c;
						IL_036c:
						_003C_003E8__1.roomIds = WorkPlace.GetRoomIds();
						if (_003C_003E8__1.roomIds == null || _003C_003E8__1.roomIds.Count == 0)
						{
							throw new ArgumentNullException("Nguoi dung khong chon phong thu ngan nao");
						}
						_003ClistCashier_003E5__2 = _003ClistCashier_003E5__2.Where((HIS_CASHIER_ROOM o) => _003C_003E8__1.roomIds.Contains(o.ROOM_ID)).ToList();
						_003C_003E4__this.InitComboCommon(_003C_003E4__this.cboCashierRoom, _003ClistCashier_003E5__2, "ID", "CASHIER_ROOM_NAME", "CASHIER_ROOM_CODE");
						if (_003ClistCashier_003E5__2 != null && _003ClistCashier_003E5__2.Count == 1)
						{
							_003C_003E4__this.cboCashierRoom.EditValue = _003ClistCashier_003E5__2.First().ID;
						}
						_003C_003E8__1 = null;
						_003ClistCashier_003E5__2 = null;
					}
					catch (Exception ex)
					{
						_003Cex_003E5__8 = ex;
						LogSystem.Error(_003Cex_003E5__8);
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

		internal HisCardSDO cardSearch = null;

		internal UCTransPatiADO transPatiADO = null;

		internal List<ServiceReqDetailSDO> serviceReqDetailSDOs;

		internal Inventec.Desktop.Common.Modules.Module currentModule;

		internal KskContractProcessor kskContractProcessor;

		internal UserControl ucKskContract;

		internal int registerNumber = 0;

		internal bool isShowMess;

		internal string lastSavedTreatmentCode = null;

		private const string IsDefaultRightRouteType__True = "1";

		private List<HIS_PATIENT_TYPE> currentPatientTypeAllowByPatientType;

		internal UserControl ucHeinBHYT;

		internal MainHisHeinBhyt mainHeinProcessor;

		private RoomExamServiceProcessor roomExamServiceProcessor;

		private UCAddressADO dataAddressPatient = new UCAddressADO();

		private UCPatientRawADO dataPatientRaw = new UCPatientRawADO();

		private CallPatientClientManager clienttManager = null;

		private frmTransPati frm;

		private HisPatientProfileSDO resultHisPatientProfileSDO = null;

		internal bool isNotPatientDayDob = false;

		private int actionType = 0;

		private bool isPrintNow;

		private bool isReadQrCode = true;

		private string appointmentCode = "";

		private long _TreatmnetIdByAppointmentCode = 0L;

		private bool _isPatientAppointmentCode = false;

		private bool isResetForm = false;

		private bool isNotLoadWhileChangeControlStateInFirst;

		private const string moduleLink = "HIS.Desktop.Plugins.RegisterV2";

		private const string moduleLinkConfigCall = "HIS.Desktop.Plugins.RegisterV2.frmConfigCall";

		private const long PRIORITY_TRUE = 1L;

		private ControlStateWorker controlStateWorker;

		private List<ControlStateRDO> currentControlStateRDO;

		private List<ControlStateRDO> currentControlStateRDOModuleLink;

		private string CallConfigString = "";

		private bool isNotCheckTT = false;

		private bool _IsDungTuyenCapCuuByTime = false;

		private bool isSaveWithRoomHasConfigAllowNotChooseService = false;

		private long roomId;

		private string numSttNow = "0";

		private string numTotal;

		private string txtNumberPer = "";

		private string baseNameControl = "";

		private bool IsEmergency = false;

		private List<V_HIS_SERVICE> lstService;

		private bool IsActionSavePrint = false;

		internal string GuarateeCode = null;

		internal string GuaranteeRequestCode = null;

		private bool IsCheckSave = false;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControl layoutControl2;

		private LayoutControl layoutControl3;

		private LayoutControlGroup Root;

		private LayoutControlGroup layoutControlGroup2;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private SimpleButton btnRecallPatient;

		private SimpleButton btnCallPatient;

		private ButtonEdit txtGateNumber;

		private TextEdit txtStepNumber;

		internal LookUpEdit cboCashierRoom;

		internal SimpleButton btnSaveAndPrint;

		internal SimpleButton btnPatientNew;

		internal SimpleButton btnSaveAndAssain;

		internal SimpleButton btnDepositDetail;

		internal SimpleButton btnTreatmentBedRoom;

		internal SimpleButton btnNewContinue;

		internal SimpleButton btnPrint;

		internal SimpleButton btnSave;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem4;

		private LayoutControlItem layoutControlItem5;

		private LayoutControlItem layoutControlItem6;

		private LayoutControlItem layoutControlItem8;

		private LayoutControlItem layoutControlItem9;

		private LayoutControlItem lcibtnPatientNewInfo;

		private LayoutControlItem layoutControlItem11;

		private LayoutControlItem lcibtnDepositDetail;

		private LayoutControlItem layoutControlItem13;

		private LayoutControlItem layoutControlItem14;

		private LayoutControlItem layoutControlItem15;

		private LayoutControlItem layoutControlItem16;

		private LayoutControlItem pnlServiceRoomInfomation;

		internal UCHeinInfo ucHeinInfo1;

		internal UCAddressCombo ucAddressCombo1;

		private LayoutControlItem layoutControlItem18;

		private LayoutControlItem lciUCHeinInfo;

		internal UCOtherServiceReqInfo ucOtherServiceReqInfo1;

		private LayoutControlItem layoutControlItem20;

		internal UCRelativeInfo ucRelativeInfo1;

		private LayoutControlItem layoutControlItem21;

		internal UCImageInfo ucImageInfo1;

		private LayoutControlItem layoutControlItem23;

		internal UCServiceRoomInfo ucServiceRoomInfo1;

		private LayoutControlItem lciUCServiceRoomInfo;

		internal SimpleButton btnTTChuyenTuyen;

		private LayoutControlItem layoutControlItem7;

		private LayoutControlItem layoutControlItem10;

		internal UCPlusInfo ucPlusInfo1;

		private EmptySpaceItem emptySpaceItem1;

		private DropDownButton dropDownButton__Other;

		private LayoutControlItem layoutControlItem12;

		private LabelControl lblRegisterNumOrder;

		private LayoutControlItem lciRegisterNumOrder;

		private System.Windows.Forms.Timer timerInitForm;

		internal SimpleButton btnDepositRequest;

		private LayoutControlItem lcibtnDepositRequest;

		private UCCheckTT ucCheckTT1;

		private LayoutControlItem layoutControlItem19;

		private CheckEdit chkPrintPatientCard;

		internal CheckEdit chkAutoCreateBill;

		private LayoutControl layoutControl4;

		private CheckEdit chkPrintExam;

		private LayoutControlGroup layoutControlGroup3;

		private LayoutControlItem layoutControlItem22;

		private LayoutControlItem layoutControlItem24;

		private LayoutControlItem layoutControlItem26;

		private LayoutControlItem layoutControlItem25;

		private CheckEdit chkAssignDoctor;

		private LayoutControlItem layoutControlItem27;

		private System.Windows.Forms.Timer timerRefeshAutoCreateBill;

		private LayoutControlItem lciAutoDeposit;

		internal CheckEdit chkAutoDeposit;

		internal CheckEdit chkAutoPay;

		private LayoutControlItem layoutControlItem17;

		private SimpleButton btnGiayTo;

		private LayoutControlItem layoutControlItem28;

		private TextEdit txtTo;

		private TextEdit txtFrom;

		private LayoutControlItem layoutControlItem29;

		private LayoutControlItem layoutControlItem30;

		private CheckEdit chkSignExam;

		private LayoutControlItem layoutControlItem31;

		internal UCPatientRaw ucPatientRaw1;

		internal CheckEdit chkBaoLanh;

		private LayoutControlItem layoutControlItem32;

		internal CheckEdit chkXemTruoc;

		private LayoutControlItem layoutControlItem33;

		public bool IsReadCardTheViet = false;

		public string HtCommuneCode = null;

		public string HtDistrictCode = null;

		public string HtProvinceCode = null;

		public string HtCommuneName = null;

		public string HtDistrictName = null;

		public string HtProvinceName = null;

		private string callPatientFormName = "";

		private int nFrom = 0;

		private int nTo = 0;

		private int bFrom = 0;

		private int bTo = 0;

		private List<long> RegisterReqIds = new List<long>();

		private Dictionary<string, List<HIS_REGISTER_REQ>> dicRegisterReq = new Dictionary<string, List<HIS_REGISTER_REQ>>();

		public const string CALL_PATIENT_MOI_BENH_NHAN = "EXE.CALL_PATIENT.MOI_BENH_NHAN";

		public const string CALL_PATIENT_CO_STT = "EXE.CALL_PATIENT.CO_STT";

		public const string CALL_PATIENT_DEN = "EXE.CALL_PATIENT.DEN";

		public const string CALL_PATIENT_CONG = "EXE.CALL_PATIENT.CONG";

		private List<string> KEY_SINGLE = new List<string> { "NUM_ORDER_STR", "NUM_ORDER", "GATE_NAME", "REGISTER_GATE_CODE", "REGISTER_GATE_NAME" };

		private long RegisterGateId;

		private string RegisterGateCode;

		private string gateCode = "";

		private string gateName = "";

		private bool IsFirstCallNotCPA = true;

		private int count = 0;

		public List<string> lstPreviousDebtTreatmentsRegister = new List<string>();

		private List<string> lstSend = new List<string>();

		private List<string> lst = new List<string>();

		private bool EmergencyBol = false;

		private long treatmentTypeID = 0L;

		private List<string> lstModuleLinkApply;

		private BarManager barManager = new BarManager();

		private PopupMenu menu;

		private bool isPrintNowBL = false;

		private List<V_HIS_SERVICE_REQ> ServiceReqList = new List<V_HIS_SERVICE_REQ>();

		private bool IsEnablePatientKey = false;

		private bool IsRunDelegateEnableSave = false;

		internal HisPatientSDO currentPatientSDO { get; set; }

		private int TreatmentTypeIdPicked { get; set; }

		internal List<long> serviceReqPrintIds { get; set; }

		private HisServiceReqExamRegisterResultSDO currentHisExamServiceReqResultSDO { get; set; }

		private HeinCardData _HeinCardData { get; set; }

		private ResultDataADO ResultDataADO { get; set; }

		public bool isCheckSS { get; set; }

		private bool ValidatedTTCT { get; set; }

		private bool IsPresent { get; set; }

		private bool IsPresentAndAppointment { get; set; }

		private bool isAlertTreatmentEndInDay { get; set; }

		private List<HIS_REGISTER_GATE> _RegisterGates { get; set; }

		private List<SDA_NATIONAL> lstNational { get; set; }

		public UCRegister(Inventec.Desktop.Common.Modules.Module module)
			: base(module)
		{
			try
			{
				LogSystem.Debug("UCRegister .1");
				currentModule = module;
				BackendDataWorker.Reset<HIS_PATIENT_TYPE>();
				LogSystem.Debug("UCRegister .2");
				HisConfigCFG.LoadConfig();
				LogSystem.Debug("UCRegister .3");
				AppConfigs.LoadConfig();
				LogSystem.Debug("UCRegister .4");
				InitializeComponent();
				LogSystem.Debug("UCRegister .5");
				roomId = module.RoomId;
				SetCaptionByLanguageKey();
				LoadServiceFromRam();
				LogSystem.Debug("UCRegister .6");
				base.KeyDown += new KeyEventHandler(UCRegister_KeyDown);
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
				LogSystem.Error(ex);
			}
		}

		private void UCRegister_Load(object sender, EventArgs e)
		{
			try
			{
				LogSystem.Debug("UCRegister_Load .1");
				timerInitForm.Enabled = true;
				timerInitForm.Interval = 1000;
				RegisterTimer(currentModule.ModuleLink, "timerInitForm", timerInitForm.Interval, new Action(timerInitForm_Tick));
				StartTimer(currentModule.ModuleLink, "timerInitForm");
				timerRefeshAutoCreateBill.Enabled = true;
				timerRefeshAutoCreateBill.Interval = 2000;
				RegisterTimer(currentModule.ModuleLink, "timerRefeshAutoCreateBill", timerRefeshAutoCreateBill.Interval, new Action(timerRefeshAutoCreateBill_Tick));
				StartTimer(currentModule.ModuleLink, "timerRefeshAutoCreateBill");
				LogSystem.Debug("UCRegister_Load .2");
				InitControlState();
				LogSystem.Debug("UCRegister_Load .3");
				ucPatientRaw1.LoadDataCboDoiTuong(roomId);
				ucPatientRaw1.LoadDataComboPrimaryPatientType(roomId);
				ucPatientRaw1.InitData(new EventHandler(PatientTypeEditValueChanged), new GetIntructionTime(GetIntructionTime), new DelegateSendIdData(PatientClassifyChanged));
				LogSystem.Debug("UCRegister_Load .4");
				SetDelegateForResetRegister();
				actionType = 1;
				SetDefaultRegisterForm();
				LogSystem.Debug("UCRegister_Load .5");
				CreateThreadInitWCFReadCard();
				LogSystem.Debug("UCRegister_Load .6");
				LogSystem.Debug("UCRegister_Load .7");
				LoadDefaultScreenSaver();
				if (!string.IsNullOrEmpty(HisConfigCFG.GuaranteeConnection) || HisConfigCFG.GuaranteeConnection != "")
				{
					layoutControlItem32.Visibility = LayoutVisibility.Always;
				}
				else
				{
					layoutControlItem32.Visibility = LayoutVisibility.Never;
				}
				LogSystem.Debug("UCRegister_Load .8");
				if (!string.IsNullOrEmpty(HisConfigCFG.ModuleLinkApply))
				{
					lstModuleLinkApply = HisConfigCFG.ModuleLinkApply.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
				}
				LogSystem.Debug("UCRegister_Load .9");
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
		}

		private void PatientClassifyChanged(long? PatientClassifyId)
		{
			try
			{
				if (ucServiceRoomInfo1 != null)
				{
					ucServiceRoomInfo1.SetPatientClassify(PatientClassifyId);
				}
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
				HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.RegisterV2.Resources.Lang", typeof(UCRegister).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("UCRegister.layoutControl1.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControl4.Text = Inventec.Common.Resource.Get.Value("UCRegister.layoutControl4.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkAutoPay.Properties.Caption = Inventec.Common.Resource.Get.Value("UCRegister.chkAutoPay.Properties.Caption", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkAutoPay.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.chkAutoPay.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkAutoDeposit.Properties.Caption = Inventec.Common.Resource.Get.Value("UCRegister.chkAutoDeposit.Properties.Caption", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkAutoDeposit.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.chkAutoDeposit.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkAssignDoctor.Properties.Caption = Inventec.Common.Resource.Get.Value("UCRegister.chkAssignDoctor.Properties.Caption", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkAssignDoctor.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.chkAssignDoctor.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkPrintExam.Properties.Caption = Inventec.Common.Resource.Get.Value("UCRegister.chkPrintExam.Properties.Caption", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkAutoCreateBill.Properties.Caption = Inventec.Common.Resource.Get.Value("UCRegister.chkAutoCreateBill.Properties.Caption", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkAutoCreateBill.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.chkAutoCreateBill.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkPrintPatientCard.Properties.Caption = Inventec.Common.Resource.Get.Value("UCRegister.chkPrintPatientCard.Properties.Caption", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				chkPrintPatientCard.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.chkPrintPatientCard.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnTTChuyenTuyen.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnTTChuyenTuyen.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControl3.Text = Inventec.Common.Resource.Get.Value("UCRegister.layoutControl3.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				txtTo.Properties.NullText = Inventec.Common.Resource.Get.Value("UCRegister.txtTo.Properties.NullText", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				txtFrom.Properties.NullText = Inventec.Common.Resource.Get.Value("UCRegister.txtFrom.Properties.NullText", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnGiayTo.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnGiayTo.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnDepositRequest.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnDepositRequest.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				dropDownButton__Other.Text = Inventec.Common.Resource.Get.Value("UCRegister.dropDownButton__Other.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnRecallPatient.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnRecallPatient.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnRecallPatient.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.btnRecallPatient.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnCallPatient.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnCallPatient.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				txtGateNumber.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("UCRegister.txtGateNumber.Properties.NullValuePrompt", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				txtGateNumber.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.txtGateNumber.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				txtStepNumber.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("UCRegister.txtStepNumber.Properties.NullValuePrompt", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				cboCashierRoom.Properties.NullText = Inventec.Common.Resource.Get.Value("UCRegister.cboCashierRoom.Properties.NullText", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnSaveAndPrint.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnSaveAndPrint.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnPatientNew.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnPatientNew.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnPatientNew.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.btnPatientNew.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnSaveAndAssain.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnSaveAndAssain.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnDepositDetail.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnDepositDetail.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnTreatmentBedRoom.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnTreatmentBedRoom.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnNewContinue.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnNewContinue.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnPrint.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnPrint.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnSave.Text = Inventec.Common.Resource.Get.Value("UCRegister.btnSave.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem5.OptionsToolTip.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.layoutControlItem5.OptionsToolTip.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem8.OptionsToolTip.ToolTip = Inventec.Common.Resource.Get.Value("UCRegister.layoutControlItem8.OptionsToolTip.ToolTip", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControlItem8.Text = Inventec.Common.Resource.Get.Value("UCRegister.layoutControlItem8.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				layoutControl2.Text = Inventec.Common.Resource.Get.Value("UCRegister.layoutControl2.Text", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadDefaultScreenSaver()
		{
			try
			{
				List<object> listArgs = new List<object>();
				WaitingManager.Hide();
				V_HIS_ROOM SCREEN_SAVER = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault((V_HIS_ROOM o) => o.ID == currentModule.RoomId);
				if (SCREEN_SAVER != null)
				{
					HIS_DEPARTMENT hIS_DEPARTMENT = BackendDataWorker.Get<HIS_DEPARTMENT>().FirstOrDefault((HIS_DEPARTMENT o) => o.ID == SCREEN_SAVER.DEPARTMENT_ID);
					if (hIS_DEPARTMENT != null)
					{
						ucPatientRaw1.SetEmergencyFromDepartment(hIS_DEPARTMENT.IS_EMERGENCY == 1);
						IsEmergency = hIS_DEPARTMENT.IS_EMERGENCY == 1;
					}
					if (!string.IsNullOrEmpty(SCREEN_SAVER.SCREEN_SAVER_MODULE_LINK))
					{
						PluginInstanceBehavior.ShowModule(SCREEN_SAVER.SCREEN_SAVER_MODULE_LINK, currentModule.RoomId, currentModule.RoomTypeId, listArgs);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void timerInitForm_Tick()
		{
			try
			{
				LogSystem.Debug("timer1_Tick .1");
				StopTimer(currentModule.ModuleLink, "timerInitForm");
				LogSystem.Debug("timer1_Tick .2");
				FocusNextUserControl();
				LogSystem.Debug("timer1_Tick .3");
				InitInputDataUCHeinInfo();
				LogSystem.Debug("timer1_Tick .4");
				SetDefaultCashierRoom();
				LogSystem.Debug("timer1_Tick .5");
				InitPopupMenuOther();
				LogSystem.Debug("timer1_Tick .6");
				GATE();
				ucAddressCombo1.SetDelegateSendCardSDO(new DelegateSendCardSDO(SendCardSDO), new DelegateReloadData(SendStateStrucAddress));
				ucPlusInfo1.SetDelegateInitTHX(delegate(bool result)
				{
					if (result)
					{
						ucAddressCombo1.InitControlState();
					}
				});
				ucPlusInfo1.InitFieldFromAsync();
				ucPlusInfo1.SetDelegateWorkPlaceUCPlusInfo(new DelegateGetWorkPlaceId(SetDelegateSetWorkPlace));
				ucOtherServiceReqInfo1.SetDelegateEmergence(new Action<bool>(SetEmergenceForRoom));
				ucOtherServiceReqInfo1.InitFieldFromAsync();
				ucRelativeInfo1.InitFieldFromAsync();
				ucPatientRaw1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void TimerRefeshAutoCreateBill_Tick(object sender, EventArgs e)
		{
			timerRefeshAutoCreateBill_Tick();
		}

		private void timerRefeshAutoCreateBill_Tick()
		{
			try
			{
				bool flag = GlobalVariables.AuthorityAccountBook != null && GlobalVariables.AuthorityAccountBook.AccountBookId.HasValue;
				if (!chkAutoDeposit.Checked)
				{
					chkAutoCreateBill.Checked = flag;
				}
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
				isNotLoadWhileChangeControlStateInFirst = true;
				controlStateWorker = new ControlStateWorker();
				currentControlStateRDO = controlStateWorker.GetData("HIS.Desktop.Plugins.RegisterV2");
				if (currentControlStateRDO != null && currentControlStateRDO.Count > 0)
				{
					foreach (ControlStateRDO item in currentControlStateRDO)
					{
						if (item.KEY == chkPrintPatientCard.Name)
						{
							chkPrintPatientCard.Checked = item.VALUE == "1";
						}
						else if (item.KEY == chkAutoCreateBill.Name)
						{
							chkAutoCreateBill.Checked = item.VALUE == "1";
						}
						else if (item.KEY == chkAutoDeposit.Name)
						{
							chkAutoDeposit.Checked = item.VALUE == "1";
						}
						else if (item.KEY == chkPrintExam.Name)
						{
							chkPrintExam.Checked = item.VALUE == "1";
						}
						else if (item.KEY == chkAssignDoctor.Name)
						{
							chkAssignDoctor.Checked = item.VALUE == "1";
						}
						else if (item.KEY == chkAutoPay.Name)
						{
							chkAutoPay.Checked = item.VALUE == "1";
						}
						else if (item.KEY == txtGateNumber.Name)
						{
							txtGateNumber.Text = item.VALUE;
						}
						else if (item.KEY == txtStepNumber.Name)
						{
							txtStepNumber.Text = item.VALUE;
						}
						else if (item.KEY == chkSignExam.Name)
						{
							chkSignExam.Checked = item.VALUE == "1";
						}
						else if (item.KEY == chkXemTruoc.Name)
						{
							chkXemTruoc.Checked = item.VALUE == "1";
						}
					}
				}
				currentControlStateRDOModuleLink = controlStateWorker.GetData("HIS.Desktop.Plugins.RegisterV2.frmConfigCall");
				foreach (ControlStateRDO item2 in currentControlStateRDOModuleLink)
				{
					if (item2.KEY == "HIS.Desktop.Plugins.RegisterV2.frmConfigCall")
					{
						CallConfigString = item2.VALUE;
					}
				}
				isNotLoadWhileChangeControlStateInFirst = false;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private DateTime GetIntructionTime()
		{
			try
			{
				return Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ucOtherServiceReqInfo1.GetValue().IntructionTime).Value;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return default(DateTime);
		}

		private bool GetIsChild()
		{
			bool result = false;
			try
			{
				result = ucPatientRaw1.GetIsChild();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		private void PatientTypeEditValueChanged(object sender, EventArgs e)
		{
			try
			{
				LogSystem.Debug("PatientTypeEditValueChanged => 1");
				long patientTypeId = Parse.ToInt64(((BaseEdit)sender).EditValue.ToString());
				if (patientTypeId > 0)
				{
					WaitingManager.Show();
					ucHeinInfo1.SetTypeCardTemp(patientTypeId);
					if (patientTypeId == HisConfigCFG.PatientTypeId__QN || GetIsChild())
					{
						ucHeinInfo1.SetDisableHasCardTemp(true);
					}
					SuspendLayoutWithPatientTypeChanged(patientTypeId);
					if (patientTypeId == HisConfigCFG.PatientTypeId__BHYT || patientTypeId == HisConfigCFG.PatientTypeId__QN)
					{
						ucHeinInfo1.InitValidateRule(patientTypeId);
						ucHeinInfo1.InitFieldFromAsync();
						DelegateFocusNextUserControl dlgFocusNextUserControl = new DelegateFocusNextUserControl(ucHeinInfo1.FocusUserControl);
						ucAddressCombo1.FocusNextUserControl(dlgFocusNextUserControl);
					}
					else if (patientTypeId == HisConfigCFG.PatientTypeId__KSK)
					{
						if (ucKskContract != null && kskContractProcessor != null)
						{
							DelegateFocusNextUserControl dlgFocusNextUserControl2 = new DelegateFocusNextUserControl(FocusInKskContract);
							ucAddressCombo1.FocusNextUserControl(dlgFocusNextUserControl2);
						}
						ucHeinInfo1.RefreshUserControl();
						ucHeinInfo1.ResetRequiredField();
					}
					else
					{
						if (ucServiceRoomInfo1 != null)
						{
							DelegateFocusNextUserControl dlgFocusNextUserControl3 = new DelegateFocusNextUserControl(ucServiceRoomInfo1.FocusUserControl);
							ucAddressCombo1.FocusNextUserControl(dlgFocusNextUserControl3);
						}
						ucHeinInfo1.RefreshUserControl();
						ucHeinInfo1.ResetRequiredField();
					}
					ucOtherServiceReqInfo1.ChangePatientType(patientTypeId);
					GlobalStore.PatientTypeIdAllows = (from o in BackendDataWorker.Get<V_HIS_PATIENT_TYPE_ALLOW>()
						where o.PATIENT_TYPE_ID == patientTypeId
						select o.PATIENT_TYPE_ALLOW_ID).ToList();
					currentPatientTypeAllowByPatientType = LoadPatientTypeExamByPatientType(patientTypeId);
					ucServiceRoomInfo1.RefreshUserControl();
					ReloadExamServiceRoom();
					long dOB = ucPatientRaw1.GetValue().DOB;
					if (patientTypeId == HisConfigCFG.PatientTypeId__BHYT)
					{
						DateTime dateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ucPatientRaw1.GetValue().DOB) ?? DateTime.MinValue;
						if (dateTime != DateTime.MinValue)
						{
							if (DateTime.Now.Year - dateTime.Year < 6)
							{
								ucHeinInfo1.SetEnableChkSS(true);
							}
							else
							{
								ucHeinInfo1.SetEnableChkSS(false);
							}
						}
					}
					if (HisConfigCFG.AutoCheckPrintExam__PatientTypeIds != null && HisConfigCFG.AutoCheckPrintExam__PatientTypeIds.Count > 0 && HisConfigCFG.AutoCheckPrintExam__PatientTypeIds.Contains(patientTypeId))
					{
						chkPrintExam.Checked = true;
					}
					else if (HisConfigCFG.AutoCheckPrintExam__PatientTypeIds != null && HisConfigCFG.AutoCheckPrintExam__PatientTypeIds.Count > 0)
					{
						chkPrintExam.Checked = false;
					}
					WaitingManager.Hide();
					ucPatientRaw1.FocusUserControl();
				}
				LogSystem.Debug("PatientTypeEditValueChanged => 2");
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
		}

		private void SuspendLayoutWithPatientTypeChanged(long patientTypeId)
		{
			LogSystem.Debug("SuspendLayoutWithPatientTypeChanged.1");
			layoutControl1.BeginUpdate();
			layoutControl1.SuspendLayout();
			if ((patientTypeId == HisConfigCFG.PatientTypeId__BHYT && HisConfigCFG.PatientTypeId__BHYT > 0) || (patientTypeId == HisConfigCFG.PatientTypeId__QN && HisConfigCFG.PatientTypeId__QN > 0))
			{
				LogSystem.Debug("SuspendLayoutWithPatientTypeChanged.2");
				lciUCHeinInfo.Visibility = LayoutVisibility.Always;
				lciUCServiceRoomInfo.BeginInit();
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowIndex = 16;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowSpan = 3;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCServiceRoomInfo.EndInit();
				lciUCHeinInfo.BeginInit();
				Control control = lciUCHeinInfo.Control;
				lciUCHeinInfo.Control = ucHeinInfo1;
				control.Parent = null;
				lciUCHeinInfo.OptionsTableLayoutItem.RowIndex = 9;
				lciUCHeinInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCHeinInfo.OptionsTableLayoutItem.RowSpan = 7;
				lciUCHeinInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCHeinInfo.EndInit();
				layoutControl1.BeginUpdate();
				lciUCServiceRoomInfo.BeginInit();
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowIndex = 16;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowSpan = 3;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCServiceRoomInfo.EndInit();
				lciUCHeinInfo.BeginInit();
				lciUCHeinInfo.Control = ucHeinInfo1;
				lciUCHeinInfo.OptionsTableLayoutItem.RowIndex = 9;
				lciUCHeinInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCHeinInfo.OptionsTableLayoutItem.RowSpan = 7;
				lciUCHeinInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCHeinInfo.EndInit();
				layoutControl1.EndUpdate();
			}
			else if (patientTypeId == HisConfigCFG.PatientTypeId__KSK && HisConfigCFG.PatientTypeId__KSK > 0)
			{
				LogSystem.Debug("SuspendLayoutWithPatientTypeChanged.3");
				lciUCHeinInfo.Visibility = LayoutVisibility.Always;
				KskContractInput kskContractInput = new KskContractInput();
				kskContractInput.DeleOutFocus = new DelegateRefreshData(DeleOutFocusKskContract);
				kskContractProcessor = new KskContractProcessor(TemplateType.ENUM.TEMPLATE_2);
				ucKskContract = (UserControl)kskContractProcessor.Run(kskContractInput);
				lciUCServiceRoomInfo.BeginInit();
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowIndex = 14;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowSpan = 5;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCServiceRoomInfo.EndInit();
				lciUCHeinInfo.BeginInit();
				Control control2 = lciUCHeinInfo.Control;
				lciUCHeinInfo.Control = ucKskContract;
				control2.Parent = null;
				lciUCHeinInfo.OptionsTableLayoutItem.RowIndex = 9;
				lciUCHeinInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCHeinInfo.OptionsTableLayoutItem.RowSpan = 5;
				lciUCHeinInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCHeinInfo.EndInit();
				lciUCServiceRoomInfo.BeginInit();
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowIndex = 14;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowSpan = 5;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCServiceRoomInfo.EndInit();
				lciUCHeinInfo.BeginInit();
				lciUCHeinInfo.Control = ucKskContract;
				lciUCHeinInfo.OptionsTableLayoutItem.RowIndex = 9;
				lciUCHeinInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCHeinInfo.OptionsTableLayoutItem.RowSpan = 5;
				lciUCHeinInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCHeinInfo.EndInit();
			}
			else
			{
				LogSystem.Debug("SuspendLayoutWithPatientTypeChanged.4");
				lciUCHeinInfo.Visibility = LayoutVisibility.Never;
				lciUCServiceRoomInfo.BeginInit();
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowIndex = 9;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnIndex = 0;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.RowSpan = 10;
				lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnSpan = 2;
				lciUCServiceRoomInfo.EndInit();
			}
			layoutControl1.ResumeLayout(false);
			layoutControl1.EndUpdate();
			LogSystem.Debug("SuspendLayoutWithPatientTypeChanged.5");
		}

		private void FocusInKskContract()
		{
			try
			{
				if (ucKskContract != null && kskContractProcessor != null)
				{
					kskContractProcessor.InFocus(ucKskContract);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void DeleOutFocusKskContract()
		{
			try
			{
				ucServiceRoomInfo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ConfigLayout()
		{
			try
			{
				layoutControl1.BeginUpdate();
				for (int i = 0; i < layoutControl1.Root.OptionsTableLayoutGroup.ColumnDefinitions.Count; i++)
				{
					layoutControl1.Root.OptionsTableLayoutGroup.ColumnDefinitions[i].SizeType = SizeType.Percent;
					layoutControl1.Root.OptionsTableLayoutGroup.ColumnDefinitions[i].Width = 16.0;
				}
				LogSystem.Debug("ConfigLayout - 1");
				for (int j = 0; j < layoutControl1.Root.OptionsTableLayoutGroup.RowDefinitions.Count; j++)
				{
					layoutControl1.Root.OptionsTableLayoutGroup.RowDefinitions[j].SizeType = SizeType.Percent;
					layoutControl1.Root.OptionsTableLayoutGroup.RowDefinitions[j].Height = 7.0;
				}
				LogSystem.Debug("ConfigLayout - 2");
				for (int k = layoutControl1.Root.OptionsTableLayoutGroup.ColumnDefinitions.Count; k < 6; k++)
				{
					layoutControl1.Root.OptionsTableLayoutGroup.ColumnDefinitions.Add(new ColumnDefinition
					{
						SizeType = SizeType.Percent,
						Width = 16.0
					});
				}
				LogSystem.Debug("ConfigLayout - 3");
				for (int l = layoutControl1.Root.OptionsTableLayoutGroup.RowDefinitions.Count; l < 14; l++)
				{
					layoutControl1.Root.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition
					{
						SizeType = SizeType.Percent,
						Height = 7.0
					});
				}
				layoutControl1.EndUpdate();
				LogSystem.Debug("ConfigLayout - 4");
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private long? GetPatientClassifyId()
		{
			try
			{
				return ucPatientRaw1.GetValue().PATIENT_CLASSIFY_ID;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return null;
		}

		private long GetPatientTypeId()
		{
			try
			{
				return ucPatientRaw1.GetValue().PATIENTTYPE_ID;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return 0L;
		}

		private void ChangeFindTypeInPatientRaw(bool isEnable)
		{
			try
			{
				ucOtherServiceReqInfo1.SetEnableChkExamOnline(isEnable);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private bool IsPatientTypeUsingHeinInfo()
		{
			return ucPatientRaw1 != null && ucPatientRaw1.GetValue() != null && ucHeinInfo1 != null && ucPatientRaw1.GetValue().PATIENTTYPE_ID > 0 && (ucPatientRaw1.GetValue().PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT || ucPatientRaw1.GetValue().PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__QN);
		}

		private void FillDataAfterSearchPatientInUCPatientRaw(object data)
		{
			try
			{
				if (data != null)
				{
					IsReadCardTheViet = false;
					string text = "";
					HeinCardData heinCardData = new HeinCardData();
					DataResultADO dataResultADO = (DataResultADO)data;
					SetValueVariableUCAddressCombo(dataResultADO);
					if (dataResultADO.SearchTypePatient == 4)
					{
						IsReadCardTheViet = true;
						HtProvinceCode = ((dataResultADO.HisPatientSDO != null) ? dataResultADO.HisPatientSDO.HT_PROVINCE_CODE : null);
						HtDistrictCode = ((dataResultADO.HisPatientSDO != null) ? dataResultADO.HisPatientSDO.HT_DISTRICT_CODE : null);
						HtCommuneCode = ((dataResultADO.HisPatientSDO != null) ? dataResultADO.HisPatientSDO.HT_COMMUNE_CODE : null);
						HtProvinceName = ((dataResultADO.HisPatientSDO != null) ? dataResultADO.HisPatientSDO.HT_PROVINCE_NAME : null);
						HtDistrictName = ((dataResultADO.HisPatientSDO != null) ? dataResultADO.HisPatientSDO.HT_DISTRICT_NAME : null);
						HtCommuneName = ((dataResultADO.HisPatientSDO != null) ? dataResultADO.HisPatientSDO.HT_COMMUNE_NAME : null);
					}
					if (!dataResultADO.OldPatient && dataResultADO.UCRelativeADO != null)
					{
						FillDataIntoUCRelativeInfo(dataResultADO.UCRelativeADO);
					}
					else if (!dataResultADO.OldPatient && dataResultADO.HeinCardData != null)
					{
						FillDataAfterSaerchPatientInUCPatientRaw(dataResultADO);
						FillDataIntoUCPlusInfo(dataResultADO.HisPatientSDO, dataResultADO.IsReadQr);
						heinCardData = dataResultADO.HeinCardData;
					}
					else if (dataResultADO.HisPatientSDO != null)
					{
						_isPatientAppointmentCode = dataResultADO.SearchTypePatient == 2;
						if (!string.IsNullOrEmpty(dataResultADO.AppointmentCode))
						{
							appointmentCode = dataResultADO.AppointmentCode;
						}
						long treatmnetIdByAppointmentCode = dataResultADO.TreatmnetIdByAppointmentCode;
						_TreatmnetIdByAppointmentCode = dataResultADO.TreatmnetIdByAppointmentCode;
						currentPatientSDO = dataResultADO.HisPatientSDO;
						FillDataIntoUCPlusInfo(currentPatientSDO, dataResultADO.IsReadQr);
						FillDataIntoUCRelativeInfo(currentPatientSDO);
						FillDataIntoUCAddressInfo(dataResultADO);
						if (dataResultADO.SearchTypePatient == 4 && !dataResultADO.OldPatient)
						{
							FillDataIntoUCHeinInfoByPatientTypeAlter(currentPatientSDO);
						}
						if (dataResultADO.SearchTypePatient == 5 && !dataResultADO.OldPatient)
						{
							FillDataIntoUCHeinInfoByPatientTypeAlter(currentPatientSDO);
						}
						else if (!string.IsNullOrEmpty(currentPatientSDO.HeinCardNumber))
						{
							FillDataIntoUCHeinInfo(currentPatientSDO);
						}
						else if (IsPatientTypeUsingHeinInfo())
						{
							ucHeinInfo1.RefreshUserControl();
						}
						FillDataIntoUCImage(currentPatientSDO);
						FillDataIntoUCOtherServiceReqInfo(currentPatientSDO);
						FillDataToExamServiceReqByPatient(currentPatientSDO);
						if (currentPatientSDO.IS_HAS_NOT_DAY_DOB == 1)
						{
							heinCardData.Dob = currentPatientSDO.DOB.ToString();
						}
						else
						{
							heinCardData.Dob = Inventec.Common.DateTime.Convert.TimeNumberToDateString(currentPatientSDO.DOB);
						}
						heinCardData.PatientName = currentPatientSDO.VIR_PATIENT_NAME;
						heinCardData.HeinCardNumber = currentPatientSDO.HeinCardNumber;
						heinCardData.Gender = GenderConvert.HisToHein(currentPatientSDO.GENDER_ID.ToString());
						heinCardData.FromDate = Inventec.Common.DateTime.Convert.TimeNumberToDateString(currentPatientSDO.HeinCardFromTime.GetValueOrDefault());
						heinCardData.MediOrgCode = currentPatientSDO.HeinMediOrgCode;
						heinCardData.Address = currentPatientSDO.HeinAddress;
						heinCardData.LiveAreaCode = currentPatientSDO.LiveAreaCode;
						heinCardData.ToDate = Inventec.Common.DateTime.Convert.TimeNumberToDateString(currentPatientSDO.HeinCardToTime.GetValueOrDefault());
					}
					UCPatientRawADO value = ucPatientRaw1.GetValue();
					long? num = null;
					if (ucPatientRaw1 != null && !string.IsNullOrEmpty(heinCardData.HeinCardNumber))
					{
						value = ucPatientRaw1.GetValue();
						if (value.PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT)
						{
							CheckTTProcessResultData(heinCardData, null, false);
						}
						num = value.TREATMENT_TYPE_ID;
					}
					text = heinCardData.HeinCardNumber;
					SetPatientSearchPanel(dataResultADO.OldPatient);
					cardSearch = dataResultADO.HisCardSDO;
					FillDataCareerUnder6AgeByHeinCardNumber(text);
					LogSystem.Debug(LogUtil.TraceData("FillDataAfterSearchPatientInUCPatientRaw(object data) treatmentTypeId ", num));
					if (HisConfigCFG.IsDefaultTreatmentTypeExam)
					{
						AutoSetTreatmentTypeCombo(1L, dataResultADO);
					}
					else if (num.HasValue && num.Value == 2)
					{
						AutoSetTreatmentTypeCombo(num, dataResultADO);
					}
					chkBaoLanh.Checked = false;
				}
				else
				{
					btnNewContinue_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private HIS_CAREER GetCareerByBhytWhiteListConfig(string heinCardNumder)
		{
			HIS_CAREER hIS_CAREER = null;
			try
			{
				if (!string.IsNullOrEmpty(heinCardNumder))
				{
					HIS_BHYT_WHITELIST bhytWhiteList = BackendDataWorker.Get<HIS_BHYT_WHITELIST>().FirstOrDefault((HIS_BHYT_WHITELIST o) => !string.IsNullOrEmpty(heinCardNumder) && o.BHYT_WHITELIST_CODE.ToUpper() == heinCardNumder.Substring(0, 3).ToUpper());
					if (bhytWhiteList != null && bhytWhiteList.CAREER_ID.GetValueOrDefault() > 0)
					{
						hIS_CAREER = BackendDataWorker.Get<HIS_CAREER>().SingleOrDefault((HIS_CAREER o) => o.ID == bhytWhiteList.CAREER_ID.Value);
						if (hIS_CAREER == null)
						{
							LogSystem.Warn("GetCareerByBhytWhiteListConfig => Khong lay duoc nghe nghiep theo id = " + bhytWhiteList.CAREER_ID);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return hIS_CAREER;
		}

		private void FillDataCareerUnder6AgeByHeinCardNumber(string heinCardNumder)
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				if (!string.IsNullOrEmpty(heinCardNumder))
				{
					HIS_CAREER hIS_CAREER = GetCareerByBhytWhiteListConfig(heinCardNumder);
					if (hIS_CAREER == null)
					{
						if (value.DOB > 0)
						{
							DateTime value2 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB).Value;
							hIS_CAREER = ((value2 != DateTime.MinValue && BhytPatientTypeData.IsChild(value2)) ? HisConfigCFG.CareerUnder6Age : ((DateTime.Now.Year - value2.Year > 18) ? HisConfigCFG.CareerBase : HisConfigCFG.CareerHS));
						}
						else
						{
							hIS_CAREER = HisConfigCFG.CareerBase;
						}
					}
					if (hIS_CAREER != null && hIS_CAREER.ID > 0)
					{
						value.CARRER_ID = hIS_CAREER.ID;
						value.CARRER_CODE = hIS_CAREER.CAREER_CODE;
						value.CARRER_NAME = hIS_CAREER.CAREER_NAME;
						LogSystem.Error("FillDataCareerUnder6AgeByHeinCardNumber");
						ucPatientRaw1.SetValue(value);
					}
				}
				ucOtherServiceReqInfo1.AutoCheckPriorityByPriorityType(value.DOB, heinCardNumder);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ProcessWhileChangeDOb()
		{
			try
			{
				HisPatientProfileSDO valuePatientTypeAlter = ucHeinInfo1.GetValuePatientTypeAlter();
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				if (valuePatientTypeAlter != null && value != null)
				{
					ucHeinInfo1.ShowCheckSS(DateTime.Now.Year - (Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB) ?? DateTime.MinValue).Year < 6);
				}
				ucOtherServiceReqInfo1.AutoCheckPriorityByPriorityType(value.DOB, valuePatientTypeAlter.HisPatientTypeAlter.HEIN_CARD_NUMBER);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetValueVariableUCAddressCombo(DataResultADO data)
		{
			try
			{
				if (data.OldPatient)
				{
					ucAddressCombo1.isPatientBHYT = true;
				}
				else
				{
					ucAddressCombo1.isPatientBHYT = false;
				}
				if (data.HeinCardData != null)
				{
					ucAddressCombo1.isReadCard = true;
				}
				else
				{
					ucAddressCombo1.isReadCard = false;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ResetPatientForm()
		{
			try
			{
				ResetPatientInfo();
				SetPatientSearchPanel(false);
				ucHeinInfo1.Controls.Clear();
				SetDefaultRegisterForm();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ResetPatientInfo()
		{
			try
			{
				if (!layoutControl2.IsInitialized)
				{
					return;
				}
				layoutControl2.BeginUpdate();
				try
				{
					foreach (BaseLayoutItem item in layoutControl2.Items)
					{
						LayoutControlItem layoutControlItem = item as LayoutControlItem;
						if (layoutControlItem != null && layoutControlItem.Control != null && layoutControlItem.Control is BaseEdit)
						{
							BaseEdit baseEdit = layoutControlItem.Control as BaseEdit;
							if (!(layoutControlItem.Name == "lciGateNumber") && !(layoutControlItem.Name == "lciStepNumber") && !(layoutControlItem.Name == "lcicboCashierRoom"))
							{
								baseEdit.ResetText();
								baseEdit.EditValue = null;
							}
						}
					}
				}
				catch (Exception ex)
				{
					LogSystem.Warn(ex);
				}
				finally
				{
					layoutControl2.EndUpdate();
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private void NewContinue()
		{
			try
			{
				isResetForm = true;
				IsReadCardTheViet = false;
				isCheckSS = false;
				if (!IsCheckSave && !string.IsNullOrEmpty(HisConfigCFG.GuaranteeConnection))
				{
					string[] array = HisConfigCFG.GuaranteeConnection.Split('|');
					if (array.Length >= 3)
					{
						string[] array2 = array[0].Split(';');
						string hasUri = ((array2.Length != 0) ? array2[0].Trim() : "");
						string acsUri = ((array2.Length > 1) ? array2[1].Trim() : "");
						string[] array3 = array[1].Split(':');
						string text = ((array3.Length != 0) ? array3[0].Trim() : "");
						string text2 = ((array3.Length > 1) ? array3[1].Trim() : "");
						string password = ((array3.Length > 2) ? array3[2].Trim() : "");
						string text3 = array[2].Trim();
						LogSystem.Debug(string.Format("Guarantee Connection - Address: {0}, AppCode: {1}, Username: {2}, DefaultLimit: {3}", array2, text, text2, text3));
						string hEIN_MEDI_ORG_CODE = BranchDataWorker.Branch.HEIN_MEDI_ORG_CODE;
						MedicalExpenseGuaranteeProcessor medicalExpenseGuaranteeProcessor = new MedicalExpenseGuaranteeProcessor();
						DataInput data = new DataInput();
						data.hasUri = hasUri;
						data.acsUri = acsUri;
						data.username = text2;
						data.password = password;
						data.applicationCode = text;
						data.limet = text3;
						data.cskcbbd = hEIN_MEDI_ORG_CODE;
						UCPatientRawADO value = ucPatientRaw1.GetValue();
						UCPlusInfoADO value2 = ucPlusInfo1.GetValue();
						data.cancelRegisterUseRequest = new CancelRegisterUseRequest
						{
							RequestId = GuaranteeRequestCode,
							ContractNumber = GuarateeCode,
							PatientFullName = ((value != null) ? value.PATIENT_NAME : ""),
							PatientDateOfBirth = ((value != null) ? value.DOB.ToString() : ""),
							PatientCccd = ((value2 != null) ? value2.CCCD_NUMBER : ""),
							Amount = text3,
							Remark = "Hủy đăng ký sử dụng bảo lãnh",
							Signature = "",
							Token = ""
						};
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => data), data));
						if (!string.IsNullOrEmpty(GuaranteeRequestCode))
						{
							CancelRegisterUseResponse rs = medicalExpenseGuaranteeProcessor.GuaranteeCancelRegisterUse(data);
							LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs));
							if (rs != null && rs.Success)
							{
								LogSystem.Info("Hủy bảo lãnh thành công, RequestId: " + GuaranteeRequestCode);
							}
							else
							{
								LogSystem.Info("Hủy bảo lãnh thất bại, RequestId: " + GuaranteeRequestCode);
							}
						}
						GuarateeCode = null;
						GuaranteeRequestCode = null;
					}
				}
				IsCheckSave = false;
				RefreshUserControl();
				ucPatientRaw1.LoadDataCboDoiTuong(roomId);
				if (ucPatientRaw1.cboPatientType.EditValue == null)
				{
					ucPatientRaw1.LoadDataComboPrimaryPatientType(roomId);
				}
				chkBaoLanh.Checked = false;
				HIS_PATIENT_TYPE patientTypeDefault = AppConfigs.PatientTypeDefault;
				if ((patientTypeDefault == null || patientTypeDefault.ID <= 0) && !HisConfigCFG.UsingPatientTypeOfPreviousPatient)
				{
					LogSystem.Debug("Truong hop khong co cau hinh mac dinh doi tuong benh nhan => reset vung thong tin bhyt___" + LogUtil.TraceData(LogUtil.GetMemberName(() => patientTypeDefault), patientTypeDefault));
					SuspendLayoutWithPatientTypeChanged(0L);
				}
				UCPatientRawADO value3 = ucPatientRaw1.GetValue();
				long item = ((value3 != null) ? value3.PATIENTTYPE_ID : 0);
				if (HisConfigCFG.AutoCheckPrintExam__PatientTypeIds != null && HisConfigCFG.AutoCheckPrintExam__PatientTypeIds.Count > 0 && HisConfigCFG.AutoCheckPrintExam__PatientTypeIds.Contains(item))
				{
					chkPrintExam.Checked = true;
				}
				else if (HisConfigCFG.AutoCheckPrintExam__PatientTypeIds != null && HisConfigCFG.AutoCheckPrintExam__PatientTypeIds.Count > 0)
				{
					chkPrintExam.Checked = false;
				}
				isResetForm = false;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void btnNewContinue_Click(object sender, EventArgs e)
		{
			try
			{
				LogTheadInSessionInfo(new Action(NewContinue), "btnNewContinue_Click");
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
				LogTheadInSessionInfo(new Action(SaveNotPrintAction), "btnSave_Click");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SaveNotPrintAction()
		{
			Save(false);
		}

		private void SaveAndPrintAction()
		{
			IsActionSavePrint = true;
			if (chkXemTruoc.Checked)
			{
				IsActionSavePrint = false;
			}
			Save(true);
		}

		private void btnSaveAndPrint_Click(object sender, EventArgs e)
		{
			try
			{
				LogTheadInSessionInfo(new Action(SaveAndPrintAction), "btnSaveAndPrint_Click");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			try
			{
				IsActionSavePrint = false;
				LogTheadInSessionInfo(new Action(PrintLogSS), "btnPrint_Click");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void PrintLogSS()
		{
			try
			{
				Print();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnDepositDetail_Click(object sender, EventArgs e)
		{
			try
			{
				DepositDetail();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnDepositRequest_Click(object sender, EventArgs e)
		{
			try
			{
				DepositRequestClick();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnBill_Click(object sender, EventArgs e)
		{
			try
			{
				Bill();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnTreatmentBedRoom_Click(object sender, EventArgs e)
		{
			try
			{
				TreatmentBedRoom();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnSaveAndAssain_Click(object sender, EventArgs e)
		{
			try
			{
				SaveAndAssain();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnCallPatient_Click(object sender, EventArgs e)
		{
			try
			{
				CreateThreadCallPatient();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnRecallPatient_Click(object sender, EventArgs e)
		{
			try
			{
				CreateThreadRecallCallPatient();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnPatientNew_Click(object sender, EventArgs e)
		{
			try
			{
				GetDataBeforeClickbtnPatientNew();
				RefreshUserControl();
				dataPatientRaw.PATIENT_CODE = "";
				dataPatientRaw.CARRER_ID = null;
				dataPatientRaw.PATIENT_CODE = "";
				dataPatientRaw.ReceptionForm = null;
				LogSystem.Error("btnPatientNew_Click");
				ucPatientRaw1.SetValue(dataPatientRaw);
				SetPatientSearchPanel(false);
				ucPatientRaw1.FocusToPatientType();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnTTChuyenTuyen_Click(object sender, EventArgs e)
		{
			try
			{
				if (btnTTChuyenTuyen.Enabled)
				{
					if (ucHeinInfo1 != null)
					{
						IsPresent = ucHeinInfo1.HeinRightRouteTypeIsPresent();
						IsPresentAndAppointment = ucHeinInfo1.HeinRightRouteTypeIsPresentAndAppointment();
					}
					else
					{
						IsPresent = false;
						IsPresentAndAppointment = false;
					}
					if (HisConfigCFG.KeyValueObligatoryTranferMediOrg == 1 && IsPresent)
					{
						ShowFormThongTinChuyenTuyen(true);
					}
					else if (HisConfigCFG.KeyValueObligatoryTranferMediOrg == 2 && (IsPresent || IsPresentAndAppointment))
					{
						ShowFormThongTinChuyenTuyen(true);
					}
					else if (HisConfigCFG.KeyValueObligatoryTranferMediOrg == 3 && IsPresent)
					{
						ShowFormThongTinChuyenTuyen(true);
					}
					else
					{
						ShowFormThongTinChuyenTuyen(false);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnGiayTo_Click(object sender, EventArgs e)
		{
			try
			{
				LogSystem.Debug("btnGiayTo_Click.1");
				if (!btnGiayTo.Enabled)
				{
					return;
				}
				Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.HisTreatmentFile").FirstOrDefault();
				if (module == null)
				{
					LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.HisTreatmentFile");
				}
				if (module.IsPlugin && module.ExtensionInfo != null)
				{
					LogSystem.Debug("btnGiayTo_Click.2");
					List<object> list = new List<object>();
					list.Add(GetTreatmentIdFromResultData());
					object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
				LogSystem.Debug("btnGiayTo_Click.3");
			}
			catch (NullReferenceException ex)
			{
				MessageBox.Show(HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBKhongTimThayPluginsCuaChucNangNay), HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				LogSystem.Error(ex);
			}
			catch (Exception ex2)
			{
				MessageBox.Show(HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.HeThongTBKhongTimThayPluginsCuaChucNangNay), HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				LogSystem.Warn(ex2);
			}
			LogSystem.Debug("btnGiayTo_Click.4");
		}

		private async Task InitPopupMenuOther()
		{
			try
			{
				HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageUCRegister = new ResourceManager("HIS.Desktop.Plugins.RegisterV2.Resources.Lang", typeof(HIS.Desktop.Plugins.RegisterV2.RunV3.UCRegister).Assembly);
				DXPopupMenu menu = new DXPopupMenu
				{
					Items = 
					{
						new DXMenuItem("Chỉ định giải phẫu bệnh", new EventHandler(onClickAssignPan)),
						new DXMenuItem(Inventec.Common.Resource.Get.Value("UCRegister.PopupMenuOther.btnDHST", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageUCRegister, LanguageManager.GetCulture()), new EventHandler(onClickDHST)),
						new DXMenuItem(Inventec.Common.Resource.Get.Value("UCRegister.PopupMenuOther.btnTaiNanThuongTich", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageUCRegister, LanguageManager.GetCulture()), new EventHandler(onClickTaiNanThuongTich)),
						new DXMenuItem(Inventec.Common.Resource.Get.Value("UCRegister.PopupMenuOther.RequestDeposit", HIS.Desktop.Plugins.RegisterV2.Resources.ResourceLanguageManager.LanguageUCRegister, LanguageManager.GetCulture()), new EventHandler(btnRequestDepositClick)),
						new DXMenuItem("Thông tin dịch tễ", new EventHandler(onClickThongTinDichTe)),
						new DXMenuItem(ResourceMessage.Title_InTheBenhNhan, new EventHandler(PrintTheBenhNhan))
					}
				};
				dropDownButton__Other.DropDownControl = menu;
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private void onClickAssignPan(object sender, EventArgs e)
		{
			long num = 0L;
			if (resultHisPatientProfileSDO != null)
			{
				num = resultHisPatientProfileSDO.HisTreatment.ID;
			}
			else if (currentHisExamServiceReqResultSDO != null)
			{
				num = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.ID;
			}
			Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.AssignPaan").FirstOrDefault();
			if (module == null)
			{
				LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.AssignPaan");
			}
			if (module.IsPlugin && module.ExtensionInfo != null)
			{
				List<object> list = new List<object>();
				list.Add(num);
				list.Add(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId));
				object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
				if (pluginInstance == null)
				{
					throw new ArgumentNullException("moduleData is null");
				}
				((Form)pluginInstance).ShowDialog();
			}
		}

		private void PrintTheBenhNhan(object sender, EventArgs e)
		{
			try
			{
				RichEditorStore richEditorStore = new RichEditorStore(HIS.Desktop.ApiConsumer.ApiConsumers.SarConsumer, ConfigSystems.URI_API_SAR, LanguageManager.GetLanguage(), GlobalVariables.TemnplatePathFolder);
				richEditorStore.RunPrintTemplate("Mps000178", new DelegateRunPrinter(DelegateRunPrinterInTheBenhNhan));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void onClickDHST(object sender, EventArgs e)
		{
			try
			{
				long num = 0L;
				if (resultHisPatientProfileSDO != null)
				{
					num = resultHisPatientProfileSDO.HisTreatment.ID;
				}
				else if (currentHisExamServiceReqResultSDO != null)
				{
					num = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.ID;
				}
				Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.HisDhst").FirstOrDefault();
				if (module == null)
				{
					LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.HisDhst");
				}
				if (module.IsPlugin && module.ExtensionInfo != null)
				{
					List<object> list = new List<object>();
					list.Add(num);
					list.Add(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId));
					object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void onClickTaiNanThuongTich(object sender, EventArgs e)
		{
			try
			{
				long num = 0L;
				if (resultHisPatientProfileSDO != null)
				{
					num = resultHisPatientProfileSDO.HisTreatment.ID;
				}
				else if (currentHisExamServiceReqResultSDO != null)
				{
					num = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.ID;
				}
				Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.AccidentHurt").FirstOrDefault();
				if (module == null)
				{
					LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.AccidentHurt");
				}
				if (module.IsPlugin && module.ExtensionInfo != null)
				{
					List<object> list = new List<object>();
					list.Add(num);
					list.Add(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId));
					object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void onClickThongTinDichTe(object sender, EventArgs e)
		{
			try
			{
				long num = 0L;
				if (resultHisPatientProfileSDO != null)
				{
					num = resultHisPatientProfileSDO.HisTreatment.ID;
				}
				else if (currentHisExamServiceReqResultSDO != null)
				{
					num = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.ID;
				}
				Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.EpidemiologyInfo").FirstOrDefault();
				if (module == null)
				{
					LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.EpidemiologyInfo");
				}
				if (module.IsPlugin && module.ExtensionInfo != null)
				{
					List<object> list = new List<object>();
					list.Add(num);
					list.Add(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId));
					object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnRequestDepositClick(object sender, EventArgs e)
		{
			try
			{
				long num = 0L;
				if (resultHisPatientProfileSDO != null)
				{
					num = resultHisPatientProfileSDO.HisTreatment.ID;
				}
				else if (currentHisExamServiceReqResultSDO != null)
				{
					num = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.ID;
				}
				if (num <= 0)
				{
					return;
				}
				Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.RequestDeposit").FirstOrDefault();
				if (module == null)
				{
					throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.RequestDeposit'");
				}
				if (module.IsPlugin && module.ExtensionInfo != null)
				{
					List<object> list = new List<object>();
					list.Add(num);
					list.Add(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId));
					object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void dropDownButton__Other_Click(object sender, EventArgs e)
		{
			try
			{
				dropDownButton__Other.ShowDropDown();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void chkPrintPatientCard_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (isNotLoadWhileChangeControlStateInFirst)
				{
					return;
				}
				WaitingManager.Show();
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == chkPrintPatientCard.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = (chkPrintPatientCard.Checked ? "1" : "");
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = chkPrintPatientCard.Name;
					controlStateRDO.VALUE = (chkPrintPatientCard.Checked ? "1" : "");
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private void chkAutoCreateBill_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (chkAutoCreateBill.Checked)
				{
					chkAutoDeposit.Checked = false;
				}
				ChangeCheckAutoPaid();
				if (isNotLoadWhileChangeControlStateInFirst)
				{
					return;
				}
				WaitingManager.Show();
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == chkAutoCreateBill.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = (chkAutoCreateBill.Checked ? "1" : "");
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = chkAutoCreateBill.Name;
					controlStateRDO.VALUE = (chkAutoCreateBill.Checked ? "1" : "");
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private void chkPrintExam_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (isNotLoadWhileChangeControlStateInFirst)
				{
					return;
				}
				WaitingManager.Show();
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == chkPrintExam.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = (chkPrintExam.Checked ? "1" : "");
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = chkPrintExam.Name;
					controlStateRDO.VALUE = (chkPrintExam.Checked ? "1" : "");
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private void chkAssignDoctor_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (isNotLoadWhileChangeControlStateInFirst)
				{
					return;
				}
				WaitingManager.Show();
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == chkAssignDoctor.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = (chkAssignDoctor.Checked ? "1" : "");
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = chkAssignDoctor.Name;
					controlStateRDO.VALUE = (chkAssignDoctor.Checked ? "1" : "");
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private void chkAutoDeposit_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (chkAutoDeposit.Checked)
				{
					chkAutoCreateBill.Checked = false;
				}
				ChangeCheckAutoPaid();
				if (isNotLoadWhileChangeControlStateInFirst)
				{
					return;
				}
				WaitingManager.Show();
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == chkAutoDeposit.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = (chkAutoDeposit.Checked ? "1" : "");
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = chkAutoDeposit.Name;
					controlStateRDO.VALUE = (chkAutoDeposit.Checked ? "1" : "");
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private void SetDefaultRegisterForm()
		{
			try
			{
				LogSystem.Debug("SetDefaultRegisterForm. 1");
				_HeinCardData = new HeinCardData();
				ResultDataADO = new ResultDataADO();
				actionType = 1;
				isReadQrCode = false;
				cardSearch = null;
				isNotPatientDayDob = false;
				baseNameControl = layoutControl2.Name + "." + layoutControlGroup2.Name + "." + layoutControlItem1.Name + "." + layoutControl1.Name + "." + layoutControlGroup1.Name + "." + pnlServiceRoomInfomation.Name + "." + ucPatientRaw1.Name;
				ucPatientRaw1.SetNameControl(baseNameControl);
				SDA_ETHNIC ethinicBase = HisConfigCFG.EthinicBase;
				HIS_PATIENT_TYPE patientType = null;
				if (!string.IsNullOrEmpty(AppConfigs.PatientTypeCodeDefault))
				{
					patientType = BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault((HIS_PATIENT_TYPE o) => o.PATIENT_TYPE_CODE == AppConfigs.PatientTypeCodeDefault && o.IS_ACTIVE == 1 && o.IS_NOT_USE_FOR_PATIENT != 1);
					if (patientType != null)
					{
					}
				}
				LogSystem.Debug("SetDefaultRegisterForm.2");
				if (patientType != null)
				{
					ucPatientRaw1.SetValuePatientType(patientType.ID);
					currentPatientTypeAllowByPatientType = LoadPatientTypeExamByPatientType(patientType.ID);
					GlobalStore.PatientTypeIdAllows = (from o in BackendDataWorker.Get<V_HIS_PATIENT_TYPE_ALLOW>()
						where o.PATIENT_TYPE_CODE == patientType.PATIENT_TYPE_CODE
						select o.PATIENT_TYPE_ALLOW_ID).ToList();
					if (patientType.ID > 0 && patientType.ID == HisConfigCFG.PatientTypeId__BHYT)
					{
						ucHeinInfo1.InitFieldFromAsync();
					}
					else
					{
						LogSystem.Debug("SetDefaultRegisterForm.3");
						SuspendLayoutWithPatientTypeChanged(patientType.ID);
					}
				}
				else
				{
					LogSystem.Debug("SetDefaultRegisterForm. 4");
					SuspendLayoutWithPatientTypeChanged(0L);
					LogSystem.Debug("Khong lay duoc doi tuong benh nhan mac dinh");
				}
				InitExamServiceRoom();
				ucServiceRoomInfo1.RefreshUserControl();
				LogSystem.Debug("t5: end");
				EnableButton(actionType, false);
				SetValidationByChildrenUnder6Years(false, true);
				_TreatmnetIdByAppointmentCode = 0L;
				isAlertTreatmentEndInDay = false;
				LogSystem.Debug("SetDefaultRegisterForm. 2");
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void chkAutoPaid_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (isNotLoadWhileChangeControlStateInFirst)
				{
					return;
				}
				WaitingManager.Show();
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == chkAutoPay.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = (chkAutoPay.Checked ? "1" : "");
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = chkAutoPay.Name;
					controlStateRDO.VALUE = (chkAutoPay.Checked ? "1" : "");
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ChangeCheckAutoPaid()
		{
			try
			{
				isNotLoadWhileChangeControlStateInFirst = true;
				if (chkAutoDeposit.Checked || chkAutoCreateBill.Checked)
				{
					chkAutoPay.Enabled = true;
					if (currentControlStateRDO != null && currentControlStateRDO.Count > 0)
					{
						foreach (ControlStateRDO item in currentControlStateRDO)
						{
							if (item.KEY == chkAutoPay.Name)
							{
								chkAutoPay.Checked = item.VALUE == "1";
								break;
							}
						}
					}
				}
				else
				{
					chkAutoPay.Enabled = false;
					chkAutoPay.Checked = false;
				}
				isNotLoadWhileChangeControlStateInFirst = false;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtStepNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
				{
					e.Handled = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
				{
					e.Handled = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
				{
					e.Handled = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void txtGateNumber_Leave(object sender, EventArgs e)
		{
			try
			{
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == txtGateNumber.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = txtGateNumber.Text;
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = txtGateNumber.Name;
					controlStateRDO.VALUE = txtGateNumber.Text;
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtStepNumber_Leave(object sender, EventArgs e)
		{
			try
			{
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == txtStepNumber.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = txtStepNumber.Text;
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = txtStepNumber.Name;
					controlStateRDO.VALUE = txtStepNumber.Text;
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtGateNumber_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				if (e.Button.Kind == ButtonPredefines.Plus)
				{
					frmConfigCall frmConfigCall2 = new frmConfigCall(new Action<bool>(ReloadConfigState));
					frmConfigCall2.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ReloadConfigState(bool obj)
		{
			try
			{
				if (!obj)
				{
					return;
				}
				currentControlStateRDOModuleLink = controlStateWorker.GetData("HIS.Desktop.Plugins.RegisterV2.frmConfigCall");
				foreach (ControlStateRDO item in currentControlStateRDOModuleLink)
				{
					if (item.KEY == "HIS.Desktop.Plugins.RegisterV2.frmConfigCall")
					{
						CallConfigString = item.VALUE;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void chkSignExam_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				WaitingManager.Show();
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == chkSignExam.Name && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = (chkSignExam.Checked ? "1" : "");
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = chkSignExam.Name;
					controlStateRDO.VALUE = (chkSignExam.Checked ? "1" : "");
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
		}

		private void UCRegister_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F11)
			{
				ucAddressCombo1.FocusTHX();
				e.Handled = true;
			}
		}

		private void chkBaoLanh_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(HisConfigCFG.GuaranteeConnection) && !(HisConfigCFG.GuaranteeConnection != ""))
				{
					return;
				}
				string[] array = HisConfigCFG.GuaranteeConnection.Split('|');
				if (array.Length < 3)
				{
					return;
				}
				string[] array2 = array[0].Split(';');
				string hasUri = ((array2.Length != 0) ? array2[0].Trim() : "");
				string acsUri = ((array2.Length > 1) ? array2[1].Trim() : "");
				string[] array3 = array[1].Split(':');
				string text = ((array3.Length != 0) ? array3[0].Trim() : "");
				string text2 = ((array3.Length > 1) ? array3[1].Trim() : "");
				string password = ((array3.Length > 2) ? array3[2].Trim() : "");
				string text3 = array[2].Trim();
				LogSystem.Debug(string.Format("Guarantee Connection - Address: {0}, AppCode: {1}, Username: {2}, DefaultLimit: {3}", array2, text, text2, text3));
				string hEIN_MEDI_ORG_CODE = BranchDataWorker.Branch.HEIN_MEDI_ORG_CODE;
				MedicalExpenseGuaranteeProcessor medicalExpenseGuaranteeProcessor = new MedicalExpenseGuaranteeProcessor();
				DataInput data = new DataInput();
				data.hasUri = hasUri;
				data.acsUri = acsUri;
				data.username = text2;
				data.password = password;
				data.applicationCode = text;
				data.limet = text3;
				data.cskcbbd = hEIN_MEDI_ORG_CODE;
				if (chkBaoLanh.Checked)
				{
					data.registerUseRequest = new RegisterUseRequest
					{
						PatientFullName = ucPatientRaw1.GetValue().PATIENT_NAME,
						PatientDateOfBirth = ucPatientRaw1.GetValue().DOB.ToString(),
						PatientCccd = ucPlusInfo1.GetValue().CCCD_NUMBER,
						RequestAmount = text3,
						ApplicationCode = text,
						Remark = "Thanh toán viện phí cho bệnh nhân " + ucPatientRaw1.GetValue().PATIENT_NAME,
						Signature = ""
					};
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => data), data));
					RegisterUseResponse rs = medicalExpenseGuaranteeProcessor.GuaranteeRegisterUse(data);
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => rs), rs));
					if (rs != null && rs.Success)
					{
						LogSystem.Debug("Gọi api thành công");
						XtraMessageBox.Show(string.Format("Xử lý thành công. Bệnh nhân có thể bảo lãnh {0} đồng", rs.Data.AvailableBalance), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						GuarateeCode = rs.Data.ContractNumber;
						GuaranteeRequestCode = rs.Data.RequestId;
						chkBaoLanh.Checked = true;
						return;
					}
					LogSystem.Debug("Gọi api thất bại");
					RegisterUseResponse registerUseResponse = rs;
					object value;
					if (registerUseResponse == null)
					{
						value = null;
					}
					else
					{
						RegisterUseData data2 = registerUseResponse.Data;
						value = ((data2 != null) ? data2.ErrorMessage : null);
					}
					object obj;
					if (string.IsNullOrWhiteSpace((string)value))
					{
						RegisterUseResponse registerUseResponse2 = rs;
						obj = ((registerUseResponse2 != null) ? registerUseResponse2.Message : null);
					}
					else
					{
						obj = rs.Data.ErrorMessage;
					}
					string arg = (string)obj;
					XtraMessageBox.Show(string.Format("Đăng ký bảo lãnh thất bại. {0} ", arg), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					chkBaoLanh.Checked = false;
				}
				else
				{
					GuarateeCode = null;
					GuaranteeRequestCode = null;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void chkXemTruoc_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				WaitingManager.Show();
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == "chkXemTruoc" && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = (chkXemTruoc.Checked ? "1" : "");
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = "chkXemTruoc";
					controlStateRDO.VALUE = (chkXemTruoc.Checked ? "1" : "");
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
				WaitingManager.Hide();
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
			StopTimer(currentModule.ModuleLink, "timerRefeshAutoCreateBill");
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			DevExpress.XtraLayout.ColumnDefinition columnDefinition = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition3 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition4 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition5 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.ColumnDefinition columnDefinition6 = new DevExpress.XtraLayout.ColumnDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition2 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition3 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition4 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition5 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition6 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition7 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition8 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition9 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition10 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition11 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition12 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition13 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition14 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition15 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition16 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition17 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition18 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.XtraLayout.RowDefinition rowDefinition19 = new DevExpress.XtraLayout.RowDefinition();
			DevExpress.Utils.SuperToolTip superToolTip = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
			this.chkAutoPay = new DevExpress.XtraEditors.CheckEdit();
			this.chkAutoDeposit = new DevExpress.XtraEditors.CheckEdit();
			this.chkAssignDoctor = new DevExpress.XtraEditors.CheckEdit();
			this.chkPrintExam = new DevExpress.XtraEditors.CheckEdit();
			this.chkAutoCreateBill = new DevExpress.XtraEditors.CheckEdit();
			this.chkPrintPatientCard = new DevExpress.XtraEditors.CheckEdit();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lciAutoDeposit = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
			this.ucCheckTT1 = new HIS.UC.UCCheckTT.UCCheckTT();
			this.ucPlusInfo1 = new HIS.UC.PlusInfo.UCPlusInfo();
			this.ucServiceRoomInfo1 = new HIS.UC.UCServiceRoomInfo.UCServiceRoomInfo();
			this.ucImageInfo1 = new HIS.UC.UCImageInfo.UCImageInfo();
			this.ucRelativeInfo1 = new HIS.UC.UCRelativeInfo.UCRelativeInfo();
			this.ucOtherServiceReqInfo1 = new HIS.UC.UCOtherServiceReqInfo.UCOtherServiceReqInfo();
			this.ucHeinInfo1 = new HIS.UC.UCHeniInfo.UCHeinInfo();
			this.ucAddressCombo1 = new HIS.UC.AddressCombo.UCAddressCombo();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.pnlServiceRoomInfomation = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lciUCHeinInfo = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lciUCServiceRoomInfo = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
			this.btnTTChuyenTuyen = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
			this.txtTo = new DevExpress.XtraEditors.TextEdit();
			this.txtFrom = new DevExpress.XtraEditors.TextEdit();
			this.btnGiayTo = new DevExpress.XtraEditors.SimpleButton();
			this.btnDepositRequest = new DevExpress.XtraEditors.SimpleButton();
			this.lblRegisterNumOrder = new DevExpress.XtraEditors.LabelControl();
			this.dropDownButton__Other = new DevExpress.XtraEditors.DropDownButton();
			this.btnRecallPatient = new DevExpress.XtraEditors.SimpleButton();
			this.btnCallPatient = new DevExpress.XtraEditors.SimpleButton();
			this.txtGateNumber = new DevExpress.XtraEditors.ButtonEdit();
			this.txtStepNumber = new DevExpress.XtraEditors.TextEdit();
			this.cboCashierRoom = new DevExpress.XtraEditors.LookUpEdit();
			this.btnSaveAndPrint = new DevExpress.XtraEditors.SimpleButton();
			this.btnPatientNew = new DevExpress.XtraEditors.SimpleButton();
			this.btnSaveAndAssain = new DevExpress.XtraEditors.SimpleButton();
			this.btnDepositDetail = new DevExpress.XtraEditors.SimpleButton();
			this.btnTreatmentBedRoom = new DevExpress.XtraEditors.SimpleButton();
			this.btnNewContinue = new DevExpress.XtraEditors.SimpleButton();
			this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lcibtnPatientNewInfo = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lcibtnDepositDetail = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lciRegisterNumOrder = new DevExpress.XtraLayout.LayoutControlItem();
			this.lcibtnDepositRequest = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.timerInitForm = new System.Windows.Forms.Timer();
			this.timerRefeshAutoCreateBill = new System.Windows.Forms.Timer();
			this.chkSignExam = new DevExpress.XtraEditors.CheckEdit();
			this.ucPatientRaw1 = new HIS.UC.UCPatientRaw.UCPatientRaw();
			this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
			this.chkBaoLanh = new DevExpress.XtraEditors.CheckEdit();
			this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
			this.chkXemTruoc = new DevExpress.XtraEditors.CheckEdit();
			this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControl4).BeginInit();
			this.layoutControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.chkAutoPay.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkAutoDeposit.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkAssignDoctor.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkPrintExam.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkAutoCreateBill.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkPrintPatientCard.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem22).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem27).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciAutoDeposit).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem24).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem26).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem17).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.pnlServiceRoomInfomation).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem18).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciUCHeinInfo).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem20).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem21).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem23).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciUCServiceRoomInfo).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem10).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem19).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem25).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl3).BeginInit();
			this.layoutControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.txtTo.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtFrom.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtGateNumber.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.txtStepNumber.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cboCashierRoom.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lcibtnPatientNewInfo).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem11).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lcibtnDepositDetail).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem13).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem14).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem15).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem9).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem16).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem12).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciRegisterNumOrder).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lcibtnDepositRequest).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem28).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem29).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem30).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl2).BeginInit();
			this.layoutControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkSignExam.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem31).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkBaoLanh.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem32).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkXemTruoc.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem33).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.layoutControl4);
			this.layoutControl1.Controls.Add(this.ucCheckTT1);
			this.layoutControl1.Controls.Add(this.ucPlusInfo1);
			this.layoutControl1.Controls.Add(this.ucServiceRoomInfo1);
			this.layoutControl1.Controls.Add(this.ucImageInfo1);
			this.layoutControl1.Controls.Add(this.ucRelativeInfo1);
			this.layoutControl1.Controls.Add(this.ucOtherServiceReqInfo1);
			this.layoutControl1.Controls.Add(this.ucHeinInfo1);
			this.layoutControl1.Controls.Add(this.ucAddressCombo1);
			this.layoutControl1.Controls.Add(this.ucPatientRaw1);
			this.layoutControl1.Location = new System.Drawing.Point(2, 2);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1363, 853);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.layoutControl4.Controls.Add(this.chkXemTruoc);
			this.layoutControl4.Controls.Add(this.chkBaoLanh);
			this.layoutControl4.Controls.Add(this.chkSignExam);
			this.layoutControl4.Controls.Add(this.chkAutoPay);
			this.layoutControl4.Controls.Add(this.chkAutoDeposit);
			this.layoutControl4.Controls.Add(this.chkAssignDoctor);
			this.layoutControl4.Controls.Add(this.chkPrintExam);
			this.layoutControl4.Controls.Add(this.chkAutoCreateBill);
			this.layoutControl4.Controls.Add(this.chkPrintPatientCard);
			this.layoutControl4.Location = new System.Drawing.Point(908, 763);
			this.layoutControl4.Name = "layoutControl4";
			this.layoutControl4.Root = this.layoutControlGroup3;
			this.layoutControl4.Size = new System.Drawing.Size(455, 90);
			this.layoutControl4.TabIndex = 14;
			this.layoutControl4.Text = "layoutControl4";
			this.chkAutoPay.Location = new System.Drawing.Point(306, 2);
			this.chkAutoPay.Name = "chkAutoPay";
			this.chkAutoPay.Properties.Caption = "Thu tiền qua thẻ";
			this.chkAutoPay.Size = new System.Drawing.Size(147, 19);
			this.chkAutoPay.StyleController = this.layoutControl4;
			this.chkAutoPay.TabIndex = 17;
			this.chkAutoPay.ToolTip = "Tự động thực hiện thu tiền từ tài khoản ngân hàng của bệnh nhân nếu bệnh nhân có thông tin Thẻ khám chữa bệnh thông minh trong trường hợp có check chọn \"Xuất biên lai/hóa đơn\" hoặc \"Tạm thu\"";
			this.chkAutoPay.CheckedChanged += new System.EventHandler(chkAutoPaid_CheckedChanged);
			this.chkAutoDeposit.Location = new System.Drawing.Point(153, 2);
			this.chkAutoDeposit.Name = "chkAutoDeposit";
			this.chkAutoDeposit.Properties.Caption = "Tạm thu";
			this.chkAutoDeposit.Size = new System.Drawing.Size(149, 19);
			this.chkAutoDeposit.StyleController = this.layoutControl4;
			this.chkAutoDeposit.TabIndex = 16;
			this.chkAutoDeposit.ToolTip = "Chỉ tự động tạm thu với bệnh nhân không phải bhyt";
			this.chkAutoDeposit.CheckedChanged += new System.EventHandler(chkAutoDeposit_CheckedChanged);
			this.chkAssignDoctor.Location = new System.Drawing.Point(2, 25);
			this.chkAssignDoctor.Name = "chkAssignDoctor";
			this.chkAssignDoctor.Properties.Caption = "Chỉ định BS";
			this.chkAssignDoctor.Size = new System.Drawing.Size(147, 19);
			this.chkAssignDoctor.StyleController = this.layoutControl4;
			this.chkAssignDoctor.TabIndex = 15;
			this.chkAssignDoctor.ToolTip = "Chỉ định bác sĩ khám";
			this.chkAssignDoctor.CheckedChanged += new System.EventHandler(chkAssignDoctor_CheckedChanged);
			this.chkPrintExam.Location = new System.Drawing.Point(153, 25);
			this.chkPrintExam.Name = "chkPrintExam";
			this.chkPrintExam.Properties.Caption = "In phiếu khám";
			this.chkPrintExam.Size = new System.Drawing.Size(149, 19);
			this.chkPrintExam.StyleController = this.layoutControl4;
			this.chkPrintExam.TabIndex = 14;
			this.chkPrintExam.CheckedChanged += new System.EventHandler(chkPrintExam_CheckedChanged);
			this.chkAutoCreateBill.Location = new System.Drawing.Point(2, 2);
			this.chkAutoCreateBill.Name = "chkAutoCreateBill";
			this.chkAutoCreateBill.Properties.Caption = "Xuất biên lai/hóa đơn";
			this.chkAutoCreateBill.Size = new System.Drawing.Size(147, 19);
			this.chkAutoCreateBill.StyleController = this.layoutControl4;
			this.chkAutoCreateBill.TabIndex = 12;
			this.chkAutoCreateBill.ToolTip = "Chỉ tự động xuất với bệnh nhân không phải bhyt";
			this.chkAutoCreateBill.CheckedChanged += new System.EventHandler(chkAutoCreateBill_CheckedChanged);
			this.chkPrintPatientCard.Location = new System.Drawing.Point(306, 25);
			this.chkPrintPatientCard.Name = "chkPrintPatientCard";
			this.chkPrintPatientCard.Properties.Caption = "In thẻ BN";
			this.chkPrintPatientCard.Size = new System.Drawing.Size(147, 19);
			this.chkPrintPatientCard.StyleController = this.layoutControl4;
			this.chkPrintPatientCard.TabIndex = 13;
			this.chkPrintPatientCard.ToolTip = "Tự động in thẻ bệnh nhân";
			this.chkPrintPatientCard.CheckedChanged += new System.EventHandler(chkPrintPatientCard_CheckedChanged);
			this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup3.GroupBordersVisible = false;
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[9] { this.layoutControlItem22, this.layoutControlItem27, this.lciAutoDeposit, this.layoutControlItem24, this.layoutControlItem26, this.layoutControlItem17, this.layoutControlItem31, this.layoutControlItem32, this.layoutControlItem33 });
			this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(455, 90);
			this.layoutControlGroup3.TextVisible = false;
			this.layoutControlItem22.Control = this.chkAutoCreateBill;
			this.layoutControlItem22.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem22.Name = "layoutControlItem22";
			this.layoutControlItem22.Size = new System.Drawing.Size(151, 23);
			this.layoutControlItem22.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem22.TextVisible = false;
			this.layoutControlItem27.Control = this.chkAssignDoctor;
			this.layoutControlItem27.Location = new System.Drawing.Point(0, 23);
			this.layoutControlItem27.Name = "layoutControlItem27";
			this.layoutControlItem27.Size = new System.Drawing.Size(151, 67);
			this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem27.TextVisible = false;
			this.lciAutoDeposit.Control = this.chkAutoDeposit;
			this.lciAutoDeposit.Location = new System.Drawing.Point(151, 0);
			this.lciAutoDeposit.Name = "lciAutoDeposit";
			this.lciAutoDeposit.Size = new System.Drawing.Size(153, 23);
			this.lciAutoDeposit.TextSize = new System.Drawing.Size(0, 0);
			this.lciAutoDeposit.TextVisible = false;
			this.layoutControlItem24.Control = this.chkPrintPatientCard;
			this.layoutControlItem24.Location = new System.Drawing.Point(304, 23);
			this.layoutControlItem24.Name = "layoutControlItem24";
			this.layoutControlItem24.Size = new System.Drawing.Size(151, 23);
			this.layoutControlItem24.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem24.TextVisible = false;
			this.layoutControlItem26.Control = this.chkPrintExam;
			this.layoutControlItem26.Location = new System.Drawing.Point(151, 23);
			this.layoutControlItem26.Name = "layoutControlItem26";
			this.layoutControlItem26.Size = new System.Drawing.Size(153, 23);
			this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem26.TextVisible = false;
			this.layoutControlItem17.Control = this.chkAutoPay;
			this.layoutControlItem17.Location = new System.Drawing.Point(304, 0);
			this.layoutControlItem17.Name = "layoutControlItem17";
			this.layoutControlItem17.Size = new System.Drawing.Size(151, 23);
			this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem17.TextVisible = false;
			this.ucCheckTT1.Location = new System.Drawing.Point(910, 360);
			this.ucCheckTT1.Margin = new System.Windows.Forms.Padding(4);
			this.ucCheckTT1.Name = "ucCheckTT1";
			this.ucCheckTT1.Size = new System.Drawing.Size(451, 401);
			this.ucCheckTT1.TabIndex = 11;
			this.ucPlusInfo1.Location = new System.Drawing.Point(454, 493);
			this.ucPlusInfo1.Margin = new System.Windows.Forms.Padding(4);
			this.ucPlusInfo1.Name = "ucPlusInfo1";
			this.ucPlusInfo1.Size = new System.Drawing.Size(454, 360);
			this.ucPlusInfo1.TabIndex = 10;
			this.ucServiceRoomInfo1.Location = new System.Drawing.Point(0, 718);
			this.ucServiceRoomInfo1.Margin = new System.Windows.Forms.Padding(4);
			this.ucServiceRoomInfo1.Name = "ucServiceRoomInfo1";
			this.ucServiceRoomInfo1.Size = new System.Drawing.Size(454, 135);
			this.ucServiceRoomInfo1.TabIndex = 3;
			this.ucImageInfo1.Location = new System.Drawing.Point(910, 2);
			this.ucImageInfo1.Margin = new System.Windows.Forms.Padding(4);
			this.ucImageInfo1.Name = "ucImageInfo1";
			this.ucImageInfo1.Size = new System.Drawing.Size(451, 354);
			this.ucImageInfo1.TabIndex = 7;
			this.ucRelativeInfo1.Location = new System.Drawing.Point(454, 0);
			this.ucRelativeInfo1.Margin = new System.Windows.Forms.Padding(4);
			this.ucRelativeInfo1.Name = "ucRelativeInfo1";
			this.ucRelativeInfo1.Size = new System.Drawing.Size(454, 178);
			this.ucRelativeInfo1.TabIndex = 5;
			this.ucOtherServiceReqInfo1.Location = new System.Drawing.Point(454, 178);
			this.ucOtherServiceReqInfo1.Margin = new System.Windows.Forms.Padding(4);
			this.ucOtherServiceReqInfo1.Name = "ucOtherServiceReqInfo1";
			this.ucOtherServiceReqInfo1.Size = new System.Drawing.Size(454, 315);
			this.ucOtherServiceReqInfo1.TabIndex = 4;
			this.ucHeinInfo1.Location = new System.Drawing.Point(0, 403);
			this.ucHeinInfo1.Margin = new System.Windows.Forms.Padding(4);
			this.ucHeinInfo1.Name = "ucHeinInfo1";
			this.ucHeinInfo1.Size = new System.Drawing.Size(454, 315);
			this.ucHeinInfo1.TabIndex = 2;
			this.ucAddressCombo1.Location = new System.Drawing.Point(0, 178);
			this.ucAddressCombo1.Margin = new System.Windows.Forms.Padding(4);
			this.ucAddressCombo1.Name = "ucAddressCombo1";
			this.ucAddressCombo1.Size = new System.Drawing.Size(454, 225);
			this.ucAddressCombo1.TabIndex = 1;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[10] { this.pnlServiceRoomInfomation, this.layoutControlItem18, this.lciUCHeinInfo, this.layoutControlItem20, this.layoutControlItem21, this.layoutControlItem23, this.lciUCServiceRoomInfo, this.layoutControlItem10, this.layoutControlItem19, this.layoutControlItem25 });
			this.layoutControlGroup1.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			columnDefinition.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition.Width = 16.629942706865783;
			columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition2.Width = 16.629942706865783;
			columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition3.Width = 16.629942706865783;
			columnDefinition4.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition4.Width = 16.629942706865783;
			columnDefinition5.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition5.Width = 16.629942706865783;
			columnDefinition6.SizeType = System.Windows.Forms.SizeType.Percent;
			columnDefinition6.Width = 16.629942706865783;
			this.layoutControlGroup1.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[6] { columnDefinition, columnDefinition2, columnDefinition3, columnDefinition4, columnDefinition5, columnDefinition6 });
			rowDefinition.Height = 5.263157894736842;
			rowDefinition.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition2.Height = 5.263157894736842;
			rowDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition3.Height = 5.263157894736842;
			rowDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition4.Height = 5.263157894736842;
			rowDefinition4.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition5.Height = 5.263157894736842;
			rowDefinition5.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition6.Height = 5.263157894736842;
			rowDefinition6.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition7.Height = 5.263157894736842;
			rowDefinition7.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition8.Height = 5.263157894736842;
			rowDefinition8.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition9.Height = 5.263157894736842;
			rowDefinition9.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition10.Height = 5.263157894736842;
			rowDefinition10.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition11.Height = 5.263157894736842;
			rowDefinition11.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition12.Height = 5.263157894736842;
			rowDefinition12.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition13.Height = 5.263157894736842;
			rowDefinition13.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition14.Height = 5.263157894736842;
			rowDefinition14.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition15.Height = 5.263157894736842;
			rowDefinition15.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition16.Height = 5.263157894736842;
			rowDefinition16.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition17.Height = 5.263157894736842;
			rowDefinition17.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition18.Height = 5.263157894736842;
			rowDefinition18.SizeType = System.Windows.Forms.SizeType.Percent;
			rowDefinition19.Height = 5.263157894736842;
			rowDefinition19.SizeType = System.Windows.Forms.SizeType.Percent;
			this.layoutControlGroup1.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[19]
			{
				rowDefinition, rowDefinition2, rowDefinition3, rowDefinition4, rowDefinition5, rowDefinition6, rowDefinition7, rowDefinition8, rowDefinition9, rowDefinition10,
				rowDefinition11, rowDefinition12, rowDefinition13, rowDefinition14, rowDefinition15, rowDefinition16, rowDefinition17, rowDefinition18, rowDefinition19
			});
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1363, 853);
			this.layoutControlGroup1.TextVisible = false;
			this.ucPatientRaw1.isAlertTreatmentEndInDay = false;
			this.ucPatientRaw1.Location = new System.Drawing.Point(0, 0);
			this.ucPatientRaw1.Margin = new System.Windows.Forms.Padding(4);
			this.ucPatientRaw1.Name = "ucPatientRaw1";
			this.ucPatientRaw1.ResultDataADO = null;
			this.ucPatientRaw1.Size = new System.Drawing.Size(454, 178);
			this.ucPatientRaw1.TabIndex = 0;
			this.pnlServiceRoomInfomation.Control = this.ucPatientRaw1;
			this.pnlServiceRoomInfomation.Location = new System.Drawing.Point(0, 0);
			this.pnlServiceRoomInfomation.Name = "pnlServiceRoomInfomation";
			this.pnlServiceRoomInfomation.OptionsTableLayoutItem.ColumnSpan = 2;
			this.pnlServiceRoomInfomation.OptionsTableLayoutItem.RowSpan = 4;
			this.pnlServiceRoomInfomation.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.pnlServiceRoomInfomation.Size = new System.Drawing.Size(454, 178);
			this.pnlServiceRoomInfomation.TextSize = new System.Drawing.Size(0, 0);
			this.pnlServiceRoomInfomation.TextVisible = false;
			this.layoutControlItem18.Control = this.ucAddressCombo1;
			this.layoutControlItem18.Location = new System.Drawing.Point(0, 178);
			this.layoutControlItem18.Name = "layoutControlItem18";
			this.layoutControlItem18.OptionsTableLayoutItem.ColumnSpan = 2;
			this.layoutControlItem18.OptionsTableLayoutItem.RowIndex = 4;
			this.layoutControlItem18.OptionsTableLayoutItem.RowSpan = 5;
			this.layoutControlItem18.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem18.Size = new System.Drawing.Size(454, 225);
			this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem18.TextVisible = false;
			this.lciUCHeinInfo.Control = this.ucHeinInfo1;
			this.lciUCHeinInfo.Location = new System.Drawing.Point(0, 403);
			this.lciUCHeinInfo.Name = "lciUCHeinInfo";
			this.lciUCHeinInfo.OptionsTableLayoutItem.ColumnSpan = 2;
			this.lciUCHeinInfo.OptionsTableLayoutItem.RowIndex = 9;
			this.lciUCHeinInfo.OptionsTableLayoutItem.RowSpan = 7;
			this.lciUCHeinInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lciUCHeinInfo.Size = new System.Drawing.Size(454, 315);
			this.lciUCHeinInfo.TextSize = new System.Drawing.Size(0, 0);
			this.lciUCHeinInfo.TextVisible = false;
			this.layoutControlItem20.Control = this.ucOtherServiceReqInfo1;
			this.layoutControlItem20.Location = new System.Drawing.Point(454, 178);
			this.layoutControlItem20.Name = "layoutControlItem20";
			this.layoutControlItem20.OptionsTableLayoutItem.ColumnIndex = 2;
			this.layoutControlItem20.OptionsTableLayoutItem.ColumnSpan = 2;
			this.layoutControlItem20.OptionsTableLayoutItem.RowIndex = 4;
			this.layoutControlItem20.OptionsTableLayoutItem.RowSpan = 7;
			this.layoutControlItem20.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem20.Size = new System.Drawing.Size(454, 315);
			this.layoutControlItem20.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem20.TextVisible = false;
			this.layoutControlItem21.Control = this.ucRelativeInfo1;
			this.layoutControlItem21.Location = new System.Drawing.Point(454, 0);
			this.layoutControlItem21.Name = "layoutControlItem21";
			this.layoutControlItem21.OptionsTableLayoutItem.ColumnIndex = 2;
			this.layoutControlItem21.OptionsTableLayoutItem.ColumnSpan = 2;
			this.layoutControlItem21.OptionsTableLayoutItem.RowSpan = 4;
			this.layoutControlItem21.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem21.Size = new System.Drawing.Size(454, 178);
			this.layoutControlItem21.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem21.TextVisible = false;
			this.layoutControlItem23.Control = this.ucImageInfo1;
			this.layoutControlItem23.Location = new System.Drawing.Point(908, 0);
			this.layoutControlItem23.Name = "layoutControlItem23";
			this.layoutControlItem23.OptionsTableLayoutItem.ColumnIndex = 4;
			this.layoutControlItem23.OptionsTableLayoutItem.ColumnSpan = 2;
			this.layoutControlItem23.OptionsTableLayoutItem.RowSpan = 8;
			this.layoutControlItem23.Size = new System.Drawing.Size(455, 358);
			this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem23.TextVisible = false;
			this.lciUCServiceRoomInfo.Control = this.ucServiceRoomInfo1;
			this.lciUCServiceRoomInfo.Location = new System.Drawing.Point(0, 718);
			this.lciUCServiceRoomInfo.Name = "lciUCServiceRoomInfo";
			this.lciUCServiceRoomInfo.OptionsTableLayoutItem.ColumnSpan = 2;
			this.lciUCServiceRoomInfo.OptionsTableLayoutItem.RowIndex = 16;
			this.lciUCServiceRoomInfo.OptionsTableLayoutItem.RowSpan = 3;
			this.lciUCServiceRoomInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.lciUCServiceRoomInfo.Size = new System.Drawing.Size(454, 135);
			this.lciUCServiceRoomInfo.TextSize = new System.Drawing.Size(0, 0);
			this.lciUCServiceRoomInfo.TextVisible = false;
			this.layoutControlItem10.Control = this.ucPlusInfo1;
			this.layoutControlItem10.Location = new System.Drawing.Point(454, 493);
			this.layoutControlItem10.Name = "layoutControlItem10";
			this.layoutControlItem10.OptionsTableLayoutItem.ColumnIndex = 2;
			this.layoutControlItem10.OptionsTableLayoutItem.ColumnSpan = 2;
			this.layoutControlItem10.OptionsTableLayoutItem.RowIndex = 11;
			this.layoutControlItem10.OptionsTableLayoutItem.RowSpan = 8;
			this.layoutControlItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem10.Size = new System.Drawing.Size(454, 360);
			this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem10.TextVisible = false;
			this.layoutControlItem19.Control = this.ucCheckTT1;
			this.layoutControlItem19.Location = new System.Drawing.Point(908, 358);
			this.layoutControlItem19.Name = "layoutControlItem19";
			this.layoutControlItem19.OptionsTableLayoutItem.ColumnIndex = 4;
			this.layoutControlItem19.OptionsTableLayoutItem.ColumnSpan = 2;
			this.layoutControlItem19.OptionsTableLayoutItem.RowIndex = 8;
			this.layoutControlItem19.OptionsTableLayoutItem.RowSpan = 9;
			this.layoutControlItem19.Size = new System.Drawing.Size(455, 405);
			this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem19.TextVisible = false;
			this.layoutControlItem25.Control = this.layoutControl4;
			this.layoutControlItem25.Location = new System.Drawing.Point(908, 763);
			this.layoutControlItem25.Name = "layoutControlItem25";
			this.layoutControlItem25.OptionsTableLayoutItem.ColumnIndex = 4;
			this.layoutControlItem25.OptionsTableLayoutItem.ColumnSpan = 2;
			this.layoutControlItem25.OptionsTableLayoutItem.RowIndex = 17;
			this.layoutControlItem25.OptionsTableLayoutItem.RowSpan = 2;
			this.layoutControlItem25.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem25.Size = new System.Drawing.Size(455, 90);
			this.layoutControlItem25.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem25.TextVisible = false;
			this.btnTTChuyenTuyen.Location = new System.Drawing.Point(750, 2);
			this.btnTTChuyenTuyen.Name = "btnTTChuyenTuyen";
			this.btnTTChuyenTuyen.Size = new System.Drawing.Size(77, 31);
			this.btnTTChuyenTuyen.StyleController = this.layoutControl3;
			this.btnTTChuyenTuyen.TabIndex = 8;
			this.btnTTChuyenTuyen.Text = "Chuyển tuyến";
			this.btnTTChuyenTuyen.Click += new System.EventHandler(btnTTChuyenTuyen_Click);
			this.layoutControl3.Controls.Add(this.txtTo);
			this.layoutControl3.Controls.Add(this.txtFrom);
			this.layoutControl3.Controls.Add(this.btnGiayTo);
			this.layoutControl3.Controls.Add(this.btnDepositRequest);
			this.layoutControl3.Controls.Add(this.lblRegisterNumOrder);
			this.layoutControl3.Controls.Add(this.dropDownButton__Other);
			this.layoutControl3.Controls.Add(this.btnRecallPatient);
			this.layoutControl3.Controls.Add(this.btnTTChuyenTuyen);
			this.layoutControl3.Controls.Add(this.btnCallPatient);
			this.layoutControl3.Controls.Add(this.txtGateNumber);
			this.layoutControl3.Controls.Add(this.txtStepNumber);
			this.layoutControl3.Controls.Add(this.cboCashierRoom);
			this.layoutControl3.Controls.Add(this.btnSaveAndPrint);
			this.layoutControl3.Controls.Add(this.btnPatientNew);
			this.layoutControl3.Controls.Add(this.btnSaveAndAssain);
			this.layoutControl3.Controls.Add(this.btnDepositDetail);
			this.layoutControl3.Controls.Add(this.btnTreatmentBedRoom);
			this.layoutControl3.Controls.Add(this.btnNewContinue);
			this.layoutControl3.Controls.Add(this.btnPrint);
			this.layoutControl3.Controls.Add(this.btnSave);
			this.layoutControl3.Location = new System.Drawing.Point(2, 859);
			this.layoutControl3.Name = "layoutControl3";
			this.layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(44, 269, 250, 350);
			this.layoutControl3.Root = this.Root;
			this.layoutControl3.Size = new System.Drawing.Size(1363, 35);
			this.layoutControl3.TabIndex = 4;
			this.layoutControl3.Text = "layoutControl3";
			this.txtTo.EditValue = "0";
			this.txtTo.Location = new System.Drawing.Point(163, 7);
			this.txtTo.Name = "txtTo";
			this.txtTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtTo.Properties.NullText = "0";
			this.txtTo.Size = new System.Drawing.Size(50, 20);
			this.txtTo.StyleController = this.layoutControl3;
			this.txtTo.TabIndex = 82;
			this.txtTo.ToolTipTitle = "Đến";
			this.txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTo_KeyPress);
			this.txtFrom.EditValue = "0";
			this.txtFrom.Location = new System.Drawing.Point(109, 7);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.txtFrom.Properties.NullText = "0";
			this.txtFrom.Size = new System.Drawing.Size(50, 20);
			this.txtFrom.StyleController = this.layoutControl3;
			this.txtFrom.TabIndex = 81;
			this.txtFrom.ToolTipTitle = "Từ";
			this.txtFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFrom_KeyPress);
			this.btnGiayTo.Enabled = false;
			this.btnGiayTo.Location = new System.Drawing.Point(831, 2);
			this.btnGiayTo.Margin = new System.Windows.Forms.Padding(2);
			this.btnGiayTo.Name = "btnGiayTo";
			this.btnGiayTo.Size = new System.Drawing.Size(56, 31);
			this.btnGiayTo.StyleController = this.layoutControl3;
			toolTipItem.Text = "Hồ sơ giấy tờ đính kèm";
			superToolTip.Items.Add(toolTipItem);
			this.btnGiayTo.SuperTip = superToolTip;
			this.btnGiayTo.TabIndex = 80;
			this.btnGiayTo.Text = "Giấy tờ";
			this.btnGiayTo.Click += new System.EventHandler(btnGiayTo_Click);
			this.btnDepositRequest.Enabled = false;
			this.btnDepositRequest.Location = new System.Drawing.Point(598, 2);
			this.btnDepositRequest.Name = "btnDepositRequest";
			this.btnDepositRequest.Size = new System.Drawing.Size(58, 31);
			this.btnDepositRequest.StyleController = this.layoutControl3;
			this.btnDepositRequest.TabIndex = 79;
			this.btnDepositRequest.Text = "YC tạm ứng";
			this.btnDepositRequest.Click += new System.EventHandler(btnDepositRequest_Click);
			this.lblRegisterNumOrder.Appearance.Font = new System.Drawing.Font("Tahoma", 10f, System.Drawing.FontStyle.Bold);
			this.lblRegisterNumOrder.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.lblRegisterNumOrder.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lblRegisterNumOrder.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblRegisterNumOrder.Location = new System.Drawing.Point(314, 10);
			this.lblRegisterNumOrder.Name = "lblRegisterNumOrder";
			this.lblRegisterNumOrder.Size = new System.Drawing.Size(48, 16);
			this.lblRegisterNumOrder.StyleController = this.layoutControl3;
			this.lblRegisterNumOrder.TabIndex = 78;
			this.dropDownButton__Other.Enabled = false;
			this.dropDownButton__Other.Location = new System.Drawing.Point(526, 2);
			this.dropDownButton__Other.Name = "dropDownButton__Other";
			this.dropDownButton__Other.Size = new System.Drawing.Size(68, 31);
			this.dropDownButton__Other.StyleController = this.layoutControl3;
			this.dropDownButton__Other.TabIndex = 77;
			this.dropDownButton__Other.Text = "Khác";
			this.dropDownButton__Other.Click += new System.EventHandler(dropDownButton__Other_Click);
			this.btnRecallPatient.Location = new System.Drawing.Point(261, 2);
			this.btnRecallPatient.Name = "btnRecallPatient";
			this.btnRecallPatient.Size = new System.Drawing.Size(49, 31);
			this.btnRecallPatient.StyleController = this.layoutControl3;
			this.btnRecallPatient.TabIndex = 76;
			this.btnRecallPatient.Text = "Gọi lại (F6)";
			this.btnRecallPatient.ToolTip = "Gọi lại (F6)";
			this.btnRecallPatient.Click += new System.EventHandler(btnRecallPatient_Click);
			this.btnCallPatient.Location = new System.Drawing.Point(217, 2);
			this.btnCallPatient.Name = "btnCallPatient";
			this.btnCallPatient.Size = new System.Drawing.Size(40, 31);
			this.btnCallPatient.StyleController = this.layoutControl3;
			this.btnCallPatient.TabIndex = 75;
			this.btnCallPatient.Text = "Gọi (F5)";
			this.btnCallPatient.Click += new System.EventHandler(btnCallPatient_Click);
			this.txtGateNumber.EditValue = "";
			this.txtGateNumber.Location = new System.Drawing.Point(2, 7);
			this.txtGateNumber.Name = "txtGateNumber";
			this.txtGateNumber.Properties.Appearance.Options.UseTextOptions = true;
			this.txtGateNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.txtGateNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "Thiết lập thông báo khi gọi/gọi lại", null, null, true)
			});
			this.txtGateNumber.Properties.NullValuePrompt = "Cổng";
			this.txtGateNumber.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtGateNumber.Properties.ShowNullValuePromptWhenFocused = true;
			this.txtGateNumber.Size = new System.Drawing.Size(58, 20);
			this.txtGateNumber.StyleController = this.layoutControl3;
			this.txtGateNumber.TabIndex = 74;
			this.txtGateNumber.ToolTip = "Nhập \"Mã cổng\" trong trường hợp các cổng sử dụng riêng dãy số thứ tự hoặc nhập theo định dạng \"Mã cổng:Mã dãy\" trong trường hợp các cổng sử dụng chung dãy số thứ tự.";
			this.txtGateNumber.ToolTipTitle = "Cổng";
			this.txtGateNumber.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(txtGateNumber_ButtonClick);
			this.txtGateNumber.Leave += new System.EventHandler(txtGateNumber_Leave);
			this.txtStepNumber.Location = new System.Drawing.Point(64, 7);
			this.txtStepNumber.Name = "txtStepNumber";
			this.txtStepNumber.Properties.Appearance.Options.UseTextOptions = true;
			this.txtStepNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.txtStepNumber.Properties.MaxLength = 5;
			this.txtStepNumber.Properties.NullValuePrompt = "Bước nhảy";
			this.txtStepNumber.Properties.NullValuePromptShowForEmptyValue = true;
			this.txtStepNumber.Properties.ShowNullValuePromptWhenFocused = true;
			this.txtStepNumber.Size = new System.Drawing.Size(41, 20);
			this.txtStepNumber.StyleController = this.layoutControl3;
			this.txtStepNumber.TabIndex = 73;
			this.txtStepNumber.ToolTipTitle = "Bước nhảy";
			this.txtStepNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtStepNumber_KeyPress);
			this.txtStepNumber.Leave += new System.EventHandler(txtStepNumber_Leave);
			this.cboCashierRoom.Location = new System.Drawing.Point(474, 7);
			this.cboCashierRoom.Name = "cboCashierRoom";
			this.cboCashierRoom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.cboCashierRoom.Properties.NullText = "";
			this.cboCashierRoom.Size = new System.Drawing.Size(48, 20);
			this.cboCashierRoom.StyleController = this.layoutControl3;
			this.cboCashierRoom.TabIndex = 64;
			this.btnSaveAndPrint.Location = new System.Drawing.Point(1133, 2);
			this.btnSaveAndPrint.Name = "btnSaveAndPrint";
			this.btnSaveAndPrint.Size = new System.Drawing.Size(77, 31);
			this.btnSaveAndPrint.StyleController = this.layoutControl3;
			this.btnSaveAndPrint.TabIndex = 70;
			this.btnSaveAndPrint.Text = "Lưu in (Ctrl I/F8)";
			this.btnSaveAndPrint.Click += new System.EventHandler(btnSaveAndPrint_Click);
			this.btnPatientNew.Appearance.ForeColor = System.Drawing.Color.Green;
			this.btnPatientNew.Appearance.Options.UseForeColor = true;
			this.btnPatientNew.Appearance.Options.UseTextOptions = true;
			this.btnPatientNew.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.btnPatientNew.Location = new System.Drawing.Point(366, 2);
			this.btnPatientNew.Name = "btnPatientNew";
			this.btnPatientNew.Size = new System.Drawing.Size(64, 31);
			this.btnPatientNew.StyleController = this.layoutControl3;
			this.btnPatientNew.TabIndex = 63;
			this.btnPatientNew.Text = "BN mới (Ctrl R)";
			this.btnPatientNew.ToolTip = "Bệnh nhân mới (Ctrl R)";
			this.btnPatientNew.Visible = false;
			this.btnPatientNew.Click += new System.EventHandler(btnPatientNew_Click);
			this.btnSaveAndAssain.Appearance.Options.UseTextOptions = true;
			this.btnSaveAndAssain.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.btnSaveAndAssain.Enabled = false;
			this.btnSaveAndAssain.Location = new System.Drawing.Point(966, 2);
			this.btnSaveAndAssain.Name = "btnSaveAndAssain";
			this.btnSaveAndAssain.Size = new System.Drawing.Size(90, 31);
			this.btnSaveAndAssain.StyleController = this.layoutControl3;
			this.btnSaveAndAssain.TabIndex = 68;
			this.btnSaveAndAssain.Text = "Chỉ định (Ctrl D)";
			this.btnSaveAndAssain.Click += new System.EventHandler(btnSaveAndAssain_Click);
			this.btnDepositDetail.Enabled = false;
			this.btnDepositDetail.Location = new System.Drawing.Point(660, 2);
			this.btnDepositDetail.Name = "btnDepositDetail";
			this.btnDepositDetail.Size = new System.Drawing.Size(86, 31);
			this.btnDepositDetail.StyleController = this.layoutControl3;
			this.btnDepositDetail.TabIndex = 65;
			this.btnDepositDetail.Text = "Tạm ứng (Ctrl T)";
			this.btnDepositDetail.Click += new System.EventHandler(btnDepositDetail_Click);
			this.btnTreatmentBedRoom.Appearance.Options.UseTextOptions = true;
			this.btnTreatmentBedRoom.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.btnTreatmentBedRoom.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Character;
			this.btnTreatmentBedRoom.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.btnTreatmentBedRoom.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.btnTreatmentBedRoom.Enabled = false;
			this.btnTreatmentBedRoom.Location = new System.Drawing.Point(891, 2);
			this.btnTreatmentBedRoom.Name = "btnTreatmentBedRoom";
			this.btnTreatmentBedRoom.Size = new System.Drawing.Size(71, 31);
			this.btnTreatmentBedRoom.StyleController = this.layoutControl3;
			this.btnTreatmentBedRoom.TabIndex = 67;
			this.btnTreatmentBedRoom.Text = "Vào buồng (Ctrl G)";
			this.btnTreatmentBedRoom.Click += new System.EventHandler(btnTreatmentBedRoom_Click);
			this.btnNewContinue.Location = new System.Drawing.Point(1280, 2);
			this.btnNewContinue.Name = "btnNewContinue";
			this.btnNewContinue.Size = new System.Drawing.Size(81, 31);
			this.btnNewContinue.StyleController = this.layoutControl3;
			this.btnNewContinue.TabIndex = 72;
			this.btnNewContinue.Text = "Mới (Ctrl N/F1)";
			this.btnNewContinue.Click += new System.EventHandler(btnNewContinue_Click);
			this.btnPrint.Enabled = false;
			this.btnPrint.Location = new System.Drawing.Point(1214, 2);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(62, 31);
			this.btnPrint.StyleController = this.layoutControl3;
			this.btnPrint.TabIndex = 71;
			this.btnPrint.Text = "In (Ctrl P)";
			this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
			this.btnSave.Location = new System.Drawing.Point(1060, 2);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(69, 31);
			this.btnSave.StyleController = this.layoutControl3;
			this.btnSave.TabIndex = 69;
			this.btnSave.Text = "Lưu (Ctrl S)";
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[21]
			{
				this.layoutControlItem3, this.layoutControlItem4, this.layoutControlItem5, this.layoutControlItem6, this.layoutControlItem8, this.lcibtnPatientNewInfo, this.layoutControlItem11, this.lcibtnDepositDetail, this.layoutControlItem13, this.layoutControlItem14,
				this.layoutControlItem15, this.layoutControlItem9, this.layoutControlItem16, this.layoutControlItem7, this.emptySpaceItem1, this.layoutControlItem12, this.lciRegisterNumOrder, this.lcibtnDepositRequest, this.layoutControlItem28, this.layoutControlItem29,
				this.layoutControlItem30
			});
			this.Root.Location = new System.Drawing.Point(0, 0);
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1363, 35);
			this.Root.TextVisible = false;
			this.layoutControlItem3.Control = this.btnRecallPatient;
			this.layoutControlItem3.Location = new System.Drawing.Point(259, 0);
			this.layoutControlItem3.MaxSize = new System.Drawing.Size(71, 35);
			this.layoutControlItem3.MinSize = new System.Drawing.Size(1, 35);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(53, 35);
			this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			this.layoutControlItem4.Control = this.btnCallPatient;
			this.layoutControlItem4.Location = new System.Drawing.Point(215, 0);
			this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem4.MinSize = new System.Drawing.Size(1, 35);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(44, 35);
			this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			this.layoutControlItem5.Control = this.txtGateNumber;
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem5.MinSize = new System.Drawing.Size(1, 35);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.OptionsToolTip.ToolTip = "Nhập \"Mã cổng\" trong trường hợp các cổng sử dụng riêng dãy số thứ tự hoặc nhập theo định dạng \"Mã cổng:Mã dãy\" trong trường hợp các cổng sử dụng chung dãy số thứ tự.";
			this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2);
			this.layoutControlItem5.Size = new System.Drawing.Size(62, 35);
			this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			this.layoutControlItem6.Control = this.txtStepNumber;
			this.layoutControlItem6.Location = new System.Drawing.Point(62, 0);
			this.layoutControlItem6.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem6.MinSize = new System.Drawing.Size(1, 35);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2);
			this.layoutControlItem6.Size = new System.Drawing.Size(45, 35);
			this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			this.layoutControlItem8.Control = this.cboCashierRoom;
			this.layoutControlItem8.Location = new System.Drawing.Point(446, 0);
			this.layoutControlItem8.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem8.MinSize = new System.Drawing.Size(1, 35);
			this.layoutControlItem8.Name = "layoutControlItem8";
			this.layoutControlItem8.OptionsToolTip.ToolTip = "Phòng thu ngân";
			this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2);
			this.layoutControlItem8.Size = new System.Drawing.Size(78, 35);
			this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem8.Text = "PTN:";
			this.layoutControlItem8.TextSize = new System.Drawing.Size(23, 13);
			this.lcibtnPatientNewInfo.Control = this.btnPatientNew;
			this.lcibtnPatientNewInfo.Location = new System.Drawing.Point(364, 0);
			this.lcibtnPatientNewInfo.MaxSize = new System.Drawing.Size(0, 35);
			this.lcibtnPatientNewInfo.MinSize = new System.Drawing.Size(1, 35);
			this.lcibtnPatientNewInfo.Name = "lcibtnPatientNewInfo";
			this.lcibtnPatientNewInfo.Size = new System.Drawing.Size(68, 35);
			this.lcibtnPatientNewInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.lcibtnPatientNewInfo.TextSize = new System.Drawing.Size(0, 0);
			this.lcibtnPatientNewInfo.TextVisible = false;
			this.lcibtnPatientNewInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			this.layoutControlItem11.Control = this.btnSaveAndAssain;
			this.layoutControlItem11.Location = new System.Drawing.Point(964, 0);
			this.layoutControlItem11.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem11.MinSize = new System.Drawing.Size(91, 35);
			this.layoutControlItem11.Name = "layoutControlItem11";
			this.layoutControlItem11.Size = new System.Drawing.Size(94, 35);
			this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem11.TextVisible = false;
			this.lcibtnDepositDetail.Control = this.btnDepositDetail;
			this.lcibtnDepositDetail.Location = new System.Drawing.Point(658, 0);
			this.lcibtnDepositDetail.MaxSize = new System.Drawing.Size(0, 35);
			this.lcibtnDepositDetail.MinSize = new System.Drawing.Size(90, 35);
			this.lcibtnDepositDetail.Name = "lcibtnDepositDetail";
			this.lcibtnDepositDetail.Size = new System.Drawing.Size(90, 35);
			this.lcibtnDepositDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.lcibtnDepositDetail.TextSize = new System.Drawing.Size(0, 0);
			this.lcibtnDepositDetail.TextVisible = false;
			this.layoutControlItem13.Control = this.btnTreatmentBedRoom;
			this.layoutControlItem13.Location = new System.Drawing.Point(889, 0);
			this.layoutControlItem13.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem13.MinSize = new System.Drawing.Size(1, 35);
			this.layoutControlItem13.Name = "layoutControlItem13";
			this.layoutControlItem13.Size = new System.Drawing.Size(75, 35);
			this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem13.TextVisible = false;
			this.layoutControlItem14.Control = this.btnNewContinue;
			this.layoutControlItem14.Location = new System.Drawing.Point(1278, 0);
			this.layoutControlItem14.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem14.MinSize = new System.Drawing.Size(82, 35);
			this.layoutControlItem14.Name = "layoutControlItem14";
			this.layoutControlItem14.Size = new System.Drawing.Size(85, 35);
			this.layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem14.TextVisible = false;
			this.layoutControlItem15.Control = this.btnPrint;
			this.layoutControlItem15.Location = new System.Drawing.Point(1212, 0);
			this.layoutControlItem15.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem15.MinSize = new System.Drawing.Size(64, 35);
			this.layoutControlItem15.Name = "layoutControlItem15";
			this.layoutControlItem15.Size = new System.Drawing.Size(66, 35);
			this.layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem15.TextVisible = false;
			this.layoutControlItem9.Control = this.btnSaveAndPrint;
			this.layoutControlItem9.Location = new System.Drawing.Point(1131, 0);
			this.layoutControlItem9.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem9.MinSize = new System.Drawing.Size(79, 35);
			this.layoutControlItem9.Name = "layoutControlItem9";
			this.layoutControlItem9.Size = new System.Drawing.Size(81, 35);
			this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem9.TextVisible = false;
			this.layoutControlItem16.Control = this.btnSave;
			this.layoutControlItem16.Location = new System.Drawing.Point(1058, 0);
			this.layoutControlItem16.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem16.MinSize = new System.Drawing.Size(71, 35);
			this.layoutControlItem16.Name = "layoutControlItem16";
			this.layoutControlItem16.Size = new System.Drawing.Size(73, 35);
			this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem16.TextVisible = false;
			this.layoutControlItem7.Control = this.btnTTChuyenTuyen;
			this.layoutControlItem7.Location = new System.Drawing.Point(748, 0);
			this.layoutControlItem7.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem7.MinSize = new System.Drawing.Size(81, 35);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Size = new System.Drawing.Size(81, 35);
			this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem7.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(432, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(14, 35);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem12.Control = this.dropDownButton__Other;
			this.layoutControlItem12.Location = new System.Drawing.Point(524, 0);
			this.layoutControlItem12.MaxSize = new System.Drawing.Size(0, 36);
			this.layoutControlItem12.MinSize = new System.Drawing.Size(70, 35);
			this.layoutControlItem12.Name = "layoutControlItem12";
			this.layoutControlItem12.Size = new System.Drawing.Size(72, 35);
			this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem12.TextVisible = false;
			this.lciRegisterNumOrder.Control = this.lblRegisterNumOrder;
			this.lciRegisterNumOrder.Location = new System.Drawing.Point(312, 0);
			this.lciRegisterNumOrder.Name = "lciRegisterNumOrder";
			this.lciRegisterNumOrder.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 2);
			this.lciRegisterNumOrder.Size = new System.Drawing.Size(52, 35);
			this.lciRegisterNumOrder.TextSize = new System.Drawing.Size(0, 0);
			this.lciRegisterNumOrder.TextVisible = false;
			this.lciRegisterNumOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
			this.lcibtnDepositRequest.Control = this.btnDepositRequest;
			this.lcibtnDepositRequest.Location = new System.Drawing.Point(596, 0);
			this.lcibtnDepositRequest.MaxSize = new System.Drawing.Size(0, 35);
			this.lcibtnDepositRequest.MinSize = new System.Drawing.Size(60, 35);
			this.lcibtnDepositRequest.Name = "lcibtnDepositRequest";
			this.lcibtnDepositRequest.Size = new System.Drawing.Size(62, 35);
			this.lcibtnDepositRequest.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.lcibtnDepositRequest.TextSize = new System.Drawing.Size(0, 0);
			this.lcibtnDepositRequest.TextVisible = false;
			this.layoutControlItem28.Control = this.btnGiayTo;
			this.layoutControlItem28.Location = new System.Drawing.Point(829, 0);
			this.layoutControlItem28.MaxSize = new System.Drawing.Size(0, 35);
			this.layoutControlItem28.MinSize = new System.Drawing.Size(60, 35);
			this.layoutControlItem28.Name = "layoutControlItem28";
			this.layoutControlItem28.Size = new System.Drawing.Size(60, 35);
			this.layoutControlItem28.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem28.TextVisible = false;
			this.layoutControlItem29.Control = this.txtFrom;
			this.layoutControlItem29.Location = new System.Drawing.Point(107, 0);
			this.layoutControlItem29.MaxSize = new System.Drawing.Size(0, 24);
			this.layoutControlItem29.MinSize = new System.Drawing.Size(54, 24);
			this.layoutControlItem29.Name = "layoutControlItem29";
			this.layoutControlItem29.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2);
			this.layoutControlItem29.Size = new System.Drawing.Size(54, 35);
			this.layoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem29.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem29.TextVisible = false;
			this.layoutControlItem30.Control = this.txtTo;
			this.layoutControlItem30.Location = new System.Drawing.Point(161, 0);
			this.layoutControlItem30.MaxSize = new System.Drawing.Size(0, 29);
			this.layoutControlItem30.MinSize = new System.Drawing.Size(54, 29);
			this.layoutControlItem30.Name = "layoutControlItem30";
			this.layoutControlItem30.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2);
			this.layoutControlItem30.Size = new System.Drawing.Size(54, 35);
			this.layoutControlItem30.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem30.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem30.TextVisible = false;
			this.layoutControl2.Controls.Add(this.layoutControl3);
			this.layoutControl2.Controls.Add(this.layoutControl1);
			this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl2.Location = new System.Drawing.Point(0, 0);
			this.layoutControl2.Name = "layoutControl2";
			this.layoutControl2.Root = this.layoutControlGroup2;
			this.layoutControl2.Size = new System.Drawing.Size(1367, 896);
			this.layoutControl2.TabIndex = 1;
			this.layoutControl2.Text = "layoutControl2";
			this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup2.GroupBordersVisible = false;
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[2] { this.layoutControlItem1, this.layoutControlItem2 });
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(1367, 896);
			this.layoutControlGroup2.TextVisible = false;
			this.layoutControlItem1.Control = this.layoutControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1367, 857);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem2.Control = this.layoutControl3;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 857);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1367, 39);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.timerInitForm.Interval = 500;
			this.timerRefeshAutoCreateBill.Interval = 2000;
			this.chkSignExam.Location = new System.Drawing.Point(153, 48);
			this.chkSignExam.Name = "chkSignExam";
			this.chkSignExam.Properties.Caption = "Ký phiếu khám";
			this.chkSignExam.Size = new System.Drawing.Size(149, 19);
			this.chkSignExam.StyleController = this.layoutControl4;
			this.chkSignExam.TabIndex = 18;
			this.chkSignExam.CheckedChanged += new System.EventHandler(chkSignExam_CheckedChanged);
			this.layoutControlItem31.Control = this.chkSignExam;
			this.layoutControlItem31.Location = new System.Drawing.Point(151, 46);
			this.layoutControlItem31.Name = "layoutControlItem31";
			this.layoutControlItem31.Size = new System.Drawing.Size(153, 44);
			this.layoutControlItem31.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem31.TextVisible = false;
			this.chkBaoLanh.Location = new System.Drawing.Point(428, 48);
			this.chkBaoLanh.Name = "chkBaoLanh";
			this.chkBaoLanh.Properties.Caption = "Bảo lãnh VP";
			this.chkBaoLanh.Size = new System.Drawing.Size(147, 19);
			this.chkBaoLanh.StyleController = this.layoutControl4;
			this.chkBaoLanh.TabIndex = 19;
			this.chkBaoLanh.ToolTip = "Bảo lãnh viện phí";
			this.chkBaoLanh.CheckedChanged += new System.EventHandler(chkBaoLanh_CheckedChanged);
			this.layoutControlItem32.Control = this.chkBaoLanh;
			this.layoutControlItem32.Location = new System.Drawing.Point(426, 46);
			this.layoutControlItem32.Name = "layoutControlItem32";
			this.layoutControlItem32.Size = new System.Drawing.Size(151, 44);
			this.layoutControlItem32.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem32.TextVisible = false;
			this.chkXemTruoc.Location = new System.Drawing.Point(2, 48);
			this.chkXemTruoc.Name = "chkXemTruoc";
			this.chkXemTruoc.Properties.Caption = "Xem trước in";
			this.chkXemTruoc.Size = new System.Drawing.Size(128, 19);
			this.chkXemTruoc.StyleController = this.layoutControl4;
			this.chkXemTruoc.TabIndex = 20;
			this.chkXemTruoc.CheckedChanged += new System.EventHandler(chkXemTruoc_CheckedChanged);
			this.layoutControlItem33.Control = this.chkXemTruoc;
			this.layoutControlItem33.Location = new System.Drawing.Point(0, 46);
			this.layoutControlItem33.Name = "layoutControlItem33";
			this.layoutControlItem33.Size = new System.Drawing.Size(132, 44);
			this.layoutControlItem33.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem33.TextVisible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.layoutControl2);
			base.Name = "UCRegister";
			base.Size = new System.Drawing.Size(1367, 896);
			base.Load += new System.EventHandler(UCRegister_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControl4).EndInit();
			this.layoutControl4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.chkAutoPay.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkAutoDeposit.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkAssignDoctor.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkPrintExam.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkAutoCreateBill.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkPrintPatientCard.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem22).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem27).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciAutoDeposit).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem24).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem26).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem17).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.pnlServiceRoomInfomation).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem18).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciUCHeinInfo).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem20).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem21).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem23).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciUCServiceRoomInfo).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem10).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem19).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem25).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl3).EndInit();
			this.layoutControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.txtTo.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtFrom.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtGateNumber.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.txtStepNumber.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cboCashierRoom.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Root).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lcibtnPatientNewInfo).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem11).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lcibtnDepositDetail).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem13).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem14).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem15).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem9).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem16).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem12).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciRegisterNumOrder).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lcibtnDepositRequest).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem28).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem29).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem30).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl2).EndInit();
			this.layoutControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkSignExam.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem31).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkBaoLanh.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem32).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkXemTruoc.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem33).EndInit();
			base.ResumeLayout(false);
		}

		private void CreateThreadInitWCFReadCard()
		{
			Thread thread = new Thread(new ThreadStart(InitWCFReadCardThread));
			try
			{
				thread.Start();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				thread.Abort();
			}
		}

		private void InitWCFReadCardThread()
		{
			try
			{
				TapCardServiceManager.OpenHost();
				TapCardServiceManager.SetDelegate(new ReadCard(CheckServiceCodeDelegate));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private bool CheckServiceCodeDelegate(string serviceCode)
		{
			bool result = false;
			try
			{
				SearchAndFillDataCardInfo(serviceCode);
				result = true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		private void SearchAndFillDataCardInfo(string serviceCode)
		{
			try
			{
				LogSystem.Debug("serviceCode: " + serviceCode);
				if (ucAddressCombo1 != null)
				{
					ucAddressCombo1.GetPatientSdo(null);
				}
				CommonParam param = new CommonParam();
				HisCardSDO patientInRegisterSearchByCard = new BackendAdapter(param).Get<HisCardSDO>("api/HisCard/GetCardSdoByCode", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, serviceCode, new Action(SessionManager.ActionLostToken), param);
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => patientInRegisterSearchByCard), patientInRegisterSearchByCard));
				IsReadCardTheViet = false;
				if (patientInRegisterSearchByCard != null)
				{
					Task<object> task = SearchByCode(patientInRegisterSearchByCard.PatientCode);
					IsReadCardTheViet = true;
					if (task != null && task.Result != null && task.Result is HisPatientSDO)
					{
						HisPatientSDO patientSDO = (HisPatientSDO)task.Result;
						patientSDO.HT_ADDRESS = patientInRegisterSearchByCard.HtAddress;
						patientSDO.HT_COMMUNE_NAME = (HtCommuneName = patientInRegisterSearchByCard.HtCommuneName);
						patientSDO.HT_DISTRICT_NAME = (HtDistrictName = patientInRegisterSearchByCard.HtDistrictName);
						patientSDO.HT_PROVINCE_NAME = (HtProvinceName = patientInRegisterSearchByCard.HtProvinceName);
						patientSDO.HT_COMMUNE_CODE = (HtCommuneCode = patientInRegisterSearchByCard.HtCommuneCode);
						patientSDO.HT_DISTRICT_CODE = (HtDistrictCode = patientInRegisterSearchByCard.HtDistrictCode);
						patientSDO.HT_PROVINCE_CODE = (HtProvinceCode = patientInRegisterSearchByCard.HtProvinceCode);
						if (ucAddressCombo1 != null)
						{
							ucAddressCombo1.GetPatientSdo(patientSDO);
						}
						Invoke((MethodInvoker)delegate
						{
							ProcessPatientCodeKeydown(patientSDO);
							FillDataIntoUCPlusInfo(patientSDO);
						});
					}
					else
					{
						Invoke((MethodInvoker)delegate
						{
							SetPatientSearchPanel(false);
							HisPatientSDO hisPatientSDO = new HisPatientSDO();
							SetPatientDTOFromCardSDO(patientInRegisterSearchByCard, hisPatientSDO);
							FillDataPatientToControl(hisPatientSDO);
							FillDataToHeinCardControlByCardSDO(patientInRegisterSearchByCard);
						});
					}
					cardSearch = patientInRegisterSearchByCard;
					return;
				}
				Invoke((MethodInvoker)delegate
				{
					cardSearch = null;
					if (param.Messages == null || param.Messages.Count == 0)
					{
						param.Messages.Add(ResourceMessage.ThongBaoKetQuaTimKiemBenhNhanKhiQuetTheDuLieuTraVeNull);
					}
					MessageManager.Show(param, null);
				});
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void Bill()
		{
			try
			{
				if (CheckCashierRoom() && GetTreatmentIdFromResultData() > 0)
				{
					TransactionBillADO transactionBillADO = new TransactionBillADO(GetTreatmentIdFromResultData(), currentModule.RoomId);
					Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.TransactionBill").FirstOrDefault();
					if (module == null)
					{
						throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.TransactionBill'");
					}
					if (!module.IsPlugin || module.ExtensionInfo == null)
					{
						throw new NullReferenceException("Module 'HIS.Desktop.Plugins.TransactionBill' is not plugins");
					}
					List<object> list = new List<object>();
					transactionBillADO.CashierRoomId = (long)cboCashierRoom.EditValue;
					list.Add(transactionBillADO);
					object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
			}
			catch (NullReferenceException ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private bool CheckCashierRoom()
		{
			bool result = true;
			try
			{
				if (Parse.ToInt64((cboCashierRoom.EditValue ?? "0").ToString()) == 0)
				{
					result = false;
					MessageManager.Show(ResourceMessage.ChonPhongThuNganTruocKhiMoTinhNangNay);
					cboCashierRoom.Focus();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private long GetTreatmentIdFromResultData()
		{
			long result = 0L;
			try
			{
				if (resultHisPatientProfileSDO != null)
				{
					result = resultHisPatientProfileSDO.HisTreatment.ID;
				}
				else if (currentHisExamServiceReqResultSDO != null)
				{
					result = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.ID;
				}
			}
			catch (Exception ex)
			{
				result = 0L;
				LogSystem.Warn(ex);
			}
			return result;
		}

		private void CreateThreadCallPatient()
		{
			Thread thread = new Thread(new ThreadStart(CallPatientNewThread));
			try
			{
				thread.Start();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				thread.Abort();
			}
		}

		private void CallPatientNewThread()
		{
			try
			{
				CallPatient();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void CeateThreadGetPatient()
		{
			Thread thread = new Thread(new ThreadStart(CallPatientSDOThread));
			try
			{
				thread.Start();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				thread.Abort();
			}
		}

		private void CallPatientSDOThread()
		{
			try
			{
				CallPatientSDO();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void CallPatientNotPCA(bool IsReCall, ref bool isCall)
		{
			try
			{
				long? num = null;
				if (!string.IsNullOrEmpty(txtStepNumber.Text))
				{
					num = long.Parse(txtStepNumber.Text);
				}
				string text = txtGateNumber.Text.Trim();
				if (txtGateNumber.Text.Contains(":"))
				{
					text = txtGateNumber.Text.Trim().Split(':').First();
				}
				else if (text != RegisterGateCode)
				{
					if (!IsReCall && bFrom != 0 && bTo != 0 && ((num == 1 && bFrom == 1 && bTo == 1) || (num != 1 && bFrom == 1 && bTo == 1 + num)))
					{
						IsFirstCallNotCPA = false;
					}
					isCall = !IsFirstCallNotCPA;
					if (!IsReCall)
					{
						nFrom = bFrom;
						nTo = bTo;
						if (num == 1)
						{
							bFrom = (bTo += (int)num.GetValueOrDefault());
						}
						else
						{
							bFrom = bTo + 1;
							bTo = bFrom + (int)num.GetValueOrDefault();
						}
						txtFrom.Text = bFrom.ToString();
						txtTo.Text = bTo.ToString();
					}
					numSttNow = txtTo.Text;
					return;
				}
				CommonParam commonParam = new CommonParam();
				RegisterGateCallSDO registerGateCallSDO = new RegisterGateCallSDO();
				registerGateCallSDO.CallPlace = text;
				registerGateCallSDO.CallStep = num;
				registerGateCallSDO.RegisterGateId = RegisterGateId;
				List<HIS_REGISTER_REQ> list = new BackendAdapter(commonParam).Post<List<HIS_REGISTER_REQ>>(IsReCall ? "api/HisRegisterGate/ReCall" : "api/HisRegisterGate/Call", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, commonParam, registerGateCallSDO, new Action(SessionManager.ActionLostToken), new object[0]);
				if (list != null && list.Count > 0)
				{
					bFrom = (int)list.Min((HIS_REGISTER_REQ o) => o.NUM_ORDER);
					bTo = (int)list.Max((HIS_REGISTER_REQ o) => o.NUM_ORDER);
					txtFrom.Text = bFrom.ToString();
					txtTo.Text = bTo.ToString();
					nFrom = bFrom;
					nTo = bTo;
					numSttNow = txtTo.Text;
				}
				else
				{
					isCall = false;
					if (IsReCall)
					{
						XtraMessageBox.Show("Không tìm thấy số thứ tự trước đó. Vui lòng thử lại sau", ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao);
					}
					else
					{
						XtraMessageBox.Show("Hiện tại không có số thứ tự tiếp theo. Vui lòng thử lại sau", ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private async void CallPatientSDO()
		{
			try
			{
				RegisterReqIds = new List<long>();
				string GateNum = txtGateNumber.Text.Trim();
				if (txtGateNumber.Text.Contains(":"))
				{
					GateNum = txtGateNumber.Text.Trim().Split(':').First();
				}
				long Step = long.Parse(txtStepNumber.Text);
				long Number = long.Parse(numSttNow) + ((Step > 1) ? 0 : Step);
				List<long> lstNumber = new List<long>();
				long SplitStep = 1L;
				if (Step > 1)
				{
					long NumberTemp;
					do
					{
						NumberTemp = Number + SplitStep;
						SplitStep++;
						lstNumber.Add(NumberTemp);
					}
					while (NumberTemp < long.Parse(numTotal) && SplitStep - 1 != Step);
					if (dicRegisterReq != null && dicRegisterReq.ContainsKey(GateNum) && lstNumber != null && lstNumber.Count > 0)
					{
						foreach (HIS_REGISTER_REQ item in dicRegisterReq[GateNum])
						{
							if (lstNumber.Where((long o) => o == item.NUM_ORDER) != null && lstNumber.Where((long o) => o == item.NUM_ORDER).ToList().Count > 0)
							{
								RegisterReqIds.Add(item.ID);
							}
						}
					}
				}
				else if (dicRegisterReq != null && dicRegisterReq.ContainsKey(GateNum))
				{
					foreach (HIS_REGISTER_REQ item2 in dicRegisterReq[GateNum])
					{
						if (item2.NUM_ORDER == Number)
						{
							RegisterReqIds.Add(item2.ID);
						}
					}
				}
				if (RegisterReqIds != null && RegisterReqIds.Count > 0)
				{
					CommonParam param = new CommonParam();
					CallPatientSDO sdo = new CallPatientSDO
					{
						CallPlace = GateNum,
						RegisterReqIds = RegisterReqIds
					};
					new BackendAdapter(param).Post<bool>("api/HisRegisterReq/CallPatient", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, param, sdo, new Action(SessionManager.ActionLostToken), new object[0]);
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Warn(ex2);
			}
		}

		public string GetIpLocal()
		{
			string result = "";
			try
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
				if (hostAddresses != null && hostAddresses.Length != 0)
				{
					IPAddress[] array = hostAddresses;
					foreach (IPAddress iPAddress in array)
					{
						if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
						{
							result = iPAddress.ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool Create(SdaEventLogSDO data)
		{
			bool result = false;
			try
			{
				CommonParam commonParam = new CommonParam();
				bool aro = new BackendAdapter(commonParam).Post<bool>("/api/SdaEventLog/Create", HIS.Desktop.ApiConsumer.ApiConsumers.SdaConsumer, data, commonParam);
				LogSystem.Info("Du lieu dau ra SdaEventLog/Create:" + LogUtil.TraceData(LogUtil.GetMemberName(() => aro), aro));
				if (aro)
				{
					result = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = false;
			}
			return result;
		}

		private async void CallPatient()
		{
			try
			{
				if (!btnCallPatient.Enabled || string.IsNullOrEmpty(txtGateNumber.Text))
				{
					return;
				}
				if (AppConfigs.DangKyTiepDonGoiBenhNhanBangCPA == "1")
				{
					if (string.IsNullOrEmpty(txtStepNumber.Text))
					{
						return;
					}
					string txtGate = txtGateNumber.Text.Trim();
					if (txtGateNumber.Text.Contains(":"))
					{
						txtGate = txtGateNumber.Text.Trim().Split(':').First();
					}
					if (clienttManager == null)
					{
						clienttManager = new CallPatientClientManager();
					}
					long numCheck = long.Parse(numSttNow) + long.Parse(txtStepNumber.Text);
					if ((!string.IsNullOrEmpty(numTotal) && numCheck <= long.Parse(numTotal)) || string.IsNullOrEmpty(numTotal))
					{
						if (HisConfigCFG.CallCpaOption == 2)
						{
							int[] nums = await clienttManager.AsyncCallNumOrderPlusString(txtGate, int.Parse(txtStepNumber.Text));
							if (nums != null && nums.Length != 0)
							{
								await CallModuleCallPatientNumOrder(nums.LastOrDefault().ToString());
							}
						}
						else
						{
							clienttManager.CallNumOrderString(txtGate, int.Parse(txtStepNumber.Text));
						}
						CeateThreadGetPatient();
					}
					else if (!string.IsNullOrEmpty(numTotal))
					{
						if (long.Parse(numSttNow) < long.Parse(numTotal))
						{
							long numSend = long.Parse(numTotal) - long.Parse(numSttNow);
							clienttManager.CallNumOrderString(txtGate, (int)numSend);
							CeateThreadGetPatient();
						}
						else if (long.Parse(numSttNow) >= long.Parse(numTotal))
						{
							XtraMessageBox.Show("Hiện tại đã hết số đăng ký. Vui lòng thử lại sau.", ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao);
						}
					}
				}
				else
				{
					bool isCall = true;
					CallPatientNotPCA(false, ref isCall);
					if (isCall)
					{
						CallPatientByFromTo();
					}
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private void UpNumber()
		{
			try
			{
				nFrom = int.Parse(txtTo.Text) + 1;
				txtFrom.Text = nFrom.ToString();
				nTo = int.Parse(txtTo.Text) + int.Parse(txtStepNumber.Text);
				txtTo.Text = nTo.ToString();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void CallPatientByFromTo()
		{
			try
			{
				SpeechPlayer.TypeSpeechCFG = HisConfigs.Get<string>("Inventec.Speech.TypeSpeechCFG");
				LogSystem.Debug(CallConfigString);
				string[] array = CallConfigString.Split(new string[2] { "<#", ";>" }, StringSplitOptions.RemoveEmptyEntries);
				if (array.ToList().Count > 0)
				{
					string[] array2 = array;
					foreach (string word in array2)
					{
						string text = KEY_SINGLE.FirstOrDefault((string o) => o == word.ToUpper());
						if (text == null || text.Count() == 0)
						{
							string[] array3 = word.Split(new string[6] { ",", ";", ".", "-", ":", "/" }, StringSplitOptions.RemoveEmptyEntries);
							string[] array4 = array3;
							foreach (string text2 in array4)
							{
								SpeechPlayer.SpeakSingle(text2.Trim());
							}
							continue;
						}
						switch (word)
						{
						case "NUM_ORDER_STR":
						{
							if (nFrom == nTo)
							{
								string content = Inventec.Common.String.Convert.CurrencyToVneseStringNoUpcase(nFrom.ToString()).Trim();
								SpeechPlayer.SpeakSingle(content);
								break;
							}
							string text3 = Inventec.Common.String.Convert.CurrencyToVneseStringNoUpcase(nFrom.ToString()).Trim();
							SpeechPlayer.SpeakSingle(text3.ToString().Trim());
							SpeechPlayer.SpeakSingle(HisConfigs.Get<string>("EXE.CALL_PATIENT.DEN"));
							string text4 = Inventec.Common.String.Convert.CurrencyToVneseStringNoUpcase(nTo.ToString());
							SpeechPlayer.SpeakSingle(text4.ToString().Trim());
							break;
						}
						case "NUM_ORDER":
							if (nFrom == nTo)
							{
								SpeechPlayer.Speak(nFrom);
								break;
							}
							SpeechPlayer.Speak(nFrom);
							SpeechPlayer.SpeakSingle(HisConfigs.Get<string>("EXE.CALL_PATIENT.DEN"));
							SpeechPlayer.Speak(nTo);
							break;
						case "GATE_NAME":
							SpeechPlayer.SpeakSingle(txtGateNumber.Text.Trim().Split(':').First());
							break;
						case "REGISTER_GATE_CODE":
							SpeechPlayer.SpeakSingle(gateCode);
							break;
						case "REGISTER_GATE_NAME":
							SpeechPlayer.SpeakSingle(gateName);
							break;
						}
					}
				}
				if (nFrom == nTo)
				{
					CallModuleCallPatientNumOrder(nFrom.ToString());
				}
				else
				{
					CallModuleCallPatientNumOrder(nFrom + " - " + nTo);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private async Task CallModuleCallPatientNumOrder(string num)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(callPatientFormName))
				{
					V_HIS_ROOM room = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault((V_HIS_ROOM o) => o.ID == currentModule.RoomId);
					callPatientFormName = "WAITING_NUM_ORDER_" + room.ROOM_CODE;
				}
				Form waitingForm = null;
				if (Application.OpenForms != null && Application.OpenForms.Count > 0)
				{
					for (int i = 0; i < Application.OpenForms.Count; i++)
					{
						Form f = Application.OpenForms[i];
						if (f.Name == callPatientFormName)
						{
							waitingForm = f;
						}
					}
				}
				if (waitingForm != null)
				{
					MethodInfo theMethod = waitingForm.GetType().GetMethod("SetNumOrder");
					if (theMethod != null)
					{
						object[] param = new object[1] { num };
						theMethod.Invoke(waitingForm, param);
					}
				}
				else
				{
					LogSystem.Warn("Nguoi dung chua mo man hinh cho CALL_PATIENT_NUM_ORDER");
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private void CreateThreadRecallCallPatient()
		{
			Thread thread = new Thread(new ThreadStart(ReacallCallPatientNewThread));
			try
			{
				thread.Start();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				thread.Abort();
			}
		}

		private void ReacallCallPatientNewThread()
		{
			try
			{
				ReCallPatient();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private async void ReCallPatient()
		{
			try
			{
				if (!btnCallPatient.Enabled || string.IsNullOrEmpty(txtGateNumber.Text))
				{
					return;
				}
				if (AppConfigs.DangKyTiepDonGoiBenhNhanBangCPA == "1")
				{
					if (string.IsNullOrEmpty(txtStepNumber.Text))
					{
						return;
					}
					string txtGate = txtGateNumber.Text.Trim();
					if (txtGateNumber.Text.Contains(":"))
					{
						txtGate = txtGateNumber.Text.Trim().Split(':').First();
					}
					if (clienttManager == null)
					{
						clienttManager = new CallPatientClientManager();
					}
					if (HisConfigCFG.CallCpaOption == 2)
					{
						int[] nums = await clienttManager.AsyncRecallNumOrderPlusString(txtGate, int.Parse(txtStepNumber.Text));
						if (nums != null && nums.Length != 0)
						{
							await CallModuleCallPatientNumOrder(nums.LastOrDefault().ToString());
						}
					}
					else
					{
						clienttManager.RecallNumOrderString(txtGate, int.Parse(txtStepNumber.Text));
					}
					CeateThreadGetPatient();
				}
				else
				{
					bool isCall = true;
					CallPatientNotPCA(true, ref isCall);
					if (isCall)
					{
						CallPatientByFromTo();
					}
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private async Task GATE()
		{
			try
			{
				_RegisterGates = new List<HIS_REGISTER_GATE>();
				HisRegisterGateFilter filter = new HisRegisterGateFilter();
				_RegisterGates = new BackendAdapter(null).Get<List<HIS_REGISTER_GATE>>("api/HisRegisterGate/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, filter, null);
				int timeSyncAll = AppConfigs.ThoiGianTuDongGoiLaySTTMoiNhat;
				if (timeSyncAll > 0)
				{
					lciRegisterNumOrder.Visibility = LayoutVisibility.Always;
					RegisterTimer(timeInterval: new System.Windows.Forms.Timer
					{
						Interval = timeSyncAll,
						Enabled = true
					}.Interval, moduleLink: currentModule.ModuleLink, timerKeyName: "timerSyncAll" + count, timerProcess: new Action(timerCall_Tick));
					StartTimer(currentModule.ModuleLink, "timerSyncAll" + count);
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private void timerCall_Tick()
		{
			CreateCallRegisterReq();
		}

		private void CreateCallRegisterReq()
		{
			Thread thread = new Thread(new ThreadStart(RegisterReqNewThread));
			try
			{
				thread.Start();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				thread.Abort();
			}
		}

		private void RegisterReqNewThread()
		{
			try
			{
				RegisterReq();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void GetSttNowAndCallApi(bool IsCPA, ref string numSttTo)
		{
			try
			{
				string gateNum = txtGateNumber.Text.Trim();
				gateCode = "";
				gateName = "";
				if (gateNum.Contains(":"))
				{
					gateNum = gateNum.Split(':').First();
					gateCode = txtGateNumber.Text.Trim().Split(':').Last();
				}
				else
				{
					gateCode = gateNum;
				}
				if (IsCPA)
				{
					long[] currentPatientCall = clienttManager.GetCurrentPatientCall(gateNum, false);
					if (currentPatientCall != null && currentPatientCall.Length != 0)
					{
						numSttNow = currentPatientCall.Last().ToString();
						numSttTo = currentPatientCall.First().ToString();
					}
				}
				HIS_REGISTER_GATE hIS_REGISTER_GATE = _RegisterGates.FirstOrDefault((HIS_REGISTER_GATE p) => p.REGISTER_GATE_CODE == gateCode);
				if (hIS_REGISTER_GATE != null)
				{
					gateName = hIS_REGISTER_GATE.REGISTER_GATE_NAME;
					RegisterGateId = hIS_REGISTER_GATE.ID;
					RegisterGateCode = hIS_REGISTER_GATE.REGISTER_GATE_CODE;
					HisRegisterReqFilter hisRegisterReqFilter = new HisRegisterReqFilter();
					hisRegisterReqFilter.REGISTER_GATE_ID = hIS_REGISTER_GATE.ID;
					hisRegisterReqFilter.REGISTER_DATE = Parse.ToInt64(System.Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") + "000000");
					List<HIS_REGISTER_REQ> datas = new BackendAdapter(null).Get<List<HIS_REGISTER_REQ>>("api/HisRegisterReq/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisRegisterReqFilter, null);
					if (datas != null && datas.Count > 0)
					{
						HIS_REGISTER_REQ hIS_REGISTER_REQ = datas.OrderByDescending((HIS_REGISTER_REQ p) => p.REGISTER_TIME).ThenByDescending((HIS_REGISTER_REQ o) => o.NUM_ORDER).FirstOrDefault();
						numTotal = hIS_REGISTER_REQ.NUM_ORDER.ToString();
						if (!IsCPA)
						{
							numSttNow = (from p in datas
								where p.CALL_TIME.HasValue
								orderby p.CALL_TIME descending
								select p).ThenByDescending((HIS_REGISTER_REQ o) => o.NUM_ORDER).FirstOrDefault().NUM_ORDER.ToString();
						}
						Invoke((MethodInvoker)delegate
						{
							if (!dicRegisterReq.ContainsKey(gateNum))
							{
								dicRegisterReq[gateNum] = new List<HIS_REGISTER_REQ>();
							}
							dicRegisterReq[gateNum] = datas;
						});
					}
					else
					{
						numTotal = "";
					}
				}
				else
				{
					numTotal = "";
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private async void RegisterReq()
		{
			try
			{
				if (string.IsNullOrEmpty(txtGateNumber.Text))
				{
					return;
				}
				if (clienttManager == null)
				{
					clienttManager = new CallPatientClientManager();
				}
				bool isCPA = false;
				if (AppConfigs.DangKyTiepDonGoiBenhNhanBangCPA == "1")
				{
					isCPA = true;
				}
				string numSttTo = "0";
				GetSttNowAndCallApi(isCPA, ref numSttTo);
				txtNumberPer = ((!string.IsNullOrEmpty(numTotal)) ? (numSttNow + "/" + numTotal) : numSttNow);
				lblRegisterNumOrder.Invoke((MethodInvoker)delegate
				{
					lblRegisterNumOrder.Text = txtNumberPer;
				});
				if (isCPA)
				{
					txtFrom.Invoke((MethodInvoker)delegate
					{
						txtFrom.Text = numSttTo;
					});
					txtTo.Invoke((MethodInvoker)delegate
					{
						txtTo.Text = numSttNow;
					});
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private void DepositRequestClick()
		{
			try
			{
				if (btnDepositRequest.Enabled)
				{
					Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.RequestDeposit").FirstOrDefault();
					Inventec.Desktop.Common.Modules.Module module2 = new Inventec.Desktop.Common.Modules.Module();
					if (module == null)
					{
						throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.RequestDeposit'");
					}
					if (module.IsPlugin && module.ExtensionInfo != null)
					{
						List<object> list = new List<object>();
						list.Add(GetTreatmentIdFromResultData());
						PluginInstanceBehavior.ShowModule("HIS.Desktop.Plugins.RequestDeposit", currentModule.RoomId, currentModule.RoomTypeId, list);
					}
				}
			}
			catch (NullReferenceException ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private void DepositDetail()
		{
			try
			{
				if (!CheckCashierRoom())
				{
					return;
				}
				if (AppConfigs.IsShowDepositService == 1)
				{
					Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.TransactionDeposit").FirstOrDefault();
					if (module == null)
					{
						throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.TransactionDeposit'");
					}
					if (module.IsPlugin && module.ExtensionInfo != null)
					{
						List<object> list = new List<object>();
						TransactionDepositADO item = new TransactionDepositADO(GetTreatmentFeeViewByResult(), (long)(cboCashierRoom.EditValue ?? ((object)0)));
						list.Add(item);
						list.Add(module);
						object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
						if (pluginInstance == null)
						{
							throw new ArgumentNullException("moduleData is null");
						}
						((Form)pluginInstance).ShowDialog();
					}
					return;
				}
				DepositServiceADO depositServiceADO = new DepositServiceADO();
				depositServiceADO.hisTreatmentId = GetTreatmentViewByResult().ID;
				if (depositServiceADO.hisTreatmentId == 0)
				{
					throw new ArgumentNullException("hisTreatmentId is null");
				}
				Inventec.Desktop.Common.Modules.Module module2 = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.DepositService").FirstOrDefault();
				if (module2 == null)
				{
					throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.DepositService'");
				}
				if (!module2.IsPlugin || module2.ExtensionInfo == null)
				{
					throw new NullReferenceException("Module 'HIS.Desktop.Plugins.DepositService' is not plugins");
				}
				List<object> list2 = new List<object>();
				depositServiceADO.BRANCH_ID = WorkPlace.GetBranchId();
				depositServiceADO.CashierRoomId = (long)(cboCashierRoom.EditValue ?? ((object)0));
				list2.Add(depositServiceADO);
				object pluginInstance2 = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module2, currentModule.RoomId, currentModule.RoomTypeId), list2);
				if (pluginInstance2 == null)
				{
					throw new ArgumentNullException("moduleData is null");
				}
				((Form)pluginInstance2).ShowDialog();
			}
			catch (NullReferenceException ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private V_HIS_TREATMENT GetTreatmentViewByResult()
		{
			V_HIS_TREATMENT v_HIS_TREATMENT = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				HisTreatmentViewFilter hisTreatmentViewFilter = new HisTreatmentViewFilter();
				hisTreatmentViewFilter.ID = GetTreatmentIdFromResultData();
				v_HIS_TREATMENT = new BackendAdapter(commonParam).Get<List<V_HIS_TREATMENT>>("api/HisTreatment/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisTreatmentViewFilter, new Action(SessionManager.ActionLostToken), commonParam).SingleOrDefault();
			}
			catch (Exception ex)
			{
				v_HIS_TREATMENT = null;
				LogSystem.Warn(ex);
			}
			return v_HIS_TREATMENT ?? new V_HIS_TREATMENT();
		}

		private V_HIS_TREATMENT_FEE GetTreatmentFeeViewByResult()
		{
			V_HIS_TREATMENT_FEE v_HIS_TREATMENT_FEE = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				HisTreatmentFeeViewFilter hisTreatmentFeeViewFilter = new HisTreatmentFeeViewFilter();
				hisTreatmentFeeViewFilter.ID = GetTreatmentIdFromResultData();
				v_HIS_TREATMENT_FEE = new BackendAdapter(commonParam).Get<List<V_HIS_TREATMENT_FEE>>("api/HisTreatment/GetFeeView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisTreatmentFeeViewFilter, new Action(SessionManager.ActionLostToken), commonParam).SingleOrDefault();
			}
			catch (Exception ex)
			{
				v_HIS_TREATMENT_FEE = null;
				LogSystem.Warn(ex);
			}
			return v_HIS_TREATMENT_FEE ?? new V_HIS_TREATMENT_FEE();
		}

		public override void ProcessDisposeModuleDataAfterClose()
		{
			try
			{
				lstService = null;
				IsEmergency = false;
				baseNameControl = null;
				txtNumberPer = null;
				numTotal = null;
				numSttNow = null;
				roomId = 0L;
				isSaveWithRoomHasConfigAllowNotChooseService = false;
				_IsDungTuyenCapCuuByTime = false;
				isNotCheckTT = false;
				isAlertTreatmentEndInDay = false;
				CallConfigString = null;
				currentControlStateRDO = null;
				controlStateWorker = null;
				isNotLoadWhileChangeControlStateInFirst = false;
				isResetForm = false;
				_isPatientAppointmentCode = false;
				_TreatmnetIdByAppointmentCode = 0L;
				IsPresentAndAppointment = false;
				IsPresent = false;
				ValidatedTTCT = false;
				appointmentCode = null;
				isReadQrCode = false;
				isPrintNow = false;
				actionType = 0;
				isNotPatientDayDob = false;
				ResultDataADO = null;
				_HeinCardData = null;
				resultHisPatientProfileSDO = null;
				currentHisExamServiceReqResultSDO = null;
				frm = null;
				clienttManager = null;
				dataPatientRaw = null;
				dataAddressPatient = null;
				roomExamServiceProcessor = null;
				mainHeinProcessor = null;
				ucHeinBHYT = null;
				currentPatientTypeAllowByPatientType = null;
				serviceReqPrintIds = null;
				isShowMess = false;
				registerNumber = 0;
				ucKskContract = null;
				kskContractProcessor = null;
				currentModule = null;
				serviceReqDetailSDOs = null;
				transPatiADO = null;
				cardSearch = null;
				currentPatientSDO = null;
				gateName = null;
				gateCode = null;
				RegisterGateId = 0L;
				KEY_SINGLE = null;
				dicRegisterReq = null;
				RegisterReqIds = null;
				bTo = 0;
				bFrom = 0;
				nTo = 0;
				nFrom = 0;
				callPatientFormName = null;
				count = 0;
				_RegisterGates = null;
				lstPreviousDebtTreatmentsRegister = null;
				menu = null;
				barManager = null;
				isPrintNowBL = false;
				ServiceReqList = null;
				treatmentTypeID = 0L;
				EmergencyBol = false;
				lst = null;
				lstModuleLinkApply = null;
				chkAutoPay.CheckedChanged -= new EventHandler(chkAutoPaid_CheckedChanged);
				chkAutoDeposit.CheckedChanged -= new EventHandler(chkAutoDeposit_CheckedChanged);
				chkAssignDoctor.CheckedChanged -= new EventHandler(chkAssignDoctor_CheckedChanged);
				chkPrintExam.CheckedChanged -= new EventHandler(chkPrintExam_CheckedChanged);
				chkAutoCreateBill.CheckedChanged -= new EventHandler(chkAutoCreateBill_CheckedChanged);
				chkPrintPatientCard.CheckedChanged -= new EventHandler(chkPrintPatientCard_CheckedChanged);
				btnTTChuyenTuyen.Click -= new EventHandler(btnTTChuyenTuyen_Click);
				txtTo.KeyPress -= new KeyPressEventHandler(txtTo_KeyPress);
				txtFrom.KeyPress -= new KeyPressEventHandler(txtFrom_KeyPress);
				btnGiayTo.Click -= new EventHandler(btnGiayTo_Click);
				btnDepositRequest.Click -= new EventHandler(btnDepositRequest_Click);
				dropDownButton__Other.Click -= new EventHandler(dropDownButton__Other_Click);
				btnRecallPatient.Click -= new EventHandler(btnRecallPatient_Click);
				btnCallPatient.Click -= new EventHandler(btnCallPatient_Click);
				txtGateNumber.Leave -= new EventHandler(txtGateNumber_Leave);
				txtGateNumber.ButtonClick -= new ButtonPressedEventHandler(txtGateNumber_ButtonClick);
				txtStepNumber.KeyPress -= new KeyPressEventHandler(txtStepNumber_KeyPress);
				txtStepNumber.Leave -= new EventHandler(txtStepNumber_Leave);
				btnSaveAndPrint.Click -= new EventHandler(btnSaveAndPrint_Click);
				btnPatientNew.Click -= new EventHandler(btnPatientNew_Click);
				btnSaveAndAssain.Click -= new EventHandler(btnSaveAndAssain_Click);
				btnDepositDetail.Click -= new EventHandler(btnDepositDetail_Click);
				btnTreatmentBedRoom.Click -= new EventHandler(btnTreatmentBedRoom_Click);
				btnNewContinue.Click -= new EventHandler(btnNewContinue_Click);
				btnPrint.Click -= new EventHandler(btnPrint_Click);
				btnSave.Click -= new EventHandler(btnSave_Click);
				timerRefeshAutoCreateBill.Tick -= new EventHandler(TimerRefeshAutoCreateBill_Tick);
				base.Load -= new EventHandler(UCRegister_Load);
				layoutControlItem30 = null;
				layoutControlItem29 = null;
				txtFrom = null;
				txtTo = null;
				layoutControlItem28 = null;
				btnGiayTo = null;
				layoutControlItem17 = null;
				chkAutoPay = null;
				chkAutoDeposit = null;
				lciAutoDeposit = null;
				timerRefeshAutoCreateBill = null;
				layoutControlItem27 = null;
				chkAssignDoctor = null;
				layoutControlItem25 = null;
				layoutControlItem26 = null;
				layoutControlItem24 = null;
				layoutControlItem22 = null;
				layoutControlGroup3 = null;
				chkPrintExam = null;
				layoutControl4 = null;
				chkAutoCreateBill = null;
				chkPrintPatientCard = null;
				layoutControlItem19 = null;
				ucCheckTT1 = null;
				lcibtnDepositRequest = null;
				btnDepositRequest = null;
				timerInitForm = null;
				lciRegisterNumOrder = null;
				lblRegisterNumOrder = null;
				layoutControlItem12 = null;
				dropDownButton__Other = null;
				emptySpaceItem1 = null;
				ucPlusInfo1 = null;
				layoutControlItem10 = null;
				layoutControlItem7 = null;
				btnTTChuyenTuyen = null;
				lciUCServiceRoomInfo = null;
				ucServiceRoomInfo1 = null;
				layoutControlItem23 = null;
				ucImageInfo1 = null;
				layoutControlItem21 = null;
				ucRelativeInfo1 = null;
				layoutControlItem20 = null;
				ucOtherServiceReqInfo1 = null;
				lciUCHeinInfo = null;
				layoutControlItem18 = null;
				ucAddressCombo1 = null;
				ucHeinInfo1 = null;
				pnlServiceRoomInfomation = null;
				ucPatientRaw1 = null;
				layoutControlItem16 = null;
				layoutControlItem15 = null;
				layoutControlItem14 = null;
				layoutControlItem13 = null;
				lcibtnDepositDetail = null;
				layoutControlItem11 = null;
				lcibtnPatientNewInfo = null;
				layoutControlItem9 = null;
				layoutControlItem8 = null;
				layoutControlItem6 = null;
				layoutControlItem5 = null;
				layoutControlItem4 = null;
				layoutControlItem3 = null;
				btnSave = null;
				btnPrint = null;
				btnNewContinue = null;
				btnTreatmentBedRoom = null;
				btnDepositDetail = null;
				btnSaveAndAssain = null;
				btnPatientNew = null;
				btnSaveAndPrint = null;
				cboCashierRoom = null;
				txtStepNumber = null;
				txtGateNumber = null;
				btnCallPatient = null;
				btnRecallPatient = null;
				layoutControlItem2 = null;
				layoutControlItem1 = null;
				layoutControlGroup2 = null;
				Root = null;
				layoutControl3 = null;
				layoutControl2 = null;
				layoutControlGroup1 = null;
				layoutControl1 = null;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public void Save()
		{
			try
			{
				if (actionType == 1 && btnSave.Enabled)
				{
					btnSave_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void SaveAndPrint()
		{
			try
			{
				if (btnSaveAndPrint.Enabled && actionType == 1)
				{
					btnSaveAndPrint_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void New()
		{
			try
			{
				btnNewContinue_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void PatientNew()
		{
			try
			{
				if (actionType == 1 && lcibtnPatientNewInfo.Visibility == LayoutVisibility.Always)
				{
					btnPatientNew_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void AssignService()
		{
			try
			{
				if (actionType == 3)
				{
					btnSaveAndAssain_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void InBed()
		{
			try
			{
				if (actionType == 3)
				{
					btnTreatmentBedRoom_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void BillKeyboard()
		{
			try
			{
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void Deposit()
		{
			try
			{
				if (actionType == 3 && btnDepositDetail.Enabled && lcibtnDepositDetail.Visibility == LayoutVisibility.Always)
				{
					btnDepositDetail_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void DepositRequest()
		{
			try
			{
				if (actionType == 3 && btnDepositRequest.Enabled && lcibtnDepositRequest.Visibility == LayoutVisibility.Always)
				{
					btnDepositRequest_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void PrintKeyboard()
		{
			try
			{
				if (actionType == 3)
				{
					btnPrint_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void ClickF1()
		{
			try
			{
				btnNewContinue_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void ClickF2()
		{
			try
			{
				if (actionType == 1)
				{
					ucPatientRaw1.FocusUserControl();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void ClickF3()
		{
			try
			{
				if (actionType != 1)
				{
					return;
				}
				long patientTypeId = GetPatientTypeId();
				if (patientTypeId == HisConfigCFG.PatientTypeId__BHYT)
				{
					ucHeinInfo1.FocusUserControl();
				}
				else if (patientTypeId == HisConfigCFG.PatientTypeId__KSK)
				{
					if (ucKskContract != null && kskContractProcessor != null)
					{
						kskContractProcessor.InFocus(ucKskContract);
					}
				}
				else
				{
					ucServiceRoomInfo1.FocusUserControl();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void ClickF4()
		{
			try
			{
				if (actionType == 1)
				{
					ucServiceRoomInfo1.FocusUserControl();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void ClickF5()
		{
			try
			{
				btnCallPatient_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public void ClickF6()
		{
			try
			{
				btnRecallPatient_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public void ClickF7()
		{
			try
			{
				ucPlusInfo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public void ClickF8()
		{
			try
			{
				if (btnSaveAndPrint.Enabled)
				{
					btnSaveAndPrint_Click(null, null);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public void ClickF9()
		{
			try
			{
				ucRelativeInfo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public void ClickF10()
		{
			try
			{
				if (actionType == 3)
				{
					RichEditorStore richEditorStore = new RichEditorStore(HIS.Desktop.ApiConsumer.ApiConsumers.SarConsumer, ConfigSystems.URI_API_SAR, LanguageManager.GetLanguage(), GlobalVariables.TemnplatePathFolder);
					richEditorStore.RunPrintTemplate("Mps000178", new DelegateRunPrinter(DelegateRunPrinterInTheBenhNhanPrintNow));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public void ClickF11()
		{
			try
			{
				if (ucAddressCombo1 != null)
				{
					ucAddressCombo1.FocusTHX();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void PeriosTreatmentMessage()
		{
			try
			{
				LogSystem.Debug("Tiep don: Cau hinh co kiem tra dot dieu tri truoc cua BN con thuoc chua uong het hay khong: IsCheckPreviousPrescription = " + HisConfigCFG.IsCheckPreviousPrescription);
				string text = "";
				lstPreviousDebtTreatmentsRegister = new List<string>();
				lstSend = new List<string>();
				if (HisConfigCFG.IsCheckPreviousPrescription && currentPatientSDO.PreviousPrescriptions != null && currentPatientSDO.PreviousPrescriptions.Count > 0)
				{
					LogSystem.Debug("Tiep don: Du lieu benh nhan cu: " + LogUtil.TraceData(LogUtil.GetMemberName(() => currentPatientSDO), currentPatientSDO));
					string treatmentCode = currentPatientSDO.TreatmentCode;
					string text2 = "";
					for (int num = 0; num < currentPatientSDO.PreviousPrescriptions.Count; num++)
					{
						text2 += string.Format(ResourceMessage.ThuocCoThoiSuDungDen, " - " + currentPatientSDO.PreviousPrescriptions[num].REQUEST_ROOM_NAME + " ", Inventec.Common.DateTime.Convert.TimeNumberToDateString(currentPatientSDO.PreviousPrescriptions[num].USE_TIME_TO.GetValueOrDefault()) + "\r\n");
					}
					text += string.Format(ResourceMessage.DotKhamTruocCuaBenhNhanCoThuocChuaUongHet, treatmentCode, "\r\n", text2, "");
				}
				LogSystem.Debug("Tiep don: Cau hinh co kiem tra dot dieu tri truoc cua BN con no tien vien phi hay khong: IsCheckPreviousDebt = " + HisConfigCFG.IsCheckPreviousDebt);
				HIS_PATIENT_TYPE dtPatientType = BackendDataWorker.Get<HIS_PATIENT_TYPE>().Find((HIS_PATIENT_TYPE o) => o.PATIENT_TYPE_CODE == HisConfigs.Get<string>("MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT"));
				btnSave.Enabled = true;
				if (HisConfigCFG.IsCheckPreviousDebt == "1" || HisConfigCFG.IsCheckPreviousDebt == "3" || HisConfigCFG.IsCheckPreviousDebt == "4" || HisConfigCFG.IsCheckPreviousDebt == "5")
				{
					if (currentPatientSDO.PreviousDebtTreatments != null && currentPatientSDO.PreviousDebtTreatments.Count > 0)
					{
						LogSystem.Debug("Tiep don: Du lieu benh nhan cu: " + LogUtil.TraceData(LogUtil.GetMemberName(() => currentPatientSDO), currentPatientSDO));
						List<string> list = currentPatientSDO.PreviousDebtTreatments.Where((string t) => !string.IsNullOrEmpty(t) && t != currentPatientSDO.TreatmentCode && t != lastSavedTreatmentCode).Distinct().ToList();
						if (list.Count > 0)
						{
							string arg = string.Join(",", list);
							if (!string.IsNullOrEmpty(text))
							{
								text += "\r\n";
							}
							if (HisConfigCFG.IsCheckPreviousDebt == "5")
							{
								text += string.Format("Đợt khám/điều trị trước đó của bệnh nhân có số tiền phải trả lớn hơn 0 hoặc hồ sơ BHYT chưa duyệt khóa viện phí. Mã hồ sơ điều trị {0}.", arg);
								if (XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
								{
									return;
								}
							}
							if (HisConfigCFG.IsCheckPreviousDebt == "4")
							{
								text += string.Format("Đợt khám/điều trị trước đó của bệnh nhân có số tiền phải trả lớn hơn 0  hoặc chưa duyệt khóa viện phí. Mã hồ sơ điều trị {0}. Bạn có muốn đăng ký tiếp đón không?", arg);
							}
							else
							{
								if (HisConfigCFG.IsCheckPreviousDebt == "1")
								{
									text += string.Format(ResourceMessage.DotKhamTruocCuaBenhNhanConNoTienVienPhi, arg);
								}
								if (HisConfigCFG.IsCheckPreviousDebt == "3" && currentPatientSDO.LastTreatmentFee != null && currentPatientSDO.LastTreatmentFee.TREATMENT_CODE != lastSavedTreatmentCode && currentPatientSDO.LastTreatmentFee.TREATMENT_CODE != currentPatientSDO.TreatmentCode && (currentPatientSDO.LastTreatmentFee.IS_ACTIVE == 1 || currentPatientSDO.LastTreatmentFee.TOTAL_PATIENT_PRICE.GetValueOrDefault() - currentPatientSDO.LastTreatmentFee.TOTAL_DEPOSIT_AMOUNT.GetValueOrDefault() - currentPatientSDO.LastTreatmentFee.TOTAL_BILL_AMOUNT.GetValueOrDefault() + currentPatientSDO.LastTreatmentFee.TOTAL_BILL_TRANSFER_AMOUNT.GetValueOrDefault() + currentPatientSDO.LastTreatmentFee.TOTAL_REPAY_AMOUNT.GetValueOrDefault() > 0m))
								{
									lstPreviousDebtTreatmentsRegister = currentPatientSDO.PreviousDebtTreatments;
									text += string.Format(ResourceMessage.DotKhamTruocCuaBenhNhanConNoTienVienPhi3, currentPatientSDO.LastTreatmentFee.TREATMENT_CODE);
								}
							}
						}
					}
					if (HisConfigCFG.IsCheckPreviousDebt == "3" && currentPatientSDO.LastTreatmentFee != null && currentPatientSDO.LastTreatmentFee.TREATMENT_CODE != lastSavedTreatmentCode && currentPatientSDO.LastTreatmentFee.TREATMENT_CODE != currentPatientSDO.TreatmentCode && (currentPatientSDO.LastTreatmentFee.IS_ACTIVE == 1 || currentPatientSDO.LastTreatmentFee.TOTAL_PATIENT_PRICE.GetValueOrDefault() - currentPatientSDO.LastTreatmentFee.TOTAL_DEPOSIT_AMOUNT.GetValueOrDefault() - currentPatientSDO.LastTreatmentFee.TOTAL_BILL_AMOUNT.GetValueOrDefault() + currentPatientSDO.LastTreatmentFee.TOTAL_BILL_TRANSFER_AMOUNT.GetValueOrDefault() + currentPatientSDO.LastTreatmentFee.TOTAL_REPAY_AMOUNT.GetValueOrDefault() > 0m))
					{
						lstSend = new List<string> { currentPatientSDO.LastTreatmentFee.TREATMENT_CODE };
						text += string.Format(ResourceMessage.DotKhamTruocCuaBenhNhanConNoTienVienPhi, currentPatientSDO.LastTreatmentFee.TREATMENT_CODE);
					}
				}
				else if (HisConfigCFG.IsCheckPreviousDebt == "2" && !IsEmergency && dtPatientType != null && currentPatientSDO.PreviousDebtTreatmentDetails != null && currentPatientSDO.PreviousDebtTreatmentDetails.Count > 0)
				{
					List<PreviousDebtTreatmentSDO> list2 = currentPatientSDO.PreviousDebtTreatmentDetails.Where((PreviousDebtTreatmentSDO o) => o.PATIENT_TYPE_ID == dtPatientType.ID && o.TDL_TREATMENT_CODE != currentPatientSDO.TreatmentCode && o.TDL_TREATMENT_CODE != lastSavedTreatmentCode).ToList();
					if (list2 != null && list2.Count > 0)
					{
						string arg2 = string.Join(",", list2.Select((PreviousDebtTreatmentSDO o) => o.TDL_TREATMENT_CODE).ToList());
						if (!string.IsNullOrEmpty(text))
						{
							text += "\r\n";
						}
						text += string.Format("Đợt khám/điều trị trước đó của bệnh nhân còn nợ viện phí hoặc chưa duyệt khóa viện phí. Mã hồ sơ điều trị {0}. Không cho phép tiếp đón", arg2);
						btnSave.Enabled = false;
						btnSaveAndPrint.Enabled = false;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				if (HisConfigCFG.IsCheckPreviousDebt == "4" || HisConfigCFG.IsCheckPreviousDebt == "3")
				{
					if (XtraMessageBox.Show(text, "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
					{
						btnSave.Enabled = false;
						btnSaveAndPrint.Enabled = false;
					}
				}
				else if (HisConfigCFG.IsCheckPreviousDebt != "5")
				{
					MessageManager.Show(text);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataToExamServiceReqNewestByPatient(HisPatientSDO data)
		{
			try
			{
				if (data == null)
				{
					throw new ArgumentNullException("FillDataToExamServiceReqNewestByPatient. Get HisPatientSDO is null");
				}
				if (ucServiceRoomInfo1 != null)
				{
					V_HIS_PATIENT v_HIS_PATIENT = new V_HIS_PATIENT();
					DataObjectMapper.Map<V_HIS_PATIENT>(v_HIS_PATIENT, data);
					ucServiceRoomInfo1.SetValueExamServiceRoom((AppConfigs.IsAutoFillDataRecentServiceRoom == "1") ? v_HIS_PATIENT : new V_HIS_PATIENT());
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private HeinCardData ConvertFromPatientData(HisPatientSDO patient)
		{
			HeinCardData heinCardData = null;
			try
			{
				heinCardData = new HeinCardData();
				heinCardData.Address = patient.HeinAddress;
				if (patient.IS_HAS_NOT_DAY_DOB == 1)
				{
					heinCardData.Dob = patient.DOB.ToString().Substring(0, 4);
				}
				else
				{
					heinCardData.Dob = Inventec.Common.DateTime.Convert.TimeNumberToDateString(patient.DOB);
				}
				heinCardData.FromDate = Inventec.Common.DateTime.Convert.TimeNumberToDateString(patient.HeinCardFromTime.GetValueOrDefault());
				heinCardData.ToDate = Inventec.Common.DateTime.Convert.TimeNumberToDateString(patient.HeinCardToTime.GetValueOrDefault());
				heinCardData.MediOrgCode = patient.HeinMediOrgCode;
				heinCardData.HeinCardNumber = patient.HeinCardNumber;
				heinCardData.LiveAreaCode = patient.LiveAreaCode;
				heinCardData.PatientName = patient.VIR_PATIENT_NAME;
				heinCardData.FineYearMonthDate = patient.Join5Year;
				heinCardData.Gender = GenderConvert.HisToHein(patient.GENDER_ID.ToString());
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return heinCardData;
		}

		private List<HIS_CASHIER_ROOM> GetCashierRoomByUser()
		{
			List<HIS_CASHIER_ROOM> result = new List<HIS_CASHIER_ROOM>();
			try
			{
				List<long> roomIds = WorkPlace.GetRoomIds();
				if (roomIds == null || roomIds.Count == 0)
				{
					throw new ArgumentNullException("Nguoi dung khong chon phong thu ngan nao");
				}
				result = (from o in BackendDataWorker.Get<HIS_CASHIER_ROOM>()
					where roomIds.Contains(o.ROOM_ID)
					select o).ToList();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private void GetPatientInfoFromResultData(ref AssignServiceADO assignServiceADO)
		{
			try
			{
				if (resultHisPatientProfileSDO != null)
				{
					assignServiceADO.PatientName = resultHisPatientProfileSDO.HisPatient.VIR_PATIENT_NAME;
					assignServiceADO.PatientDob = resultHisPatientProfileSDO.HisPatient.DOB;
					assignServiceADO.GenderName = BackendDataWorker.Get<HIS_GENDER>().FirstOrDefault((HIS_GENDER o) => o.ID == resultHisPatientProfileSDO.HisPatient.GENDER_ID).GENDER_NAME;
				}
				else if (currentHisExamServiceReqResultSDO != null)
				{
					assignServiceADO.PatientName = currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatient.VIR_PATIENT_NAME;
					assignServiceADO.PatientDob = currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatient.DOB;
					assignServiceADO.GenderName = BackendDataWorker.Get<HIS_GENDER>().FirstOrDefault((HIS_GENDER o) => o.ID == currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatient.GENDER_ID).GENDER_NAME;
				}
				if (currentHisExamServiceReqResultSDO != null && currentHisExamServiceReqResultSDO.ServiceReqs != null && currentHisExamServiceReqResultSDO.ServiceReqs.Count > 0)
				{
					V_HIS_SERVICE_REQ v_HIS_SERVICE_REQ = currentHisExamServiceReqResultSDO.ServiceReqs.FirstOrDefault((V_HIS_SERVICE_REQ o) => o.PRIORITY == 1);
					assignServiceADO.IsPriority = v_HIS_SERVICE_REQ != null && v_HIS_SERVICE_REQ.PRIORITY == 1;
				}
				if (HisConfigCFG.SetDefaultRequestRoomByExamRoomWhenAssigningService && serviceReqDetailSDOs != null && serviceReqDetailSDOs.Count > 0 && serviceReqDetailSDOs.Exists((ServiceReqDetailSDO o) => o.RoomId.GetValueOrDefault() > 0))
				{
					assignServiceADO.ExamRegisterRoomId = serviceReqDetailSDOs.Where((ServiceReqDetailSDO o) => o.RoomId.GetValueOrDefault() > 0).FirstOrDefault().RoomId;
					LogSystem.Info("Tiep don benh nhan___co cau hinh:" + LogUtil.TraceData(LogUtil.GetMemberName(() => HisConfigCFG.SetDefaultRequestRoomByExamRoomWhenAssigningService), HisConfigCFG.SetDefaultRequestRoomByExamRoomWhenAssigningService) + " => luon mac dinh phong chi dinh khi chi dinh dv tu tiep don theo phong kham dau tien nguoi dung chon");
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private async void ProcessPatientCodeKeydown(object data)
		{
			try
			{
				RefreshUserControl();
				if (data != null)
				{
					ResultDataADO = null;
					_HeinCardData = null;
					string heinAddressOfPatient = "";
					SetPatientSearchPanel(false);
					isReadQrCode = false;
					if (data is HisPatientSDO)
					{
						if (!AlertTreatmentInOutInDayForTreatmentMessage(data as HisPatientSDO))
						{
							currentPatientSDO = null;
							_HeinCardData = null;
							ResultDataADO = null;
							ResetPatientForm();
							return;
						}
						SetPatientSearchPanel(true);
						HisPatientSDO patient = data as HisPatientSDO;
						LoadOneBNToControl(patient, true);
						heinAddressOfPatient = patient.HeinAddress;
						_HeinCardData = ConvertFromPatientData(patient);
					}
					else if (data is HeinCardData)
					{
						_HeinCardData = (HeinCardData)data;
						isReadQrCode = true;
						string patientName = Inventec.Common.String.Convert.HexToUTF8Fix(_HeinCardData.PatientName);
						if (!string.IsNullOrEmpty(patientName))
						{
							_HeinCardData.PatientName = patientName;
						}
						string address = Inventec.Common.String.Convert.HexToUTF8Fix(_HeinCardData.Address);
						if (!string.IsNullOrEmpty(address))
						{
							_HeinCardData.Address = address;
						}
						FillDataAfterFindQrCodeNoExistsCard(_HeinCardData);
					}
					HeinGOVManager heinGOVManager = new HeinGOVManager(ResourceMessage.GoiSangCongBHXHTraVeMaLoi);
					UCServiceReqInfoADO ado = ucOtherServiceReqInfo1.GetValue();
					if (ucPatientRaw1 != null)
					{
						UCPatientRawADO patientRawADO = ucPatientRaw1.GetValue();
						if (patientRawADO.PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT)
						{
							DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ado.IntructionTime).Value;
							if (HisConfigCFG.IsBlockingInvalidBhyt == 1.ToString() || HisConfigCFG.IsBlockingInvalidBhyt == 2.ToString())
							{
								heinGOVManager.SetDelegateHeinEnableButtonSave(new DelegateHeinEnableButtonSave(HeinEnableSave));
							}
							ResultDataADO = await heinGOVManager.Check(_HeinCardData, null, false, heinAddressOfPatient, time, isReadQrCode);
						}
					}
					if (ResultDataADO != null)
					{
						ucPatientRaw1.ResultDataADO = ResultDataADO;
						if (!string.IsNullOrEmpty(_HeinCardData.HeinCardNumber) && ResultDataADO.IsShowQuestionWhileChangeHeinTime__Choose)
						{
							_HeinCardData.HeinCardNumber = ResultDataADO.ResultHistoryLDO.maTheMoi;
						}
					}
					if (isReadQrCode)
					{
						ProcessQrCodeData(_HeinCardData);
					}
					if (ucPatientRaw1 != null)
					{
						UCPatientRawADO patientRawADO2 = ucPatientRaw1.GetValue();
						if (patientRawADO2.PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT)
						{
							await CheckTTProcessResultData(_HeinCardData);
						}
					}
					UCHeinProcessFillDataCareerUnder6AgeByHeinCardNumber(_HeinCardData, true);
				}
				else
				{
					int n;
					bool isNumeric = int.TryParse(ucPatientRaw1.txtPatientCode.Text, out n);
					string codeFind = ((!isNumeric) ? ucPatientRaw1.txtPatientCode.Text : string.Format("{0:0000000000}", System.Convert.ToInt64(ucPatientRaw1.txtPatientCode.Text)));
					ucPatientRaw1.txtPatientCode.Text = codeFind;
					XtraMessageBox.Show(ResourceMessage.MaBenhNhanKhongTontai + " '" + codeFind + "'", Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao));
					ucPatientRaw1.txtPatientCode.Focus();
					ucPatientRaw1.txtPatientCode.SelectAll();
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private bool AlertTreatmentInOutInDayForTreatmentMessage(HisPatientSDO patientDTO)
		{
			bool result = true;
			isAlertTreatmentEndInDay = false;
			try
			{
				string text = "";
				if (HisConfigCFG.IsCheckTodayFinishTreatment && patientDTO.ID > 0 && patientDTO.TodayFinishTreatments != null && patientDTO.TodayFinishTreatments.Count > 0)
				{
					string text2 = string.Join(",", patientDTO.TodayFinishTreatments);
					if (!string.IsNullOrEmpty(text2))
					{
						LogSystem.Debug("Tiep don: tim thay benh nhan cu co dot dieu tri gan nhat ra vien trong ngay: " + LogUtil.TraceData("HisPatientSDO", patientDTO));
						text += string.Format(ResourceMessage.DotDieuTriGanNhatCuaBenhNhanCoNgayRaLaHomNay, text2);
					}
					if (!string.IsNullOrEmpty(text) && XtraMessageBox.Show(text, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						result = false;
						isAlertTreatmentEndInDay = true;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private void LoadOneBNToControl(HisPatientSDO patientDTO, bool isReloadUCHein)
		{
			try
			{
				if (patientDTO != null)
				{
					LogSystem.Debug("Bat dau gan du lieu benh nhan len form");
					currentPatientSDO = patientDTO;
					FillDataPatientToControl(patientDTO);
					LogSystem.Debug("t3.3. Call uc BHYT Load Combo TheBHYT cua benh nhan id = " + patientDTO.ID);
					if (isReloadUCHein && mainHeinProcessor != null && ucHeinInfo1 != null)
					{
						mainHeinProcessor.FillDataToHeinInsuranceInfoByOldPatient(ucHeinBHYT, patientDTO);
					}
					SetPatientSearchPanel(true);
					LogSystem.Debug("SetPatientSearchPanel");
					FillDataToExamServiceReqNewestByPatient(patientDTO);
					LogSystem.Debug("Fill du lieu yeu cau kham moi nhat cua benh nhan (neu co)");
					PeriosTreatmentMessage();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataPatientToControl(HisPatientSDO patientDTO)
		{
			try
			{
				LogSystem.Warn("FillDataPatientToControl");
				if (patientDTO == null)
				{
					throw new ArgumentNullException("patientDTO is null");
				}
				LogSystem.Debug("DOB " + patientDTO.DOB);
				if (patientDTO.DOB > 0 && patientDTO.DOB.ToString().Length >= 6)
				{
					if (patientDTO.IS_HAS_NOT_DAY_DOB == 1)
					{
						LoadNgayThangNamSinhBNToForm(patientDTO.DOB, true);
					}
					else
					{
						LoadNgayThangNamSinhBNToForm(patientDTO.DOB, false);
					}
				}
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				UCPatientRawADO uCPatientRawADO = new UCPatientRawADO();
				uCPatientRawADO.CARRER_ID = patientDTO.CAREER_ID;
				uCPatientRawADO.DOB = patientDTO.DOB;
				uCPatientRawADO.GENDER_ID = patientDTO.GENDER_ID;
				uCPatientRawADO.HEIN_CARD_NUMBER = patientDTO.HeinCardNumber;
				uCPatientRawADO.IS_HAS_NOT_DAY_DOB = patientDTO.IS_HAS_NOT_DAY_DOB;
				uCPatientRawADO.PATIENT_CODE = patientDTO.PATIENT_CODE;
				uCPatientRawADO.PATIENT_NAME = patientDTO.VIR_PATIENT_NAME;
				if (value != null)
				{
					uCPatientRawADO.PATIENTTYPE_ID = value.PATIENTTYPE_ID;
				}
				HIS_CAREER hIS_CAREER = GetCareerByBhytWhiteListConfig(patientDTO.HeinCardNumber);
				if (hIS_CAREER != null)
				{
					FillDataCareerUnder6AgeByHeinCardNumber(patientDTO.HeinCardNumber);
				}
				else
				{
					hIS_CAREER = BackendDataWorker.Get<HIS_CAREER>().SingleOrDefault((HIS_CAREER o) => o.ID == patientDTO.CAREER_ID);
				}
				if (hIS_CAREER != null && hIS_CAREER.ID > 0)
				{
					uCPatientRawADO.CARRER_ID = patientDTO.CAREER_ID;
					uCPatientRawADO.CARRER_CODE = hIS_CAREER.CAREER_CODE;
				}
				bool flag = BhytPatientTypeData.IsChild(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(patientDTO.DOB) ?? DateTime.Now);
				if (flag)
				{
					SetValidationByChildrenUnder6Years(flag, false);
				}
				uCPatientRawADO.PATIENT_CLASSIFY_ID = patientDTO.PATIENT_CLASSIFY_ID;
				uCPatientRawADO.MILITARY_RANK_ID = patientDTO.MILITARY_RANK_ID;
				uCPatientRawADO.POSITION_ID = patientDTO.POSITION_ID;
				uCPatientRawADO.WORK_PLACE_ID = patientDTO.WORK_PLACE_ID;
				uCPatientRawADO.ETHNIC_CODE = patientDTO.ETHNIC_CODE;
				uCPatientRawADO.ETHNIC_NAME = patientDTO.ETHNIC_NAME;
				ucPatientRaw1.SetValue(uCPatientRawADO);
				UCAddressADO value2 = ucAddressCombo1.GetValue();
				value2.District_Code = patientDTO.DISTRICT_CODE;
				value2.District_Name = patientDTO.DISTRICT_NAME;
				value2.Province_Code = patientDTO.PROVINCE_CODE;
				value2.Province_Name = patientDTO.PROVINCE_NAME;
				value2.Commune_Code = patientDTO.COMMUNE_CODE;
				value2.Commune_Name = patientDTO.COMMUNE_NAME;
				value2.Address = patientDTO.ADDRESS;
				value2.Phone = patientDTO.PHONE;
				ucAddressCombo1.SetValue(value2);
				UCRelativeADO value3 = ucRelativeInfo1.GetValue();
				value3.RelativeAddress = patientDTO.RELATIVE_ADDRESS;
				value3.RelativeCMND = patientDTO.RELATIVE_CMND_NUMBER;
				value3.RelativeName = patientDTO.RELATIVE_NAME;
				value3.FatherName = patientDTO.FATHER_NAME;
				value3.MotherName = patientDTO.MOTHER_NAME;
				value3.RelativePhone = patientDTO.RELATIVE_PHONE;
				if (flag)
				{
					ucRelativeInfo1.SetValue(value3);
				}
				else
				{
					ucRelativeInfo1.SetValue(value3, flag);
				}
				UCServiceReqInfoADO value4 = ucOtherServiceReqInfo1.GetValue();
				value4.PATIENT_CLASSIFY_ID = patientDTO.PATIENT_CLASSIFY_ID;
				value4.NOTE = patientDTO.NOTE;
				value4.IsHiv = patientDTO.IS_HIV == 1;
				ucOtherServiceReqInfo1.SetValue(value4);
				HisPatientProfileSDO value5 = ucHeinInfo1.GetValue();
				ucHeinInfo1.SetValue(patientDTO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadNgayThangNamSinhBNToForm(long dob, bool hasNotDayDob)
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				LogSystem.Debug("Bat dau gan du lieu nam sinh benh nhan len form. p1: tinh toan nam sinh");
				if (dob > 0)
				{
					LogSystem.Warn("LoadNgayThangNamSinhBNToForm");
					DateTime dateOfBirth = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(dob) ?? DateTime.MinValue;
					bool hasDobCertificate = BhytPatientTypeData.IsChild(dateOfBirth);
					value.DOB = dob;
					isNotPatientDayDob = hasNotDayDob;
					if (hasNotDayDob)
					{
						value.DOB_STR = dateOfBirth.ToString("yyyy");
					}
					else
					{
						value.DOB_STR = dateOfBirth.ToString("dd/MM/yyyy");
					}
					CalculatePatientAge.AgeObject ageObject = CalculatePatientAge.Calculate(dob);
					if (mainHeinProcessor != null)
					{
						LogSystem.Debug("Bat dau gan du lieu nam sinh benh nhan len form. p2: goi uc bhyt update checkbox co giay khai sinh hay khong");
						mainHeinProcessor.UpdateHasDobCertificateEnable(ucHeinBHYT, hasDobCertificate);
						LogSystem.Debug("Bat dau gan du lieu nam sinh benh nhan len form. p3: ket thuc update");
					}
					ucPatientRaw1.SetValue(value);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetValidationByChildrenUnder6Years(bool isTreSoSinh, bool isHasReset)
		{
		}

		private void ProcessQrCodeData(HeinCardData dataHein)
		{
			try
			{
				string currentHeincardNumber = dataHein.HeinCardNumber;
				if (dataHein == null)
				{
					throw new ArgumentNullException("ProcessQrCodeData => dataHein is null");
				}
				if (!string.IsNullOrEmpty(dataHein.HeinCardNumber))
				{
					if (dataHein.HeinCardNumber.Length > 15)
					{
						dataHein.HeinCardNumber = dataHein.HeinCardNumber.Substring(0, 15);
					}
					else if (dataHein.HeinCardNumber.Length < 15)
					{
						LogSystem.Debug("Do dai so the bhyt cua benh nhan khong hop le. " + LogUtil.TraceData(LogUtil.GetMemberName(() => currentHeincardNumber), currentHeincardNumber));
					}
				}
				CommonParam commonParam = new CommonParam();
				HisPatientAdvanceFilter hisPatientAdvanceFilter = new HisPatientAdvanceFilter();
				hisPatientAdvanceFilter.HEIN_CARD_NUMBER__EXACT = dataHein.HeinCardNumber;
				List<HisPatientSDO> list = new BackendAdapter(commonParam).Get<List<HisPatientSDO>>("api/HisPatient/GetSdoAdvance", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisPatientAdvanceFilter, new Action(SessionManager.ActionLostToken), commonParam);
				if (list != null && list.Count > 0)
				{
					if (list.Count > 1)
					{
						LogSystem.Debug("Quet the BHYT tim thay " + list.Count + " benh nhan cu => mo form chon benh nhan => chon 1 => fill du lieu bn duoc chon.");
						frmPatientChoice frmPatientChoice2 = new frmPatientChoice(list, new UpdatePatientInfo(SelectOnePatientProcess));
						frmPatientChoice2.ShowDialog();
					}
					else
					{
						LogSystem.Debug("Quet the BHYT tim thay thong tin bhyt cua benh nhan cu theo so the HeinCardNumber = " + dataHein.HeinCardNumber + ". " + LogUtil.TraceData("HisPatientSDO searched", list[0]));
						SelectOnePatientProcess(list[0]);
						SetPatientSearchPanel(true);
						ucPatientRaw1.txtPatientCode.Text = list[0].PATIENT_CODE;
					}
				}
				else
				{
					LogSystem.Debug("Quet the BHYT khong tim thay Bn cu => fill du lieu theo du lieu gih tren the bhyt");
					currentPatientSDO = null;
					FillDataAfterFindQrCodeNoExistsCard(dataHein);
					ucPatientRaw1.txtPatientCode.Text = "";
					ucPatientRaw1.txtPatientCode.Update();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SelectOnePatientProcess(HisPatientSDO patient)
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				if (!AlertTreatmentInOutInDayForTreatmentMessage(patient))
				{
					currentPatientSDO = null;
					ResetPatientForm();
					return;
				}
				LogSystem.Debug("SelectOnePatientProcess => t1. Gan thong tin cua benh nhan len form");
				actionType = 1;
				SetPatientSearchPanel(true);
				if (value.DOB.ToString().Length == 4)
				{
					patient.IS_HAS_NOT_DAY_DOB = (short)1;
				}
				else
				{
					patient.IS_HAS_NOT_DAY_DOB = null;
				}
				currentPatientSDO = patient;
				LoadOneBNToControlWithThread();
				LogSystem.Debug("SelectOnePatientProcess => t2. SetDefaultFocusUserControl");
				SetDefaultFocusUserControl();
				if (AppConfigs.DangKyTiepDonHienThiThongBaoTimDuocBenhNhan == 1)
				{
					XtraMessageBox.Show(ResourceMessage.TimDuocMotBenhNhanTheoThongTinNguoiDungNhapNeuKhongPhaiBNCuVuiLongNhanNutBNMoi, ResourceMessage.TieuDeCuaSoThongBaoLaThongBao, DefaultBoolean.True);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void LoadOneBNToControlWithThread()
		{
			try
			{
				FillDataPatientToControlWithThread();
				FillDataToExamServiceReqNewestByPatientWithThread();
				PeriosTreatmentMessage();
				GetCareerByConfig();
				FocusInServiceRoomInfo();
				if (isReadQrCode)
				{
					FillDataAfterFindQrCodeNoExistsCardWithThread();
				}
				else
				{
					FillDataToHeinInsuranceInfoUCHeinByOldPatientWithThread();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataPatientToControlWithThread()
		{
			try
			{
				FillDataPatientToControl(currentPatientSDO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataToExamServiceReqNewestByPatientWithThread()
		{
			try
			{
				FillDataToExamServiceReqNewestByPatient(currentPatientSDO);
				LogSystem.Debug("Fill du lieu yeu cau kham moi nhat cua benh nhan (neu co)");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void GetCareerByConfig()
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				HIS_CAREER hIS_CAREER = GetCareerByBhytWhiteListConfig(_HeinCardData.HeinCardNumber);
				if (hIS_CAREER != null)
				{
					hIS_CAREER = HisConfigCFG.CareerUnder6Age;
				}
				else if (Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB) != DateTime.MinValue)
				{
					DateTime value2 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB).Value;
					bool flag = true;
					hIS_CAREER = ((Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB) != DateTime.MinValue && BhytPatientTypeData.IsChild(value2)) ? HisConfigCFG.CareerUnder6Age : ((DateTime.Now.Year - Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB).Value.Year > 18) ? HisConfigCFG.CareerBase : HisConfigCFG.CareerHS));
				}
				if (hIS_CAREER == null)
				{
					hIS_CAREER = HisConfigCFG.CareerBase;
				}
				if (hIS_CAREER != null && hIS_CAREER.ID > 0)
				{
					value.CARRER_ID = hIS_CAREER.ID;
					value.CARRER_CODE = hIS_CAREER.CAREER_CODE;
				}
				LogSystem.Warn("GetCareerByConfig");
				ucPatientRaw1.SetValue(value);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FocusInServiceRoomInfo()
		{
			try
			{
				if (roomExamServiceProcessor == null)
				{
					return;
				}
				foreach (Control control in ucHeinInfo1.Controls)
				{
					if (control != null && (control is UserControl || control is XtraUserControl))
					{
						roomExamServiceProcessor.FocusAndShow(control);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataAfterFindQrCodeNoExistsCardWithThread()
		{
			try
			{
				FillDataAfterFindQrCodeNoExistsCard(_HeinCardData);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataToHeinInsuranceInfoUCHeinByOldPatientWithThread()
		{
			try
			{
				if (mainHeinProcessor != null && ucHeinBHYT != null)
				{
					mainHeinProcessor.FillDataToHeinInsuranceInfoByOldPatient(ucHeinBHYT, currentPatientSDO);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private async Task CheckTTProcessResultData(HeinCardData dataHein)
		{
			try
			{
				LogSystem.Debug("dataHein" + ((dataHein != null) ? dataHein.ToString() : null));
				UCPatientRawADO patient = ucPatientRaw1.GetValue();
				if (isNotCheckTT)
				{
					return;
				}
				if (ResultDataADO != null && ResultDataADO.ResultHistoryLDO != null)
				{
					dataHein.FineYearMonthDate = ResultDataADO.ResultHistoryLDO.ngayDu5Nam;
					if (ResultDataADO.IsShowQuestionWhileChangeHeinTime__Choose)
					{
						long dataGenderId = GenderConvert.HeinToHisNumber(dataHein.Gender);
						HIS_GENDER gender = BackendDataWorker.Get<HIS_GENDER>().SingleOrDefault((HIS_GENDER o) => o.ID == dataGenderId);
						if (gender != null && gender.ID > 0)
						{
							patient.GENDER_ID = gender.ID;
						}
						patient.PATIENT_NAME = ResultDataADO.ResultHistoryLDO.hoTen;
						if (mainHeinProcessor != null && ucHeinBHYT != null)
						{
							mainHeinProcessor.FillDataAfterCheckBHYT(ucHeinBHYT, dataHein);
						}
					}
					if (ResultDataADO.IsToDate)
					{
						if (mainHeinProcessor != null && ucHeinBHYT != null)
						{
							mainHeinProcessor.FillDataAfterCheckBHYT(ucHeinBHYT, ResultDataADO.HeinCardData);
						}
						LogSystem.Debug("Ket thuc gan du lieu cho benh nhan khi doc the va khong co han den");
					}
					if (ResultDataADO.IsThongTinNguoiDungThayDoiSoVoiCong__Choose)
					{
						long dataGenderId2 = GenderConvert.HeinToHisNumber(dataHein.Gender);
						HIS_GENDER gender2 = BackendDataWorker.Get<HIS_GENDER>().SingleOrDefault((HIS_GENDER o) => o.ID == dataGenderId2);
						if (gender2 != null && gender2.ID > 0)
						{
							patient.GENDER_ID = gender2.ID;
						}
						patient.PATIENT_NAME = ResultDataADO.ResultHistoryLDO.hoTen;
						if (mainHeinProcessor != null && ucHeinBHYT != null)
						{
							mainHeinProcessor.FillDataAfterCheckBHYT(ucHeinBHYT, dataHein);
						}
					}
					if (HisConfigCFG.IsCheckExamHistory && ResultDataADO.ResultHistoryLDO != null)
					{
						if (ResultDataADO.IsShowQuestionWhileChangeHeinTime__Choose || ResultDataADO.SuccessWithoutMessage)
						{
							LogSystem.Debug("Mo form lich su voi data rsIns");
							frmCheckHeinCardGOV frm = new frmCheckHeinCardGOV(ResultDataADO.ResultHistoryLDO);
							frm.ShowDialog();
							ucCheckTT1.FillDataIntoUCCheckTT(ResultDataADO.ResultHistoryLDO);
						}
						else
						{
							ucCheckTT1.ResetDataControl(ResultDataADO.ResultHistoryLDO);
						}
					}
					LogSystem.Debug("CheckTTProcessResultData 1");
					LogSystem.Debug("CheckHanSDTheBHYT => 3");
				}
				ucPatientRaw1.SetValue(patient);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private void AutoCheckPriorityByPriorityType(long patientDob, string heinCardNumber)
		{
			try
			{
				LogSystem.Debug("patientDob " + patientDob);
				LogSystem.Debug("heinCardNumber " + heinCardNumber);
				bool flag = false;
				long patientAge = 0L;
				UCServiceReqInfoADO value = ucOtherServiceReqInfo1.GetValue();
				List<HIS_PRIORITY_TYPE> list = BackendDataWorker.Get<HIS_PRIORITY_TYPE>();
				if (list != null && list.Count > 0 && (patientDob > 0 || !string.IsNullOrEmpty(heinCardNumber)))
				{
					DateTime value2 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(patientDob).Value;
					long ticks = (DateTime.Now - value2).Ticks;
					int num = new DateTime(ticks).Year - 1;
					patientAge = ((num == 0) ? 1 : num);
					List<HIS_PRIORITY_TYPE> list2 = list.Where((HIS_PRIORITY_TYPE o) => (!o.AGE_FROM.HasValue || (o.AGE_FROM.HasValue && o.AGE_FROM <= patientAge)) && (!o.AGE_TO.HasValue || (o.AGE_TO.HasValue && o.AGE_TO >= patientAge)) && (string.IsNullOrEmpty(o.BHYT_PREFIXS) || (!string.IsNullOrEmpty(o.BHYT_PREFIXS) && StartIn(o.BHYT_PREFIXS, heinCardNumber))) && ((o.AGE_FROM.HasValue && o.AGE_FROM > 0) || (o.AGE_TO.HasValue && o.AGE_TO > 0) || !string.IsNullOrEmpty(o.BHYT_PREFIXS))).ToList();
					flag = list2 != null && list2.Count > 0;
					if (flag)
					{
						value.IsPriority = flag;
						value.PriorityType = list2.FirstOrDefault().ID;
					}
					else
					{
						value.IsPriority = false;
						value.PriorityType = null;
					}
					ucOtherServiceReqInfo1.SetValue(value);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private bool StartIn(string BHYT_PREFIXS, string heincardnumber)
		{
			bool result = false;
			try
			{
				List<string> list = null;
				if (!string.IsNullOrEmpty(BHYT_PREFIXS) && !string.IsNullOrEmpty(heincardnumber))
				{
					string[] array = BHYT_PREFIXS.Split(new string[2] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
					if (array != null && array.Count() > 0)
					{
						list = (from o in array.ToList()
							where heincardnumber.StartsWith(o)
							select o).ToList();
						result = ((list != null && list.Count > 0) ? true : false);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private List<HIS_PATIENT_TYPE> LoadPatientTypeExamByPatientType(long patientTypeId)
		{
			List<HIS_PATIENT_TYPE> result = null;
			try
			{
				CommonParam commonParam = new CommonParam();
				HisPatientTypeAllowFilter hisPatientTypeAllowFilter = new HisPatientTypeAllowFilter();
				hisPatientTypeAllowFilter.PATIENT_TYPE_ID = patientTypeId;
				List<HIS_PATIENT_TYPE_ALLOW> list = new BackendAdapter(commonParam).Get<List<HIS_PATIENT_TYPE_ALLOW>>("api/HisPatientTypeAllow/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisPatientTypeAllowFilter, new Action(SessionManager.ActionLostToken), commonParam);
				List<HIS_PATIENT_TYPE> source = BackendDataWorker.Get<HIS_PATIENT_TYPE>();
				if (list != null && list.Count > 0)
				{
					long[] arrpatientTypeAllowIds = list.Select((HIS_PATIENT_TYPE_ALLOW o) => o.PATIENT_TYPE_ID).ToArray();
					result = source.Where((HIS_PATIENT_TYPE o) => arrpatientTypeAllowIds.Contains(o.ID)).ToList();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private void RefreshUserControl()
		{
			try
			{
				currentHisExamServiceReqResultSDO = null;
				serviceReqDetailSDOs = null;
				resultHisPatientProfileSDO = null;
				lst = new List<string>();
				lstSend = new List<string>();
				lstPreviousDebtTreatmentsRegister = new List<string>();
				EmergencyBol = false;
				treatmentTypeID = 0L;
				dataAddressPatient = new UCAddressADO();
				ucHeinInfo1.RefreshUserControl();
				ucPatientRaw1.RefreshUserControl();
				ucAddressCombo1.RefreshUserControl();
				ucImageInfo1.RefreshUserControl();
				ucOtherServiceReqInfo1.RefreshUserControl();
				ucRelativeInfo1.RefreshUserControl();
				ucPlusInfo1.RefreshUserControl();
				SetPatientSearchPanel(false);
				EnableControl(true);
				ucCheckTT1.ResetData();
				ucServiceRoomInfo1.RefreshUserControl();
				transPatiADO = null;
				actionType = 1;
				frm = null;
				ValidatedTTCT = false;
				ResetVariableUCAddress(false);
				_TreatmnetIdByAppointmentCode = 0L;
				cardSearch = null;
				ucHeinInfo1.RefreshUserControl();
				ucPatientRaw1.FocusUserControl();
				UCPatientRawADO uCPatientRawADO = ((ucPatientRaw1 != null) ? ucPatientRaw1.GetValue() : null);
				if (uCPatientRawADO != null && uCPatientRawADO.PATIENTTYPE_ID > 0)
				{
					ucOtherServiceReqInfo1.ChangePatientType(uCPatientRawADO.PATIENTTYPE_ID);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ResetVariableUCAddress(bool isTrue)
		{
			try
			{
				ucAddressCombo1.isReadCard = isTrue;
				ucAddressCombo1.isPatientBHYT = isTrue;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetPatientSearchPanel(bool isFinded)
		{
			try
			{
				if (isFinded)
				{
					lcibtnPatientNewInfo.Visibility = LayoutVisibility.Always;
				}
				else
				{
					currentPatientSDO = null;
					lcibtnPatientNewInfo.Visibility = LayoutVisibility.Never;
				}
				LogSystem.Debug("SetPatientSearchPanel");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void EnableControl(bool _isEnable)
		{
			try
			{
				SimpleButton simpleButton = btnSave;
				SimpleButton simpleButton2 = btnSaveAndPrint;
				bool flag = (btnTTChuyenTuyen.Enabled = _isEnable);
				bool enabled = (simpleButton2.Enabled = flag);
				simpleButton.Enabled = enabled;
				DropDownButton dropDownButton = dropDownButton__Other;
				SimpleButton simpleButton3 = btnDepositDetail;
				SimpleButton simpleButton4 = btnDepositRequest;
				SimpleButton simpleButton5 = btnGiayTo;
				SimpleButton simpleButton6 = btnPrint;
				bool flag4 = (btnSaveAndAssain.Enabled = !_isEnable);
				bool flag6 = (simpleButton6.Enabled = flag4);
				bool flag8 = (simpleButton5.Enabled = flag6);
				flag = (simpleButton4.Enabled = flag8);
				enabled = (simpleButton3.Enabled = flag);
				dropDownButton.Enabled = enabled;
				HIS_PATIENT_TYPE_ALTER hIS_PATIENT_TYPE_ALTER = null;
				if (currentHisExamServiceReqResultSDO != null && currentHisExamServiceReqResultSDO.HisPatientProfile != null && currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatientTypeAlter != null)
				{
					hIS_PATIENT_TYPE_ALTER = currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatientTypeAlter;
				}
				if (resultHisPatientProfileSDO != null && resultHisPatientProfileSDO.HisPatientTypeAlter != null)
				{
					hIS_PATIENT_TYPE_ALTER = resultHisPatientProfileSDO.HisPatientTypeAlter;
				}
				if (hIS_PATIENT_TYPE_ALTER != null)
				{
					if (hIS_PATIENT_TYPE_ALTER.TREATMENT_TYPE_ID != 1)
					{
						btnTreatmentBedRoom.Enabled = !_isEnable;
					}
					else
					{
						btnTreatmentBedRoom.Enabled = false;
					}
				}
				else
				{
					btnTreatmentBedRoom.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		protected bool CheckPreviousDebtTreatment()
		{
			bool result = true;
			try
			{
				if (lst != null && lst.Count > 0 && !EmergencyBol && treatmentTypeID > 0 && treatmentTypeID == 1)
				{
					WaitingManager.Hide();
					XtraMessageBox.Show(string.Format(ResourceMessage.NoVienPhi, string.Join(", ", lst.Distinct())), ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK);
					result = false;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		private void Save(bool printNow)
		{
			try
			{
				resultHisPatientProfileSDO = null;
				currentHisExamServiceReqResultSDO = null;
				isShowMess = false;
				bool flag = false;
				CommonParam param = new CommonParam();
				if (CheckValidateForSave(param))
				{
					if (!CheckTreatmentOrder())
					{
						return;
					}
					if (chkAssignDoctor.Checked && !CheckAssignedExecuteLoginName())
					{
						XtraMessageBox.Show(ResourceMessage.ChuaCoThongTinBacSiKham, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					UCPatientRawADO patientRawInfoValue = ucPatientRaw1.GetValue();
					if (HisConfigCFG.IsCheckPreviousDebt == "3")
					{
						UCServiceReqInfoADO value = ucOtherServiceReqInfo1.GetValue();
						lst = patientRawInfoValue.lstPreviousDebtTreatments;
						if (lst == null || lst.Count < 1)
						{
							lst = lstSend;
						}
						EmergencyBol = value.IsEmergency;
						treatmentTypeID = value.TreatmentType_ID;
						if (!CheckPreviousDebtTreatment())
						{
							return;
						}
					}
					string text = null;
					string text2 = null;
					string text3 = null;
					List<string> list = new List<string>();
					if (serviceReqDetailSDOs != null && serviceReqDetailSDOs.Count > 0 && serviceReqDetailSDOs.Where((ServiceReqDetailSDO o) => o.ServiceId > 0) != null && serviceReqDetailSDOs.Where((ServiceReqDetailSDO o) => o.ServiceId > 0).ToList().Count > 0)
					{
						foreach (ServiceReqDetailSDO item in serviceReqDetailSDOs.Where((ServiceReqDetailSDO o) => o.ServiceId > 0))
						{
							V_HIS_SERVICE v_HIS_SERVICE = lstService.FirstOrDefault((V_HIS_SERVICE o) => o.ID == item.ServiceId);
							if (v_HIS_SERVICE != null && v_HIS_SERVICE.GENDER_ID.HasValue && v_HIS_SERVICE.GENDER_ID != patientRawInfoValue.GENDER_ID)
							{
								List<HIS_GENDER> source = BackendDataWorker.Get<HIS_GENDER>();
								text3 = source.FirstOrDefault((HIS_GENDER o) => o.ID == patientRawInfoValue.GENDER_ID).GENDER_NAME;
								list.Add(v_HIS_SERVICE.SERVICE_NAME);
							}
							DateTime? dateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(patientRawInfoValue.DOB);
							long ticks = (DateTime.Now.Date - dateTime.Value.Date).Ticks;
							DateTime dateTime2 = new DateTime(ticks);
							int num = (dateTime2.Year - 1) * 12 + dateTime2.Month - 1;
							string text4 = null;
							long? num2 = 0L;
							if ((!v_HIS_SERVICE.AGE_FROM.HasValue || !(v_HIS_SERVICE.AGE_FROM > num)) && (!v_HIS_SERVICE.AGE_TO.HasValue || !(v_HIS_SERVICE.AGE_TO < num)))
							{
								continue;
							}
							if (v_HIS_SERVICE.AGE_FROM.HasValue && !v_HIS_SERVICE.AGE_TO.HasValue)
							{
								if (v_HIS_SERVICE.AGE_FROM < 72)
								{
									num2 = v_HIS_SERVICE.AGE_FROM;
									text4 = "tháng tuổi";
								}
								else if (v_HIS_SERVICE.AGE_FROM >= 72)
								{
									num2 = v_HIS_SERVICE.AGE_FROM / 12;
									text4 = "tuổi";
								}
								string[] obj = new string[8] { text, "Dịch vụ ", v_HIS_SERVICE.SERVICE_NAME, " chỉ cho phép chỉ định với bệnh nhân từ ", null, null, null, null };
								long? num3 = num2;
								obj[4] = num3.ToString();
								obj[5] = " ";
								obj[6] = text4;
								obj[7] = "\r\n";
								text = string.Concat(obj);
							}
							else if (v_HIS_SERVICE.AGE_TO.HasValue && !v_HIS_SERVICE.AGE_FROM.HasValue)
							{
								if (v_HIS_SERVICE.AGE_TO < 72)
								{
									num2 = v_HIS_SERVICE.AGE_TO;
									text4 = "tháng tuổi";
								}
								else if (v_HIS_SERVICE.AGE_TO >= 72)
								{
									num2 = v_HIS_SERVICE.AGE_TO / 12;
									text4 = "tuổi";
								}
								string[] obj2 = new string[8] { text, "Dịch vụ ", v_HIS_SERVICE.SERVICE_NAME, " chỉ cho phép chỉ định với bệnh nhân dưới ", null, null, null, null };
								long? num3 = num2;
								obj2[4] = num3.ToString();
								obj2[5] = " ";
								obj2[6] = text4;
								obj2[7] = "\r\n";
								text = string.Concat(obj2);
							}
							else if (v_HIS_SERVICE.AGE_TO.HasValue && v_HIS_SERVICE.AGE_FROM.HasValue)
							{
								string text5 = null;
								long? num4 = 0L;
								if (v_HIS_SERVICE.AGE_FROM < 72)
								{
									num2 = v_HIS_SERVICE.AGE_FROM;
									text4 = "tháng tuổi";
								}
								else if (v_HIS_SERVICE.AGE_FROM >= 72)
								{
									num2 = v_HIS_SERVICE.AGE_FROM / 12;
									text4 = "tuổi";
								}
								if (v_HIS_SERVICE.AGE_TO < 72)
								{
									num4 = v_HIS_SERVICE.AGE_TO;
									text5 = "tháng tuổi";
								}
								else if (v_HIS_SERVICE.AGE_TO >= 72)
								{
									num4 = v_HIS_SERVICE.AGE_TO / 12;
									text5 = "tuổi";
								}
								string[] obj3 = new string[12]
								{
									text, "Dịch vụ ", v_HIS_SERVICE.SERVICE_NAME, " chỉ cho phép chỉ định với bệnh nhân từ ", null, null, null, null, null, null,
									null, null
								};
								long? num3 = num2;
								obj3[4] = num3.ToString();
								obj3[5] = " ";
								obj3[6] = text4;
								obj3[7] = " đến ";
								num3 = num4;
								obj3[8] = num3.ToString();
								obj3[9] = " ";
								obj3[10] = text5;
								obj3[11] = "\r\n";
								text = string.Concat(obj3);
							}
						}
						if (list != null && list.Count > 0)
						{
							text2 = text2 + "Dịch vụ " + string.Join(", ", list) + " không cho phép chỉ định đối với bệnh nhân giới tính " + text3 + "\r\n";
							XtraMessageBox.Show(text2, HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao), MessageBoxButtons.OK);
							return;
						}
						if (!string.IsNullOrEmpty(text))
						{
							XtraMessageBox.Show(text, HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TieuDeCuaSoThongBaoLaThongBao), MessageBoxButtons.OK);
							return;
						}
					}
					if (!CheckDuplicateCCCD())
					{
						return;
					}
					try
					{
						WaitingManager.Show(base.ParentForm);
						IAppDelegacyT appDelegacyT = new ServiceRequestRegister(param, this, currentPatientSDO);
						switch (GlobalStore.currentFactorySaveType)
						{
						case UCServiceRequestRegisterFactorySaveType.REGISTER:
							currentHisExamServiceReqResultSDO = appDelegacyT.Execute<HisServiceReqExamRegisterResultSDO>();
							LogSystem.Debug("Save.1");
							if (currentHisExamServiceReqResultSDO == null || currentHisExamServiceReqResultSDO.HisPatientProfile == null || currentHisExamServiceReqResultSDO.ServiceReqs == null || currentHisExamServiceReqResultSDO.ServiceReqs.Count <= 0)
							{
								break;
							}
							resultHisPatientProfileSDO = currentHisExamServiceReqResultSDO.HisPatientProfile;
							ExamRegisterSuccess(param);
							if (currentHisExamServiceReqResultSDO.ServiceReqs.Count > 0 && AppConfigs.IsDangKyQuaTongDai == "1")
							{
								frmServiceReqChoice frmServiceReqChoice2 = new frmServiceReqChoice(currentHisExamServiceReqResultSDO.SereServs, currentHisExamServiceReqResultSDO.ServiceReqs);
								frmServiceReqChoice2.ShowDialog();
							}
							ServiceReqList = currentHisExamServiceReqResultSDO.ServiceReqs;
							if (transPatiADO != null && transPatiADO.TRANSFER_IN_TIME_FROM.HasValue)
							{
								LogSystem.Info("Saved transfer info: " + LogUtil.TraceData(LogUtil.GetMemberName(() => transPatiADO), transPatiADO));
							}
							isPrintNow = printNow;
							if ((isSaveWithRoomHasConfigAllowNotChooseService || printNow) && (chkPrintExam.Checked || chkSignExam.Checked))
							{
								Print(true);
							}
							if (printNow)
							{
								if (chkPrintPatientCard.Checked)
								{
									PrintPatientCard();
								}
								if (chkAutoCreateBill.Checked)
								{
									PrintBienLai();
								}
								if (chkAutoDeposit.Checked)
								{
									PrintTamThu();
									if (currentHisExamServiceReqResultSDO.Transactions != null && GlobalVariables.RefreshSessionDepositInfo != null)
									{
										List<V_HIS_TRANSACTION> source2 = currentHisExamServiceReqResultSDO.Transactions.Where((V_HIS_TRANSACTION o) => o.TRANSACTION_TYPE_ID == 1).ToList();
										long num5 = source2.Max((V_HIS_TRANSACTION o) => o.NUM_ORDER);
										GlobalVariables.RefreshSessionDepositInfo(num5);
									}
								}
							}
							if (currentHisExamServiceReqResultSDO.Transactions != null && currentHisExamServiceReqResultSDO.Transactions.Count > 0 && GlobalVariables.RefreshUsingAccountBookModule != null)
							{
								GlobalVariables.RefreshUsingAccountBookModule(currentHisExamServiceReqResultSDO.Transactions.OrderByDescending((V_HIS_TRANSACTION o) => o.NUM_ORDER).FirstOrDefault().NUM_ORDER);
							}
							else
							{
								LogSystem.Info("chkAutoCreateBill: " + chkAutoCreateBill.Checked);
								LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => currentHisExamServiceReqResultSDO.Transactions), currentHisExamServiceReqResultSDO.Transactions));
							}
							flag = true;
							break;
						case UCServiceRequestRegisterFactorySaveType.PROFILE:
							resultHisPatientProfileSDO = appDelegacyT.Execute<HisPatientProfileSDO>();
							LogSystem.Debug("Save.2");
							if (resultHisPatientProfileSDO == null)
							{
								break;
							}
							if (transPatiADO != null && transPatiADO.TRANSFER_IN_TIME_FROM.HasValue)
							{
								LogSystem.Info("Saved transfer info: " + LogUtil.TraceData(LogUtil.GetMemberName(() => transPatiADO), transPatiADO));
							}
							PatientProfileSuccess(param);
							flag = true;
							break;
						}
						if (!flag && !isShowMess)
						{
							MessageManager.Show(base.ParentForm, param, false);
						}
						if (flag)
						{
							EnableControl(false);
							IsCheckSave = true;
						}
						WaitingManager.Hide();
						return;
					}
					catch (Exception ex)
					{
						WaitingManager.Hide();
						LogSystem.Warn(ex);
						return;
					}
				}
				if (ValidatedTTCT)
				{
					return;
				}
				bool flag2 = false;
				bool isValidateAll = false;
				if (HisConfigCFG.KeyValueObligatoryTranferMediOrg == 1 && IsPresent)
				{
					flag2 = true;
				}
				else if (HisConfigCFG.KeyValueObligatoryTranferMediOrg == 2 && (IsPresent || IsPresentAndAppointment))
				{
					flag2 = true;
				}
				else if (HisConfigCFG.KeyValueObligatoryTranferMediOrg == 3 && IsPresent)
				{
					flag2 = true;
					isValidateAll = true;
				}
				if (!flag2)
				{
					return;
				}
				try
				{
					frm = new frmTransPati(true, transPatiADO, new UpdateSelectedTranPati(UpdateSelectedTranPati), true, isValidateAll);
					frm.SetValidForTTCT(new DelegateVisible(CheckValidateFormTTCT));
					frm.ShowDialog();
				}
				catch (Exception ex2)
				{
					LogSystem.Warn(ex2);
				}
			}
			catch (Exception ex3)
			{
				EnableControl(true);
				LogSystem.Error(ex3);
			}
		}

		private bool CheckDuplicateCCCD()
		{
			bool result = true;
			try
			{
				if ((HisConfigCFG.CHECK_DUPLICATION == "1" || HisConfigCFG.CHECK_DUPLICATION == "2") && (!string.IsNullOrEmpty(ucPlusInfo1.GetValue().CMND_NUMBER) || !string.IsNullOrEmpty(ucPlusInfo1.GetValue().CCCD_NUMBER) || !string.IsNullOrEmpty(ucPlusInfo1.GetValue().PASSPORT_NUMBER)))
				{
					string text = ucPlusInfo1.GetValue().CMND_NUMBER ?? ucPlusInfo1.GetValue().CCCD_NUMBER ?? ucPlusInfo1.GetValue().PASSPORT_NUMBER;
					HisPatientAdvanceFilter hisPatientAdvanceFilter = new HisPatientAdvanceFilter();
					if (text.Length == 9)
					{
						hisPatientAdvanceFilter.CMND_NUMBER__EXACT = text;
					}
					else
					{
						hisPatientAdvanceFilter.CCCD_NUMBER__EXACT = text;
					}
					CommonParam commonParam = new CommonParam();
					List<HisPatientSDO> list = new BackendAdapter(commonParam).Get<List<HisPatientSDO>>("api/HisPatient/GetSdoAdvance", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisPatientAdvanceFilter, new Action(SessionManager.ActionLostToken), commonParam);
					if (list.Any((HisPatientSDO p) => p.PATIENT_CODE == ucPatientRaw1.txtPatientCode.Text || p.CCCD_NUMBER == ucPatientRaw1.txtPatientCode.Text))
					{
						return true;
					}
					if (list != null && list.Count > 0 && ((ucPatientRaw1.patientTD3 != null && !string.IsNullOrEmpty(ucPatientRaw1.patientTD3.PATIENT_CODE) && !list.Exists((HisPatientSDO o) => o.PATIENT_CODE == ucPatientRaw1.patientTD3.PATIENT_CODE)) || ucPatientRaw1.patientTD3 == null || string.IsNullOrEmpty(ucPatientRaw1.patientTD3.PATIENT_CODE)) && ((HisConfigCFG.CHECK_DUPLICATION == "1" && XtraMessageBox.Show(string.Format("Số CCCD {0} đã được sử dụng bởi bệnh nhân có mã {1}", text, list.OrderByDescending((HisPatientSDO o) => o.PATIENT_CODE).ToList()[0].PATIENT_CODE), "Thông báo", MessageBoxButtons.OK) == DialogResult.OK) || (HisConfigCFG.CHECK_DUPLICATION == "2" && XtraMessageBox.Show(string.Format("Số CCCD {0} đã được sử dụng bởi bệnh nhân có mã {1}", text, list.OrderByDescending((HisPatientSDO o) => o.PATIENT_CODE).ToList()[0].PATIENT_CODE), "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.No)))
					{
						ucPatientRaw1.SearchPatientByCodeOrQrCode(text, HIS.UC.UCPatientRaw.ResourceMessage.typeCodeFind__MaCMCC);
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool BlockingHeinLevelCode()
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				if (value.PATIENTTYPE_ID != HisConfigCFG.PatientTypeId__BHYT && value.PATIENTTYPE_ID != HisConfigCFG.PatientTypeId__QN)
				{
					return true;
				}
				HisPatientProfileSDO heindata = ucHeinInfo1.GetValue();
				if (value.PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT && heindata != null && !string.IsNullOrEmpty(heindata.HisPatientTypeAlter.HEIN_MEDI_ORG_CODE))
				{
					HIS_MEDI_ORG hIS_MEDI_ORG = BackendDataWorker.Get<HIS_MEDI_ORG>().FirstOrDefault((HIS_MEDI_ORG o) => o.MEDI_ORG_CODE == heindata.HisPatientTypeAlter.HEIN_MEDI_ORG_CODE);
					if (!string.IsNullOrEmpty(BranchDataWorker.Branch.DO_NOT_ALLOW_HEIN_LEVEL_CODE) && hIS_MEDI_ORG != null && (";" + BranchDataWorker.Branch.DO_NOT_ALLOW_HEIN_LEVEL_CODE + ";").Contains(";" + hIS_MEDI_ORG.LEVEL_CODE + ";"))
					{
						XtraMessageBox.Show(string.Format("Nơi đăng ký khám chữa bệnh ban đầu thuộc tuyến {0}, không được hưởng BHYT", (hIS_MEDI_ORG.LEVEL_CODE == "1") ? "trung ương" : ((hIS_MEDI_ORG.LEVEL_CODE == "2") ? "Tỉnh" : ((hIS_MEDI_ORG.LEVEL_CODE == "3") ? "Huyện" : "Xã"))), ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK);
						return false;
					}
				}
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private bool CheckValidateForSave(CommonParam param)
		{
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = true;
			bool flag6 = true;
			bool flag7 = true;
			bool flag8 = true;
			bool flag9 = true;
			bool flag10 = true;
			bool flag11 = true;
			bool flag12 = true;
			bool flag13 = true;
			bool flag14 = true;
			bool flag15 = true;
			try
			{
				long patientTypeId = GetPatientTypeId();
				bool flag16 = patientTypeId == HisConfigCFG.PatientTypeId__BHYT || patientTypeId == HisConfigCFG.PatientTypeId__QN;
				flag4 = ucPatientRaw1.ValidateRequiredField();
				flag5 = ucRelativeInfo1.ValidateRequiredField();
				flag7 = ucOtherServiceReqInfo1.ValidateRequiredField();
				flag3 = ucPlusInfo1.ValidateRequiredField();
				flag8 = ucServiceRoomInfo1.ValidateRequiredField();
				flag6 = ucAddressCombo1.ValidateRequiredField();
				flag2 = !flag16 || ucHeinInfo1.ValidateRequiredField();
				flag14 = !flag16 || ucHeinInfo1.ValidateHeinPatientTypeCode(patientTypeId);
				if (flag16)
				{
					flag13 = BlockingHeinLevelCode() && BlockingInvalidBhyt();
				}
				if (patientTypeId == HisConfigCFG.PatientTypeId__KSK && ucKskContract != null && kskContractProcessor != null)
				{
					flag10 = kskContractProcessor.GetValidate(ucKskContract);
				}
				try
				{
					if (flag16)
					{
						IsPresent = ucHeinInfo1.HeinRightRouteTypeIsPresent();
						IsPresentAndAppointment = ucHeinInfo1.HeinRightRouteTypeIsPresentAndAppointment();
					}
					else
					{
						IsPresent = false;
						IsPresentAndAppointment = false;
					}
					flag9 = (((HisConfigCFG.KeyValueObligatoryTranferMediOrg == 1 || HisConfigCFG.KeyValueObligatoryTranferMediOrg == 3) && IsPresent) ? ValidatedTTCT : (HisConfigCFG.KeyValueObligatoryTranferMediOrg != 2 || (!IsPresent && !IsPresentAndAppointment) || ValidatedTTCT));
				}
				catch (Exception ex)
				{
					flag9 = true;
					LogSystem.Warn(ex);
				}
				bool flag17 = true;
				if (chkAutoCreateBill.Checked && (GlobalVariables.AuthorityAccountBook == null || !GlobalVariables.AuthorityAccountBook.AccountBookId.HasValue))
				{
					MessageBox.Show(ResourceMessage.BanChuaDuocCapSoHoaDon, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK);
					flag17 = false;
				}
				bool flag18 = true;
				if (chkAutoDeposit.Checked)
				{
					if (cboCashierRoom.EditValue == null && (GlobalVariables.SessionInfo == null || (GlobalVariables.SessionInfo != null && !GlobalVariables.SessionInfo.CashierWorkingRoomId.HasValue)))
					{
						MessageBox.Show(ResourceMessage.BanChuaChonPhongThuNgan, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK);
						flag18 = false;
					}
					else if (GlobalVariables.SessionInfo == null || GlobalVariables.SessionInfo.DepositAccountBook == null)
					{
						MessageBox.Show(ResourceMessage.BanChuaChonSoTamUng, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK);
						flag18 = false;
					}
					else if (GlobalVariables.SessionInfo == null || GlobalVariables.SessionInfo.PayForm == null)
					{
						MessageBox.Show(ResourceMessage.BanChuaChonHinhThucGiaoDich, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK);
						flag18 = false;
					}
				}
				string phone = ucAddressCombo1.GetValue().Phone;
				if (!string.IsNullOrEmpty(phone))
				{
					if (phone.Length < 10 || phone.Length > 10)
					{
						MessageBox.Show("Số điện thoại mới nhập " + phone.Length + " ký tự", ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK);
						ucAddressCombo1.FocusPhoneNumber();
						flag11 = false;
					}
				}
				else if (HisConfigCFG.PhoneRequired == "1")
				{
					MessageBox.Show("Bạn chưa nhập Điện thoại", ResourceMessage.ThongBao, MessageBoxButtons.OK);
					ucAddressCombo1.FocusPhoneNumber();
					flag11 = false;
				}
				else if (HisConfigCFG.PhoneRequired == "2" && MessageBox.Show("Bạn chưa nhập Điện thoại. Bạn có muốn tiếp tục?", ResourceMessage.ThongBao, MessageBoxButtons.YesNo) == DialogResult.No)
				{
					ucAddressCombo1.FocusPhoneNumber();
					flag11 = false;
				}
				HIS_PATIENT_TYPE hIS_PATIENT_TYPE = BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault((HIS_PATIENT_TYPE o) => o.ID == patientTypeId);
				if (hIS_PATIENT_TYPE != null && hIS_PATIENT_TYPE.MUST_BE_GUARANTEED == 1)
				{
					UCServiceReqInfoADO value = ucOtherServiceReqInfo1.GetValue();
					if (string.IsNullOrWhiteSpace(value.GUARANTEE_LOGINNAME) || string.IsNullOrEmpty(value.GUARANTEE_USERNAME))
					{
						MessageBox.Show(string.Format("Đối tượng {0} bắt buộc nhập thông tin Bảo lãnh", hIS_PATIENT_TYPE.PATIENT_TYPE_NAME), ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OK);
						ucOtherServiceReqInfo1.FocusGuarantee();
						flag12 = false;
					}
				}
				GlobalStore.currentFactorySaveType = GetEnumSaveType(param);
				flag = GlobalStore.currentFactorySaveType != UCServiceRequestRegisterFactorySaveType.VALID;
				flag = flag && flag14 && flag2 && flag4 && flag5 && flag7 && flag3 && flag8 && flag6 && flag9 && flag10 && flag17 && flag18 && flag11 && flag12 && flag13 && CheckRRCodeTTFee(true) && CheckTransferInTimeWarning();
				UCPatientRawADO value2 = ucPatientRaw1.GetValue();
				if (value2.isTypeHenKham)
				{
					flag = flag && AlertExpriedTimeHeinCardBhyt();
				}
				try
				{
					GlobalStore.DepartmentId = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault((V_HIS_ROOM o) => o.ID == currentModule.RoomId).DEPARTMENT_ID;
				}
				catch
				{
				}
				if (GlobalStore.DepartmentId == 0)
				{
					throw new ArgumentNullException("departmentId == 0");
				}
				if (HisConfigCFG.MustHaveNCSInfoForChild && ucPatientRaw1.GetIsChild() && ucRelativeInfo1.ValidateNeedOne())
				{
					flag = false;
				}
			}
			catch (Exception ex2)
			{
				flag = false;
				LogSystem.Error(ex2);
			}
			return flag;
		}

		private bool CheckTransferInTimeWarning()
		{
			bool result = true;
			try
			{
				if (HisConfigCFG.WarnOverMonthsTransfer <= 0)
				{
					return true;
				}
				HisPatientProfileSDO value = ucHeinInfo1.GetValue();
				UCPlusInfoADO value2 = ucPlusInfo1.GetValue();
				if (value2 == null || value2.PROGRAM_ID <= 0)
				{
					return true;
				}
				if (value == null || value.HisPatientTypeAlter == null || value.HisPatientTypeAlter.RIGHT_ROUTE_TYPE_CODE != "HK")
				{
					return true;
				}
				string hEIN_MEDI_ORG_CODE = BranchDataWorker.Branch.HEIN_MEDI_ORG_CODE;
				if (string.IsNullOrEmpty(value.HisPatientTypeAlter.HEIN_MEDI_ORG_CODE) || value.HisPatientTypeAlter.HEIN_MEDI_ORG_CODE == hEIN_MEDI_ORG_CODE)
				{
					return true;
				}
				if (transPatiADO == null || !transPatiADO.TRANSFER_IN_TIME_FROM.HasValue)
				{
					return true;
				}
				DateTime dateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(transPatiADO.TRANSFER_IN_TIME_FROM.Value) ?? DateTime.Now;
				DateTime now = DateTime.Now;
				int num = CalculateMonthsDifference(dateTime, now);
				LogSystem.Debug(string.Format("CheckTransferInTimeWarning - transferDate: {0:dd/MM/yyyy}, currentDate: {1:dd/MM/yyyy}, monthsDiff: {2}, warningMonths: {3}", dateTime, now, num, HisConfigCFG.WarnOverMonthsTransfer));
				if (num >= HisConfigCFG.WarnOverMonthsTransfer)
				{
					string text = string.Format("Thời gian chuyển tuyến của bệnh nhân đã được {0} tháng. Lưu ý bệnh nhân cần xin giấy chuyển tuyến mới. Bạn có muốn tiếp tục?", num);
					if (MessageBox.Show(text, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
					{
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		private int CalculateMonthsDifference(DateTime fromDate, DateTime toDate)
		{
			if (toDate < fromDate)
			{
				return 0;
			}
			return (toDate.Year - fromDate.Year) * 12 + (toDate.Month - fromDate.Month);
		}

		private bool BlockingInvalidBhyt()
		{
			try
			{
				bool flag = true;
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				HisPatientProfileSDO value2 = ucHeinInfo1.GetValue();
				if (ucPatientRaw1.ResultDataADO != null && ucPatientRaw1.ResultDataADO.ResultHistoryLDO != null && HisConfigCFG.WarningInvalidCheckHistoryHeinCard && ucPatientRaw1.ResultDataADO.ResultHistoryLDO.message == "Thẻ BHYT có thông tin kiểm tra thẻ chưa ra viện.")
				{
					DialogResult dialogResult = XtraMessageBox.Show(ucPatientRaw1.ResultDataADO.ResultHistoryLDO.message + " Bạn có muốn tiếp tục?", ResourceMessage.ThongBao, MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.No)
					{
						return false;
					}
				}
				if (value.PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT && (HisConfigCFG.IsBlockingInvalidBhyt == 1.ToString() || HisConfigCFG.IsBlockingInvalidBhyt == 2.ToString()) && value2 != null && !CheckBhytWhiteListAcceptNoCheckBHYT(value2.HisPatientTypeAlter.HEIN_CARD_NUMBER) && value2.HisPatientTypeAlter.HAS_BIRTH_CERTIFICATE != "C")
				{
					if (ucPatientRaw1.ResultDataADO == null)
					{
						LogSystem.Info("ucPatientRaw1.ResultDataADO is null");
						flag = false;
					}
					else if (ucPatientRaw1.ResultDataADO.ResultHistoryLDO == null)
					{
						LogSystem.Info("ucPatientRaw1.ResultDataADO.ResultHistoryLDO  is null");
						flag = false;
					}
					else if (HisConfigCFG.MaKetQuaBlockings.Contains(ucPatientRaw1.ResultDataADO.ResultHistoryLDO.maKetQua))
					{
						flag = false;
					}
					if (!flag)
					{
						XtraMessageBox.Show("Thẻ BHYT không hợp lệ. Không cho phép đăng ký với đối tượng BHYT");
						return flag;
					}
				}
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private bool CheckBhytWhiteListAcceptNoCheckBHYT(string heinCardNumder)
		{
			bool result = false;
			try
			{
				LogSystem.Debug("CheckBhytWhiteListAcceptNoCheckBHYT__" + LogUtil.TraceData(LogUtil.GetMemberName(() => heinCardNumder), heinCardNumder));
				if (!string.IsNullOrEmpty(heinCardNumder))
				{
					HIS_BHYT_WHITELIST hIS_BHYT_WHITELIST = BackendDataWorker.Get<HIS_BHYT_WHITELIST>().FirstOrDefault((HIS_BHYT_WHITELIST o) => o.BHYT_WHITELIST_CODE != null && heinCardNumder.ToUpper().Contains(o.BHYT_WHITELIST_CODE.ToUpper()) && o.IS_NOT_CHECK_BHYT == 1);
					result = hIS_BHYT_WHITELIST != null && hIS_BHYT_WHITELIST.IS_NOT_CHECK_BHYT == 1;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private UCServiceRequestRegisterFactorySaveType GetEnumSaveType(CommonParam param)
		{
			UCServiceRequestRegisterFactorySaveType result = UCServiceRequestRegisterFactorySaveType.REGISTER;
			try
			{
				serviceReqDetailSDOs = ucServiceRoomInfo1.GetDetail();
				isSaveWithRoomHasConfigAllowNotChooseService = false;
				bool flag = serviceReqDetailSDOs != null && serviceReqDetailSDOs.Count > 0 && serviceReqDetailSDOs.Exists((ServiceReqDetailSDO o) => o.RoomId.GetValueOrDefault() > 0);
				List<ServiceReqDetailSDO> validHasShowMess = (flag ? serviceReqDetailSDOs.Where((ServiceReqDetailSDO o) => o.RoomId.GetValueOrDefault() > 0 && o.ServiceId == 0).ToList() : null);
				if (validHasShowMess != null && validHasShowMess.Count > 0)
				{
					List<HIS_EXECUTE_ROOM> list = (from p in BackendDataWorker.Get<HIS_EXECUTE_ROOM>()
						where p.ALLOW_NOT_CHOOSE_SERVICE == 1 && validHasShowMess.Exists((ServiceReqDetailSDO k) => k.RoomId == p.ROOM_ID)
						select p).ToList();
					List<HIS_EXECUTE_ROOM> list2 = (from p in BackendDataWorker.Get<HIS_EXECUTE_ROOM>()
						where (!p.ALLOW_NOT_CHOOSE_SERVICE.HasValue || p.ALLOW_NOT_CHOOSE_SERVICE != 1) && validHasShowMess.Exists((ServiceReqDetailSDO k) => k.RoomId == p.ROOM_ID)
						select p).ToList();
					if (list != null && list.Count > 0 && (list2 == null || list2.Count == 0))
					{
						LogSystem.Info("TH nếu người dùng chọn phòng khám và không chọn dịch vụ khám và phòng khám đó có cấu hình \"cho phép không chọn dịch vụ\" (ALLOW_NOT_CHOOSE_SERVICE = 1) thì khi thực hiện lưu sẽ xử lý:+ KHÔNG hiển thị thông báo confirm+ Gọi đến API đăng ký khám (thay vì gọi đến api tạo hồ sơ điều trị)");
						isSaveWithRoomHasConfigAllowNotChooseService = true;
						return UCServiceRequestRegisterFactorySaveType.REGISTER;
					}
					if ((list == null || list.Count == 0) && list2 != null && list2.Count > 0)
					{
						LogSystem.Info("Th Nếu người dùng chọn phòng khám và không chọn dịch vụ khám và phòng khám không có cấu hình \"cho phép không chọn dịch vụ\" (ALLOW_NOT_CHOOSE_SERVICE khác 1) thì xử lý như cũ. Cụ thể:+ Hiển thị thông báo confirm + Nếu người dùng đồng ý thì gọi api để tạo hồ sơ điều trị.");
					}
					else
					{
						LogSystem.Info("Th Nếu người dùng chọn phòng khám và không chọn dịch vụ khám và phòng khám chọn nhiều phòng và có cả phòng khám có và không có cấu hình \"cho phép không chọn dịch vụ\" (ALLOW_NOT_CHOOSE_SERVICE khác 1) thì tạm thời xử lý như cũ.");
					}
					if (MessageBox.Show(ResourceMessage.BanChuaChonDichVuKham, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Cancel)
					{
						return UCServiceRequestRegisterFactorySaveType.PROFILE;
					}
					result = UCServiceRequestRegisterFactorySaveType.VALID;
				}
				else
				{
					result = ((!flag) ? UCServiceRequestRegisterFactorySaveType.PROFILE : UCServiceRequestRegisterFactorySaveType.REGISTER);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private void EnableButton(int actionType, bool isPrint)
		{
			try
			{
				if (this.actionType == 1)
				{
					btnSave.Enabled = true;
					btnSaveAndPrint.Enabled = true;
					btnSaveAndAssain.Enabled = false;
					btnPrint.Enabled = false;
					btnTreatmentBedRoom.Enabled = false;
					SimpleButton simpleButton = btnDepositDetail;
					bool enabled = (btnDepositRequest.Enabled = false);
					simpleButton.Enabled = enabled;
					dropDownButton__Other.Enabled = false;
					btnGiayTo.Enabled = false;
				}
				else
				{
					btnSave.Enabled = false;
					btnGiayTo.Enabled = true;
					btnSaveAndPrint.Enabled = false;
					btnPrint.Enabled = true;
					btnSaveAndAssain.Enabled = true;
					btnTreatmentBedRoom.Enabled = true;
					SimpleButton simpleButton2 = btnDepositDetail;
					bool enabled = (btnDepositRequest.Enabled = true);
					simpleButton2.Enabled = enabled;
					dropDownButton__Other.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private bool AlertExpriedTimeHeinCardBhyt()
		{
			bool result = true;
			long resultDayAlert = -1L;
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				if (value.PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT)
				{
					resultDayAlert = ucHeinInfo1.GetExpriedTimeHeinCardBhyt(AppConfigs.AlertExpriedTimeHeinCardBhyt, ref resultDayAlert);
					result = (resultDayAlert >= 0 && MessageBox.Show(string.Format(ResourceMessage.CanhBaoTheBhytSapHatHan, resultDayAlert), ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) || resultDayAlert < 0;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private void ExamRegisterSuccess(CommonParam param)
		{
			bool value = false;
			try
			{
				if (currentHisExamServiceReqResultSDO != null)
				{
					if (currentHisExamServiceReqResultSDO.ServiceReqs != null && currentHisExamServiceReqResultSDO.ServiceReqs.Count > 0 && currentHisExamServiceReqResultSDO.SereServs != null)
					{
						UCPatientRawADO value2 = ucPatientRaw1.GetValue();
						value2.PATIENT_CODE = currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatient.PATIENT_CODE;
						ucPatientRaw1.SetPatientCodeAfterSavePatient(value2.PATIENT_CODE);
						if (currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatientTypeAlter.HAS_BIRTH_CERTIFICATE == "C" || currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatientTypeAlter.IS_TEMP_QN == 1)
						{
							ucHeinInfo1.ChangeDataHeinInsuranceInfoByPatientTypeAlter(currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatientTypeAlter);
						}
						value = true;
						actionType = 3;
						EnableButton(actionType, true);
						if (!HisConfigCFG.IsManualInCode && !string.IsNullOrEmpty(currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.IN_CODE))
						{
							ucOtherServiceReqInfo1.SetValueIncode(currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.IN_CODE);
						}
						ucServiceRoomInfo1.InitComboRoom(new ExecuteRoomGet1().GetLCounter2(), new ExecuteRoomGet1().GetLCounter());
						ucPatientRaw1.FocusToPatientName();
						ucHeinInfo1.SetTreatmentId(currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.ID);
						string text = (lastSavedTreatmentCode = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.TREATMENT_CODE);
						ucPatientRaw1.SetLastSavedTreatmentCode(text);
					}
					else
					{
						LogSystem.Error("Api thuc hien tao yeu cau dang ky thanh cong nhung ket qua tra ve khong hop le:" + LogUtil.TraceData(LogUtil.GetMemberName(() => currentHisExamServiceReqResultSDO), currentHisExamServiceReqResultSDO));
						param.Messages.Add(ResourceMessage.HeThongTBKetQuaTraVeCuaServerKhongHopLe);
					}
				}
				else
				{
					LogSystem.Error("Api thuc hien dang ky yeu cau xu ly that bai:" + LogUtil.TraceData(LogUtil.GetMemberName(() => currentHisExamServiceReqResultSDO), currentHisExamServiceReqResultSDO));
				}
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
			MessageManager.Show(base.ParentForm, param, value);
		}

		private void PatientProfileSuccess(CommonParam param)
		{
			bool value = false;
			try
			{
				if (resultHisPatientProfileSDO != null)
				{
					if (resultHisPatientProfileSDO.HisPatient != null && resultHisPatientProfileSDO.HisPatientTypeAlter != null && resultHisPatientProfileSDO.HisTreatment != null)
					{
						ucHeinInfo1.SetTreatmentId(resultHisPatientProfileSDO.HisTreatment.ID);
						ucPatientRaw1.SetPatientCodeAfterSavePatient(resultHisPatientProfileSDO.HisPatient.PATIENT_CODE);
						string text = (lastSavedTreatmentCode = resultHisPatientProfileSDO.HisTreatment.TREATMENT_CODE);
						ucPatientRaw1.SetLastSavedTreatmentCode(text);
						if (resultHisPatientProfileSDO.HisPatientTypeAlter.HAS_BIRTH_CERTIFICATE == "C" || resultHisPatientProfileSDO.HisPatientTypeAlter.IS_TEMP_QN == 1)
						{
							ucHeinInfo1.ChangeDataHeinInsuranceInfoByPatientTypeAlter(resultHisPatientProfileSDO.HisPatientTypeAlter);
						}
						if (!HisConfigCFG.IsManualInCode && !string.IsNullOrEmpty(resultHisPatientProfileSDO.HisTreatment.IN_CODE))
						{
							ucOtherServiceReqInfo1.SetValueIncode(resultHisPatientProfileSDO.HisTreatment.IN_CODE);
						}
						value = true;
						actionType = 3;
						EnableButton(actionType, false);
					}
					else
					{
						LogSystem.Error("Api thuc hien tao thong tin ho so va chi dinh dich vu thanh cong nhung ket qua tra ve khong hop le:" + LogUtil.TraceData(LogUtil.GetMemberName(() => resultHisPatientProfileSDO), resultHisPatientProfileSDO));
						param.Messages.Add(ResourceMessage.HeThongTBKetQuaTraVeCuaServerKhongHopLe);
					}
				}
				else
				{
					LogSystem.Error("Api thuc hien tao thong tin ho so va chi dinh dich vu that bai:" + LogUtil.TraceData(LogUtil.GetMemberName(() => resultHisPatientProfileSDO), resultHisPatientProfileSDO));
				}
				LogSystem.Debug("Service request PatientProfile end process: time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Warn(ex);
			}
			MessageManager.Show(base.ParentForm, param, value);
		}

		private bool CheckAssignedExecuteLoginName()
		{
			bool result = true;
			try
			{
				List<ServiceReqDetailSDO> detail = ucServiceRoomInfo1.GetDetail();
				if (detail != null && detail.Count() > 0)
				{
					result = detail.All((ServiceReqDetailSDO o) => !string.IsNullOrWhiteSpace(o.AssignedExecuteLoginName));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private bool CheckTreatmentOrder()
		{
			bool result = true;
			try
			{
				UCServiceReqInfoADO value = ucOtherServiceReqInfo1.GetValue();
				if (value != null && value.TreatmentOrder.HasValue)
				{
					HisTreatmentOrderSDO hisTreatmentOrderSDO = new HisTreatmentOrderSDO();
					hisTreatmentOrderSDO.TreatmentOrder = value.TreatmentOrder.Value;
					hisTreatmentOrderSDO.InDate = value.IntructionTime - value.IntructionTime % 1000000;
					ApiResultObject<HisTreatmentOrderSDO> apiResultObject = new BackendAdapter(new CommonParam()).PostRO<HisTreatmentOrderSDO>("api/HisTreatment/CheckExistsTreatmentOrder", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisTreatmentOrderSDO, null);
					if (apiResultObject == null || !apiResultObject.Success)
					{
						string arg = "";
						if (apiResultObject.Param != null && apiResultObject.Param.Messages != null && apiResultObject.Param.Messages.Count > 0)
						{
							arg = string.Join(",", apiResultObject.Param.Messages);
						}
						if (XtraMessageBox.Show(string.Format("Số thứ tự hồ sơ đã tồn tại ({0}), Bạn có muốn tiếp tục?", arg), "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, DefaultBoolean.True) == DialogResult.Yes)
						{
							return true;
						}
						return false;
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = false;
			}
			return result;
		}

		private void SaveAndAssain()
		{
			try
			{
				Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.AssignService").FirstOrDefault();
				if (moduleData == null)
				{
					throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.AssignService'");
				}
				if (!moduleData.IsPlugin || moduleData.ExtensionInfo == null)
				{
					throw new NullReferenceException("Module 'HIS.Desktop.Plugins.AssignService' is not plugins");
				}
				List<object> list = new List<object>();
				AssignServiceADO assignServiceADO = new AssignServiceADO(GetTreatmentIdFromResultData(), 0L, 0L, null);
				if (_isPatientAppointmentCode && !string.IsNullOrEmpty(appointmentCode) && _TreatmnetIdByAppointmentCode > 0)
				{
					assignServiceADO.PreviusTreatmentId = _TreatmnetIdByAppointmentCode;
				}
				assignServiceADO.IsAutoEnableEmergency = true;
				GetPatientInfoFromResultData(ref assignServiceADO);
				list.Add(assignServiceADO);
				if (!IsApplyFormClosingOption(moduleData.ModuleLink))
				{
					LogSystem.Debug("ExamServiceReqExecute.btnAssignService_Click.4");
					object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(moduleData, currentModule.RoomId, currentModule.RoomTypeId), list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
				else
				{
					if (lstModuleLinkApply.FirstOrDefault((string o) => o == moduleData.ModuleLink) == null)
					{
						return;
					}
					if (GlobalVariables.FormAssignService != null)
					{
						GlobalVariables.FormAssignService.WindowState = FormWindowState.Maximized;
						GlobalVariables.FormAssignService.ShowInTaskbar = true;
						Type type = GlobalVariables.FormAssignService.GetType();
						MethodInfo method = type.GetMethod("ReloadModuleByInputData");
						method.Invoke(GlobalVariables.FormAssignService, new object[2] { currentModule, assignServiceADO });
						GlobalVariables.FormAssignService.Activate();
						return;
					}
					GlobalVariables.FormAssignService = (Form)PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(moduleData, currentModule.RoomId, currentModule.RoomTypeId), list);
					GlobalVariables.FormAssignService.ShowInTaskbar = true;
					if (GlobalVariables.FormAssignService == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					GlobalVariables.FormAssignService.Show();
					Type type2 = GlobalVariables.FormAssignService.GetType();
					MethodInfo method2 = type2.GetMethod("ChangeIsUseApplyFormClosingOption");
					method2.Invoke(GlobalVariables.FormAssignService, new object[1] { true });
				}
			}
			catch (NullReferenceException ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private void TreatmentBedRoom()
		{
			try
			{
				Inventec.Desktop.Common.Modules.Module module = GlobalVariables.currentModuleRaws.Where((Inventec.Desktop.Common.Modules.Module o) => o.ModuleLink == "HIS.Desktop.Plugins.HisBedRoomIn").FirstOrDefault();
				if (module == null)
				{
					LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.HisBedRoomIn");
				}
				if (module.IsPlugin && module.ExtensionInfo != null)
				{
					List<object> list = new List<object>();
					list.Add(GetTreatmentIdFromResultData());
					object pluginInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(module, currentModule.RoomId, currentModule.RoomTypeId), list);
					if (pluginInstance == null)
					{
						throw new ArgumentNullException("moduleData is null");
					}
					((Form)pluginInstance).ShowDialog();
				}
			}
			catch (NullReferenceException ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private void ShowFormThongTinChuyenTuyen(bool _isValidate)
		{
			try
			{
				long patientTypeId = GetPatientTypeId();
				bool flag = patientTypeId == HisConfigCFG.PatientTypeId__BHYT;
				IsPresent = flag && ucHeinInfo1.HeinRightRouteTypeIsPresent();
				bool _isValidateAll = HisConfigCFG.KeyValueObligatoryTranferMediOrg == 3 && IsPresent;
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => IsPresent), IsPresent) + LogUtil.TraceData(LogUtil.GetMemberName(() => _isValidateAll), _isValidateAll));
				if (HisConfigCFG.IsAutoShowTransferFormInCaseOfAppointment)
				{
					UCTransPatiADO uCTransPatiADO = null;
					if (transPatiADO == null && currentPatientSDO != null)
					{
						uCTransPatiADO = new UCTransPatiADO();
						uCTransPatiADO.HINHTHUCHUYEN_ID = currentPatientSDO.TransferInFormId;
						uCTransPatiADO.ICD_CODE = currentPatientSDO.TransferInIcdCode;
						uCTransPatiADO.ICD_NAME = currentPatientSDO.TransferInIcdName;
						uCTransPatiADO.LYDOCHUYEN_ID = currentPatientSDO.TransferInReasonId;
						uCTransPatiADO.NOICHUYENDEN_CODE = currentPatientSDO.TransferInMediOrgCode;
						uCTransPatiADO.NOICHUYENDEN_NAME = currentPatientSDO.TransferInMediOrgName;
						uCTransPatiADO.SOCHUYENVIEN = currentPatientSDO.TransferInCode;
						uCTransPatiADO.TRANSFER_IN_CMKT = currentPatientSDO.TransferInCmkt;
						uCTransPatiADO.TRANSFER_IN_TIME_FROM = currentPatientSDO.TransferInTimeFrom;
						uCTransPatiADO.TRANSFER_IN_TIME_TO = currentPatientSDO.TransferInTimeTo;
						uCTransPatiADO.ICD_SUB_CODE = currentPatientSDO.TransferInIcdSubCode;
						uCTransPatiADO.ICD_SUB_NAME = currentPatientSDO.TransferInIcdText;
					}
					else if (transPatiADO != null)
					{
						uCTransPatiADO = new UCTransPatiADO();
						DataObjectMapper.Map<UCTransPatiADO>(uCTransPatiADO, transPatiADO);
					}
					frm = new frmTransPati(_isValidate, _isValidateAll, uCTransPatiADO, new UpdateSelectedTranPati(UpdateSelectedTranPati));
				}
				else
				{
					frm = new frmTransPati(_isValidate, _isValidateAll, transPatiADO, new UpdateSelectedTranPati(UpdateSelectedTranPati));
				}
				frm.SetValidForTTCT(new DelegateVisible(CheckValidateFormTTCT));
				frm.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void UpdateSelectedTranPati(UCTransPatiADO transpatiADO)
		{
			try
			{
				transPatiADO = transpatiADO;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void UpdateTranPatiDataByPatientOld(HIS_PATIENT_TYPE_ALTER patientTypeAlter)
		{
			try
			{
				if (patientTypeAlter == null)
				{
					throw new ArgumentNullException("patientTypeAlter");
				}
				if (transPatiADO == null)
				{
					transPatiADO = new UCTransPatiADO();
				}
				CommonParam commonParam = new CommonParam();
				HisTreatmentFilter hisTreatmentFilter = new HisTreatmentFilter();
				hisTreatmentFilter.ID = patientTypeAlter.TREATMENT_ID;
				HIS_TREATMENT treatmentByPatient = new BackendAdapter(commonParam).Get<List<HIS_TREATMENT>>("api/HisTreatment/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisTreatmentFilter, new Action(SessionManager.ActionLostToken), commonParam).FirstOrDefault();
				transPatiADO.HINHTHUCHUYEN_ID = treatmentByPatient.TRAN_PATI_FORM_ID;
				transPatiADO.LYDOCHUYEN_ID = treatmentByPatient.TRAN_PATI_REASON_ID;
				transPatiADO.ICD_CODE = treatmentByPatient.TRANSFER_IN_ICD_CODE;
				if (!string.IsNullOrEmpty(treatmentByPatient.TRANSFER_IN_ICD_CODE))
				{
					HIS_ICD hIS_ICD = BackendDataWorker.Get<HIS_ICD>().FirstOrDefault((HIS_ICD o) => o.ICD_CODE == treatmentByPatient.TRANSFER_IN_ICD_CODE);
					transPatiADO.ICD_TEXT = ((treatmentByPatient.TRANSFER_IN_ICD_NAME == hIS_ICD.ICD_NAME) ? "" : treatmentByPatient.TRANSFER_IN_ICD_NAME);
				}
				transPatiADO.ICD_SUB_CODE = treatmentByPatient.TRANSFER_IN_ICD_SUB_CODE;
				transPatiADO.ICD_SUB_NAME = treatmentByPatient.TRANSFER_IN_ICD_TEXT;
				transPatiADO.ICD_NAME = treatmentByPatient.TRANSFER_IN_ICD_NAME;
				transPatiADO.NOICHUYENDEN_CODE = treatmentByPatient.TRANSFER_IN_MEDI_ORG_CODE;
				transPatiADO.NOICHUYENDEN_NAME = treatmentByPatient.TRANSFER_IN_MEDI_ORG_NAME;
				transPatiADO.SOCHUYENVIEN = treatmentByPatient.TRANSFER_IN_CODE;
				transPatiADO.TRANSFER_IN_CMKT = treatmentByPatient.TRANSFER_IN_CMKT;
				transPatiADO.HINHTHUCHUYEN_ID = treatmentByPatient.TRANSFER_IN_FORM_ID;
				transPatiADO.LYDOCHUYEN_ID = treatmentByPatient.TRANSFER_IN_REASON_ID;
				transPatiADO.TRANSFER_IN_TIME_FROM = treatmentByPatient.TRANSFER_IN_TIME_FROM;
				transPatiADO.TRANSFER_IN_TIME_TO = treatmentByPatient.TRANSFER_IN_TIME_TO;
				transPatiADO.TRANSFER_IN_REVIEWS = treatmentByPatient.TRANSFER_IN_REVIEWS;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void CheckValidateFormTTCT(bool isValidated)
		{
			try
			{
				ValidatedTTCT = isValidated;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataIntoUCImage(HisPatientSDO hisPatientSDO)
		{
			try
			{
				UCImageInfoADO uCImageInfoADO = new UCImageInfoADO();
				uCImageInfoADO.ListImageData = new List<ImageInfoADO>();
				if (!string.IsNullOrEmpty(hisPatientSDO.AVATAR_URL))
				{
					uCImageInfoADO.ListImageData.Add(new ImageInfoADO
					{
						Url = hisPatientSDO.AVATAR_URL,
						Type = ImageType.CHAN_DUNG
					});
				}
				if (!string.IsNullOrEmpty(hisPatientSDO.BHYT_URL))
				{
					uCImageInfoADO.ListImageData.Add(new ImageInfoADO
					{
						Url = hisPatientSDO.BHYT_URL,
						Type = ImageType.THE_BHYT
					});
				}
				if (!string.IsNullOrEmpty(hisPatientSDO.CMND_BEFORE_URL))
				{
					uCImageInfoADO.ListImageData.Add(new ImageInfoADO
					{
						Url = hisPatientSDO.CMND_BEFORE_URL,
						Type = ImageType.CMND_CCCD_TRUOC
					});
				}
				if (!string.IsNullOrEmpty(hisPatientSDO.CMND_AFTER_URL))
				{
					uCImageInfoADO.ListImageData.Add(new ImageInfoADO
					{
						Url = hisPatientSDO.CMND_AFTER_URL,
						Type = ImageType.CMND_CCCD_SAU
					});
				}
				ucImageInfo1.SetValue(uCImageInfoADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ReloadDataByCmndBefore(object obj)
		{
			try
			{
				if (obj == null)
				{
					return;
				}
				Image data = (Image)obj;
				WaitingManager.Show();
				string text = ConvertBase64Image(data);
				if (!string.IsNullOrEmpty(text))
				{
					CommonParam commonParam = new CommonParam();
					RecognitionSDO recognitionSDO = new RecognitionSDO();
					recognitionSDO.ImageBase64 = text;
					RecognitionResultSDO recognitionResultSDO = new BackendAdapter(commonParam).Post<RecognitionResultSDO>("api/HisPatient/Recognition", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, recognitionSDO, commonParam);
					if (recognitionResultSDO != null)
					{
						SetDataToControlBefore(recognitionResultSDO);
					}
				}
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
		}

		private void SetDataToControlBefore(RecognitionResultSDO data)
		{
			try
			{
				if (data == null)
				{
					return;
				}
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				HIS_GENDER hIS_GENDER = null;
				string inputValue = "";
				if (!string.IsNullOrEmpty(data.sex) && data.sex != "N/A")
				{
					hIS_GENDER = BackendDataWorker.Get<HIS_GENDER>().FirstOrDefault((HIS_GENDER o) => o.GENDER_NAME.ToUpper() == data.sex.ToUpper());
				}
				if (!string.IsNullOrEmpty(data.birthday) && data.birthday != "N/A")
				{
					string[] array = ((data.birthday != null) ? data.birthday.Split('-') : null);
					if (array != null && array.Length == 3)
					{
						string arg = string.Format("{0:00}", System.Convert.ToInt64(array[0]));
						string arg2 = string.Format("{0:00}", System.Convert.ToInt64(array[1]));
						string arg3 = string.Format("{0:0000}", System.Convert.ToInt64(array[2]));
						inputValue = string.Format("{0}{1}{2}000000", arg3, arg2, arg);
					}
				}
				if (value != null && !string.IsNullOrEmpty(value.PATIENT_NAME) && MessageBox.Show(ResourceMessage.BanCoMuonThayDoiThongTinBenhNhanTheoCmndCccdHayKhong, ResourceMessage.ThongBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
				value = new UCPatientRawADO();
				UCAddressADO uCAddressADO = ucAddressCombo1.GetValue();
				UCPlusInfoADO uCPlusInfoADO = ucPlusInfo1.GetValue();
				if (uCAddressADO == null)
				{
					uCAddressADO = new UCAddressADO();
				}
				if (uCPlusInfoADO == null)
				{
					uCPlusInfoADO = new UCPlusInfoADO();
				}
				uCAddressADO.Address = data.address;
				value.PATIENT_NAME = ((data.name != "N/A") ? data.name : "");
				value.DOB = Parse.ToInt64(inputValue);
				uCPlusInfoADO.CMND_NUMBER = ((data.id != "N/A") ? data.id : "");
				if (hIS_GENDER != null)
				{
					value.GENDER_ID = hIS_GENDER.ID;
				}
				if (!string.IsNullOrWhiteSpace(data.province) && data.province.ToUpper() != "N/A")
				{
					V_SDA_PROVINCE province = BackendDataWorker.Get<V_SDA_PROVINCE>().FirstOrDefault((V_SDA_PROVINCE o) => o.PROVINCE_CODE.ToUpper() == data.province.ToUpper());
					if (province != null)
					{
						uCAddressADO.Province_Code = province.PROVINCE_CODE;
						uCAddressADO.Province_Name = province.PROVINCE_CODE;
					}
					if (province != null && !string.IsNullOrWhiteSpace(data.district) && data.district.ToUpper() != "N/A")
					{
						V_SDA_DISTRICT disTrict = BackendDataWorker.Get<V_SDA_DISTRICT>().FirstOrDefault((V_SDA_DISTRICT o) => o.PROVINCE_CODE == province.PROVINCE_CODE && o.DISTRICT_CODE.ToUpper() == data.district.ToUpper());
						if (disTrict != null)
						{
							uCAddressADO.District_Code = disTrict.DISTRICT_CODE;
							uCAddressADO.District_Name = disTrict.DISTRICT_CODE;
						}
						if (disTrict != null && !string.IsNullOrWhiteSpace(data.precinct) && data.precinct.ToUpper() != "N/A")
						{
							V_SDA_COMMUNE v_SDA_COMMUNE = BackendDataWorker.Get<V_SDA_COMMUNE>().FirstOrDefault((V_SDA_COMMUNE o) => o.DISTRICT_CODE.ToUpper() == disTrict.DISTRICT_CODE.ToUpper() && o.COMMUNE_CODE.ToUpper() == data.precinct.ToUpper());
							if (v_SDA_COMMUNE != null)
							{
								uCAddressADO.Commune_Code = v_SDA_COMMUNE.COMMUNE_CODE;
								uCAddressADO.Commune_Name = v_SDA_COMMUNE.COMMUNE_CODE;
							}
						}
					}
				}
				LogSystem.Error("SetDataToControlBefore");
				ucPatientRaw1.SetValue(value);
				ucAddressCombo1.SetValue(uCAddressADO);
				ucPlusInfo1.SetValue(uCPlusInfoADO);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private string ConvertBase64Image(Image data)
		{
			string result = "";
			Bitmap bitmap = new Bitmap(data);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, ImageFormat.Jpeg);
				byte[] inArray = memoryStream.ToArray();
				string text = System.Convert.ToBase64String(inArray);
				result = ((text.Length % 4 == 1) ? (text + "===") : ((text.Length % 4 == 2) ? (text + "==") : ((text.Length % 4 != 3) ? text : (text + "="))));
			}
			return result;
		}

		private void ReloadDataByCmndAfter(object obj)
		{
			try
			{
				if (obj == null)
				{
					return;
				}
				Image data = (Image)obj;
				WaitingManager.Show();
				string text = ConvertBase64Image(data);
				if (!string.IsNullOrEmpty(text))
				{
					CommonParam commonParam = new CommonParam();
					RecognitionSDO recognitionSDO = new RecognitionSDO();
					recognitionSDO.ImageBase64 = text;
					RecognitionResultSDO recognitionResultSDO = new BackendAdapter(commonParam).Post<RecognitionResultSDO>("api/HisPatient/Recognition", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, recognitionSDO, commonParam);
					if (recognitionResultSDO != null)
					{
						SetDataToControlAfter(recognitionResultSDO);
					}
				}
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetDataToControlAfter(RecognitionResultSDO data)
		{
			try
			{
				if (data == null)
				{
					return;
				}
				UCPlusInfoADO uCPlusInfoADO = ucPlusInfo1.GetValue();
				string value = "";
				if (!string.IsNullOrEmpty(data.issue_date) && data.issue_date != "N/A")
				{
					string[] array = ((data.issue_date != null) ? data.issue_date.Split('-') : null);
					if (array != null && array.Length == 3)
					{
						string arg = string.Format("{0:00}", System.Convert.ToInt64(array[0]));
						string arg2 = string.Format("{0:00}", System.Convert.ToInt64(array[1]));
						string arg3 = string.Format("{0:0000}", System.Convert.ToInt64(array[2]));
						value = string.Format("{0}{1}{2}000000", arg3, arg2, arg);
					}
				}
				if (uCPlusInfoADO == null)
				{
					uCPlusInfoADO = new UCPlusInfoADO();
				}
				if (((!uCPlusInfoADO.CMND_DATE.HasValue || string.IsNullOrEmpty(value) || System.Convert.ToInt64(value) == uCPlusInfoADO.CMND_DATE.Value) && (string.IsNullOrEmpty(uCPlusInfoADO.CMND_PLACE) || !(data.issue_by.ToUpper() != uCPlusInfoADO.CMND_PLACE.Trim().ToUpper()))) || MessageBox.Show(ResourceMessage.BanCoMuonThayDoiThongTinNgayCapNoiCapTheoCmndCccdHayKhong, ResourceMessage.ThongBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
				{
					uCPlusInfoADO.CMND_DATE = System.Convert.ToInt64(value);
					uCPlusInfoADO.CMND_PLACE = ((data.issue_by != "N/A") ? data.issue_by : "");
					ucPlusInfo1.SetValue(uCPlusInfoADO);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ReloadDataByCmnd(object obj1, object obj2)
		{
			try
			{
				if (obj1 != null)
				{
					Image data = (Image)obj1;
					WaitingManager.Show();
					string text = ConvertBase64Image(data);
					if (!string.IsNullOrEmpty(text))
					{
						CommonParam commonParam = new CommonParam();
						RecognitionSDO recognitionSDO = new RecognitionSDO();
						recognitionSDO.ImageBase64 = text;
						RecognitionResultSDO recognitionResultSDO = new BackendAdapter(commonParam).Post<RecognitionResultSDO>("api/HisPatient/Recognition", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, recognitionSDO, commonParam);
						if (recognitionResultSDO != null)
						{
							SetDataToControlAfter(recognitionResultSDO);
						}
					}
					WaitingManager.Hide();
				}
				if (obj2 == null)
				{
					return;
				}
				Image data2 = (Image)obj2;
				WaitingManager.Show();
				string text2 = ConvertBase64Image(data2);
				if (!string.IsNullOrEmpty(text2))
				{
					CommonParam commonParam2 = new CommonParam();
					RecognitionSDO recognitionSDO2 = new RecognitionSDO();
					recognitionSDO2.ImageBase64 = text2;
					RecognitionResultSDO recognitionResultSDO2 = new BackendAdapter(commonParam2).Post<RecognitionResultSDO>("api/HisPatient/Recognition", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, recognitionSDO2, commonParam2);
					if (recognitionResultSDO2 != null)
					{
						SetDataToControlBefore(recognitionResultSDO2);
					}
				}
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
		}

		private void FillDataPatientRawInfo(HisPatientSDO data)
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				UCPatientRawADO uCPatientRawADO = new UCPatientRawADO();
				uCPatientRawADO.CARRER_ID = data.CAREER_ID;
				uCPatientRawADO.DOB = data.DOB;
				uCPatientRawADO.GENDER_ID = data.GENDER_ID;
				uCPatientRawADO.HEIN_CARD_NUMBER = data.HeinCardNumber;
				uCPatientRawADO.IS_HAS_NOT_DAY_DOB = data.IS_HAS_NOT_DAY_DOB;
				uCPatientRawADO.PATIENT_CODE = data.PATIENT_CODE;
				uCPatientRawADO.PATIENT_NAME = data.VIR_PATIENT_NAME;
				if (value != null)
				{
					uCPatientRawADO.PATIENTTYPE_ID = value.PATIENTTYPE_ID;
				}
				uCPatientRawADO.PATIENT_CLASSIFY_ID = data.PATIENT_CLASSIFY_ID;
				uCPatientRawADO.MILITARY_RANK_ID = data.MILITARY_RANK_ID;
				uCPatientRawADO.POSITION_ID = data.POSITION_ID;
				uCPatientRawADO.WORK_PLACE_ID = data.WORK_PLACE_ID;
				uCPatientRawADO.ETHNIC_CODE = data.ETHNIC_CODE;
				LogSystem.Error("FillDataPatientRawInfo");
				ucPatientRaw1.SetValue(uCPatientRawADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataPreviewForSearchByQrcodeInUCPatientRaw(HeinCardData dataCheck)
		{
			try
			{
				if (dataCheck != null)
				{
					string heinCardNumber = dataCheck.HeinCardNumber;
					DataResultADO dt = new DataResultADO
					{
						HeinCardData = dataCheck
					};
					FillDataAfterSaerchPatientInUCPatientRaw(dt);
					FillDataCareerUnder6AgeByHeinCardNumber(heinCardNumber);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataPreviewForSearchByQrcodeInUCPatientRawPatientSDO(HisPatientSDO dataCheck)
		{
			try
			{
				if (dataCheck != null)
				{
					HeinCardData heinCardData = ConvertFromPatientData(dataCheck);
					heinCardData.Address = heinCardData.Address ?? dataCheck.VIR_ADDRESS;
					string heinCardNumber = dataCheck.HeinCardNumber;
					DataResultADO dt = new DataResultADO
					{
						HisPatientSDO = dataCheck,
						HeinCardData = heinCardData
					};
					FillDataAfterSaerchPatientInUCPatientRaw(dt);
					FillDataCareerUnder6AgeByHeinCardNumber(heinCardNumber);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ShowControlHrmKskCode(bool _isShow)
		{
			try
			{
				if (ucPlusInfo1 != null)
				{
					ucPlusInfo1.ShowControlhrmKskCode(_isShow);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ShowControlHrmKskCodeNotValid(bool _isShow)
		{
			try
			{
				if (ucPlusInfo1 != null)
				{
					ucPlusInfo1.ShowControlhrmKskCodeNotValid(_isShow, false);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ShowControlGuaranteeLoginname(bool _isShow)
		{
			try
			{
				if (ucOtherServiceReqInfo1 != null)
				{
					ucOtherServiceReqInfo1.ShowValidation(_isShow);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataIntoUCOtherServiceReqInfo(HisPatientSDO data)
		{
			try
			{
				ucOtherServiceReqInfo1.SetValueByPatientInfo(data);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void AutoSetDataForOtherServiceReqInfo(bool value, long patientTypeId)
		{
			try
			{
				UCServiceReqInfoADO value2 = ucOtherServiceReqInfo1.GetValue();
				value2.IsNotRequireFee = value;
				ucOtherServiceReqInfo1.SetValue(value2);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void AutoSetTreatmentTypeCombo(long? value, DataResultADO dataResult = null)
		{
			try
			{
				if (value.HasValue)
				{
					UCServiceReqInfoADO value2 = ucOtherServiceReqInfo1.GetValue();
					value2.TreatmentType_ID = value.Value;
					if (dataResult != null && dataResult.HisPatientSDO != null)
					{
						value2.NOTE = dataResult.HisPatientSDO.NOTE;
					}
					ucOtherServiceReqInfo1.SetValue(value2);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SendPatientName(string _patientName)
		{
			try
			{
				if (ucOtherServiceReqInfo1 != null)
				{
					ucOtherServiceReqInfo1.SetPatientName(_patientName);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SendPatientSDO(HisPatientSDO sdo)
		{
			try
			{
				if (ucAddressCombo1 != null)
				{
					ucAddressCombo1.GetPatientSdo(sdo);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataIntoUCAddressInfo(DataResultADO data)
		{
			try
			{
				UCAddressADO uCAddressADO = new UCAddressADO();
				uCAddressADO.Commune_Code = data.HisPatientSDO.COMMUNE_CODE;
				uCAddressADO.Commune_Name = data.HisPatientSDO.COMMUNE_NAME;
				uCAddressADO.District_Code = data.HisPatientSDO.DISTRICT_CODE;
				uCAddressADO.District_Name = data.HisPatientSDO.DISTRICT_NAME;
				uCAddressADO.Province_Code = data.HisPatientSDO.PROVINCE_CODE;
				uCAddressADO.Province_Name = data.HisPatientSDO.PROVINCE_NAME;
				uCAddressADO.Address = data.HisPatientSDO.ADDRESS;
				uCAddressADO.Phone = data.HisPatientSDO.PHONE;
				uCAddressADO.IsNoDistrict = data.IsNoDistrict;
				ucAddressCombo1.SetValue(uCAddressADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private async void CheckTTFull(HeinCardData heinCard, Action focusNextControl)
		{
			try
			{
				if (!HisConfigCFG.IsCheckExamHistory)
				{
					return;
				}
				if (ucPatientRaw1 != null)
				{
					UCPatientRawADO patientRawADO = ucPatientRaw1.GetValue();
					if (patientRawADO.PATIENTTYPE_ID != HisConfigCFG.PatientTypeId__BHYT)
					{
						return;
					}
				}
				if (heinCard == null || isResetForm)
				{
					return;
				}
				HeinGOVManager heinGOVManager = new HeinGOVManager(ResourceMessage.GoiSangCongBHXHTraVeMaLoi);
				if (HisConfigCFG.IsBlockingInvalidBhyt == 1.ToString() || HisConfigCFG.IsBlockingInvalidBhyt == 2.ToString())
				{
					heinGOVManager.SetDelegateHeinEnableButtonSave(new DelegateHeinEnableButtonSave(HeinEnableSave));
				}
				if (string.IsNullOrEmpty(heinCard.Dob) || string.IsNullOrEmpty(heinCard.PatientName) || string.IsNullOrEmpty(heinCard.Gender))
				{
					if (ucPatientRaw1 != null)
					{
						UCPatientRawADO dataPatient = ucPatientRaw1.GetValue();
						if (dataPatient != null)
						{
							if (dataPatient.IS_HAS_NOT_DAY_DOB == 1)
							{
								heinCard.Dob = dataPatient.DOB.ToString().Substring(0, 4);
							}
							else
							{
								heinCard.Dob = Inventec.Common.DateTime.Convert.TimeNumberToDateString(dataPatient.DOB);
							}
							heinCard.PatientName = dataPatient.PATIENT_NAME;
							heinCard.Gender = GenderConvert.HisToHein(dataPatient.GENDER_ID.ToString());
						}
					}
					else
					{
						LogSystem.Debug("ucPatientRaw1 null, khong khoi tao dc ucPatientRaw1");
					}
				}
				if ((string.IsNullOrEmpty(heinCard.HeinCardNumber) || string.IsNullOrEmpty(heinCard.FromDate) || string.IsNullOrEmpty(heinCard.MediOrgCode)) && ucHeinInfo1 != null)
				{
					HisPatientProfileSDO dataHein = ucHeinInfo1.GetValue();
					if (dataHein != null && dataHein.HisPatientTypeAlter != null)
					{
						heinCard.HeinCardNumber = dataHein.HisPatientTypeAlter.HEIN_CARD_NUMBER;
						heinCard.Address = dataHein.HisPatientTypeAlter.ADDRESS;
						heinCard.FromDate = Inventec.Common.DateTime.Convert.TimeNumberToDateString(dataHein.HisPatientTypeAlter.HEIN_CARD_FROM_TIME.ToString());
						heinCard.MediOrgCode = dataHein.HisPatientTypeAlter.HEIN_MEDI_ORG_CODE;
						heinCard.ToDate = Inventec.Common.DateTime.Convert.TimeNumberToDateString(dataHein.HisPatientTypeAlter.HEIN_CARD_TO_TIME.ToString());
						heinCard.LiveAreaCode = dataHein.HisPatientTypeAlter.LIVE_AREA_CODE;
					}
				}
				UCPatientRaw uCPatientRaw = ucPatientRaw1;
				uCPatientRaw.ResultDataADO = await heinGOVManager.Check(heinCard, focusNextControl, false, (currentPatientSDO != null && currentPatientSDO.ID > 0) ? currentPatientSDO.HeinAddress : "", GetIntructionTime(), isReadQrCode);
				if (ucHeinInfo1 != null)
				{
					ucHeinInfo1.ResultDataADO = ucPatientRaw1.ResultDataADO;
				}
				if (ucPatientRaw1.ResultDataADO != null)
				{
					if (!string.IsNullOrEmpty(heinCard.HeinCardNumber) && ucPatientRaw1.ResultDataADO.IsShowQuestionWhileChangeHeinTime__Choose)
					{
						heinCard.HeinCardNumber = ucPatientRaw1.ResultDataADO.ResultHistoryLDO.maTheMoi;
					}
					CheckTTProcessResultData(heinCard, focusNextControl, false);
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				HeinEnableSave(true);
				LogSystem.Warn(ex2);
			}
		}

		private async Task CheckTTProcessResultData(HeinCardData dataHein, Action focusNextControl, bool ischeckChange)
		{
			try
			{
				if (ucPatientRaw1.ResultDataADO == null || ucPatientRaw1.ResultDataADO.ResultHistoryLDO == null)
				{
					return;
				}
				ucPatientRaw1.ResultDataADO.HeinCardData.FineYearMonthDate = ucPatientRaw1.ResultDataADO.ResultHistoryLDO.ngayDu5Nam;
				if (ucPatientRaw1.ResultDataADO.IsShowQuestionWhileChangeHeinTime__Choose)
				{
					UCPatientRawADO data = new UCPatientRawADO
					{
						PATIENT_NAME = ucPatientRaw1.ResultDataADO.HeinCardData.PatientName,
						GENDER_ID = Parse.ToInt64(ucPatientRaw1.ResultDataADO.HeinCardData.Gender),
						DOB_STR = ucPatientRaw1.ResultDataADO.HeinCardData.Dob
					};
					DateTime? dtPatientDob = null;
					if (data.DOB_STR.Length == 10)
					{
						data.IS_HAS_NOT_DAY_DOB = 0;
						dtPatientDob = DateTimeHelper.ConvertDateStringToSystemDate(data.DOB_STR);
					}
					else if (data.DOB_STR.Length == 4)
					{
						data.IS_HAS_NOT_DAY_DOB = (short)1;
						dtPatientDob = DateTimeHelper.ConvertDateStringToSystemDate("01/01/" + data.DOB_STR);
					}
					data.DOB = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtPatientDob).GetValueOrDefault();
					ucPatientRaw1.UpdateValueAfterCheckTT(data);
					if (IsPatientTypeUsingHeinInfo())
					{
						ucHeinInfo1.FillDataByHeinCardData(ucPatientRaw1.ResultDataADO.HeinCardData);
					}
				}
				if (ucPatientRaw1.ResultDataADO.IsToDate)
				{
					if (IsPatientTypeUsingHeinInfo())
					{
						ucHeinInfo1.FillDataByHeinCardData(ucPatientRaw1.ResultDataADO.HeinCardData);
					}
					LogSystem.Debug("Ket thuc gan du lieu cho benh nhan khi doc the va khong co han den");
				}
				if ((ucPatientRaw1.ResultDataADO.IsAddress || ucPatientRaw1.ResultDataADO.IsThongTinNguoiDungThayDoiSoVoiCong__Choose) && AppConfigs.CheDoTuDongFillDuLieuDiaChiGhiTrenTheVaoODiaChiBenhNhanHayKhong == 1)
				{
					dataAddressPatient = ucAddressCombo1.GetValue() ?? new UCAddressADO();
					AddressProcessor adProc = new AddressProcessor(BackendDataWorker.Get<V_SDA_PROVINCE>(), BackendDataWorker.Get<V_SDA_DISTRICT>(), BackendDataWorker.Get<V_SDA_COMMUNE>());
					AddressADO data2 = adProc.SplitFromFullAddress(ucPatientRaw1.ResultDataADO.ResultHistoryLDO.diaChi);
					if (data2 != null)
					{
						dataAddressPatient.Province_Code = data2.ProvinceCode;
						dataAddressPatient.Province_Name = data2.ProvinceName;
						dataAddressPatient.District_Code = data2.DistrictCode;
						dataAddressPatient.District_Name = data2.DistrictName;
						dataAddressPatient.Commune_Code = data2.CommuneCode;
						dataAddressPatient.Commune_Name = data2.CommuneName;
						dataAddressPatient.IsNoDistrict = data2.IsNoDistrict;
					}
					dataAddressPatient.Address = ucPatientRaw1.ResultDataADO.ResultHistoryLDO.diaChi;
					ucAddressCombo1.SetValue(dataAddressPatient);
				}
				if (ucPatientRaw1.ResultDataADO.IsThongTinNguoiDungThayDoiSoVoiCong__Choose)
				{
					UCPatientRawADO data3 = new UCPatientRawADO
					{
						PATIENT_NAME = ucPatientRaw1.ResultDataADO.HeinCardData.PatientName,
						GENDER_ID = Parse.ToInt64(ucPatientRaw1.ResultDataADO.HeinCardData.Gender),
						DOB_STR = ucPatientRaw1.ResultDataADO.HeinCardData.Dob
					};
					DateTime? dtPatientDob2 = null;
					if (data3.DOB_STR.Length == 10)
					{
						data3.IS_HAS_NOT_DAY_DOB = 0;
						dtPatientDob2 = DateTimeHelper.ConvertDateStringToSystemDate(data3.DOB_STR);
					}
					else if (data3.DOB_STR.Length == 4)
					{
						data3.IS_HAS_NOT_DAY_DOB = (short)1;
						dtPatientDob2 = DateTimeHelper.ConvertDateStringToSystemDate("01/01/" + data3.DOB_STR);
					}
					data3.DOB = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtPatientDob2).GetValueOrDefault();
					ucPatientRaw1.UpdateValueAfterCheckTT(data3);
					if (IsPatientTypeUsingHeinInfo())
					{
						ucHeinInfo1.FillDataByHeinCardData(ucPatientRaw1.ResultDataADO.HeinCardData);
					}
				}
				CheckRRCodeTTFee(false);
				if (HisConfigCFG.IsCheckExamHistory && ucPatientRaw1.ResultDataADO.ResultHistoryLDO != null)
				{
					if (ucPatientRaw1.ResultDataADO.IsShowQuestionWhileChangeHeinTime__Choose || ucPatientRaw1.ResultDataADO.SuccessWithoutMessage)
					{
						ucCheckTT1.FillDataIntoUCCheckTT(ucPatientRaw1.ResultDataADO.ResultHistoryLDO);
					}
					else
					{
						ucCheckTT1.ResetDataControl(ucPatientRaw1.ResultDataADO.ResultHistoryLDO);
					}
				}
				LogSystem.Debug("CheckTTProcessResultData 3");
				LogSystem.Debug("CheckHanSDTheBHYT => 3");
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
		}

		private bool CheckRRCodeTTFee(bool IsSave)
		{
			bool result = true;
			try
			{
				HisPatientProfileSDO value = ucHeinInfo1.GetValue();
				if (value.HisPatientTypeAlter.RIGHT_ROUTE_CODE == "TT" && BranchDataWorker.Branch.IS_WARNING_WRONG_ROUTE_FEE == 1)
				{
					result = (IsSave ? (XtraMessageBox.Show("Bệnh nhân trái tuyến cần thu tiền khám. Bạn có muốn tiếp tục không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes) : (XtraMessageBox.Show("Bệnh nhân trái tuyến cần thu tiền khám.", "Thông báo", MessageBoxButtons.OK) == DialogResult.OK));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool CheckChangeInfo(HeinCardData dataHein, ResultHistoryLDO rsIns, bool isHasNewCard)
		{
			bool flag = false;
			try
			{
				string value = GenderConvert.TextToNumber(rsIns.gioiTinh);
				bool flag2 = false;
				DateTime dateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(ucOtherServiceReqInfo1.GetValue().IntructionTime) ?? DateTime.MinValue;
				if (isHasNewCard && !string.IsNullOrEmpty(rsIns.gtTheTuMoi))
				{
					DateTime value2 = DateTimeHelper.ConvertDateStringToSystemDate(rsIns.gtTheTuMoi).Value;
					DateTime dateTime2 = DateTimeHelper.ConvertDateStringToSystemDate(rsIns.gtTheDenMoi) ?? DateTime.MinValue;
					if (value2.Date <= dateTime.Date && (dateTime2 == DateTime.MinValue || dateTime.Date <= dateTime2.Date))
					{
						flag2 = true;
					}
				}
				if (!string.IsNullOrEmpty(dataHein.Address))
				{
					flag = ((currentPatientSDO == null || string.IsNullOrEmpty(currentPatientSDO.HeinAddress)) ? (flag || dataHein.Address != rsIns.diaChi) : (flag || dataHein.Address != currentPatientSDO.HeinAddress));
				}
				flag = flag || (flag2 ? (HeinCardHelper.TrimHeinCardNumber(dataHein.HeinCardNumber) != rsIns.maTheMoi) : (HeinCardHelper.TrimHeinCardNumber(dataHein.HeinCardNumber) != rsIns.maThe));
				flag = ((rsIns.ngaySinh.Length != 4) ? (flag || dataHein.Dob != rsIns.ngaySinh) : (flag || dataHein.Dob.Substring(6, 4) != rsIns.ngaySinh));
				flag = flag || !dataHein.Gender.Equals(value);
				flag = flag || (flag2 ? (dataHein.FromDate != rsIns.gtTheTuMoi) : (dataHein.FromDate != rsIns.gtTheTu));
				flag = flag || (flag2 ? (dataHein.ToDate != rsIns.gtTheDenMoi) : (dataHein.ToDate != rsIns.gtTheDen));
				flag = flag || (!string.IsNullOrEmpty(dataHein.MediOrgCode) && (flag2 ? (dataHein.MediOrgCode != rsIns.maDKBDMoi) : (dataHein.MediOrgCode != rsIns.maDKBD)));
				flag = flag || dataHein.PatientName.ToUpper() != rsIns.hoTen.ToUpper();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return flag;
		}

		private void CheckHeinCardByServerBhxh(HeinCardData dataHein)
		{
			try
			{
				if (HisConfigCFG.IsCheckExamHistory)
				{
					CheckTTFull(dataHein, null);
					_HeinCardData = dataHein;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
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

		private void InitComboCommon(Control cboEditor, object data, string valueMember, string displayMember, string displayMemberCode)
		{
			try
			{
				InitComboCommon(cboEditor, data, valueMember, displayMember, 0, displayMemberCode, 0);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void InitComboCommon(Control cboEditor, object data, string valueMember, string displayMember, int displayMemberWidth, string displayMemberCode, int displayMemberCodeWidth)
		{
			try
			{
				int num = 0;
				List<ColumnInfo> list = new List<ColumnInfo>();
				if (!string.IsNullOrEmpty(displayMemberCode))
				{
					list.Add(new ColumnInfo(displayMemberCode, "", (displayMemberCodeWidth > 0) ? displayMemberCodeWidth : 100, 1));
					num += ((displayMemberCodeWidth > 0) ? displayMemberCodeWidth : 100);
				}
				if (!string.IsNullOrEmpty(displayMember))
				{
					list.Add(new ColumnInfo(displayMember, "", (displayMemberWidth > 0) ? displayMemberWidth : 250, 2));
					num += ((displayMemberWidth > 0) ? displayMemberWidth : 250);
				}
				ControlEditorADO controlEditorADO = new ControlEditorADO(displayMember, valueMember, list, false, num);
				ControlEditorLoader.Load(cboEditor, data, controlEditorADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void GetDataBeforeClickbtnPatientNew()
		{
			try
			{
				dataPatientRaw = ucPatientRaw1.GetValue();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataIntoUCPlusInfo(HisPatientSDO _currentPatientSDO, bool IsReadQr = false)
		{
			try
			{
				UCPlusInfoADO uCPlusInfoADO = new UCPlusInfoADO();
				if (IsReadQr)
				{
					if (lstNational == null || lstNational.Count == 0)
					{
						lstNational = (from o in BackendDataWorker.Get<SDA_NATIONAL>()
							where o.IS_ACTIVE == 1
							select o).ToList();
					}
					SDA_NATIONAL sDA_NATIONAL = lstNational.FirstOrDefault((SDA_NATIONAL o) => o.NATIONAL_NAME.ToLower().Equals("việt nam"));
					if (sDA_NATIONAL != null)
					{
						uCPlusInfoADO.NATIONAL_NAME = sDA_NATIONAL.NATIONAL_NAME;
						uCPlusInfoADO.NATIONAL_CODE = sDA_NATIONAL.NATIONAL_CODE;
					}
					if (_currentPatientSDO != null)
					{
						uCPlusInfoADO.CCCD_NUMBER = _currentPatientSDO.CCCD_NUMBER;
						uCPlusInfoADO.CMND_DATE = _currentPatientSDO.CMND_DATE;
					}
				}
				ucPlusInfo1.RefreshUserControl();
				if (_currentPatientSDO == null)
				{
					UCPlusInfoADO value = ucPlusInfo1.GetValue();
					value.NATIONAL_NAME = uCPlusInfoADO.NATIONAL_NAME;
					value.NATIONAL_CODE = uCPlusInfoADO.NATIONAL_CODE;
					ucPlusInfo1.SetValue(value);
					return;
				}
				uCPlusInfoADO.ETHNIC_NAME = _currentPatientSDO.ETHNIC_NAME;
				uCPlusInfoADO.ETHNIC_CODE = _currentPatientSDO.ETHNIC_CODE;
				uCPlusInfoADO.MILITARYRANK_ID = _currentPatientSDO.MILITARY_RANK_ID;
				if (!IsReadQr)
				{
					uCPlusInfoADO.NATIONAL_NAME = _currentPatientSDO.NATIONAL_NAME;
					uCPlusInfoADO.NATIONAL_CODE = _currentPatientSDO.NATIONAL_CODE;
				}
				uCPlusInfoADO.PROGRAM_ID = _currentPatientSDO.ProgramId.GetValueOrDefault();
				uCPlusInfoADO.PROGRAM_CODE = _currentPatientSDO.PatientProgramCode;
				uCPlusInfoADO.workPlaceADO = new WorkPlaceADO();
				uCPlusInfoADO.workPlaceADO.WORK_PLACE = _currentPatientSDO.WORK_PLACE;
				uCPlusInfoADO.workPlaceADO.WORK_PLACE_ID = _currentPatientSDO.WORK_PLACE_ID;
				uCPlusInfoADO.PATIENT_ID = _currentPatientSDO.ID;
				uCPlusInfoADO.EMAIL = _currentPatientSDO.EMAIL;
				uCPlusInfoADO.PATIENT_STORE_CODE = _currentPatientSDO.PATIENT_STORE_CODE;
				uCPlusInfoADO.PHONE_NUMBER = _currentPatientSDO.PHONE;
				uCPlusInfoADO.PROVINCE_OfBIRTH_CODE = _currentPatientSDO.BORN_PROVINCE_CODE;
				uCPlusInfoADO.PROVINCE_OfBIRTH_NAME = _currentPatientSDO.BORN_PROVINCE_NAME;
				uCPlusInfoADO.HT_COMMUNE_NAME = _currentPatientSDO.HT_COMMUNE_NAME;
				uCPlusInfoADO.HT_DISTRICT_NAME = _currentPatientSDO.HT_DISTRICT_NAME;
				uCPlusInfoADO.HT_PROVINCE_NAME = _currentPatientSDO.HT_PROVINCE_NAME;
				uCPlusInfoADO.HT_COMMUNE_CODE = _currentPatientSDO.HT_COMMUNE_CODE;
				uCPlusInfoADO.HT_DISTRICT_CODE = _currentPatientSDO.HT_DISTRICT_CODE;
				uCPlusInfoADO.HT_PROVINCE_CODE = _currentPatientSDO.HT_PROVINCE_CODE;
				uCPlusInfoADO.HT_ADDRESS = _currentPatientSDO.HT_ADDRESS;
				uCPlusInfoADO.BLOOD_ABO_CODE = _currentPatientSDO.BLOOD_ABO_CODE;
				uCPlusInfoADO.BLOOD_RH_CODE = _currentPatientSDO.BLOOD_RH_CODE;
				uCPlusInfoADO.MOTHER_NAME = _currentPatientSDO.MOTHER_NAME;
				uCPlusInfoADO.FATHER_NAME = _currentPatientSDO.FATHER_NAME;
				if (_currentPatientSDO.CMND_DATE.HasValue)
				{
					uCPlusInfoADO.CMND_DATE = _currentPatientSDO.CMND_DATE;
				}
				else
				{
					uCPlusInfoADO.CMND_DATE = _currentPatientSDO.CCCD_DATE;
				}
				if (!string.IsNullOrEmpty(_currentPatientSDO.CMND_NUMBER))
				{
					uCPlusInfoADO.CMND_NUMBER = _currentPatientSDO.CMND_NUMBER;
				}
				else
				{
					uCPlusInfoADO.CMND_NUMBER = _currentPatientSDO.CCCD_NUMBER;
				}
				if (!string.IsNullOrEmpty(_currentPatientSDO.CMND_PLACE))
				{
					uCPlusInfoADO.CMND_PLACE = _currentPatientSDO.CMND_PLACE;
				}
				else
				{
					uCPlusInfoADO.CMND_PLACE = _currentPatientSDO.CCCD_PLACE;
				}
				uCPlusInfoADO.HOUSEHOLD_CODE = _currentPatientSDO.HOUSEHOLD_CODE;
				uCPlusInfoADO.HOUSEHOLD_RELATION_NAME = _currentPatientSDO.HOUSEHOLD_RELATION_NAME;
				uCPlusInfoADO.TAX_CODE = _currentPatientSDO.TAX_CODE;
				uCPlusInfoADO.BUD_REL_UNIT_CODE = _currentPatientSDO.BUD_REL_UNIT_CODE;
				ucPlusInfo1.SetValue(uCPlusInfoADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void Print(bool isPrintExam = false)
		{
			try
			{
				if (isPrintExam)
				{
					if (currentHisExamServiceReqResultSDO == null || currentHisExamServiceReqResultSDO.ServiceReqs == null || currentHisExamServiceReqResultSDO.ServiceReqs.Count == 0 || actionType == 1)
					{
						XtraMessageBox.Show(ResourceMessage.NguoiDungInPhieuYeCauKhamKhongCoDuLieuDangKyKham, ResourceMessage.TieuDeCuaSoThongBaoLaThongBao, DefaultBoolean.True);
						return;
					}
					currentHisExamServiceReqResultSDO.ServiceReqs = currentHisExamServiceReqResultSDO.ServiceReqs.Where((V_HIS_SERVICE_REQ o) => serviceReqPrintIds.Contains(o.ID)).ToList();
					PrintProcess(currentHisExamServiceReqResultSDO);
				}
				else
				{
					InitMenuPrint();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void PrintProcess(HisServiceReqExamRegisterResultSDO data)
		{
			try
			{
				if (currentHisExamServiceReqResultSDO == null)
				{
					throw new ArgumentNullException("DelegateRunPrinter => currentHisExamServiceReqResultSDO is null");
				}
				if (currentHisExamServiceReqResultSDO.ServiceReqs == null || currentHisExamServiceReqResultSDO.ServiceReqs.Count == 0)
				{
					throw new ArgumentNullException("DelegateRunPrinter => ServiceReqs is null");
				}
				currentHisExamServiceReqResultSDO.ServiceReqs = currentHisExamServiceReqResultSDO.ServiceReqs.Where((V_HIS_SERVICE_REQ o) => serviceReqPrintIds.Contains(o.ID)).ToList();
				HisServiceReqListResultSDO hisServiceReqListResultSDO = new HisServiceReqListResultSDO();
				hisServiceReqListResultSDO.SereServs = currentHisExamServiceReqResultSDO.SereServs;
				hisServiceReqListResultSDO.ServiceReqs = currentHisExamServiceReqResultSDO.ServiceReqs;
				List<HIS_SERE_SERV_DEPOSIT> sereServDeposits = new List<HIS_SERE_SERV_DEPOSIT>();
				List<HIS_SERE_SERV_BILL> sereServBills = new List<HIS_SERE_SERV_BILL>();
				if (currentHisExamServiceReqResultSDO.CollectedTransactions != null && currentHisExamServiceReqResultSDO.CollectedTransactions.Count > 0)
				{
					if (currentHisExamServiceReqResultSDO.SereServBills != null && currentHisExamServiceReqResultSDO.SereServBills.Count > 0)
					{
						sereServBills = currentHisExamServiceReqResultSDO.SereServBills.Where((HIS_SERE_SERV_BILL o) => currentHisExamServiceReqResultSDO.CollectedTransactions.Exists((V_HIS_TRANSACTION s) => s.ID == o.BILL_ID)).ToList();
					}
					if (currentHisExamServiceReqResultSDO.SereServDeposits != null && currentHisExamServiceReqResultSDO.SereServDeposits.Count > 0)
					{
						sereServDeposits = currentHisExamServiceReqResultSDO.SereServDeposits.Where((HIS_SERE_SERV_DEPOSIT o) => currentHisExamServiceReqResultSDO.CollectedTransactions.Exists((V_HIS_TRANSACTION s) => s.ID == o.DEPOSIT_ID)).ToList();
					}
				}
				hisServiceReqListResultSDO.SereServBills = sereServBills;
				hisServiceReqListResultSDO.SereServDeposits = sereServDeposits;
				hisServiceReqListResultSDO.Transactions = currentHisExamServiceReqResultSDO.Transactions;
				hisServiceReqListResultSDO.DepositedSereServs = currentHisExamServiceReqResultSDO.DepositedSereServs;
				HisTreatmentWithPatientTypeInfoSDO HisTreatment = new HisTreatmentWithPatientTypeInfoSDO();
				CommonParam commonParam = new CommonParam();
				HisTreatmentWithPatientTypeInfoFilter hisTreatmentWithPatientTypeInfoFilter = new HisTreatmentWithPatientTypeInfoFilter();
				hisTreatmentWithPatientTypeInfoFilter.TREATMENT_ID = resultHisPatientProfileSDO.HisTreatment.ID;
				hisTreatmentWithPatientTypeInfoFilter.INTRUCTION_TIME = null;
				List<HisTreatmentWithPatientTypeInfoSDO> list = new BackendAdapter(commonParam).Get<List<HisTreatmentWithPatientTypeInfoSDO>>("api/HisTreatment/GetTreatmentWithPatientTypeInfoSdo", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisTreatmentWithPatientTypeInfoFilter, new Action(base.ProcessLostToken), commonParam);
				if (list != null && list.Count > 0)
				{
					HisTreatment = list.FirstOrDefault();
				}
				if (HisTreatment.TDL_PATIENT_TYPE_ID.HasValue && string.IsNullOrEmpty(HisTreatment.PATIENT_TYPE_CODE))
				{
					HisTreatment.PATIENT_TYPE_CODE = BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault((HIS_PATIENT_TYPE o) => o.ID == HisTreatment.TDL_PATIENT_TYPE_ID).PATIENT_TYPE_CODE;
					HisTreatment.HEIN_CARD_FROM_TIME = HisTreatment.TDL_HEIN_CARD_FROM_TIME.GetValueOrDefault();
					HisTreatment.HEIN_CARD_NUMBER = HisTreatment.TDL_HEIN_CARD_NUMBER;
					HisTreatment.HEIN_CARD_TO_TIME = HisTreatment.TDL_HEIN_CARD_TO_TIME.GetValueOrDefault();
					HisTreatment.HEIN_MEDI_ORG_CODE = HisTreatment.TDL_HEIN_MEDI_ORG_CODE;
					HisTreatment.LEVEL_CODE = resultHisPatientProfileSDO.HisPatientTypeAlter.LEVEL_CODE;
					HisTreatment.RIGHT_ROUTE_CODE = resultHisPatientProfileSDO.HisPatientTypeAlter.RIGHT_ROUTE_CODE;
					HisTreatment.RIGHT_ROUTE_TYPE_CODE = resultHisPatientProfileSDO.HisPatientTypeAlter.RIGHT_ROUTE_TYPE_CODE;
					HisTreatment.TREATMENT_TYPE_CODE = BackendDataWorker.Get<HIS_TREATMENT_TYPE>().FirstOrDefault((HIS_TREATMENT_TYPE o) => o.ID == HisTreatment.TDL_TREATMENT_TYPE_ID).TREATMENT_TYPE_CODE;
					HisTreatment.HEIN_CARD_ADDRESS = resultHisPatientProfileSDO.HisPatientTypeAlter.ADDRESS;
				}
				MPS.ProcessorBase.PrintConfig.PreviewType previewType = MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow;
				if (!IsActionSavePrint)
				{
					previewType = MPS.ProcessorBase.PrintConfig.PreviewType.ShowDialog;
				}
				else if (chkPrintExam.Checked && !chkSignExam.Checked)
				{
					previewType = MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow;
				}
				else if (!chkPrintExam.Checked && chkSignExam.Checked)
				{
					previewType = MPS.ProcessorBase.PrintConfig.PreviewType.EmrSignNow;
				}
				else if (chkPrintExam.Checked && chkSignExam.Checked)
				{
					previewType = MPS.ProcessorBase.PrintConfig.PreviewType.EmrSignAndPrintNow;
				}
				if (chkXemTruoc.Checked)
				{
					isPrintNow = false;
				}
				PrintServiceReqProcessor printServiceReqProcessor = new PrintServiceReqProcessor(hisServiceReqListResultSDO, HisTreatment, null, (currentModule != null) ? currentModule.RoomId : 0, string.IsNullOrEmpty(txtGateNumber.Text.Trim()) ? null : (txtGateNumber.Text.Contains(":") ? txtGateNumber.Text.Trim().Split(':')[0] : txtGateNumber.Text.Trim()), previewType);
				printServiceReqProcessor.Print("Mps000001", isPrintNow);
				isPrintNow = false;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void PrintBienLai()
		{
			try
			{
				isPrintNowBL = true;
				RichEditorStore richEditorStore = new RichEditorStore(HIS.Desktop.ApiConsumer.ApiConsumers.SarConsumer, ConfigSystems.URI_API_SAR, LanguageManager.GetLanguage(), GlobalVariables.TemnplatePathFolder);
				richEditorStore.RunPrintTemplate("Mps000420", new DelegateRunPrinter(DelegateRunPrinterInGiaoDichThanhToanPrintNow));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void PrintTamThu()
		{
			try
			{
				List<V_HIS_TRANSACTION> list = currentHisExamServiceReqResultSDO.Transactions.Where((V_HIS_TRANSACTION o) => o.TRANSACTION_TYPE_ID == 1).ToList();
				if (list != null && list.Count > 0)
				{
					isPrintNowBL = true;
					RichEditorStore richEditorStore = new RichEditorStore(HIS.Desktop.ApiConsumer.ApiConsumers.SarConsumer, ConfigSystems.URI_API_SAR, LanguageManager.GetLanguage(), GlobalVariables.TemnplatePathFolder);
					richEditorStore.RunPrintTemplate("Mps000102", new DelegateRunPrinter(DelegateRunPrinterInGiaoDichTamThuPrintNow));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void PrintPatientCard()
		{
			try
			{
				RichEditorStore richEditorStore = new RichEditorStore(HIS.Desktop.ApiConsumer.ApiConsumers.SarConsumer, ConfigSystems.URI_API_SAR, LanguageManager.GetLanguage(), GlobalVariables.TemnplatePathFolder);
				richEditorStore.RunPrintTemplate("Mps000178", new DelegateRunPrinter(DelegateRunPrinter));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void InitMenuPrint()
		{
			try
			{
				barManager.Form = this;
				if (menu == null)
				{
					menu = new PopupMenu(barManager);
				}
				menu.ItemLinks.Clear();
				BarButtonItem barButtonItem = new BarButtonItem(barManager, ResourceMessage.Title_InDichVuKham, 1);
				barButtonItem.Tag = PrintType.InDvKham;
				barButtonItem.ItemClick += new ItemClickEventHandler(onClick__Pluss);
				menu.AddItem(barButtonItem);
				BarButtonItem barButtonItem2 = new BarButtonItem(barManager, ResourceMessage.Title_InPhieuYeuCauKham, 1);
				barButtonItem2.Tag = PrintType.InPhieuYeuCauKham;
				barButtonItem2.ItemClick += new ItemClickEventHandler(onClick__Pluss);
				menu.AddItem(barButtonItem2);
				BarButtonItem barButtonItem3 = new BarButtonItem(barManager, ResourceMessage.Title_InTheBenhNhan, 2);
				barButtonItem3.Tag = PrintType.InTheBenhNhan;
				barButtonItem3.ItemClick += new ItemClickEventHandler(onClick__Pluss);
				menu.AddItem(barButtonItem3);
				BarButtonItem barButtonItem4 = new BarButtonItem(barManager, ResourceMessage.Title_InBangKiemTruocTiemChung, 1);
				barButtonItem4.Tag = PrintType.BangKiemTruocTiemChung;
				barButtonItem4.ItemClick += new ItemClickEventHandler(onClick__Pluss);
				menu.AddItem(barButtonItem4);
				if (currentHisExamServiceReqResultSDO != null && currentHisExamServiceReqResultSDO.Transactions != null && currentHisExamServiceReqResultSDO.Transactions.Count() > 0)
				{
					BarButtonItem barButtonItem5 = new BarButtonItem(barManager, ResourceMessage.Title_InBienLaiHoaDon, 1);
					barButtonItem5.Tag = PrintType.InBienLaiHoaDon;
					barButtonItem5.ItemClick += new ItemClickEventHandler(onClick__Pluss);
					menu.AddItem(barButtonItem5);
				}
				menu.ShowPopup(Cursor.Position);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void onClick__Pluss(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (!(e.Item is BarButtonItem))
				{
					return;
				}
				BarButtonItem barButtonItem = sender as BarButtonItem;
				PrintType printType = (PrintType)e.Item.Tag;
				RichEditorStore richEditorStore = new RichEditorStore(HIS.Desktop.ApiConsumer.ApiConsumers.SarConsumer, ConfigSystems.URI_API_SAR, LanguageManager.GetLanguage(), GlobalVariables.TemnplatePathFolder);
				switch (printType)
				{
				case PrintType.InDvKham:
					if (currentHisExamServiceReqResultSDO == null || currentHisExamServiceReqResultSDO.ServiceReqs == null || currentHisExamServiceReqResultSDO.ServiceReqs.Count == 0 || actionType == 1)
					{
						XtraMessageBox.Show(ResourceMessage.NguoiDungInPhieuYeCauKhamKhongCoDuLieuDangKyKham, ResourceMessage.TieuDeCuaSoThongBaoLaThongBao, DefaultBoolean.True);
						break;
					}
					currentHisExamServiceReqResultSDO.ServiceReqs = currentHisExamServiceReqResultSDO.ServiceReqs.Where((V_HIS_SERVICE_REQ o) => serviceReqPrintIds.Contains(o.ID)).ToList();
					isPrintNow = false;
					PrintProcess(currentHisExamServiceReqResultSDO);
					break;
				case PrintType.InTheBenhNhan:
					richEditorStore.RunPrintTemplate("Mps000178", new DelegateRunPrinter(DelegateRunPrinterInTheBenhNhan));
					break;
				case PrintType.InPhieuYeuCauKham:
					richEditorStore.RunPrintTemplate("Mps000309", new DelegateRunPrinter(DelegateRunPrinterInPhieuYeuCauKham));
					break;
				case PrintType.BangKiemTruocTiemChung:
					richEditorStore.RunPrintTemplate("Mps000358", new DelegateRunPrinter(DelegateRunPrinterInBangKiemTruocTiemChung));
					break;
				case PrintType.InBienLaiHoaDon:
					isPrintNowBL = false;
					richEditorStore.RunPrintTemplate("Mps000420", new DelegateRunPrinter(DelegateRunPrinterInGiaoDichThanhToanPrintNow));
					break;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private bool DelegateRunPrinterInGiaoDichThanhToan(string printTypeCode, string fileName)
		{
			bool result = false;
			try
			{
				WaitingManager.Show();
				if (currentHisExamServiceReqResultSDO == null || currentHisExamServiceReqResultSDO.Transactions == null || currentHisExamServiceReqResultSDO.Transactions.Count() == 0)
				{
					LogSystem.Error("Transaction null Print Mps000111 false");
					return result;
				}
				List<HIS_BILL_FUND> listBillFund = new List<HIS_BILL_FUND>();
				foreach (V_HIS_TRANSACTION transaction in currentHisExamServiceReqResultSDO.Transactions)
				{
					HisSereServBillFilter hisSereServBillFilter = new HisSereServBillFilter();
					hisSereServBillFilter.BILL_ID = transaction.ID;
					List<HIS_SERE_SERV_BILL> list = new BackendAdapter(new CommonParam()).Get<List<HIS_SERE_SERV_BILL>>("api/HisSereServBill/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisSereServBillFilter, null);
					if (list == null || list.Count <= 0)
					{
						LogSystem.Error("Khong lay duoc SereServBill theo BillId: " + transaction.ID);
						return result;
					}
					List<HIS_SERE_SERV> list2 = new List<HIS_SERE_SERV>();
					foreach (V_HIS_SERE_SERV sereServ in currentHisExamServiceReqResultSDO.SereServs)
					{
						HIS_SERE_SERV hIS_SERE_SERV = new HIS_SERE_SERV();
						DataObjectMapper.Map<HIS_SERE_SERV>(hIS_SERE_SERV, sereServ);
						list2.Add(hIS_SERE_SERV);
					}
					V_HIS_PATIENT_TYPE_ALTER patientTypeAlter = new V_HIS_PATIENT_TYPE_ALTER();
					if (resultHisPatientProfileSDO.HisPatientTypeAlter != null)
					{
						patientTypeAlter = GetPatientTypeAlterByPatient(resultHisPatientProfileSDO.HisPatientTypeAlter);
					}
					HisDepartmentTranLastFilter hisDepartmentTranLastFilter = new HisDepartmentTranLastFilter();
					hisDepartmentTranLastFilter.TREATMENT_ID = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.ID;
					hisDepartmentTranLastFilter.BEFORE_LOG_TIME = System.Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
					V_HIS_DEPARTMENT_TRAN departmentTran = new BackendAdapter(new CommonParam()).Get<V_HIS_DEPARTMENT_TRAN>("api/HisDepartmentTran/GetLastByTreatmentId", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisDepartmentTranLastFilter, null);
					V_HIS_PATIENT v_HIS_PATIENT = null;
					DataObjectMapper.Map<HIS_SERE_SERV>(currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatient, v_HIS_PATIENT);
					string tREATMENT_CODE = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.TREATMENT_CODE;
					InputADO inputADO = new EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode(tREATMENT_CODE, printTypeCode, (currentModule != null) ? currentModule.RoomId : 0);
					Mps000111PDO data = new Mps000111PDO(transaction, v_HIS_PATIENT, listBillFund, list2, departmentTran, patientTypeAlter, HisConfigCFG.PatientTypeId__BHYT);
					WaitingManager.Hide();
					string printerName = "";
					if (GlobalVariables.dicPrinter.ContainsKey(printTypeCode))
					{
						printerName = GlobalVariables.dicPrinter[printTypeCode];
					}
					PrintData printData = null;
					printData = ((ConfigApplications.CheDoInChoCacChucNangTrongPhanMem != 2) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.Show, printerName) : new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, printerName));
					printData.EmrInputADO = new EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode(resultHisPatientProfileSDO.HisTreatment.TREATMENT_CODE, printTypeCode, currentModule.RoomId);
					result = MpsPrinter.Run(printData);
				}
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool DelegateRunPrinterInGiaoDichThanhToanPrintNow(string printTypeCode, string fileName)
		{
			bool result = false;
			try
			{
				WaitingManager.Show();
				if (currentHisExamServiceReqResultSDO == null || currentHisExamServiceReqResultSDO.ServiceReqs == null || currentHisExamServiceReqResultSDO.ServiceReqs.Count == 0)
				{
					LogSystem.Error("ServiceReqs null Print Mps000420 false");
					return result;
				}
				if (currentHisExamServiceReqResultSDO.HisPatientProfile == null || currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatientTypeAlter == null || currentHisExamServiceReqResultSDO.HisPatientProfile.HisPatientTypeAlter.PATIENT_TYPE_ID == HisConfigCFG.PatientTypeId__BHYT)
				{
					LogSystem.Error("doi tuong thanh toan khac BHYT Print Mps000420 false");
					return result;
				}
				foreach (V_HIS_SERVICE_REQ ServiceReq in ServiceReqList)
				{
					if (currentHisExamServiceReqResultSDO.SereServs == null || currentHisExamServiceReqResultSDO.SereServs.Count <= 0)
					{
						continue;
					}
					List<V_HIS_SERE_SERV> sereServByServiceReqs = currentHisExamServiceReqResultSDO.SereServs.Where((V_HIS_SERE_SERV o) => o.SERVICE_REQ_ID == ServiceReq.ID).ToList();
					if (sereServByServiceReqs == null || sereServByServiceReqs.Count <= 0 || currentHisExamServiceReqResultSDO.SereServDeposits == null || currentHisExamServiceReqResultSDO.SereServDeposits.Count <= 0)
					{
						continue;
					}
					List<HIS_SERE_SERV_DEPOSIT> hisSSDeposits = currentHisExamServiceReqResultSDO.SereServDeposits.Where((HIS_SERE_SERV_DEPOSIT o) => sereServByServiceReqs.Exists((V_HIS_SERE_SERV e) => e.ID == o.SERE_SERV_ID)).ToList();
					if (hisSSDeposits == null || hisSSDeposits.Count <= 0 || currentHisExamServiceReqResultSDO.Transactions == null || currentHisExamServiceReqResultSDO.Transactions.Count <= 0)
					{
						continue;
					}
					List<V_HIS_TRANSACTION> list = currentHisExamServiceReqResultSDO.Transactions.Where((V_HIS_TRANSACTION o) => hisSSDeposits.Exists((HIS_SERE_SERV_DEPOSIT e) => e.DEPOSIT_ID == o.ID)).ToList();
					if (list == null || list.Count <= 0)
					{
						continue;
					}
					foreach (V_HIS_TRANSACTION item in list)
					{
						string tREATMENT_CODE = currentHisExamServiceReqResultSDO.HisPatientProfile.HisTreatment.TREATMENT_CODE;
						InputADO inputADO = new EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode(tREATMENT_CODE, printTypeCode, (currentModule != null) ? currentModule.RoomId : 0);
						Mps000420PDO data = new Mps000420PDO(item, sereServByServiceReqs, ServiceReq);
						WaitingManager.Hide();
						string printerName = "";
						if (GlobalVariables.dicPrinter.ContainsKey(printTypeCode))
						{
							printerName = GlobalVariables.dicPrinter[printTypeCode];
						}
						PrintData printData = null;
						printData = (((isPrintNowBL || ConfigApplications.CheDoInChoCacChucNangTrongPhanMem == 2) && !chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, printerName) : ((!chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.Show, printerName) : new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.ShowDialog, printerName)));
						printData.EmrInputADO = new EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode(resultHisPatientProfileSDO.HisTreatment.TREATMENT_CODE, printTypeCode, currentModule.RoomId);
						result = MpsPrinter.Run(printData);
					}
				}
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool DelegateRunPrinterInGiaoDichTamThuPrintNow(string printTypeCode, string fileName)
		{
			bool result = false;
			try
			{
				WaitingManager.Show();
				if (currentHisExamServiceReqResultSDO == null || currentHisExamServiceReqResultSDO.Transactions == null)
				{
					LogSystem.Error("Transactions null Print Mps000102 false");
					return result;
				}
				List<V_HIS_TRANSACTION> list = currentHisExamServiceReqResultSDO.Transactions.Where((V_HIS_TRANSACTION o) => o.TRANSACTION_TYPE_ID == 1).ToList();
				if (list == null)
				{
					LogSystem.Error("deposit null Print Mps000102 false");
					return result;
				}
				CommonParam commonParam = new CommonParam();
				HisSereServView12Filter hisSereServView12Filter = new HisSereServView12Filter();
				hisSereServView12Filter.TREATMENT_ID = currentHisExamServiceReqResultSDO.Transactions.First().TREATMENT_ID.Value;
				hisSereServView12Filter.IDs = currentHisExamServiceReqResultSDO.SereServDeposits.Select((HIS_SERE_SERV_DEPOSIT s) => s.SERE_SERV_ID).ToList();
				List<V_HIS_SERE_SERV_12> list2 = new BackendAdapter(commonParam).Get<List<V_HIS_SERE_SERV_12>>("api/HisSereServ/GetView12", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisSereServView12Filter, commonParam);
				foreach (V_HIS_SERE_SERV_12 item in list2)
				{
					HIS_SERE_SERV_DEPOSIT hIS_SERE_SERV_DEPOSIT = currentHisExamServiceReqResultSDO.SereServDeposits.FirstOrDefault((HIS_SERE_SERV_DEPOSIT o) => o.SERE_SERV_ID == item.ID);
					if (hIS_SERE_SERV_DEPOSIT != null)
					{
						item.VIR_TOTAL_PATIENT_PRICE = hIS_SERE_SERV_DEPOSIT.AMOUNT;
					}
				}
				isPrintNow = false;
				if (isPrintNowBL || ConfigApplications.CheDoInChoCacChucNangTrongPhanMem == 2)
				{
					isPrintNow = true;
				}
				if (!chkXemTruoc.Checked)
				{
					DepositServicePrintProcess.LoadPhieuThuPhiDichVu(printTypeCode, fileName, true, list2, currentHisExamServiceReqResultSDO, isPrintNow, currentModule);
				}
				else
				{
					DepositServicePrintProcess.LoadPhieuThuPhiDichVu(printTypeCode, fileName, true, list2, currentHisExamServiceReqResultSDO, false, currentModule);
				}
				result = true;
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool DelegateRunPrinterInBangKiemTruocTiemChung(string printTypeCode, string fileName)
		{
			bool result = false;
			try
			{
				if (resultHisPatientProfileSDO == null || resultHisPatientProfileSDO.HisPatient == null)
				{
					XtraMessageBox.Show(ResourceMessage.DuLieuRong, ResourceMessage.TieuDeCuaSoThongBaoLaThongBao, DefaultBoolean.True);
					return result;
				}
				WaitingManager.Show();
				V_HIS_PATIENT patient = new V_HIS_PATIENT();
				if (resultHisPatientProfileSDO.HisPatient != null)
				{
					HisPatientViewFilter hisPatientViewFilter = new HisPatientViewFilter();
					hisPatientViewFilter.ID = resultHisPatientProfileSDO.HisPatient.ID;
					List<V_HIS_PATIENT> list = new BackendAdapter(new CommonParam()).Get<List<V_HIS_PATIENT>>("api/HisPatient/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisPatientViewFilter, new Action(SessionManager.ActionLostToken), null);
					if (list != null && list.Count > 0)
					{
						patient = list.FirstOrDefault();
					}
				}
				MPS.Processor.Mps000358.PDO.SingleKeyValue singleKeyValue = new MPS.Processor.Mps000358.PDO.SingleKeyValue();
				Mps000358PDO data = new Mps000358PDO(patient, singleKeyValue);
				WaitingManager.Hide();
				PrintData printData = null;
				InputADO emrInputADO = new EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode(resultHisPatientProfileSDO.HisTreatment.TREATMENT_CODE, printTypeCode, currentModule.RoomId);
				printData = ((ConfigApplications.CheDoInChoCacChucNangTrongPhanMem == 2 && !chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "")
				{
					EmrInputADO = emrInputADO
				} : ((!chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.Show, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "", 1, false, true)
				{
					EmrInputADO = emrInputADO
				} : new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.ShowDialog, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "", 1, false, true)
				{
					EmrInputADO = emrInputADO
				}));
				result = MpsPrinter.Run(printData);
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool DelegateRunPrinterInPhieuYeuCauKham(string printTypeCode, string fileName)
		{
			bool result = false;
			try
			{
				if (resultHisPatientProfileSDO == null || resultHisPatientProfileSDO.HisPatient == null)
				{
					XtraMessageBox.Show(ResourceMessage.DuLieuRong, ResourceMessage.TieuDeCuaSoThongBaoLaThongBao, DefaultBoolean.True);
					return result;
				}
				WaitingManager.Show();
				V_HIS_PATIENT currentPatient = new V_HIS_PATIENT();
				if (resultHisPatientProfileSDO.HisPatient != null)
				{
					HisPatientViewFilter hisPatientViewFilter = new HisPatientViewFilter();
					hisPatientViewFilter.ID = resultHisPatientProfileSDO.HisPatient.ID;
					List<V_HIS_PATIENT> list = new BackendAdapter(new CommonParam()).Get<List<V_HIS_PATIENT>>("api/HisPatient/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisPatientViewFilter, new Action(SessionManager.ActionLostToken), null);
					if (list != null && list.Count > 0)
					{
						currentPatient = list.FirstOrDefault();
					}
				}
				MPS.Processor.Mps000309.PDO.SingleKeyValue singleKeyValue = new MPS.Processor.Mps000309.PDO.SingleKeyValue
				{
					LoginName = ClientTokenManagerStore.ClientTokenManager.GetLoginName(),
					Username = ClientTokenManagerStore.ClientTokenManager.GetUserName()
				};
				HIS_DHST hIS_DHST = new HIS_DHST();
				CommonParam commonParam = new CommonParam();
				HisDhstFilter hisDhstFilter = new HisDhstFilter();
				hisDhstFilter.TREATMENT_ID = resultHisPatientProfileSDO.HisTreatment.ID;
				hisDhstFilter.ORDER_FIELD = "EXECUTE_TIME";
				hisDhstFilter.ORDER_DIRECTION = "DESC";
				hIS_DHST = new BackendAdapter(commonParam).Get<List<HIS_DHST>>("api/HisDHST/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisDhstFilter, commonParam).FirstOrDefault();
				V_HIS_PATIENT_TYPE_ALTER patyAlter = new V_HIS_PATIENT_TYPE_ALTER();
				if (resultHisPatientProfileSDO.HisPatientTypeAlter != null)
				{
					patyAlter = GetPatientTypeAlterByPatient(resultHisPatientProfileSDO.HisPatientTypeAlter);
				}
				Mps000309PDO data = new Mps000309PDO(currentPatient, patyAlter, hIS_DHST, resultHisPatientProfileSDO.HisTreatment, singleKeyValue);
				WaitingManager.Hide();
				PrintData printData = null;
				InputADO emrInputADO = new EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode(resultHisPatientProfileSDO.HisTreatment.TREATMENT_CODE, printTypeCode, currentModule.RoomId);
				printData = ((ConfigApplications.CheDoInChoCacChucNangTrongPhanMem == 2 && !chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "")
				{
					EmrInputADO = emrInputADO
				} : ((!chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.Show, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "", 1, false, true)
				{
					EmrInputADO = emrInputADO
				} : new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.ShowDialog, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "", 1, false, true)
				{
					EmrInputADO = emrInputADO
				}));
				result = MpsPrinter.Run(printData);
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool DelegateRunPrinterInTheBenhNhan(string printTypeCode, string fileName)
		{
			bool result = false;
			try
			{
				if (resultHisPatientProfileSDO == null || resultHisPatientProfileSDO.HisPatient == null)
				{
					XtraMessageBox.Show(ResourceMessage.DuLieuRong, ResourceMessage.TieuDeCuaSoThongBaoLaThongBao, DefaultBoolean.True);
					return result;
				}
				WaitingManager.Show();
				V_HIS_PATIENT currentPatient = new V_HIS_PATIENT();
				if (resultHisPatientProfileSDO.HisPatient != null)
				{
					HisPatientViewFilter hisPatientViewFilter = new HisPatientViewFilter();
					hisPatientViewFilter.ID = resultHisPatientProfileSDO.HisPatient.ID;
					List<V_HIS_PATIENT> list = new BackendAdapter(new CommonParam()).Get<List<V_HIS_PATIENT>>("api/HisPatient/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisPatientViewFilter, new Action(SessionManager.ActionLostToken), null);
					if (list != null && list.Count > 0)
					{
						currentPatient = list.FirstOrDefault();
					}
				}
				V_HIS_PATIENT_TYPE_ALTER patientTypeAlter = new V_HIS_PATIENT_TYPE_ALTER();
				if (resultHisPatientProfileSDO.HisPatientTypeAlter != null)
				{
					patientTypeAlter = GetPatientTypeAlterByPatient(resultHisPatientProfileSDO.HisPatientTypeAlter);
				}
				V_HIS_TREATMENT_4 v_HIS_TREATMENT_ = new V_HIS_TREATMENT_4();
				V_HIS_DEPARTMENT_TRAN departmentTran = new V_HIS_DEPARTMENT_TRAN();
				if (resultHisPatientProfileSDO.HisTreatment != null)
				{
					HisTreatmentView4Filter hisTreatmentView4Filter = new HisTreatmentView4Filter();
					hisTreatmentView4Filter.ID = resultHisPatientProfileSDO.HisTreatment.ID;
					List<V_HIS_TREATMENT_4> list2 = new BackendAdapter(new CommonParam()).Get<List<V_HIS_TREATMENT_4>>("api/HisTreatment/GetView4", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisTreatmentView4Filter, null);
					if (list2 != null && list2.Count > 0)
					{
						v_HIS_TREATMENT_ = list2.First();
					}
					HisDepartmentTranViewFilter hisDepartmentTranViewFilter = new HisDepartmentTranViewFilter();
					hisDepartmentTranViewFilter.ID = resultHisPatientProfileSDO.HisTreatment.ID;
					List<V_HIS_DEPARTMENT_TRAN> list3 = new BackendAdapter(new CommonParam()).Get<List<V_HIS_DEPARTMENT_TRAN>>("api/HisDepartmentTran/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisDepartmentTranViewFilter, null);
					if (list3 != null && list3.Count > 0)
					{
						list3 = (from o in list3
							orderby o.DEPARTMENT_IN_TIME ?? long.MaxValue descending, o.ID descending
							select o).ToList();
						departmentTran = list3.First();
					}
				}
				Mps000178PDO data = new Mps000178PDO(currentPatient, patientTypeAlter, v_HIS_TREATMENT_, departmentTran);
				WaitingManager.Hide();
				PrintData printData = null;
				InputADO emrInputADO = new EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode(v_HIS_TREATMENT_.TREATMENT_CODE, printTypeCode, currentModule.RoomId);
				printData = ((ConfigApplications.CheDoInChoCacChucNangTrongPhanMem == 2 && !chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "")
				{
					EmrInputADO = emrInputADO
				} : ((!chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.Show, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "", 1, false, true)
				{
					EmrInputADO = emrInputADO
				} : new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.ShowDialog, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "", 1, false, true)
				{
					EmrInputADO = emrInputADO
				}));
				result = MpsPrinter.Run(printData);
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool DelegateRunPrinterInTheBenhNhanPrintNow(string printTypeCode, string fileName)
		{
			bool result = false;
			try
			{
				if (resultHisPatientProfileSDO == null || resultHisPatientProfileSDO.HisPatient == null)
				{
					XtraMessageBox.Show(ResourceMessage.DuLieuRong, ResourceMessage.TieuDeCuaSoThongBaoLaThongBao, DefaultBoolean.True);
					return result;
				}
				WaitingManager.Show();
				V_HIS_PATIENT currentPatient = new V_HIS_PATIENT();
				if (resultHisPatientProfileSDO.HisPatient != null)
				{
					HisPatientViewFilter hisPatientViewFilter = new HisPatientViewFilter();
					hisPatientViewFilter.ID = resultHisPatientProfileSDO.HisPatient.ID;
					List<V_HIS_PATIENT> list = new BackendAdapter(new CommonParam()).Get<List<V_HIS_PATIENT>>("api/HisPatient/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisPatientViewFilter, new Action(SessionManager.ActionLostToken), null);
					if (list != null && list.Count > 0)
					{
						currentPatient = list.FirstOrDefault();
					}
				}
				V_HIS_PATIENT_TYPE_ALTER patientTypeAlter = new V_HIS_PATIENT_TYPE_ALTER();
				if (resultHisPatientProfileSDO.HisPatientTypeAlter != null)
				{
					patientTypeAlter = GetPatientTypeAlterByPatient(resultHisPatientProfileSDO.HisPatientTypeAlter);
				}
				V_HIS_TREATMENT_4 treatment = new V_HIS_TREATMENT_4();
				V_HIS_DEPARTMENT_TRAN departmentTran = new V_HIS_DEPARTMENT_TRAN();
				if (resultHisPatientProfileSDO.HisTreatment != null)
				{
					HisTreatmentView4Filter hisTreatmentView4Filter = new HisTreatmentView4Filter();
					hisTreatmentView4Filter.ID = resultHisPatientProfileSDO.HisTreatment.ID;
					List<V_HIS_TREATMENT_4> list2 = new BackendAdapter(new CommonParam()).Get<List<V_HIS_TREATMENT_4>>("api/HisTreatment/GetView4", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisTreatmentView4Filter, null);
					if (list2 != null && list2.Count > 0)
					{
						treatment = list2.First();
					}
					HisDepartmentTranViewFilter hisDepartmentTranViewFilter = new HisDepartmentTranViewFilter();
					hisDepartmentTranViewFilter.TREATMENT_ID = resultHisPatientProfileSDO.HisTreatment.ID;
					List<V_HIS_DEPARTMENT_TRAN> list3 = new BackendAdapter(new CommonParam()).Get<List<V_HIS_DEPARTMENT_TRAN>>("api/HisDepartmentTran/GetView", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisDepartmentTranViewFilter, null);
					if (list3 != null && list3.Count > 0)
					{
						list3 = (from o in list3
							orderby o.DEPARTMENT_IN_TIME ?? long.MaxValue descending, o.ID descending
							select o).ToList();
						departmentTran = list3.First();
					}
				}
				Mps000178PDO data = new Mps000178PDO(currentPatient, patientTypeAlter, treatment, departmentTran);
				WaitingManager.Hide();
				PrintData printData = null;
				printData = ((GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode]) && !chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, GlobalVariables.dicPrinter[printTypeCode]) : ((!chkXemTruoc.Checked) ? new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, "") : new PrintData(printTypeCode, fileName, data, MPS.ProcessorBase.PrintConfig.PreviewType.ShowDialog, "")));
				result = MpsPrinter.Run(printData);
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
			return result;
		}

		private bool DelegateRunPrinter(string printTypeCode, string fileName)
		{
			bool result = false;
			try
			{
				if (printTypeCode == "Mps000178")
				{
					result = DelegateRunPrinterInTheBenhNhanPrintNow(printTypeCode, fileName);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return result;
		}

		private V_HIS_PATIENT_TYPE_ALTER GetPatientTypeAlterByPatient(HIS_PATIENT_TYPE_ALTER patientTypeAlter)
		{
			V_HIS_PATIENT_TYPE_ALTER v_HIS_PATIENT_TYPE_ALTER = new V_HIS_PATIENT_TYPE_ALTER();
			try
			{
				DataObjectMapper.Map<V_HIS_PATIENT_TYPE_ALTER>(v_HIS_PATIENT_TYPE_ALTER, patientTypeAlter);
				HIS_PATIENT_TYPE hIS_PATIENT_TYPE = BackendDataWorker.Get<HIS_PATIENT_TYPE>().FirstOrDefault((HIS_PATIENT_TYPE o) => o.ID == patientTypeAlter.PATIENT_TYPE_ID);
				if (hIS_PATIENT_TYPE != null)
				{
					v_HIS_PATIENT_TYPE_ALTER.PATIENT_TYPE_CODE = hIS_PATIENT_TYPE.PATIENT_TYPE_CODE;
					v_HIS_PATIENT_TYPE_ALTER.PATIENT_TYPE_NAME = hIS_PATIENT_TYPE.PATIENT_TYPE_NAME;
				}
				HIS_TREATMENT_TYPE hIS_TREATMENT_TYPE = BackendDataWorker.Get<HIS_TREATMENT_TYPE>().FirstOrDefault((HIS_TREATMENT_TYPE o) => o.ID == patientTypeAlter.TREATMENT_TYPE_ID);
				if (hIS_TREATMENT_TYPE != null)
				{
					v_HIS_PATIENT_TYPE_ALTER.TREATMENT_TYPE_CODE = hIS_TREATMENT_TYPE.TREATMENT_TYPE_CODE;
					v_HIS_PATIENT_TYPE_ALTER.TREATMENT_TYPE_NAME = hIS_TREATMENT_TYPE.TREATMENT_TYPE_NAME;
				}
				if (v_HIS_PATIENT_TYPE_ALTER == null)
				{
					LogSystem.Debug("GetPatientTypeAlterByPatient => null");
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return v_HIS_PATIENT_TYPE_ALTER;
		}

		private string GetDefaultHeinRatioForView(string heinCardNumber, string treatmentTypeCode, string levelCode, string rightRouteCode)
		{
			string result = "";
			try
			{
				result = new BhytHeinProcessor().GetDefaultHeinRatio(treatmentTypeCode, heinCardNumber, levelCode, rightRouteCode).GetValueOrDefault() * 100m + "%";
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private V_HIS_ROOM GetRoomById(long id)
		{
			V_HIS_ROOM v_HIS_ROOM = null;
			try
			{
				v_HIS_ROOM = BackendDataWorker.Get<V_HIS_ROOM>().SingleOrDefault((V_HIS_ROOM o) => o.ID == id);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return v_HIS_ROOM ?? new V_HIS_ROOM();
		}

		private HIS_TRAN_PATI_REASON GetTranPatiReasonById(long id)
		{
			HIS_TRAN_PATI_REASON hIS_TRAN_PATI_REASON = null;
			try
			{
				hIS_TRAN_PATI_REASON = BackendDataWorker.Get<HIS_TRAN_PATI_REASON>().SingleOrDefault((HIS_TRAN_PATI_REASON o) => o.ID == id);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return hIS_TRAN_PATI_REASON ?? new HIS_TRAN_PATI_REASON();
		}

        private async Task SetDefaultCashierRoom()
        {
            try
            {
                List<HIS_CASHIER_ROOM> listCashier;

                if (BackendDataWorker.IsExistsKey<HIS_CASHIER_ROOM>())
                {
                    listCashier = BackendDataWorker.Get<HIS_CASHIER_ROOM>();
                }
                else
                {
                    CommonParam paramCommon = new CommonParam();
                    dynamic filter = new ExpandoObject();

                    listCashier = await new BackendAdapter(paramCommon)
                        .GetAsync<List<HIS_CASHIER_ROOM>>(
                            "api/HisCashierRoom/Get",
                            HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer,
                            filter,
                            paramCommon);

                    if (listCashier != null)
                    {
                        BackendDataWorker.UpdateToRam(
                            typeof(HIS_CASHIER_ROOM),
                            listCashier,
                            long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
                    }
                }

                List<long> roomIds = WorkPlace.GetRoomIds();

                if (roomIds == null || roomIds.Count == 0)
                {
                    throw new ArgumentNullException(
                        "Nguoi dung khong chon phong thu ngan nao");
                }

                listCashier = listCashier
                    .Where(o => roomIds.Contains(o.ROOM_ID))
                    .ToList();

                InitComboCommon(
                    cboCashierRoom,
                    listCashier,
                    "ID",
                    "CASHIER_ROOM_NAME",
                    "CASHIER_ROOM_CODE");

                if (listCashier != null && listCashier.Count == 1)
                {
                    cboCashierRoom.EditValue = listCashier[0].ID;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

		private void SetPatientDTOFromCardSDO(HisCardSDO cardSDO, HisPatientSDO patientByCard)
		{
			try
			{
				if (cardSDO == null)
				{
					throw new ArgumentNullException("cardSDO");
				}
				if (patientByCard == null)
				{
					throw new ArgumentNullException("patientByCard");
				}
				patientByCard.ID = cardSDO.PatientId.GetValueOrDefault();
				patientByCard.PATIENT_CODE = cardSDO.PatientCode;
				patientByCard.FIRST_NAME = cardSDO.FirstName;
				patientByCard.LAST_NAME = cardSDO.LastName;
				patientByCard.ADDRESS = cardSDO.Address;
				patientByCard.CAREER_ID = cardSDO.CareerId;
				patientByCard.CMND_DATE = cardSDO.CmndDate;
				patientByCard.CMND_NUMBER = cardSDO.CmndNumber;
				patientByCard.CMND_PLACE = cardSDO.CmndPlace;
				patientByCard.COMMUNE_NAME = cardSDO.CommuneName;
				patientByCard.DISTRICT_NAME = cardSDO.DistrictName;
				patientByCard.PROVINCE_NAME = cardSDO.ProvinceName;
				patientByCard.NATIONAL_NAME = cardSDO.NationalName;
				patientByCard.DOB = cardSDO.Dob;
				patientByCard.EMAIL = cardSDO.Email;
				patientByCard.ETHNIC_NAME = cardSDO.EthnicName;
				if (cardSDO.Dob > 0 && cardSDO.Dob.ToString().Length == 4)
				{
					patientByCard.IS_HAS_NOT_DAY_DOB = (short)1;
				}
				else
				{
					patientByCard.IS_HAS_NOT_DAY_DOB = 0;
				}
				patientByCard.PHONE = cardSDO.Phone;
				patientByCard.RELIGION_NAME = cardSDO.ReligionName;
				patientByCard.VIR_ADDRESS = cardSDO.VirAddress;
				patientByCard.VIR_PATIENT_NAME = patientByCard.LAST_NAME + " " + patientByCard.FIRST_NAME;
				patientByCard.GENDER_ID = cardSDO.GenderId;
				patientByCard.HeinAddress = cardSDO.HeinAddress;
				patientByCard.HeinCardFromTime = cardSDO.HeinCardFromTime;
				patientByCard.HeinCardNumber = cardSDO.HeinCardNumber;
				patientByCard.HeinCardToTime = cardSDO.HeinCardToTime;
				patientByCard.HeinMediOrgCode = cardSDO.HeinOrgCode;
				patientByCard.HeinMediOrgName = cardSDO.HeinOrgName;
				patientByCard.IS_HAS_NOT_DAY_DOB = cardSDO.IsHasNotDayDob;
				patientByCard.Join5Year = cardSDO.Join5Year;
				patientByCard.LiveAreaCode = cardSDO.LiveAreaCode;
				patientByCard.Paid6Month = cardSDO.Paid6Month;
				patientByCard.RightRouteCode = cardSDO.RightRouteCode;
				patientByCard.WORK_PLACE = cardSDO.WorkPlace;
				patientByCard.VIR_ADDRESS = cardSDO.VirAddress;
				patientByCard.PERSON_CODE = cardSDO.PersonCode;
				patientByCard.HT_COMMUNE_NAME = (HtCommuneName = cardSDO.HtCommuneName);
				patientByCard.HT_DISTRICT_NAME = (HtDistrictName = cardSDO.HtDistrictName);
				patientByCard.HT_PROVINCE_NAME = (HtProvinceName = cardSDO.HtProvinceName);
				patientByCard.HT_COMMUNE_CODE = (HtCommuneCode = cardSDO.HtCommuneCode);
				patientByCard.HT_DISTRICT_CODE = (HtDistrictCode = cardSDO.HtDistrictCode);
				patientByCard.HT_PROVINCE_CODE = (HtProvinceCode = cardSDO.HtProvinceCode);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void FillDataToHeinCardControlByCardSDO(HisCardSDO cardSDO)
		{
			try
			{
				if (string.IsNullOrEmpty(cardSDO.HeinCardNumber))
				{
					return;
				}
				if (new BhytHeinProcessor().IsValidHeinCardNumber(cardSDO.HeinCardNumber))
				{
					HIS_PATIENT_TYPE_ALTER hIS_PATIENT_TYPE_ALTER = new HIS_PATIENT_TYPE_ALTER();
					hIS_PATIENT_TYPE_ALTER.HEIN_CARD_NUMBER = cardSDO.HeinCardNumber;
					hIS_PATIENT_TYPE_ALTER.HEIN_CARD_FROM_TIME = cardSDO.HeinCardFromTime;
					hIS_PATIENT_TYPE_ALTER.HEIN_CARD_TO_TIME = cardSDO.HeinCardToTime;
					hIS_PATIENT_TYPE_ALTER.HEIN_MEDI_ORG_CODE = cardSDO.HeinOrgCode;
					hIS_PATIENT_TYPE_ALTER.HEIN_MEDI_ORG_NAME = cardSDO.HeinOrgName;
					hIS_PATIENT_TYPE_ALTER.ADDRESS = cardSDO.HeinAddress;
					hIS_PATIENT_TYPE_ALTER.JOIN_5_YEAR = cardSDO.Join5Year;
					hIS_PATIENT_TYPE_ALTER.PAID_6_MONTH = cardSDO.Paid6Month;
					hIS_PATIENT_TYPE_ALTER.LEVEL_CODE = cardSDO.LevelCode;
					hIS_PATIENT_TYPE_ALTER.LIVE_AREA_CODE = cardSDO.LiveAreaCode;
					hIS_PATIENT_TYPE_ALTER.RIGHT_ROUTE_CODE = cardSDO.RightRouteCode;
				}
				else
				{
					LogSystem.Debug("So the bhyt (tu du lieu tra ve khi quet the thong minh vao dau doc) khong hop le. " + LogUtil.TraceData(LogUtil.GetMemberName(() => cardSDO), cardSDO));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataIntoUCRelativeInfo(object data)
		{
			try
			{
				if (data is HisPatientSDO)
				{
					HisPatientSDO hisPatientSDO = (HisPatientSDO)data;
					UCRelativeADO uCRelativeADO = new UCRelativeADO();
					uCRelativeADO.Correlated = hisPatientSDO.RELATIVE_TYPE;
					uCRelativeADO.RelativeCMND = hisPatientSDO.RELATIVE_CMND_NUMBER;
					uCRelativeADO.RelativeAddress = hisPatientSDO.RELATIVE_ADDRESS;
					uCRelativeADO.RelativeName = hisPatientSDO.RELATIVE_NAME;
					uCRelativeADO.RelativePhone = hisPatientSDO.RELATIVE_PHONE;
					uCRelativeADO.FatherName = hisPatientSDO.FATHER_NAME;
					uCRelativeADO.MotherName = hisPatientSDO.MOTHER_NAME;
					ucRelativeInfo1.SetValue(uCRelativeADO);
				}
				else if (data is UCRelativeADO)
				{
					UCRelativeADO value = (UCRelativeADO)data;
					ucRelativeInfo1.SetValue(value);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private async Task FocusNextUserControl()
		{
			try
			{
				ucPatientRaw1.FocusNextUserControl(new DelegateFocusNextUserControl(focusToUCAddressCombo));
				ucPatientRaw1.FocusToUCRelativeWhenPatientIsChild(new DelegateFocusNextUserControl(focusToUCPersonHomeInfo));
				ucPatientRaw1.SetDelegateFocusNextUserControlWhenPatientIsChild(new DelegateSetFocusWhenPatientIsChild(SetDelegateFocusWhenPatientIsChild));
				ucPatientRaw1.GetDataBySearchPatient(new DelegateSetDataRegisterBeforeSerachPatient(FillDataAfterSearchPatientInUCPatientRaw), new Action<HeinCardData>(FillDataPreviewForSearchByQrcodeInUCPatientRaw), new Action<HisPatientSDO>(FillDataPreviewForSearchByQrcodeInUCPatientRawPatientSDO), new Action<HisPatientSDO>(InitExamServiceRoomByAppoimentTime));
				ucPatientRaw1.InitDelegateProcessChangePatientDob(new Action(ProcessWhileChangeDOb));
				ucPatientRaw1.SetDelegateVisibleUCHein(new DelegateVisibleUCHein(IsVisibleUCHein));
				ucPatientRaw1.SetDelegateShowControlHrmKskCode(new DelegateShowControlHrmKskCode(ShowControlHrmKskCode));
				ucPatientRaw1.SetDelegateShowControlHrmKskCodeNotValid(new DelegateShowControlHrmKskCodeNotValid(ShowControlHrmKskCodeNotValid));
				ucPatientRaw1.SetDelegateShowControlGuaranteeLoginname(new DelegateShowControlGuaranteeLoginname(ShowControlGuaranteeLoginname));
				ucPatientRaw1.SetDelegateSendPatientName(new DelegateSendPatientName(SendPatientName));
				ucPatientRaw1.SetDelegateSendPatientSDO(new DelegateSendPatientSDO(SendPatientSDO));
				ucPatientRaw1.SetDelegateShowCheckWorkingLetter(new Action<bool>(ucHeinInfo1.ShowCheckWorkingLetter));
				ucPatientRaw1.SetDelegateCheckSS(new HIS.Desktop.DelegateRegister.DelegateCheckSS(ucHeinInfo1.ShowCheckSS));
				ucPatientRaw1.SetDelegateShowOrtherPaySource(new Action<long>(ucOtherServiceReqInfo1.ShowOrtherPay));
				ucPatientRaw1.SetDelegateSendTypeFind(new DelegateEnableFindType(ChangeFindTypeInPatientRaw));
				ucPatientRaw1.SetDelegateCheckboxExamOnline(new DelegateCheckExamOnline(ucOtherServiceReqInfo1.CheckExamOnline));
				ucOtherServiceReqInfo1.SetDelegateHeinRightRouteType(new Action<bool>(SetRightRouteEmergencyWhenRegisterOutTime));
				ucOtherServiceReqInfo1.SetDelegatePriorityNumberChanged(new Action<long?>(SetServuceRoomAddButtonWhenRegisterHasPriorityNumber));
				ucOtherServiceReqInfo1.FillDataOweTypeDefault();
				if (HisConfigCFG.IsAutoFocusToSavePrintAfterChoosingExam)
				{
					ucServiceRoomInfo1.FocusNextUserControlSurcharge(new DelegateFocusNextUserControl(focusToBtnSaveAndPrint));
				}
				else
				{
					ucServiceRoomInfo1.FocusNextUserControlSurcharge(new DelegateFocusNextUserControl(focusToBtnSave));
				}
				if (HisConfigCFG.IsAutoFocusToSavePrintAfterChoosingExam)
				{
					ucServiceRoomInfo1.FocusNextUserControl(new DelegateFocusNextUserControl(focusToBtnSaveAndPrint));
				}
				else
				{
					ucServiceRoomInfo1.FocusNextUserControl(new DelegateFocusNextUserControl(focusToBtnSave));
				}
				await ucPlusInfo1.UCPlusInfoOnLoadAsync();
				ucPlusInfo1.FocusNextUserControl(new DelegateFocusNextUserControl(focusToUCImageInfo));
				UCImageInfoADO dataImage = new UCImageInfoADO
				{
					_FocusNextUserControl = new Action<object>(focusOutUserControlImageInfo),
					_ReloadDataByCmndAfter = new Action<object>(ReloadDataByCmndAfter),
					_ReloadDataByCmndBefore = new Action<object>(ReloadDataByCmndBefore)
				};
				ucImageInfo1.SetValue(dataImage);
				ValidationRelative();
				EnableLciBenhNhanMoiAfterSearchPatient();
				SetDelegateCheckTT();
				SetDelegateEnableButtonSave();
				long patientTypeId = ucPatientRaw1.GetValue().PATIENTTYPE_ID;
				if (patientTypeId == HisConfigCFG.PatientTypeId__BHYT || patientTypeId == HisConfigCFG.PatientTypeId__QN)
				{
					ucAddressCombo1.FocusNextUserControl(new DelegateFocusNextUserControl(focusToUCHeinInfo));
				}
				else if (patientTypeId == HisConfigCFG.PatientTypeId__KSK)
				{
					if (ucKskContract != null && kskContractProcessor != null)
					{
						DelegateFocusNextUserControl dlg = new DelegateFocusNextUserControl(FocusInKskContract);
						ucAddressCombo1.FocusNextUserControl(dlg);
					}
				}
				else if (ucServiceRoomInfo1 != null)
				{
					DelegateFocusNextUserControl dlg2 = new DelegateFocusNextUserControl(ucServiceRoomInfo1.FocusUserControl);
					ucAddressCombo1.FocusNextUserControl(dlg2);
				}
				long? treatmentTypeId = ucPatientRaw1.GetValue().TREATMENT_TYPE_ID;
				LogSystem.Debug(LogUtil.TraceData("FocusNextUserControl treatmentTypeId ", treatmentTypeId));
				if (HisConfigCFG.IsDefaultTreatmentTypeExam)
				{
					AutoSetTreatmentTypeCombo(1L);
				}
				else if (treatmentTypeId.HasValue && treatmentTypeId.Value == 2)
				{
					AutoSetTreatmentTypeCombo(treatmentTypeId);
				}
				ucAddressCombo1.SetDelegateSetAddressUCHein(new DelegateSetAddressUCHein(SetDelegateSetAddressUCHein));
				ucAddressCombo1.SetDelegateSetAddressUCPlusInfo(new DelegateSetAddressUCPlusInfo(SetDelegateSetAddressUCProvinceOfBirth));
				ucAddressCombo1.SetDelegateSendProvince(new DelegateSendCodeProvince(SendCodeProvince));
				ucHeinInfo1.SetEnableControlEmergency(new DelegateVisible(SetEnableEmergency));
				ucHeinInfo1.SetShowThongTinChuyenTuyen(new DelegateVisible(ShowFormThongTinChuyenTuyen));
				ucHeinInfo1.SetCareerByHeinCardNumber(new DelegateSetCareerByHeinCardNumber(SetCareerByCardNumber));
				ucHeinInfo1.SetDelegateDisableBtnTTCT(new DelegateVisible(SetDisableBtnTTCT));
				ucHeinInfo1.FocusNextUserControl(new DelegateFocusNextUserControl(focusToUCServiceRoomInfo));
				ucHeinInfo1.SetDelegateChangePatientDob(new Action(ProcessWhileChangeDOb));
				ucHeinInfo1.SetInformationPatientRawFromUC(ucPatientRaw1);
				ucHeinInfo1.SetCurrentModule(currentModule);
				ucHeinInfo1.Send3WBhytCode(new DelegateSend3WBhytCode(Send3WCode));
				ucOtherServiceReqInfo1.GetTreatmentTypeIdForUcHeinInfo(new Action<string>(ucHeinInfo1.ReceiveIdFromUcOtherSviceReqInfo));
				ucOtherServiceReqInfo1.GetTreatmentTypeIdForUcHeinInfo(new Action<string>(ucServiceRoomInfo1.getTreatmentTypeId));
				ucOtherServiceReqInfo1.GetIntructionTimeSelected(new Action<long?>(ucServiceRoomInfo1.ReceivedIntructionTime));
				ucOtherServiceReqInfo1.GetTreatmentTypeId(new Action<string>(ucPlusInfo1.ReceiveTreatmentTypeIdFromUcOther));
				ucPatientRaw1.TransferPatient(new Action<long, long?>(ucPlusInfo1.ReceivePatientFromUcPatientRaw));
				ucHeinInfo1.SendTreatmentTypeId(new Action<string>(ucOtherServiceReqInfo1.ReceiveTreatmentTypeId));
				EnableOrDisablechkTheTam();
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Warn(ex2);
			}
		}

		private void SendStateStrucAddress(bool isReloadState)
		{
			try
			{
				ucPlusInfo1.GetStateChangeStrucAddess(isReloadState);
				ucPatientRaw1.GetStateChangeStrucAddess(isReloadState);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SendCardSDO(HisCardSDO cardSDO)
		{
			try
			{
				ucPatientRaw1.GetCardSDO(cardSDO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void Send3WCode(string code)
		{
			try
			{
				ucPatientRaw1.Set3WBhytCode(code);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusToUCAddressCombo()
		{
			try
			{
				ucAddressCombo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusToUCHeinInfo()
		{
			try
			{
				ucHeinInfo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusToUCServiceRoomInfo()
		{
			try
			{
				ucServiceRoomInfo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusToUCOtherServiceReqInfo()
		{
			try
			{
				ucOtherServiceReqInfo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusToUCPersonHomeInfo()
		{
			try
			{
				ucRelativeInfo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusToUCImageInfo()
		{
			try
			{
				ucImageInfo1.FocusUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusOutUserControlImageInfo(object obj)
		{
			try
			{
				if (btnSave.Enabled)
				{
					btnSave.Focus();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusToBtnSave()
		{
			try
			{
				btnSave.Focus();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void focusToBtnSaveAndPrint()
		{
			btnSaveAndPrint.Focus();
		}

		private void SetDelegateFocusWhenPatientIsChild(bool _isPatientChild)
		{
			try
			{
				if (_isPatientChild)
				{
					ucRelativeInfo1.FocusNextUserControl(new DelegateFocusNextUserControl(focusToUCAddressCombo));
				}
				else
				{
					ucRelativeInfo1.FocusNextUserControl(new DelegateFocusNextUserControl(focusToUCOtherServiceReqInfo));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void EnableOrDisablechkTheTam()
		{
			try
			{
				DelegateEnableOrDisableControl isEnable = new DelegateEnableOrDisableControl(ucHeinInfo1.ChangeCheckCardTemp);
				ucPatientRaw1.EnableOrDisableControl(isEnable);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ValidationRelative()
		{
			try
			{
				DelegateValidationUserControl isValidate = new DelegateValidationUserControl(ucRelativeInfo1.SetValidateControl);
				ucPatientRaw1.ValidateRelative(isValidate);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void EnableLciBenhNhanMoiAfterSearchPatient()
		{
			try
			{
				DelegateEnableOrDisableBtnPatientNew enableControl = new DelegateEnableOrDisableBtnPatientNew(EnableLciBenhNhanMoi);
				ucPatientRaw1.EnableOrDisableBtnPatientNew(enableControl);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void EnableLciBenhNhanMoi(bool _enableLciBenhNhanMoi)
		{
			try
			{
				lcibtnPatientNewInfo.Enabled = _enableLciBenhNhanMoi;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void IsVisibleUCHein(long patientTypeID)
		{
			try
			{
				if (patientTypeID > 0)
				{
					SuspendLayoutWithPatientTypeChanged(patientTypeID);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetTypeCardTemp(long patientTypeID)
		{
			try
			{
				ucHeinInfo1.SetTypeCardTemp(patientTypeID);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDisableBtnTTCT(bool _isDisable)
		{
			try
			{
				if (!_isDisable)
				{
					transPatiADO = null;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDelegateCheckTT()
		{
			try
			{
				ucPatientRaw1.SetDelegateCheckTT(new DelegateCheckTT(CheckTTFull));
				ucHeinInfo1.SetDelegateCheckTT(new DelegateCheckTT(CheckTTFull));
				ucPatientRaw1.SetIsReadQrCode(new Action<bool>(SetIsReadQrCode));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDelegateEnableButtonSave()
		{
			try
			{
				ucPatientRaw1.SetDelegateEnableButtonSave(new DelegateEnableButtonSave(EnableSave));
				ucPatientRaw1.SetDelegateHeinEnableButtonSave(new DelegateHeinEnableButtonSave(HeinEnableSave));
				ucHeinInfo1.SetDelegateEnableButtonSave(new Action<bool>(EnableSave));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void EnableSave(bool isEnable)
		{
			try
			{
				IsRunDelegateEnableSave = true;
				IsEnablePatientKey = isEnable;
				btnSave.Enabled = IsEnablePatientKey;
				btnSaveAndPrint.Enabled = IsEnablePatientKey;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void HeinEnableSave(bool isEnable)
		{
			try
			{
				if (!IsRunDelegateEnableSave)
				{
					IsEnablePatientKey = true;
				}
				btnSave.Enabled = IsEnablePatientKey && isEnable;
				btnSaveAndPrint.Enabled = IsEnablePatientKey && isEnable;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetIsReadQrCode(bool _isReadQrCode)
		{
			try
			{
				isReadQrCode = _isReadQrCode;
				ucAddressCombo1.isReadCard = _isReadQrCode;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetDelegateSetAddressUCHein(string address)
		{
			try
			{
				ucHeinInfo1.SetValueAddress(address);
				ucRelativeInfo1.SetValueAddress(address);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SendCodeProvince(string codeProvince)
		{
			try
			{
				ucHeinInfo1.SetCodeProvince(codeProvince);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDelegateSetAddressUCPlusInfo(string address)
		{
			try
			{
				ucPlusInfo1.SetAddressNow(address);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDelegateSetAddressUCProvinceOfBirth(object data, bool isCallByUCAddress)
		{
			try
			{
				ucPlusInfo1.SetDataAddress(data, isCallByUCAddress);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDelegateSetWorkPlace(object data)
		{
			try
			{
				ucPlusInfo1.setWorkPlace(data);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetEnableEmergency(bool isEnable)
		{
			try
			{
				ucOtherServiceReqInfo1.SetEnableControl(isEnable);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetCareerByCardNumber(string heinCardNumber)
		{
			try
			{
				ucPatientRaw1.SetCareerByCardNumber(heinCardNumber);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDelegateForResetRegister()
		{
			try
			{
				ucPatientRaw1.SetDelegateForResetRegisterForm(new Action(RefreshUserControl));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetCurrentModuleForUCWorkPlace()
		{
			try
			{
				ucPlusInfo1.SetCurrentModuleAgain(currentModule);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetRightRouteEmergencyWhenRegisterOutTime(bool _isOutTime)
		{
			try
			{
				ucHeinInfo1.RightRouteEmergencyWhenRegisterOutTime(_isOutTime);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetEmergenceForRoom(bool _isEmer)
		{
			try
			{
				ucServiceRoomInfo1.getIsEmergencyChecked(_isEmer);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetServuceRoomAddButtonWhenRegisterHasPriorityNumber(long? priorityNumber)
		{
			try
			{
				ucServiceRoomInfo1.PriorityChanged(priorityNumber);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataAfterSaerchPatientInUCPatientRaw(DataResultADO dt)
		{
			try
			{
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => dt), dt));
				CommonParam commonParam = new CommonParam();
				HeinCardData heinCardData = dt.HeinCardData;
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => heinCardData), heinCardData));
				LogSystem.Debug("FillDataAfterSaerchPatientInUCPatientRaw.1");
				UCPatientRawADO ucPatientRawData = ucPatientRaw1.GetValue();
				if (heinCardData != null && !string.IsNullOrEmpty(heinCardData.HeinCardNumber) && ucPatientRawData != null && (ucPatientRawData.PATIENTTYPE_ID == 0L || (ucPatientRawData.PATIENTTYPE_ID != HisConfigCFG.PatientTypeId__BHYT && ucPatientRawData.PATIENTTYPE_ID != HisConfigCFG.PatientTypeId__QN)) && !ucPatientRawData.IsReadQrCccd)
				{
					LogSystem.Debug("FillDataAfterSaerchPatientInUCPatientRaw.2");
					LogSystem.Debug("Trường hợp đối tượng BN đang chọn không phải là đối tượng BHYT, người dùng nhập qrcode để tìm kiếm ==>tự động gán đối tượng thanh toán mặc định là đối tượng BHYT," + LogUtil.TraceData(LogUtil.GetMemberName(() => ucPatientRawData.PATIENTTYPE_ID), ucPatientRawData.PATIENTTYPE_ID));
					ucPatientRaw1.SetValuePatientType(HisConfigCFG.PatientTypeId__BHYT);
					ucPatientRawData = ucPatientRaw1.GetValue();
				}
				ucPatientRaw1.InitEthnic();
				if (ucPatientRawData != null && ucPatientRawData.PATIENTTYPE_ID > 0 && (ucPatientRawData.PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT || ucPatientRawData.PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__QN))
				{
					LogSystem.Debug("FillDataAfterSaerchPatientInUCPatientRaw.3");
					ucHeinInfo1.FillDataByHeinCardData(heinCardData);
					LogSystem.Debug("FillDataAfterSaerchPatientInUCPatientRaw.4");
				}
				ucAddressCombo1.SetValue(null);
				if (AppConfigs.CheDoTuDongFillDuLieuDiaChiGhiTrenTheVaoODiaChiBenhNhanHayKhong == 1 || dt.HisPatientSDO == null || !dt.HisPatientSDO.TreatmentId.HasValue || dt.HisPatientSDO.TreatmentId <= 0)
				{
					LogSystem.Debug("FillDataAfterSaerchPatientInUCPatientRaw.5");
					dataAddressPatient = ucAddressCombo1.GetValue() ?? new UCAddressADO();
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => dataAddressPatient), dataAddressPatient));
					AddressProcessor addressProcessor = new AddressProcessor(BackendDataWorker.Get<V_SDA_PROVINCE>(), BackendDataWorker.Get<V_SDA_DISTRICT>(), BackendDataWorker.Get<V_SDA_COMMUNE>());
					AddressADO addressADO = addressProcessor.SplitFromFullAddress(heinCardData.Address);
					if (addressADO != null)
					{
						dataAddressPatient.Province_Code = addressADO.ProvinceCode;
						dataAddressPatient.Province_Name = addressADO.ProvinceName;
						dataAddressPatient.District_Code = addressADO.DistrictCode;
						dataAddressPatient.District_Name = addressADO.DistrictName;
						dataAddressPatient.Commune_Code = addressADO.CommuneCode;
						dataAddressPatient.Commune_Name = addressADO.CommuneName;
						dataAddressPatient.IsNoDistrict = addressADO.IsNoDistrict;
					}
					dataAddressPatient.Address = heinCardData.Address;
					if (dt.HisPatientSDO != null && dt.HisPatientSDO.PHONE != null)
					{
						dataAddressPatient.Phone = dt.HisPatientSDO.PHONE;
					}
					ucAddressCombo1.SetValue(dataAddressPatient);
					LogSystem.Debug("FillDataAfterSaerchPatientInUCPatientRaw.6");
				}
				else if (dt.HisPatientSDO != null && dt.HisPatientSDO.TreatmentId.HasValue && dt.HisPatientSDO.TreatmentId > 0 && AppConfigs.CheDoTuDongFillDuLieuDiaChiGhiTrenTheVaoODiaChiBenhNhanHayKhong == 2)
				{
					HisTreatmentFilter hisTreatmentFilter = new HisTreatmentFilter();
					hisTreatmentFilter.ID = dt.HisPatientSDO.TreatmentId;
					List<HIS_TREATMENT> source = new BackendAdapter(commonParam).Get<List<HIS_TREATMENT>>("api/HisTreatment/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, hisTreatmentFilter, commonParam);
					HIS_TREATMENT latestTreatment = (from t in source
						where t.PATIENT_ID == currentPatientSDO.ID && !string.IsNullOrEmpty(t.TDL_PATIENT_ADDRESS)
						orderby t.IN_TIME descending
						select t).FirstOrDefault();
					LogSystem.Debug("FillDataAfterSaerchPatientInUCPatientRaw.6.5");
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => latestTreatment), latestTreatment));
					if (latestTreatment != null)
					{
						dataAddressPatient.Province_Code = latestTreatment.TDL_PATIENT_PROVINCE_CODE;
						dataAddressPatient.Province_Name = latestTreatment.TDL_PATIENT_PROVINCE_NAME;
						dataAddressPatient.District_Code = latestTreatment.TDL_PATIENT_DISTRICT_CODE;
						dataAddressPatient.District_Name = latestTreatment.TDL_PATIENT_DISTRICT_NAME;
						dataAddressPatient.Commune_Code = latestTreatment.TDL_PATIENT_COMMUNE_CODE;
						dataAddressPatient.Commune_Name = latestTreatment.TDL_PATIENT_COMMUNE_NAME;
						dataAddressPatient.Phone = latestTreatment.TDL_PATIENT_PHONE;
						dataAddressPatient.Address = latestTreatment.TDL_PATIENT_ADDRESS;
					}
					ucAddressCombo1.SetValue(dataAddressPatient);
				}
				if (ucOtherServiceReqInfo1 != null)
				{
					ucOtherServiceReqInfo1.RefreshUserControl();
				}
				chkBaoLanh.Checked = false;
				LogSystem.Debug("FillDataAfterSaerchPatientInUCPatientRaw.7");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDefaultFocusUserControl()
		{
			try
			{
				if (ucPatientRaw1.cboPatientType.EditValue != null)
				{
					ucHeinInfo1.FocusUserControl();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FocusNextControlUCHein()
		{
			try
			{
				if (ucServiceRoomInfo1 != null)
				{
					ucServiceRoomInfo1.FocusUserControl();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private async Task InitInputDataUCHeinInfo()
		{
			try
			{
				DataInitUCHeniInfo dataInitUCHeniInfo = new DataInitUCHeniInfo
				{
					dlgGetIsChild = new GetIsChild(GetIsChild),
					PATIENT_TYPE_ID__BHYT = HisConfigCFG.PatientTypeId__BHYT,
					isVisibleControl = AppConfigs.TiepDon_HienThiMotSoThongTinThemBenhNhan,
					IsShowCheckKhongKTHSD = HisConfigCFG.IsShowCheckExpired,
					IsDefaultRightRouteType = (HisConfigCFG.IsDefaultRightRouteType == "1"),
					AlertExpriedTimeHeinCardBhyt = AppConfigs.AlertExpriedTimeHeinCardBhyt,
					dlgFillDataPatientSDOToRegisterForm = new HIS.UC.UCHeniInfo.FillDataPatientSDOToRegisterForm(FillDataIntoFormBySearchCardInUcHein),
					dlgFocusNextUserControl = new DelegateFocusNextUserControl(FocusNextControlUCHein),
					dlgAutoCheckCC = new HIS.UC.UCHeniInfo.DelegateAutoCheckCC(AutoSetCheckCC),
					dlgCheckExamHistory = new HIS.UC.UCHeniInfo.CheckExamHistoryByHeinCardNumber(CheckHeinCardByServerBhxh),
					dlgProcessFillDataCareerUnder6AgeByHeinCardNumber = null,
					UpdateTranPatiDataByPatientOld = new UpdateTranPatiDataByPatientOld(UpdateTranPatiDataByPatientOld),
					dlgCheckSS = new HIS.UC.UCHeniInfo.DelegateCheckSS(UpdateCheckSS)
				};
				ucHeinInfo1.InitInputData(dataInitUCHeniInfo);
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Warn(ex2);
			}
		}

		private void UpdateCheckSS(bool isCheck)
		{
			try
			{
				isCheckSS = isCheck;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataIntoUCHeinInfo(HisPatientSDO patient)
		{
			try
			{
				if (IsPatientTypeUsingHeinInfo())
				{
					ucHeinInfo1.SetValue(patient);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataIntoUCHeinInfoByPatientTypeAlter(HisPatientSDO patientSDO)
		{
			try
			{
				if (IsPatientTypeUsingHeinInfo())
				{
					HIS_PATIENT_TYPE_ALTER hIS_PATIENT_TYPE_ALTER = new HIS_PATIENT_TYPE_ALTER();
					hIS_PATIENT_TYPE_ALTER.HEIN_CARD_FROM_TIME = patientSDO.HeinCardFromTime;
					hIS_PATIENT_TYPE_ALTER.HEIN_CARD_NUMBER = patientSDO.HeinCardNumber;
					hIS_PATIENT_TYPE_ALTER.HEIN_CARD_TO_TIME = patientSDO.HeinCardToTime;
					hIS_PATIENT_TYPE_ALTER.HEIN_MEDI_ORG_CODE = patientSDO.HeinMediOrgCode;
					hIS_PATIENT_TYPE_ALTER.HEIN_MEDI_ORG_NAME = patientSDO.HeinMediOrgName;
					hIS_PATIENT_TYPE_ALTER.JOIN_5_YEAR = patientSDO.Join5Year;
					hIS_PATIENT_TYPE_ALTER.PAID_6_MONTH = patientSDO.Paid6Month;
					hIS_PATIENT_TYPE_ALTER.RIGHT_ROUTE_CODE = patientSDO.RightRouteCode;
					hIS_PATIENT_TYPE_ALTER.RIGHT_ROUTE_TYPE_CODE = patientSDO.RightRouteTypeCode;
					hIS_PATIENT_TYPE_ALTER.LIVE_AREA_CODE = patientSDO.LiveAreaCode;
					if (ucHeinInfo1 != null)
					{
						ucHeinInfo1.SetValueByPatientTypeAlter(hIS_PATIENT_TYPE_ALTER);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn("Tiep don: UCRegistor/FillDataIntoUCHeinInfoByPatientTypeAlter:\n" + ((ex != null) ? ex.ToString() : null));
			}
		}

		private bool FillDataIntoFormBySearchCardInUcHein(HisPatientSDO patient)
		{
			bool result = true;
			try
			{
				if (!ucPatientRaw1.AlertTreatmentInOutInDayForTreatmentMessage(patient))
				{
					currentPatientSDO = null;
					isReadQrCode = true;
					RefreshUserControl();
					return false;
				}
				_HeinCardData = ConvertFromPatientData(patient);
				actionType = 1;
				SetPatientSearchPanel(true);
				currentPatientSDO = patient;
				FillDataToExamServiceReqNewestByPatient(currentPatientSDO);
				FillDataPatientRawInfo(currentPatientSDO);
				FillDataIntoUCPlusInfo(currentPatientSDO);
				FillDataIntoUCRelativeInfo(currentPatientSDO);
				FillDataIntoUCAddressInfo(new DataResultADO
				{
					HisPatientSDO = currentPatientSDO
				});
				PeriosTreatmentMessage();
				if (AppConfigs.DangKyTiepDonHienThiThongBaoTimDuocBenhNhan == 1)
				{
					XtraMessageBox.Show(ResourceMessage.TimDuocMotBenhNhanTheoThongTinNguoiDungNhapNeuKhongPhaiBNCuVuiLongNhanNutBNMoi, ResourceMessage.TieuDeCuaSoThongBaoLaThongBao, DefaultBoolean.True);
				}
				chkBaoLanh.Checked = false;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private void AutoSetCheckCC(bool value)
		{
			try
			{
				UCServiceReqInfoADO value2 = ucOtherServiceReqInfo1.GetValue();
				value2.IsEmergency = value;
				ucOtherServiceReqInfo1.SetValue(value2);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private async Task<object> SearchByCode(string code)
		{
			try
			{
				new HisPatientSDO();
				if (string.IsNullOrEmpty(code))
				{
					throw new ArgumentNullException("code is null");
				}
				if (code.Length > 10 && code.Contains("|"))
				{
					isReadQrCode = true;
					return await GetDataQrCodeHeinCard(code);
				}
				CommonParam param = new CommonParam();
				HisPatientAdvanceFilter filter = new HisPatientAdvanceFilter
				{
					PATIENT_CODE__EXACT = string.Format("{0:0000000000}", System.Convert.ToInt64(code))
				};
				return new BackendAdapter(param).Get<List<HisPatientSDO>>("api/HisPatient/GetSdoAdvance", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, filter, new Action(SessionManager.ActionLostToken), param).SingleOrDefault();
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Error(ex2);
			}
			return null;
		}

		private async Task<HeinCardData> GetDataQrCodeHeinCard(string qrCode)
		{
			HeinCardData dataHein = null;
			try
			{
				ReadQrCodeHeinCard readQrCode = new ReadQrCodeHeinCard();
				dataHein = readQrCode.ReadDataQrCode(qrCode);
				BhytHeinProcessor _BhytHeinProcessor = new BhytHeinProcessor();
				if (!_BhytHeinProcessor.IsValidHeinCardNumber(dataHein.HeinCardNumber))
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				LogSystem.Warn(ex2);
			}
			return dataHein;
		}

		private void FillDataAfterFindQrCodeNoExistsCard(HeinCardData dataHein)
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				string text = Inventec.Common.String.Convert.HexToUTF8Fix(dataHein.PatientName);
				value.PATIENT_NAME = (string.IsNullOrEmpty(text) ? dataHein.PatientName : text);
				if (!string.IsNullOrEmpty(dataHein.Dob))
				{
					if (dataHein.Dob.Length == 4)
					{
						isNotPatientDayDob = true;
						value.DOB = long.Parse(dataHein.Dob);
						value.DOB_STR = dataHein.Dob;
					}
					else
					{
						value.DOB = long.Parse(dataHein.Dob);
						value.DOB_STR = Inventec.Common.DateTime.Convert.TimeNumberToDateString(value.DOB);
					}
				}
				if (!string.IsNullOrEmpty(dataHein.Gender))
				{
					long dataGenderId = GenderConvert.HeinToHisNumber(dataHein.Gender);
					HIS_GENDER hIS_GENDER = BackendDataWorker.Get<HIS_GENDER>().FirstOrDefault((HIS_GENDER o) => o.ID == dataGenderId);
					if (hIS_GENDER != null)
					{
						value.GENDER_ID = hIS_GENDER.ID;
					}
				}
				CalulatePatientAge(value.DOB, false);
				SetValueCareerComboByCondition();
				string text2 = Inventec.Common.String.Convert.HexToUTF8Fix(dataHein.Address);
				if (mainHeinProcessor != null && ucHeinBHYT != null)
				{
					mainHeinProcessor.FillDataAfterFindQrCode(ucHeinBHYT, dataHein);
				}
				LogSystem.Error("FillDataAfterFindQrCodeNoExistsCard");
				value.PATIENT_CODE = "";
				ucPatientRaw1.SetValue(value);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void CalulatePatientAge(long strDob, bool isHasReset)
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				value.DOB = strDob;
				long dOB = value.DOB;
				if (!(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB) != DateTime.MinValue))
				{
					return;
				}
				bool flag = true;
				DateTime value2 = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB).Value;
				long ticks = (DateTime.Now - value2).Ticks;
				if (ticks < 0)
				{
					value.DOB_STR = "";
					value.DOB = 4000000L;
					return;
				}
				DateTime dateTime = new DateTime(ticks);
				int num = dateTime.Year - 1;
				int num2 = dateTime.Month - 1;
				int num3 = dateTime.Day - 1;
				int hour = dateTime.Hour;
				int minute = dateTime.Minute;
				int second = dateTime.Second;
				flag = BhytPatientTypeData.IsChild(value2);
				if (num >= 7)
				{
					value.DOB = 1000000L;
					ucPatientRaw1.txtAge.Enabled = false;
					ucPatientRaw1.cboAge.Enabled = false;
					if (!flag)
					{
						ucPatientRaw1.txtAge.EditValue = DateTime.Now.Year - value2.Year;
					}
					else
					{
						ucPatientRaw1.txtAge.EditValue = num.ToString();
					}
				}
				else if (num > 0 && num < 7)
				{
					if (num == 6)
					{
						if (num2 > 0 || num3 > 0)
						{
							ucPatientRaw1.cboAge.EditValue = 1;
							ucPatientRaw1.txtAge.Enabled = false;
							ucPatientRaw1.cboAge.Enabled = false;
							if (!flag)
							{
								ucPatientRaw1.txtAge.EditValue = DateTime.Now.Year - value2.Year;
							}
							else
							{
								ucPatientRaw1.txtAge.EditValue = num.ToString();
							}
						}
						else
						{
							ucPatientRaw1.txtAge.EditValue = num * 12 - 1;
							ucPatientRaw1.cboAge.EditValue = 2;
							ucPatientRaw1.txtAge.Enabled = false;
							ucPatientRaw1.cboAge.Enabled = false;
						}
					}
					else
					{
						ucPatientRaw1.txtAge.EditValue = num * 12 + num2;
						ucPatientRaw1.cboAge.EditValue = 2;
						ucPatientRaw1.txtAge.Enabled = false;
						ucPatientRaw1.cboAge.Enabled = false;
					}
				}
				else if (num2 > 0)
				{
					ucPatientRaw1.txtAge.EditValue = num2.ToString();
					ucPatientRaw1.cboAge.EditValue = 2;
					ucPatientRaw1.txtAge.Enabled = false;
					ucPatientRaw1.cboAge.Enabled = false;
				}
				else if (num3 > 0)
				{
					ucPatientRaw1.txtAge.EditValue = num3.ToString();
					ucPatientRaw1.cboAge.EditValue = 3;
					ucPatientRaw1.txtAge.Enabled = false;
					ucPatientRaw1.cboAge.Enabled = false;
				}
				else
				{
					ucPatientRaw1.txtAge.EditValue = "";
					ucPatientRaw1.cboAge.EditValue = 4;
					ucPatientRaw1.txtAge.Enabled = true;
					ucPatientRaw1.cboAge.Enabled = false;
				}
				CheckEdit checkEdit = mainHeinProcessor.GetchkHasDobCertificate(ucHeinBHYT);
				if (!(checkEdit != null && checkEdit.Checked && flag) && mainHeinProcessor != null && ucHeinBHYT != null)
				{
					mainHeinProcessor.UpdateHasDobCertificateEnable(ucHeinBHYT, flag);
				}
				SetValidationByChildrenUnder6Years(flag, isHasReset);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetValueCareerComboByCondition()
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				HIS_CAREER hIS_CAREER = null;
				hIS_CAREER = (BhytPatientTypeData.IsChild(System.Convert.ToDateTime(value.DOB)) ? HisConfigCFG.CareerUnder6Age : ((DateTime.Now.Year - Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB).Value.Year > 18) ? HisConfigCFG.CareerBase : HisConfigCFG.CareerHS));
				if (hIS_CAREER != null && hIS_CAREER.ID > 0)
				{
					value.CARRER_ID = hIS_CAREER.ID;
					value.CARRER_CODE = hIS_CAREER.CAREER_CODE;
				}
				LogSystem.Error("SetValueCareerComboByCondition");
				ucPatientRaw1.SetValue(value);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void UCHeinProcessFillDataCareerUnder6AgeByHeinCardNumber(HeinCardData heinCard, bool isSearchHeinCardNumber)
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				if (heinCard != null && !string.IsNullOrEmpty(heinCard.HeinCardNumber))
				{
					FillDataCareerUnder6AgeByHeinCardNumber(heinCard.HeinCardNumber);
				}
				AutoCheckPriorityByPriorityType(Parse.ToInt64(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB).Value.ToString("yyyyMMdd") + "000000"), heinCard.HeinCardNumber);
				ucPatientRaw1.SetValue(value);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ChoiceTemplateHeinCard(string patientTypeCode, bool focusMoveOut)
		{
			try
			{
				UCPatientRawADO value = ucPatientRaw1.GetValue();
				LogSystem.Debug("t3.1: begin process ChoiceTemplateHeinCard");
				ucHeinBHYT = new UserControl();
				mainHeinProcessor = new MainHisHeinBhyt();
				if (patientTypeCode == HisConfigCFG.PatientTypeCode__BHYT || patientTypeCode == HisConfigCFG.PatientTypeCode__QN)
				{
					LogSystem.Debug("t3.1.1: set default data to control hein");
					DataInitHeinBhyt dataInitHeinBhyt = new DataInitHeinBhyt();
					dataInitHeinBhyt.BhytWhiteLists = BackendDataWorker.Get<HIS_BHYT_WHITELIST>();
					dataInitHeinBhyt.BhytBlackLists = BackendDataWorker.Get<HIS_BHYT_BLACKLIST>();
					dataInitHeinBhyt.Genders = BackendDataWorker.Get<HIS_GENDER>();
					dataInitHeinBhyt.Template = MainHisHeinBhyt.TEMPLATE__BHYT1;
					long dOB = value.DOB;
					if (value.DOB > 0)
					{
						dataInitHeinBhyt.IsChild = BhytPatientTypeData.IsChild(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(value.DOB).Value);
					}
					dataInitHeinBhyt.HEIN_LEVEL_CODE__CURRENT = HisHeinLevelCFG.HEIN_LEVEL_CODE__CURRENT;
					dataInitHeinBhyt.HeinRightRouteTypes = HeinRightRouteTypeStore.Get();
					dataInitHeinBhyt.Icds = BackendDataWorker.Get<HIS_ICD>();
					dataInitHeinBhyt.LiveAreas = HeinLiveAreaStore.Get();
					dataInitHeinBhyt.MEDI_ORG_CODE__CURRENT = HisMediOrgCFG.MEDI_ORG_VALUE__CURRENT;
					dataInitHeinBhyt.MEDI_ORG_CODES__ACCEPTs = HisMediOrgCFG.MEDI_ORG_CODES__ACCEPT;
					dataInitHeinBhyt.MediOrgs = BackendDataWorker.Get<HIS_MEDI_ORG>();
					dataInitHeinBhyt.PATIENT_TYPE_ID__BHYT = HisConfigCFG.PatientTypeId__BHYT;
					dataInitHeinBhyt.PatientTypes = BackendDataWorker.Get<HIS_PATIENT_TYPE>();
					if (patientTypeCode == HisConfigCFG.PatientTypeCode__QN && !string.IsNullOrEmpty(HisConfigCFG.CheckTempQN))
					{
						dataInitHeinBhyt.IsTempQN = HisConfigCFG.CheckTempQN.Contains(patientTypeCode);
					}
					dataInitHeinBhyt.TranPatiForms = BackendDataWorker.Get<HIS_TRAN_PATI_FORM>();
					dataInitHeinBhyt.TranPatiReasons = BackendDataWorker.Get<HIS_TRAN_PATI_REASON>();
					dataInitHeinBhyt.TREATMENT_TYPE_ID__EXAM = 1L;
					dataInitHeinBhyt.TreatmentTypes = BackendDataWorker.Get<HIS_TREATMENT_TYPE>();
					dataInitHeinBhyt.PatientTypeId = HisConfigCFG.PatientTypeId__BHYT;
					dataInitHeinBhyt.isVisibleControl = AppConfigs.TiepDon_HienThiMotSoThongTinThemBenhNhan;
					dataInitHeinBhyt.IsShowCheckKhongKTHSD = HisConfigCFG.IsShowCheckExpired;
					dataInitHeinBhyt.AutoCheckIcd = HisConfigCFG.AutoCheckIcd;
					dataInitHeinBhyt.IsDefaultRightRouteType = HisConfigCFG.IsDefaultRightRouteType == "1";
					dataInitHeinBhyt.SetFocusMoveOut = new SetFocusMoveOut(FocusDelegate);
					dataInitHeinBhyt.SetShortcutKeyDown = new SetShortcutKeyDown(ShortcutDelegate);
					dataInitHeinBhyt.AutoCheckCC = new His.UC.UCHein.DelegateAutoCheckCC(AutoSetCheckCC);
					dataInitHeinBhyt.ProcessFillDataCareerUnder6AgeByHeinCardNumber = new His.UC.UCHein.ProcessFillDataCareerUnder6AgeByHeinCardNumber(UCHeinProcessFillDataCareerUnder6AgeByHeinCardNumber);
					dataInitHeinBhyt.IsDungTuyenCapCuuByTime = _IsDungTuyenCapCuuByTime;
					dataInitHeinBhyt.IsObligatoryTranferMediOrg = HisConfigCFG.IsObligatoryTranferMediOrg;
					dataInitHeinBhyt.ActChangePatientDob = new Action(ProcessWhileChangeDOb);
					LogSystem.Debug("t3.1.2: uCMainHein init");
				}
				LogSystem.Debug("t3.2: end process ChoiceTemplateHeinCard");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FocusDelegate()
		{
			try
			{
				FocusInServiceRoomInfo();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ShortcutDelegate(Keys key)
		{
			try
			{
				switch (key)
				{
				case Keys.I:
					btnSaveAndPrint.PerformClick();
					break;
				case Keys.S:
					btnSave.PerformClick();
					break;
				case Keys.N:
					btnNewContinue.PerformClick();
					break;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void InitExamServiceRoom()
		{
			try
			{
				ServiceRoomInitADO serviceRoomInitADO = new ServiceRoomInitADO();
				serviceRoomInitADO.RoomId = currentModule.RoomId;
				serviceRoomInitADO.dlgGetPatientTypeId = new dlgGetPatientTypeId(GetPatientTypeId);
				serviceRoomInitADO.DelegateFocusNextUserControl = new DelegateFocusNextUserControl(FoucusMoveOutServiceRoomInfo);
				serviceRoomInitADO.DelegateFocusNextUserControlSurcharge = new DelegateFocusNextUserControl(FoucusMoveOutServiceRoomInfoSurcharge);
				serviceRoomInitADO.IsFocusCombo = HisConfigCFG.IsByPassTextBoxRoomCode;
				if (ucPatientRaw1.GetValue().PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT)
				{
					serviceRoomInitADO.RegisterPatientWithRightRouteBHYT = new Action(ProcessRegisterPatientWithRightRouteBHYT);
					serviceRoomInitADO.ChangeRoomNotEmergency = new Action(ProcessChangeRoomNotEmergency);
				}
				serviceRoomInitADO.dlgGetIntructionTime = new DelegateGetIntructionTime(GetIntructionTime);
				ucServiceRoomInfo1.InitForm(serviceRoomInitADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void InitExamServiceRoomByAppoimentTime(HisPatientSDO patientSDO)
		{
			try
			{
				LogSystem.Debug("InitExamServiceRoomByAppoimentTime.1");
				if (patientSDO != null && patientSDO.AppointmentTime.HasValue && patientSDO.AppointmentExamServiceId.HasValue && patientSDO.AppointmentExamRoomIds != null && patientSDO.AppointmentExamRoomIds.Count > 0)
				{
					V_HIS_SERE_SERV sereServExamForAppoiment = new V_HIS_SERE_SERV();
					sereServExamForAppoiment.SERVICE_ID = patientSDO.AppointmentExamServiceId.Value;
					sereServExamForAppoiment.TDL_EXECUTE_ROOM_ID = patientSDO.AppointmentExamRoomIds.First();
					ucServiceRoomInfo1.InitExamServiceRoomByAppoiment(sereServExamForAppoiment, patientSDO);
					if (Inventec.Common.DateTime.Convert.TimeNumberToDateString(patientSDO.AppointmentTime.Value) != Inventec.Common.DateTime.Convert.SystemDateTimeToDateString(DateTime.Now))
					{
						List<V_HIS_EXECUTE_ROOM> list = BackendDataWorker.Get<V_HIS_EXECUTE_ROOM>();
						V_HIS_EXECUTE_ROOM v_HIS_EXECUTE_ROOM = ((list != null && list.Count > 0) ? list.Where((V_HIS_EXECUTE_ROOM t) => t.ROOM_ID == sereServExamForAppoiment.TDL_EXECUTE_ROOM_ID).FirstOrDefault() : null);
						string message = string.Format(ResourceMessage.BenhNhanCoHenKhamVaoNgayTaiPhongKhamY, Inventec.Common.DateTime.Convert.TimeNumberToDateString(patientSDO.AppointmentTime.Value), (v_HIS_EXECUTE_ROOM != null) ? v_HIS_EXECUTE_ROOM.EXECUTE_ROOM_NAME : "");
						LogSystem.Debug(message);
						XtraMessageBox.Show(message);
					}
				}
				LogSystem.Debug("InitExamServiceRoomByAppoimentTime.2");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessRegisterPatientWithRightRouteBHYT()
		{
			try
			{
				if (ucHeinInfo1 != null)
				{
					ucHeinInfo1.RightRouteEmergencyWhenRegisterOutTime(true);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessChangeRoomNotEmergency()
		{
			try
			{
				if (ucHeinInfo1 != null)
				{
					ucHeinInfo1.ChangeRoomNotEmergency();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FoucusMoveOutServiceRoomInfo()
		{
			try
			{
				if ((HisConfigCFG.IsSetPrimaryPatientType == "1" || HisConfigCFG.IsSetPrimaryPatientType == "2" || HisConfigCFG.IsSetPrimaryPatientType == "3") && HisConfigCFG.PrimaryPatientTypeByService == "1")
				{
					ucServiceRoomInfo1.FocusSurcharge_frmServiceRoom();
				}
				else if (HisConfigCFG.IsAutoFocusToSavePrintAfterChoosingExam)
				{
					focusToBtnSaveAndPrint();
				}
				else
				{
					focusToBtnSave();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FoucusMoveOutServiceRoomInfoSurcharge()
		{
			try
			{
				if (HisConfigCFG.IsAutoFocusToSavePrintAfterChoosingExam)
				{
					focusToBtnSaveAndPrint();
				}
				else
				{
					focusToBtnSave();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ReloadExamServiceRoom()
		{
			try
			{
				ServiceRoomInitADO serviceRoomInitADO = new ServiceRoomInitADO();
				serviceRoomInitADO.dlgGetPatientTypeId = new dlgGetPatientTypeId(GetPatientTypeId);
				serviceRoomInitADO.RoomId = currentModule.RoomId;
				serviceRoomInitADO.DelegateFocusNextUserControl = new DelegateFocusNextUserControl(FoucusMoveOutServiceRoomInfo);
				serviceRoomInitADO.DelegateFocusNextUserControlSurcharge = new DelegateFocusNextUserControl(FoucusMoveOutServiceRoomInfoSurcharge);
				serviceRoomInitADO.IsFocusCombo = HisConfigCFG.IsByPassTextBoxRoomCode;
				if (ucPatientRaw1.GetValue().PATIENTTYPE_ID == HisConfigCFG.PatientTypeId__BHYT)
				{
					serviceRoomInitADO.RegisterPatientWithRightRouteBHYT = new Action(ProcessRegisterPatientWithRightRouteBHYT);
				}
				serviceRoomInitADO.dlgGetIntructionTime = new DelegateGetIntructionTime(GetIntructionTime);
				ucServiceRoomInfo1.ReloadExamServiceRoom(serviceRoomInitADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void FillDataToExamServiceReqByPatient(HisPatientSDO data)
		{
			try
			{
				if (data == null)
				{
					throw new ArgumentNullException("FillDataToExamServiceReqByPatient. Get HisPatientSDO is null");
				}
				if (AppConfigs.IsAutoFillDataRecentServiceRoom == "1" && ucServiceRoomInfo1 != null && ucServiceRoomInfo1 != null)
				{
					V_HIS_PATIENT v_HIS_PATIENT = new V_HIS_PATIENT();
					DataObjectMapper.Map<V_HIS_PATIENT>(v_HIS_PATIENT, data);
					ucServiceRoomInfo1.SetValueExamServiceRoom(v_HIS_PATIENT);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private bool DeleteServiceRoomInfo(object LayoutControlItemName)
		{
			bool result = false;
			try
			{
				if (LayoutControlItemName != null)
				{
					foreach (Control control in ucHeinInfo1.Controls)
					{
						if (control != null && (control is UserControl || control is XtraUserControl) && control.Name == (string)LayoutControlItemName)
						{
							ucHeinInfo1.Controls.Remove(control);
							control.Dispose();
							GC.Collect();
							GC.WaitForPendingFinalizers();
							GC.Collect();
							result = true;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}
	}
}
