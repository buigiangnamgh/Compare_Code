using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Common.Mapper;
using Inventec.Common.Resource;
using Inventec.Core;
using Inventec.Desktop.Common.Controls.ValidationRule;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Modules;
using MOS.EFMODEL.DataModels;
using MOS.SDO;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.PtttTemp
{
	public class FormPtttTemp : FormBase
	{
		private Inventec.Desktop.Common.Modules.Module Module;

		private HIS_SERE_SERV_PTTT_TEMP TempData;

		private int positionHandle = -1;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private TextEdit txtPtttTempCode;

		private LayoutControlItem lciPtttTempCode;

		private SimpleButton btnSave;

		private CheckEdit chkPublicDepartment;

		private CheckEdit chkPublic;

		private TextEdit txtPtttTempName;

		private LayoutControlItem lciPtttTempName;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem4;

		private EmptySpaceItem emptySpaceItem1;

		private LayoutControlItem layoutControlItem5;

		private DXValidationProvider dxValidationProvider1;

		private BarManager barManager1;

		private Bar bar1;

		private BarButtonItem barBtnSave;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		public FormPtttTemp(Inventec.Desktop.Common.Modules.Module _module, HIS_SERE_SERV_PTTT_TEMP tempData)
			: base(_module)
		{
			InitializeComponent();
			try
			{
				this.Module = _module;
				TempData = tempData;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void FormPtttTemp_Load(object sender, EventArgs e)
		{
			try
			{
				SetCaptionByLanguageKey();
				ValidateForm();
				txtPtttTempCode.Focus();
				txtPtttTempCode.SelectAll();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ValidateForm()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			try
			{
				ControlMaxLengthValidationRule val = new ControlMaxLengthValidationRule();
				val.editor = txtPtttTempCode;
				val.maxLength = 50;
				val.IsRequired = true;
				((ValidationRuleBase)(object)val).ErrorText = string.Format(ResourceMessage.TruongDuLieuVuotQuaKyTu, "50");
				((ValidationRuleBase)(object)val).ErrorType = ErrorType.Warning;
				dxValidationProvider1.SetValidationRule(txtPtttTempCode, (ValidationRuleBase)(object)val);
				ControlMaxLengthValidationRule val2 = new ControlMaxLengthValidationRule();
				val2.editor = txtPtttTempName;
				val2.maxLength = 500;
				val2.IsRequired = true;
				((ValidationRuleBase)(object)val2).ErrorText = string.Format(ResourceMessage.TruongDuLieuVuotQuaKyTu, "500");
				((ValidationRuleBase)(object)val2).ErrorType = ErrorType.Warning;
				dxValidationProvider1.SetValidationRule(txtPtttTempName, (ValidationRuleBase)(object)val2);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtPtttTempCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					txtPtttTempName.Focus();
					txtPtttTempName.SelectAll();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtPtttTempName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					chkPublic.Focus();
					chkPublic.SelectAll();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void chkPublic_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					chkPublicDepartment.Focus();
					chkPublicDepartment.SelectAll();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void chkPublicDepartment_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					btnSave.Focus();
					e.Handled = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Expected O, but got Unknown
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				if (TempData == null)
				{
					MessageBox.Show(ResourceMessage.KhongCoDuLieuMau);
					return;
				}
				positionHandle = -1;
				if (!dxValidationProvider1.Validate())
				{
					return;
				}
				bool value = false;
				CommonParam val = new CommonParam();
				HIS_SERE_SERV_PTTT_TEMP val2 = new HIS_SERE_SERV_PTTT_TEMP();
				DataObjectMapper.Map<HIS_SERE_SERV_PTTT_TEMP>((object)val2, (object)TempData);
				val2.SERE_SERV_PTTT_TEMP_CODE = txtPtttTempCode.Text.Trim();
				val2.SERE_SERV_PTTT_TEMP_NAME = txtPtttTempName.Text.Trim();
				val2.IS_PUBLIC = (chkPublic.Checked ? new short?(1) : ((short?)null));
				if (chkPublicDepartment.Checked)
				{
					long departmentId = WorkPlace.WorkPlaceSDO.FirstOrDefault((WorkPlaceSDO o) => o.RoomId == this.Module.RoomId).DepartmentId;
					val2.DEPARTMENT_ID = departmentId;
					val2.IS_PUBLIC_IN_DEPARTMENT = (chkPublicDepartment.Checked ? new short?(1) : ((short?)null));
				}
				WaitingManager.Show();
				HIS_SERE_SERV_PTTT_TEMP val3 = ((AdapterBase)new BackendAdapter(val)).Post<HIS_SERE_SERV_PTTT_TEMP>("api/HisSereServPtttTemp/Create", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)val2, val);
				WaitingManager.Hide();
				if (val3 != null)
				{
					value = true;
					((Form)this).Close();
				}
				MessageManager.Show((Form)(object)this, val, (bool?)value);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void barBtnSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			btnSave_Click(null, null);
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
				if (positionHandle == -1)
				{
					positionHandle = baseEdit.TabIndex;
					if (baseEdit.Visible)
					{
						baseEdit.SelectAll();
						baseEdit.Focus();
					}
				}
				if (positionHandle > baseEdit.TabIndex)
				{
					positionHandle = baseEdit.TabIndex;
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

		private void SetCaptionByLanguageKey()
		{
			try
			{
				ResourceLanguageManager.LanguageResource__FormPtttTemp = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(FormPtttTemp).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("FormPtttTemp.layoutControl1.Text", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				btnSave.Text = Inventec.Common.Resource.Get.Value("FormPtttTemp.btnSave.Text", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				chkPublicDepartment.Properties.Caption = Inventec.Common.Resource.Get.Value("FormPtttTemp.chkPublicDepartment.Properties.Caption", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				chkPublic.Properties.Caption = Inventec.Common.Resource.Get.Value("FormPtttTemp.chkPublic.Properties.Caption", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				lciPtttTempCode.Text = Inventec.Common.Resource.Get.Value("FormPtttTemp.lciPtttTempCode.Text", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				lciPtttTempName.Text = Inventec.Common.Resource.Get.Value("FormPtttTemp.lciPtttTempName.Text", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				layoutControlItem3.Text = Inventec.Common.Resource.Get.Value("FormPtttTemp.layoutControlItem3.Text", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				bar1.Text = Inventec.Common.Resource.Get.Value("FormPtttTemp.bar1.Text", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				barBtnSave.Caption = Inventec.Common.Resource.Get.Value("FormPtttTemp.barBtnSave.Caption", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
				((Control)(object)this).Text = Inventec.Common.Resource.Get.Value("FormPtttTemp.Text", ResourceLanguageManager.LanguageResource__FormPtttTemp, LanguageManager.GetCulture());
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
			layoutControl1 = new LayoutControl();
			btnSave = new SimpleButton();
			chkPublicDepartment = new CheckEdit();
			chkPublic = new CheckEdit();
			txtPtttTempName = new TextEdit();
			txtPtttTempCode = new TextEdit();
			layoutControlGroup1 = new LayoutControlGroup();
			lciPtttTempCode = new LayoutControlItem();
			lciPtttTempName = new LayoutControlItem();
			layoutControlItem3 = new LayoutControlItem();
			layoutControlItem4 = new LayoutControlItem();
			emptySpaceItem1 = new EmptySpaceItem();
			layoutControlItem5 = new LayoutControlItem();
			dxValidationProvider1 = new DXValidationProvider();
			barManager1 = new BarManager();
			bar1 = new Bar();
			barBtnSave = new BarButtonItem();
			barDockControlTop = new BarDockControl();
			barDockControlBottom = new BarDockControl();
			barDockControlLeft = new BarDockControl();
			barDockControlRight = new BarDockControl();
			((ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((ISupportInitialize)chkPublicDepartment.Properties).BeginInit();
			((ISupportInitialize)chkPublic.Properties).BeginInit();
			((ISupportInitialize)txtPtttTempName.Properties).BeginInit();
			((ISupportInitialize)txtPtttTempCode.Properties).BeginInit();
			((ISupportInitialize)layoutControlGroup1).BeginInit();
			((ISupportInitialize)lciPtttTempCode).BeginInit();
			((ISupportInitialize)lciPtttTempName).BeginInit();
			((ISupportInitialize)layoutControlItem3).BeginInit();
			((ISupportInitialize)layoutControlItem4).BeginInit();
			((ISupportInitialize)emptySpaceItem1).BeginInit();
			((ISupportInitialize)layoutControlItem5).BeginInit();
			((ISupportInitialize)dxValidationProvider1).BeginInit();
			((ISupportInitialize)barManager1).BeginInit();
			((Control)this).SuspendLayout();
			layoutControl1.Controls.Add(btnSave);
			layoutControl1.Controls.Add(chkPublicDepartment);
			layoutControl1.Controls.Add(chkPublic);
			layoutControl1.Controls.Add(txtPtttTempName);
			layoutControl1.Controls.Add(txtPtttTempCode);
			layoutControl1.Dock = DockStyle.Fill;
			layoutControl1.Location = new Point(0, 29);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new Size(440, 69);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			btnSave.Location = new Point(319, 74);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(102, 22);
			btnSave.StyleController = layoutControl1;
			btnSave.TabIndex = 8;
			btnSave.Text = "Lưu (Ctrl S)";
			btnSave.Click += btnSave_Click;
			chkPublicDepartment.Location = new Point(214, 50);
			chkPublicDepartment.Name = "chkPublicDepartment";
			chkPublicDepartment.Properties.Caption = "Công khai trong khoa";
			chkPublicDepartment.Size = new Size(207, 19);
			chkPublicDepartment.StyleController = layoutControl1;
			chkPublicDepartment.TabIndex = 7;
			chkPublicDepartment.KeyDown += chkPublicDepartment_KeyDown;
			chkPublic.Location = new Point(67, 50);
			chkPublic.Name = "chkPublic";
			chkPublic.Properties.Caption = "Công khai toàn viện";
			chkPublic.Size = new Size(143, 19);
			chkPublic.StyleController = layoutControl1;
			chkPublic.TabIndex = 6;
			chkPublic.PreviewKeyDown += chkPublic_PreviewKeyDown;
			txtPtttTempName.Location = new Point(67, 26);
			txtPtttTempName.Name = "txtPtttTempName";
			txtPtttTempName.Size = new Size(354, 20);
			txtPtttTempName.StyleController = layoutControl1;
			txtPtttTempName.TabIndex = 5;
			txtPtttTempName.PreviewKeyDown += txtPtttTempName_PreviewKeyDown;
			txtPtttTempCode.Location = new Point(67, 2);
			txtPtttTempCode.Name = "txtPtttTempCode";
			txtPtttTempCode.Size = new Size(354, 20);
			txtPtttTempCode.StyleController = layoutControl1;
			txtPtttTempCode.TabIndex = 4;
			txtPtttTempCode.PreviewKeyDown += txtPtttTempCode_PreviewKeyDown;
			layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Items.AddRange(new BaseLayoutItem[6] { lciPtttTempCode, lciPtttTempName, layoutControlItem3, layoutControlItem4, emptySpaceItem1, layoutControlItem5 });
			layoutControlGroup1.Location = new Point(0, 0);
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			layoutControlGroup1.Size = new Size(423, 98);
			layoutControlGroup1.TextVisible = false;
			lciPtttTempCode.AppearanceItemCaption.ForeColor = Color.Maroon;
			lciPtttTempCode.AppearanceItemCaption.Options.UseForeColor = true;
			lciPtttTempCode.AppearanceItemCaption.Options.UseTextOptions = true;
			lciPtttTempCode.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
			lciPtttTempCode.Control = txtPtttTempCode;
			lciPtttTempCode.Location = new Point(0, 0);
			lciPtttTempCode.Name = "lciPtttTempCode";
			lciPtttTempCode.Size = new Size(423, 24);
			lciPtttTempCode.Text = "Mã:";
			lciPtttTempCode.TextAlignMode = TextAlignModeItem.CustomSize;
			lciPtttTempCode.TextSize = new Size(60, 20);
			lciPtttTempCode.TextToControlDistance = 5;
			lciPtttTempName.AppearanceItemCaption.ForeColor = Color.Maroon;
			lciPtttTempName.AppearanceItemCaption.Options.UseForeColor = true;
			lciPtttTempName.AppearanceItemCaption.Options.UseTextOptions = true;
			lciPtttTempName.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
			lciPtttTempName.Control = txtPtttTempName;
			lciPtttTempName.Location = new Point(0, 24);
			lciPtttTempName.Name = "lciPtttTempName";
			lciPtttTempName.Size = new Size(423, 24);
			lciPtttTempName.Text = "Tên:";
			lciPtttTempName.TextAlignMode = TextAlignModeItem.CustomSize;
			lciPtttTempName.TextSize = new Size(60, 20);
			lciPtttTempName.TextToControlDistance = 5;
			layoutControlItem3.Control = chkPublic;
			layoutControlItem3.Location = new Point(0, 48);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new Size(212, 24);
			layoutControlItem3.Text = " ";
			layoutControlItem3.TextAlignMode = TextAlignModeItem.CustomSize;
			layoutControlItem3.TextSize = new Size(60, 20);
			layoutControlItem3.TextToControlDistance = 5;
			layoutControlItem4.Control = chkPublicDepartment;
			layoutControlItem4.Location = new Point(212, 48);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new Size(211, 24);
			layoutControlItem4.TextSize = new Size(0, 0);
			layoutControlItem4.TextVisible = false;
			emptySpaceItem1.AllowHotTrack = false;
			emptySpaceItem1.Location = new Point(0, 72);
			emptySpaceItem1.Name = "emptySpaceItem1";
			emptySpaceItem1.Size = new Size(317, 26);
			emptySpaceItem1.TextSize = new Size(0, 0);
			layoutControlItem5.Control = btnSave;
			layoutControlItem5.Location = new Point(317, 72);
			layoutControlItem5.Name = "layoutControlItem5";
			layoutControlItem5.Size = new Size(106, 26);
			layoutControlItem5.TextSize = new Size(0, 0);
			layoutControlItem5.TextVisible = false;
			dxValidationProvider1.ValidationFailed += dxValidationProvider1_ValidationFailed;
			barManager1.Bars.AddRange(new Bar[1] { bar1 });
			barManager1.DockControls.Add(barDockControlTop);
			barManager1.DockControls.Add(barDockControlBottom);
			barManager1.DockControls.Add(barDockControlLeft);
			barManager1.DockControls.Add(barDockControlRight);
			barManager1.Form = (Control)(object)this;
			barManager1.Items.AddRange(new BarItem[1] { barBtnSave });
			barManager1.MaxItemId = 1;
			bar1.BarName = "Tools";
			bar1.DockCol = 0;
			bar1.DockRow = 0;
			bar1.DockStyle = BarDockStyle.Top;
			bar1.LinksPersistInfo.AddRange(new LinkPersistInfo[1]
			{
				new LinkPersistInfo(barBtnSave)
			});
			bar1.Text = "Tools";
			bar1.Visible = false;
			barBtnSave.Caption = "Ctrl S";
			barBtnSave.Id = 0;
			barBtnSave.ItemShortcut = new BarShortcut(Keys.S | Keys.Control);
			barBtnSave.Name = "barBtnSave";
			barBtnSave.ItemClick += barBtnSave_ItemClick;
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = DockStyle.Top;
			barDockControlTop.Location = new Point(0, 0);
			barDockControlTop.Size = new Size(440, 29);
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = DockStyle.Bottom;
			barDockControlBottom.Location = new Point(0, 98);
			barDockControlBottom.Size = new Size(440, 0);
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = DockStyle.Left;
			barDockControlLeft.Location = new Point(0, 29);
			barDockControlLeft.Size = new Size(0, 69);
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = DockStyle.Right;
			barDockControlRight.Location = new Point(440, 29);
			barDockControlRight.Size = new Size(0, 69);
			((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
			((ContainerControl)this).AutoScaleMode = AutoScaleMode.Font;
			((Form)this).ClientSize = new Size(440, 98);
			((Control)this).Controls.Add(layoutControl1);
			((Control)this).Controls.Add(barDockControlLeft);
			((Control)this).Controls.Add(barDockControlRight);
			((Control)this).Controls.Add(barDockControlBottom);
			((Control)this).Controls.Add(barDockControlTop);
			((Control)this).Name = "FormPtttTemp";
			((Form)this).StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)this).Text = "Mẫu phẫu thuật thủ thuật";
			((Form)this).Load += FormPtttTemp_Load;
			((Control)this).Controls.SetChildIndex(barDockControlTop, 0);
			((Control)this).Controls.SetChildIndex(barDockControlBottom, 0);
			((Control)this).Controls.SetChildIndex(barDockControlRight, 0);
			((Control)this).Controls.SetChildIndex(barDockControlLeft, 0);
			((Control)this).Controls.SetChildIndex(layoutControl1, 0);
			((ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			((ISupportInitialize)chkPublicDepartment.Properties).EndInit();
			((ISupportInitialize)chkPublic.Properties).EndInit();
			((ISupportInitialize)txtPtttTempName.Properties).EndInit();
			((ISupportInitialize)txtPtttTempCode.Properties).EndInit();
			((ISupportInitialize)layoutControlGroup1).EndInit();
			((ISupportInitialize)lciPtttTempCode).EndInit();
			((ISupportInitialize)lciPtttTempName).EndInit();
			((ISupportInitialize)layoutControlItem3).EndInit();
			((ISupportInitialize)layoutControlItem4).EndInit();
			((ISupportInitialize)emptySpaceItem1).EndInit();
			((ISupportInitialize)layoutControlItem5).EndInit();
			((ISupportInitialize)dxValidationProvider1).EndInit();
			((ISupportInitialize)barManager1).EndInit();
			((Control)this).ResumeLayout(false);
			((Control)this).PerformLayout();
		}
	}
}
