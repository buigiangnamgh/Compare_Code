using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraLayout;
using EMR.EFMODEL.DataModels;
using EMR.SDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;

namespace Inventec.Common.SignLibrary.Popup
{
	public class frmUpdateSigner : Form
	{
		private EMR_SIGNER EditData;

		private int positionHandle = -1;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private SimpleButton simpleButton1;

		private TextEdit txtHsmUserCode;

		private LabelControl lblWaring;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem lciHsmUserCode;

		private EmptySpaceItem emptySpaceItem1;

		private LayoutControlItem layoutControlItem3;

		private DXValidationProvider dxValidationProvider1;

		private BarManager barManager1;

		private Bar bar1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private BarButtonItem barButtonItem1;

		public frmUpdateSigner(EMR_SIGNER data, int type)
		{
			InitializeComponent();
			EditData = data;
			if (type == 1)
			{
				lblWaring.Text = "Mã người ký không hợp kệ. Vui lòng nhập và thực hiện ký lại";
			}
		}

		private void frmUpdateSigner_Load(object sender, EventArgs e)
		{
			try
			{
				if (EditData == null)
				{
					Close();
				}
				CodeValidationRule codeValidationRule = new CodeValidationRule();
				codeValidationRule.txt = txtHsmUserCode;
				dxValidationProvider1.SetValidationRule(txtHsmUserCode, codeValidationRule);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			try
			{
				positionHandle = -1;
				if (!dxValidationProvider1.Validate())
				{
					return;
				}
				EditData.HSM_USER_CODE = txtHsmUserCode.Text.Trim();
				EmrSignerSDO val = new EmrSignerSDO();
				val.EmrSigner = EditData;
				if (EditData.SIGN_IMAGE != null)
				{
					val.ImgBase64Data = Convert.ToBase64String(EditData.SIGN_IMAGE);
				}
				CommonParam paramCommon = new CommonParam();
				EMR_SIGNER val2 = GlobalStore.EmrConsumer.Post<EMR_SIGNER>("api/EmrSigner/Update", paramCommon, val, new object[0]);
				if (val2 != null)
				{
					Close();
					return;
				}
				LogSystem.Error(LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>((Expression<Func<CommonParam>>)(() => paramCommon)), (object)paramCommon) + LogUtil.TraceData(LogUtil.GetMemberName<EMR_SIGNER>(Expression.Lambda<Func<EMR_SIGNER>>(Expression.Field(Expression.Constant(this, typeof(frmUpdateSigner)), FieldInfo.GetFieldFromHandle((RuntimeFieldHandle)/*OpCode not supported: LdMemberToken*/)), new ParameterExpression[0])), (object)EditData));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
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
				if (baseEditViewInfo != null)
				{
					if (positionHandle == -1)
					{
						positionHandle = baseEdit.TabIndex;
						baseEdit.SelectAll();
						baseEdit.Focus();
					}
					if (positionHandle > baseEdit.TabIndex)
					{
						positionHandle = baseEdit.TabIndex;
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

		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			simpleButton1_Click(null, null);
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
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.txtHsmUserCode = new DevExpress.XtraEditors.TextEdit();
			this.lblWaring = new DevExpress.XtraEditors.LabelControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lciHsmUserCode = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.txtHsmUserCode.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciHsmUserCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.simpleButton1);
			this.layoutControl1.Controls.Add(this.txtHsmUserCode);
			this.layoutControl1.Controls.Add(this.lblWaring);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 29);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(416, 90);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.simpleButton1.Location = new System.Drawing.Point(306, 56);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(98, 22);
			this.simpleButton1.StyleController = this.layoutControl1;
			this.simpleButton1.TabIndex = 6;
			this.simpleButton1.Text = "Lưu (Ctrl S)";
			this.simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
			this.txtHsmUserCode.Location = new System.Drawing.Point(107, 32);
			this.txtHsmUserCode.Name = "txtHsmUserCode";
			this.txtHsmUserCode.Size = new System.Drawing.Size(297, 20);
			this.txtHsmUserCode.StyleController = this.layoutControl1;
			this.txtHsmUserCode.TabIndex = 5;
			this.lblWaring.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lblWaring.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.lblWaring.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblWaring.Location = new System.Drawing.Point(12, 12);
			this.lblWaring.Name = "lblWaring";
			this.lblWaring.Size = new System.Drawing.Size(392, 16);
			this.lblWaring.StyleController = this.layoutControl1;
			this.lblWaring.TabIndex = 4;
			this.lblWaring.Text = "Thiếu thông tin mã người ký. Vui lòng nhập mã người ký để tiếp tục";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[4] { this.layoutControlItem1, this.lciHsmUserCode, this.emptySpaceItem1, this.layoutControlItem3 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(416, 90);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.lblWaring;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.MinSize = new System.Drawing.Size(14, 17);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(396, 20);
			this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.lciHsmUserCode.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
			this.lciHsmUserCode.AppearanceItemCaption.Options.UseForeColor = true;
			this.lciHsmUserCode.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciHsmUserCode.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciHsmUserCode.Control = this.txtHsmUserCode;
			this.lciHsmUserCode.Location = new System.Drawing.Point(0, 20);
			this.lciHsmUserCode.Name = "lciHsmUserCode";
			this.lciHsmUserCode.Size = new System.Drawing.Size(396, 24);
			this.lciHsmUserCode.Text = "Mã người ký:";
			this.lciHsmUserCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.lciHsmUserCode.TextSize = new System.Drawing.Size(90, 20);
			this.lciHsmUserCode.TextToControlDistance = 5;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 44);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(294, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.Control = this.simpleButton1;
			this.layoutControlItem3.Location = new System.Drawing.Point(294, 44);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(102, 26);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			this.dxValidationProvider1.ValidationFailed += new DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventHandler(dxValidationProvider1_ValidationFailed);
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[1] { this.barButtonItem1 });
			this.barManager1.MaxItemId = 1;
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)
			});
			this.bar1.Text = "Tools";
			this.bar1.Visible = false;
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(416, 29);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 119);
			this.barDockControlBottom.Size = new System.Drawing.Size(416, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 90);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(416, 29);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 90);
			this.barButtonItem1.Caption = "Save";
			this.barButtonItem1.Id = 0;
			this.barButtonItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
			this.barButtonItem1.Name = "barButtonItem1";
			this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem1_ItemClick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(416, 119);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmUpdateSigner";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Thông tin người ký";
			base.Load += new System.EventHandler(frmUpdateSigner_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.txtHsmUserCode.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciHsmUserCode).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
