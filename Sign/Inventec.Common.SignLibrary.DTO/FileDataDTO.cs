using System.IO;

namespace Inventec.Common.SignLibrary.DTO
{
	public class FileDataDTO
	{
		public byte[] BFile { get; set; }

		public int Size { get; set; }

		public MemoryStream Stream { get; set; }
	}
}
