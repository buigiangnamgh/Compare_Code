using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Inventec.Common.Logging;
using Inventec.Common.SignFile;
using Inventec.Common.SignLibrary.CacheClient;
using Inventec.Common.SignLibrary.LibraryMessage;

namespace Inventec.Common.SignLibrary
{
	public class frmConfirmSign : Form
	{
		private Action actNo;

		private Action actYes;

		private IContainer components = null;

		private Label lblTitle;

		private SimpleButton btnYes;

		private SimpleButton btnNo;

		private CheckEdit chkState;

		private Label label1;

		public frmConfirmSign(Action actNo, Action actYes)
		{
			InitializeComponent();
			this.actNo = actNo;
			this.actYes = actYes;
		}

		private void frmConfirmSign_Load(object sender, EventArgs e)
		{
			try
			{
				btnYes.Focus();
				lblTitle.Text = MessageUitl.GetMessage("TuDongCapNhatChuKyThatBaiBanCoMuonTiepTucVaBoHienThiChuKy");
				if (!string.IsNullOrEmpty(CacheClientWorker.GetValue()))
				{
					chkState.Checked = true;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnNo_Click(object sender, EventArgs e)
		{
			try
			{
				if (chkState.Checked)
				{
					CacheClientWorker.ChangeValue("0");
				}
				if (actNo != null)
				{
					actNo();
				}
				Close();
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
				if (chkState.Checked)
				{
					CacheClientWorker.ChangeValue(Constans.DISPLAY_IMAGE_STAMP.ToString());
				}
				if (actYes != null)
				{
					actYes();
				}
				Close();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void chkState_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
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
			this.btnNo = new DevExpress.XtraEditors.SimpleButton();
			this.chkState = new DevExpress.XtraEditors.CheckEdit();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)this.chkState.Properties).BeginInit();
			base.SuspendLayout();
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.lblTitle.Location = new System.Drawing.Point(19, 17);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(509, 54);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Tài khoản thiếu thông tin ảnh, bạn có muốn bỏ hiển thị ảnh chữ ký không?";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnYes.Location = new System.Drawing.Point(183, 127);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(75, 23);
			this.btnYes.TabIndex = 1;
			this.btnYes.Text = "Có";
			this.btnYes.Click += new System.EventHandler(btnYes_Click);
			this.btnNo.Location = new System.Drawing.Point(264, 127);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(75, 23);
			this.btnNo.TabIndex = 1;
			this.btnNo.Text = "Không";
			this.btnNo.Click += new System.EventHandler(btnNo_Click);
			this.chkState.Location = new System.Drawing.Point(51, 74);
			this.chkState.Name = "chkState";
			this.chkState.Properties.Caption = "Lưu lại lựa chọn cho các lần ký sau";
			this.chkState.Size = new System.Drawing.Size(458, 19);
			this.chkState.TabIndex = 2;
			this.chkState.CheckedChanged += new System.EventHandler(chkState_CheckedChanged);
			this.label1.Location = new System.Drawing.Point(63, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(276, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = " (Lưu ý: nhấn Ctrl + Shift + R để xóa trạng thái đã lưu)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(546, 160);
			base.Controls.Add(this.chkState);
			base.Controls.Add(this.btnNo);
			base.Controls.Add(this.btnYes);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lblTitle);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmConfirmSign";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Hỏi đáp";
			base.Load += new System.EventHandler(frmConfirmSign_Load);
			((System.ComponentModel.ISupportInitialize)this.chkState.Properties).EndInit();
			base.ResumeLayout(false);
		}
	}
}
