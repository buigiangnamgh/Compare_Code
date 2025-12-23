namespace Inventec.Common.SignLibrary.DTO
{
	public class ImageOfPageDTO
	{
		public string Path { get; set; }

		public byte[] ImageContent { get; set; }

		public float Height { get; set; }

		public float Width { get; set; }

		public int PageNumber { get; set; }
	}
}
