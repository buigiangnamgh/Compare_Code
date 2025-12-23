using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary
{
	public class frmConfirmAutoUpdateSign : Form
	{
		private bool isYes;

		private Action<bool> actYes;

		private IContainer components = null;

		private Label lblTitle;

		private SimpleButton btnYes;

		public frmConfirmAutoUpdateSign(Action<bool> actYes)
		{
			InitializeComponent();
			this.actYes = actYes;
		}

		private void frmConfirmSign_Load(object sender, EventArgs e)
		{
			try
			{
				btnYes.Focus();
				lblTitle.Text = MessageUitl.GetMessage("TaiKhoanThieuThongTinAnhHeThongTuDongCapNhat");
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnYes_Click(object sender, EventArgs e)
		{
			try
			{
				if (actYes != null)
				{
					isYes = true;
					actYes(isYes);
				}
				Close();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void frmConfirmAutoUpdateSign_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				if (!isYes && actYes != null)
				{
					actYes(isYes);
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
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnYes = new DevExpress.XtraEditors.SimpleButton();
			base.SuspendLayout();
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.lblTitle.Location = new System.Drawing.Point(25, 9);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(509, 51);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Tài khoản thiếu thông tin ảnh chữ ký. Hệ thống sẽ tự động cập nhật ảnh chữ ký theo thông tin chứng thư";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnYes.Location = new System.Drawing.Point(235, 76);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(75, 23);
			this.btnYes.TabIndex = 1;
			this.btnYes.Text = "Tiếp tục";
			this.btnYes.Click += new System.EventHandler(btnYes_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(546, 104);
			base.Controls.Add(this.btnYes);
			base.Controls.Add(this.lblTitle);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmConfirmAutoUpdateSign";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Thông báo";
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmConfirmAutoUpdateSign_FormClosed);
			base.Load += new System.EventHandler(frmConfirmSign_Load);
			base.ResumeLayout(false);
		}
	}
}
