using System;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Common.Mapper;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
	public class AcsUserADO : ACS_USER
	{
		public string DEPARTMENT_NAME { get; set; }

		public string DOB { get; set; }

		public string DIPLOMA { get; set; }

		public string DOB_STR { get; set; }

		public string DEPARTMENT_CODE { get; set; }

		public long? DEPARTMENT_ID { get; set; }

		public AcsUserADO()
		{
		}

		public AcsUserADO(ACS_USER data)
		{
			try
			{
				if (data != null)
				{
					DataObjectMapper.Map<AcsUserADO>((object)this, (object)data);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		public AcsUserADO(V_HIS_EMPLOYEE data)
		{
			try
			{
				if (data != null)
				{
					DataObjectMapper.Map<AcsUserADO>((object)this, (object)data);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}
	}
}
