using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using EMR.SDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Newtonsoft.Json;

namespace Inventec.Common.SignLibrary.Popup
{
	public class frmPatientAuthenticationSigner : Form
	{
		private const string VerifyUrl = "http://localhost:7000/api/v1/verify";

		private const string FignerUrl = "http://localhost:7000/api/v1/verify/finger";

		private const string FignerMatchUrl = "http://localhost:7000/api/v1/verify/finger/matches";

		private const string HpsApiUrl = "/api/HpsPatientService/NotificationSend";

		private EMR_PATIENT_CERTIFICATE patientCertificate;

		private InputADO inputSignADO;

		private EMR_TREATMENT emrTreatment;

		private Action<string> DelegateImage;

		private Action<bool> DelegateResult;

		private bool isPatient;

		private IContainer components = null;

		private SimpleButton btnSignCCCD;

		private SimpleButton btnFignerPrintAuth;

		public string VerifiedIdentifyNumber { get; private set; }

		public frmPatientAuthenticationSigner(InputADO _input, EMR_TREATMENT Treatment, Action<bool> _delegateResult, Action<string> _delImage, bool isPatient)
		{
			LogSystem.Info("frmPatientAuthenticationSigner:1");
			InitializeComponent();
			inputSignADO = _input;
			DelegateResult = _delegateResult;
			emrTreatment = Treatment;
			DelegateImage = _delImage;
			this.isPatient = isPatient;
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
				if (!isPatient)
				{
					return;
				}
				CommonParam commonParam = new CommonParam();
				if (emrTreatment == null || string.IsNullOrWhiteSpace(emrTreatment.PATIENT_CODE))
				{
					MessageBox.Show("Dữ liệu PATIENT_CODE null.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				bool flag = !string.IsNullOrWhiteSpace(emrTreatment.CCCD_NUMBER);
				long? num = DateTimeConvert.SystemDateTimeToTimeNumber(DateTime.Now.Date);
				if (flag)
				{
					patientCertificate = CheckPatientCertificateByCCCDAsync(emrTreatment.CCCD_NUMBER);
					if (patientCertificate != null && !string.IsNullOrEmpty(patientCertificate.SERIAL_NUMBER) && (!patientCertificate.EXPIRED_DATE.HasValue || num <= patientCertificate.EXPIRED_DATE.Value))
					{
						btnSignCCCD.Enabled = true;
						btnFignerPrintAuth.Enabled = true;
						return;
					}
					DialogResult dialogResult = MessageBox.Show("Chứng thư không hợp lệ hoặc đã hết hạn. Bạn có muốn phát hành chứng thư không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult == DialogResult.Yes && inputSignADO.DelegateIssuanceCer != null)
					{
						inputSignADO.DelegateIssuanceCer();
						patientCertificate = CheckPatientCertificateByCCCDAsync(emrTreatment.CCCD_NUMBER);
						bool enabled = patientCertificate != null && !string.IsNullOrEmpty(patientCertificate.SERIAL_NUMBER) && (!patientCertificate.EXPIRED_DATE.HasValue || num <= patientCertificate.EXPIRED_DATE.Value);
						btnSignCCCD.Enabled = enabled;
						btnFignerPrintAuth.Enabled = true;
					}
					else
					{
						btnSignCCCD.Enabled = false;
						btnFignerPrintAuth.Enabled = true;
					}
				}
				else
				{
					DialogResult dialogResult2 = MessageBox.Show("Bệnh nhân chưa có CCCD. Bạn có muốn phát hành chứng thư không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					btnSignCCCD.Enabled = true;
					if (dialogResult2 == DialogResult.Yes && inputSignADO.DelegateIssuanceCer != null)
					{
						inputSignADO.DelegateIssuanceCer();
						btnFignerPrintAuth.Enabled = true;
					}
					else
					{
						btnFignerPrintAuth.Enabled = true;
					}
				}
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
					WaitingManager.Show();
					HttpResponseMessage response = await client.GetAsync("http://localhost:7000/api/v1/verify");
					if (response.IsSuccessStatusCode)
					{
						Root result = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());
						LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<Root>((Expression<Func<Root>>)(() => result)), (object)result));
						if (patientCertificate != null && isPatient && result.Result.Data.IdentifyNumber != patientCertificate.CCCD_NUMBER)
						{
							MessageBox.Show("Xác thực CCCD không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							IsSuccess = false;
							return;
						}
						if (result != null && result.Result != null && result.Result.Data != null && result.Result.Data.isPass)
						{
							EMR_PATIENT_CERTIFICATE cert = CheckPatientCertificateByCCCDAsync(result.Result.Data.IdentifyNumber);
							byte[] SignImage = cert.SIGN_IMAGE;
							string SignImageBase64 = ((cert != null) ? Convert.ToBase64String(SignImage) : null);
							if (DelegateImage != null)
							{
								DelegateImage(SignImageBase64);
							}
							if (cert != null && !string.IsNullOrEmpty(cert.SERIAL_NUMBER))
							{
								MessageBox.Show("Xác thực CCCD và chứng thư thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								VerifiedIdentifyNumber = result.Result.Data.IdentifyNumber;
								LogSession.Info(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => VerifiedIdentifyNumber)), (object)VerifiedIdentifyNumber));
								IsSuccess = true;
							}
							else
							{
								DialogResult dialogResult = MessageBox.Show("Bệnh nhân chưa có chứng thư không thể thực hiện ký số. Bạn có muốn phát hành chứng thư hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
								if (dialogResult == DialogResult.Yes && inputSignADO.DelegateIssuanceCer != null)
								{
									inputSignADO.DelegateIssuanceCer();
								}
								IsSuccess = false;
							}
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

		private EMR_PATIENT_CERTIFICATE CheckPatientCertificateByCCCDAsync(string cccdNumber)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			try
			{
				CommonParam commonParam = new CommonParam();
				EmrPatientCertificateCheckSDO data = new EmrPatientCertificateCheckSDO
				{
					CccdNumber = cccdNumber
				};
				return GlobalStore.EmrConsumer.Post<EMR_PATIENT_CERTIFICATE>("/api/EmrPatientCertificate/Check", commonParam, data, new object[0]);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return null;
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
					var anon = new
					{
						ApiData = new
						{
							personCode = new List<string> { patientCertificate.PERSON_CODE },
							treatmentCode = inputSignADO.Treatment.TREATMENT_CODE,
							content = string.Format("Có văn bản {0} cần ký. Vui lòng xác nhận!", inputSignADO.DocumentName),
							categoryCode = "034",
							hisCode = inputSignADO.HisCode,
							applicationCode = "HIS"
						}
					};
					LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => patientCertificate.PERSON_CODE)), (object)patientCertificate.PERSON_CODE));
					httpClient.DefaultRequestHeaders.Accept.Clear();
					httpClient.DefaultRequestHeaders.Clear();
					httpClient.DefaultRequestHeaders.Add("TokenCode", GlobalStore.TokenCode);
					string jsonContent = JsonConvert.SerializeObject((object)anon);
					LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => jsonContent)), (object)jsonContent));
					StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
					HttpResponseMessage result = httpClient.PostAsync(requestUri, content).Result;
					if (result.IsSuccessStatusCode)
					{
						string responseContent = result.Content.ReadAsStringAsync().Result;
						ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(responseContent);
						LogSystem.Info(LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => responseContent)), (object)responseContent));
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
				MessageBox.Show(string.Format("Lỗi khi gửi thông báo: {0}", ex.Message));
			}
		}

		private void StartCheckingPatientSignAuth()
		{
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			bool obj = false;
			DateTime now = DateTime.Now;
			while (true)
			{
				try
				{
					string text = string.Format("{0}api/HpsPatientService/PatientSignAuthGet?PeopleCode={1}&TreatmentCode={2}&HisCode={3}", GlobalStore.HPS_BASE_URI, patientCertificate.PERSON_CODE, inputSignADO.Treatment.TREATMENT_CODE, inputSignADO.HisCode);
					EmrPatientSignAuthFilter val = new EmrPatientSignAuthFilter();
					val.PERSON_CODE__EXACT = patientCertificate.PERSON_CODE;
					val.TREATMENT_CODE__EXACT = inputSignADO.Treatment.TREATMENT_CODE;
					val.HIS_CODE__EXACT = inputSignADO.HisCode;
					List<EMR_PATIENT_SIGN_AUTH> list = GlobalStore.EmrConsumer.Get<List<EMR_PATIENT_SIGN_AUTH>>("api/EmrPatientSignAuth/Get", new CommonParam(), val, new object[0]);
					if (list == null || list.Count == 0)
					{
						MessageBox.Show("Không lấy được thông tin trạng thái xác thực.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
					}
					EMR_PATIENT_SIGN_AUTH val2 = list.OrderByDescending((EMR_PATIENT_SIGN_AUTH x) => x.ID).FirstOrDefault();
					if (val2 != null)
					{
						if (val2.PERSON_CODE == patientCertificate.PERSON_CODE && val2.TREATMENT_CODE == inputSignADO.Treatment.TREATMENT_CODE && val2.HIS_CODE == inputSignADO.HisCode && val2.CREATE_TIME > long.Parse(now.AddMinutes(-5.0).ToString("yyyyMMddHHmmss")))
						{
							if (val2.AUTH_STATUS == 1)
							{
								MessageBox.Show("Người dùng đã xác nhận ký, thực hiện gọi API ký số...");
								obj = true;
								break;
							}
							if (val2.AUTH_STATUS == 2)
							{
								MessageBox.Show("Người dùng đã từ chối ký văn bản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								break;
							}
						}
						else
						{
							if (val2.CREATE_TIME < long.Parse(now.AddMinutes(-5.0).ToString("yyyyMMddHHmmss")))
							{
								break;
							}
							MessageBox.Show("Bản ghi không thỏa điều kiện, tiếp tục kiểm tra...");
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("Lỗi kiểm tra trạng thái: {0}", ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand));
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

		private void btnFignerPrintAuth_Click(object sender, EventArgs e)
		{
			FingerPrintAuth();
		}

		public async void FingerPrintAuth()
		{
			bool IsSuccess = false;
			FingerApiResponse fingerResponse = null;
			try
			{
				using (HttpClient client = new HttpClient())
				{
					client.Timeout = TimeSpan.FromSeconds(15.0);
					HttpResponseMessage response = await client.GetAsync("http://localhost:7000/api/v1/verify/finger");
					if (!response.IsSuccessStatusCode)
					{
						MessageBox.Show(string.Format("Không gọi được API: {0}", response.StatusCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand));
						IsSuccess = false;
						return;
					}
					string jsonResponse = await response.Content.ReadAsStringAsync();
					LogSystem.Info(string.Format("[FingerPrintAuth] Response: {0}", jsonResponse));
					fingerResponse = JsonConvert.DeserializeObject<FingerApiResponse>(jsonResponse);
					if (fingerResponse == null || fingerResponse.result == null || string.IsNullOrEmpty(fingerResponse.result.data))
					{
						MessageBox.Show("Không có dữ liệu vân tay trả về!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						IsSuccess = false;
						return;
					}
					try
					{
						LogSystem.Info(string.Format("[FingerPrintAuth] InputADO: {0}", JsonConvert.SerializeObject((object)inputSignADO)));
						if (!string.IsNullOrEmpty(fingerResponse.result.data))
						{
							try
							{
								Utils.SignPadImageData = Convert.FromBase64String(fingerResponse.result.data);
								MessageBox.Show("Lấy dữ liệu vân tay thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								VerifiedIdentifyNumber = null;
								IsSuccess = true;
							}
							catch (FormatException ex)
							{
								FormatException ex2 = ex;
								LogSystem.Error((Exception)ex2);
								MessageBox.Show("Dữ liệu vân tay trả về không đúng định dạng base64.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								IsSuccess = false;
							}
						}
						else
						{
							MessageBox.Show("Không có dữ liệu vân tay trả về!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}
					catch (Exception ex3)
					{
						LogSystem.Error(ex3);
						MessageBox.Show("Lỗi khi lấy vân tay: " + ex3.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						IsSuccess = false;
					}
				}
			}
			catch (HttpRequestException ex4)
			{
				HttpRequestException ex5 = ex4;
				LogSystem.Error((Exception)ex5);
				MessageBox.Show("Không kết nối được API vân tay. Hãy kiểm tra dịch vụ tại cổng 7000.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				IsSuccess = false;
			}
			catch (Exception ex6)
			{
				Exception ex7 = ex6;
				LogSystem.Error(ex7);
				MessageBox.Show("Lỗi khi lấy vân tay: " + ex7.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				IsSuccess = false;
			}
			finally
			{
				if (DelegateImage != null)
				{
					DelegateImage((fingerResponse != null) ? fingerResponse.result.data : null);
				}
				if (DelegateResult != null)
				{
					DelegateResult(IsSuccess);
					Close();
				}
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
			this.btnSignCCCD = new DevExpress.XtraEditors.SimpleButton();
			this.btnFignerPrintAuth = new DevExpress.XtraEditors.SimpleButton();
			base.SuspendLayout();
			this.btnSignCCCD.Location = new System.Drawing.Point(12, 10);
			this.btnSignCCCD.Name = "btnSignCCCD";
			this.btnSignCCCD.Size = new System.Drawing.Size(147, 63);
			this.btnSignCCCD.TabIndex = 0;
			this.btnSignCCCD.Text = "Xác thực \r\nký bằng CCCD";
			this.btnSignCCCD.ToolTip = "Xác thực ký bằng căn cước công dân";
			this.btnSignCCCD.Click += new System.EventHandler(btnSignCCCD_Click);
			this.btnFignerPrintAuth.Location = new System.Drawing.Point(165, 10);
			this.btnFignerPrintAuth.Name = "btnFignerPrintAuth";
			this.btnFignerPrintAuth.Size = new System.Drawing.Size(161, 63);
			this.btnFignerPrintAuth.TabIndex = 1;
			this.btnFignerPrintAuth.Text = "Xác thực \r\nký bằng vân tay";
			this.btnFignerPrintAuth.Click += new System.EventHandler(btnFignerPrintAuth_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(338, 85);
			base.Controls.Add(this.btnFignerPrintAuth);
			base.Controls.Add(this.btnSignCCCD);
			base.Name = "frmPatientAuthenticationSigner";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Phương án xác thực bệnh nhân ký";
			base.Load += new System.EventHandler(frmPatientAuthenticationSigner_Load);
			base.ResumeLayout(false);
		}
	}
}
