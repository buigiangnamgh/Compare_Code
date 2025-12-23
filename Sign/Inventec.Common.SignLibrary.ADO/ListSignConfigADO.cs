using EMR.EFMODEL.DataModels;
using Inventec.Common.Integrate;

namespace Inventec.Common.SignLibrary.ADO
{
	internal class ListSignConfigADO : V_EMR_SIGN
	{
		public int Action { get; set; }

		public string SIGN_TIME_STR { get; set; }

		public string REJECT_TIME_STR { get; set; }

		public long? DEPARTMENT_ID { get; set; }

		public bool IsPatient { get; set; }

		public long IdRow { get; set; }

		public object Signer { get; set; }

		public ListSignConfigADO()
		{
		}

		public ListSignConfigADO(V_EMR_SIGN data)
		{
			if (data == null)
			{
				return;
			}
			base.ATTEMPT_NUMBER = data.ATTEMPT_NUMBER;
			base.CARD_CODE = data.CARD_CODE;
			base.CREATE_TIME = data.CREATE_TIME;
			base.CREATOR = data.CREATOR;
			base.DEPARTMENT_CODE = data.DEPARTMENT_CODE;
			base.DEPARTMENT_NAME = data.DEPARTMENT_NAME;
			base.DOCUMENT_ID = data.DOCUMENT_ID;
			base.FIRST_NAME = data.FIRST_NAME;
			base.FLOW_CODE = data.FLOW_CODE;
			base.FLOW_ID = data.FLOW_ID;
			base.FLOW_NAME = data.FLOW_NAME;
			base.GROUP_CODE = data.GROUP_CODE;
			base.ID = data.ID;
			base.IS_ACTIVE = data.IS_ACTIVE;
			base.IS_DELETE = data.IS_DELETE;
			base.IS_SIGNING = data.IS_SIGNING;
			base.LAST_NAME = data.LAST_NAME;
			base.LINK_CODE = data.LINK_CODE;
			base.LOGINNAME = data.LOGINNAME;
			base.MODIFIER = data.MODIFIER;
			base.MODIFY_TIME = data.MODIFY_TIME;
			base.NUM_ORDER = data.NUM_ORDER;
			base.PATIENT_CODE = data.PATIENT_CODE;
			base.PCA_SERIAL = data.PCA_SERIAL;
			base.REJECT_DATE = data.REJECT_DATE;
			base.REJECT_REASON = data.REJECT_REASON;
			base.REJECT_TIME = data.REJECT_TIME;
			base.RELATION_NAME = data.RELATION_NAME;
			base.RELATION_PEOPLE_NAME = data.RELATION_PEOPLE_NAME;
			base.ROOM_CODE = data.ROOM_CODE;
			base.ROOM_NAME = data.ROOM_NAME;
			base.ROOM_TYPE_CODE = data.ROOM_TYPE_CODE;
			base.SERVICE_CODE = data.SERVICE_CODE;
			base.SIGN_DATE = data.SIGN_DATE;
			base.SIGN_STT_ID = data.SIGN_STT_ID;
			base.SIGN_TIME = data.SIGN_TIME;
			base.TITLE = data.TITLE;
			base.USERNAME = data.USERNAME;
			base.VERSION_ID = data.VERSION_ID;
			base.VIR_PATIENT_NAME = data.VIR_PATIENT_NAME;
			SIGN_TIME_STR = (data.SIGN_TIME.HasValue ? DateTimeConvert.TimeNumberToTimeString(data.SIGN_TIME.Value) : "");
			REJECT_TIME_STR = (data.REJECT_TIME.HasValue ? DateTimeConvert.TimeNumberToTimeString(data.REJECT_TIME.Value) : "");
			if (base.FLOW_ID.HasValue)
			{
				Signer = base.FLOW_NAME;
			}
			else if (!string.IsNullOrWhiteSpace(base.LOGINNAME))
			{
				Signer = data.LOGINNAME;
			}
			if (!string.IsNullOrWhiteSpace(data.PATIENT_CODE))
			{
				IsPatient = true;
				if (!string.IsNullOrWhiteSpace(data.RELATION_NAME))
				{
					Signer = ((!string.IsNullOrEmpty(data.CARD_CODE)) ? string.Format("{0}({1})", data.RELATION_PEOPLE_NAME, data.CARD_CODE) : string.Format("{0}", data.RELATION_PEOPLE_NAME));
					base.TITLE = data.RELATION_NAME;
				}
				else
				{
					Signer = data.VIR_PATIENT_NAME;
				}
			}
		}
	}
}
