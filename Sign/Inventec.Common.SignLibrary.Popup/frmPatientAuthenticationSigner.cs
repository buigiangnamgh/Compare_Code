using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Newtonsoft.Json;

namespace Inventec.Common.SignLibrary.Popup
{
	public class frmPatientAuthenticationSigner : Form
	{
		private const string VerifyUrl = "http://localhost:7000/api/v1/verify";

		private const string HpsApiUrl = "/api/HpsPatientService/NotificationSend";

		private EMR_PATIENT_CERTIFICATE patientCertificate;

		private InputADO inputSignADO;

		private EMR_TREATMENT emrTreatment;

		private Action<bool> DelegateResult;

		private IContainer components = null;

		private SimpleButton btnSignCCCD;

		private SimpleButton btnSignVNyte;

		public frmPatientAuthenticationSigner(InputADO _input, EMR_TREATMENT Treatment, Action<bool> _delegateResult)
		{
			LogSystem.Info("frmPatientAuthenticationSigner:1");
			InitializeComponent();
			inputSignADO = _input;
			DelegateResult = _delegateResult;
			emrTreatment = Treatment;
		}

		private void frmPatientAuthenticationSigner_Load(object sender, EventArgs e)
		{
			LogSystem.Info("frmPatientAuthenticationSigner_Load:2");
			LoadPatientCertificate();
		}

		private void LoadPatientCertificate()
		{
			try
			{
				CommonParam param = new CommonParam();
				if (emrTreatment == null && string.IsNullOrWhiteSpace(emrTreatment.PATIENT_CODE))
				{
					MessageBox.Show("Dữ liệu PATIENT_CODE null.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				EmrPatientCertificateFilter emrPatientCertificateFilter = new EmrPatientCertificateFilter();
				emrPatientCertificateFilter.PATIENT_CODE = emrTreatment.PATIENT_CODE;
				EmrPatientCertificateFilter filter = emrPatientCertificateFilter;
				long? num = DateTimeConvert.SystemDateTimeToTimeNumber(DateTime.Now.Date);
				List<EMR_PATIENT_CERTIFICATE> list = GlobalStore.EmrConsumer.Get<List<EMR_PATIENT_CERTIFICATE>>("api/EmrPatientCertificate/Get", param, filter, new object[0]);
				LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => param), param));
				if (list != null && list.Count > 0)
				{
					patientCertificate = list.FirstOrDefault();
					if (string.IsNullOrEmpty(patientCertificate.SERIAL_NUMBER))
					{
						MessageBox.Show("Không tìm thấy thông tin chứng thư. Vui lòng cập nhật chứng thư số cho bệnh nhân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						Close();
						return;
					}
					if (patientCertificate == null || (patientCertificate.EXPIRED_DATE.HasValue && num > patientCertificate.EXPIRED_DATE.Value))
					{
						MessageBox.Show("Chứng thư đã hết hạn. Vui lòng gia hạn chứng thư số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						Close();
						return;
					}
					emrTreatment.CCCD_NUMBER = patientCertificate.CCCD_NUMBER;
				}
				btnSignCCCD.Enabled = true;
				btnSignVNyte.Enabled = true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnSignCCCD_Click(object sender, EventArgs e)
		{
			VerifyCCCDAsync();
		}

		public async Task VerifyCCCDAsync()
		{
			bool IsSuccess = false;
			try
			{
				using (HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync("http://localhost:7000/api/v1/verify");
					if (response.IsSuccessStatusCode)
					{
						Root root = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());
						Root result = root;
						if (result != null && result.Result != null && result.Result.Data != null && result.Result.Data.isPass && result.Result.Data.IdentifyNumber == patientCertificate.CCCD_NUMBER)
						{
							MessageBox.Show("Xác thực CCCD thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							IsSuccess = true;
						}
						else
						{
							MessageBox.Show("Xác thực căn cước công dân không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							IsSuccess = false;
						}
					}
					else
					{
						MessageBox.Show("Lỗi kết nối đến thiết bị xác thực.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						IsSuccess = false;
					}
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				MessageBox.Show("Lỗi khi xác thực CCCD: " + ex2.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				LogSystem.Error(ex2);
				IsSuccess = false;
			}
			finally
			{
				if (DelegateResult != null)
				{
					DelegateResult(IsSuccess);
					Close();
				}
			}
		}

		private void btnSignVNyte_Click(object sender, EventArgs e)
		{
			SendNotificationToVNyTe();
		}

		public void SendNotificationToVNyTe()
		{
			try
			{
				if (string.IsNullOrEmpty(inputSignADO.Treatment.TREATMENT_CODE))
				{
					MessageBox.Show("Không có thông tin hồ sơ điều trị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				using (HttpClient httpClient = new HttpClient())
				{
					string requestUri = GlobalStore.HPS_BASE_URI.Trim('/') + "/" + "/api/HpsPatientService/NotificationSend".Trim('/');
					List<string> list = new List<string>();
					list.Add(patientCertificate.PERSON_CODE);
					var value = new
					{
						ApiData = new
						{
							personCode = list,
							treatmentCode = inputSignADO.Treatment.TREATMENT_CODE,
							content = "Có văn bản " + inputSignADO.DocumentName + " cần ký. Vui lòng xác nhận!",
							categoryCode = "034",
							hisCode = inputSignADO.HisCode,
							applicationCode = "HIS"
						}
					};
					LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => patientCertificate.PERSON_CODE), patientCertificate.PERSON_CODE));
					httpClient.DefaultRequestHeaders.Accept.Clear();
					httpClient.DefaultRequestHeaders.Clear();
					httpClient.DefaultRequestHeaders.Add("TokenCode", GlobalStore.TokenCode);
					string jsonContent = JsonConvert.SerializeObject(value);
					LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => jsonContent), jsonContent));
					StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
					HttpResponseMessage result = httpClient.PostAsync(requestUri, content).Result;
					if (result.IsSuccessStatusCode)
					{
						string responseContent = result.Content.ReadAsStringAsync().Result;
						ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(responseContent);
						LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName(() => responseContent), responseContent));
						if (apiResult.success && ((apiResult.param != null && (apiResult.param.Messages == null || apiResult.param.Messages.Count == 0)) || apiResult.param == null))
						{
							StartCheckingPatientSignAuth();
						}
					}
					else
					{
						MessageBox.Show("Không thể gửi thông báo tới VNyTe.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi gửi thông báo: " + ex.Message);
			}
		}

		private void StartCheckingPatientSignAuth()
		{
			bool obj = false;
			DateTime now = DateTime.Now;
			while (true)
			{
				bool flag = true;
				try
				{
					string text = GlobalStore.HPS_BASE_URI + "api/HpsPatientService/PatientSignAuthGet?PeopleCode=" + patientCertificate.PERSON_CODE + "&TreatmentCode=" + inputSignADO.Treatment.TREATMENT_CODE + "&HisCode=" + inputSignADO.HisCode;
					EmrPatientSignAuthFilter emrPatientSignAuthFilter = new EmrPatientSignAuthFilter();
					emrPatientSignAuthFilter.PERSON_CODE__EXACT = patientCertificate.PERSON_CODE;
					emrPatientSignAuthFilter.TREATMENT_CODE__EXACT = inputSignADO.Treatment.TREATMENT_CODE;
					emrPatientSignAuthFilter.HIS_CODE__EXACT = inputSignADO.HisCode;
					List<EMR_PATIENT_SIGN_AUTH> list = GlobalStore.EmrConsumer.Get<List<EMR_PATIENT_SIGN_AUTH>>("api/EmrPatientSignAuth/Get", new CommonParam(), emrPatientSignAuthFilter, new object[0]);
					if (list == null || list.Count == 0)
					{
						MessageBox.Show("Không lấy được thông tin trạng thái xác thực.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						StopCheckingPatientSignAuth();
						break;
					}
					EMR_PATIENT_SIGN_AUTH eMR_PATIENT_SIGN_AUTH = list.OrderByDescending((EMR_PATIENT_SIGN_AUTH x) => x.ID).FirstOrDefault();
					if (eMR_PATIENT_SIGN_AUTH != null)
					{
						if (eMR_PATIENT_SIGN_AUTH.PERSON_CODE == patientCertificate.PERSON_CODE && eMR_PATIENT_SIGN_AUTH.TREATMENT_CODE == inputSignADO.Treatment.TREATMENT_CODE && eMR_PATIENT_SIGN_AUTH.HIS_CODE == inputSignADO.HisCode && eMR_PATIENT_SIGN_AUTH.CREATE_TIME > long.Parse(now.AddMinutes(-5.0).ToString("yyyyMMddHHmmss")))
						{
							if (eMR_PATIENT_SIGN_AUTH.AUTH_STATUS == 1)
							{
								MessageBox.Show("Người dùng đã xác nhận ký, thực hiện gọi API ký số...");
								StopCheckingPatientSignAuth();
								obj = true;
								break;
							}
							if (eMR_PATIENT_SIGN_AUTH.AUTH_STATUS == 2)
							{
								MessageBox.Show("Người dùng đã từ chối ký văn bản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								StopCheckingPatientSignAuth();
								break;
							}
						}
						else
						{
							if (eMR_PATIENT_SIGN_AUTH.CREATE_TIME < long.Parse(now.AddMinutes(-5.0).ToString("yyyyMMddHHmmss")))
							{
								break;
							}
							MessageBox.Show("Bản ghi không thỏa điều kiện, tiếp tục kiểm tra...");
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi kiểm tra trạng thái: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					StopCheckingPatientSignAuth();
					break;
				}
				Thread.Sleep(5000);
			}
			if (DelegateResult != null)
			{
				DelegateResult(obj);
				Close();
			}
		}

		private void StopCheckingPatientSignAuth()
		{
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
			this.btnSignCCCD = new DevExpress.XtraEditors.SimpleButton();
			this.btnSignVNyte = new DevExpress.XtraEditors.SimpleButton();
			base.SuspendLayout();
			this.btnSignCCCD.Location = new System.Drawing.Point(12, 10);
			this.btnSignCCCD.Name = "btnSignCCCD";
			this.btnSignCCCD.Size = new System.Drawing.Size(147, 63);
			this.btnSignCCCD.TabIndex = 0;
			this.btnSignCCCD.Text = "Xác thực \r\nký bằng CCCD";
			this.btnSignCCCD.ToolTip = "Xác thực ký bằng căn cước công dân";
			this.btnSignCCCD.Click += new System.EventHandler(btnSignCCCD_Click);
			this.btnSignVNyte.Location = new System.Drawing.Point(165, 10);
			this.btnSignVNyte.Name = "btnSignVNyte";
			this.btnSignVNyte.Size = new System.Drawing.Size(161, 63);
			this.btnSignVNyte.TabIndex = 1;
			this.btnSignVNyte.Text = "Xác thực \r\nký bằng app VNyte";
			this.btnSignVNyte.Click += new System.EventHandler(btnSignVNyte_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(338, 85);
			base.Controls.Add(this.btnSignVNyte);
			base.Controls.Add(this.btnSignCCCD);
			base.Name = "frmPatientAuthenticationSigner";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Phương án xác thực bệnh nhân ký";
			base.Load += new System.EventHandler(frmPatientAuthenticationSigner_Load);
			base.ResumeLayout(false);
		}
	}
}
