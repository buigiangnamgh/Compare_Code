using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.Integrate;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary
{
	public class frmAddNote : Form
	{
		private Action<string> actAddNote;

		private IContainer components = null;

		private MemoEdit txtNote;

		private LayoutControl layoutControl1;

		private SimpleButton btnOk;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		public frmAddNote(Action<string> _actAddNote)
		{
			try
			{
				InitializeComponent();
				actAddNote = _actAddNote;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtNote.Text))
				{
					MessageBox.Show("Chưa nhập ý kiến người ký");
				}
				else if (actAddNote != null)
				{
					actAddNote(txtNote.Text.Trim());
					Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				MessageManager.Show(MessageUitl.GetMessage("DuLieuKhongHopLe"));
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
			this.txtNote = new DevExpress.XtraEditors.MemoEdit();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.btnOk = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)this.txtNote.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			base.SuspendLayout();
			this.txtNote.Location = new System.Drawing.Point(12, 12);
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(356, 81);
			this.txtNote.StyleController = this.layoutControl1;
			this.txtNote.TabIndex = 0;
			this.layoutControl1.Controls.Add(this.btnOk);
			this.layoutControl1.Controls.Add(this.txtNote);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(380, 131);
			this.layoutControl1.TabIndex = 1;
			this.layoutControl1.Text = "layoutControl1";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3] { this.layoutControlItem1, this.layoutControlItem2, this.emptySpaceItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(380, 131);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem1.Control = this.txtNote;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(360, 85);
			this.layoutControlItem1.Text = "Ghi chú:";
			this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextToControlDistance = 0;
			this.layoutControlItem1.TextVisible = false;
			this.btnOk.Location = new System.Drawing.Point(260, 97);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(108, 22);
			this.btnOk.StyleController = this.layoutControl1;
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Đồng ý";
			this.btnOk.Click += new System.EventHandler(btnOk_Click);
			this.layoutControlItem2.Control = this.btnOk;
			this.layoutControlItem2.Location = new System.Drawing.Point(248, 85);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(112, 26);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 85);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(248, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(380, 131);
			base.Controls.Add(this.layoutControl1);
			base.Name = "frmAddNote";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ý kiến người ký";
			((System.ComponentModel.ISupportInitialize)this.txtNote.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
