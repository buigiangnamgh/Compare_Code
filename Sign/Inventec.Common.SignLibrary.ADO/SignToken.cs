using EMR.EFMODEL.DataModels;
using Inventec.Common.Integrate;

namespace Inventec.Common.SignLibrary.ADO
{
	internal class SignToken
	{
		public string TokenCode { get; set; }

		public string LoginName { get; set; }

		public string UserName { get; set; }

		public EMR_SIGNER Singer { get; set; }

		public EMR_TREATMENT Treatment { get; set; }

		public bool IsUseTimespan { get; set; }

		public string Password { get; set; }

		public TokenData TokenData { get; set; }

		public ApiConsumer EmrConsumer { get; set; }

		public string PIN { get; set; }

		internal SignToken()
		{
		}
	}
}
