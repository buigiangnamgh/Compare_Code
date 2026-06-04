using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HID.EFMODEL.DataModels;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Common;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using HIS.Desktop.Plugins.RegisterV2.Choice;
using HIS.Desktop.Plugins.RegisterV2.Run2;
using HIS.UC.AddressCombo.ADO;
using HIS.UC.KskContract.ADO;
using HIS.UC.PlusInfo.ADO;
using His.UC.UCHein;
using HIS.UC.UCHeniInfo;
using HIS.UC.UCImageInfo.ADO;
using HIS.UC.UCImageInfo.Base;
using HIS.UC.UCOtherServiceReqInfo.ADO;
using HIS.UC.UCPatientRaw.ADO;
using HIS.UC.UCRelativeInfo.ADO;
using HIS.UC.UCTransPati.ADO;
using Inventec.Common.Adapter;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Common.Mapper;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Modules;
using Inventec.UC.Login.Base;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.SDO;

namespace HIS.Desktop.Plugins.RegisterV2.Register
{
	internal abstract class ServiceRequestRegisterBehaviorBase : HIS.Desktop.Common.BusinessBase
	{
		internal UCHeinInfo ucHeinInfo1;

		protected int registerNumber = 0;

		protected UCRegister ucRequestService;

		protected string provinceCodeKS { get; set; }

		protected string provinceNameKS { get; set; }

		protected string districtCodeKS { get; set; }

		protected string districtNameKS { get; set; }

		protected string communeCodeKS { get; set; }

		protected string communeNameKS { get; set; }

		protected string addressKS { get; set; }

		protected string hohName { get; set; }

		protected string PeopleCode { get; set; }

		protected string patientName { get; set; }

		protected string patient_Last_Name { get; set; }

		protected string patient_First_Name { get; set; }

		protected long? careerId { get; set; }

		protected string careerName { get; set; }

		protected string careerCode { get; set; }

		protected long GenderId { get; set; }

		protected long dob { get; set; }

		protected long patientTypeId { get; set; }

		protected string patientCode { get; set; }

		protected short? iS_Has_Not_Day_Dob { get; set; }

		protected long? PositionId { get; set; }

		protected List<string> lstPreviousDebtTreatments { get; set; }

		protected long? receptionForm { get; set; }

		protected string CardCode { get; set; }

		protected string CardServiceCode { get; set; }

		protected string BankCardCode { get; set; }

		protected string SocialInsuranceNumberPatient { get; set; }

		protected string appointmentCode { get; set; }

		protected string codeFind { get; set; }

		protected string typeCodeFind__MaBN { get; set; }

		protected string typeCodeFind__MaHK { get; set; }

		protected string typeCodeFind__MaCT { get; set; }

		protected string typeCodeFind__SoThe { get; set; }

		protected string typeCodeFind__MaMS { get; set; }

		protected long patientId { get; set; }

		protected long? cMNDDate { get; set; }

		protected string cMNDNumber { get; set; }

		protected string passPortNumber { get; set; }

		protected string cMNDPlace { get; set; }

		protected long? cCCDDate { get; set; }

		protected string cCCDNumber { get; set; }

		protected string cCCDPlace { get; set; }

		protected string communeNowCode { get; set; }

		protected string districtNowCode { get; set; }

		protected string provinceNowCode { get; set; }

		protected string communeNowName { get; set; }

		protected string districtNowName { get; set; }

		protected string provinceNowName { get; set; }

		protected string addressNow { get; set; }

		protected string email { get; set; }

		protected string phone { get; set; }

		protected string born_provinceCode { get; set; }

		protected string born_provinceName { get; set; }

		protected long programId { get; set; }

		protected string programCode { get; set; }

		protected string nationalName { get; set; }

		protected string nationalCode { get; set; }

		protected string mpsNationalCode { get; set; }

		protected string ethnicName { get; set; }

		protected string ethnicCode { get; set; }

		protected long militaryId { get; set; }

		protected object workPlace { get; set; }

		protected long? blood_ABO_ID { get; set; }

		protected string blood_ABO_Code { get; set; }

		protected long? blood_Rh_Id { get; set; }

		protected string blood_Rh_Code { get; set; }

		protected string houseHold_Code { get; set; }

		protected string hoseHold_Relative { get; set; }

		protected long? houseHoldRelative_ID { get; set; }

		protected string maHoNgheo { get; set; }

		protected string dvqhnsCode { get; set; }

		protected string patientStoreCode { get; set; }

		protected string taxCode { get; set; }

		protected string communeCode { get; set; }

		protected string communeName { get; set; }

		protected string districtCode { get; set; }

		protected string districtName { get; set; }

		protected string provinceCode { get; set; }

		protected string provinceName { get; set; }

		protected string address { get; set; }

		protected string relativeAddress { get; set; }

		protected string relativeName { get; set; }

		protected string fatherName { get; set; }

		protected string motherName { get; set; }

		protected string relativeType { get; set; }

		protected string relativePhone { get; set; }

		protected string relativeCMNDNumber { get; set; }

		protected string religionName { get; set; }

		protected bool? IsNeedSickLeaveCert { get; set; }

		protected decimal? weight { get; set; }

		protected decimal? height { get; set; }

		protected long intructionTime { get; set; }

		protected long treatmentTypeId { get; set; }

		protected long oweTypeId { get; set; }

		public bool isPriority { get; set; }

		protected bool chkEmergency { get; set; }

		protected bool isNotPatientDayDob { get; set; }

		protected bool chkChronic { get; set; }

		protected bool chkTuberculosis { get; set; }

		protected long emergencyWTimeId { get; set; }

		protected long departmentId { get; set; }

		protected short? isNotRequireFee { get; set; }

		protected long? priority { get; set; }

		protected long? priorityTypeId { get; set; }

		protected long? priorityNumber { get; set; }

		protected long? treatmentOrder { get; set; }

		protected bool chkIsCapMaMS { get; set; }

		protected string mSCode { get; set; }

		protected long? otherPaySourceId { get; set; }

		protected string inCode { get; set; }

		protected long? patientClassifyId { get; set; }

		protected string HospitalizeReasonCode { get; set; }

		protected string HospitalizeReasonName { get; set; }

		protected string HospitalizationReason { get; set; }

		public string GUARANTEE_LOGINNAME { get; set; }

		public string GUARANTEE_USERNAME { get; set; }

		public string GUARANTEE_REASON { get; set; }

		public string NOTE { get; set; }

		protected long FUND_ID { get; set; }

		protected string FUND_NUMBER { get; set; }

		protected decimal? FUND_BUDGET { get; set; }

		protected string FUND_COMPANY_NAME { get; set; }

		protected long? FUND_FROM_TIME { get; set; }

		protected long? FUND_TO_TIME { get; set; }

		protected long? FUND_ISSUE_TIME { get; set; }

		protected string FUND_TYPE_NAME { get; set; }

		protected string FUND_CUSTOMER_NAME { get; set; }

		protected bool IsWarningForNext { get; set; }

		protected bool IsHiv { get; set; }

		protected short? isBhytHolded { get; set; }

		protected string HeinPatientCode { get; set; }

		protected string TransferInCode { get; set; }

		protected short? IsTransferIn { get; set; }

		protected short? IS_CAPD { get; set; }

		protected bool IsCAPD { get; set; }

		protected string icd_Code { get; set; }

		protected string icd_Name { get; set; }

		protected string icd_Text { get; set; }

		protected string icd_Sub_Code { get; set; }

		protected string icd_Sub_Name { get; set; }

		protected string noiChuyenDen_Code { get; set; }

		protected string noiChuyenDen_Name { get; set; }

		protected string soChuyenVien { get; set; }

		protected string right_Router_Type { get; set; }

		protected long? hinhThucChuyen_ID { get; set; }

		protected long? lyDoChuyen_ID { get; set; }

		protected long? transfer_In_CMKT { get; set; }

		protected bool isHasDialogText { get; set; }

		protected bool isDisablelblEditICD { get; set; }

		protected long chuyenTuyen_ID { get; set; }

		protected string chuyenTuyen_Name { get; set; }

		protected string chuyenTuyen_MoTa { get; set; }

		protected long? transferInTimeFrom { get; set; }

		protected long? transferInTimeTo { get; set; }

		protected short? transferInReviews { get; set; }

		protected byte[] ImgTransferInData { get; set; }

		protected byte[] img_avatar { get; set; }

		protected byte[] img_BHYT { get; set; }

		protected byte[] FileImageCMNDTruoc { get; set; }

		protected byte[] FileImageCMNDSau { get; set; }

		protected long? kskContractId { get; set; }

		protected string hrmEmployeeCode { get; set; }

		protected string hrmKskCode { get; set; }

		protected HisCardSDO cardSearch { get; set; }

		protected UCPatientExtendADO patientInformationADO { get; set; }

		protected HisPatientSDO patientData { get; set; }

		protected HisPatientProfileSDO patientProfile { get; set; }

		public bool isCheckSS { get; set; }

		protected Module currentModule { get; set; }

		protected HisPatientProfileSDO heinInfoValue { get; set; }

		protected UCPatientRawADO patientRawInfoValue { get; set; }

		protected UCAddressADO addressInfoValue { get; set; }

		protected UCServiceReqInfoADO serviceReqInfoValue { get; set; }

		protected UCPlusInfoADO patientPlusInformationInfoValue { get; set; }

		protected UCRelativeADO relativeInfoValue { get; set; }

		protected UCTransPatiADO UCTransPatiADO { get; set; }

		protected UCImageInfoADO imageADO { get; set; }

		protected List<ServiceReqDetailSDO> serviceRoomInfoValue { get; set; }

		protected MainHisHeinBhyt uCMainHein { get; set; }

		protected bool chkAutoCreateBill { get; set; }

		protected bool chkAutoDeposit { get; set; }

		protected bool chkAutoPay { get; set; }

		protected bool chkExamOnline { get; set; }

		protected long? cashierRoom_RoomId { get; set; }

		protected bool isCheckBaoLanh { get; set; }

		protected string Guarantee_Code { get; set; }

		protected string Guarantee_Request_Code { get; set; }

		protected string NguonKhachCode { get; set; }

		protected string NguonKhachName { get; set; }

		protected string NguonKhachCTName { get; set; }

		protected string NguonKhachCT { get; set; }

		protected short? ChamSocDa { get; set; }

		protected bool CheckMaMS()
		{
			bool flag = false;
			try
			{
				if (chkIsCapMaMS)
				{
					if (string.IsNullOrEmpty(cMNDNumber))
					{
						flag = true;
						base.param.Messages.Add(ResourceMessage.ChuaNhapCMNDNumberKhiDaCheckCapMaMS);
					}
					if (img_avatar == null || img_avatar.Length == 0 || img_BHYT == null || img_BHYT.Length == 0)
					{
						flag = true;
						base.param.Messages.Add(ResourceMessage.ChuaNhapAnhChupCMNDMatTruocMatSauKhiDaCheckCapMaMS);
					}
					if (!flag)
					{
						HisCardSDO data = new HisCardSDO();
						HisCardSDO hisCardSDO = new BackendAdapter(base.param).Post<HisCardSDO>("api/HisCard/CreateByMSCode", ApiConsumers.MosConsumer, data, new Action(SessionManager.ActionLostToken), base.param);
						if (hisCardSDO != null)
						{
							flag = false;
							mSCode = hisCardSDO.CardCode;
							ucRequestService.ucOtherServiceReqInfo1.SetMaMS(mSCode);
						}
						else if (XtraMessageBox.Show(ResourceMessage.CapMaMSThatBaiBanCoMuonTiepTuc, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						{
							flag = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return flag;
		}

		private bool CheckProfile()
		{
			bool flag = true;
			try
			{
				flag = flag && patientProfile.HisPatientTypeAlter != null;
				flag = flag && IsChild();
				flag = flag && patientProfile.HisPatientTypeAlter.HAS_BIRTH_CERTIFICATE == "C";
				flag = flag && ((string.IsNullOrEmpty(patientProfile.DistrictCode) && string.IsNullOrEmpty(patientProfile.HisPatient.COMMUNE_CODE)) || string.IsNullOrEmpty(patientProfile.ProvinceCode));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return flag;
		}

		private bool Check()
		{
			try
			{
				bool flag = true && CheckIsChildWithoutAddress() && CheckLiveAreaCode();
				CallSyncHID();
				if (!flag)
				{
					WaitingManager.Hide();
				}
				return flag;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return false;
		}

		private void CallSyncHID()
		{
			try
			{
				if (!HisConfigCFG.IsSyncHID || (patientData != null && !string.IsNullOrEmpty(patientData.PERSON_CODE)))
				{
					return;
				}
				CommonParam paramHID = new CommonParam();
				HID_PERSON filter = new HID_PERSON();
				if (ucRequestService != null && ucRequestService.cardSearch != null && !string.IsNullOrEmpty(ucRequestService.cardSearch.CardCode))
				{
					filter.CARD_CODE = ucRequestService.cardSearch.CardCode;
				}
				filter.BRANCH_CODE = BranchDataWorker.Branch.HEIN_MEDI_ORG_CODE;
				filter.BRANCH_NAME = BranchDataWorker.Branch.BRANCH_NAME;
				filter.BHYT_NUMBER = ((patientProfile.HisPatientTypeAlter != null) ? patientProfile.HisPatientTypeAlter.HEIN_CARD_NUMBER : "");
				filter.ADDRESS = patientProfile.HisPatient.ADDRESS;
				filter.COMMUNE_NAME = patientProfile.HisPatient.COMMUNE_NAME;
				filter.DISTRICT_NAME = patientProfile.HisPatient.DISTRICT_NAME;
				filter.PROVINCE_NAME = patientProfile.HisPatient.PROVINCE_NAME;
				filter.CAREER_NAME = ((patientProfile.HisPatient.CAREER_ID > 0) ? (BackendDataWorker.Get<HIS_CAREER>().FirstOrDefault((HIS_CAREER o) => o.ID == patientProfile.HisPatient.CAREER_ID) ?? new HIS_CAREER()).CAREER_NAME : "");
				filter.DOB = patientProfile.HisPatient.DOB;
				filter.GENDER_ID = patientProfile.HisPatient.GENDER_ID;
				filter.FIRST_NAME = patientProfile.HisPatient.FIRST_NAME;
				filter.LAST_NAME = patientProfile.HisPatient.LAST_NAME;
				if (IsChild())
				{
					filter.VIR_PERSON_NAME = patientProfile.HisPatient.RELATIVE_NAME;
				}
				else
				{
					filter.VIR_PERSON_NAME = patientProfile.HisPatient.LAST_NAME + " " + patientProfile.HisPatient.FIRST_NAME;
				}
				filter.IS_HAS_NOT_DAY_DOB = patientProfile.HisPatient.IS_HAS_NOT_DAY_DOB;
				filter.ETHNIC_NAME = patientProfile.HisPatient.ETHNIC_NAME;
				filter.EMAIL = patientProfile.HisPatient.EMAIL;
				filter.NATIONAL_NAME = patientProfile.HisPatient.NATIONAL_NAME;
				filter.MOBILE = patientProfile.HisPatient.PHONE;
				filter.HOH_NAME = hohName;
				filter.HOUSEHOLD_CODE = houseHold_Code;
				filter.HOUSEHOLD_RELATION_NAME = hoseHold_Relative;
				if (!string.IsNullOrEmpty(cMNDNumber))
				{
					if (cMNDNumber.Length > 9)
					{
						filter.CCCD_DATE = cMNDDate;
						filter.CCCD_NUMBER = cMNDNumber;
						filter.CCCD_PLACE = cMNDPlace;
					}
					else
					{
						filter.CMND_DATE = cMNDDate;
						filter.CMND_NUMBER = cMNDNumber;
						filter.CMND_PLACE = cMNDPlace;
					}
				}
				filter.HT_ADDRESS = addressNow;
				filter.HT_COMMUNE_NAME = communeNowName;
				filter.HT_DISTRICT_NAME = districtNowName;
				filter.HT_PROVINCE_NAME = provinceNowName;
				filter.MOTHER_NAME = motherName;
				filter.FATHER_NAME = fatherName;
				filter.RELATIVE_PHONE = patientProfile.HisPatient.RELATIVE_PHONE;
				filter.RELATIVE_ADDRESS = relativeAddress;
				filter.RELATIVE_NAME = relativeName;
				filter.RELATIVE_TYPE = relativeType;
				filter.RELATIVE_CMND_NUMBER = relativeCMNDNumber;
				filter.BORN_PROVINCE_CODE = born_provinceCode;
				filter.BORN_PROVINCE_NAME = born_provinceName;
				filter.BORN_ADDRESS = addressKS;
				filter.BORN_COMMUNE_NAME = communeNameKS;
				filter.BORN_DISTRICT_NAME = districtNameKS;
				filter.BORN_PROVINCE_NAME = provinceNameKS;
				filter.BLOOD_ABO_CODE = blood_ABO_Code;
				filter.BLOOD_RH_CODE = blood_Rh_Code;
				List<HID_PERSON> persons = ApiConsumers.HidWrapConsumer.Post<List<HID_PERSON>>(true, "api/HidPerson/Take", paramHID, filter, new object[0]);
				if (persons != null && persons.Count > 0)
				{
					if (persons.Count == 1)
					{
						SelectPerson(persons[0]);
					}
					else
					{
						frmPersonSelect frmPersonSelect2 = new frmPersonSelect(persons, new SelectPerson(SelectPerson));
						frmPersonSelect2.ShowDialog();
					}
					if (string.IsNullOrEmpty(patientProfile.HisPatient.PERSON_CODE))
					{
						HID_PERSON hID_PERSON = ApiConsumers.HidWrapConsumer.Post<HID_PERSON>(true, "api/HidPerson/Create", paramHID, filter, new object[0]);
						if (hID_PERSON != null)
						{
							SelectPerson(hID_PERSON);
						}
					}
					return;
				}
				if (paramHID.Messages != null && paramHID.Messages.Count > 0)
				{
					base.param.Messages.AddRange(paramHID.Messages);
				}
				if (paramHID.BugCodes != null && paramHID.BugCodes.Count > 0)
				{
					base.param.BugCodes.AddRange(paramHID.BugCodes);
				}
				LogSystem.Debug("Goi len he thong HID lay thong tin ho so suc khoe ca nhan that bai. ____Input data: " + LogUtil.TraceData(LogUtil.GetMemberName(() => filter), filter) + "____Result data:" + LogUtil.TraceData(LogUtil.GetMemberName(() => paramHID), paramHID) + LogUtil.TraceData(LogUtil.GetMemberName(() => persons), persons));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private string GenerateProvinceCode(string provinceCode)
		{
			try
			{
				return string.Format("{0:000}", System.Convert.ToInt64(provinceCode));
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return provinceCode;
		}

		private void SelectPerson(HID_PERSON data)
		{
			try
			{
				patientProfile.HisPatient.PERSON_CODE = data.PERSON_CODE;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private bool CheckIsChildWithoutAddress()
		{
			try
			{
				bool flag = true && patientProfile.HisPatientTypeAlter != null && IsChild() && patientProfile.HisPatientTypeAlter.HAS_BIRTH_CERTIFICATE == "C" && ((string.IsNullOrEmpty(patientProfile.DistrictCode) && string.IsNullOrEmpty(patientProfile.HisPatient.COMMUNE_CODE)) || string.IsNullOrEmpty(patientProfile.ProvinceCode));
				if (flag)
				{
					base.param.Messages.Add(ResourceMessage.TreEmCoGiayKhaiSinhPhaiNhapThongTinHanhChinh);
					ucRequestService.ucAddressCombo1.FocusToProvince();
				}
				return !flag;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return true;
		}

		private bool CheckLiveAreaCode()
		{
			try
			{
				if (true && patientProfile != null && patientProfile.HisPatientTypeAlter != null && !string.IsNullOrEmpty(patientProfile.HisPatientTypeAlter.LIVE_AREA_CODE) && XtraMessageBox.Show(ResourceMessage.BanCoMuonNhapThongTinKhuVuc, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					ucRequestService.ucHeinInfo1.FocusUserByLiveAreaCode();
					ucRequestService.isShowMess = false;
					throw new NullReferenceException("LiveAreaCode");
				}
				return true;
			}
			catch (NullReferenceException ex)
			{
				LogSystem.Warn("Truong hop nguoi dung nhap o khu vuc, nguoi dung chon khong muon nhap khu vuc da chon => focus vao o khu vuc cho nguoi dung nhap gia tri khac\n" + ((ex != null) ? ex.ToString() : null));
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
			}
			return false;
		}

		private bool IsChild()
		{
			bool flag = false;
			try
			{
				DateTime dateOfBirth = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(patientProfile.HisPatient.DOB) ?? DateTime.Now;
				flag = BhytPatientTypeData.IsChild(dateOfBirth);
			}
			catch (Exception ex)
			{
				flag = false;
				LogSystem.Error(ex);
			}
			return flag;
		}

		internal ServiceRequestRegisterBehaviorBase(CommonParam param, UCRegister ucServiceRequestRegiter)
			: base(param)
		{
			try
			{
				ucHeinInfo1 = ucServiceRequestRegiter.ucHeinInfo1;
				ucRequestService = ucServiceRequestRegiter;
				heinInfoValue = ucServiceRequestRegiter.ucHeinInfo1.GetValue();
				patientRawInfoValue = ucServiceRequestRegiter.ucPatientRaw1.GetValue();
				addressInfoValue = ucServiceRequestRegiter.ucAddressCombo1.GetValue();
				serviceReqInfoValue = ucServiceRequestRegiter.ucOtherServiceReqInfo1.GetValue();
				patientPlusInformationInfoValue = ucServiceRequestRegiter.ucPlusInfo1.GetValue();
				relativeInfoValue = ucServiceRequestRegiter.ucRelativeInfo1.GetValue();
				serviceRoomInfoValue = ucServiceRequestRegiter.ucServiceRoomInfo1.GetDetail();
				imageADO = ucServiceRequestRegiter.ucImageInfo1.GetValue();
				UCTransPatiADO = ucServiceRequestRegiter.transPatiADO;
				currentModule = ucServiceRequestRegiter.currentModule;
				uCMainHein = ucServiceRequestRegiter.mainHeinProcessor;
				PeopleCode = patientRawInfoValue.PERSON_CODE;
				patientName = patientRawInfoValue.PATIENT_NAME;
				patient_Last_Name = patientRawInfoValue.PATIENT_LAST_NAME;
				patient_First_Name = patientRawInfoValue.PATIENT_FIRST_NAME;
				if (patientRawInfoValue.GENDER_ID > 0)
				{
					GenderId = patientRawInfoValue.GENDER_ID;
				}
				dob = patientRawInfoValue.DOB;
				patientTypeId = patientRawInfoValue.PATIENTTYPE_ID;
				if (patientRawInfoValue.CARRER_ID.HasValue && patientRawInfoValue.CARRER_ID > 0)
				{
					careerId = patientRawInfoValue.CARRER_ID;
				}
				careerCode = patientRawInfoValue.CARRER_CODE;
				careerName = patientRawInfoValue.CARRER_NAME;
				iS_Has_Not_Day_Dob = patientRawInfoValue.IS_HAS_NOT_DAY_DOB;
				isNotPatientDayDob = patientRawInfoValue.IS_HAS_NOT_DAY_DOB == 1;
				hrmEmployeeCode = patientRawInfoValue.EMPLOYEE_CODE;
				if (patientRawInfoValue.MILITARY_RANK_ID.HasValue)
				{
					militaryId = patientRawInfoValue.MILITARY_RANK_ID.Value;
				}
				if (patientRawInfoValue.PATIENT_CLASSIFY_ID.HasValue)
				{
					patientClassifyId = patientRawInfoValue.PATIENT_CLASSIFY_ID.Value;
				}
				if (patientRawInfoValue.POSITION_ID.HasValue)
				{
					PositionId = patientRawInfoValue.POSITION_ID.Value;
				}
				if (patientRawInfoValue.WORK_PLACE_ID.HasValue)
				{
					workPlace = patientRawInfoValue.WORK_PLACE_ID.Value;
				}
				lstPreviousDebtTreatments = patientRawInfoValue.lstPreviousDebtTreatments;
				if (patientRawInfoValue.ReceptionForm.HasValue)
				{
					receptionForm = patientRawInfoValue.ReceptionForm.Value;
				}
				CardCode = patientRawInfoValue.CardCode;
				CardServiceCode = patientRawInfoValue.CardServiceCode;
				BankCardCode = patientRawInfoValue.BankCardCode;
				SocialInsuranceNumberPatient = patientRawInfoValue.SocialInsuranceNumberPatient;
				address = addressInfoValue.Address;
				communeName = addressInfoValue.Commune_Name;
				communeCode = addressInfoValue.Commune_Code;
				districtName = addressInfoValue.District_Name;
				districtCode = addressInfoValue.District_Code;
				provinceName = addressInfoValue.Province_Name;
				provinceCode = addressInfoValue.Province_Code;
				phone = addressInfoValue.Phone;
				IS_CAPD = serviceReqInfoValue.IS_CAPD;
				IsCAPD = serviceReqInfoValue.IsCAPD;
				chkExamOnline = serviceReqInfoValue.IsExamOnline;
				chkEmergency = serviceReqInfoValue.IsEmergency;
				intructionTime = serviceReqInfoValue.IntructionTime;
				ChamSocDa = (serviceReqInfoValue.isChamSocDa ? new short?(1) : ((short?)null));
				if (serviceReqInfoValue.TreatmentType_ID > 0)
				{
					treatmentTypeId = serviceReqInfoValue.TreatmentType_ID;
				}
				isPriority = serviceReqInfoValue.IsPriority;
				if (serviceReqInfoValue.PriorityType.HasValue && serviceReqInfoValue.PriorityType > 0)
				{
					priorityTypeId = serviceReqInfoValue.PriorityType;
				}
				chkChronic = serviceReqInfoValue.IsChronic;
				chkTuberculosis = serviceReqInfoValue.IsTuberCulosis;
				if (serviceReqInfoValue.OweType_ID > 0)
				{
					oweTypeId = serviceReqInfoValue.OweType_ID;
				}
				if (serviceReqInfoValue.IsEmergency && serviceReqInfoValue.EmergencyTime_ID > 0)
				{
					emergencyWTimeId = serviceReqInfoValue.EmergencyTime_ID;
				}
				if (serviceReqInfoValue.OTHER_PAY_SOURCE_ID > 0)
				{
					otherPaySourceId = serviceReqInfoValue.OTHER_PAY_SOURCE_ID;
				}
				inCode = serviceReqInfoValue.IN_CODE;
				if (serviceReqInfoValue.PATIENT_CLASSIFY_ID.HasValue && !patientClassifyId.HasValue)
				{
					patientClassifyId = serviceReqInfoValue.PATIENT_CLASSIFY_ID;
				}
				GUARANTEE_LOGINNAME = serviceReqInfoValue.GUARANTEE_LOGINNAME;
				GUARANTEE_USERNAME = serviceReqInfoValue.GUARANTEE_USERNAME;
				GUARANTEE_REASON = serviceReqInfoValue.GUARANTEE_REASON;
				NguonKhachCode = serviceReqInfoValue.NguonKhachCode;
				NguonKhachName = serviceReqInfoValue.NguonKhachName;
				NguonKhachCTName = serviceReqInfoValue.NguonKhachCTName;
				NguonKhachCT = serviceReqInfoValue.NguonKhachCT;
				NOTE = serviceReqInfoValue.NOTE;
				IsWarningForNext = serviceReqInfoValue.IsWarningForNext;
				IsHiv = serviceReqInfoValue.IsHiv;
				treatmentOrder = serviceReqInfoValue.TreatmentOrder;
				chkIsCapMaMS = serviceReqInfoValue.IsCapMaMS;
				mSCode = serviceReqInfoValue.MaMS;
				FUND_ID = serviceReqInfoValue.FUND_ID;
				FUND_BUDGET = serviceReqInfoValue.FUND_BUDGET;
				FUND_COMPANY_NAME = serviceReqInfoValue.FUND_COMPANY_NAME;
				FUND_FROM_TIME = serviceReqInfoValue.FUND_FROM_TIME;
				FUND_ISSUE_TIME = serviceReqInfoValue.FUND_ISSUE_TIME;
				FUND_NUMBER = serviceReqInfoValue.FUND_NUMBER;
				FUND_TO_TIME = serviceReqInfoValue.FUND_TO_TIME;
				FUND_TYPE_NAME = serviceReqInfoValue.FUND_TYPE_NAME;
				FUND_CUSTOMER_NAME = serviceReqInfoValue.FUND_CUSTOMER_NAME;
				HospitalizeReasonCode = serviceReqInfoValue.HospitalizeReasonCode;
				HospitalizeReasonName = serviceReqInfoValue.HospitalizeReasonName;
				HospitalizationReason = serviceReqInfoValue.HospitalizationReason;
				if (heinInfoValue != null && heinInfoValue.HisTreatment != null)
				{
					isBhytHolded = heinInfoValue.HisTreatment.IS_BHYT_HOLDED;
					HeinPatientCode = heinInfoValue.HisTreatment.HEIN_PATIENT_TYPE_CODE;
					TransferInCode = heinInfoValue.HisTreatment.TRANSFER_IN_CODE;
					IsTransferIn = heinInfoValue.HisTreatment.IS_TRANSFER_IN;
				}
				isCheckSS = ucServiceRequestRegiter.isCheckSS;
				born_provinceCode = patientPlusInformationInfoValue.PROVINCE_OfBIRTH_CODE;
				born_provinceName = patientPlusInformationInfoValue.PROVINCE_OfBIRTH_NAME;
				districtCodeKS = patientPlusInformationInfoValue.DISTRICT_OfBIRTH_CODE;
				districtNameKS = patientPlusInformationInfoValue.DISTRICT_OfBIRTH_NAME;
				communeCodeKS = patientPlusInformationInfoValue.COMMUNE_OfBIRTH_CODE;
				communeNameKS = patientPlusInformationInfoValue.COMMUNE_OfBIRTH_NAME;
				addressKS = patientPlusInformationInfoValue.ADDRESS_OfBIRTH;
				communeNowCode = (ucRequestService.IsReadCardTheViet ? ucRequestService.HtCommuneCode : patientPlusInformationInfoValue.HT_COMMUNE_CODE);
				communeNowName = (ucRequestService.IsReadCardTheViet ? ucRequestService.HtCommuneName : patientPlusInformationInfoValue.HT_COMMUNE_NAME);
				provinceNowCode = (ucRequestService.IsReadCardTheViet ? ucRequestService.HtProvinceCode : patientPlusInformationInfoValue.HT_PROVINCE_CODE);
				provinceNowName = (ucRequestService.IsReadCardTheViet ? ucRequestService.HtProvinceName : patientPlusInformationInfoValue.HT_PROVINCE_NAME);
				districtNowCode = (ucRequestService.IsReadCardTheViet ? ucRequestService.HtDistrictCode : patientPlusInformationInfoValue.HT_DISTRICT_CODE);
				districtNowName = (ucRequestService.IsReadCardTheViet ? ucRequestService.HtDistrictName : patientPlusInformationInfoValue.HT_DISTRICT_NAME);
				addressNow = patientPlusInformationInfoValue.HT_ADDRESS;
				if (AppConfigs.ChangeEthnic != 0)
				{
					ethnicName = patientRawInfoValue.ETHNIC_NAME;
					ethnicCode = patientRawInfoValue.ETHNIC_CODE;
				}
				else
				{
					ethnicName = patientPlusInformationInfoValue.ETHNIC_NAME;
					ethnicCode = patientPlusInformationInfoValue.ETHNIC_CODE;
				}
				if (patientPlusInformationInfoValue.MILITARYRANK_ID.HasValue && militaryId == 0)
				{
					militaryId = patientPlusInformationInfoValue.MILITARYRANK_ID.Value;
				}
				nationalName = patientPlusInformationInfoValue.NATIONAL_NAME;
				nationalCode = patientPlusInformationInfoValue.NATIONAL_CODE;
				mpsNationalCode = patientPlusInformationInfoValue.MPS_NATIONAL_CODE;
				programId = patientPlusInformationInfoValue.PROGRAM_ID;
				programCode = patientPlusInformationInfoValue.PROGRAM_CODE;
				if (string.IsNullOrWhiteSpace(phone))
				{
					phone = patientPlusInformationInfoValue.PHONE_NUMBER;
				}
				if (workPlace == null)
				{
					workPlace = patientPlusInformationInfoValue.workPlace;
				}
				email = patientPlusInformationInfoValue.EMAIL;
				blood_ABO_Code = patientPlusInformationInfoValue.BLOOD_ABO_CODE;
				blood_ABO_ID = patientPlusInformationInfoValue.BLOOD_ABO_ID;
				blood_Rh_Code = patientPlusInformationInfoValue.BLOOD_RH_CODE;
				blood_Rh_Id = patientPlusInformationInfoValue.BLOOD_RH_ID;
				if (!string.IsNullOrEmpty(patientPlusInformationInfoValue.CMND_NUMBER))
				{
					cMNDNumber = patientPlusInformationInfoValue.CMND_NUMBER;
				}
				else if (!string.IsNullOrEmpty(patientPlusInformationInfoValue.CCCD_NUMBER))
				{
					cCCDNumber = patientPlusInformationInfoValue.CCCD_NUMBER;
				}
				else if (!string.IsNullOrEmpty(patientPlusInformationInfoValue.PASSPORT_NUMBER))
				{
					passPortNumber = patientPlusInformationInfoValue.PASSPORT_NUMBER;
				}
				cMNDDate = patientPlusInformationInfoValue.CMND_DATE;
				cMNDPlace = patientPlusInformationInfoValue.CMND_PLACE;
				houseHold_Code = patientPlusInformationInfoValue.HOUSEHOLD_CODE;
				houseHoldRelative_ID = patientPlusInformationInfoValue.HOUSEHOLD_RELATION_ID;
				hoseHold_Relative = patientPlusInformationInfoValue.HOUSEHOLD_RELATION_NAME;
				maHoNgheo = ((patientPlusInformationInfoValue.HONGHEO_CODE == "") ? "" : patientPlusInformationInfoValue.HONGHEO_CODE);
				dvqhnsCode = ((patientPlusInformationInfoValue.BUD_REL_UNIT_CODE == "") ? "" : patientPlusInformationInfoValue.BUD_REL_UNIT_CODE);
				patientStoreCode = patientPlusInformationInfoValue.PATIENT_STORE_CODE;
				hrmKskCode = patientPlusInformationInfoValue.HRM_KSK_CODE;
				taxCode = patientPlusInformationInfoValue.TAX_CODE;
				relativeAddress = relativeInfoValue.RelativeAddress;
				relativeType = relativeInfoValue.Correlated;
				relativePhone = relativeInfoValue.RelativePhone;
				fatherName = relativeInfoValue.FatherName;
				motherName = relativeInfoValue.MotherName;
				relativeName = relativeInfoValue.RelativeName;
				relativeCMNDNumber = relativeInfoValue.RelativeCMND;
				IsNeedSickLeaveCert = relativeInfoValue.IsNeedSickLeaveCert;
				religionName = "";
				departmentId = GlobalStore.DepartmentId;
				if (imageADO != null && imageADO.ListImageData != null && imageADO.ListImageData.Count > 0)
				{
					foreach (ImageInfoADO listImageDatum in imageADO.ListImageData)
					{
						switch (listImageDatum.Type)
						{
						case ImageType.CHAN_DUNG:
							img_avatar = listImageDatum.FileImage;
							break;
						case ImageType.CMND_CCCD_SAU:
							FileImageCMNDSau = listImageDatum.FileImage;
							break;
						case ImageType.CMND_CCCD_TRUOC:
							FileImageCMNDTruoc = listImageDatum.FileImage;
							break;
						case ImageType.THE_BHYT:
							img_BHYT = listImageDatum.FileImage;
							break;
						}
					}
				}
				if (UCTransPatiADO != null)
				{
					icd_Code = UCTransPatiADO.ICD_CODE;
					icd_Name = UCTransPatiADO.ICD_NAME;
					icd_Text = UCTransPatiADO.ICD_TEXT;
					icd_Sub_Code = UCTransPatiADO.ICD_SUB_CODE;
					icd_Sub_Name = UCTransPatiADO.ICD_SUB_NAME;
					noiChuyenDen_Code = UCTransPatiADO.NOICHUYENDEN_CODE;
					noiChuyenDen_Name = UCTransPatiADO.NOICHUYENDEN_NAME;
					soChuyenVien = UCTransPatiADO.SOCHUYENVIEN;
					right_Router_Type = UCTransPatiADO.RIGHT_ROUTER_TYPE;
					hinhThucChuyen_ID = UCTransPatiADO.HINHTHUCHUYEN_ID;
					lyDoChuyen_ID = UCTransPatiADO.LYDOCHUYEN_ID;
					transfer_In_CMKT = UCTransPatiADO.TRANSFER_IN_CMKT;
					isHasDialogText = UCTransPatiADO.IsHasDialogText;
					isDisablelblEditICD = UCTransPatiADO.IsDisablelblEditICD;
					transferInTimeFrom = UCTransPatiADO.TRANSFER_IN_TIME_FROM;
					transferInTimeTo = UCTransPatiADO.TRANSFER_IN_TIME_TO;
					transferInReviews = UCTransPatiADO.TRANSFER_IN_REVIEWS;
					ImgTransferInData = UCTransPatiADO.ImgTransferInData;
					IsTransferIn = (short)1;
				}
				patientId = ((ucRequestService.currentPatientSDO != null) ? ucRequestService.currentPatientSDO.ID : 0);
				cardSearch = ucRequestService.cardSearch;
				if (cardSearch != null && string.IsNullOrEmpty(PeopleCode))
				{
					PeopleCode = cardSearch.PersonCode;
				}
				patientData = ucRequestService.currentPatientSDO;
				appointmentCode = ((ucRequestService.currentPatientSDO != null) ? ucRequestService.currentPatientSDO.AppointmentCode : "");
				if (patientId <= 0)
				{
					patientId = patientRawInfoValue.PATIENT_ID;
					patientCode = patientRawInfoValue.PATIENT_CODE;
				}
				if (ucRequestService.ucKskContract != null && ucRequestService.kskContractProcessor != null)
				{
					KskContractOutput kskContractOutput = (KskContractOutput)ucRequestService.kskContractProcessor.GetValue(ucRequestService.ucKskContract);
					if (kskContractOutput != null && kskContractOutput.IsVali && kskContractOutput.KskContract != null)
					{
						kskContractId = kskContractOutput.KskContract.ID;
					}
				}
				chkAutoCreateBill = ucServiceRequestRegiter.chkAutoCreateBill.Checked;
				chkAutoDeposit = ucServiceRequestRegiter.chkAutoDeposit.Checked;
				chkAutoPay = ucServiceRequestRegiter.chkAutoPay.Checked;
				isCheckBaoLanh = ucServiceRequestRegiter.chkBaoLanh.Checked;
				Guarantee_Code = ucServiceRequestRegiter.GuarateeCode;
				Guarantee_Request_Code = ucServiceRequestRegiter.GuaranteeRequestCode;
				if (ucServiceRequestRegiter.cboCashierRoom.EditValue != null)
				{
					V_HIS_CASHIER_ROOM v_HIS_CASHIER_ROOM = BackendDataWorker.Get<V_HIS_CASHIER_ROOM>().FirstOrDefault((V_HIS_CASHIER_ROOM o) => o.ID == System.Convert.ToInt64(ucServiceRequestRegiter.cboCashierRoom.EditValue));
					if (v_HIS_CASHIER_ROOM != null)
					{
						cashierRoom_RoomId = v_HIS_CASHIER_ROOM.ROOM_ID;
					}
				}
				else if (GlobalVariables.SessionInfo != null)
				{
					cashierRoom_RoomId = GlobalVariables.SessionInfo.CashierWorkingRoomId;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		protected void InitBase()
		{
			try
			{
				if (patientProfile == null)
				{
					patientProfile = new HisPatientProfileSDO();
				}
				if (patientProfile.HisPatient == null)
				{
					patientProfile.HisPatient = new HIS_PATIENT();
				}
				if (patientProfile.HisTreatment == null)
				{
					patientProfile.HisTreatment = new HIS_TREATMENT();
				}
				if (patientProfile.HisPatientTypeAlter == null)
				{
					patientProfile.HisPatientTypeAlter = new HIS_PATIENT_TYPE_ALTER();
				}
				if (patientData != null)
				{
					DataObjectMapper.Map<HIS_PATIENT>(patientProfile.HisPatient, patientData);
				}
				if (currentModule != null)
				{
					patientProfile.RequestRoomId = currentModule.RoomId;
				}
				ProcessPatientData();
				ProcessPatientTypeAlterData();
				ProcessTreatmentData();
				ProcessHeinPatientTypeCode();
				if (chkEmergency)
				{
					ProcessEmergency();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		protected object RunBase(object data, UCRegister ucRequestService)
		{
			try
			{
				if (data == null)
				{
					throw new ArgumentNullException("Input data is null");
				}
				if (this.ucRequestService == null)
				{
					throw new ArgumentNullException("ucRequestService is null");
				}
				if (CheckProfile())
				{
					this.ucRequestService.ucAddressCombo1.FocusToProvince();
					WaitingManager.Hide();
					base.param.Messages.Add(ResourceMessage.TreEmCoGiayKhaiSinhPhaiNhapThongTinHanhChinh);
				}
				else
				{
					CallSyncHID();
					this.ucRequestService.ucServiceRoomInfo1.GetDetail();
					LogSystem.Debug("RunBase => begin call api____Input data:");
					LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => data), data));
					if (data.GetType() == typeof(HisServiceReqExamRegisterSDO))
					{
						LogSystem.Debug("Tiep don goi api: " + LogUtil.TraceData(LogUtil.GetMemberName(() => "api/HisServiceReq/ExamRegister"), "api/HisServiceReq/ExamRegister"));
						return new BackendAdapter(base.param).Post<HisServiceReqExamRegisterResultSDO>("api/HisServiceReq/ExamRegister", ApiConsumers.MosConsumer, data, new Action(SessionManager.ActionLostToken), base.param);
					}
					if (data.GetType() == typeof(HisPatientProfileSDO))
					{
						return new BackendAdapter(base.param).Post<HisPatientProfileSDO>("api/HisPatient/RegisterProfile", ApiConsumers.MosConsumer, data, new Action(SessionManager.ActionLostToken), base.param);
					}
					LogSystem.Debug("RunBase => end call api");
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
			return null;
		}

		private void ProcessPatientData()
		{
			try
			{
				if (patientProfile == null)
				{
					patientProfile = new HisPatientProfileSDO();
				}
				if (patientProfile.HisPatient == null)
				{
					patientProfile.HisPatient = new HIS_PATIENT();
				}
				if (patientId != 0)
				{
					patientProfile.HisPatient.ID = patientId;
				}
				patientProfile.HisPatient.IS_HAS_NOT_DAY_DOB = (short)(isNotPatientDayDob ? 1 : 0);
				patientProfile.HisPatient.EMAIL = email;
				patientProfile.HisPatient.PATIENT_STORE_CODE = patientStoreCode;
				patientProfile.HisPatient.HOUSEHOLD_CODE = houseHold_Code;
				patientProfile.HisPatient.HOUSEHOLD_RELATION_NAME = hoseHold_Relative;
				patientProfile.HisPatient.IS_HIV = (IsHiv ? new short?(1) : ((short?)null));
				patientProfile.HisPatient.BRANCH_ID = WorkPlace.GetBranchId();
				if (!string.IsNullOrEmpty(cMNDNumber))
				{
					patientProfile.HisPatient.CMND_DATE = cMNDDate;
					patientProfile.HisPatient.CMND_NUMBER = cMNDNumber;
					patientProfile.HisPatient.CMND_PLACE = cMNDPlace;
				}
				else if (!string.IsNullOrEmpty(cCCDNumber))
				{
					patientProfile.HisPatient.CCCD_DATE = cMNDDate;
					patientProfile.HisPatient.CCCD_NUMBER = cCCDNumber;
					patientProfile.HisPatient.CCCD_PLACE = cMNDPlace;
				}
				else if (!string.IsNullOrEmpty(passPortNumber))
				{
					patientProfile.HisPatient.PASSPORT_DATE = cMNDDate;
					patientProfile.HisPatient.PASSPORT_NUMBER = passPortNumber;
					patientProfile.HisPatient.PASSPORT_PLACE = cMNDPlace;
				}
				patientProfile.HisPatient.IS_CAPD = IS_CAPD;
				patientProfile.IsCAPD = IsCAPD;
				patientProfile.HisPatient.COMMUNE_CODE = communeCode;
				patientProfile.HisPatient.HT_ADDRESS = addressNow;
				patientProfile.HisPatient.HT_COMMUNE_NAME = communeNowName;
				patientProfile.HisPatient.HT_DISTRICT_NAME = districtNowName;
				patientProfile.HisPatient.HT_PROVINCE_NAME = provinceNowName;
				patientProfile.HisPatient.HT_COMMUNE_CODE = communeNowCode;
				patientProfile.HisPatient.HT_DISTRICT_CODE = districtNowCode;
				patientProfile.HisPatient.HT_PROVINCE_CODE = provinceNowCode;
				patientProfile.HisPatient.RELATIVE_MOBILE = phone;
				patientProfile.HisPatient.BLOOD_ABO_CODE = blood_ABO_Code;
				patientProfile.HisPatient.BLOOD_RH_CODE = blood_Rh_Code;
				patientProfile.HisPatient.RELATIVE_ADDRESS = relativeAddress;
				patientProfile.HisPatient.RELATIVE_NAME = relativeName;
				patientProfile.HisPatient.FATHER_NAME = fatherName;
				patientProfile.HisPatient.MOTHER_NAME = motherName;
				patientProfile.HisPatient.RELATIVE_TYPE = relativeType;
				patientProfile.HisPatient.RELATIVE_PHONE = relativePhone;
				patientProfile.HisPatient.RELATIVE_CMND_NUMBER = relativeCMNDNumber;
				patientProfile.HisPatient.BORN_PROVINCE_CODE = GenerateProvinceCode(born_provinceCode);
				patientProfile.HisPatient.BORN_PROVINCE_NAME = born_provinceName;
				patientProfile.HisPatient.FIRST_NAME = patient_First_Name;
				patientProfile.HisPatient.LAST_NAME = patient_Last_Name;
				patientProfile.HisPatient.PERSON_CODE = PeopleCode;
				patientProfile.HisPatient.PROVINCE_CODE = provinceCode;
				patientProfile.HisPatient.DOB = dob;
				patientProfile.HisPatient.GENDER_ID = GenderId;
				patientProfile.HisPatient.ADDRESS = address;
				patientProfile.HisPatient.PROVINCE_NAME = provinceName;
				patientProfile.HisPatient.DISTRICT_CODE = districtCode;
				patientProfile.HisPatient.DISTRICT_NAME = districtName;
				patientProfile.HisPatient.COMMUNE_NAME = communeName;
				if (careerId.HasValue)
				{
					patientProfile.HisPatient.CAREER_ID = careerId.Value;
				}
				patientProfile.HisPatient.CAREER_NAME = careerName;
				patientProfile.HisPatient.CAREER_CODE = careerCode;
				patientProfile.HisPatient.ETHNIC_NAME = ethnicName;
				patientProfile.HisPatient.ETHNIC_CODE = ethnicCode;
				patientProfile.HisPatient.NATIONAL_NAME = nationalName;
				patientProfile.HisPatient.NATIONAL_CODE = nationalCode;
				patientProfile.HisPatient.MPS_NATIONAL_CODE = mpsNationalCode;
				if (workPlace != null && (workPlace is long || workPlace is long?) && (long?)workPlace > 0)
				{
					patientProfile.HisPatient.WORK_PLACE_ID = (long?)workPlace;
				}
				else if (workPlace != null && workPlace is string)
				{
					patientProfile.HisPatient.WORK_PLACE = (string)workPlace;
				}
				else
				{
					patientProfile.HisPatient.WORK_PLACE_ID = null;
					patientProfile.HisPatient.WORK_PLACE = null;
				}
				if (militaryId > 0)
				{
					patientProfile.HisPatient.MILITARY_RANK_ID = militaryId;
				}
				patientProfile.HisPatient.PHONE = phone;
				patientProfile.IsChronic = chkChronic;
				if (chkChronic)
				{
					patientProfile.HisPatient.IS_CHRONIC = (short)1;
				}
				else
				{
					patientProfile.HisPatient.IS_CHRONIC = null;
				}
				patientProfile.HisPatient.IS_TUBERCULOSIS = (chkTuberculosis ? new short?(1) : new short?(0));
				patientProfile.ImgAvatarData = img_avatar;
				patientProfile.ImgBhytData = img_BHYT;
				patientProfile.ImgCmndBeforeData = FileImageCMNDTruoc;
				patientProfile.ImgCmndAfterData = FileImageCMNDSau;
				patientProfile.HisPatient.HRM_EMPLOYEE_CODE = hrmEmployeeCode;
				patientProfile.HisPatient.TAX_CODE = taxCode;
				patientProfile.HisPatient.PATIENT_CLASSIFY_ID = patientClassifyId;
				patientProfile.HisPatient.POSITION_ID = PositionId;
				patientProfile.HisPatient.SOCIAL_INSURANCE_NUMBER = SocialInsuranceNumberPatient;
				patientProfile.HisPatient.BUD_REL_UNIT_CODE = dvqhnsCode;
				if (!string.IsNullOrEmpty(patientCode))
				{
					patientProfile.HisPatient.PATIENT_CODE = patientCode;
				}
				if (IsWarningForNext)
				{
					patientProfile.HisPatient.NOTE = NOTE;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessHeinPatientTypeCode()
		{
			try
			{
				string data = HeinPatientCode ?? "";
				HIS_HEIN_PATIENT_TYPE hIS_HEIN_PATIENT_TYPE = new BackendAdapter(base.param).Post<HIS_HEIN_PATIENT_TYPE>("api/HisServiceReq/ExamRegister", ApiConsumers.MosConsumer, data, base.param);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ProcessTreatmentData()
		{
			try
			{
				if (ClientTokenManagerStore.ClientTokenManager.GetTokenData() != null && departmentId > 0)
				{
					patientProfile.DepartmentId = departmentId;
				}
				if (programId != 0)
				{
					patientProfile.HisTreatment.PROGRAM_ID = programId;
				}
				if (oweTypeId > 0)
				{
					patientProfile.HisTreatment.OWE_TYPE_ID = oweTypeId;
					patientProfile.HisTreatment.OWE_MODIFY_TIME = intructionTime;
				}
				if (otherPaySourceId > 0)
				{
					patientProfile.HisTreatment.OTHER_PAY_SOURCE_ID = otherPaySourceId;
				}
				if (patientId != 0)
				{
					patientProfile.HisTreatment.PATIENT_ID = patientId;
					if (codeFind == typeCodeFind__MaCT && programId != 0)
					{
						patientProfile.HisTreatment.PROGRAM_ID = programId;
					}
					else if (codeFind == typeCodeFind__MaHK && !string.IsNullOrEmpty(appointmentCode))
					{
						patientProfile.HisTreatment.APPOINTMENT_CODE = appointmentCode;
					}
				}
				patientProfile.ProvinceCode = provinceCode;
				patientProfile.DistrictCode = districtCode;
				patientProfile.TreatmentTime = intructionTime;
				if (isCheckBaoLanh)
				{
					patientProfile.HisTreatment.GUARANTEE_CODE = Guarantee_Code;
					patientProfile.HisTreatment.GUARANTEE_REQUEST_CODE = Guarantee_Request_Code;
				}
				if (UCTransPatiADO != null && (!string.IsNullOrEmpty(icd_Code) || !string.IsNullOrEmpty(icd_Text) || !string.IsNullOrEmpty(icd_Name) || !string.IsNullOrEmpty(noiChuyenDen_Code) || !string.IsNullOrEmpty(noiChuyenDen_Name) || hinhThucChuyen_ID > 0 || !string.IsNullOrEmpty(soChuyenVien) || transfer_In_CMKT > 0 || ImgTransferInData != null || !string.IsNullOrEmpty(icd_Sub_Code) || !string.IsNullOrEmpty(icd_Sub_Name)))
				{
					patientProfile.HisTreatment.IS_TRANSFER_IN = (short)1;
				}
				if (ImgTransferInData != null)
				{
					patientProfile.ImgTransferInData = ImgTransferInData;
				}
				patientProfile.HisTreatment.TRANSFER_IN_ICD_CODE = icd_Code;
				if (!string.IsNullOrEmpty(TransferInCode))
				{
					patientProfile.HisTreatment.TRANSFER_IN_CODE = TransferInCode;
				}
				else
				{
					patientProfile.HisTreatment.TRANSFER_IN_CODE = soChuyenVien;
				}
				patientProfile.HisTreatment.IS_TRANSFER_IN = IsTransferIn;
				patientProfile.HisTreatment.TRANSFER_IN_ICD_NAME = ((!string.IsNullOrEmpty(icd_Text)) ? icd_Text : icd_Name);
				patientProfile.HisTreatment.TRANSFER_IN_ICD_SUB_CODE = icd_Sub_Code;
				patientProfile.HisTreatment.TRANSFER_IN_ICD_TEXT = icd_Sub_Name;
				patientProfile.HisTreatment.TRANSFER_IN_MEDI_ORG_CODE = noiChuyenDen_Code;
				patientProfile.HisTreatment.TRANSFER_IN_MEDI_ORG_NAME = noiChuyenDen_Name;
				patientProfile.HisTreatment.TRANSFER_IN_FORM_ID = hinhThucChuyen_ID;
				patientProfile.HisTreatment.TRANSFER_IN_REASON_ID = lyDoChuyen_ID;
				patientProfile.HisTreatment.TRANSFER_IN_CMKT = transfer_In_CMKT;
				patientProfile.HisTreatment.HRM_KSK_CODE = hrmKskCode;
				patientProfile.HisTreatment.TRANSFER_IN_TIME_FROM = transferInTimeFrom;
				patientProfile.HisTreatment.TRANSFER_IN_TIME_TO = transferInTimeTo;
				patientProfile.HisTreatment.TRANSFER_IN_REVIEWS = transferInReviews;
				patientProfile.HisTreatment.TREATMENT_ORDER = treatmentOrder;
				patientProfile.HisTreatment.IN_CODE = inCode;
				patientProfile.HisTreatment.IS_BHYT_HOLDED = isBhytHolded;
				patientProfile.HisTreatment.HEIN_PATIENT_TYPE_CODE = HeinPatientCode;
				patientProfile.HisTreatment.IS_HIV = (IsHiv ? new short?(1) : ((short?)null));
				if (FUND_ID > 0)
				{
					patientProfile.HisTreatment.FUND_ID = FUND_ID;
					patientProfile.HisTreatment.FUND_BUDGET = FUND_BUDGET;
					patientProfile.HisTreatment.FUND_COMPANY_NAME = FUND_COMPANY_NAME;
					patientProfile.HisTreatment.FUND_FROM_TIME = FUND_FROM_TIME;
					patientProfile.HisTreatment.FUND_ISSUE_TIME = FUND_ISSUE_TIME;
					patientProfile.HisTreatment.FUND_NUMBER = FUND_NUMBER;
					patientProfile.HisTreatment.FUND_TO_TIME = FUND_TO_TIME;
					patientProfile.HisTreatment.FUND_TYPE_NAME = FUND_TYPE_NAME;
					patientProfile.HisTreatment.FUND_CUSTOMER_NAME = FUND_CUSTOMER_NAME;
				}
				if (IsNeedSickLeaveCert.HasValue)
				{
					patientProfile.HisTreatment.NEED_SICK_LEAVE_CERT = (IsNeedSickLeaveCert.Value ? new short?(1) : ((short?)null));
				}
				if (receptionForm.HasValue)
				{
					patientProfile.HisTreatment.RECEPTION_FORM = receptionForm;
				}
				patientProfile.HisTreatment.TDL_SOCIAL_INSURANCE_NUMBER = SocialInsuranceNumberPatient;
				patientProfile.HisTreatment.HOSPITALIZE_REASON_CODE = HospitalizeReasonCode;
				patientProfile.HisTreatment.HOSPITALIZE_REASON_NAME = HospitalizeReasonName;
				patientProfile.HisTreatment.HOSPITALIZATION_REASON = HospitalizationReason;
				patientProfile.HisTreatment.CUSTOMER_SOURCE_CODE = NguonKhachCode;
				patientProfile.HisTreatment.CUSTOMER_SOURCE_NAME = NguonKhachName;
				patientProfile.HisTreatment.CUSTOMER_SOURCE_DETAIL = NguonKhachCT;
				patientProfile.HisTreatment.CUS_SOURCE_DETAIL_LOGINNAMES = NguonKhachCTName;
				patientProfile.HisTreatment.GUARANTEE_LOGINNAME = GUARANTEE_LOGINNAME;
				patientProfile.HisTreatment.GUARANTEE_USERNAME = GUARANTEE_USERNAME;
				patientProfile.HisTreatment.GUARANTEE_REASON = GUARANTEE_REASON;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessPatientTypeAlterData()
		{
			try
			{
				if (patientTypeId > 0)
				{
					CommonParam commonParam = new CommonParam();
					HisPatientProfileSDO hisPatientProfileSDO = new HisPatientProfileSDO();
					hisPatientProfileSDO.HisPatientTypeAlter = new HIS_PATIENT_TYPE_ALTER();
					if (patientTypeId == HisConfigCFG.PatientTypeId__BHYT || patientTypeId == HisConfigCFG.PatientTypeId__QN)
					{
						hisPatientProfileSDO.HisPatientTypeAlter = heinInfoValue.HisPatientTypeAlter;
					}
					if (patientProfile.HisPatientTypeAlter == null)
					{
						patientProfile.HisPatientTypeAlter = new HIS_PATIENT_TYPE_ALTER();
					}
					DataObjectMapper.Map<HIS_PATIENT_TYPE_ALTER>(patientProfile.HisPatientTypeAlter, hisPatientProfileSDO.HisPatientTypeAlter);
					if (patientTypeId == HisConfigCFG.PatientTypeId__KSK && kskContractId.HasValue)
					{
						patientProfile.HisPatientTypeAlter.KSK_CONTRACT_ID = kskContractId.Value;
					}
					patientProfile.HisPatientTypeAlter.PATIENT_TYPE_ID = patientTypeId;
					patientProfile.HisPatientTypeAlter.HNCODE = maHoNgheo;
					if (treatmentTypeId > 0)
					{
						patientProfile.HisPatientTypeAlter.TREATMENT_TYPE_ID = treatmentTypeId;
					}
					else
					{
						LogSystem.Debug("Lay doi tuong benh nhan theo gia tri cua combo dien dieu tri man hinh dang ky tiep don khong thanh cong. treatmentTypeId= " + treatmentTypeId);
					}
					if (cardSearch != null && !string.IsNullOrEmpty(cardSearch.CardCode))
					{
						patientProfile.CardCode = cardSearch.CardCode;
						patientProfile.CardServiceCode = cardSearch.ServiceCode;
						patientProfile.BankCardCode = cardSearch.BankCardCode;
					}
					else if (!string.IsNullOrEmpty(CardCode))
					{
						patientProfile.CardCode = CardCode;
						patientProfile.CardServiceCode = CardServiceCode;
						patientProfile.BankCardCode = BankCardCode;
					}
					if (patientTypeId == HisConfigCFG.PatientTypeId__BHYT || patientTypeId == HisConfigCFG.PatientTypeId__QN)
					{
						if (patientProfile.HisPatientTypeAlter.RIGHT_ROUTE_CODE == "DT" && patientProfile.HisPatientTypeAlter.RIGHT_ROUTE_TYPE_CODE == "GT")
						{
							patientProfile.HisTreatment.IS_TRANSFER_IN = (short)1;
						}
					}
					else
					{
						patientProfile.HisPatientTypeAlter.RIGHT_ROUTE_CODE = null;
						patientProfile.HisPatientTypeAlter.RIGHT_ROUTE_TYPE_CODE = null;
					}
					if ((HisConfigCFG.IsSetPrimaryPatientType == "2" || HisConfigCFG.IsSetPrimaryPatientType == "3") && patientRawInfoValue != null)
					{
						patientProfile.HisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID = patientRawInfoValue.PRIMARY_PATIENT_TYPE_ID;
					}
					patientProfile.HisPatientTypeAlter.GUARANTEE_LOGINNAME = GUARANTEE_LOGINNAME;
					patientProfile.HisPatientTypeAlter.GUARANTEE_USERNAME = GUARANTEE_USERNAME;
					patientProfile.HisPatientTypeAlter.GUARANTEE_REASON = GUARANTEE_REASON;
					patientProfile.HisPatientTypeAlter.IS_NEWBORN = (short)(isCheckSS ? 1 : 0);
				}
				else
				{
					LogSystem.Debug("Lay doi tuong benh nhan theo gia tri cua combo doi tuong man hinh dang ky tiep don khong thanh cong. patientTypeId= " + patientTypeId);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessEmergency()
		{
			try
			{
				if (patientProfile.HisTreatment == null)
				{
					patientProfile.HisTreatment = new HIS_TREATMENT();
				}
				patientProfile.HisTreatment.IS_EMERGENCY = (short)1;
				if (emergencyWTimeId > 0)
				{
					patientProfile.HisTreatment.EMERGENCY_WTIME_ID = emergencyWTimeId;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
