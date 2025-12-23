using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using DevExpress.Images;
using DevExpress.Pdf;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraPdfViewer.Bars;
using DevExpress.XtraRichEdit;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using EMR.SDO;
using EMR.TDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignFile;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.CacheClient;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.FingerPrint;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;
using Inventec.Common.SignLibrary.SignBoard;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Inventec.Common.SignLibrary
{
	public class UCViewer : UserControl
	{
		private string resultFileStore = "";

		private string currentFileWorking = "";

		private Stream currentStream;

		private string inputFileWork = "";

		private Stream inputStream;

		private bool mouseButtonPressed = false;

		private PdfDocumentPosition startPosition;

		private PdfDocumentPosition endPosition;

		private bool isShowImage = false;

		private bool isShowRangtax = true;

		private const float dpi = 72f;

		private const int lcStep = 10;

		private PointF startPoint1;

		private PointF endPoint1;

		private float xImg;

		private float yImg;

		private List<VerifierADO> verifiers;

		private PdfReader readerWorking;

		private Action<bool> closeAfterSign;

		private Action<bool> dlgCloseAfterSignCheckedChanged;

		private Action<bool> dlgOptionSignTypeCheckedChanged;

		private bool? isCloseAfterSign = false;

		private bool? isOptionSignType = false;

		private bool isInitForm = false;

		private Action<float, float> dlgChoosePoint;

		private Action<DocumentSignedUpdateIGSysResultDTO> dlgSendResultSigned;

		private bool isSelectRangeRectangle;

		private List<string> watermarks;

		private int pageNumberCurrent = 0;

		private int totalPageNumber = 0;

		private SignType signType;

		private TreatmentDTO treatment;

		private string treatmentCode;

		private string signName;

		private string signReason;

		private string documentName;

		private string businessCode;

		private List<string> printTypeBusinessCodes;

		private string roomCode;

		private string roomTypeCode;

		private DocumentTDO currentDocument;

		private Action<DocumentTDO> dlgOpenModuleConfig;

		private InputADO inputADOWorking;

		private List<SignTDO> listSign;

		private bool isSigning;

		private string rejectReason;

		private EMR_SIGN signSelected;

		private EMR_SIGN signSelectedByUser;

		private bool isMultiSign;

		private bool isMultiSignByType = false;

		private bool isPatientSign;

		private bool isHomeRelativeSign;

		private bool isPrintOnlyContent;

		private long? DocumentTypeId;

		private string hisCode;

		private List<SignPositionADO> signPositionADOs;

		private List<SignPositionADO> signAutoPositionADOs;

		private int signedCount;

		private bool hasNextSignPosition;

		private SignPositionADO nextSignPosition;

		private bool isSignNow;

		private bool isPrintDocSignedNow;

		private Action<string> actionAfterSigned;

		private int plusNumberWithShowingSignInformation = 0;

		private short printNumberCopies;

		private int typeDisplayOption = -1;

		private PageSettings currentPageSettings;

		private string vlViewPACSUrlFormat = "";

		public bool IsLoadFirst = true;

		private const short IS_SIGN_ELECTRONIC_VALUE = 1;

		public int Widths;

		public int Heights;

		private FileType fileType;

		private Action<bool> actChangeUsingSignPad;

		private bool isUsingSignPad;

		private FileADO fileADOJson = null;

		private FileADO fileADOXml = null;

		private FileADO fileADOMain = null;

		private Action reload = null;

		private Pen penDrawSignal = new Pen(Color.Aqua);

		private Image image = null;

		private long total = 0L;

		private long sizeToSignAndDeskcription = 0L;

		private long sizeToRelativeHomeSign = 0L;

		private bool IsNotCaculateSignAndDeskcription = false;

		private bool IsNotCaculateRelativeHomeSign = false;

		private bool IsAddPatientSign = false;

		private IContainer components = null;

		private PdfViewer pdfViewer1;

		private BarManager barManager1;

		private PdfCommandBar pdfCommandBar1;

		private PdfFileOpenBarItem pdfFileOpenBarItem1;

		private PdfFileSaveAsBarItem pdfFileSaveAsBarItem1;

		private PdfFilePrintBarItem pdfFilePrintBarItem1;

		private PdfFindTextBarItem pdfFindTextBarItem1;

		private PdfPreviousPageBarItem pdfPreviousPageBarItem1;

		private PdfNextPageBarItem pdfNextPageBarItem1;

		private PdfZoomOutBarItem pdfZoomOutBarItem1;

		private PdfZoomInBarItem pdfZoomInBarItem1;

		private PdfExactZoomListBarSubItem pdfExactZoomListBarSubItem1;

		private PdfZoom10CheckItem pdfZoom10CheckItem1;

		private PdfZoom25CheckItem pdfZoom25CheckItem1;

		private PdfZoom50CheckItem pdfZoom50CheckItem1;

		private PdfZoom75CheckItem pdfZoom75CheckItem1;

		private PdfZoom100CheckItem pdfZoom100CheckItem1;

		private PdfZoom125CheckItem pdfZoom125CheckItem1;

		private PdfZoom150CheckItem pdfZoom150CheckItem1;

		private PdfZoom200CheckItem pdfZoom200CheckItem1;

		private PdfZoom400CheckItem pdfZoom400CheckItem1;

		private PdfZoom500CheckItem pdfZoom500CheckItem1;

		private PdfSetActualSizeZoomModeCheckItem pdfSetActualSizeZoomModeCheckItem1;

		private PdfSetPageLevelZoomModeCheckItem pdfSetPageLevelZoomModeCheckItem1;

		private PdfSetFitWidthZoomModeCheckItem pdfSetFitWidthZoomModeCheckItem1;

		private PdfSetFitVisibleZoomModeCheckItem pdfSetFitVisibleZoomModeCheckItem1;

		private PdfExportFormDataBarItem pdfExportFormDataBarItem1;

		private PdfImportFormDataBarItem pdfImportFormDataBarItem1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private PdfBarController pdfBarController1;

		private BarButtonItem bbtnPrint;

		private BarButtonItem bbtnSendERM;

		private BarButtonItem bbtnSign;

		private BarButtonItem bbtnPatientSign;

		private BarButtonItem bbtnConfigSign;

		private BarButtonItem bbtnRejectSign;

		private BarButtonItem bbtnSignEnd;

		private RepositoryItemCheckEdit repositoryItemCheckEdit1;

		private BarButtonItem bbtnListSign;

		private BarButtonItem bbtnResetChkState;

		private ImageList imageList1;

		private ImageCollection imageCollection1;

		private BarButtonItem btnViewPACSImage;

		private BarButtonItem bbtnCtrlShiftU;

		private BarCheckItem bbtnChkCloseAfterSign;

		private BarSubItem bbtnOther;

		private BarStaticItem bbtnConfigBussinessMenu;

		private BarStaticItem bbtnAttackMentsMenu;

		private BarButtonItem bbtnRelativeHomeSign;

		private BarButtonItem bbtnRelativeHomeSignOther;

		private BarButtonItem bbtnConfigBussinessMenu1;

		private BarButtonItem bbtnAttackMentsMenu1;

		private BarSubItem barSubItem1;

		private BarCheckItem bbtnSignType;

		private BarCheckItem barCheckUsingSignPad;

		private BarCheckItem bbtnchkSignParanel;

		private DockManager dockManager1;

		private DockPanel dockPanel1;

		private ControlContainer dockPanel1_Container;

		private LayoutControl layoutControl1;

		private RichEditControl txtSignDescriptionList;

		private MemoEdit txtSignDescription;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControlItem layoutControlItem2;

		private LayoutControlItem layoutControlItem3;

		private BarButtonItem bbtnSignAndDeskcription;

		private BarButtonItem bbtnSignAndDeskcriptionOther;

		private BarButtonItem bbtnConfigBussinessMenu2;

		private BarStaticItem barStaticItem1;

		private Timer timerLoadSinglePage;

		private List<EMR_SIGNER> signers { get; set; }

		private EMR_SIGNER Signer { get; set; }

		private EMR_TREATMENT Treatment { get; set; }

		private string TokenCode { get; set; }

		private V_EMR_DOCUMENT currentEmrDocument { get; set; }

		public EMR_RELATION Relation { get; set; }

		public string RelationPeopleName { get; set; }

		public UCViewer()
		{
			InitializeComponent();
		}

		private UCViewer(InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode)
		{
			//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b06: Expected O, but got Unknown
			//IL_0b86: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b8d: Expected O, but got Unknown
			UCViewer uCViewer = this;
			LogSystem.Debug("UCViewer.InitializeComponent.1");
			InitializeComponent();
			isInitForm = true;
			inputADOWorking = inputADO;
			Signer = signer;
			Treatment = treatment;
			TokenCode = tokenCode;
			if (inputADO != null)
			{
				watermarks = inputADO.Watermarks;
				signType = SignType.HMS;
				if (inputADO.SignType == SignType.OptionDefaultHsm || inputADO.SignType == SignType.OptionDefaultUsb)
				{
					bbtnSignType.Visibility = BarItemVisibility.Always;
					if (inputADO.IsOptionSignType.HasValue)
					{
						signType = (inputADO.IsOptionSignType.Value ? SignType.USB : SignType.HMS);
					}
					else if (inputADO.SignType == SignType.OptionDefaultUsb)
					{
						signType = SignType.USB;
					}
					else
					{
						signType = SignType.HMS;
					}
					bbtnSignType.Checked = signType == SignType.USB;
				}
				else
				{
					signType = inputADO.SignType;
				}
				LogSystem.Info("1__________");
				if (inputADO.IsUsingSignPad.HasValue && inputADO.IsUsingSignPad.Value)
				{
					isUsingSignPad = true;
					barCheckUsingSignPad.Checked = true;
				}
				actChangeUsingSignPad = inputADO.ActChangeUsingSignPad;
				Relation = inputADO.Relation;
				RelationPeopleName = inputADO.RelationPeopleName;
				dlgChoosePoint = inputADO.DlgChoosePoint;
				dlgSendResultSigned = inputADO.DlgSendResultSigned;
				isSelectRangeRectangle = inputADO.IsSelectRangeRectangle;
				dlgOpenModuleConfig = inputADO.DlgOpenModuleConfig;
				dlgCloseAfterSignCheckedChanged = inputADO.DlgCloseAfterSign;
				dlgOptionSignTypeCheckedChanged = inputADO.DlgChangeOptionSignType;
				this.treatment = inputADO.Treatment;
				isPrintOnlyContent = inputADO.IsPrintOnlyContent;
				plusNumberWithShowingSignInformation = ((!isPrintOnlyContent) ? 1 : 0);
				treatmentCode = ((inputADO.Treatment != null) ? inputADO.Treatment.TREATMENT_CODE : "");
				documentName = inputADO.DocumentName;
				signName = (string.IsNullOrEmpty(GlobalStore.UserName) ? GlobalStore.LoginName : (GlobalStore.LoginName + " (" + GlobalStore.UserName + ")"));
				hisCode = inputADO.HisCode;
				signReason = inputADO.SignReason;
				businessCode = inputADO.BusinessCode;
				printTypeBusinessCodes = inputADO.PrintTypeBusinessCodes;
				roomCode = inputADO.RoomCode;
				roomTypeCode = inputADO.RoomTypeCode;
				GlobalStore.IsUseTimespan = inputADO.IsUseTimespan;
				printNumberCopies = (short)((!inputADO.PrintNumberCopies.HasValue) ? 1 : inputADO.PrintNumberCopies.Value);
				isCloseAfterSign = inputADO.IsCloseAfterSign;
				isOptionSignType = inputADO.IsOptionSignType;
				reload = inputADO.reload;
				if (inputADO.IsReject)
				{
					bbtnRejectSign.Visibility = BarItemVisibility.Always;
				}
				LogSystem.Info("2__________");
				if (inputADO.IsSign)
				{
					bbtnSign.Visibility = BarItemVisibility.Always;
					bbtnConfigSign.Visibility = BarItemVisibility.Always;
					bbtnSendERM.Visibility = BarItemVisibility.Always;
					bbtnPatientSign.Visibility = BarItemVisibility.Always;
					bbtnSignAndDeskcription.Visibility = BarItemVisibility.Always;
					bbtnRelativeHomeSign.Visibility = BarItemVisibility.Always;
					bbtnPatientSign.Enabled = true;
				}
				if (inputADO.IsSignConfig.HasValue)
				{
					bbtnConfigSign.Visibility = ((!inputADO.IsSignConfig.Value) ? BarItemVisibility.Never : BarItemVisibility.Always);
					bbtnConfigBussinessMenu1.Visibility = ((!inputADO.IsSignConfig.Value) ? BarItemVisibility.Never : BarItemVisibility.Always);
				}
				else
				{
					bbtnConfigBussinessMenu1.Visibility = BarItemVisibility.Always;
				}
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<InputADO>((Expression<Func<InputADO>>)(() => inputADO)), (object)inputADO));
				bool isSignParanel = false;
				if (!string.IsNullOrEmpty(inputADO.DocumentTypeCode))
				{
					try
					{
						inputADO.DocumentTypeCode = string.Format("{0:00}", inputADO.DocumentTypeCode);
					}
					catch (Exception ex)
					{
						LogSystem.Warn(ex);
					}
					new EmrDocumentType().DocumentTypeProperty(inputADO.DocumentTypeCode, ref isMultiSignByType, ref isSignParanel, ref DocumentTypeId);
					isMultiSign = isMultiSignByType;
					if (inputADO.IsMultiSign.HasValue)
					{
						isMultiSign = isMultiSign && inputADO.IsMultiSign.Value;
					}
				}
				LogSystem.Info("3__________");
				currentDocument = ((!string.IsNullOrEmpty(inputADO.DocumentCode)) ? GenerateByDocumentCode(inputADO.DocumentCode) : null);
				if (currentDocument != null && !string.IsNullOrEmpty(currentDocument.DocumentCode))
				{
					if (inputADO.IsPrint)
					{
						bbtnPrint.Visibility = BarItemVisibility.Always;
						if (inputADO.IsEnableButtonPrint.HasValue)
						{
							bbtnPrint.Enabled = inputADO.IsEnableButtonPrint.Value;
						}
					}
					bbtnAttackMentsMenu1.Visibility = BarItemVisibility.Always;
					bbtnAttackMentsMenu1.Enabled = true;
					bbtnConfigBussinessMenu2.Enabled = false;
					bbtnSendERM.Visibility = BarItemVisibility.Never;
					bbtnListSign.Visibility = BarItemVisibility.Always;
					isSignParanel = currentDocument.IsSignParallel.HasValue && currentDocument.IsSignParallel.Value;
					bbtnchkSignParanel.Enabled = false;
					List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
					if (emrConfigs != null && emrConfigs.Count > 0)
					{
						IEnumerable<EMR_CONFIG> enumerable = emrConfigs.Where((EMR_CONFIG o) => o.KEY == "EMR.VIEW_PACS_URL_FORMAT");
						EMR_CONFIG val = ((enumerable != null) ? enumerable.FirstOrDefault() : null);
						if (val != null)
						{
							vlViewPACSUrlFormat = ((!string.IsNullOrEmpty(val.VALUE)) ? val.VALUE : val.DEFAULT_VALUE);
							if (!string.IsNullOrEmpty(vlViewPACSUrlFormat))
							{
								btnViewPACSImage.Visibility = BarItemVisibility.Always;
							}
						}
					}
				}
				else
				{
					bbtnchkSignParanel.Enabled = isSignParanel && inputADO.IsSign;
					bbtnPrint.Visibility = BarItemVisibility.Always;
					bbtnPrint.Enabled = true;
				}
				LogSystem.Info("4__________");
				if (inputADO.IsSign)
				{
					bbtnChkCloseAfterSign.Visibility = BarItemVisibility.Always;
				}
				bbtnchkSignParanel.Checked = isSignParanel;
				if (isCloseAfterSign.HasValue && isCloseAfterSign.Value)
				{
					bbtnChkCloseAfterSign.Checked = true;
				}
				bbtnSignEnd.Visibility = ((!isMultiSign) ? BarItemVisibility.Never : BarItemVisibility.Always);
				bbtnSignEnd.Enabled = false;
				dockPanel1.AllowDrop = false;
				txtSignDescriptionList.AllowDrop = false;
				LogSystem.Info("5__________");
				if (GlobalStore.EMR_SIGN_BOARD__OPTION == "2")
				{
					barCheckUsingSignPad.Visibility = BarItemVisibility.Always;
				}
				if (GlobalStore.EMR_SIGN_SIGN_DESCRIPTION_INFO == "2")
				{
					bbtnSignAndDeskcription.Visibility = BarItemVisibility.Never;
					if (dockPanel1.Visibility != DockVisibility.Visible)
					{
						dockPanel1.Visibility = DockVisibility.Visible;
					}
				}
				else
				{
					bbtnSignAndDeskcription.Visibility = BarItemVisibility.Always;
					dockPanel1.Visibility = DockVisibility.Hidden;
				}
				LogSystem.Info("6__________");
				if (!string.IsNullOrEmpty(inputADO.BusinessCode))
				{
					bbtnConfigSign.Visibility = BarItemVisibility.Never;
				}
				else if (inputADO.SignerConfigs != null && inputADO.SignerConfigs.Count > 0)
				{
					inputADO.SignerConfigs = inputADO.SignerConfigs.OrderBy((SignerConfigDTO o) => o.NumOrder).ToList();
					signers = (from o in new EmrSigner().Get(new EmrSignerFilter
						{
							LOGINNAMEs = inputADO.SignerConfigs.Select((SignerConfigDTO o) => o.Loginname).ToList()
						})
						orderby o.USERNAME, o.NUM_ORDER
						select o).ToList();
					listSign = new List<SignTDO>();
					foreach (SignerConfigDTO signerConfig in inputADO.SignerConfigs)
					{
						SignTDO val2 = new SignTDO();
						EMR_SIGNER signerByLoginname = GetSignerByLoginname(signerConfig.Loginname);
						if (signerByLoginname != null)
						{
							val2.SignerId = signerByLoginname.ID;
							val2.Loginname = signerByLoginname.LOGINNAME;
							val2.Username = signerByLoginname.USERNAME;
							val2.FullName = signerByLoginname.USERNAME;
							val2.FirstName = signerByLoginname.USERNAME;
							if (signerConfig.NumOrder > 0)
							{
								val2.NumOrder = signerConfig.NumOrder;
							}
							else
							{
								val2.NumOrder = GetMaxNumOrder();
							}
							val2.Title = signerByLoginname.TITLE;
							val2.DepartmentCode = signerByLoginname.DEPARTMENT_CODE;
							val2.DepartmentName = signerByLoginname.DEPARTMENT_NAME;
							val2.SignTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
							listSign.Add(val2);
						}
					}
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<List<SignTDO>>((Expression<Func<List<SignTDO>>>)(() => listSign)), (object)listSign));
				}
				LogSystem.Info("7__________");
				if (bbtnConfigBussinessMenu1.Visibility == BarItemVisibility.Never && bbtnAttackMentsMenu1.Visibility == BarItemVisibility.Never)
				{
					VisibleButonOrther(false);
				}
				else
				{
					VisibleButonOrther(true);
				}
				pdfViewer1.NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden;
				pdfViewer1.NavigationPaneVisibility = PdfNavigationPaneVisibility.Hidden;
				isInitForm = false;
			}
			if (GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION == "3" && GlobalStore.SIGN_CERTIFICATE_OPTION != "1")
			{
				isUsingSignPad = true;
				barCheckUsingSignPad.Checked = true;
			}
			LogSystem.Debug("UCViewer.InitializeComponent.2");
		}

		internal UCViewer(string inputFile, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode)
			: this(inputFile, FileType.Pdf, inputADO, signer, treatment, tokenCode, null, false)
		{
		}

		internal UCViewer(string inputFile, FileType fileType, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode, Action<string> _actionAfterSigned, bool _isSignNow, bool _isPrintDocSignedNow = false, Action<bool> _dlgCloseAfterSign = null)
			: this(inputADO, signer, treatment, tokenCode)
		{
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Expected O, but got Unknown
			this.fileType = fileType;
			isSignNow = _isSignNow;
			isPrintDocSignedNow = _isPrintDocSignedNow;
			actionAfterSigned = _actionAfterSigned;
			closeAfterSign = _dlgCloseAfterSign;
			if (!string.IsNullOrEmpty(inputFile))
			{
				if (fileType != FileType.Json && fileType != FileType.Xml)
				{
					string extByFileType = Utils.GetExtByFileType(fileType);
					Utils.ProcessFileInput(inputFile, extByFileType, ref inputFileWork);
					ProcessCommentKey();
					ProcessStoreCurrentFileToPrint(inputFileWork);
					readerWorking = new PdfReader(inputFileWork);
				}
				EnableSignButton(inputADO.IsSign);
				if (readerWorking != null)
				{
					ProcessSignPdf();
				}
			}
		}

		internal UCViewer(Stream inputStream, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode)
			: this(inputADO, signer, treatment, tokenCode)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			this.inputStream = inputStream;
			readerWorking = new PdfReader(inputStream);
			EnableSignButton(true);
			ProcessSignPdf();
		}

		internal UCViewer(byte[] inputByte, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode)
			: this(inputADO, signer, treatment, tokenCode)
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			fileType = FileType.Pdf;
			inputFileWork = Utils.GenerateTempFileWithin();
			Utils.ByteToFile(inputByte, inputFileWork);
			ProcessCommentKey();
			ProcessStoreCurrentFileToPrint(inputFileWork);
			readerWorking = new PdfReader(inputFileWork);
			EnableSignButton(inputADO.IsSign);
			ProcessSignPdf();
		}

		internal UCViewer(byte[] inputByte, FileType fileType, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode)
			: this(inputADO, signer, treatment, tokenCode)
		{
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			this.fileType = fileType;
			string extByFileType = Utils.GetExtByFileType(fileType);
			Utils.ProcessFileInput(inputByte, extByFileType, ref inputFileWork, inputADO.DocumentTypeCode);
			if (fileType != FileType.Json && fileType != FileType.Xml)
			{
				ProcessCommentKey();
				ProcessStoreCurrentFileToPrint(inputFileWork);
				readerWorking = new PdfReader(inputFileWork);
			}
			EnableSignButton(inputADO.IsSign);
			if (readerWorking != null)
			{
				ProcessSignPdf();
			}
		}

		internal UCViewer(byte[] inputByte, FileType fileType, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode, Action<string> _actionAfterSigned, bool _isSignNow, bool _isPrintDocSignedNow = false, Action<bool> _dlgCloseAfterSign = null)
			: this(inputByte, fileType, inputADO, signer, treatment, tokenCode)
		{
			isSignNow = _isSignNow;
			isPrintDocSignedNow = _isPrintDocSignedNow;
			actionAfterSigned = _actionAfterSigned;
			closeAfterSign = _dlgCloseAfterSign;
		}

		private void UCViewer1_Load(object sender, EventArgs e)
		{
			try
			{
				string vlState = CacheClientWorker.GetValue();
				if (!string.IsNullOrEmpty(vlState))
				{
					LogSystem.Info("Nguoi dung da luu lai trang thai cua lua chon khi vao th nguoi ky thieu anh chu ky & cau hinh EMR.EMR_SIGN.SIGN_DISPLAY_OPTION = 2" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => vlState)), (object)vlState));
				}
				ProcessBussinessCFG();
				ApplySignatureAppearanceFromConfigToInput();
				ProcessMemoryUsageuser();
				base.Disposed += DisposeVariable;
				currentPageSettings = PdfDocumentProcess.GetPaperSize(currentFileWorking);
				InitSignByDocument();
				pdfViewer1.QueryPageSettings += OnQueryPageSettings;
				pdfViewer1.Paint += pdfViewer1_Paint;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal void UpdateExtFileType(FileADO _fileADOMain, FileADO _fileADOJson, FileADO _fileADOXml)
		{
			fileADOMain = _fileADOMain;
			fileADOJson = _fileADOJson;
			fileADOXml = _fileADOXml;
		}

		internal void DisposeVariable(object sender, EventArgs e)
		{
			try
			{
				LogSystem.Debug("DisposeVariable.1");
				resultFileStore = "";
				currentFileWorking = "";
				signAutoPositionADOs = null;
				try
				{
					if (currentStream != null)
					{
						Utils.DisposeStream(currentStream);
					}
				}
				catch
				{
				}
				try
				{
					if (inputStream != null)
					{
						Utils.DisposeStream(inputStream);
					}
				}
				catch
				{
				}
				startPosition = null;
				endPosition = null;
				verifiers = null;
				if (readerWorking != null)
				{
					readerWorking.Close();
				}
				readerWorking = null;
				treatment = null;
				printTypeBusinessCodes = null;
				resultFileStore = "";
				currentFileWorking = "";
				inputFileWork = "";
				treatmentCode = null;
				signName = null;
				signReason = null;
				documentName = null;
				businessCode = null;
				dlgOpenModuleConfig = null;
				currentDocument = null;
				inputADOWorking = null;
				listSign = null;
				signSelected = null;
				signSelectedByUser = null;
				TokenCode = null;
				rejectReason = null;
				hisCode = null;
				signPositionADOs = null;
				nextSignPosition = null;
				signers = null;
				dlgSendResultSigned = null;
				dlgChoosePoint = null;
				dlgOptionSignTypeCheckedChanged = null;
				dlgCloseAfterSignCheckedChanged = null;
				closeAfterSign = null;
				pdfViewer1.QueryPageSettings -= OnQueryPageSettings;
				penDrawSignal.Dispose();
				if (image != null)
				{
					image.Dispose();
				}
				LogSystem.Debug("DisposeVariable.2");
				Dispose(true);
				LogSystem.Debug("DisposeVariable.3");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void VisibleButonOrther(bool visible)
		{
			bbtnConfigBussinessMenu1.Visibility = ((!visible) ? BarItemVisibility.Never : BarItemVisibility.Always);
			bbtnAttackMentsMenu1.Visibility = ((!visible) ? BarItemVisibility.Never : BarItemVisibility.Always);
		}

		private void InitSignByDocument()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			try
			{
				EmrDocumentTypeFilter val = new EmrDocumentTypeFilter();
				if (currentDocument != null && currentDocument.DocumentTypeId > 0)
				{
					((FilterBase)val).ID = currentDocument.DocumentTypeId;
				}
				if (inputADOWorking != null && !string.IsNullOrEmpty(inputADOWorking.DocumentTypeCode))
				{
					val.DOCUMENT_TYPE_CODE__EXACT = inputADOWorking.DocumentTypeCode;
				}
				List<EMR_DOCUMENT_TYPE> list = new EmrDocumentType().Get(val);
				if (list != null && list.Count > 0)
				{
					IsAddPatientSign = list.FirstOrDefault().PATIENT_MUST_SIGN == 1;
				}
				if (currentDocument != null && !string.IsNullOrEmpty(currentDocument.DocumentCode) && inputADOWorking.IsSign)
				{
					signSelectedByUser = new EmrSign().GetSignDocumentFirst(currentDocument.DocumentCode, Signer, Treatment, isMultiSign, true);
					signSelected = new EmrSign().GetSignDocumentFirst(currentDocument.DocumentCode, Signer, Treatment, isMultiSign, true);
					if (signSelected == null && inputADOWorking.IsShowPatientSign)
					{
						signSelected = new EmrSign().GetSignDocumentFirst(currentDocument.DocumentCode, null, Treatment, isMultiSign, true);
					}
					bbtnSignEnd.Enabled = signSelected != null && signSelected.IS_SIGNING == 1;
					if (signSelected != null && signSelected.FLOW_ID > 0)
					{
						bbtnConfigSign.Visibility = BarItemVisibility.Never;
					}
				}
				UpdateAfterAddSignThread(listSign);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void OnQueryPageSettings(object sender, PdfQueryPageSettingsEventArgs e)
		{
			try
			{
				Widths = (int)e.PageSize.Width;
				Heights = (int)e.PageSize.Height;
				currentPageSettings.PaperSize.Width = (int)e.PageSize.Width;
				currentPageSettings.PaperSize.Height = (int)e.PageSize.Height;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessMemoryUsageuser()
		{
			try
			{
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessStoreCurrentFileToPrint(string filename)
		{
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Expected O, but got Unknown
			try
			{
				if (!string.IsNullOrEmpty(inputADOWorking.DocumentCode) && !inputADOWorking.IsSign)
				{
					EMR_DOCUMENT byCode = new EmrDocument().GetByCode(inputADOWorking.DocumentCode);
					if (byCode != null && byCode.ATTACHMENT_COUNT > 0)
					{
						string text = Utils.GenerateTempFileWithin();
						EMR_VERSION signedDocumentLast = new EmrVersion().GetSignedDocumentLast(byCode.ID);
						MemoryStream streamSource = FssFileDownload.GetFile(byCode.LAST_VERSION_URL);
						streamSource.Position = 0L;
						LogSystem.Debug(LogUtil.TraceData("đây là dữ liệu: " + LogUtil.GetMemberName<long>((Expression<Func<long>>)(() => streamSource.Length)), (object)streamSource.Length));
						List<string> list = new List<string>();
						CommonParam commonParam = new CommonParam();
						EmrAttachmentFilter val = new EmrAttachmentFilter();
						val.DOCUMENT_ID = byCode.ID;
						((FilterBase)val).ORDER_DIRECTION = "DESC";
						((FilterBase)val).ORDER_FIELD = "ID";
						List<EMR_ATTACHMENT> list2 = new EmrAttachment().Get(val);
						if (list2 != null && list2.Count > 0)
						{
							list = list2.Select((EMR_ATTACHMENT o) => o.URL).ToList();
							PdfDocumentProcess.InsertPage(streamSource, list, text);
							filename = text;
							inputFileWork = text;
						}
					}
				}
				currentFileWorking = Utils.GenerateTempFileWithin();
				File.Copy(filename, currentFileWorking, true);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessCommentKey()
		{
			try
			{
				if (inputADOWorking == null || !inputADOWorking.IsSign)
				{
					return;
				}
				string outFile = Utils.GenerateTempFileWithin();
				PdfCommentKeyProcess pdfCommentKeyProcess = new PdfCommentKeyProcess();
				List<SignPositionADO> list = pdfCommentKeyProcess.Run(inputFileWork, ref outFile);
				if (list != null && list.Count > 0 && !string.IsNullOrEmpty(outFile) && File.Exists(outFile))
				{
					try
					{
						File.Delete(inputFileWork);
					}
					catch
					{
					}
					inputFileWork = outFile;
					if (signPositionADOs == null)
					{
						signPositionADOs = new List<SignPositionADO>();
					}
					signPositionADOs.AddRange(list);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessBussinessCFG()
		{
			try
			{
				if (string.IsNullOrEmpty(inputADOWorking.BusinessCode) && inputADOWorking.IsAutoChooseBusiness.HasValue && !inputADOWorking.IsAutoChooseBusiness.Value)
				{
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private EMR_SIGNER GetSignerByLoginname(string loginname)
		{
			return (signers != null) ? signers.FirstOrDefault((EMR_SIGNER o) => o.LOGINNAME == loginname) : null;
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

		private long? GetDocumentTypeId(string code)
		{
			long? result = null;
			try
			{
				EMR_DOCUMENT_TYPE byCode = new EmrDocumentType().GetByCode(code);
				if (byCode != null)
				{
					result = byCode.ID;
					return result;
				}
				LogSystem.Warn("Ma loai van ban truyen vao khong hop le. DocumentTypeCode = " + code);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private DocumentTDO GenerateByDocumentCode(string documentCode)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			DocumentTDO val = new DocumentTDO();
			try
			{
				EMR_DOCUMENT byCode = new EmrDocument().GetByCode(documentCode);
				val.DocumentCode = byCode.DOCUMENT_CODE;
				val.DocumentName = byCode.DOCUMENT_NAME;
				val.DocumentTypeId = byCode.DOCUMENT_TYPE_ID;
				val.TreatmentCode = byCode.TREATMENT_CODE;
				val.OriginalVersion = new VersionTDO();
				val.OriginalVersion.DocumentCode = byCode.DOCUMENT_CODE;
				val.HisCode = byCode.HIS_CODE;
				val.DocumentGroupId = byCode.DOCUMENT_GROUP_ID;
				val.DocumentTime = byCode.DOCUMENT_TIME;
				val.IsCapture = byCode.IS_CAPTURE == 1;
				val.IsSignParallel = byCode.IS_SIGN_PARALLEL == 1;
				val.MergeCode = byCode.MERGE_CODE;
				val.DependentCode = byCode.DEPENDENT_CODE;
				val.ParentDependentCode = byCode.PARENT_DEPENDENT_CODE;
				val.AttachmentCount = byCode.ATTACHMENT_COUNT;
				val.PaperName = byCode.PAPER_NAME;
				val.RawKind = byCode.RAW_KIND;
				val.Width = byCode.WIDTH;
				val.Height = byCode.HEIGHT;
				isMultiSign = byCode.IS_MULTI_SIGN == 1;
			}
			catch
			{
				val = null;
			}
			return val;
		}

		private void EnableSignButton(bool enable)
		{
			enable = inputADOWorking.IsShowPatientSign || enable;
			bbtnSign.Enabled = enable;
			bbtnPatientSign.Enabled = enable;
			bbtnConfigSign.Enabled = enable;
			bbtnSendERM.Enabled = enable;
			bbtnRejectSign.Enabled = enable;
			bbtnRelativeHomeSign.Enabled = enable;
			bbtnSignAndDeskcription.Enabled = enable;
		}

		private void EnableSignButton(bool enableSign, bool enableConfigSign, bool enableSendERM, bool enableRejectSign, bool enablePrint, bool enableSignEnd)
		{
			bbtnSign.Enabled = enableSign;
			bbtnPatientSign.Enabled = enableSign;
			bbtnRelativeHomeSign.Enabled = enableSign;
			bbtnConfigSign.Enabled = enableConfigSign;
			bbtnSendERM.Enabled = enableSendERM;
			bbtnRejectSign.Enabled = enableRejectSign;
			bbtnPrint.Enabled = enablePrint;
			bbtnSignEnd.Enabled = enableSignEnd;
			bbtnSignAndDeskcription.Enabled = enableSign;
			bbtnSignEnd.Visibility = ((!isMultiSign) ? BarItemVisibility.Never : BarItemVisibility.Always);
			if (bbtnPrint.Visibility == BarItemVisibility.Never)
			{
				bbtnPrint.Visibility = ((!enablePrint) ? BarItemVisibility.Never : BarItemVisibility.Always);
			}
		}

		private void CancelSign()
		{
			try
			{
				typeDisplayOption = -1;
				isSigning = false;
				bbtnPatientSign.Caption = "Bệnh nhân Ký";
				bbtnRelativeHomeSign.Caption = "Người nhà ký";
				bbtnSign.Caption = "Ký";
				pdfViewer1.MouseDown -= pdfViewer1_MouseDown;
				pdfViewer1.MouseMove -= pdfViewer1_MouseMove;
				pdfViewer1.MouseUp -= pdfViewer1_MouseUp;
				startPosition = null;
				endPosition = null;
				pdfViewer1.Cursor = Cursors.Hand;
				pdfViewer1.Refresh();
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private string GetOutputNameSafe(string file)
		{
			string extension = Path.GetExtension(file);
			string directoryName = Path.GetDirectoryName(file);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
			string text = string.Empty;
			int num = 0;
			string result;
			while (File.Exists(result = directoryName + "\\" + fileNameWithoutExtension + "-signed" + text + extension))
			{
				num++;
				text = "(" + num + ")";
			}
			return result;
		}

		private void ProcessPositionSigned()
		{
			try
			{
				if (isPatientSign)
				{
					signPositionADOs = Utils.GetPdfPatientSignPosition(readerWorking);
					if (signPositionADOs != null && signPositionADOs.Count > 0)
					{
						nextSignPosition = signPositionADOs.FirstOrDefault();
						hasNextSignPosition = nextSignPosition != null;
						signAutoPositionADOs = (hasNextSignPosition ? signPositionADOs.Where((SignPositionADO o) => o.Text == nextSignPosition.Text).ToList() : null);
					}
					if (signAutoPositionADOs != null && signAutoPositionADOs.Count > 0)
					{
						return;
					}
				}
				signPositionADOs = Utils.GetPdfSignPosition(readerWorking);
				signedCount = Utils.GetSignedCount(readerWorking);
				if (signPositionADOs != null && signPositionADOs.Count > 0)
				{
					signPositionADOs = signPositionADOs.OrderBy((SignPositionADO o) => VerifySign.GetNumOderByCommentText(o.Text)).ToList();
					int nextNum = ((isPatientSign || isHomeRelativeSign) ? (-1) : 0);
					List<SignPositionADO> list = signPositionADOs.Where((SignPositionADO o) => VerifySign.GetNumOderByCommentText(o.Text) == VerifySign.GetNumOrderBySignOrDefault(signSelectedByUser, Signer, Treatment, listSign, isPatientSign, isHomeRelativeSign, nextNum)).ToList();
					nextSignPosition = ((list != null && list.Count > 0) ? list.FirstOrDefault() : null);
					hasNextSignPosition = nextSignPosition != null;
					signAutoPositionADOs = (hasNextSignPosition ? signPositionADOs.Where((SignPositionADO o) => o.Text == nextSignPosition.Text).ToList() : null);
				}
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => signedCount)), (object)signedCount) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => hasNextSignPosition)), (object)hasNextSignPosition) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nextSignPosition)), (object)nextSignPosition));
			}
			catch (Exception ex)
			{
				nextSignPosition = null;
				hasNextSignPosition = false;
				signAutoPositionADOs = null;
				LogSystem.Warn(ex);
			}
		}

		private void ProcessSignPdf()
		{
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d3: Expected O, but got Unknown
			string message = "";
			if (!isPrintOnlyContent)
			{
				VerifyPdfInputFile(ref message);
			}
			ApplySignatureAppearanceFromConfigToInput();
			isShowImage = false;
			xImg = 0f;
			yImg = 0f;
			int num = 1;
			num = readerWorking.NumberOfPages;
			Rectangle pageSizeWithRotation = readerWorking.GetPageSizeWithRotation(readerWorking.NumberOfPages);
			totalPageNumber = readerWorking.NumberOfPages;
			string text = Utils.GenerateTempFileWithin();
			string outputPdfPath = "";
			ProcessInsertSignInformationPage(text, ref outputPdfPath, ref num);
			LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => currentFileWorking)), (object)currentFileWorking));
			string text2 = Utils.GenerateTempFileWithin();
			List<EMR_SIGN> list = ((currentDocument != null && !string.IsNullOrEmpty(currentDocument.DocumentCode)) ? new EmrSign().GetSignDocumentForDocument(currentDocument) : null);
			currentEmrDocument = null;
			try
			{
				if (currentDocument != null && !string.IsNullOrEmpty(currentDocument.DocumentCode) && list != null && list.Count > 0)
				{
					currentEmrDocument = new EmrDocument().GetViewByCode(currentDocument.DocumentCode);
					if (currentEmrDocument == null)
					{
						currentEmrDocument = new V_EMR_DOCUMENT
						{
							DOCUMENT_CODE = currentDocument.DocumentCode,
							DOCUMENT_NAME = (currentDocument.DocumentName ?? documentName),
							TREATMENT_CODE = (currentDocument.TreatmentCode ?? treatmentCode),
							CREATE_TIME = Utils.GetTimeNow()
						};
					}
					DisplayConfig displayConfig = new DisplayConfig();
					DisplayConfigDTO displayConfigByCommentOrDefault = GetDisplayConfigByCommentOrDefault();
					if (displayConfigByCommentOrDefault != null)
					{
						if (displayConfigByCommentOrDefault.WidthRectangle.HasValue)
						{
							displayConfig.WidthRectangle = displayConfigByCommentOrDefault.WidthRectangle.Value;
						}
						if (displayConfigByCommentOrDefault.HeightRectangle.HasValue)
						{
							displayConfig.HeightRectangle = displayConfigByCommentOrDefault.HeightRectangle.Value;
						}
						if (displayConfigByCommentOrDefault.SizeFont.HasValue)
						{
							displayConfig.SizeFont = displayConfigByCommentOrDefault.SizeFont.Value;
						}
						if (displayConfigByCommentOrDefault.TextPosition.HasValue)
						{
							displayConfig.TextPosition = (Constans.TEXT_POSITON)displayConfigByCommentOrDefault.TextPosition.Value;
						}
						if (displayConfigByCommentOrDefault.TypeDisplay.HasValue)
						{
							displayConfig.TypeDisplay = displayConfigByCommentOrDefault.TypeDisplay.Value;
						}
						if (displayConfigByCommentOrDefault.IsDisplaySignature.HasValue)
						{
							displayConfig.IsDisplaySignature = displayConfigByCommentOrDefault.IsDisplaySignature.Value;
						}
						if (!string.IsNullOrEmpty(displayConfigByCommentOrDefault.FormatRectangleText))
						{
							displayConfig.FormatRectangleText = displayConfigByCommentOrDefault.FormatRectangleText;
						}
						if (displayConfigByCommentOrDefault.Titles != null)
						{
							displayConfig.Titles = displayConfigByCommentOrDefault.Titles;
						}
						if (displayConfig.TextFormat == null)
						{
							displayConfig.TextFormat = new FontConfig();
						}
						if (displayConfigByCommentOrDefault.Alignment.HasValue)
						{
							displayConfig.TextFormat.Alignment = (ALIGNMENT_OPTION)displayConfigByCommentOrDefault.Alignment.Value;
						}
						if (displayConfigByCommentOrDefault.IsBold.HasValue)
						{
							displayConfig.TextFormat.IsBold = displayConfigByCommentOrDefault.IsBold.Value;
						}
						if (displayConfigByCommentOrDefault.IsItalic.HasValue)
						{
							displayConfig.TextFormat.IsItalic = displayConfigByCommentOrDefault.IsItalic.Value;
						}
						if (displayConfigByCommentOrDefault.IsUnderlined.HasValue)
						{
							displayConfig.TextFormat.IsUnderlined = displayConfigByCommentOrDefault.IsUnderlined.Value;
						}
						if (!string.IsNullOrEmpty(displayConfigByCommentOrDefault.FontName))
						{
							displayConfig.TextFormat.FontName = displayConfigByCommentOrDefault.FontName;
						}
					}
					WaterMarkProcess.ProcessInsertWaterMark(readerWorking, text2, currentEmrDocument, list, !isPrintOnlyContent && verifiers != null && verifiers.Count > 0, ref txtSignDescriptionList, displayConfig);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			try
			{
				readerWorking.Close();
			}
			catch
			{
			}
			if (File.Exists(text2) && GlobalStore.PrintUsingWaterMark == "1")
			{
				currentFileWorking = Utils.GenerateTempFileWithin();
				File.Copy(text2, currentFileWorking, true);
			}
			else if (!string.IsNullOrEmpty(outputPdfPath) && File.Exists(outputPdfPath))
			{
				currentFileWorking = outputPdfPath;
			}
			if (string.IsNullOrEmpty(text2) || !File.Exists(text2))
			{
				text2 = currentFileWorking;
			}
			pdfViewer1.DetachStreamAfterLoadComplete = true;
			pdfViewer1.LoadDocument(text2);
			timerLoadSinglePage.Start();
			try
			{
				if (File.Exists(text2))
				{
					File.Delete(text2);
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
			try
			{
				if (File.Exists(outputPdfPath))
				{
					File.Delete(outputPdfPath);
				}
			}
			catch
			{
			}
			try
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
			catch
			{
			}
		}

		private void ApplySignatureAppearanceFromConfigToInput()
		{
			try
			{
				List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
				EMR_CONFIG val = ((emrConfigs != null) ? emrConfigs.FirstOrDefault((EMR_CONFIG o) => o.KEY == "EMR.EMR_SIGN.SIGNATURE_APPEARANCE_OPTION") : null);
				if (val == null)
				{
					return;
				}
				string text = ((!string.IsNullOrEmpty(val.VALUE)) ? val.VALUE : val.DEFAULT_VALUE);
				if (string.IsNullOrWhiteSpace(text))
				{
					return;
				}
				if (inputADOWorking.DisplayConfigDTO == null)
				{
					inputADOWorking.DisplayConfigDTO = new DisplayConfigDTO();
				}
				string[] array = text.Split(new char[1] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text2 in array)
				{
					string[] array2 = text2.Split(new char[1] { ':' }, StringSplitOptions.RemoveEmptyEntries);
					if (array2.Length != 2)
					{
						continue;
					}
					string text3 = array2[0].Trim().ToLowerInvariant();
					string text4 = array2[1].Trim();
					switch (text3)
					{
					case "p":
					{
						int result3;
						if (int.TryParse(text4, out result3))
						{
							inputADOWorking.DisplayConfigDTO.TextPosition = result3;
						}
						break;
					}
					case "f":
					{
						int result5;
						if (int.TryParse(text4, out result5))
						{
							inputADOWorking.DisplayConfigDTO.SizeFont = result5;
						}
						break;
					}
					case "w":
					{
						int result;
						if (int.TryParse(text4, out result))
						{
							inputADOWorking.DisplayConfigDTO.WidthRectangle = result;
						}
						break;
					}
					case "h":
					{
						int result4;
						if (int.TryParse(text4, out result4))
						{
							inputADOWorking.DisplayConfigDTO.HeightRectangle = result4;
						}
						break;
					}
					case "a":
					{
						int result2;
						if (int.TryParse(text4, out result2))
						{
							inputADOWorking.DisplayConfigDTO.Alignment = result2;
						}
						break;
					}
					case "fs":
					{
						string text5 = text4.ToUpperInvariant();
						inputADOWorking.DisplayConfigDTO.IsBold = text5.Contains("B");
						inputADOWorking.DisplayConfigDTO.IsItalic = text5.Contains("I");
						inputADOWorking.DisplayConfigDTO.IsUnderlined = text5.Contains("U");
						break;
					}
					case "fn":
						if (!string.IsNullOrEmpty(text4))
						{
							inputADOWorking.DisplayConfigDTO.FontName = text4;
						}
						break;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessInsertSignInformationPage(string outputPdfPathTemp, ref string outputPdfPath, ref int pageCount)
		{
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Expected O, but got Unknown
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Expected O, but got Unknown
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Expected O, but got Unknown
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Expected O, but got Unknown
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Expected O, but got Unknown
			//IL_0126: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Expected O, but got Unknown
			try
			{
				if (isPrintOnlyContent || verifiers == null || verifiers.Count <= 0)
				{
					return;
				}
				FileStream fileStream = File.Open(outputPdfPathTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				Document val = new Document(readerWorking.GetPageSizeWithRotation(readerWorking.NumberOfPages));
				PdfWriter instance = PdfWriter.GetInstance(val, (Stream)fileStream);
				val.Open();
				PdfPTable val2 = AddPdfPTable();
				val.Add((IElement)(object)val2);
				val.Close();
				List<int> list = new List<int>();
				for (int i = 0; i <= readerWorking.NumberOfPages; i++)
				{
					list.Add(i);
				}
				outputPdfPath = Utils.GenerateTempFileWithin();
				currentStream = File.Open(outputPdfPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				PdfConcatenate val3 = new PdfConcatenate(currentStream);
				PdfReader val4 = null;
				val4 = (string.IsNullOrEmpty(inputFileWork) ? new PdfReader(inputStream) : new PdfReader(inputFileWork));
				val4.SelectPages((ICollection<int>)list);
				val3.AddPages(val4);
				val4.Close();
				val4 = new PdfReader(outputPdfPathTemp);
				val4.SelectPages((ICollection<int>)new List<int> { 0, 1 });
				val3.AddPages(val4);
				try
				{
					fileStream.Close();
					fileStream.Dispose();
				}
				catch
				{
				}
				try
				{
					val4.Close();
				}
				catch
				{
				}
				try
				{
					val3.Close();
				}
				catch
				{
				}
				try
				{
					readerWorking.Close();
				}
				catch
				{
				}
				try
				{
					if (File.Exists(outputPdfPathTemp))
					{
						File.Delete(outputPdfPathTemp);
					}
				}
				catch
				{
				}
				readerWorking = new PdfReader(outputPdfPath);
				pageCount++;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private bool VerifyPdfInputFile(ref string message)
		{
			VerifyPdfFileHandle verifyPdfFileHandle = new VerifyPdfFileHandle();
			try
			{
				verifiers = (from o in verifyPdfFileHandle.verify(readerWorking)
					orderby o.Date
					select o).ToList();
			}
			catch
			{
				verifiers = null;
			}
			if (verifiers == null)
			{
				message = "File cần xác thực không hợp lệ";
				return false;
			}
			if (verifiers.Count == 0)
			{
				message = "File đã được chọn không tìm thấy chữ ký số";
				return false;
			}
			bool flag = true;
			string text = "";
			for (int num = verifiers.Count; num > 0; num--)
			{
				VerifierADO verifierADO = verifiers[num - 1];
				if (verifierADO != null)
				{
					if (verifierADO.Modified)
					{
						flag = false;
						text = ((text.Length != 0) ? (text + "\nChữ ký số không hợp lệ.") : "Chữ ký số không hợp lệ.");
					}
					if (!verifierADO.Valid)
					{
						flag = false;
						if (verifierADO.InvalidReasonList == null || verifierADO.InvalidReasonList.Count <= 0)
						{
							text = ((text.Length != 0) ? (text + "\nFile chứa CTS không hợp lệ.") : "File chứa CTS không hợp lệ.");
						}
						else
						{
							foreach (InvalidReasonADO invalidReason in verifierADO.InvalidReasonList)
							{
								if (text.Length == 0)
								{
									text = invalidReason.ReasonVnLang;
								}
								else if (text.IndexOf(invalidReason.ReasonVnLang) != -1)
								{
									text = text + "\n" + invalidReason.ReasonVnLang;
								}
							}
						}
					}
				}
			}
			if (flag)
			{
				message = "Hợp lệ";
			}
			else
			{
				message = text;
			}
			return flag;
		}

		private bool VerifyWithExistsDocument(DocumentTDO document, bool checkSigner = true)
		{
			if (document != null && !string.IsNullOrEmpty(document.DocumentCode) && checkSigner && string.IsNullOrEmpty(inputADOWorking.BusinessCode))
			{
				EMR_SIGN val = null;
				val = ((!(GlobalStore.EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION == "1") || !IsAddPatientSign || (!isPatientSign && !isHomeRelativeSign)) ? ((signSelectedByUser != null) ? signSelectedByUser : new EmrSign().GetSignDocumentFirst(document.DocumentCode, (isPatientSign || isHomeRelativeSign) ? null : Signer, Treatment, isMultiSign, true)) : new EmrSign().GetSignDocumentFirst(document.DocumentCode, (isPatientSign || isHomeRelativeSign) ? null : Signer, Treatment, isMultiSign, true));
				if (val == null || val.ID == 0L || (GlobalStore.EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION == "1" && IsAddPatientSign && (isPatientSign || isHomeRelativeSign) && string.IsNullOrEmpty(val.PATIENT_CODE)))
				{
					MessageManager.Show(MessageUitl.GetMessage("PhaiTaoLuongKyChoVanBanDaCoTrenHeThong"));
					LogSystem.Warn(MessageUitl.GetMessage("PhaiTaoLuongKyChoVanBanDaCoTrenHeThong"));
					if (dlgOpenModuleConfig != null)
					{
						dlgOpenModuleConfig(document);
					}
					return false;
				}
			}
			ProcessPositionSigned();
			return true;
		}

		private bool VerifySignPad()
		{
			bool result = true;
			try
			{
				if (isUsingSignPad && GlobalStore.EMR_SIGN_BOARD__OPTION == "2" && ((EmrConfigKeys.EMR_EMR_SIGN_CONNECT_DEVICE_TYPE_OPTION == "2" && new FingerPrintUseBehavior(null, null).IsProcessOpen("Inventec.FingerPrintManager")) || (EmrConfigKeys.EMR_EMR_SIGN_CONNECT_DEVICE_TYPE_OPTION != "2" && new SignBoardUseBehavior(null, null).IsProcessOpen("Inventec.SignPadManager"))))
				{
					result = false;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return result;
		}

		private PdfPTable AddPdfPTable()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Expected O, but got Unknown
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Expected O, but got Unknown
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Expected O, but got Unknown
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Expected O, but got Unknown
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Expected O, but got Unknown
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected O, but got Unknown
			//IL_0120: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Expected O, but got Unknown
			//IL_0176: Unknown result type (might be due to invalid IL or missing references)
			//IL_017d: Expected O, but got Unknown
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Expected O, but got Unknown
			//IL_019b: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Expected O, but got Unknown
			//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b6: Expected O, but got Unknown
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c7: Expected O, but got Unknown
			//IL_01df: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e9: Expected O, but got Unknown
			//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fa: Expected O, but got Unknown
			//IL_0212: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Expected O, but got Unknown
			//IL_0283: Unknown result type (might be due to invalid IL or missing references)
			//IL_028a: Expected O, but got Unknown
			//IL_028f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0299: Expected O, but got Unknown
			//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02aa: Expected O, but got Unknown
			//IL_02af: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b9: Expected O, but got Unknown
			//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ca: Expected O, but got Unknown
			//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02de: Expected O, but got Unknown
			PdfPTable val = new PdfPTable(7);
			val.SetTotalWidth(new float[7] { 7f, 20f, 20f, 15f, 30f, 30f, 30f });
			Font val2 = new Font(Utils.GetBaseFont(), 9f, 0);
			Font val3 = new Font(Utils.GetBaseFont(), 9f, 1);
			PdfPCell val4 = new PdfPCell();
			val4.AddElement((IElement)new Paragraph("STT", val3));
			val.AddCell(val4);
			PdfPCell val5 = new PdfPCell();
			val5.AddElement((IElement)new Paragraph("Người ký", val3));
			val.AddCell(val5);
			PdfPCell val6 = new PdfPCell();
			val6.AddElement((IElement)new Paragraph("Thời gian ký", val3));
			val.AddCell(val6);
			PdfPCell val7 = new PdfPCell();
			val7.AddElement((IElement)new Paragraph("Hạn CT", val3));
			val.AddCell(val7);
			PdfPCell val8 = new PdfPCell();
			val8.AddElement((IElement)new Paragraph("Đơn vị", val3));
			val.AddCell(val8);
			PdfPCell val9 = new PdfPCell();
			val9.AddElement((IElement)new Paragraph("Chức danh", val3));
			val.AddCell(val9);
			PdfPCell val10 = new PdfPCell();
			val10.AddElement((IElement)new Paragraph("Ý kiến của người ký", val3));
			val.AddCell(val10);
			int num = 1;
			if (verifiers != null && verifiers.Count > 0)
			{
				foreach (VerifierADO verifier in verifiers)
				{
					PdfPCell val11 = new PdfPCell();
					val11.AddElement((IElement)new Chunk(num.ToString(), val2));
					val.AddCell(val11);
					PdfPCell val12 = new PdfPCell();
					val12.AddElement((IElement)new Chunk(verifier.SignerName, val2));
					val.AddCell(val12);
					PdfPCell val13 = new PdfPCell();
					val13.AddElement((IElement)new Chunk(verifier.Date.ToString("dd/MM/yyyy HH:mm:ss"), val2));
					val.AddCell(val13);
					PdfPCell val14 = new PdfPCell();
					val14.AddElement((IElement)new Chunk(verifier.NotAfter.ToString("dd/MM/yyyy"), val2));
					val.AddCell(val14);
					string text = "";
					string text2 = "";
					if (!string.IsNullOrEmpty(verifier.Location))
					{
						string[] array = verifier.Location.Split(new string[1] { "|" }, StringSplitOptions.None);
						if (array.Length == 2)
						{
							text = array[0];
							text2 = array[1];
						}
					}
					PdfPCell val15 = new PdfPCell();
					val15.AddElement((IElement)new Chunk(text, val2));
					val.AddCell(val15);
					PdfPCell val16 = new PdfPCell();
					val16.AddElement((IElement)new Chunk(text2, val2));
					val.AddCell(val16);
					PdfPCell val17 = new PdfPCell();
					val17.AddElement((IElement)new Chunk(verifier.Comment, val2));
					val.AddCell(val17);
					num++;
				}
			}
			return val;
		}

		private bool SignDigital(float _x, float _y, int _pageNumberCurrent, int _totalPageNumber, DisplayConfigDTO displayConfigDTO, bool? isMultiSignForProcess = null)
		{
			//IL_0992: Unknown result type (might be due to invalid IL or missing references)
			//IL_099c: Expected O, but got Unknown
			//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05dc: Expected O, but got Unknown
			//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b0: Expected O, but got Unknown
			bool success = false;
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<float>((Expression<Func<float>>)(() => _x)), (object)_x) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<float>((Expression<Func<float>>)(() => _y)), (object)_y) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => _pageNumberCurrent)), (object)_pageNumberCurrent) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => _totalPageNumber)), (object)_totalPageNumber) + "____SignDigital:" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => Signer.LOGINNAME)), (object)Signer.LOGINNAME) + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => Treatment.TREATMENT_CODE)), (object)Treatment.TREATMENT_CODE) + LogUtil.TraceData(LogUtil.GetMemberName<DisplayConfigDTO>((Expression<Func<DisplayConfigDTO>>)(() => displayConfigDTO)), (object)displayConfigDTO) + LogUtil.TraceData(LogUtil.GetMemberName<bool?>((Expression<Func<bool?>>)(() => isMultiSignForProcess)), (object)isMultiSignForProcess));
			WaitingManager.Show();
			if (!bbtnSign.Enabled || !bbtnPatientSign.Enabled)
			{
				CancelSign();
				return false;
			}
			if (dlgChoosePoint != null)
			{
				dlgChoosePoint(_x, _y);
			}
			if ((startPosition != null && endPosition != null) || nextSignPosition != null || fileType == FileType.Xml || fileType == FileType.Json)
			{
				CommonParam param = new CommonParam();
				LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => inputFileWork)), (object)inputFileWork));
				SignHandle signHandle = (string.IsNullOrEmpty(inputFileWork) ? new SignHandle(inputStream, _x, _y, _pageNumberCurrent, _totalPageNumber, CancelSign, dlgOpenModuleConfig, documentName, treatmentCode, signName, signReason, verifiers, signType, listSign, signSelected, isPatientSign, isHomeRelativeSign, DocumentTypeId, isMultiSignForProcess.HasValue ? isMultiSignForProcess.Value : isMultiSign, hisCode, inputADOWorking, displayConfigDTO, param, base.ParentForm, GetCheckSignParanel(), Treatment, Signer, TokenCode, null) : new SignHandle(inputFileWork, _x, _y, _pageNumberCurrent, _totalPageNumber, CancelSign, dlgOpenModuleConfig, documentName, treatmentCode, signName, signReason, verifiers, signType, listSign, signSelected, isPatientSign, isHomeRelativeSign, DocumentTypeId, isMultiSignForProcess.HasValue ? isMultiSignForProcess.Value : isMultiSign, hisCode, inputADOWorking, displayConfigDTO, param, base.ParentForm, GetCheckSignParanel(), Treatment, Signer, TokenCode, null));
				string message = "";
				if (!signHandle.VerifyFile(currentDocument, signedCount, ref message))
				{
					WaitingManager.Hide();
					CancelSign();
					return false;
				}
				string outputFile = resultFileStore;
				try
				{
					if (currentDocument == null)
					{
						currentDocument = new DocumentTDO();
					}
					signHandle.SetUsingSignPad(isUsingSignPad);
					signHandle.SetFileType(fileType);
					currentDocument.OriginalVersion = new VersionTDO();
					if (fileADOMain != null && !string.IsNullOrEmpty(fileADOMain.Base64FileContent))
					{
						currentDocument.OriginalVersion.Base64Data = fileADOMain.Base64FileContent;
					}
					if (fileADOXml != null && !string.IsNullOrEmpty(fileADOXml.Base64FileContent))
					{
						currentDocument.OriginalVersion.Base64DataXml = fileADOXml.Base64FileContent;
					}
					if (fileADOJson != null && !string.IsNullOrEmpty(fileADOJson.Base64FileContent))
					{
						currentDocument.OriginalVersion.Base64DataJson = fileADOJson.Base64FileContent;
					}
					success = signHandle.SignFile(currentDocument, ref outputFile);
					LogSystem.Info("SignDigital__" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => success)), (object)success) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => outputFile)), (object)outputFile));
					if (success)
					{
						if (dlgSendResultSigned != null)
						{
							dlgSendResultSigned(new DocumentSignedUpdateIGSysResultDTO
							{
								DocumentCode = currentDocument.DocumentCode
							});
						}
						try
						{
							if (readerWorking != null)
							{
								readerWorking.Close();
							}
							if (File.Exists(inputFileWork))
							{
								File.Delete(inputFileWork);
							}
						}
						catch
						{
						}
						ProcessDependentCodeAfterSigned();
						LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName<bool?>((Expression<Func<bool?>>)(() => isCloseAfterSign)), (object)isCloseAfterSign) + LogUtil.TraceData(LogUtil.GetMemberName<bool?>((Expression<Func<bool?>>)(() => isMultiSignForProcess)), (object)isMultiSignForProcess));
						if (isSignNow && actionAfterSigned != null)
						{
							WaitingManager.Hide();
							actionAfterSigned(outputFile);
							return success;
						}
						if (isCloseAfterSign.HasValue && isCloseAfterSign.Value && closeAfterSign != null && (!isMultiSignForProcess.HasValue || !isMultiSignForProcess.Value))
						{
							WaitingManager.Hide();
							closeAfterSign(success);
							return success;
						}
						inputFileWork = outputFile;
						try
						{
							if (inputStream != null)
							{
								inputStream.Flush();
								inputStream.Dispose();
								inputStream = null;
							}
						}
						catch (Exception ex)
						{
							LogSystem.Warn(ex);
						}
						ProcessStoreCurrentFileToPrint(outputFile);
						if (fileType != FileType.Xml && fileType != FileType.Json)
						{
							readerWorking = new PdfReader(inputFileWork);
							ProcessSignPdf();
							currentPageSettings = PdfDocumentProcess.GetPaperSize(inputFileWork);
							pdfViewer1.CurrentPageNumber = _pageNumberCurrent + plusNumberWithShowingSignInformation;
							if ((!hasNextSignPosition || nextSignPosition == null) && signedCount > 0)
							{
								int amount = (int)((double)_x / ((double)pdfViewer1.ZoomFactor * 0.01) - (double)pdfViewer1.ClientRectangle.Width / ((double)pdfViewer1.ZoomFactor * 0.01) / 2.0);
								int amount2 = (int)((double)_y / ((double)pdfViewer1.ZoomFactor * 0.01) - (double)pdfViewer1.ClientRectangle.Height / ((double)pdfViewer1.ZoomFactor * 0.01) / 2.0);
								pdfViewer1.ScrollHorizontal(amount);
								pdfViewer1.ScrollVertical(amount2);
							}
							else
							{
								pdfViewer1.HorizontalScrollPosition = _x;
								pdfViewer1.VerticalScrollPosition = _y;
							}
						}
						EnableSignButton(isMultiSignForProcess.HasValue ? isMultiSignForProcess.Value : isMultiSign, true, false, false, true, isMultiSignForProcess.HasValue ? isMultiSignForProcess.Value : isMultiSign);
						if (isMultiSignForProcess.HasValue ? isMultiSignForProcess.Value : isMultiSign)
						{
							signSelected = new EmrSign().GetSignDocumentFirst(currentDocument.DocumentCode, (isPatientSign || isHomeRelativeSign) ? null : Signer, Treatment, isMultiSignForProcess.HasValue ? isMultiSignForProcess.Value : isMultiSign, false);
							bbtnSignEnd.Enabled = signSelected != null && signSelected.IS_SIGNING == 1;
						}
						if (bbtnConfigBussinessMenu1.Visibility == BarItemVisibility.Never && bbtnAttackMentsMenu1.Visibility == BarItemVisibility.Never)
						{
							VisibleButonOrther(false);
						}
						else
						{
							VisibleButonOrther(true);
						}
					}
					else
					{
						try
						{
						}
						catch
						{
						}
						try
						{
						}
						catch
						{
						}
						try
						{
						}
						catch
						{
						}
					}
					UpdateStateIGSys((!success) ? "01" : (((isMultiSignForProcess.HasValue ? isMultiSignForProcess.Value : isMultiSign) || (listSign != null && listSign.Count > 1)) ? "05" : "00"));
					CancelSign();
					WaitingManager.Hide();
				}
				catch (Exception ex2)
				{
					LogSystem.Warn(ex2);
					WaitingManager.Hide();
					MessageBox.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					CancelSign();
				}
			}
			return success;
		}

		private void ProcessDependentCodeAfterSigned()
		{
			try
			{
				if (currentDocument == null || string.IsNullOrEmpty(currentDocument.DocumentCode) || string.IsNullOrEmpty(currentDocument.DependentCode))
				{
					return;
				}
				string signer = ((Signer != null) ? Signer.LOGINNAME : null);
				List<V_EMR_DOCUMENT> documentDependent = new EmrDocument().GetDocumentDependent(currentDocument.DependentCode, currentDocument.TreatmentCode, signer);
				if (documentDependent == null || documentDependent.Count <= 0)
				{
					return;
				}
				if (documentDependent.Count == 1)
				{
					if (!(currentDocument.DocumentCode == documentDependent[0].DOCUMENT_CODE))
					{
						ProcessSignOneDocumentDependent(documentDependent[0]);
					}
				}
				else
				{
					frmChooseDocumentDependent frmChooseDocumentDependent2 = new frmChooseDocumentDependent(ProcessSignOneDocumentDependent, documentDependent, "");
					frmChooseDocumentDependent2.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessSignOneDocumentDependent(V_EMR_DOCUMENT doc)
		{
			try
			{
				if (doc != null)
				{
					bool num;
					if (!isPatientSign && !isHomeRelativeSign)
					{
						if (doc.IS_SIGN_PARALLEL != 1 && doc.NEXT_SIGNER == Signer.LOGINNAME)
						{
							goto IL_010f;
						}
						if (doc.IS_SIGN_PARALLEL != 1 || string.IsNullOrEmpty(doc.UN_SIGNERS))
						{
							goto IL_03fc;
						}
						num = string.Format(",{0},", doc.UN_SIGNERS).Contains(string.Format(",{0},", Signer.LOGINNAME));
					}
					else
					{
						num = doc.PATIENT_CODE == Treatment.PATIENT_CODE;
					}
					if (num)
					{
						goto IL_010f;
					}
				}
				goto IL_03fc;
				IL_03fc:
				MessageBox.Show(string.Format(MessageUitl.GetMessage("KyVanBanPhuThocThatBai__KhongPhaiLuotKyCuaBan"), doc.DOCUMENT_CODE, doc.DOCUMENT_NAME));
				LogSystem.Warn(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => doc.NEXT_SIGNER)), (object)doc.NEXT_SIGNER) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => Signer.LOGINNAME)), (object)Signer.LOGINNAME) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => Treatment.PATIENT_CODE)), (object)Treatment.PATIENT_CODE) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isPatientSign)), (object)isPatientSign) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isHomeRelativeSign)), (object)isHomeRelativeSign));
				return;
				IL_010f:
				EMR_VERSION signedDocumentLast = new EmrVersion().GetSignedDocumentLast(doc.ID);
				if (signedDocumentLast != null && !string.IsNullOrWhiteSpace(signedDocumentLast.URL))
				{
					string tempFileName = Path.GetTempFileName();
					tempFileName = tempFileName.Replace(".tmp", ".pdf");
					using (MemoryStream memoryStream = FssFileDownload.GetFile(signedDocumentLast.URL))
					{
						if (memoryStream != null)
						{
							using (FileStream destination = new FileStream(tempFileName, FileMode.Create, FileAccess.Write))
							{
								memoryStream.CopyTo(destination);
							}
						}
						else
						{
							MessageBox.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
						}
					}
					SignLibraryGUIProcessor signLibraryGUIProcessor = new SignLibraryGUIProcessor();
					InputADO inputADO = new InputADO();
					inputADO.DTI = inputADOWorking.DTI;
					inputADO.IsSave = false;
					inputADO.IsSign = true;
					inputADO.IsReject = false;
					inputADO.IsPrint = false;
					inputADO.IsExport = false;
					inputADO.IsPrintOnlyContent = inputADOWorking.IsPrintOnlyContent;
					inputADO.SignType = inputADOWorking.SignType;
					inputADO.Treatment = inputADOWorking.Treatment;
					inputADO.DocumentCode = doc.DOCUMENT_CODE;
					inputADO.DocumentName = doc.DOCUMENT_NAME;
					inputADO.DlgOpenModuleConfig = inputADOWorking.DlgOpenModuleConfig;
					inputADO.RoomCode = inputADOWorking.RoomCode;
					inputADO.RoomName = inputADOWorking.RoomName;
					inputADO.RoomTypeCode = inputADOWorking.RoomTypeCode;
					inputADO.DlgCloseAfterSign = ProcessDlgCloseAfterSign;
					inputADO.IsCloseAfterSign = true;
					inputADO.DepartmentCode = inputADOWorking.DepartmentCode;
					inputADO.DepartmentName = inputADOWorking.DepartmentName;
					inputADO.DependentCode = inputADOWorking.DependentCode;
					inputADO.ParentDependentCode = inputADOWorking.ParentDependentCode;
					inputADO.IsOptionSignType = inputADOWorking.IsOptionSignType;
					inputADO.DlgChangeOptionSignType = inputADOWorking.DlgChangeOptionSignType;
					inputADO.PaperSizeDefault = inputADOWorking.PaperSizeDefault;
					inputADO.PrinterDefault = inputADOWorking.PrinterDefault;
					inputADO.IsOutsideTreatment = doc.IS_OUTSIDE_TREATMENT;
					if (!string.IsNullOrWhiteSpace(tempFileName) && File.Exists(tempFileName))
					{
						signLibraryGUIProcessor.SignNow(Utils.FileToBase64String(tempFileName), FileType.Pdf, inputADO);
					}
					else
					{
						MessageBox.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					}
					if (File.Exists(tempFileName))
					{
						File.Delete(tempFileName);
					}
				}
				else
				{
					MessageBox.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessDlgCloseAfterSign(bool success)
		{
			base.ParentForm.Close();
		}

		private void UpdateStateIGSys(string code)
		{
			try
			{
				DocumentSignedUpdateIGSysResultDTO documentSignedUpdateIGSysResultDTO = new DocumentSignedUpdateIGSysResultDTO();
				documentSignedUpdateIGSysResultDTO.DocumentName = documentName;
				documentSignedUpdateIGSysResultDTO.DocumentCode = ((currentDocument != null) ? currentDocument.DocumentCode : "");
				documentSignedUpdateIGSysResultDTO.DocumentTypeCode = inputADOWorking.DocumentTypeCode;
				documentSignedUpdateIGSysResultDTO.HisCode = inputADOWorking.HisCode;
				documentSignedUpdateIGSysResultDTO.TREATMENT_CODE = treatmentCode;
				documentSignedUpdateIGSysResultDTO.NgayKy = DateTimeConvert.SystemDateTimeToTimeNumber(DateTime.Now).ToString() ?? "";
				documentSignedUpdateIGSysResultDTO.NguoiKy = signName;
				documentSignedUpdateIGSysResultDTO.MaLoi = code;
				documentSignedUpdateIGSysResultDTO.token = ((!string.IsNullOrEmpty(TokenCode)) ? TokenCode : ((GlobalStore.TokenData != null) ? GlobalStore.TokenData.TokenCode : GlobalStore.TokenCode));
				if (dlgSendResultSigned != null)
				{
					dlgSendResultSigned(documentSignedUpdateIGSysResultDTO);
				}
				if (!string.IsNullOrEmpty(GlobalStore.INTERGRATE_SYS_BASE_URI))
				{
					CommonParam param = new CommonParam();
					IGDocumentState iGDocumentState = new IGDocumentState(param);
					iGDocumentState.SendSignedInfoToIGSys(documentSignedUpdateIGSysResultDTO);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void UpdateAfterAddSignThread(List<SignTDO> signs)
		{
			try
			{
				listSign = signs;
				if (GlobalStore.EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION == "1" && IsAddPatientSign)
				{
					BarCheckItem barCheckItem = bbtnchkSignParanel;
					bool flag = (bbtnchkSignParanel.Enabled = false);
					barCheckItem.Checked = flag;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void UpdateRejectInfo(string rejectReason)
		{
			this.rejectReason = rejectReason;
		}

		internal DocumentTDO GetCurrentDocument()
		{
			return currentDocument;
		}

		internal void Print()
		{
			try
			{
				if (GlobalStore.OptionPrintType == OptionPrintType.PdfAposeLib)
				{
					PrintAposeLib();
				}
				else
				{
					PrintDevLib();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void PrintExeServiceLib()
		{
			try
			{
				LogSystem.Debug("PrintExeServiceLib.1");
				bool flag = false;
				string inputFile = "";
				if (File.Exists(currentFileWorking))
				{
					LogSystem.Debug("PrintExeServiceLib.2");
					inputFile = currentFileWorking;
					LogSystem.Debug("Print currentFileWorking exists: " + currentFileWorking);
				}
				else if (File.Exists(inputFileWork))
				{
					LogSystem.Debug("PrintExeServiceLib.3");
					inputFile = inputFileWork;
					LogSystem.Debug("Print currentFileWorking not exists, replace with inputFileWork: " + inputFileWork);
				}
				PrintLibProcess.ExecutePrintCallExeService(inputFile, printNumberCopies, inputADOWorking.PrinterDefault, inputADOWorking.PaperSizeDefault);
				LogSystem.Debug("PrintExeServiceLib.4");
				if (true && inputADOWorking.ActPrintSuccess != null)
				{
					LogSystem.Debug("PrintExeServiceLib.5");
					LogSystem.Debug("inputADOWorking.ActPrintSuccess != null: " + (inputADOWorking.ActPrintSuccess != null));
					try
					{
						inputADOWorking.ActPrintSuccess();
					}
					catch (Exception ex)
					{
						LogSystem.Warn(ex);
					}
				}
				LogSystem.Debug("PrintExeServiceLib.6");
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private void PrintAposeLib()
		{
			try
			{
				bool flag = false;
				string inputFile = "";
				if (File.Exists(currentFileWorking))
				{
					inputFile = currentFileWorking;
					LogSystem.Debug("Print currentFileWorking exists: " + currentFileWorking);
				}
				else if (File.Exists(inputFileWork))
				{
					inputFile = inputFileWork;
					LogSystem.Debug("Print currentFileWorking not exists, replace with inputFileWork: " + inputFileWork);
				}
				if (PrintLibProcess.SimplePrint(inputFile, printNumberCopies, inputADOWorking.PrinterDefault, inputADOWorking.PaperSizeDefault) && inputADOWorking.ActPrintSuccess != null)
				{
					LogSystem.Debug("inputADOWorking.ActPrintSuccess != null: " + (inputADOWorking.ActPrintSuccess != null));
					try
					{
						inputADOWorking.ActPrintSuccess();
						return;
					}
					catch (Exception ex)
					{
						LogSystem.Warn(ex);
						return;
					}
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private void PrintDevLib()
		{
			try
			{
				LogSystem.Debug("PrintDevLib.1");
				string inputFile = "";
				if (File.Exists(currentFileWorking))
				{
					LogSystem.Debug("PrintDevLib.2");
					inputFile = currentFileWorking;
					LogSystem.Debug("Print currentFileWorking exists: " + currentFileWorking);
				}
				else if (File.Exists(inputFileWork))
				{
					LogSystem.Debug("PrintDevLib.3");
					inputFile = inputFileWork;
					LogSystem.Debug("Print currentFileWorking not exists, replace with inputFileWork: " + inputFileWork);
				}
				if (PrintLibProcess.SimplePrintDevLib(inputFile, printNumberCopies, inputADOWorking.PrinterDefault, inputADOWorking.PaperSizeDefault))
				{
					LogSystem.Debug("PrintDevLib.4");
					if (inputADOWorking.ActPrintSuccess != null)
					{
						LogSystem.Debug("inputADOWorking.ActPrintSuccess != null: " + (inputADOWorking.ActPrintSuccess != null));
						try
						{
							inputADOWorking.ActPrintSuccess();
						}
						catch (Exception ex)
						{
							LogSystem.Warn(ex);
						}
					}
					LogSystem.Debug("PrintDevLib.5");
				}
				LogSystem.Debug("PrintDevLib.6");
			}
			catch (Exception ex2)
			{
				LogSystem.Warn(ex2);
			}
		}

		private void UpdateNote(string _note)
		{
			signReason = _note;
		}

		private void ChooseBusinessClick(EMR_BUSINESS dataBusiness)
		{
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			try
			{
				businessCode = ((dataBusiness != null) ? dataBusiness.BUSINESS_CODE : "");
				if (!string.IsNullOrEmpty(businessCode))
				{
					bbtnConfigSign.Visibility = BarItemVisibility.Never;
					inputADOWorking.BusinessCode = businessCode;
					EmrSignerFlow emrSignerFlow = new EmrSignerFlow();
					List<V_EMR_SIGNER_FLOW> list = ((Signer != null) ? emrSignerFlow.GetView(new EmrSignerFlowViewFilter
					{
						IS_ACTIVE = (short)1,
						BUSINESS_CODE__EXACT = businessCode,
						LOGINNAME__EXACT = Signer.LOGINNAME
					}) : null);
					if (list != null && list.Count > 0)
					{
						inputADOWorking.RoomCode = list.FirstOrDefault().ROOM_CODE;
						inputADOWorking.RoomTypeCode = list.FirstOrDefault().ROOM_TYPE_CODE;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SignParanelEditValueChanged(bool issignparanel)
		{
			try
			{
				bbtnchkSignParanel.Checked = issignparanel;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private bool GetCheckSignParanel()
		{
			return bbtnchkSignParanel.Checked;
		}

		private void pdfViewer1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && pdfViewer1.IsDocumentOpened)
			{
				isShowRangtax = true;
				startPosition = pdfViewer1.GetDocumentPosition(e.Location);
				if (!isSelectRangeRectangle)
				{
					endPosition = pdfViewer1.GetDocumentPosition(new PointF(e.Location.X + 10, e.Location.Y + 10));
				}
				mouseButtonPressed = true;
				pdfViewer1.Invalidate();
			}
		}

		private void pdfViewer1_MouseMove(object sender, MouseEventArgs e)
		{
			if (!pdfViewer1.IsDocumentOpened)
			{
				return;
			}
			if (mouseButtonPressed)
			{
				pdfViewer1.Cursor = Cursors.Cross;
				if (isSelectRangeRectangle)
				{
					endPosition = pdfViewer1.GetDocumentPosition(e.Location);
					startPoint1 = pdfViewer1.GetClientPoint(startPosition);
					endPoint1 = pdfViewer1.GetClientPoint(endPosition);
					xImg = (startPoint1.X + endPoint1.X) / 2f;
					yImg = (startPoint1.Y + endPoint1.Y) / 2f;
				}
				else
				{
					startPosition = pdfViewer1.GetDocumentPosition(e.Location);
					endPosition = pdfViewer1.GetDocumentPosition(new PointF(e.Location.X + 10, e.Location.Y + 10));
					startPoint1 = pdfViewer1.GetClientPoint(startPosition);
					endPoint1 = pdfViewer1.GetClientPoint(endPosition);
					xImg = (startPoint1.X + endPoint1.X) / 2f;
					yImg = (startPoint1.Y + endPoint1.Y) / 2f;
				}
				pdfViewer1.Invalidate();
			}
			else if (isSigning)
			{
				startPosition = pdfViewer1.GetDocumentPosition(e.Location);
				endPosition = pdfViewer1.GetDocumentPosition(new PointF(e.Location.X + 10, e.Location.Y + 10));
				startPoint1 = pdfViewer1.GetClientPoint(startPosition);
				endPoint1 = pdfViewer1.GetClientPoint(endPosition);
				xImg = (startPoint1.X + endPoint1.X) / 2f;
				yImg = (startPoint1.Y + endPoint1.Y) / 2f;
				pdfViewer1.Refresh();
			}
		}

		private void pdfViewer1_MouseUp(object sender, MouseEventArgs e)
		{
			mouseButtonPressed = false;
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			pdfViewer1.Cursor = Cursors.Hand;
			if (startPosition != null && endPosition != null && isShowRangtax)
			{
				startPoint1 = pdfViewer1.GetClientPoint(startPosition);
				endPoint1 = pdfViewer1.GetClientPoint(endPosition);
				pageNumberCurrent = startPosition.PageNumber;
				xImg = (float)(startPosition.Point.X + endPosition.Point.X) / 2f;
				yImg = (float)(startPosition.Point.Y + endPosition.Point.Y) / 2f;
			}
			float num = 0f;
			float num2 = 0f;
			if (!(yImg > 0f) || !(xImg > 0f))
			{
				return;
			}
			num = xImg;
			if (num < 0f)
			{
				num = 0f;
			}
			num2 = yImg;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			if ((!hasNextSignPosition || nextSignPosition == null) && signedCount > 0)
			{
				LogSystem.Debug("Truong hop khong co toa do ky fix trong file, va so luong chu ky da ky > 0 ==> giam pageNumberCurrent ve dung gia tri trang hien tai (do co them trang ky o dau tien): pageNumberCurrent = pageNumberCurrent - 1.____" + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => pageNumberCurrent)), (object)pageNumberCurrent));
			}
			DisplayConfigDTO displayConfigByCommentOrDefault = GetDisplayConfigByCommentOrDefault();
			if (SignDigital(num, num2, pageNumberCurrent, totalPageNumber, displayConfigByCommentOrDefault))
			{
				signReason = "";
				txtSignDescription.Text = "";
			}
		}

		private void pdfViewer1_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				if (!pdfViewer1.IsDocumentOpened)
				{
					return;
				}
				Graphics graphics = e.Graphics;
				if (startPosition != null && endPosition != null && isShowRangtax && (isPatientSign || isHomeRelativeSign))
				{
					startPoint1 = pdfViewer1.GetClientPoint(startPosition);
					endPoint1 = pdfViewer1.GetClientPoint(endPosition);
					pageNumberCurrent = startPosition.PageNumber;
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Aqua)), RectangleF.FromLTRB(Math.Min(startPoint1.X, endPoint1.X), Math.Min(startPoint1.Y, endPoint1.Y), Math.Max(startPoint1.X, endPoint1.X), Math.Max(startPoint1.Y, endPoint1.Y)));
					xImg = (startPoint1.X + endPoint1.X) / 2f;
					yImg = (startPoint1.Y + endPoint1.Y) / 2f;
				}
				if (verifiers != null && verifiers.Count > 0 && pageNumberCurrent == 1)
				{
					return;
				}
				float num = 0f;
				float num2 = 0f;
				if (!(yImg > 0f) || !(xImg > 0f) || !isSigning || isPatientSign || isHomeRelativeSign)
				{
					return;
				}
				byte[] array = null;
				float num3 = 0f;
				Image val = null;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				num6 = ((typeDisplayOption <= 0) ? inputADOWorking.DisplayConfigDTO.TypeDisplay.GetValueOrDefault() : typeDisplayOption);
				if (Signer != null && Signer.SIGN_IMAGE != null)
				{
					val = Image.GetInstance(Signer.SIGN_IMAGE);
					array = Signer.SIGN_IMAGE;
				}
				else if (File.Exists(Path.Combine(Utils.SignatureFolder(), "NotImage.jpg")))
				{
					val = Image.GetInstance(Path.Combine(Utils.SignatureFolder(), "NotImage.jpg"));
					array = Utils.FileToByte(Path.Combine(Utils.SignatureFolder(), "NotImage.jpg"));
				}
				if (array == null || val == null)
				{
					return;
				}
				using (MemoryStream stream = new MemoryStream(array))
				{
					if (Signer != null && Signer.SIGNALTURE_IMAGE_WIDTH.HasValue)
					{
						decimal? sIGNALTURE_IMAGE_WIDTH = Signer.SIGNALTURE_IMAGE_WIDTH;
						if ((sIGNALTURE_IMAGE_WIDTH.GetValueOrDefault() > default(decimal)) & sIGNALTURE_IMAGE_WIDTH.HasValue)
						{
							num3 = (float)Signer.SIGNALTURE_IMAGE_WIDTH.Value;
						}
					}
					float plusH = SignPdfAsynchronous.ProcessHeightPlus(100f, inputADOWorking.DisplayConfigDTO.WidthRectangle.GetValueOrDefault());
					float num7 = SharedUtils.CalculateWidthPercent(inputADOWorking.DisplayConfigDTO.WidthRectangle.GetValueOrDefault(), inputADOWorking.DisplayConfigDTO.HeightRectangle.GetValueOrDefault(), val, num3, 100f, plusH);
					float num8 = 0f;
					int num9;
					int num10;
					if (num3 > 0f)
					{
						num9 = (int)num3;
						num10 = (int)(inputADOWorking.DisplayConfigDTO.HeightRectangle.GetValueOrDefault() * (num3 / inputADOWorking.DisplayConfigDTO.WidthRectangle).GetValueOrDefault());
					}
					else
					{
						num9 = (int)inputADOWorking.DisplayConfigDTO.WidthRectangle.GetValueOrDefault();
						num10 = (int)inputADOWorking.DisplayConfigDTO.HeightRectangle.GetValueOrDefault();
					}
					Size size;
					if (num6 == Constans.DISPLAY_IMAGE_STAMP)
					{
						size = ResizeFit(new Size((int)((Rectangle)val).Width, (int)((Rectangle)val).Height), new Size(num9 - 10, num10 - 10));
						Size size2 = ConstrainVerbose((int)((Rectangle)val).Width, (int)((Rectangle)val).Height, num9 - 10, num10 - 10);
					}
					else
					{
						size = ResizeFit(new Size((int)((Rectangle)val).Width, (int)((Rectangle)val).Height), new Size(num9 + 10, num10 + 10));
						Size size2 = ConstrainVerbose((int)((Rectangle)val).Width, (int)((Rectangle)val).Height, num9 + 10, num10 + 10);
					}
					num5 = size.Height;
					num4 = size.Width;
					image = new Bitmap(new Bitmap(stream), new Size(num4, num5));
					num = xImg - (float)(image.Width / 2);
					if (num < 0f)
					{
						num = 0f;
					}
					num2 = yImg - (float)(image.Height / 2);
					if (num2 < 0f)
					{
						num2 = 0f;
					}
					graphics.DrawImage(rect: new RectangleF(num, num2, image.Width, image.Height), image: image);
					graphics.DrawRectangle(penDrawSignal, num, num2, image.Width, image.Height);
				}
				val = null;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private Size ConstrainVerbose(int imageWidth, int imageHeight, int maxWidth, int maxHeight)
		{
			float num = (float)maxWidth / (float)maxHeight;
			float num2 = (float)imageWidth / (float)imageHeight;
			if (num <= num2 && imageWidth > maxWidth)
			{
				return new Size(maxWidth, (int)Math.Min(maxHeight, (float)maxWidth / num2));
			}
			if (num > num2 && imageHeight > maxHeight)
			{
				return new Size((int)Math.Min(maxWidth, (float)maxHeight * num2), maxHeight);
			}
			return new Size(imageWidth, imageHeight);
		}

		private Size ResizeFit(Size originalSize, Size maxSize)
		{
			double val = (double)maxSize.Width / (double)originalSize.Width;
			double val2 = (double)maxSize.Height / (double)originalSize.Height;
			double num = Math.Min(val, val2);
			return new Size((int)((double)originalSize.Width * num), (int)((double)originalSize.Height * num));
		}

		private void pdfViewer1_PageSetupDialogShowing(object sender, PdfPageSetupDialogShowingEventArgs e)
		{
			try
			{
				e.FormStartPosition = FormStartPosition.CenterScreen;
				int num = 600;
				int num2 = 400;
				if (Screen.PrimaryScreen != null)
				{
					num = ((Screen.PrimaryScreen.WorkingArea.Width > 400) ? (Screen.PrimaryScreen.WorkingArea.Width - 400) : 100);
					num2 = ((Screen.PrimaryScreen.WorkingArea.Height > 100) ? (Screen.PrimaryScreen.WorkingArea.Height - 100) : 50);
				}
				e.FormSize = new Size(num, num2);
				if (printNumberCopies > 1)
				{
					e.PrinterSettings.Settings.Copies = printNumberCopies;
				}
				if (!string.IsNullOrEmpty(inputADOWorking.PrinterDefault))
				{
					e.PrinterSettings.Settings.PrinterName = inputADOWorking.PrinterDefault;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void pdfViewer1_PopupMenuShowing(object sender, PdfPopupMenuShowingEventArgs e)
		{
			try
			{
				LogSystem.Info("pdfViewer1_PopupMenuShowing" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => inputADOWorking.IsPrint)), (object)inputADOWorking.IsPrint));
				if (bbtnPrint.Visibility == BarItemVisibility.Always && bbtnPrint.Enabled)
				{
					e.Menu.BeginUpdate();
					for (int num = e.Menu.ItemLinks.Count - 1; num >= 0; num--)
					{
						if (e.Menu.ItemLinks[num].Caption == "Print..." || e.Menu.ItemLinks[num].Caption == "Print")
						{
							e.Menu.ItemLinks.Remove(e.Menu.ItemLinks[num]);
						}
					}
					e.Menu.ItemLinks.Insert(3, new BarButtonItem());
					e.Menu.ItemLinks[3].Caption = "Print";
					e.Menu.ItemLinks[3].Item.Name = "PrintReport";
					e.Menu.ItemLinks[3].Item.ItemShortcut = new BarShortcut(Keys.P | Keys.Alt);
					e.Menu.ItemLinks[3].Item.ItemClick += bbtnPrint_ItemClick;
					e.Menu.ItemLinks[3].Item.Glyph = imageCollection1.Images[0];
					e.Menu.EndUpdate();
					return;
				}
				for (int num2 = e.Menu.ItemLinks.Count - 1; num2 >= 0; num2--)
				{
					if (e.Menu.ItemLinks[num2].Caption == "Print..." || e.Menu.ItemLinks[num2].Caption == "Print")
					{
						e.Menu.ItemLinks.Remove(e.Menu.ItemLinks[num2]);
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private DisplayConfigDTO GetDisplayConfigByCommentOrDefault(SignPositionADO autoSP = null)
		{
			SignPositionADO signPositionADO = null;
			if (signPositionADOs != null && signPositionADOs.Count > 0)
			{
				signPositionADO = signPositionADOs.FirstOrDefault((SignPositionADO o) => VerifySign.GetNumOderByCommentText(o.Text) == 0);
			}
			return new DisplayConfigDTO
			{
				HeightRectangle = ((autoSP != null && autoSP.HeightRectangle > 0f) ? new float?(autoSP.HeightRectangle) : ((signPositionADO != null && signPositionADO.HeightRectangle > 0f) ? new float?(signPositionADO.HeightRectangle) : ((inputADOWorking.DisplayConfigDTO != null) ? inputADOWorking.DisplayConfigDTO.HeightRectangle : ((float?)null)))),
				WidthRectangle = ((autoSP != null && autoSP.WidthRectangle > 0f) ? new float?(autoSP.WidthRectangle) : ((signPositionADO != null && signPositionADO.WidthRectangle > 0f) ? new float?(signPositionADO.WidthRectangle) : ((inputADOWorking.DisplayConfigDTO != null) ? inputADOWorking.DisplayConfigDTO.WidthRectangle : ((float?)null)))),
				SizeFont = ((autoSP != null && autoSP.SizeFont > 0) ? new int?(autoSP.SizeFont) : ((signPositionADO != null && signPositionADO.SizeFont > 0) ? new int?(signPositionADO.SizeFont) : ((inputADOWorking.DisplayConfigDTO != null) ? inputADOWorking.DisplayConfigDTO.SizeFont : ((int?)null)))),
				TextPosition = ((autoSP != null && autoSP.TextPosition > Constans.TEXT_POSITON.x100) ? new int?((int)autoSP.TextPosition) : ((signPositionADO != null && signPositionADO.TextPosition > Constans.TEXT_POSITON.x100) ? new int?((int)signPositionADO.TextPosition) : ((inputADOWorking.DisplayConfigDTO != null) ? inputADOWorking.DisplayConfigDTO.TextPosition : ((int?)null)))),
				TypeDisplay = ((typeDisplayOption > 0) ? new int?(typeDisplayOption) : ((autoSP != null && autoSP.TypeDisplay > 0) ? new int?(autoSP.TypeDisplay) : ((signPositionADO != null && signPositionADO.TypeDisplay > 0) ? new int?(signPositionADO.TypeDisplay) : ((inputADOWorking.DisplayConfigDTO != null) ? inputADOWorking.DisplayConfigDTO.TypeDisplay : ((int?)null))))),
				IsDisplaySignature = ((autoSP != null && autoSP.IsDisplaySignature.HasValue) ? autoSP.IsDisplaySignature : ((signPositionADO != null && signPositionADO.IsDisplaySignature.HasValue) ? signPositionADO.IsDisplaySignature : ((inputADOWorking.DisplayConfigDTO != null) ? inputADOWorking.DisplayConfigDTO.IsDisplaySignature : ((bool?)null)))),
				FormatRectangleText = ((inputADOWorking.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADOWorking.DisplayConfigDTO.FormatRectangleText)) ? inputADOWorking.DisplayConfigDTO.FormatRectangleText : null),
				Location = ((inputADOWorking.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADOWorking.DisplayConfigDTO.Location)) ? inputADOWorking.DisplayConfigDTO.Location : null),
				Alignment = ((inputADOWorking.DisplayConfigDTO != null && inputADOWorking.DisplayConfigDTO.Alignment.HasValue) ? inputADOWorking.DisplayConfigDTO.Alignment : ((int?)null)),
				IsBold = ((inputADOWorking.DisplayConfigDTO != null && inputADOWorking.DisplayConfigDTO.IsBold.HasValue) ? inputADOWorking.DisplayConfigDTO.IsBold : ((bool?)null)),
				IsItalic = ((inputADOWorking.DisplayConfigDTO != null && inputADOWorking.DisplayConfigDTO.IsItalic.HasValue) ? inputADOWorking.DisplayConfigDTO.IsItalic : ((bool?)null)),
				IsUnderlined = ((inputADOWorking.DisplayConfigDTO != null && inputADOWorking.DisplayConfigDTO.IsUnderlined.HasValue) ? inputADOWorking.DisplayConfigDTO.IsUnderlined : ((bool?)null)),
				FontName = ((inputADOWorking.DisplayConfigDTO != null && !string.IsNullOrEmpty(inputADOWorking.DisplayConfigDTO.FontName)) ? inputADOWorking.DisplayConfigDTO.FontName : null)
			};
		}

		private void bbtnPrint_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (bbtnPrint.Enabled && bbtnPrint.Visibility != BarItemVisibility.Never)
				{
					if (pdfViewer1.IsDocumentOpened)
					{
						Print();
						return;
					}
					MessageBox.Show(MessageUitl.GetMessage("KhongTimThayVanBanDeIn"));
					LogSystem.Warn(MessageUitl.GetMessage("KhongTimThayVanBanDeIn"));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
			}
		}

		private void bbtnSendERM_ItemClick(object sender, ItemClickEventArgs e)
		{
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Expected O, but got Unknown
			//IL_0176: Unknown result type (might be due to invalid IL or missing references)
			//IL_0180: Expected O, but got Unknown
			try
			{
				CommonParam param = new CommonParam();
				SignHandle signHandle = (string.IsNullOrEmpty(inputFileWork) ? new SignHandle(inputStream, 0f, 0f, 0, 0, CancelSign, null, documentName, treatmentCode, signName, signReason, verifiers, signType, listSign, null, isPatientSign, isHomeRelativeSign, DocumentTypeId, isMultiSign, hisCode, inputADOWorking, null, param, base.ParentForm, GetCheckSignParanel(), Treatment, Signer, TokenCode, null) : new SignHandle(inputFileWork, 0f, 0f, 0, 0, CancelSign, null, documentName, treatmentCode, signName, signReason, verifiers, signType, listSign, null, isPatientSign, isHomeRelativeSign, DocumentTypeId, isMultiSign, hisCode, inputADOWorking, null, param, base.ParentForm, GetCheckSignParanel(), Treatment, Signer, TokenCode, null));
				DocumentTDO val = new DocumentTDO();
				val.IsSignParallel = GetCheckSignParanel();
				signHandle.SetFileType(fileType);
				val.OriginalVersion = new VersionTDO();
				if (fileADOMain != null && !string.IsNullOrEmpty(fileADOMain.Base64FileContent))
				{
					val.OriginalVersion.Base64Data = fileADOMain.Base64FileContent;
				}
				if (fileADOXml != null && !string.IsNullOrEmpty(fileADOXml.Base64FileContent))
				{
					val.OriginalVersion.Base64DataXml = fileADOXml.Base64FileContent;
				}
				if (fileADOJson != null && !string.IsNullOrEmpty(fileADOJson.Base64FileContent))
				{
					val.OriginalVersion.Base64DataJson = fileADOJson.Base64FileContent;
				}
				LogSystem.Info("document " + LogUtil.TraceData("signProcessor.SendDocument(document) ", (object)val));
				currentDocument = signHandle.SendDocument(val);
				if (currentDocument != null && !string.IsNullOrEmpty(currentDocument.DocumentCode))
				{
					LogSystem.Debug("documentCode: " + currentDocument.DocumentCode);
					UpdateStateIGSys((listSign != null && listSign.Count > 0) ? "05" : "03");
					listSign = new List<SignTDO>();
					bbtnSendERM.Enabled = false;
					bbtnchkSignParanel.Enabled = false;
					bbtnListSign.Visibility = BarItemVisibility.Always;
					bbtnAttackMentsMenu1.Visibility = BarItemVisibility.Always;
					bbtnAttackMentsMenu1.Enabled = true;
					if (string.IsNullOrEmpty(inputADOWorking.BusinessCode))
					{
						if (currentDocument.Signs != null && currentDocument.Signs.Count > 0)
						{
							signSelected = new EmrSign().GetSignDocumentFirst(currentDocument.DocumentCode, Signer, Treatment, isMultiSign, false);
							bbtnSignEnd.Enabled = signSelected != null && signSelected.IS_SIGNING == 1;
						}
						else if (XtraMessageBox.Show(MessageUitl.GetMessage("BanCoMuonTaoThemLuongKy"), MessageUitl.GetMessage("ThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && dlgOpenModuleConfig != null)
						{
							dlgOpenModuleConfig(currentDocument);
						}
					}
					if (bbtnConfigBussinessMenu1.Visibility == BarItemVisibility.Never && bbtnAttackMentsMenu1.Visibility == BarItemVisibility.Never)
					{
						VisibleButonOrther(false);
					}
					else
					{
						VisibleButonOrther(true);
					}
					MessageManager.Show(param, true);
				}
				else
				{
					LogSystem.Error("Khong tao duoc documentCode");
					MessageManager.Show(param, false);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnSign_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				bool success = false;
				isPatientSign = false;
				isHomeRelativeSign = false;
				typeDisplayOption = -1;
				if (isSigning)
				{
					CancelSign();
					return;
				}
				signReason = txtSignDescription.Text;
				if (!VerifyWithExistsDocument(currentDocument))
				{
					CancelSign();
					return;
				}
				bool? flag = VerifySign.VerifySignImageWithOption(inputADOWorking, Signer, hasNextSignPosition, nextSignPosition, isMultiSign);
				if (flag.HasValue)
				{
					if (!flag.Value)
					{
						CancelSign();
						return;
					}
					typeDisplayOption = Constans.DISPLAY_RECTANGLE_TEXT;
				}
				if (hasNextSignPosition && nextSignPosition != null && (signSelected == null || (signSelected != null && signSelected.SIGN_TIME.GetValueOrDefault() <= 0)))
				{
					if (signAutoPositionADOs != null && signAutoPositionADOs.Count > 0)
					{
						List<SignPositionADO> SignPositionAutoForAdds = signAutoPositionADOs.OrderBy((SignPositionADO o) => o.Text).ToList();
						bool isMultiSignForAuto = false;
						if (!isMultiSign && SignPositionAutoForAdds != null && SignPositionAutoForAdds.Count >= 2)
						{
							isMultiSignForAuto = true;
						}
						int demKey = 1;
						foreach (SignPositionADO nSp in SignPositionAutoForAdds)
						{
							bool isMultiSignForProcess = isMultiSign;
							if (isMultiSignForAuto && demKey == SignPositionAutoForAdds.Count)
							{
								isMultiSignForProcess = false;
							}
							else if (isMultiSignForAuto)
							{
								isMultiSignForProcess = true;
							}
							else
							{
								isMultiSignForProcess = isMultiSign;
							}
							LogSystem.Info("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nSp)), (object)nSp) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isMultiSignForProcess)), (object)isMultiSignForProcess) + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => SignPositionAutoForAdds.Count)), (object)SignPositionAutoForAdds.Count) + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => demKey)), (object)demKey) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isMultiSignForAuto)), (object)isMultiSignForAuto));
							float left = nSp.Reactanle.Left;
							left = ((left < 0f) ? 0f : left);
							float bottom = nSp.Reactanle.Bottom;
							bottom = ((bottom < 0f) ? 0f : bottom);
							DisplayConfigDTO displayConfigByCommentOrDefault = GetDisplayConfigByCommentOrDefault(nSp);
							success = SignDigital(left, bottom, nSp.PageNUm, totalPageNumber, displayConfigByCommentOrDefault, isMultiSignForProcess);
							demKey++;
						}
					}
					else
					{
						LogSystem.Info("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nextSignPosition)), (object)nextSignPosition));
						float left2 = nextSignPosition.Reactanle.Left;
						left2 = ((left2 < 0f) ? 0f : left2);
						float bottom2 = nextSignPosition.Reactanle.Bottom;
						bottom2 = ((bottom2 < 0f) ? 0f : bottom2);
						DisplayConfigDTO displayConfigByCommentOrDefault2 = GetDisplayConfigByCommentOrDefault(nextSignPosition);
						success = SignDigital(left2, bottom2, nextSignPosition.PageNUm, totalPageNumber, displayConfigByCommentOrDefault2);
					}
					if (success)
					{
						signReason = "";
						txtSignDescription.Text = "";
					}
				}
				else if (fileType == FileType.Json || fileType == FileType.Xml)
				{
					DisplayConfigDTO displayConfigByCommentOrDefault3 = GetDisplayConfigByCommentOrDefault();
					success = SignDigital(1f, 1f, 1, 1, displayConfigByCommentOrDefault3);
					if (success)
					{
						signReason = "";
						txtSignDescription.Text = "";
					}
				}
				else
				{
					isSigning = true;
					bbtnSign.Caption = "Hủy";
					pdfViewer1.MouseDown += pdfViewer1_MouseDown;
					pdfViewer1.MouseMove += pdfViewer1_MouseMove;
					pdfViewer1.MouseUp += pdfViewer1_MouseUp;
					pdfViewer1.Cursor = Cursors.Cross;
				}
				LogSystem.Debug("bbtnSign_ItemClick____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => success)), (object)success));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnPatientSign_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				bool flag = false;
				isPatientSign = true;
				isHomeRelativeSign = false;
				if (isSigning)
				{
					CancelSign();
				}
				else
				{
					if (!VerifyWithExistsDocument(currentDocument, GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION != "3" || GlobalStore.EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION == "1") || !VerifySignPad())
					{
						return;
					}
					signReason = txtSignDescription.Text;
					if (hasNextSignPosition && nextSignPosition != null && (signSelected == null || (signSelected != null && signSelected.SIGN_TIME.GetValueOrDefault() <= 0)))
					{
						if (signAutoPositionADOs != null && signAutoPositionADOs.Count > 0)
						{
							List<SignPositionADO> SignPositionAutoForAdds = signAutoPositionADOs.OrderBy((SignPositionADO o) => o.Text).ToList();
							bool isMultiSignForAuto = false;
							if (!isMultiSign && SignPositionAutoForAdds != null && SignPositionAutoForAdds.Count >= 2)
							{
								isMultiSignForAuto = true;
							}
							int demKey = 1;
							foreach (SignPositionADO nSp in SignPositionAutoForAdds)
							{
								bool isMultiSignForProcess = isMultiSign;
								if (isMultiSignForAuto && demKey == SignPositionAutoForAdds.Count)
								{
									isMultiSignForProcess = false;
								}
								else if (isMultiSignForAuto)
								{
									isMultiSignForProcess = isMultiSignForAuto;
								}
								else
								{
									isMultiSignForProcess = isMultiSign;
								}
								LogSystem.Debug("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nSp)), (object)nSp) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isMultiSignForProcess)), (object)isMultiSignForProcess) + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => SignPositionAutoForAdds.Count)), (object)SignPositionAutoForAdds.Count) + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => demKey)), (object)demKey) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isMultiSignForAuto)), (object)isMultiSignForAuto));
								float left = nSp.Reactanle.Left;
								left = ((left < 0f) ? 0f : left);
								float bottom = nSp.Reactanle.Bottom;
								bottom = ((bottom < 0f) ? 0f : bottom);
								DisplayConfigDTO displayConfigByCommentOrDefault = GetDisplayConfigByCommentOrDefault(nSp);
								flag = SignDigital(left, bottom, nSp.PageNUm, totalPageNumber, displayConfigByCommentOrDefault, isMultiSignForProcess);
								demKey++;
							}
						}
						else
						{
							LogSystem.Debug("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nextSignPosition)), (object)nextSignPosition));
							float left2 = nextSignPosition.Reactanle.Left;
							left2 = ((left2 < 0f) ? 0f : left2);
							float bottom2 = nextSignPosition.Reactanle.Bottom;
							bottom2 = ((bottom2 < 0f) ? 0f : bottom2);
							DisplayConfigDTO displayConfigByCommentOrDefault2 = GetDisplayConfigByCommentOrDefault(nextSignPosition);
							flag = SignDigital(left2, bottom2, nextSignPosition.PageNUm, totalPageNumber, displayConfigByCommentOrDefault2);
						}
						if (flag)
						{
							signReason = "";
							txtSignDescription.Text = "";
						}
					}
					else if (fileType == FileType.Json || fileType == FileType.Xml)
					{
						DisplayConfigDTO displayConfigByCommentOrDefault3 = GetDisplayConfigByCommentOrDefault();
						flag = SignDigital(1f, 1f, 1, 1, displayConfigByCommentOrDefault3);
						if (flag)
						{
							signReason = "";
							txtSignDescription.Text = "";
						}
					}
					else
					{
						isSigning = true;
						bbtnPatientSign.Caption = "Hủy";
						pdfViewer1.MouseDown += pdfViewer1_MouseDown;
						pdfViewer1.MouseMove += pdfViewer1_MouseMove;
						pdfViewer1.MouseUp += pdfViewer1_MouseUp;
						pdfViewer1.Cursor = Cursors.Cross;
					}
					if (flag && reload != null)
					{
						reload();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
			}
		}

		private void bbtnRelativeHomeSign_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				bool flag = false;
				isHomeRelativeSign = true;
				isPatientSign = false;
				if (isSigning)
				{
					CancelSign();
				}
				else
				{
					if (!VerifyWithExistsDocument(currentDocument, GlobalStore.EMR__EMR_DOCUMENT__PATIENT_SIGN__OPTION != "3" || GlobalStore.EMR_EMR_DOCUMENT_PATIENT_SIGN_FIRST_OPTION == "1"))
					{
						return;
					}
					signReason = txtSignDescription.Text;
					if (hasNextSignPosition && nextSignPosition != null && (signSelected == null || (signSelected != null && signSelected.SIGN_TIME.GetValueOrDefault() <= 0)))
					{
						if (signAutoPositionADOs != null && signAutoPositionADOs.Count > 0)
						{
							List<SignPositionADO> SignPositionAutoForAdds = signAutoPositionADOs.OrderBy((SignPositionADO o) => o.Text).ToList();
							bool isMultiSignForAuto = false;
							if (!isMultiSign && SignPositionAutoForAdds != null && SignPositionAutoForAdds.Count >= 2)
							{
								isMultiSignForAuto = true;
							}
							int demKey = 1;
							foreach (SignPositionADO nSp in SignPositionAutoForAdds)
							{
								bool isMultiSignForProcess = isMultiSign;
								if (isMultiSignForAuto && demKey == SignPositionAutoForAdds.Count)
								{
									isMultiSignForProcess = false;
								}
								else if (isMultiSignForAuto)
								{
									isMultiSignForProcess = isMultiSignForAuto;
								}
								else
								{
									isMultiSignForProcess = isMultiSign;
								}
								LogSystem.Debug("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nSp)), (object)nSp) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isMultiSignForProcess)), (object)isMultiSignForProcess) + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => SignPositionAutoForAdds.Count)), (object)SignPositionAutoForAdds.Count) + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => demKey)), (object)demKey) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isMultiSignForAuto)), (object)isMultiSignForAuto));
								float left = nSp.Reactanle.Left;
								left = ((left < 0f) ? 0f : left);
								float bottom = nSp.Reactanle.Bottom;
								bottom = ((bottom < 0f) ? 0f : bottom);
								DisplayConfigDTO displayConfigByCommentOrDefault = GetDisplayConfigByCommentOrDefault(nSp);
								flag = SignDigital(left, bottom, nSp.PageNUm, totalPageNumber, displayConfigByCommentOrDefault, isMultiSignForProcess);
								demKey++;
							}
						}
						else
						{
							LogSystem.Debug("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nextSignPosition)), (object)nextSignPosition));
							float left2 = nextSignPosition.Reactanle.Left;
							left2 = ((left2 < 0f) ? 0f : left2);
							float bottom2 = nextSignPosition.Reactanle.Bottom;
							bottom2 = ((bottom2 < 0f) ? 0f : bottom2);
							DisplayConfigDTO displayConfigByCommentOrDefault2 = GetDisplayConfigByCommentOrDefault(nextSignPosition);
							flag = SignDigital(left2, bottom2, nextSignPosition.PageNUm, totalPageNumber, displayConfigByCommentOrDefault2);
						}
					}
					else if (fileType == FileType.Json || fileType == FileType.Xml)
					{
						DisplayConfigDTO displayConfigByCommentOrDefault3 = GetDisplayConfigByCommentOrDefault();
						flag = SignDigital(1f, 1f, 1, 1, displayConfigByCommentOrDefault3);
						if (flag)
						{
							signReason = "";
							txtSignDescription.Text = "";
						}
					}
					else
					{
						isSigning = true;
						bbtnRelativeHomeSign.Caption = "Hủy";
						pdfViewer1.MouseDown += pdfViewer1_MouseDown;
						pdfViewer1.MouseMove += pdfViewer1_MouseMove;
						pdfViewer1.MouseUp += pdfViewer1_MouseUp;
						pdfViewer1.Cursor = Cursors.Cross;
					}
				}
				if (flag)
				{
					signReason = "";
					txtSignDescription.Text = "";
					if (reload != null)
					{
						reload();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
			}
		}

		private void bbtnConfigSign_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (currentDocument != null && !string.IsNullOrEmpty(currentDocument.DocumentCode))
				{
					if (dlgOpenModuleConfig != null)
					{
						dlgOpenModuleConfig(currentDocument);
					}
					UpdateAfterAddSignThread(listSign);
				}
				else
				{
					frmSignerAdd frmSignerAdd2 = new frmSignerAdd(listSign, UpdateAfterAddSignThread, SignParanelEditValueChanged, bbtnchkSignParanel.Checked, bbtnchkSignParanel.Enabled, "", Treatment, Signer, IsAddPatientSign, delegate
					{
						bbtnSendERM_ItemClick(null, null);
					});
					frmSignerAdd2.ReloadDocument = (GetDocument)Delegate.Combine(frmSignerAdd2.ReloadDocument, new GetDocument(GetDocumentWhenCreate));
					frmSignerAdd2.ShowDialog(base.ParentForm);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private string GetDocumentWhenCreate()
		{
			return (currentDocument != null) ? currentDocument.DocumentCode : null;
		}

		private void bbtnRejectSign_ItemClick(object sender, ItemClickEventArgs e)
		{
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Expected O, but got Unknown
			//IL_0182: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Expected O, but got Unknown
			try
			{
				if (currentDocument == null || string.IsNullOrEmpty(currentDocument.DocumentCode))
				{
					MessageManager.Show(MessageUitl.GetMessage("KhongTonTaiVanBanDeHuyKy"));
					return;
				}
				EMR_SIGN signDocumentFirst = new EmrSign().GetSignDocumentFirst(currentDocument.DocumentCode, Signer, Treatment, isMultiSign, true);
				if (signDocumentFirst == null)
				{
					MessageManager.Show(MessageUitl.GetMessage("KhongXacDinhDuocDuLieuDeHuyKy"));
					return;
				}
				frmRejectInfo frmRejectInfo2 = new frmRejectInfo(UpdateRejectInfo);
				frmRejectInfo2.ShowDialog();
				if (!string.IsNullOrEmpty(rejectReason))
				{
					CommonParam param = new CommonParam();
					EmrSign emrSign = new EmrSign(param);
					EmrSignRejectSDO val = new EmrSignRejectSDO();
					val.EmrSignId = ((signDocumentFirst != null) ? signDocumentFirst.ID : 0);
					val.RejectReason = rejectReason;
					val.RejectTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
					val.RoomCode = roomCode;
					val.RoomTypeCode = roomTypeCode;
					bool flag = emrSign.Reject(TokenCode, val);
					MessageManager.Show(param, flag);
					if (flag)
					{
						EnableSignButton(false);
						UpdateStateIGSys("02");
						inputStream = null;
						ProcessStoreCurrentFileToPrint(inputFileWork);
						readerWorking = new PdfReader(inputFileWork);
						ProcessSignPdf();
						currentPageSettings = PdfDocumentProcess.GetPaperSize(inputFileWork);
					}
				}
			}
			catch (Exception ex)
			{
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
				LogSystem.Warn(ex);
			}
		}

		private void bbtnSignEnd_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				CommonParam param = new CommonParam();
				EMR_SIGN val = new EmrSign(param).SignEnd(TokenCode, signSelected.ID);
				bool flag = val != null;
				if (flag)
				{
					EnableSignButton(false);
					bbtnSignEnd.Enabled = false;
					bbtnPatientSign.Enabled = (inputADOWorking.IsShowPatientSign ? true : false);
					UpdateStateIGSys("00");
				}
				MessageManager.Show(param, flag);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				MessageManager.Show(MessageUitl.GetMessage("CoSuCoXayRaVuiLongKiemTraLaiHoacLienHeVoiQuanTriHeThongDeDuocHoTro"));
			}
		}

		private void bbtnListSign_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (currentDocument != null && !string.IsNullOrEmpty(currentDocument.DocumentCode))
				{
					FormEmrSign formEmrSign = new FormEmrSign(currentDocument.DocumentCode);
					formEmrSign.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnResetChkState_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				CacheClientWorker.ChangeValue("");
				MessageBox.Show(MessageUitl.GetMessage("ResetTrangThaiNguoiDungDaLuuTaiMayTram"));
				typeDisplayOption = -1;
				GlobalStore.EmrConfigs = null;
				GlobalStore.EmrBusiness = null;
				List<EMR_CONFIG> emrConfigs = GlobalStore.EmrConfigs;
				List<EMR_BUSINESS> emrBusiness = GlobalStore.EmrBusiness;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnViewPACSImage_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(vlViewPACSUrlFormat))
				{
					return;
				}
				string newValue = "";
				string newValue2 = "";
				if (!string.IsNullOrEmpty(currentDocument.HisCode))
				{
					string[] array = currentDocument.HisCode.Split(new string[1] { " " }, StringSplitOptions.None);
					if (array != null && array.Length != 0)
					{
						string[] array2 = array;
						foreach (string text in array2)
						{
							string[] array3 = text.Split(new string[1] { ":" }, StringSplitOptions.None);
							if (text.Contains("SERVICE_REQ_CODE:"))
							{
								if (array3 != null && array3.Length > 1)
								{
									newValue2 = array3[1];
								}
							}
							else if (text.Contains("SER_SERV_ID:") && array3 != null && array3.Length > 1)
							{
								newValue = array3[1];
							}
						}
					}
				}
				string stringToEscape = vlViewPACSUrlFormat.Replace("<#TREATMENT_CODE;>", Treatment.TREATMENT_CODE).Replace("<#DOCUMENT_CODE;>", currentDocument.DocumentCode).Replace("<#PATIENT_CODE;>", Treatment.PATIENT_CODE)
					.Replace("<#HIS_CODE;>", currentDocument.HisCode)
					.Replace("<#SERE_SERV_ID;>", newValue)
					.Replace("<#SERVICE_REQ_CODE;>", newValue2)
					.Replace("<#DOCUMENT_TIME;>", currentDocument.DocumentTime.HasValue ? currentDocument.DocumentTime.ToString() : "")
					.Replace("<#DOCUMENT_DATE;>", currentDocument.DocumentTime.HasValue ? currentDocument.DocumentTime.ToString().Substring(0, 8) : "");
				new LaunchBrowse().Launch(Uri.EscapeUriString(stringToEscape));
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnCtrlShiftU_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				MessageBox.Show("LOGINNAME:" + Signer.LOGINNAME + ", TREATMENT_CODE:" + Treatment.TREATMENT_CODE + ", TokenCode:" + TokenCode);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnChkCloseAfterSign_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (!isInitForm)
				{
					if (dlgCloseAfterSignCheckedChanged != null)
					{
						dlgCloseAfterSignCheckedChanged(bbtnChkCloseAfterSign.Checked);
					}
					isCloseAfterSign = bbtnChkCloseAfterSign.Checked;
					inputADOWorking.IsCloseAfterSign = bbtnChkCloseAfterSign.Checked;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnConfigBussinessMenu_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				List<EMR_BUSINESS> list = null;
				if (GlobalStore.EmrBusiness != null && GlobalStore.EmrBusiness.Count > 0)
				{
					list = ((printTypeBusinessCodes == null || printTypeBusinessCodes.Count <= 0) ? GlobalStore.EmrBusiness.Where((EMR_BUSINESS o) => o.IS_ACTIVE == 1 && ((GlobalStore.EMR_EMR_BUSINESS_SHOW_ALL_TEMPLATES == "1" && o.CREATOR == GlobalStore.LoginName) || (GlobalStore.EMR_EMR_BUSINESS_SHOW_ALL_TEMPLATES != "1" && o.DEPARTMENT_CODE == null) || string.IsNullOrEmpty(inputADOWorking.DepartmentCode) || inputADOWorking.DepartmentCode == o.DEPARTMENT_CODE)).ToList() : GlobalStore.EmrBusiness.Where((EMR_BUSINESS o) => printTypeBusinessCodes.Contains(o.BUSINESS_CODE) || (o.IS_ACTIVE == 1 && ((GlobalStore.EMR_EMR_BUSINESS_SHOW_ALL_TEMPLATES == "1" && o.CREATOR == GlobalStore.LoginName) || (GlobalStore.EMR_EMR_BUSINESS_SHOW_ALL_TEMPLATES != "1" && o.DEPARTMENT_CODE == null) || string.IsNullOrEmpty(inputADOWorking.DepartmentCode) || inputADOWorking.DepartmentCode == o.DEPARTMENT_CODE))).ToList());
				}
				if (list != null && list.Count > 0)
				{
					list = list.OrderByDescending((EMR_BUSINESS o) => o.CREATE_TIME).ToList();
				}
				frmCreateEmrBusiness frmCreateEmrBusiness2 = new frmCreateEmrBusiness(inputADOWorking.DepartmentCode, list, listSign, UpdateAfterAddSignThread, businessCode, delegate
				{
					bbtnSendERM_ItemClick(null, null);
				});
				frmCreateEmrBusiness2.ReloadDocument = (GetDocument)Delegate.Combine(frmCreateEmrBusiness2.ReloadDocument, new GetDocument(GetDocumentWhenCreate));
				frmCreateEmrBusiness2.ShowDialog();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnAttackMentsMenu_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (currentDocument != null && !string.IsNullOrEmpty(currentDocument.DocumentCode))
				{
					frmAttachMents frmAttachMents2 = new frmAttachMents(currentDocument.DocumentCode);
					frmAttachMents2.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnSignType_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (!isInitForm && bbtnSignType.Visibility != BarItemVisibility.Never)
				{
					CacheClientWorker.ChangeValue("SignTypeOption", bbtnSignType.Checked ? "1" : "0");
					isOptionSignType = bbtnSignType.Checked;
					inputADOWorking.IsOptionSignType = bbtnSignType.Checked;
					signType = (bbtnSignType.Checked ? SignType.USB : SignType.HMS);
					inputADOWorking.SignType = signType;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void barCheckUsingSignPad_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			try
			{
				isUsingSignPad = barCheckUsingSignPad.Checked;
				if (actChangeUsingSignPad != null)
				{
					actChangeUsingSignPad(barCheckUsingSignPad.Checked);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void UCViewer_Resize(object sender, EventArgs e)
		{
			try
			{
				if (IsLoadFirst)
				{
					CaculateSizeItemBarmanager();
				}
				IsLoadFirst = false;
				if (base.Size.Width - 50 <= sizeToSignAndDeskcription)
				{
					bbtnSignAndDeskcription.Visibility = BarItemVisibility.Never;
					bbtnSignAndDeskcriptionOther.Visibility = BarItemVisibility.Always;
					bbtnOther.Visibility = BarItemVisibility.Always;
				}
				else
				{
					bbtnSignAndDeskcription.Visibility = BarItemVisibility.Always;
					bbtnSignAndDeskcriptionOther.Visibility = BarItemVisibility.Never;
					bbtnOther.Visibility = BarItemVisibility.Never;
				}
				if (base.Size.Width - 50 <= sizeToRelativeHomeSign)
				{
					bbtnRelativeHomeSign.Visibility = BarItemVisibility.Never;
					bbtnRelativeHomeSignOther.Visibility = BarItemVisibility.Always;
					bbtnOther.Visibility = BarItemVisibility.Always;
				}
				else
				{
					bbtnRelativeHomeSign.Visibility = BarItemVisibility.Always;
					bbtnRelativeHomeSignOther.Visibility = BarItemVisibility.Never;
					bbtnOther.Visibility = BarItemVisibility.Never;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void CaculateSizeItemBarmanager()
		{
			try
			{
				foreach (object itemLink in pdfCommandBar1.ItemLinks)
				{
					if (itemLink.GetType().Equals(typeof(BarButtonItemLink)))
					{
						BarButtonItemLink barButtonItemLink = itemLink as BarButtonItemLink;
						total += barButtonItemLink.Bounds.Width;
						if (!IsNotCaculateSignAndDeskcription)
						{
							sizeToSignAndDeskcription += barButtonItemLink.Bounds.Width;
						}
						if (!IsNotCaculateRelativeHomeSign)
						{
							sizeToRelativeHomeSign += barButtonItemLink.Bounds.Width;
						}
						if (barButtonItemLink.ItemId == bbtnSignAndDeskcription.Id)
						{
							IsNotCaculateSignAndDeskcription = true;
						}
						if (barButtonItemLink.ItemId == bbtnRelativeHomeSign.Id)
						{
							IsNotCaculateRelativeHomeSign = true;
						}
					}
					else if (itemLink.GetType().Equals(typeof(BarSubItemLink)))
					{
						BarSubItemLink barSubItemLink = itemLink as BarSubItemLink;
						total += barSubItemLink.Bounds.Width;
						if (!IsNotCaculateSignAndDeskcription)
						{
							sizeToSignAndDeskcription += barSubItemLink.Bounds.Width;
						}
						if (!IsNotCaculateRelativeHomeSign)
						{
							sizeToRelativeHomeSign += barSubItemLink.Bounds.Width;
						}
						if (barSubItemLink.ItemId == bbtnSignAndDeskcription.Id)
						{
							IsNotCaculateSignAndDeskcription = true;
						}
						if (barSubItemLink.ItemId == bbtnRelativeHomeSign.Id)
						{
							IsNotCaculateRelativeHomeSign = true;
						}
					}
					else if (itemLink.GetType().Equals(typeof(BarCheckItemLink)))
					{
						BarCheckItemLink barCheckItemLink = itemLink as BarCheckItemLink;
						total += barCheckItemLink.Bounds.Width;
						if (!IsNotCaculateSignAndDeskcription)
						{
							sizeToSignAndDeskcription += barCheckItemLink.Bounds.Width;
						}
						if (!IsNotCaculateRelativeHomeSign)
						{
							sizeToRelativeHomeSign += barCheckItemLink.Bounds.Width;
						}
						if (barCheckItemLink.ItemId == bbtnSignAndDeskcription.Id)
						{
							IsNotCaculateSignAndDeskcription = true;
						}
						if (barCheckItemLink.ItemId == bbtnRelativeHomeSign.Id)
						{
							IsNotCaculateRelativeHomeSign = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				timerLoadSinglePage.Stop();
				pdfViewer1.ZoomMode = PdfZoomMode.ActualSize;
				if (currentEmrDocument != null && currentEmrDocument.VIEW_ZOOM_PERCENT.HasValue)
				{
					pdfViewer1.ZoomFactor = currentEmrDocument.VIEW_ZOOM_PERCENT.Value;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnSignAndDeskcription_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				bool success = false;
				isPatientSign = false;
				isHomeRelativeSign = false;
				typeDisplayOption = -1;
				if (isSigning)
				{
					CancelSign();
					return;
				}
				signReason = "";
				frmAddNote frmAddNote2 = new frmAddNote(UpdateNote);
				frmAddNote2.ShowDialog();
				if (string.IsNullOrWhiteSpace(signReason))
				{
					MessageManager.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
					CancelSign();
					return;
				}
				if (!VerifyWithExistsDocument(currentDocument))
				{
					CancelSign();
					return;
				}
				bool? flag = VerifySign.VerifySignImageWithOption(inputADOWorking, Signer, hasNextSignPosition, nextSignPosition, isMultiSign);
				if (flag.HasValue)
				{
					if (!flag.Value)
					{
						CancelSign();
						return;
					}
					typeDisplayOption = Constans.DISPLAY_RECTANGLE_TEXT;
				}
				if (hasNextSignPosition && nextSignPosition != null && (signSelected == null || (signSelected != null && signSelected.SIGN_TIME.GetValueOrDefault() <= 0)))
				{
					if (signAutoPositionADOs != null && signAutoPositionADOs.Count > 0)
					{
						List<SignPositionADO> SignPositionAutoForAdds = signAutoPositionADOs.OrderBy((SignPositionADO o) => o.Text).ToList();
						bool isMultiSignForAuto = false;
						if (!isMultiSign && SignPositionAutoForAdds != null && SignPositionAutoForAdds.Count >= 2)
						{
							isMultiSignForAuto = true;
						}
						int demKey = 1;
						foreach (SignPositionADO nSp in SignPositionAutoForAdds)
						{
							bool isMultiSignForProcess = isMultiSign;
							if (isMultiSignForAuto && demKey == SignPositionAutoForAdds.Count)
							{
								isMultiSignForProcess = false;
							}
							else if (isMultiSignForAuto)
							{
								isMultiSignForProcess = true;
							}
							else
							{
								isMultiSignForProcess = isMultiSign;
							}
							LogSystem.Debug("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nSp)), (object)nSp) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isMultiSignForProcess)), (object)isMultiSignForProcess) + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => SignPositionAutoForAdds.Count)), (object)SignPositionAutoForAdds.Count) + LogUtil.TraceData(LogUtil.GetMemberName<int>((Expression<Func<int>>)(() => demKey)), (object)demKey) + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => isMultiSignForAuto)), (object)isMultiSignForAuto));
							float left = nSp.Reactanle.Left;
							left = ((left < 0f) ? 0f : left);
							float bottom = nSp.Reactanle.Bottom;
							bottom = ((bottom < 0f) ? 0f : bottom);
							DisplayConfigDTO displayConfigByCommentOrDefault = GetDisplayConfigByCommentOrDefault(nSp);
							success = SignDigital(left, bottom, nSp.PageNUm, totalPageNumber, displayConfigByCommentOrDefault, isMultiSignForProcess);
							demKey++;
						}
					}
					else
					{
						LogSystem.Debug("Truong hop van ban ky co comment danh dau vi tri can ky." + LogUtil.TraceData(LogUtil.GetMemberName<SignPositionADO>((Expression<Func<SignPositionADO>>)(() => nextSignPosition)), (object)nextSignPosition));
						float left2 = nextSignPosition.Reactanle.Left;
						left2 = ((left2 < 0f) ? 0f : left2);
						float bottom2 = nextSignPosition.Reactanle.Bottom;
						bottom2 = ((bottom2 < 0f) ? 0f : bottom2);
						DisplayConfigDTO displayConfigByCommentOrDefault2 = GetDisplayConfigByCommentOrDefault(nextSignPosition);
						success = SignDigital(left2, bottom2, nextSignPosition.PageNUm, totalPageNumber, displayConfigByCommentOrDefault2);
					}
					if (success)
					{
						signReason = "";
						txtSignDescription.Text = "";
					}
				}
				else
				{
					isSigning = true;
					bbtnSign.Caption = "Hủy";
					pdfViewer1.MouseDown += pdfViewer1_MouseDown;
					pdfViewer1.MouseMove += pdfViewer1_MouseMove;
					pdfViewer1.MouseUp += pdfViewer1_MouseUp;
					pdfViewer1.Cursor = Cursors.Cross;
				}
				LogSystem.Debug("bbtnSign_ItemClick____" + LogUtil.TraceData(LogUtil.GetMemberName<bool>((Expression<Func<bool>>)(() => success)), (object)success));
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventec.Common.SignLibrary.UCViewer));
			DevExpress.Utils.SuperToolTip superToolTip = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
			this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.pdfCommandBar1 = new DevExpress.XtraPdfViewer.Bars.PdfCommandBar();
			this.pdfFileOpenBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfFileOpenBarItem();
			this.pdfFileSaveAsBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfFileSaveAsBarItem();
			this.pdfFilePrintBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfFilePrintBarItem();
			this.bbtnPrint = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnResetChkState = new DevExpress.XtraBars.BarButtonItem();
			this.pdfFindTextBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfFindTextBarItem();
			this.pdfPreviousPageBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfPreviousPageBarItem();
			this.pdfNextPageBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfNextPageBarItem();
			this.pdfZoomOutBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoomOutBarItem();
			this.pdfZoomInBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoomInBarItem();
			this.pdfExactZoomListBarSubItem1 = new DevExpress.XtraPdfViewer.Bars.PdfExactZoomListBarSubItem();
			this.pdfZoom10CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom10CheckItem();
			this.pdfZoom25CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom25CheckItem();
			this.pdfZoom50CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom50CheckItem();
			this.pdfZoom75CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom75CheckItem();
			this.pdfZoom100CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom100CheckItem();
			this.pdfZoom125CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom125CheckItem();
			this.pdfZoom150CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom150CheckItem();
			this.pdfZoom200CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom200CheckItem();
			this.pdfZoom400CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom400CheckItem();
			this.pdfZoom500CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom500CheckItem();
			this.pdfSetActualSizeZoomModeCheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfSetActualSizeZoomModeCheckItem();
			this.pdfSetPageLevelZoomModeCheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfSetPageLevelZoomModeCheckItem();
			this.pdfSetFitWidthZoomModeCheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfSetFitWidthZoomModeCheckItem();
			this.pdfSetFitVisibleZoomModeCheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfSetFitVisibleZoomModeCheckItem();
			this.pdfExportFormDataBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfExportFormDataBarItem();
			this.pdfImportFormDataBarItem1 = new DevExpress.XtraPdfViewer.Bars.PdfImportFormDataBarItem();
			this.bbtnSendERM = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnSign = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnPatientSign = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnRelativeHomeSign = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnSignAndDeskcription = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnConfigSign = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnConfigBussinessMenu2 = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnRejectSign = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnSignEnd = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnListSign = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnchkSignParanel = new DevExpress.XtraBars.BarCheckItem();
			this.bbtnChkCloseAfterSign = new DevExpress.XtraBars.BarCheckItem();
			this.barCheckUsingSignPad = new DevExpress.XtraBars.BarCheckItem();
			this.bbtnSignType = new DevExpress.XtraBars.BarCheckItem();
			this.btnViewPACSImage = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnCtrlShiftU = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnAttackMentsMenu1 = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnOther = new DevExpress.XtraBars.BarSubItem();
			this.bbtnSignAndDeskcriptionOther = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnRelativeHomeSignOther = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
			this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.txtSignDescriptionList = new DevExpress.XtraRichEdit.RichEditControl();
			this.txtSignDescription = new DevExpress.XtraEditors.MemoEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.bbtnConfigBussinessMenu = new DevExpress.XtraBars.BarStaticItem();
			this.bbtnAttackMentsMenu = new DevExpress.XtraBars.BarStaticItem();
			this.bbtnConfigBussinessMenu1 = new DevExpress.XtraBars.BarButtonItem();
			this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
			this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.pdfBarController1 = new DevExpress.XtraPdfViewer.Bars.PdfBarController();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
			this.timerLoadSinglePage = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dockManager1).BeginInit();
			this.dockPanel1.SuspendLayout();
			this.dockPanel1_Container.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.txtSignDescription.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.pdfBarController1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.imageCollection1).BeginInit();
			base.SuspendLayout();
			this.pdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pdfViewer1.Location = new System.Drawing.Point(0, 31);
			this.pdfViewer1.MenuManager = this.barManager1;
			this.pdfViewer1.Name = "pdfViewer1";
			this.pdfViewer1.Size = new System.Drawing.Size(1906, 528);
			this.pdfViewer1.TabIndex = 0;
			this.pdfViewer1.PageSetupDialogShowing += new DevExpress.XtraPdfViewer.PdfPageSetupDialogShowingEventHandler(pdfViewer1_PageSetupDialogShowing);
			this.pdfViewer1.PopupMenuShowing += new DevExpress.XtraPdfViewer.PdfPopupMenuShowingEventHandler(pdfViewer1_PopupMenuShowing);
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.pdfCommandBar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.DockManager = this.dockManager1;
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[52]
			{
				this.pdfFileOpenBarItem1, this.pdfFileSaveAsBarItem1, this.pdfFilePrintBarItem1, this.pdfFindTextBarItem1, this.pdfPreviousPageBarItem1, this.pdfNextPageBarItem1, this.pdfZoomOutBarItem1, this.pdfZoomInBarItem1, this.pdfExactZoomListBarSubItem1, this.pdfZoom10CheckItem1,
				this.pdfZoom25CheckItem1, this.pdfZoom50CheckItem1, this.pdfZoom75CheckItem1, this.pdfZoom100CheckItem1, this.pdfZoom125CheckItem1, this.pdfZoom150CheckItem1, this.pdfZoom200CheckItem1, this.pdfZoom400CheckItem1, this.pdfZoom500CheckItem1, this.pdfSetActualSizeZoomModeCheckItem1,
				this.pdfSetPageLevelZoomModeCheckItem1, this.pdfSetFitWidthZoomModeCheckItem1, this.pdfSetFitVisibleZoomModeCheckItem1, this.pdfExportFormDataBarItem1, this.pdfImportFormDataBarItem1, this.bbtnPrint, this.bbtnSendERM, this.bbtnSign, this.bbtnPatientSign, this.bbtnConfigSign,
				this.bbtnRejectSign, this.bbtnSignEnd, this.bbtnListSign, this.bbtnResetChkState, this.btnViewPACSImage, this.bbtnCtrlShiftU, this.bbtnChkCloseAfterSign, this.bbtnOther, this.bbtnConfigBussinessMenu, this.bbtnAttackMentsMenu,
				this.bbtnRelativeHomeSign, this.bbtnConfigBussinessMenu1, this.barSubItem1, this.bbtnAttackMentsMenu1, this.bbtnSignType, this.barCheckUsingSignPad, this.bbtnchkSignParanel, this.bbtnSignAndDeskcription, this.bbtnSignAndDeskcriptionOther, this.bbtnRelativeHomeSignOther,
				this.bbtnConfigBussinessMenu2, this.barStaticItem1
			});
			this.barManager1.MaxItemId = 60;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[1] { this.repositoryItemCheckEdit1 });
			this.pdfCommandBar1.Control = this.pdfViewer1;
			this.pdfCommandBar1.DockCol = 0;
			this.pdfCommandBar1.DockRow = 0;
			this.pdfCommandBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.pdfCommandBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[31]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfFileOpenBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfFileSaveAsBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfFilePrintBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnPrint),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnResetChkState),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfFindTextBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfPreviousPageBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfNextPageBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoomOutBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoomInBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfExactZoomListBarSubItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfExportFormDataBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfImportFormDataBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnSendERM),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnSign),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnPatientSign),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnRelativeHomeSign),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnSignAndDeskcription),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnConfigSign),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnConfigBussinessMenu2),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnRejectSign),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnSignEnd),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnListSign),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnchkSignParanel),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnChkCloseAfterSign),
				new DevExpress.XtraBars.LinkPersistInfo(this.barCheckUsingSignPad),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnSignType),
				new DevExpress.XtraBars.LinkPersistInfo(this.btnViewPACSImage),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnCtrlShiftU),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnAttackMentsMenu1),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnOther)
			});
			this.pdfCommandBar1.Offset = 1;
			this.pdfFileOpenBarItem1.Id = 0;
			this.pdfFileOpenBarItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.O | System.Windows.Forms.Keys.Control);
			this.pdfFileOpenBarItem1.Name = "pdfFileOpenBarItem1";
			this.pdfFileOpenBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.pdfFileSaveAsBarItem1.Id = 1;
			this.pdfFileSaveAsBarItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
			this.pdfFileSaveAsBarItem1.Name = "pdfFileSaveAsBarItem1";
			this.pdfFileSaveAsBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.pdfFilePrintBarItem1.Id = 2;
			this.pdfFilePrintBarItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.P | System.Windows.Forms.Keys.Control);
			this.pdfFilePrintBarItem1.Name = "pdfFilePrintBarItem1";
			this.pdfFilePrintBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnPrint.Caption = "In";
			this.bbtnPrint.Glyph = (System.Drawing.Image)resources.GetObject("bbtnPrint.Glyph");
			this.bbtnPrint.Id = 29;
			this.bbtnPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.P | System.Windows.Forms.Keys.Control);
			this.bbtnPrint.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnPrint.LargeGlyph");
			this.bbtnPrint.Name = "bbtnPrint";
			this.bbtnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnPrint_ItemClick);
			this.bbtnResetChkState.Caption = "Xóa mặc định";
			this.bbtnResetChkState.Glyph = (System.Drawing.Image)resources.GetObject("bbtnResetChkState.Glyph");
			this.bbtnResetChkState.Id = 40;
			this.bbtnResetChkState.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnResetChkState.LargeGlyph");
			this.bbtnResetChkState.Name = "bbtnResetChkState";
			this.bbtnResetChkState.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnResetChkState_ItemClick);
			this.pdfFindTextBarItem1.Id = 3;
			this.pdfFindTextBarItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Control);
			this.pdfFindTextBarItem1.Name = "pdfFindTextBarItem1";
			this.pdfPreviousPageBarItem1.Id = 4;
			this.pdfPreviousPageBarItem1.Name = "pdfPreviousPageBarItem1";
			this.pdfPreviousPageBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.pdfNextPageBarItem1.Id = 5;
			this.pdfNextPageBarItem1.Name = "pdfNextPageBarItem1";
			this.pdfNextPageBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.pdfZoomOutBarItem1.Id = 7;
			this.pdfZoomOutBarItem1.Name = "pdfZoomOutBarItem1";
			this.pdfZoomOutBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.pdfZoomInBarItem1.Id = 8;
			this.pdfZoomInBarItem1.Name = "pdfZoomInBarItem1";
			this.pdfZoomInBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.pdfExactZoomListBarSubItem1.Id = 9;
			this.pdfExactZoomListBarSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[14]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom10CheckItem1, true),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom25CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom50CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom75CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom100CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom125CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom150CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom200CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom400CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfZoom500CheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfSetActualSizeZoomModeCheckItem1, true),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfSetPageLevelZoomModeCheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfSetFitWidthZoomModeCheckItem1),
				new DevExpress.XtraBars.LinkPersistInfo(this.pdfSetFitVisibleZoomModeCheckItem1)
			});
			this.pdfExactZoomListBarSubItem1.Name = "pdfExactZoomListBarSubItem1";
			this.pdfExactZoomListBarSubItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
			this.pdfExactZoomListBarSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.pdfZoom10CheckItem1.Id = 10;
			this.pdfZoom10CheckItem1.Name = "pdfZoom10CheckItem1";
			this.pdfZoom25CheckItem1.Id = 11;
			this.pdfZoom25CheckItem1.Name = "pdfZoom25CheckItem1";
			this.pdfZoom50CheckItem1.Id = 12;
			this.pdfZoom50CheckItem1.Name = "pdfZoom50CheckItem1";
			this.pdfZoom75CheckItem1.Id = 13;
			this.pdfZoom75CheckItem1.Name = "pdfZoom75CheckItem1";
			this.pdfZoom100CheckItem1.Id = 14;
			this.pdfZoom100CheckItem1.Name = "pdfZoom100CheckItem1";
			this.pdfZoom125CheckItem1.Id = 15;
			this.pdfZoom125CheckItem1.Name = "pdfZoom125CheckItem1";
			this.pdfZoom150CheckItem1.Id = 16;
			this.pdfZoom150CheckItem1.Name = "pdfZoom150CheckItem1";
			this.pdfZoom200CheckItem1.Id = 17;
			this.pdfZoom200CheckItem1.Name = "pdfZoom200CheckItem1";
			this.pdfZoom400CheckItem1.Id = 18;
			this.pdfZoom400CheckItem1.Name = "pdfZoom400CheckItem1";
			this.pdfZoom500CheckItem1.Id = 19;
			this.pdfZoom500CheckItem1.Name = "pdfZoom500CheckItem1";
			this.pdfSetActualSizeZoomModeCheckItem1.Id = 20;
			this.pdfSetActualSizeZoomModeCheckItem1.Name = "pdfSetActualSizeZoomModeCheckItem1";
			this.pdfSetPageLevelZoomModeCheckItem1.Id = 21;
			this.pdfSetPageLevelZoomModeCheckItem1.Name = "pdfSetPageLevelZoomModeCheckItem1";
			this.pdfSetFitWidthZoomModeCheckItem1.Id = 22;
			this.pdfSetFitWidthZoomModeCheckItem1.Name = "pdfSetFitWidthZoomModeCheckItem1";
			this.pdfSetFitVisibleZoomModeCheckItem1.Id = 23;
			this.pdfSetFitVisibleZoomModeCheckItem1.Name = "pdfSetFitVisibleZoomModeCheckItem1";
			this.pdfExportFormDataBarItem1.Id = 27;
			this.pdfExportFormDataBarItem1.Name = "pdfExportFormDataBarItem1";
			this.pdfExportFormDataBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.pdfImportFormDataBarItem1.Id = 28;
			this.pdfImportFormDataBarItem1.Name = "pdfImportFormDataBarItem1";
			this.pdfImportFormDataBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnSendERM.Caption = "Tạo văn bản";
			this.bbtnSendERM.Glyph = (System.Drawing.Image)resources.GetObject("bbtnSendERM.Glyph");
			this.bbtnSendERM.Id = 30;
			this.bbtnSendERM.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnSendERM.LargeGlyph");
			this.bbtnSendERM.Name = "bbtnSendERM";
			this.bbtnSendERM.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnSendERM.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnSendERM_ItemClick);
			this.bbtnSign.Caption = "Ký";
			this.bbtnSign.Glyph = (System.Drawing.Image)resources.GetObject("bbtnSign.Glyph");
			this.bbtnSign.Id = 31;
			this.bbtnSign.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnSign.LargeGlyph");
			this.bbtnSign.Name = "bbtnSign";
			this.bbtnSign.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnSign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnSign_ItemClick);
			this.bbtnPatientSign.Caption = "Bệnh nhân ký";
			this.bbtnPatientSign.Glyph = (System.Drawing.Image)resources.GetObject("bbtnPatientSign.Glyph");
			this.bbtnPatientSign.Id = 33;
			this.bbtnPatientSign.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnPatientSign.LargeGlyph");
			this.bbtnPatientSign.Name = "bbtnPatientSign";
			this.bbtnPatientSign.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnPatientSign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnPatientSign_ItemClick);
			this.bbtnRelativeHomeSign.Caption = "Người nhà ký";
			this.bbtnRelativeHomeSign.Glyph = (System.Drawing.Image)resources.GetObject("bbtnRelativeHomeSign.Glyph");
			this.bbtnRelativeHomeSign.Id = 50;
			this.bbtnRelativeHomeSign.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnRelativeHomeSign.LargeGlyph");
			this.bbtnRelativeHomeSign.Name = "bbtnRelativeHomeSign";
			this.bbtnRelativeHomeSign.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnRelativeHomeSign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnRelativeHomeSign_ItemClick);
			this.bbtnSignAndDeskcription.Caption = "Ký && Ghi chú";
			this.bbtnSignAndDeskcription.Glyph = (System.Drawing.Image)resources.GetObject("bbtnSignAndDeskcription.Glyph");
			this.bbtnSignAndDeskcription.Id = 57;
			this.bbtnSignAndDeskcription.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnSignAndDeskcription.LargeGlyph");
			this.bbtnSignAndDeskcription.Name = "bbtnSignAndDeskcription";
			this.bbtnSignAndDeskcription.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnSignAndDeskcription.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnSignAndDeskcription_ItemClick);
			this.bbtnConfigSign.Caption = "Thiết lập ký";
			this.bbtnConfigSign.Glyph = (System.Drawing.Image)resources.GetObject("bbtnConfigSign.Glyph");
			this.bbtnConfigSign.Id = 34;
			this.bbtnConfigSign.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnConfigSign.LargeGlyph");
			this.bbtnConfigSign.Name = "bbtnConfigSign";
			this.bbtnConfigSign.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnConfigSign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnConfigSign_ItemClick);
			this.bbtnConfigBussinessMenu2.Caption = "Thiết lập nghiệp vụ ký";
			this.bbtnConfigBussinessMenu2.Glyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu2.Glyph");
			this.bbtnConfigBussinessMenu2.Id = 58;
			this.bbtnConfigBussinessMenu2.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu2.LargeGlyph");
			this.bbtnConfigBussinessMenu2.Name = "bbtnConfigBussinessMenu2";
			this.bbtnConfigBussinessMenu2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnConfigBussinessMenu2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnConfigBussinessMenu_ItemClick);
			this.bbtnRejectSign.Caption = "Từ chối ký";
			this.bbtnRejectSign.Glyph = (System.Drawing.Image)resources.GetObject("bbtnRejectSign.Glyph");
			this.bbtnRejectSign.Id = 35;
			this.bbtnRejectSign.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnRejectSign.LargeGlyph");
			this.bbtnRejectSign.Name = "bbtnRejectSign";
			this.bbtnRejectSign.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnRejectSign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnRejectSign_ItemClick);
			this.bbtnSignEnd.Caption = "Kết thúc ký";
			this.bbtnSignEnd.Glyph = (System.Drawing.Image)resources.GetObject("bbtnSignEnd.Glyph");
			this.bbtnSignEnd.Id = 36;
			this.bbtnSignEnd.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnSignEnd.LargeGlyph");
			this.bbtnSignEnd.Name = "bbtnSignEnd";
			this.bbtnSignEnd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnSignEnd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnSignEnd_ItemClick);
			this.bbtnListSign.Caption = "Danh sách ký";
			this.bbtnListSign.Glyph = (System.Drawing.Image)resources.GetObject("bbtnListSign.Glyph");
			this.bbtnListSign.Id = 39;
			this.bbtnListSign.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnListSign.LargeGlyph");
			this.bbtnListSign.Name = "bbtnListSign";
			this.bbtnListSign.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnListSign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnListSign_ItemClick);
			this.bbtnchkSignParanel.Caption = "Ký song song";
			this.bbtnchkSignParanel.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
			this.bbtnchkSignParanel.Id = 56;
			this.bbtnchkSignParanel.Name = "bbtnchkSignParanel";
			this.bbtnChkCloseAfterSign.Caption = "Đóng sau khi ký";
			this.bbtnChkCloseAfterSign.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
			this.bbtnChkCloseAfterSign.Id = 46;
			this.bbtnChkCloseAfterSign.Name = "bbtnChkCloseAfterSign";
			this.bbtnChkCloseAfterSign.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnChkCloseAfterSign.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(bbtnChkCloseAfterSign_CheckedChanged);
			this.barCheckUsingSignPad.Caption = "Sử dụng bảng ký";
			this.barCheckUsingSignPad.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
			this.barCheckUsingSignPad.Id = 55;
			this.barCheckUsingSignPad.Name = "barCheckUsingSignPad";
			this.barCheckUsingSignPad.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.barCheckUsingSignPad.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(barCheckUsingSignPad_CheckedChanged);
			this.bbtnSignType.Caption = "Ký USB token";
			this.bbtnSignType.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
			this.bbtnSignType.Id = 54;
			this.bbtnSignType.Name = "bbtnSignType";
			toolTipItem.Text = "Ký bằng USB token hoặc HSM";
			superToolTip.Items.Add(toolTipItem);
			this.bbtnSignType.SuperTip = superToolTip;
			this.bbtnSignType.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnSignType.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(bbtnSignType_CheckedChanged);
			this.btnViewPACSImage.Caption = "Xem ảnh PACS";
			this.btnViewPACSImage.Id = 44;
			this.btnViewPACSImage.Name = "btnViewPACSImage";
			this.btnViewPACSImage.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.btnViewPACSImage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(btnViewPACSImage_ItemClick);
			this.bbtnCtrlShiftU.Caption = "Ctrl Shift U";
			this.bbtnCtrlShiftU.Id = 45;
			this.bbtnCtrlShiftU.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.U | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control);
			this.bbtnCtrlShiftU.Name = "bbtnCtrlShiftU";
			this.bbtnCtrlShiftU.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnCtrlShiftU.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnCtrlShiftU_ItemClick);
			this.bbtnAttackMentsMenu1.Caption = "Tập tin đính kèm";
			this.bbtnAttackMentsMenu1.Glyph = (System.Drawing.Image)resources.GetObject("bbtnAttackMentsMenu1.Glyph");
			this.bbtnAttackMentsMenu1.Id = 53;
			this.bbtnAttackMentsMenu1.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnAttackMentsMenu1.LargeGlyph");
			this.bbtnAttackMentsMenu1.Name = "bbtnAttackMentsMenu1";
			this.bbtnAttackMentsMenu1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnAttackMentsMenu1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnAttackMentsMenu1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnAttackMentsMenu_ItemClick);
			this.bbtnOther.Caption = "Khác";
			this.bbtnOther.Glyph = (System.Drawing.Image)resources.GetObject("bbtnOther.Glyph");
			this.bbtnOther.Id = 47;
			this.bbtnOther.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnOther.LargeGlyph");
			this.bbtnOther.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnSignAndDeskcriptionOther),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnRelativeHomeSignOther)
			});
			this.bbtnOther.Name = "bbtnOther";
			this.bbtnOther.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnSignAndDeskcriptionOther.Caption = "Ký && Ghi chú";
			this.bbtnSignAndDeskcriptionOther.Glyph = (System.Drawing.Image)resources.GetObject("bbtnSignAndDeskcriptionOther.Glyph");
			this.bbtnSignAndDeskcriptionOther.Id = 58;
			this.bbtnSignAndDeskcriptionOther.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnSignAndDeskcriptionOther.LargeGlyph");
			this.bbtnSignAndDeskcriptionOther.Name = "bbtnSignAndDeskcriptionOther";
			this.bbtnSignAndDeskcriptionOther.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnSignAndDeskcriptionOther.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnSignAndDeskcriptionOther.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnSignAndDeskcription_ItemClick);
			this.bbtnRelativeHomeSignOther.Caption = "Người nhà ký";
			this.bbtnRelativeHomeSignOther.Glyph = (System.Drawing.Image)resources.GetObject("bbtnRelativeHomeSignOther.Glyph");
			this.bbtnRelativeHomeSignOther.Id = 59;
			this.bbtnRelativeHomeSignOther.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnRelativeHomeSignOther.LargeGlyph");
			this.bbtnRelativeHomeSignOther.Name = "bbtnRelativeHomeSignOther";
			this.bbtnRelativeHomeSignOther.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.bbtnRelativeHomeSignOther.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnRelativeHomeSignOther.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnRelativeHomeSign_ItemClick);
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(2129, 31);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 559);
			this.barDockControlBottom.Size = new System.Drawing.Size(2129, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 528);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(2129, 31);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 528);
			this.dockManager1.Form = this;
			this.dockManager1.MenuManager = this.barManager1;
			this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[1] { this.dockPanel1 });
			this.dockManager1.TopZIndexControls.AddRange(new string[9] { "DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane" });
			this.dockPanel1.Controls.Add(this.dockPanel1_Container);
			this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
			this.dockPanel1.ID = new System.Guid("529d1c7c-406a-4a9f-871a-d38066fae95d");
			this.dockPanel1.Location = new System.Drawing.Point(1906, 31);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.OriginalSize = new System.Drawing.Size(223, 200);
			this.dockPanel1.Size = new System.Drawing.Size(223, 528);
			this.dockPanel1.Text = "Ý kiến người ký";
			this.dockPanel1_Container.Controls.Add(this.layoutControl1);
			this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
			this.dockPanel1_Container.Name = "dockPanel1_Container";
			this.dockPanel1_Container.Size = new System.Drawing.Size(215, 501);
			this.dockPanel1_Container.TabIndex = 0;
			this.layoutControl1.Controls.Add(this.txtSignDescriptionList);
			this.layoutControl1.Controls.Add(this.txtSignDescription);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(215, 483);
			this.layoutControl1.TabIndex = 6;
			this.layoutControl1.Text = "layoutControl1";
			this.txtSignDescriptionList.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
			this.txtSignDescriptionList.Location = new System.Drawing.Point(2, 2);
			this.txtSignDescriptionList.MenuManager = this.barManager1;
			this.txtSignDescriptionList.Name = "txtSignDescriptionList";
			this.txtSignDescriptionList.ReadOnly = true;
			this.txtSignDescriptionList.Size = new System.Drawing.Size(211, 369);
			this.txtSignDescriptionList.TabIndex = 4;
			this.txtSignDescriptionList.Views.SimpleView.Padding = new System.Windows.Forms.Padding(8, 2, 8, 0);
			this.txtSignDescription.EditValue = "";
			this.txtSignDescription.Location = new System.Drawing.Point(2, 391);
			this.txtSignDescription.MenuManager = this.barManager1;
			this.txtSignDescription.Name = "txtSignDescription";
			this.txtSignDescription.Properties.Appearance.Options.UseTextOptions = true;
			this.txtSignDescription.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.txtSignDescription.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.txtSignDescription.Properties.AppearanceDisabled.Options.UseTextOptions = true;
			this.txtSignDescription.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.txtSignDescription.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.txtSignDescription.Properties.AppearanceFocused.Options.UseTextOptions = true;
			this.txtSignDescription.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.txtSignDescription.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.txtSignDescription.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
			this.txtSignDescription.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.txtSignDescription.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.txtSignDescription.Size = new System.Drawing.Size(211, 90);
			this.txtSignDescription.StyleController = this.layoutControl1;
			this.txtSignDescription.TabIndex = 1;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[2] { this.layoutControlItem2, this.layoutControlItem3 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(215, 483);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem2.Control = this.txtSignDescription;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 373);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(215, 110);
			this.layoutControlItem2.Text = "Ý kiến của bạn:";
			this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 13);
			this.layoutControlItem3.Control = this.txtSignDescriptionList;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(215, 373);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			this.bbtnConfigBussinessMenu.Caption = "Thiết lập nghiệp vụ ký";
			this.bbtnConfigBussinessMenu.Glyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu.Glyph");
			this.bbtnConfigBussinessMenu.Id = 48;
			this.bbtnConfigBussinessMenu.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu.LargeGlyph");
			this.bbtnConfigBussinessMenu.Name = "bbtnConfigBussinessMenu";
			this.bbtnConfigBussinessMenu.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bbtnConfigBussinessMenu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnConfigBussinessMenu_ItemClick);
			this.bbtnAttackMentsMenu.Caption = "Tập tin đính kèm";
			this.bbtnAttackMentsMenu.Glyph = (System.Drawing.Image)resources.GetObject("bbtnAttackMentsMenu.Glyph");
			this.bbtnAttackMentsMenu.Id = 49;
			this.bbtnAttackMentsMenu.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnAttackMentsMenu.LargeGlyph");
			this.bbtnAttackMentsMenu.Name = "bbtnAttackMentsMenu";
			this.bbtnAttackMentsMenu.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bbtnAttackMentsMenu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnAttackMentsMenu_ItemClick);
			this.bbtnConfigBussinessMenu1.Caption = "Thiết lập nghiệp vụ ký";
			this.bbtnConfigBussinessMenu1.Glyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu1.Glyph");
			this.bbtnConfigBussinessMenu1.Id = 51;
			this.bbtnConfigBussinessMenu1.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu1.LargeGlyph");
			this.bbtnConfigBussinessMenu1.Name = "bbtnConfigBussinessMenu1";
			this.bbtnConfigBussinessMenu1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			this.bbtnConfigBussinessMenu1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnConfigBussinessMenu_ItemClick);
			this.barSubItem1.Caption = "barSubItem1";
			this.barSubItem1.Id = 52;
			this.barSubItem1.Name = "barSubItem1";
			this.barStaticItem1.Caption = "barStaticItem1";
			this.barStaticItem1.Id = 59;
			this.barStaticItem1.Name = "barStaticItem1";
			this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			this.pdfBarController1.BarItems.Add(this.pdfFileOpenBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfFileSaveAsBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfFilePrintBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfFindTextBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfPreviousPageBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfNextPageBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoomOutBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoomInBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfExactZoomListBarSubItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom10CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom25CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom50CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom75CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom100CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom125CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom150CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom200CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom400CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom500CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetActualSizeZoomModeCheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetPageLevelZoomModeCheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetFitWidthZoomModeCheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetFitVisibleZoomModeCheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfExportFormDataBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfImportFormDataBarItem1);
			this.pdfBarController1.Control = this.pdfViewer1;
			this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "InsertComment_16x16.png");
			this.imageList1.Images.SetKeyName(1, "BONote_16x16.png");
			this.imageCollection1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("imageCollection1.ImageStream");
			this.imageCollection1.InsertGalleryImage("print_16x16.png", "images/print/print_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/print/print_16x16.png"), 0);
			this.imageCollection1.Images.SetKeyName(0, "print_16x16.png");
			this.timerLoadSinglePage.Tick += new System.EventHandler(timer1_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.pdfViewer1);
			base.Controls.Add(this.dockPanel1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "UCViewer";
			base.Size = new System.Drawing.Size(2129, 559);
			base.Load += new System.EventHandler(UCViewer1_Load);
			base.Resize += new System.EventHandler(UCViewer_Resize);
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dockManager1).EndInit();
			this.dockPanel1.ResumeLayout(false);
			this.dockPanel1_Container.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.txtSignDescription.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.pdfBarController1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.imageCollection1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
