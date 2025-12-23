namespace Inventec.Common.SignLibrary.ADO
{
	public class DocumentSignedResultDTO
	{
		public bool Success { get; set; }

		public string Message { get; set; }

		public string Base64FileSigned { get; set; }

		public string DocumentCode { get; set; }
	}
}
