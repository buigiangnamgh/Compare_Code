using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using HIS.Desktop.Plugins.RegisterV2.Run2;
using Inventec.Common.Adapter;
using Inventec.Common.DateTime;
using Inventec.Common.Logging;
using Inventec.Common.TypeConvert;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using Inventec.UC.Login.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;

namespace HIS.Desktop.Plugins.RegisterV2.Register
{
	internal class ServiceRequestRegisterExamBehavior : ServiceRequestRegisterBehaviorBase, IServiceRequestRegisterExam
	{
		private HisServiceReqExamRegisterResultSDO result = null;

		private List<ServiceReqDetailSDO> serviceReqDetailSDOs;

		internal ServiceRequestRegisterExamBehavior(CommonParam param, UCRegister ucServiceRequestRegiter, HisPatientSDO patientData)
			: base(param, ucServiceRequestRegiter)
		{
			registerNumber = ucServiceRequestRegiter.registerNumber;
			base.priority = (base.serviceReqInfoValue.IsPriority ? GlobalVariables.HAS_PRIORITY : 0);
			base.priorityNumber = base.serviceReqInfoValue.PriorityNumber;
			base.isNotRequireFee = (base.serviceReqInfoValue.IsNotRequireFee ? new short?(1) : ((short?)null));
			serviceReqDetailSDOs = ucServiceRequestRegiter.serviceReqDetailSDOs;
		}

		HisServiceReqExamRegisterResultSDO IServiceRequestRegisterExam.Run()
		{
			HisServiceReqExamRegisterSDO serviceReqExamRegister = new HisServiceReqExamRegisterSDO();
			serviceReqExamRegister.HisPatientProfile = new HisPatientProfileSDO();
			InitBase();
			if (!string.IsNullOrEmpty(base.patientProfile.HisPatientTypeAlter.LIVE_AREA_CODE))
			{
				LogSystem.Debug("Thong bao khu vuc");
				WaitingManager.Hide();
				if (XtraMessageBox.Show(ResourceMessage.BanCoMuonNhapThongTinKhuVuc, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					ucRequestService.isShowMess = true;
					return null;
				}
				WaitingManager.Show();
			}
			if (CheckMaMS())
			{
				ucRequestService.isShowMess = false;
				return null;
			}
			if (base.patientProfile.HisPatientTypeAlter != null && base.patientProfile.HisPatientTypeAlter.ID == HisConfigCFG.PatientTypeId__BHYT && base.intructionTime > 0 && base.patientProfile.HisPatientTypeAlter.HEIN_CARD_TO_TIME.HasValue && base.treatmentTypeId != 3 && Parse.ToInt64(base.intructionTime.ToString().Substring(0, 8) + "000000") > base.patientProfile.HisPatientTypeAlter.HEIN_CARD_TO_TIME)
			{
				LogSystem.Debug("Thong bao han the");
				XtraMessageBox.Show(ResourceMessage.ThoiGianYLenhLonHonThoiGianHanDenCuaTheBHYT, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao);
				ucRequestService.isShowMess = true;
				return null;
			}
			serviceReqExamRegister.HisPatientProfile = base.patientProfile;
			serviceReqExamRegister.RequestRoomId = ((base.currentModule != null) ? base.currentModule.RoomId : 0);
			serviceReqExamRegister.Note = base.NOTE;
			serviceReqExamRegister.IsAutoCreateDepositForNonBhyt = base.chkAutoDeposit || base.chkAutoCreateBill;
			serviceReqExamRegister.IsUsingEpayment = base.chkAutoPay;
			serviceReqExamRegister.IsExamOnline = base.chkExamOnline;
			serviceReqExamRegister.IsRequestSkinCare = base.ChamSocDa;
			if (base.chkAutoCreateBill)
			{
				if (GlobalVariables.DefaultPayformRequest.HasValue && GlobalVariables.DefaultPayformRequest.Value > 0)
				{
					serviceReqExamRegister.PayFormId = GlobalVariables.DefaultPayformRequest.Value;
				}
				else
				{
					serviceReqExamRegister.PayFormId = 1L;
				}
				if (GlobalVariables.AuthorityAccountBook != null && GlobalVariables.AuthorityAccountBook.AccountBookId.HasValue)
				{
					serviceReqExamRegister.AccountBookId = GlobalVariables.AuthorityAccountBook.AccountBookId;
					serviceReqExamRegister.CashierLoginName = GlobalVariables.AuthorityAccountBook.CashierLoginName;
					serviceReqExamRegister.CashierUserName = GlobalVariables.AuthorityAccountBook.CashierUserName;
					serviceReqExamRegister.CashierWorkingRoomId = GlobalVariables.AuthorityAccountBook.CashierWorkingRoomId;
				}
			}
			else if (base.chkAutoDeposit)
			{
				serviceReqExamRegister.CashierWorkingRoomId = base.cashierRoom_RoomId;
				if (GlobalVariables.SessionInfo != null)
				{
					serviceReqExamRegister.AccountBookId = GlobalVariables.SessionInfo.DepositAccountBook.ID;
					serviceReqExamRegister.CashierLoginName = ClientTokenManagerStore.ClientTokenManager.GetLoginName();
					serviceReqExamRegister.CashierUserName = ClientTokenManagerStore.ClientTokenManager.GetUserName();
					if (GlobalVariables.SessionInfo.PayForm != null)
					{
						serviceReqExamRegister.PayFormId = GlobalVariables.SessionInfo.PayForm.ID;
					}
					if (GlobalVariables.SessionInfo.DepositAccountBook.IS_NOT_GEN_TRANSACTION_ORDER == 1)
					{
						serviceReqExamRegister.TransNumOrder = GlobalVariables.SessionInfo.NextDepositNumOrder;
					}
				}
			}
			List<long> serviceIds = new List<long>();
			List<long> _roomIds = new List<long>();
			ProcessExamServiceRequestData(ref serviceReqExamRegister, ref serviceIds, ref _roomIds);
			ServiceReqDetailSDO serviceReqDetailSDO = null;
			if (serviceReqExamRegister.ServiceReqDetails != null && serviceReqExamRegister.ServiceReqDetails.Count > 0 && base.patientData != null)
			{
				serviceReqDetailSDO = serviceReqExamRegister.ServiceReqDetails.FirstOrDefault((ServiceReqDetailSDO o) => o.ServiceId == base.patientData.AppointmentExamServiceId || o.RoomId == ((base.patientData.AppointmentExamRoomIds != null) ? base.patientData.AppointmentExamRoomIds.First() : 0));
			}
			if (base.patientData != null && base.patientData.AppointmentTime.HasValue && base.patientData.AppointmentExamServiceId.HasValue && base.patientData.AppointmentExamRoomIds != null && base.patientData.AppointmentExamRoomIds.Count > 0 && base.patientData.NumOrderIssueId.HasValue && base.patientData.NextExamNumOrder.HasValue && (serviceReqDetailSDO == null || serviceReqDetailSDO.RoomId != base.patientData.AppointmentExamRoomIds.First()))
			{
				LogSystem.Debug("Thong bao doi cau hinh");
				List<V_HIS_EXECUTE_ROOM> list = BackendDataWorker.Get<V_HIS_EXECUTE_ROOM>();
				V_HIS_EXECUTE_ROOM v_HIS_EXECUTE_ROOM = ((list != null && list.Count > 0) ? list.Where((V_HIS_EXECUTE_ROOM t) => t.ROOM_ID == base.patientData.AppointmentExamRoomIds.First()).FirstOrDefault() : null);
				WaitingManager.Hide();
				if (XtraMessageBox.Show(string.Format(ResourceMessage.DoiDichVuHenKhamSeDoiSttDaCap, Inventec.Common.DateTime.Convert.TimeNumberToDateString(base.patientData.AppointmentTime.Value), (v_HIS_EXECUTE_ROOM != null) ? v_HIS_EXECUTE_ROOM.EXECUTE_ROOM_NAME : ""), ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					ucRequestService.isShowMess = true;
					return null;
				}
				serviceReqDetailSDO.NumOrder = base.patientData.NextExamNumOrder;
				serviceReqDetailSDO.NumOrderIssueId = base.patientData.NumOrderIssueId;
				WaitingManager.Show();
			}
			if (_roomIds != null && _roomIds.Count > 0 && HisConfigCFG.IsWarningOverExamBhyt && base.patientTypeId == HisConfigCFG.PatientTypeId__BHYT && base.treatmentTypeId == 1)
			{
				HisSereServBhytOutpatientExamFilter hisSereServBhytOutpatientExamFilter = new HisSereServBhytOutpatientExamFilter();
				hisSereServBhytOutpatientExamFilter.ROOM_IDs = _roomIds;
				hisSereServBhytOutpatientExamFilter.INTRUCTION_DATE = Parse.ToInt64(base.intructionTime.ToString().Substring(0, 8) + "000000");
				List<HIS_SERE_SERV> list2 = new BackendAdapter(new CommonParam()).Get<List<HIS_SERE_SERV>>("api/HisSereServ/GetSereServBhytOutpatientExam", ApiConsumers.MosConsumer, hisSereServBhytOutpatientExamFilter, null);
				if (list2 != null && list2.Count > 0)
				{
					HisExecuteRoomFilter hisExecuteRoomFilter = new HisExecuteRoomFilter();
					hisExecuteRoomFilter.ROOM_IDs = _roomIds;
					List<HIS_EXECUTE_ROOM> list3 = new BackendAdapter(new CommonParam()).Get<List<HIS_EXECUTE_ROOM>>("api/HisExecuteRoom/Get", ApiConsumers.MosConsumer, hisExecuteRoomFilter, null);
					if (list3 != null && list3.Count > 0)
					{
						foreach (HIS_EXECUTE_ROOM itemRoom in list3)
						{
							int num = list2.Count((HIS_SERE_SERV p) => p.TDL_EXECUTE_ROOM_ID == itemRoom.ROOM_ID);
							if (itemRoom.MAX_REQ_BHYT_BY_DAY.HasValue && num >= itemRoom.MAX_REQ_BHYT_BY_DAY)
							{
								LogSystem.Debug("Thong bao vuot");
								WaitingManager.Hide();
								if (XtraMessageBox.Show(string.Format(ResourceMessage.VuotQuaLuotKhamBHYTTrongNgay, itemRoom.EXECUTE_ROOM_NAME, itemRoom.MAX_REQ_BHYT_BY_DAY), ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
								{
									ucRequestService.isShowMess = true;
									return null;
								}
								WaitingManager.Show();
							}
						}
					}
				}
			}
			if (AppConfigs.IsDangKyQuaTongDai == "1")
			{
				serviceReqExamRegister.IsNoExecute = true;
			}
			List<HIS_SERE_SERV> sereServWithMinDuration = GetSereServWithMinDuration(base.patientId, serviceIds);
			if (sereServWithMinDuration != null && sereServWithMinDuration.Count > 0)
			{
				string text = "";
				foreach (HIS_SERE_SERV item in sereServWithMinDuration)
				{
					text = text + item.TDL_SERVICE_CODE + " - " + item.TDL_SERVICE_NAME + "; ";
				}
				LogSystem.Debug("Thong bao dich vu");
				if (MessageBox.Show(string.Format(ResourceMessage.CanhBaoDichVuDaDuocChiDinhTrongKhoangThoiGianCauHinh, text), ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo) == DialogResult.No)
				{
					ucRequestService.isShowMess = true;
					LogSystem.Warn("Cac dich vu sau co thoi gian chi dinh nam trong khoang thoi gian khong cho phep, ____" + text);
					return null;
				}
				ucRequestService.isShowMess = false;
			}
			if (HisConfigCFG.IsCheckExamination && (serviceIds == null || serviceIds.Count <= 0))
			{
				LogSystem.Debug("Thong bao cong kham");
				WaitingManager.Hide();
				if (XtraMessageBox.Show(ResourceMessage.BenhNhanChuaChonCongKham, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					ucRequestService.isShowMess = true;
					return null;
				}
				WaitingManager.Show();
			}
			result = (HisServiceReqExamRegisterResultSDO)RunBase(serviceReqExamRegister, ucRequestService);
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => result), result));
			if (result != null && (result.HisPatientProfile == null || result.ServiceReqs == null || result.ServiceReqs.Count == 0))
			{
				LogSystem.Warn("Goi api dang ky tiep don thanh cong, tuy nhien du lieu tra ve khong hop le, Dau vao____" + LogUtil.TraceData(LogUtil.GetMemberName(() => serviceReqExamRegister), serviceReqExamRegister) + ", Dau ra____" + LogUtil.TraceData(LogUtil.GetMemberName(() => result), result) + "__" + LogUtil.TraceData(LogUtil.GetMemberName(() => param), base.param));
			}
			else if (result != null)
			{
				if (result.SereServs != null && result.SereServs.Count > 0)
				{
					ucRequestService.serviceReqPrintIds = (from o in result.SereServs
						where serviceIds.Contains(o.SERVICE_ID)
						select o.SERVICE_REQ_ID.GetValueOrDefault()).Distinct().ToList();
				}
				else
				{
					ucRequestService.serviceReqPrintIds = result.ServiceReqs.Select((V_HIS_SERVICE_REQ o) => o.ID).Distinct().ToList();
				}
			}
			else
			{
				LogSystem.Warn("Goi api dang ky tiep don that bai, Dau vao____" + LogUtil.TraceData(LogUtil.GetMemberName(() => serviceReqExamRegister), serviceReqExamRegister) + ", Dau ra____" + LogUtil.TraceData(LogUtil.GetMemberName(() => result), result) + "__" + LogUtil.TraceData(LogUtil.GetMemberName(() => param), base.param));
			}
			return result;
		}

		private void ProcessExamServiceRequestData(ref HisServiceReqExamRegisterSDO ServiceReqData, ref List<long> serviceIds, ref List<long> _roomIds)
		{
			try
			{
				if (ServiceReqData.ServiceReqDetails == null)
				{
					ServiceReqData.ServiceReqDetails = new List<ServiceReqDetailSDO>();
				}
				if (serviceReqDetailSDOs != null && serviceReqDetailSDOs.Count > 0)
				{
					ServiceReqData.ServiceReqDetails.AddRange(serviceReqDetailSDOs);
				}
				foreach (ServiceReqDetailSDO serviceReqDetail in ServiceReqData.ServiceReqDetails)
				{
					if ((HisConfigCFG.IsSetPrimaryPatientType == "2" || HisConfigCFG.IsSetPrimaryPatientType == "3") && (!serviceReqDetail.PrimaryPatientTypeId.HasValue || serviceReqDetail.PrimaryPatientTypeId <= 0) && serviceReqDetail.PatientTypeId != base.patientProfile.HisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID)
					{
						serviceReqDetail.PrimaryPatientTypeId = base.patientProfile.HisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID;
					}
					serviceIds.Add(serviceReqDetail.ServiceId);
					_roomIds.Add(serviceReqDetail.RoomId.GetValueOrDefault());
				}
				ServiceReqData.Priority = base.priority;
				if (base.priorityNumber.HasValue)
				{
					ServiceReqData.NumOrder = base.priorityNumber;
				}
				ServiceReqData.InstructionTime = base.intructionTime;
				ServiceReqData.IsNotRequireFee = base.isNotRequireFee;
				ServiceReqData.PriorityTypeId = base.priorityTypeId;
				ServiceReqData.RequestLoginName = ClientTokenManagerStore.ClientTokenManager.GetLoginName();
				ServiceReqData.RequestUserName = ClientTokenManagerStore.ClientTokenManager.GetUserName();
				if (ServiceReqData.ServiceReqDetails == null || ServiceReqData.ServiceReqDetails.Count <= 0)
				{
					return;
				}
				foreach (ServiceReqDetailSDO serviceReqDetail2 in ServiceReqData.ServiceReqDetails)
				{
					if (base.otherPaySourceId > 0)
					{
						serviceReqDetail2.OtherPaySourceId = base.otherPaySourceId;
					}
					serviceReqDetail2.IsGuaranteed = base.isCheckBaoLanh;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ServiceAttachForServicePrimary(HisServiceReqExamRegisterSDO serviceReqExamRegisterSDO, ref HisServiceReqExamRegisterSDO result)
		{
			try
			{
				List<long> list = (from o in serviceReqExamRegisterSDO.ServiceReqDetails
					where o.ServiceId > 0
					select o.ServiceId).ToList();
				IEnumerable<ServiceReqDetailSDO> enumerable = serviceReqExamRegisterSDO.ServiceReqDetails.Where((ServiceReqDetailSDO o) => o.PrimaryPatientTypeId.HasValue);
				long? num = ((enumerable != null && enumerable.Count() > 0) ? enumerable.Select((ServiceReqDetailSDO o) => o.PrimaryPatientTypeId).FirstOrDefault() : ((long?)null));
				List<HIS_SERVICE_FOLLOW> list2 = BackendDataWorker.Get<HIS_SERVICE_FOLLOW>();
				List<HIS_SERVICE_FOLLOW> list3 = ((list2 != null) ? list2.Where((HIS_SERVICE_FOLLOW o) => serviceReqExamRegisterSDO != null && serviceReqExamRegisterSDO.ServiceReqDetails.Exists((ServiceReqDetailSDO t) => t.ServiceId == o.SERVICE_ID)).ToList() : null);
				if (list3 == null || list3.Count <= 0)
				{
					return;
				}
				List<ServiceReqDetailSDO> list4 = new List<ServiceReqDetailSDO>();
				long pATIENT_TYPE_ID = serviceReqExamRegisterSDO.HisPatientProfile.HisPatientTypeAlter.PATIENT_TYPE_ID;
				foreach (ServiceReqDetailSDO sdo in serviceReqExamRegisterSDO.ServiceReqDetails)
				{
					List<HIS_SERVICE_FOLLOW> list5 = list3.Where((HIS_SERVICE_FOLLOW t) => t.SERVICE_ID == sdo.ServiceId).ToList();
					if (list5 == null || list5.Count <= 0)
					{
						continue;
					}
					StringBuilder stringBuilder = new StringBuilder();
					StringBuilder stringBuilder2 = new StringBuilder();
					foreach (HIS_SERVICE_FOLLOW f in list5)
					{
						V_HIS_SERVICE_PATY v_HIS_SERVICE_PATY = null;
						if (BranchDataWorker.DicServicePatyInBranch != null && BranchDataWorker.DicServicePatyInBranch.ContainsKey(f.FOLLOW_ID))
						{
							v_HIS_SERVICE_PATY = (from m in BranchDataWorker.ServicePatyWithPatientType(f.FOLLOW_ID, pATIENT_TYPE_ID)
								orderby m.MODIFY_TIME descending
								select m).FirstOrDefault();
						}
						long? patientTypeId = null;
						if (v_HIS_SERVICE_PATY != null)
						{
							patientTypeId = pATIENT_TYPE_ID;
							LogSystem.Debug("ServiceAttachForServicePrimary____" + LogUtil.TraceData(LogUtil.GetMemberName(() => patientTypeId), patientTypeId));
						}
						else
						{
							V_HIS_SERVICE_PATY otherServicePaty = (from m in BranchDataWorker.ServicePatyWithListPatientType(f.FOLLOW_ID, GlobalStore.PatientTypeIdAllows)
								orderby m.MODIFY_TIME descending
								select m).FirstOrDefault();
							List<HIS_PATIENT_TYPE> source = (from o in BackendDataWorker.Get<HIS_PATIENT_TYPE>()
								where o.IS_ACTIVE == 1
								select o).ToList();
							patientTypeId = ((otherServicePaty != null) ? new long?(otherServicePaty.PATIENT_TYPE_ID) : ((long?)null));
							List<HIS_PATIENT_TYPE> patientTypeIdPlus = source.Where((HIS_PATIENT_TYPE k) => k.BASE_PATIENT_TYPE_ID.HasValue && GlobalStore.PatientTypeIdAllows.Contains(k.BASE_PATIENT_TYPE_ID.Value)).ToList();
							if (patientTypeIdPlus != null && patientTypeIdPlus.Count > 0 && otherServicePaty != null && !string.IsNullOrEmpty(otherServicePaty.INHERIT_PATIENT_TYPE_IDS) && patientTypeIdPlus.Exists((HIS_PATIENT_TYPE k) => k.ID != patientTypeId))
							{
								patientTypeId = patientTypeIdPlus.First().ID;
							}
							LogSystem.Debug("ServiceAttachForServicePrimary____" + LogUtil.TraceData(LogUtil.GetMemberName(() => otherServicePaty), otherServicePaty) + LogUtil.TraceData(LogUtil.GetMemberName(() => patientTypeIdPlus), patientTypeIdPlus) + LogUtil.TraceData(LogUtil.GetMemberName(() => patientTypeId), patientTypeId));
						}
						if (patientTypeId.HasValue)
						{
							ServiceReqDetailSDO serviceReqDetailSDO = new ServiceReqDetailSDO();
							serviceReqDetailSDO.ServiceId = f.FOLLOW_ID;
							serviceReqDetailSDO.Amount = f.AMOUNT;
							serviceReqDetailSDO.IsExpend = f.IS_EXPEND;
							serviceReqDetailSDO.PatientTypeId = patientTypeId.Value;
							if ((HisConfigCFG.IsSetPrimaryPatientType == "2" || HisConfigCFG.IsSetPrimaryPatientType == "3") && (!num.HasValue || num <= 0) && serviceReqDetailSDO.PatientTypeId != base.patientProfile.HisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID)
							{
								serviceReqDetailSDO.PrimaryPatientTypeId = base.patientProfile.HisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID;
							}
							list4.Add(serviceReqDetailSDO);
						}
						else
						{
							List<V_HIS_SERVICE> source2 = BackendDataWorker.Get<V_HIS_SERVICE>();
							stringBuilder.Append(source2.SingleOrDefault((V_HIS_SERVICE o) => o.ID == f.SERVICE_ID).SERVICE_NAME).Append(",");
							stringBuilder2.Append(source2.SingleOrDefault((V_HIS_SERVICE o) => o.ID == sdo.ServiceId).SERVICE_NAME).Append(",");
						}
					}
					if (!string.IsNullOrEmpty(stringBuilder.ToString()) || !string.IsNullOrEmpty(stringBuilder2.ToString()))
					{
						MessageManager.Show(string.Format(ResourceMessage.DichVuDinhKemDichVuChuaCoChinhSachGia, stringBuilder.ToString(), stringBuilder2.ToString()));
					}
				}
				if (list4 != null && list4.Count > 0)
				{
					serviceReqExamRegisterSDO.ServiceReqDetails.AddRange(list4);
				}
				result = serviceReqExamRegisterSDO;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private List<HIS_SERE_SERV> GetSereServWithMinDuration(long patientId, List<long> serviceIds)
		{
			List<HIS_SERE_SERV> list = new List<HIS_SERE_SERV>();
			try
			{
				if (serviceIds == null || serviceIds.Count == 0)
				{
					LogSystem.Debug("Khong truyen danh sach serviceids");
					return null;
				}
				List<V_HIS_SERVICE> list2 = (from o in BackendDataWorker.Get<V_HIS_SERVICE>()
					where serviceIds.Contains(o.ID) && o.MIN_DURATION.HasValue
					select o).ToList();
				if (list2 != null && list2.Count > 0)
				{
					List<ServiceDuration> list3 = new List<ServiceDuration>();
					foreach (V_HIS_SERVICE item in list2)
					{
						ServiceDuration serviceDuration = new ServiceDuration();
						serviceDuration.MinDuration = item.MIN_DURATION.Value;
						serviceDuration.ServiceId = item.ID;
						list3.Add(serviceDuration);
					}
					CommonParam commonParam = new CommonParam();
					HisSereServMinDurationFilter hisSereServMinDurationFilter = new HisSereServMinDurationFilter();
					hisSereServMinDurationFilter.ServiceDurations = list3;
					hisSereServMinDurationFilter.PatientId = patientId;
					hisSereServMinDurationFilter.InstructionTime = base.intructionTime;
					list = new BackendAdapter(commonParam).Get<List<HIS_SERE_SERV>>("api/HisSereServ/GetExceedMinDuration", ApiConsumers.MosConsumer, hisSereServMinDurationFilter, commonParam);
					if (list != null && list.Count > 0)
					{
						IEnumerable<HIS_SERE_SERV> source = from SereServResult in list
							group SereServResult by SereServResult.SERVICE_ID into g
							orderby g.Key
							select g.FirstOrDefault();
						list = source.ToList();
					}
				}
			}
			catch (Exception ex)
			{
				list = null;
				LogSystem.Warn(ex);
			}
			return list;
		}
	}
}
