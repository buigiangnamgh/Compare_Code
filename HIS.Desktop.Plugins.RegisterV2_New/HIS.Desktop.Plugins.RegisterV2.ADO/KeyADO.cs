using System.Collections.Generic;

namespace HIS.Desktop.Plugins.RegisterV2.ADO
{
	public class KeyADO
	{
		public virtual List<KeyADO> lstKeyADO { get; set; }

		public string Key { get; set; }

		public string Details { get; set; }
	}
}
