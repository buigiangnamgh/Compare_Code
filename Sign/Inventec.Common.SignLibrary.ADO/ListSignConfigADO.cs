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
			((V_EMR_SIGN)this).ATTEMPT_NUMBER = data.ATTEMPT_NUMBER;
			((V_EMR_SIGN)this).CARD_CODE = data.CARD_CODE;
			((V_EMR_SIGN)this).CREATE_TIME = data.CREATE_TIME;
			((V_EMR_SIGN)this).CREATOR = data.CREATOR;
			((V_EMR_SIGN)this).DEPARTMENT_CODE = data.DEPARTMENT_CODE;
			((V_EMR_SIGN)this).DEPARTMENT_NAME = data.DEPARTMENT_NAME;
			((V_EMR_SIGN)this).DOCUMENT_ID = data.DOCUMENT_ID;
			((V_EMR_SIGN)this).FIRST_NAME = data.FIRST_NAME;
			((V_EMR_SIGN)this).FLOW_CODE = data.FLOW_CODE;
			((V_EMR_SIGN)this).FLOW_ID = data.FLOW_ID;
			((V_EMR_SIGN)this).FLOW_NAME = data.FLOW_NAME;
			((V_EMR_SIGN)this).GROUP_CODE = data.GROUP_CODE;
			((V_EMR_SIGN)this).ID = data.ID;
			((V_EMR_SIGN)this).IS_ACTIVE = data.IS_ACTIVE;
			((V_EMR_SIGN)this).IS_DELETE = data.IS_DELETE;
			((V_EMR_SIGN)this).IS_SIGNING = data.IS_SIGNING;
			((V_EMR_SIGN)this).LAST_NAME = data.LAST_NAME;
			((V_EMR_SIGN)this).LINK_CODE = data.LINK_CODE;
			((V_EMR_SIGN)this).LOGINNAME = data.LOGINNAME;
			((V_EMR_SIGN)this).MODIFIER = data.MODIFIER;
			((V_EMR_SIGN)this).MODIFY_TIME = data.MODIFY_TIME;
			((V_EMR_SIGN)this).NUM_ORDER = data.NUM_ORDER;
			((V_EMR_SIGN)this).PATIENT_CODE = data.PATIENT_CODE;
			((V_EMR_SIGN)this).PCA_SERIAL = data.PCA_SERIAL;
			((V_EMR_SIGN)this).REJECT_DATE = data.REJECT_DATE;
			((V_EMR_SIGN)this).REJECT_REASON = data.REJECT_REASON;
			((V_EMR_SIGN)this).REJECT_TIME = data.REJECT_TIME;
			((V_EMR_SIGN)this).RELATION_NAME = data.RELATION_NAME;
			((V_EMR_SIGN)this).RELATION_PEOPLE_NAME = data.RELATION_PEOPLE_NAME;
			((V_EMR_SIGN)this).ROOM_CODE = data.ROOM_CODE;
			((V_EMR_SIGN)this).ROOM_NAME = data.ROOM_NAME;
			((V_EMR_SIGN)this).ROOM_TYPE_CODE = data.ROOM_TYPE_CODE;
			((V_EMR_SIGN)this).SERVICE_CODE = data.SERVICE_CODE;
			((V_EMR_SIGN)this).SIGN_DATE = data.SIGN_DATE;
			((V_EMR_SIGN)this).SIGN_STT_ID = data.SIGN_STT_ID;
			((V_EMR_SIGN)this).SIGN_TIME = data.SIGN_TIME;
			((V_EMR_SIGN)this).TITLE = data.TITLE;
			((V_EMR_SIGN)this).USERNAME = data.USERNAME;
			((V_EMR_SIGN)this).VERSION_ID = data.VERSION_ID;
			((V_EMR_SIGN)this).VIR_PATIENT_NAME = data.VIR_PATIENT_NAME;
			SIGN_TIME_STR = (data.SIGN_TIME.HasValue ? DateTimeConvert.TimeNumberToTimeString(data.SIGN_TIME.Value) : "");
			REJECT_TIME_STR = (data.REJECT_TIME.HasValue ? DateTimeConvert.TimeNumberToTimeString(data.REJECT_TIME.Value) : "");
			if (((V_EMR_SIGN)this).FLOW_ID.HasValue)
			{
				Signer = ((V_EMR_SIGN)this).FLOW_NAME;
			}
			else if (!string.IsNullOrWhiteSpace(((V_EMR_SIGN)this).LOGINNAME))
			{
				Signer = data.LOGINNAME;
			}
			if (!string.IsNullOrWhiteSpace(data.PATIENT_CODE))
			{
				IsPatient = true;
				if (!string.IsNullOrWhiteSpace(data.RELATION_NAME))
				{
					Signer = ((!string.IsNullOrEmpty(data.CARD_CODE)) ? string.Format("{0}({1})", data.RELATION_PEOPLE_NAME, data.CARD_CODE) : string.Format("{0}", data.RELATION_PEOPLE_NAME));
					((V_EMR_SIGN)this).TITLE = data.RELATION_NAME;
				}
				else
				{
					Signer = data.VIR_PATIENT_NAME;
				}
			}
		}
	}
}
