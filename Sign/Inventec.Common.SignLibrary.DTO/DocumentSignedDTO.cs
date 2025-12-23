namespace Inventec.Common.SignLibrary.DTO
{
	public class DocumentSignedDTO
	{
		public string DocumentTypeCode { get; set; }

		public string HisCode { get; set; }

		public string TreatmentCode { get; set; }

		public bool? IsPrintOnlyContent { get; set; }
	}
}
