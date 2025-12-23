using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using EMR.EFMODEL.DataModels;
using Inventec.Common.Integrate.EditorLoader;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.Api;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary
{
	public class frmChoiceRelation : Form
	{
		private Action<EMR_RELATION, bool> actChoose;

		private List<EMR_RELATION> relationDatas;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private SimpleButton btnChoose;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private CheckEdit chkIsPatientSign;

		private LayoutControlItem layoutControlItem3;

		private GridLookUpEdit cboRelation;

		private GridView gridLookUpEdit1View;

		private LayoutControlItem lciForcboRelation;

		public frmChoiceRelation(Action<EMR_RELATION, bool> _actChoose)
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
				EMR_RELATION val = ((cboRelation.EditValue != null) ? relationDatas.Where((EMR_RELATION o) => o.ID == (long)cboRelation.EditValue).FirstOrDefault() : null);
				if (val == null && !chkIsPatientSign.Checked)
				{
					MessageManager.Show(MessageUitl.GetMessage("ChuaChonMoiQuanHeVoiBenhNhan"));
					return;
				}
				actChoose(val, chkIsPatientSign.Checked);
				Close();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void chkIsPatientSign_CheckedChanged(object sender, EventArgs e)
		{
			if (chkIsPatientSign.Checked)
			{
				cboRelation.EditValue = null;
				lciForcboRelation.Enabled = false;
			}
			else
			{
				lciForcboRelation.Enabled = true;
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
			this.cboRelation = new DevExpress.XtraEditors.GridLookUpEdit();
			this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.chkIsPatientSign = new DevExpress.XtraEditors.CheckEdit();
			this.btnChoose = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.lciForcboRelation = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.cboRelation.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit1View).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chkIsPatientSign.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lciForcboRelation).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.cboRelation);
			this.layoutControl1.Controls.Add(this.chkIsPatientSign);
			this.layoutControl1.Controls.Add(this.btnChoose);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(293, 97);
			this.layoutControl1.TabIndex = 2;
			this.layoutControl1.Text = "layoutControl1";
			this.cboRelation.Location = new System.Drawing.Point(97, 36);
			this.cboRelation.Name = "cboRelation";
			this.cboRelation.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.cboRelation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			this.cboRelation.Properties.NullText = "";
			this.cboRelation.Properties.View = this.gridLookUpEdit1View;
			this.cboRelation.Size = new System.Drawing.Size(184, 20);
			this.cboRelation.StyleController = this.layoutControl1;
			this.cboRelation.TabIndex = 6;
			this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
			this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			this.chkIsPatientSign.Location = new System.Drawing.Point(97, 12);
			this.chkIsPatientSign.Name = "chkIsPatientSign";
			this.chkIsPatientSign.Properties.Caption = "";
			this.chkIsPatientSign.Size = new System.Drawing.Size(184, 19);
			this.chkIsPatientSign.StyleController = this.layoutControl1;
			this.chkIsPatientSign.TabIndex = 5;
			this.chkIsPatientSign.CheckedChanged += new System.EventHandler(chkIsPatientSign_CheckedChanged);
			this.btnChoose.Location = new System.Drawing.Point(209, 60);
			this.btnChoose.Name = "btnChoose";
			this.btnChoose.Size = new System.Drawing.Size(72, 22);
			this.btnChoose.StyleController = this.layoutControl1;
			this.btnChoose.TabIndex = 4;
			this.btnChoose.Text = "Chọn (Ctrl A)";
			this.btnChoose.Click += new System.EventHandler(btnChoose_Click);
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[4] { this.layoutControlItem2, this.emptySpaceItem1, this.layoutControlItem3, this.lciForcboRelation });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(293, 97);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem2.Control = this.btnChoose;
			this.layoutControlItem2.Location = new System.Drawing.Point(197, 48);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(76, 29);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(197, 29);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem3.Control = this.chkIsPatientSign;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(273, 24);
			this.layoutControlItem3.Text = "Là bệnh nhân:";
			this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(80, 20);
			this.layoutControlItem3.TextToControlDistance = 5;
			this.lciForcboRelation.AppearanceItemCaption.Options.UseTextOptions = true;
			this.lciForcboRelation.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lciForcboRelation.Control = this.cboRelation;
			this.lciForcboRelation.Location = new System.Drawing.Point(0, 24);
			this.lciForcboRelation.Name = "lciForcboRelation";
			this.lciForcboRelation.Size = new System.Drawing.Size(273, 24);
			this.lciForcboRelation.Text = "Quan hệ:";
			this.lciForcboRelation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.lciForcboRelation.TextSize = new System.Drawing.Size(80, 20);
			this.lciForcboRelation.TextToControlDistance = 5;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(293, 97);
			base.Controls.Add(this.layoutControl1);
			base.Name = "frmChoiceRelation";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chọn mối quan hệ";
			base.Load += new System.EventHandler(frmChoiceRelation_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.cboRelation.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridLookUpEdit1View).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chkIsPatientSign.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lciForcboRelation).EndInit();
			base.ResumeLayout(false);
		}
	}
}
