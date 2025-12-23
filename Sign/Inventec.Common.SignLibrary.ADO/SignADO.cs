using EMR.TDO;

namespace Inventec.Common.SignLibrary.ADO
{
	public class SignADO
	{
		public DocumentTDO Document { get; set; }

		public float X { get; set; }

		public float Y { get; set; }

		public int PageNumberCurrent { get; set; }

		public int TotalPageNumber { get; set; }
	}
}
