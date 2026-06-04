using System;
using HID.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Common.Mapper;

namespace HIS.Desktop.Plugins.RegisterV2.ADO
{
	internal class PersonADO : HID_PERSON
	{
		internal PersonADO()
		{
		}

		internal PersonADO(HID_PERSON p)
		{
			try
			{
				DataObjectMapper.Map<PersonADO>(this, p);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
