using System;

namespace Inventec.Common.Integrate
{
	public class TokenData
	{
		public DateTime ExpireTime { get; set; }

		public DateTime LastAccessTime { get; set; }

		public string LoginAddress { get; set; }

		public DateTime LoginTime { get; set; }

		public string MachineName { get; set; }

		public string RenewCode { get; set; }

		public string TokenCode { get; set; }

		public UserData User { get; set; }

		public string VersionApp { get; set; }
	}
}
