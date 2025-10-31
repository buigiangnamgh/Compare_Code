using System.Collections.Generic;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
	public class EkipUsersADO
	{
		public long? idPtttMethod { get; set; }

		public long? idEkip { get; set; }

		public List<HisEkipUserADO> listEkipUser { get; set; }
	}
}
