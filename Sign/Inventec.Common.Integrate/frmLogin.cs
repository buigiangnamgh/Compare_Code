using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignToolViewer.Integrate;

namespace Inventec.Common.Integrate
{
	internal class frmLogin : Form
	{
		private Action<SignToken> actSignToken;

		private SignToken signToken;

		private IContainer components = null;

		private TextBox txtLoginName;

		private TextBox txtPassword;

		private Label label1;

		private Button btnSave;

		private Label label2;

		private TextBox txtEmrUri;

		private Label lblFortxtEmrUri;

		private TextBox txtAcsUri;

		private Label lblFortxtAcsUri;

		private TextBox txtFssUri;

		private Label lblFortxtFssUri;

		private TextBox txtHpsUri;

		private Label lblHpsUri;

		internal frmLogin(Action<SignToken> _actSignToken)
		{
			InitializeComponent();
			actSignToken = _actSignToken;
			if (GlobalStore.IsUseSendDTI)
			{
				lblFortxtEmrUri.Visible = false;
				lblFortxtAcsUri.Visible = false;
				lblFortxtFssUri.Visible = false;
				txtEmrUri.Visible = false;
				txtAcsUri.Visible = false;
				txtFssUri.Visible = false;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtLoginName.Text))
			{
				MessageBox.Show("Chưa nhập trường tên đăng nhập");
				return;
			}
			if (string.IsNullOrEmpty(txtPassword.Text))
			{
				MessageBox.Show("Chưa nhập trường mật khẩu");
				return;
			}
			if (string.IsNullOrEmpty(txtEmrUri.Text))
			{
				MessageBox.Show("Chưa nhập trường địa chỉ hệ thống EMR");
				return;
			}
			if (string.IsNullOrEmpty(txtAcsUri.Text))
			{
				MessageBox.Show("Chưa nhập trường địa chỉ hệ thống xác thực (ACS)");
				return;
			}
			if (string.IsNullOrEmpty(txtFssUri.Text))
			{
				MessageBox.Show("Chưa nhập trường địa chỉ hệ thống QL file tập trung (FSS)");
				return;
			}
			ConstanIG.ACS_BASE_URI = txtAcsUri.Text;
			GlobalStore.EMR_BASE_URI = txtEmrUri.Text;
			FssConstant.BASE_URI = txtFssUri.Text;
			GlobalStore.HPS_BASE_URI = txtHpsUri.Text;
			RegistryProcessor.Write("ACS_BASE_URI", ConstanIG.ACS_BASE_URI);
			RegistryProcessor.Write("EMR_BASE_URI", GlobalStore.EMR_BASE_URI);
			RegistryProcessor.Write("FSS_BASE_URI", FssConstant.BASE_URI);
			RegistryProcessor.Write("HPS_BASE_URI", GlobalStore.HPS_BASE_URI);
			Login(txtLoginName.Text, txtPassword.Text);
		}

		private void frmLogin_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(ConstanIG.ACS_BASE_URI))
			{
				if (string.IsNullOrEmpty((string)RegistryProcessor.Read("ACS_BASE_URI")))
				{
					RegistryProcessor.Write("ACS_BASE_URI", ConstanIG.ACS_BASE_URI);
				}
			}
			else
			{
				ConstanIG.ACS_BASE_URI = (string)RegistryProcessor.Read("ACS_BASE_URI");
			}
			if (!string.IsNullOrEmpty(GlobalStore.EMR_BASE_URI))
			{
				if (string.IsNullOrEmpty((string)RegistryProcessor.Read("EMR_BASE_URI")))
				{
					RegistryProcessor.Write("EMR_BASE_URI", GlobalStore.EMR_BASE_URI);
				}
			}
			else
			{
				GlobalStore.EMR_BASE_URI = (string)RegistryProcessor.Read("EMR_BASE_URI");
			}
			if (!string.IsNullOrEmpty(FssConstant.BASE_URI))
			{
				if (string.IsNullOrEmpty((string)RegistryProcessor.Read("FSS_BASE_URI")))
				{
					RegistryProcessor.Write("FSS_BASE_URI", FssConstant.BASE_URI);
				}
			}
			else
			{
				FssConstant.BASE_URI = (string)RegistryProcessor.Read("FSS_BASE_URI");
			}
			if (!string.IsNullOrEmpty(GlobalStore.HPS_BASE_URI))
			{
				if (string.IsNullOrEmpty((string)RegistryProcessor.Read("HPS_BASE_URI")))
				{
					RegistryProcessor.Write("HPS_BASE_URI", GlobalStore.HPS_BASE_URI);
				}
			}
			else
			{
				GlobalStore.HPS_BASE_URI = (string)RegistryProcessor.Read("HPS_BASE_URI");
			}
			if (!string.IsNullOrEmpty(ConstanIG.ACS_BASE_URI))
			{
				txtAcsUri.Text = ConstanIG.ACS_BASE_URI;
			}
			if (!string.IsNullOrEmpty(GlobalStore.EMR_BASE_URI))
			{
				txtEmrUri.Text = GlobalStore.EMR_BASE_URI;
			}
			if (!string.IsNullOrEmpty(FssConstant.BASE_URI))
			{
				txtFssUri.Text = FssConstant.BASE_URI;
			}
			if (string.IsNullOrEmpty(ConstanIG.ACS_BASE_URI) || string.IsNullOrEmpty(GlobalStore.EMR_BASE_URI))
			{
				return;
			}
			CommonParam commonParam = new CommonParam();
			ClientTokenManager clientTokenManager = new ClientTokenManager("HIS", ConstanIG.ACS_BASE_URI);
			clientTokenManager.UseRegistry(true);
			signToken = new SignToken();
			signToken.TokenData = clientTokenManager.Init(commonParam);
			if (signToken.TokenData == null && !string.IsNullOrEmpty(signToken.LoginName) && !string.IsNullOrEmpty(signToken.Password))
			{
				Login(signToken.LoginName, signToken.Password);
			}
			else if (signToken.TokenData != null)
			{
				GlobalStore.AcsConsumer.SetTokenCode(signToken.TokenData.TokenCode);
				GlobalStore.EmrConsumer.SetTokenCode(signToken.TokenData.TokenCode);
				signToken.LoginName = signToken.TokenData.User.LoginName;
				signToken.UserName = signToken.TokenData.User.UserName;
				signToken.TokenCode = signToken.TokenData.TokenCode;
				if (actSignToken != null)
				{
					actSignToken(signToken);
				}
				Close();
			}
		}

		public bool Login(string loginName, string password)
		{
			bool result = false;
			try
			{
				CommonParam commonParam = new CommonParam();
				ClientTokenManager clientTokenManager = new ClientTokenManager("HIS", ConstanIG.ACS_BASE_URI);
				clientTokenManager.UseRegistry(true);
				signToken = new SignToken();
				signToken.TokenData = clientTokenManager.Login(commonParam, loginName, password);
				if (signToken.TokenData != null && signToken.TokenData.User != null)
				{
					signToken.LoginName = signToken.TokenData.User.LoginName;
					signToken.UserName = signToken.TokenData.User.UserName;
					signToken.TokenCode = signToken.TokenData.TokenCode;
					GlobalStore.AcsConsumer.SetTokenCode(signToken.TokenData.TokenCode);
					GlobalStore.EmrConsumer.SetTokenCode(signToken.TokenData.TokenCode);
					if (actSignToken != null)
					{
						actSignToken(signToken);
					}
					result = true;
					Close();
				}
				else
				{
					MessageBox.Show("Lưu cấu hình & truy cập vào hệ thống EMR thất bại");
					LogSystem.Info("Tai khoan hoac mat khau truy cap vao he thong EMR khong chinh xac____" + LogUtil.TraceData("loginName", (object)loginName) + "____" + LogUtil.TraceData("appCode", (object)"HIS") + "____" + LogUtil.TraceData("ACS_BASE_URI", (object)ConstanIG.ACS_BASE_URI));
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				result = false;
			}
			return result;
		}

		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btnSave_Click(null, null);
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
			this.txtLoginName = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtEmrUri = new System.Windows.Forms.TextBox();
			this.lblFortxtEmrUri = new System.Windows.Forms.Label();
			this.txtAcsUri = new System.Windows.Forms.TextBox();
			this.lblFortxtAcsUri = new System.Windows.Forms.Label();
			this.txtFssUri = new System.Windows.Forms.TextBox();
			this.lblFortxtFssUri = new System.Windows.Forms.Label();
			this.txtHpsUri = new System.Windows.Forms.TextBox();
			this.lblHpsUri = new System.Windows.Forms.Label();
			base.SuspendLayout();
			this.txtLoginName.Location = new System.Drawing.Point(180, 17);
			this.txtLoginName.Name = "txtLoginName";
			this.txtLoginName.Size = new System.Drawing.Size(196, 20);
			this.txtLoginName.TabIndex = 1;
			this.txtPassword.Location = new System.Drawing.Point(180, 44);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(196, 20);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(txtPassword_KeyDown);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(89, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Tên đăng nhập:";
			this.btnSave.Location = new System.Drawing.Point(301, 173);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Lưu";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(118, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Mật khẩu:";
			this.txtEmrUri.Location = new System.Drawing.Point(180, 70);
			this.txtEmrUri.Name = "txtEmrUri";
			this.txtEmrUri.Size = new System.Drawing.Size(196, 20);
			this.txtEmrUri.TabIndex = 3;
			this.lblFortxtEmrUri.AutoSize = true;
			this.lblFortxtEmrUri.Location = new System.Drawing.Point(13, 73);
			this.lblFortxtEmrUri.Name = "lblFortxtEmrUri";
			this.lblFortxtEmrUri.Size = new System.Drawing.Size(160, 13);
			this.lblFortxtEmrUri.TabIndex = 1;
			this.lblFortxtEmrUri.Text = "Địa chỉ hệ thống backend EMR:";
			this.txtAcsUri.Location = new System.Drawing.Point(180, 96);
			this.txtAcsUri.Name = "txtAcsUri";
			this.txtAcsUri.Size = new System.Drawing.Size(196, 20);
			this.txtAcsUri.TabIndex = 4;
			this.lblFortxtAcsUri.AutoSize = true;
			this.lblFortxtAcsUri.Location = new System.Drawing.Point(11, 99);
			this.lblFortxtAcsUri.Name = "lblFortxtAcsUri";
			this.lblFortxtAcsUri.Size = new System.Drawing.Size(162, 13);
			this.lblFortxtAcsUri.TabIndex = 1;
			this.lblFortxtAcsUri.Text = "Địa chỉ hệ thống xác thực (ACS):";
			this.txtFssUri.Location = new System.Drawing.Point(180, 122);
			this.txtFssUri.Name = "txtFssUri";
			this.txtFssUri.Size = new System.Drawing.Size(196, 20);
			this.txtFssUri.TabIndex = 4;
			this.lblFortxtFssUri.AutoSize = true;
			this.lblFortxtFssUri.Location = new System.Drawing.Point(20, 125);
			this.lblFortxtFssUri.Name = "lblFortxtFssUri";
			this.lblFortxtFssUri.Size = new System.Drawing.Size(153, 13);
			this.lblFortxtFssUri.TabIndex = 1;
			this.lblFortxtFssUri.Text = "Địa chỉ hệ thống QL File (FSS):";
			this.txtHpsUri.Location = new System.Drawing.Point(180, 147);
			this.txtHpsUri.Name = "txtHpsUri";
			this.txtHpsUri.Size = new System.Drawing.Size(196, 20);
			this.txtHpsUri.TabIndex = 4;
			this.lblHpsUri.AutoSize = true;
			this.lblHpsUri.Location = new System.Drawing.Point(20, 150);
			this.lblHpsUri.Name = "lblHpsUri";
			this.lblHpsUri.Size = new System.Drawing.Size(155, 13);
			this.lblHpsUri.TabIndex = 1;
			this.lblHpsUri.Text = "Địa chỉ hệ thống VNyTe (HPS):";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(388, 208);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.lblHpsUri);
			base.Controls.Add(this.lblFortxtFssUri);
			base.Controls.Add(this.lblFortxtAcsUri);
			base.Controls.Add(this.lblFortxtEmrUri);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.txtHpsUri);
			base.Controls.Add(this.txtFssUri);
			base.Controls.Add(this.txtAcsUri);
			base.Controls.Add(this.txtEmrUri);
			base.Controls.Add(this.txtLoginName);
			base.Name = "frmLogin";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tham số cấu hình";
			base.Load += new System.EventHandler(frmLogin_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
