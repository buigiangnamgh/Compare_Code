using Inventec.Common.Mapper;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
	public class DmvADO : HIS_STENT_CONCLUDE
	{
		public int Action { get; set; }

		public DmvADO()
		{
		}

		public DmvADO(HIS_STENT_CONCLUDE data)
		{
			DataObjectMapper.Map<DmvADO>((object)this, (object)data);
		}
	}
}
