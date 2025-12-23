using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Inventec.Common.SignLibrary
{
	public class frmRejectInfo : Form
	{
		private Action<string> saveAfterEnter;

		private IContainer components = null;

		private LabelControl labelControl1;

		private SimpleButton btnSave;

		private TextEdit txtReasonreject;

		public frmRejectInfo(Action<string> save)
		{
			InitializeComponent();
			saveAfterEnter = save;
		}

		private void frmRejectInfo_Load(object sender, EventArgs e)
		{
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtReasonreject.Text))
			{
				MessageBox.Show("Thiếu trường dữ liệu bắt buộc");
			}
			else if (saveAfterEnter != null)
			{
				saveAfterEnter(txtReasonreject.Text);
				Close();
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
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.txtReasonreject = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)this.txtReasonreject.Properties).BeginInit();
			base.SuspendLayout();
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Maroon;
			this.labelControl1.Location = new System.Drawing.Point(12, 12);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(62, 13);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "Lý do từ chối";
			this.btnSave.Location = new System.Drawing.Point(285, 57);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Đồng ý";
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.txtReasonreject.Location = new System.Drawing.Point(12, 30);
			this.txtReasonreject.Name = "txtReasonreject";
			this.txtReasonreject.Properties.MaxLength = 500;
			this.txtReasonreject.Size = new System.Drawing.Size(348, 20);
			this.txtReasonreject.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(372, 89);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.labelControl1);
			base.Controls.Add(this.txtReasonreject);
			base.Name = "frmRejectInfo";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Thông tin từ chối ký";
			base.Load += new System.EventHandler(frmRejectInfo_Load);
			((System.ComponentModel.ISupportInitialize)this.txtReasonreject.Properties).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
