using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.Validation;

namespace Inventec.Common.SignLibrary.Popup
{
	public class frmRelationship : Form
	{
		private int positionHandleControl = -1;

		internal string RelationName = "";

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private SimpleButton btnAccept;

		private TextEdit txtRelationName;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControlItem lciRelationName;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private BarManager barManager1;

		private Bar bar1;

		private BarButtonItem barBtnAccept;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private DXValidationProvider dxValidationProvider1;

		public frmRelationship()
		{
			InitializeComponent();
			try
			{
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void frmRelationship_Load(object sender, EventArgs e)
		{
			try
			{
				ValidRelationName();
				txtRelationName.Focus();
				txtRelationName.SelectAll();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ValidRelationName()
		{
			try
			{
				RelationNameValidationRule relationNameValidationRule = new RelationNameValidationRule();
				relationNameValidationRule.txtRelationName = txtRelationName;
				dxValidationProvider1.SetValidationRule(txtRelationName, relationNameValidationRule);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void txtRelationName_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					SendKeys.Send("{TAB}");
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			try
			{
				if (btnAccept.Enabled)
				{
					if (!dxValidationProvider1.Validate())
					{
						txtRelationName.Focus();
						txtRelationName.SelectAll();
					}
					else
					{
						RelationName = txtRelationName.Text.Trim();
						Close();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void barBtnAccept_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				btnAccept_Click(null, null);
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
						baseEdit.Focus();
						baseEdit.SelectAll();
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
			this.components = new System.ComponentModel.Container();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
			this.txtRelationName = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.lciRelationName = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barBtnAccept = new DevExpress.XtraBars.BarButtonItem();
			this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.txtRelationName.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciRelationName).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.btnAccept);
			this.layoutControl1.Controls.Add(this.txtRelationName);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 29);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(440, 52);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.btnAccept.Location = new System.Drawing.Point(319, 36);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(102, 22);
			this.btnAccept.StyleController = this.layoutControl1;
			this.btnAccept.TabIndex = 5;
			this.btnAccept.Text = "Đồng ý (Ctrl A)";
			this.btnAccept.Click += new System.EventHandler(btnAccept_Click);
			this.txtRelationName.Location = new System.Drawing.Point(97, 12);
			this.txtRelationName.Name = "txtRelationName";
			this.txtRelationName.Size = new System.Drawing.Size(324, 20);
			this.txtRelationName.StyleController = this.layoutControl1;
			this.txtRelationName.TabIndex = 4;
			this.txtRelationName.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRelationName_KeyDown);
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3] { this.lciRelationName, this.layoutControlItem2, this.emptySpaceItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(423, 60);
			this.layoutControlGroup1.TextVisible = false;
			this.lciRelationName.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
			this.lciRelationName.AppearanceItemCaption.Options.UseForeColor = true;
			this.lciRelationName.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciRelationName.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciRelationName.Control = this.txtRelationName;
			this.lciRelationName.Location = new System.Drawing.Point(0, 0);
			this.lciRelationName.Name = "lciRelationName";
			this.lciRelationName.Size = new System.Drawing.Size(423, 24);
			this.lciRelationName.Text = "Quan hệ:";
			this.lciRelationName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.lciRelationName.TextSize = new System.Drawing.Size(90, 20);
			this.lciRelationName.TextToControlDistance = 5;
			this.layoutControlItem2.Control = this.btnAccept;
			this.layoutControlItem2.Location = new System.Drawing.Point(317, 24);
			this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 50);
			this.layoutControlItem2.MinSize = new System.Drawing.Size(86, 26);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(106, 26);
			this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 24);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(317, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[1] { this.barBtnAccept });
			this.barManager1.MaxItemId = 1;
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(440, 29);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 81);
			this.barDockControlBottom.Size = new System.Drawing.Size(440, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 52);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(440, 29);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 52);
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAccept)
			});
			this.bar1.Text = "Tools";
			this.bar1.Visible = false;
			this.barBtnAccept.Caption = "Đồng ý (Ctrl A)";
			this.barBtnAccept.Id = 0;
			this.barBtnAccept.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.A | System.Windows.Forms.Keys.Control);
			this.barBtnAccept.Name = "barBtnAccept";
			this.barBtnAccept.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtnAccept_ItemClick);
			this.dxValidationProvider1.ValidationFailed += new DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventHandler(dxValidationProvider1_ValidationFailed);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(440, 81);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmRelationship";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Nhập thông tin quan hệ";
			base.Load += new System.EventHandler(frmRelationship_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.txtRelationName.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciRelationName).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
