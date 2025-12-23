namespace Inventec.Common.SignLibrary
{
	internal class InvalidReasonADO
	{
		private string reasonEnLang;

		private string reasonVnLang;

		private string status;

		public string ReasonEnLang
		{
			get
			{
				return reasonEnLang;
			}
			set
			{
				reasonEnLang = value;
			}
		}

		public string ReasonVnLang
		{
			get
			{
				return reasonVnLang;
			}
			set
			{
				reasonVnLang = value;
			}
		}

		public string Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}
	}
}
