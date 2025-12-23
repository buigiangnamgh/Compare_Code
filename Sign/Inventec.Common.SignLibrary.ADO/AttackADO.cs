using EMR.EFMODEL.DataModels;

namespace Inventec.Common.SignLibrary.ADO
{
	public class AttackADO : EMR_ATTACHMENT
	{
		public string FILE_NAME { get; set; }

		public string Base64Data { get; set; }

		public long DocumentId { get; set; }

		public string Extension { get; set; }

		public string FullName { get; set; }
	}
}
