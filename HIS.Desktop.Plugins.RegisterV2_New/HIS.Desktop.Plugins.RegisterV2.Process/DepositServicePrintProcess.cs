using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HIS.Common.Treatment;
using HIS.Desktop.ADO;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.ModuleExt;
using HIS.Desktop.Plugins.Library.EmrGenerate;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.LibraryHein.Bhyt;
using MOS.SDO;
using MPS;
using MPS.Processor.Mps000102.PDO;
using MPS.ProcessorBase;
using MPS.ProcessorBase.Core;

namespace HIS.Desktop.Plugins.RegisterV2.Process
{
	internal class DepositServicePrintProcess
	{
		private static Module module;

		public static void LoadPhieuThuPhiDichVu(string printTypeCode, string fileName, bool isExpand, List<V_HIS_SERE_SERV_12> SereServAlls, HisServiceReqExamRegisterResultSDO resultSDO, bool isPrintNow, Module moduleData)
		{
			bool flag = false;
			V_HIS_PATIENT_TYPE_ALTER v_HIS_PATIENT_TYPE_ALTER = new V_HIS_PATIENT_TYPE_ALTER();
			MPS.Processor.Mps000102.PDO.PatientADO patientADO = new MPS.Processor.Mps000102.PDO.PatientADO();
			V_HIS_TREATMENT_FEE currentHisTreatment = null;
			try
			{
				module = moduleData;
				if (resultSDO.HisPatientProfile.HisPatient == null)
				{
					HisPatientViewFilter hisPatientViewFilter = new HisPatientViewFilter();
					hisPatientViewFilter.ID = resultSDO.HisPatientProfile.HisTreatment.PATIENT_ID;
					List<V_HIS_PATIENT> list = new BackendAdapter(new CommonParam()).Get<List<V_HIS_PATIENT>>("api/HisPatient/GetView", ApiConsumers.MosConsumer, hisPatientViewFilter, null);
					if (list != null && list.Count > 0)
					{
						patientADO = new MPS.Processor.Mps000102.PDO.PatientADO(list.FirstOrDefault());
					}
				}
				else
				{
					Mapper.CreateMap<HIS_PATIENT, V_HIS_PATIENT>();
					V_HIS_PATIENT data = Mapper.Map<V_HIS_PATIENT>(resultSDO.HisPatientProfile.HisPatient);
					patientADO = new MPS.Processor.Mps000102.PDO.PatientADO(data);
				}
				HisTreatmentFeeViewFilter hisTreatmentFeeViewFilter = new HisTreatmentFeeViewFilter();
				hisTreatmentFeeViewFilter.ID = resultSDO.HisPatientProfile.HisTreatment.ID;
				List<V_HIS_TREATMENT_FEE> list2 = new BackendAdapter(new CommonParam()).Get<List<V_HIS_TREATMENT_FEE>>("api/HisTreatment/GetFeeView", ApiConsumers.MosConsumer, hisTreatmentFeeViewFilter, null);
				if (list2 != null)
				{
					currentHisTreatment = list2.FirstOrDefault();
				}
				HisPatientTypeAlterViewFilter hisPatientTypeAlterViewFilter = new HisPatientTypeAlterViewFilter();
				hisPatientTypeAlterViewFilter.TREATMENT_ID = resultSDO.HisPatientProfile.HisTreatment.ID;
				hisPatientTypeAlterViewFilter.ID = resultSDO.HisPatientProfile.HisPatientTypeAlter.ID;
				List<V_HIS_PATIENT_TYPE_ALTER> list3 = new BackendAdapter(new CommonParam()).Get<List<V_HIS_PATIENT_TYPE_ALTER>>("api/HisPatientTypeALter/GetView", ApiConsumers.MosConsumer, hisPatientTypeAlterViewFilter, null);
				if (list3 != null && list3.Count > 0)
				{
					v_HIS_PATIENT_TYPE_ALTER = list3.OrderByDescending((V_HIS_PATIENT_TYPE_ALTER o) => o.LOG_TIME).FirstOrDefault();
				}
				List<V_HIS_DEPARTMENT_TRAN> departmentTrans = new BackendAdapter(new CommonParam()).Get<List<V_HIS_DEPARTMENT_TRAN>>("api/HisDepartmentTran/GetHospitalInOut", ApiConsumers.MosConsumer, resultSDO.HisPatientProfile.HisTreatment.ID, null);
				long? num = null;
				num = ((resultSDO.HisPatientProfile.HisTreatment.TDL_PATIENT_TYPE_ID != HisConfigCFG.PatientTypeId__BHYT) ? Calculation.DayOfTreatment(resultSDO.HisPatientProfile.HisTreatment.IN_TIME, resultSDO.HisPatientProfile.HisTreatment.OUT_TIME, resultSDO.HisPatientProfile.HisTreatment.TREATMENT_END_TYPE_ID, resultSDO.HisPatientProfile.HisTreatment.TREATMENT_RESULT_ID, PatientTypeEnum.TYPE.THU_PHI) : Calculation.DayOfTreatment(resultSDO.HisPatientProfile.HisTreatment.IN_TIME, resultSDO.HisPatientProfile.HisTreatment.OUT_TIME, resultSDO.HisPatientProfile.HisTreatment.TREATMENT_END_TYPE_ID, resultSDO.HisPatientProfile.HisTreatment.TREATMENT_RESULT_ID, PatientTypeEnum.TYPE.BHYT));
				string departmentName = WorkPlace.GetDepartmentName();
				long SERVICE_REPORT_ID__HIGHTECH = 2L;
				List<V_HIS_SERE_SERV_12> sereServs = SereServAlls.Where((V_HIS_SERE_SERV_12 o) => o.TDL_HEIN_SERVICE_TYPE_ID == SERVICE_REPORT_ID__HIGHTECH).ToList();
				List<SereServGroupPlusADO> list4 = PriceBHYTSereServAdoProcess(sereServs);
				long SERVICE_REPORT__MATERIAL_VTTT_ID = 14L;
				List<V_HIS_SERE_SERV_12> sereServs2 = SereServAlls.Where((V_HIS_SERE_SERV_12 o) => o.TDL_HEIN_SERVICE_TYPE_ID == SERVICE_REPORT__MATERIAL_VTTT_ID && o.IS_OUT_PARENT_FEE.HasValue).ToList();
				List<SereServGroupPlusADO> list5 = PriceBHYTSereServAdoProcess(sereServs2);
				List<V_HIS_SERE_SERV_12> source = SereServAlls.Where((V_HIS_SERE_SERV_12 o) => o.TDL_HEIN_SERVICE_TYPE_ID != SERVICE_REPORT_ID__HIGHTECH).ToList();
				foreach (SereServGroupPlusADO sereServHitech in list4)
				{
					List<SereServGroupPlusADO> source2 = new List<SereServGroupPlusADO>();
					List<V_HIS_SERE_SERV_12> sereServs3 = SereServAlls.Where((V_HIS_SERE_SERV_12 o) => o.PARENT_ID == sereServHitech.ID && !o.IS_OUT_PARENT_FEE.HasValue).ToList();
					SereServGroupPlusADO sereServGroupPlusADO = sereServHitech;
					sereServGroupPlusADO.VIR_PRICE += source2.Sum((SereServGroupPlusADO o) => o.VIR_TOTAL_PRICE);
					source2 = PriceBHYTSereServAdoProcess(sereServs3);
					SereServGroupPlusADO sereServGroupPlusADO2 = sereServHitech;
					sereServGroupPlusADO2.VIR_HEIN_PRICE += source2.Sum((SereServGroupPlusADO o) => o.VIR_HEIN_PRICE);
					SereServGroupPlusADO sereServGroupPlusADO3 = sereServHitech;
					sereServGroupPlusADO3.VIR_PATIENT_PRICE += source2.Sum((SereServGroupPlusADO o) => o.VIR_HEIN_PRICE);
					decimal num2 = default(decimal);
					foreach (SereServGroupPlusADO item2 in source2)
					{
						num2 += item2.AMOUNT * item2.PRICE_BHYT;
					}
					sereServHitech.PRICE_BHYT += num2;
					SereServGroupPlusADO sereServGroupPlusADO4 = sereServHitech;
					sereServGroupPlusADO4.HEIN_LIMIT_PRICE += source2.Sum((SereServGroupPlusADO o) => o.HEIN_LIMIT_PRICE);
					SereServGroupPlusADO sereServGroupPlusADO5 = sereServHitech;
					sereServGroupPlusADO5.VIR_TOTAL_PRICE += source2.Sum((SereServGroupPlusADO o) => o.VIR_TOTAL_PRICE);
					SereServGroupPlusADO sereServGroupPlusADO6 = sereServHitech;
					sereServGroupPlusADO6.VIR_TOTAL_HEIN_PRICE += source2.Sum((SereServGroupPlusADO o) => o.VIR_TOTAL_HEIN_PRICE);
					SereServGroupPlusADO sereServGroupPlusADO7 = sereServHitech;
					sereServGroupPlusADO7.VIR_TOTAL_PATIENT_PRICE += source2.Sum((SereServGroupPlusADO o) => o.VIR_TOTAL_PATIENT_PRICE);
				}
				List<SereServGroupPlusADO> list6 = new List<SereServGroupPlusADO>();
				foreach (SereServGroupPlusADO sereServVTTTADO in list5)
				{
					List<SereServGroupPlusADO> list7 = list4.Where((SereServGroupPlusADO o) => o.ID == sereServVTTTADO.PARENT_ID).ToList();
					if (list7.Count == 0)
					{
						list6.Add(sereServVTTTADO);
					}
				}
				foreach (SereServGroupPlusADO item3 in list6)
				{
					list5.Remove(item3);
				}
				IEnumerable<long> sereServVTTTIds = list5.Select((SereServGroupPlusADO o) => o.ID);
				source = source.Where((V_HIS_SERE_SERV_12 o) => !sereServVTTTIds.Contains(o.ID)).ToList();
				List<SereServGroupPlusADO> sereServNotHiTechs = PriceBHYTSereServAdoProcess(source);
				HisHeinServiceTypeFilter hisHeinServiceTypeFilter = new HisHeinServiceTypeFilter();
				hisHeinServiceTypeFilter.IS_ACTIVE = 1;
				List<HIS_HEIN_SERVICE_TYPE> serviceReports = new BackendAdapter(new CommonParam()).Get<List<HIS_HEIN_SERVICE_TYPE>>("api/HisHeinServiceType/Get", ApiConsumers.MosConsumer, hisHeinServiceTypeFilter, null);
				string ratio = (new BhytHeinProcessor().GetDefaultHeinRatio(v_HIS_PATIENT_TYPE_ALTER.HEIN_TREATMENT_TYPE_CODE, v_HIS_PATIENT_TYPE_ALTER.HEIN_CARD_NUMBER, v_HIS_PATIENT_TYPE_ALTER.LEVEL_CODE, v_HIS_PATIENT_TYPE_ALTER.RIGHT_ROUTE_CODE).GetValueOrDefault() * 100m).ToString() ?? "";
				InputADO emrInputADO = new EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode((resultSDO.HisPatientProfile.HisTreatment != null) ? resultSDO.HisPatientProfile.HisTreatment.TREATMENT_CODE : "", printTypeCode, moduleData.RoomId);
				List<V_HIS_TRANSACTION> list8 = resultSDO.Transactions.Where((V_HIS_TRANSACTION o) => o.TRANSACTION_TYPE_ID == 1).ToList();
				if (list8 != null && list8.Count > 0)
				{
					foreach (V_HIS_TRANSACTION item in list8)
					{
						List<HIS_SERE_SERV_DEPOSIT> dereDetails = resultSDO.SereServDeposits.Where((HIS_SERE_SERV_DEPOSIT o) => o.DEPOSIT_ID == item.ID).ToList();
						Mps000102PDO data2 = new Mps000102PDO(patientADO, v_HIS_PATIENT_TYPE_ALTER, departmentName, sereServNotHiTechs, list4, list5, departmentTrans, currentHisTreatment, serviceReports, item, dereDetails, num, ratio, resultSDO.ServiceReqs.FirstOrDefault());
						PrintData printData = null;
						printData = ((!isPrintNow) ? new PrintData(printTypeCode, fileName, data2, MPS.ProcessorBase.PrintConfig.PreviewType.Show, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "", 1, false, true)
						{
							EmrInputADO = emrInputADO
						} : new PrintData(printTypeCode, fileName, data2, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, (GlobalVariables.dicPrinter.ContainsKey(printTypeCode) && !string.IsNullOrEmpty(GlobalVariables.dicPrinter[printTypeCode])) ? GlobalVariables.dicPrinter[printTypeCode] : "")
						{
							EmrInputADO = emrInputADO
						});
						printData.ShowPrintLog = new MPS.ProcessorBase.PrintConfig.DelegateShowPrintLog(CallModuleShowPrintLog);
						MpsPrinter.Run(printData);
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				flag = false;
			}
		}

		private static List<SereServGroupPlusADO> PriceBHYTSereServAdoProcess(List<V_HIS_SERE_SERV_12> sereServs)
		{
			List<SereServGroupPlusADO> list = new List<SereServGroupPlusADO>();
			try
			{
				using (List<V_HIS_SERE_SERV_12>.Enumerator enumerator = sereServs.GetEnumerator())
				{
					SereServGroupPlusADO sereServGroupPlusADO;
					for (; enumerator.MoveNext(); list.Add(sereServGroupPlusADO))
					{
						V_HIS_SERE_SERV_12 current = enumerator.Current;
						sereServGroupPlusADO = new SereServGroupPlusADO();
						Mapper.CreateMap<V_HIS_SERE_SERV_12, SereServGroupPlusADO>();
						sereServGroupPlusADO = Mapper.Map<V_HIS_SERE_SERV_12, SereServGroupPlusADO>(current);
						string text = HisConfigs.Get<string>("MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT");
						if (sereServGroupPlusADO.PATIENT_TYPE_CODE != text)
						{
							sereServGroupPlusADO.PRICE_BHYT = 0m;
							continue;
						}
						if (sereServGroupPlusADO.HEIN_LIMIT_PRICE.HasValue)
						{
							decimal? hEIN_LIMIT_PRICE = sereServGroupPlusADO.HEIN_LIMIT_PRICE;
							if ((hEIN_LIMIT_PRICE.GetValueOrDefault() > default(decimal)) & hEIN_LIMIT_PRICE.HasValue)
							{
								sereServGroupPlusADO.PRICE_BHYT = current.HEIN_LIMIT_PRICE.GetValueOrDefault();
								continue;
							}
						}
						sereServGroupPlusADO.PRICE_BHYT = current.VIR_PRICE_NO_ADD_PRICE.GetValueOrDefault();
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return list;
		}

		private static void CallModuleShowPrintLog(string printTypeCode, string uniqueCode)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(printTypeCode) && !string.IsNullOrWhiteSpace(uniqueCode))
				{
					PrintLogADO item = new PrintLogADO(printTypeCode, uniqueCode);
					List<object> list = new List<object>();
					list.Add(item);
					PluginInstanceBehavior.ShowModule("Inventec.Desktop.Plugins.PrintLog", module.RoomId, module.RoomTypeId, list);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}
	}
}
