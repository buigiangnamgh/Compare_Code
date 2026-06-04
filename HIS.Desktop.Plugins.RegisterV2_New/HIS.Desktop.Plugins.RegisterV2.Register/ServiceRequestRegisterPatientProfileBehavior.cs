using System.Windows.Forms;
using DevExpress.XtraEditors;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using HIS.Desktop.Plugins.RegisterV2.Run2;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.SDO;

namespace HIS.Desktop.Plugins.RegisterV2.Register
{
	internal class ServiceRequestRegisterPatientProfileBehavior : ServiceRequestRegisterBehaviorBase, IServiceRequestRegisterPatientProfile
	{
		private HisPatientProfileSDO result = null;

		private UCRegister _ucServiceRequestRegister;

		internal ServiceRequestRegisterPatientProfileBehavior(CommonParam param, UCRegister ucServiceRequestRegiter, HisPatientSDO patientData)
			: base(param, ucServiceRequestRegiter)
		{
			_ucServiceRequestRegister = ucServiceRequestRegiter;
		}

		HisPatientProfileSDO IServiceRequestRegisterPatientProfile.Run()
		{
			base.patientProfile = new HisPatientProfileSDO();
			base.patientProfile.HisPatient = new HIS_PATIENT();
			base.patientProfile.HisPatientTypeAlter = new HIS_PATIENT_TYPE_ALTER();
			base.patientProfile.HisTreatment = new HIS_TREATMENT();
			InitBase();
			if (HisConfigCFG.IsCheckExamination && (_ucServiceRequestRegister.serviceReqDetailSDOs == null || ucRequestService.serviceReqDetailSDOs.Count <= 0))
			{
				WaitingManager.Hide();
				if (XtraMessageBox.Show(ResourceMessage.BenhNhanChuaChonCongKham, ResourceMessage.TieuDeCuaSoThongBaoLaCanhBao, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					_ucServiceRequestRegister.isShowMess = true;
					return null;
				}
				WaitingManager.Show();
			}
			result = (HisPatientProfileSDO)RunBase(base.patientProfile, ucRequestService);
			LogSystem.Debug(LogUtil.TraceData(LogUtil.GetMemberName(() => result), result));
			if (result == null)
			{
				LogSystem.Warn("Goi api dang ky tiep don that bai, Dau vao____" + LogUtil.TraceData(LogUtil.GetMemberName(() => patientProfile), base.patientProfile) + ", Dau ra____" + LogUtil.TraceData(LogUtil.GetMemberName(() => result), result) + "__" + LogUtil.TraceData(LogUtil.GetMemberName(() => param), base.param));
			}
			return result;
		}
	}
}
