using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.DelegateRegister;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Plugins.RegisterV2.Resources;
using HIS.Desktop.Utility;
using HIS.UC.UCTransPati;
using HIS.UC.UCTransPati.ADO;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Modules;

namespace HIS.Desktop.Plugins.RegisterV2
{
	public class frmTransPati : FormBase
	{
		private DelegateVisible dlgSetValidateForValidTTCT;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private BarManager barManagerUCTransPati;

		private Bar bar1;

		private BarButtonItem bbntNhap;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private UCTransPati ucTransPati1;

		private LayoutControlItem layoutControlItem1;

		private SimpleButton btnClose;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private BarButtonItem barButtonItem1;

		private bool isValidate { get; set; }

		private UCTransPatiADO UCTransPatiADO { get; set; }

		private UpdateSelectedTranPati UpdateSelectedTranPati { get; set; }

		private bool _RunValidate { get; set; }

		private bool isValidateAll { get; set; }

		public frmTransPati(bool _isValidate, bool _isValidateAll, UCTransPatiADO _ucTransPatiADO, UpdateSelectedTranPati _updateSelectedTranPati)
			: this(null)
		{
			isValidate = _isValidate;
			isValidateAll = _isValidateAll;
			UCTransPatiADO = _ucTransPatiADO;
			UpdateSelectedTranPati = _updateSelectedTranPati;
		}

		public frmTransPati(bool _isValidate, UCTransPatiADO _ucTransPatiADO, UpdateSelectedTranPati _updateSelectedTranPati, bool _runValidate, bool _isValidateAll = false)
			: this(null)
		{
			_RunValidate = _runValidate;
			isValidate = _isValidate;
			UCTransPatiADO = _ucTransPatiADO;
			UpdateSelectedTranPati = _updateSelectedTranPati;
			isValidateAll = _isValidateAll;
		}

		public frmTransPati(Module module)
			: base(module)
		{
			InitializeComponent();
		}

		private void frmTransPati_Load(object sender, EventArgs e)
		{
			try
			{
				SetIcon();
				SetCaptionByLanguageKey();
				ucTransPati1.ResetRequiredField(isValidate, isValidateAll);
				ucTransPati1.SetValue(UCTransPatiADO);
				ucTransPati1.FocusNextUserControl(new Action<object>(FocusToButtonSave));
				if (_RunValidate)
				{
					ucTransPati1.ValidateRequiredField();
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
				ResourceLanguageManager.LanguageResource = new ResourceManager("HIS.Desktop.Plugins.RegisterV2.Resources.Lang", typeof(frmTransPati).Assembly);
				layoutControl1.Text = Get.Value("frmTransPati.layoutControl1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				btnClose.Text = Get.Value("frmTransPati.btnClose.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				bar1.Text = Get.Value("frmTransPati.bar1.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				bbntNhap.Caption = Get.Value("frmTransPati.bbntNhap.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				barButtonItem1.Caption = Get.Value("frmTransPati.barButtonItem1.Caption", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
				Text = Get.Value("frmTransPati.Text", ResourceLanguageManager.LanguageResource, LanguageManager.GetCulture());
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

		private void FocusToButtonSave(object data)
		{
			try
			{
				btnClose.Focus();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void bbntNhap_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				btnClose_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			try
			{
				if (dlgSetValidateForValidTTCT != null)
				{
					dlgSetValidateForValidTTCT(ValidateRequiredField());
				}
				UCTransPatiADO value = ucTransPati1.GetValue();
				UpdateSelectedTranPati(value);
				Close();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessImageDataByControl(PictureEdit pte, ref UCTransPatiADO data)
		{
			try
			{
				if (pte != null && pte.Image != null && pte.Image.Tag != null && !pte.Image.Tag.Equals("noImage"))
				{
					MemoryStream memoryStream = new MemoryStream();
					Bitmap bitmap = new Bitmap(pte.Image);
					bitmap.Save(memoryStream, ImageFormat.Jpeg);
					data.ImgTransferInData = memoryStream.ToArray();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public bool ValidateRequiredField()
		{
			bool flag = true;
			try
			{
				flag = ucTransPati1.ValidateRequiredField();
				LogSystem.Debug("Get validate tu form TransPati thanh cong. valid = " + flag);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				flag = false;
				LogSystem.Debug("Get validate tu form TransPati that bai.");
			}
			return flag;
		}

		public void RefreshFormTransPati()
		{
			try
			{
				ucTransPati1.RefreshUserControl();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		public void SetValidForTTCT(DelegateVisible _dlgHideForm)
		{
			try
			{
				if (_dlgHideForm != null)
				{
					dlgSetValidateForValidTTCT = _dlgHideForm;
				}
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
				isValidateAll = false;
				_RunValidate = false;
				dlgSetValidateForValidTTCT = null;
				UpdateSelectedTranPati = null;
				UCTransPatiADO = null;
				isValidate = false;
				btnClose.Click -= new EventHandler(btnClose_Click);
				bbntNhap.ItemClick -= new ItemClickEventHandler(bbntNhap_ItemClick);
				base.Load -= new EventHandler(frmTransPati_Load);
				barButtonItem1 = null;
				emptySpaceItem1 = null;
				layoutControlItem2 = null;
				btnClose = null;
				layoutControlItem1 = null;
				ucTransPati1 = null;
				barDockControlRight = null;
				barDockControlLeft = null;
				barDockControlBottom = null;
				barDockControlTop = null;
				bbntNhap = null;
				bar1 = null;
				barManagerUCTransPati = null;
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
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			this.ucTransPati1 = new HIS.UC.UCTransPati.UCTransPati();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.barManagerUCTransPati = new DevExpress.XtraBars.BarManager();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.bbntNhap = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManagerUCTransPati).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.btnClose);
			this.layoutControl1.Controls.Add(this.ucTransPati1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 29);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(534, 228);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.btnClose.Location = new System.Drawing.Point(415, 204);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(117, 22);
			this.btnClose.StyleController = this.layoutControl1;
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Nhập (Ctrl S)";
			this.btnClose.Click += new System.EventHandler(btnClose_Click);
			this.ucTransPati1.Location = new System.Drawing.Point(2, 2);
			this.ucTransPati1.Margin = new System.Windows.Forms.Padding(4);
			this.ucTransPati1.Name = "ucTransPati1";
			this.ucTransPati1.Size = new System.Drawing.Size(530, 198);
			this.ucTransPati1.TabIndex = 4;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3] { this.layoutControlItem1, this.layoutControlItem2, this.emptySpaceItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(534, 228);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.ucTransPati1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(534, 202);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem2.Control = this.btnClose;
			this.layoutControlItem2.Location = new System.Drawing.Point(413, 202);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(121, 26);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 202);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(413, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.barManagerUCTransPati.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar1 });
			this.barManagerUCTransPati.DockControls.Add(this.barDockControlTop);
			this.barManagerUCTransPati.DockControls.Add(this.barDockControlBottom);
			this.barManagerUCTransPati.DockControls.Add(this.barDockControlLeft);
			this.barManagerUCTransPati.DockControls.Add(this.barDockControlRight);
			this.barManagerUCTransPati.Form = this;
			this.barManagerUCTransPati.Items.AddRange(new DevExpress.XtraBars.BarItem[2] { this.bbntNhap, this.barButtonItem1 });
			this.barManagerUCTransPati.MaxItemId = 2;
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.bbntNhap)
			});
			this.bar1.Text = "Tools";
			this.bar1.Visible = false;
			this.bbntNhap.Caption = "Nhập";
			this.bbntNhap.Id = 0;
			this.bbntNhap.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
			this.bbntNhap.Name = "bbntNhap";
			this.bbntNhap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbntNhap_ItemClick);
			this.barButtonItem1.Caption = "barButtonItem1";
			this.barButtonItem1.Id = 1;
			this.barButtonItem1.Name = "barButtonItem1";
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(534, 29);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 257);
			this.barDockControlBottom.Size = new System.Drawing.Size(534, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 228);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(534, 29);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 228);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(534, 257);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmTransPati";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Thông tin chuyển tuyến";
			base.Load += new System.EventHandler(frmTransPati_Load);
			base.Controls.SetChildIndex(this.barDockControlTop, 0);
			base.Controls.SetChildIndex(this.barDockControlBottom, 0);
			base.Controls.SetChildIndex(this.barDockControlRight, 0);
			base.Controls.SetChildIndex(this.barDockControlLeft, 0);
			base.Controls.SetChildIndex(this.layoutControl1, 0);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.barManagerUCTransPati).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
