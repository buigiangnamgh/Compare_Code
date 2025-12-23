using EMR.TDO;
using Inventec.Common.SignLibrary.Api;

namespace Inventec.Common.SignLibrary.SignHandler
{
	internal abstract class SignHandlerBase : BussinessBase
	{
		protected float CoorXRectangle { get; set; }

		protected float CoorYRectangle { get; set; }

		protected int MaxPageNumber { get; set; }

		protected int PageNumber { get; set; }

		protected string SignReason { get; set; }

		protected bool IsPatientSign { get; set; }

		protected long? DocumentTypeId { get; set; }

		protected bool IsMultiSign { get; set; }

		protected string HisCode { get; set; }

		protected string Base64FileData { get; set; }

		protected DocumentTDO Document { get; set; }
	}
}
