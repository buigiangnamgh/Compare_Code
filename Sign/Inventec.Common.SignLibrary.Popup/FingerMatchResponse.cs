using System;

namespace Inventec.Common.SignLibrary.Popup
{
	public class FingerMatchResponse
	{
		public string success { get; set; }

		public string error { get; set; }

		public string messages { get; set; }

		public DateTime? serverTime { get; set; }

		public FingerMatchResult result { get; set; }
	}
}
