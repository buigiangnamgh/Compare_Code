using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using EMR.EFMODEL.DataModels;
using Inventec.Common.Integrate;
using Inventec.Common.Integrate.EditorLoader;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.Validation;

namespace Inventec.Common.SignLibrary
{
	public class frmChoicePatientRelation : Form
	{
		private Action<EMR_RELATION, string> actChoose;

		private List<EMR_RELATION> relationDatas;

		private int positionHandleControl = -1;

		internal EMR_RELATION relation;

		internal string relationName;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private SimpleButton btnChoose;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private GridLookUpEdit cboRelation;

		private GridView gridLookUpEdit1View;

		private LayoutControlItem lciForcboRelation;

		private TextEdit txtRelationName;

		private LayoutControlItem layoutControlItem1;

		private DXValidationProvider dxValidationProvider1;

		private BarManager barManager1;

		private Bar bar1;

		private BarButtonItem barBtnAccept;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		public frmChoicePatientRelation()
		{
			InitializeComponent();
		}

		public frmChoicePatientRelation(Action<EMR_RELATION, string> _actChoose)
		{
			InitializeComponent();
			actChoose = _actChoose;
		}

		private void frmChoiceRelation_Load(object sender, EventArgs e)
		{
			try
			{
				relationDatas = new EmrRelation().Get();
				List<ColumnInfo> list = new List<ColumnInfo>();
				list.Add(new ColumnInfo("RELATION_NAME", "", 250, 2));
				ControlEditorADO controlEditorADO = new ControlEditorADO("RELATION_NAME", "ID", list, false, 250);
				ControlEditorLoader.Load(cboRelation, relationDatas, controlEditorADO);
				ValidRelationName();
				ValidRelationCombo();
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

		private void ValidRelationCombo()
		{
			try
			{
				RelationComboValidationRule relationComboValidationRule = new RelationComboValidationRule();
				relationComboValidationRule.cboRelation = cboRelation;
				dxValidationProvider1.SetValidationRule(cboRelation, relationComboValidationRule);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnChoose_Click(object sender, EventArgs e)
		{
			try
			{
				if (dxValidationProvider1.Validate())
				{
					relation = ((cboRelation.EditValue != null) ? relationDatas.Where((EMR_RELATION o) => o.ID == (long)cboRelation.EditValue).FirstOrDefault() : null);
					relationName = txtRelationName.Text;
					actChoose(relation, txtRelationName.Text);
					Close();
				}
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

		private void txtRelationName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Return)
				{
					cboRelation.Focus();
					cboRelation.ShowPopup();
					PopupLoader.SelectFirstRowPopup(cboRelation);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void barBtnAccept_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (btnChoose.Enabled)
				{
					btnChoose_Click(null, null);
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
			this.txtRelationName = new DevExpress.XtraEditors.TextEdit();
			this.cboRelation = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.btnChoose = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.lciForcboRelation = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barBtnAccept = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.txtRelationName.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.cboRelation.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit1View).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciForcboRelation).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.txtRelationName);
			this.layoutControl1.Controls.Add(this.cboRelation);
			this.layoutControl1.Controls.Add(this.btnChoose);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 29);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(293, 85);
			this.layoutControl1.TabIndex = 2;
			this.layoutControl1.Text = "layoutControl1";
			this.txtRelationName.Location = new System.Drawing.Point(97, 12);
			this.txtRelationName.Name = "txtRelationName";
			this.txtRelationName.Size = new System.Drawing.Size(167, 20);
			this.txtRelationName.StyleController = this.layoutControl1;
			this.txtRelationName.TabIndex = 7;
			this.txtRelationName.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(txtRelationName_PreviewKeyDown);
			this.cboRelation.Location = new System.Drawing.Point(97, 36);
			this.cboRelation.Name = "cboRelation";
			this.cboRelation.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.cboRelation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.cboRelation.Properties.NullText = "";
			this.cboRelation.Properties.View = this.gridLookUpEdit1View;
			this.cboRelation.Size = new System.Drawing.Size(167, 20);
			this.cboRelation.StyleController = this.layoutControl1;
			this.cboRelation.TabIndex = 6;
			this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
			this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			this.btnChoose.Location = new System.Drawing.Point(192, 60);
			this.btnChoose.Name = "btnChoose";
			this.btnChoose.Size = new System.Drawing.Size(72, 22);
			this.btnChoose.StyleController = this.layoutControl1;
			this.btnChoose.TabIndex = 4;
			this.btnChoose.Text = "Chọn (Ctrl A)";
			this.btnChoose.Click += new System.EventHandler(btnChoose_Click);
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[4] { this.layoutControlItem2, this.emptySpaceItem1, this.lciForcboRelation, this.layoutControlItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(276, 94);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem2.Control = this.btnChoose;
			this.layoutControlItem2.Location = new System.Drawing.Point(180, 48);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(76, 26);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(180, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.lciForcboRelation.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
			this.lciForcboRelation.AppearanceItemCaption.Options.UseForeColor = true;
			this.lciForcboRelation.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciForcboRelation.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciForcboRelation.Control = this.cboRelation;
			this.lciForcboRelation.Location = new System.Drawing.Point(0, 24);
			this.lciForcboRelation.Name = "lciForcboRelation";
			this.lciForcboRelation.Size = new System.Drawing.Size(256, 24);
			this.lciForcboRelation.Text = "Quan hệ:";
			this.lciForcboRelation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.lciForcboRelation.TextSize = new System.Drawing.Size(80, 20);
			this.lciForcboRelation.TextToControlDistance = 5;
			this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem1.Control = this.txtRelationName;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(256, 24);
			this.layoutControlItem1.Text = "Người nhà:";
			this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem1.TextSize = new System.Drawing.Size(80, 20);
			this.layoutControlItem1.TextToControlDistance = 5;
			this.dxValidationProvider1.ValidationFailed += new DevExpress.XtraEditors.DXErrorProvider.ValidationFailedEventHandler(dxValidationProvider1_ValidationFailed);
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[1] { this.barBtnAccept });
			this.barManager1.MaxItemId = 1;
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
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(293, 29);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 114);
			this.barDockControlBottom.Size = new System.Drawing.Size(293, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 85);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(293, 29);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 85);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(293, 114);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmChoicePatientRelation";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Khai báo người nhà";
			base.Load += new System.EventHandler(frmChoiceRelation_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.txtRelationName.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.cboRelation.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit1View).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciForcboRelation).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.dxValidationProvider1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
