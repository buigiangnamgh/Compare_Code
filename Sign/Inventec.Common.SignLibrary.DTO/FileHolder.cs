using System.IO;

namespace Inventec.Common.SignLibrary.DTO
{
	public class FileHolder
	{
		public MemoryStream Content { get; set; }

		public string FileName { get; set; }

		public FileHolder()
		{
		}

		public FileHolder(MemoryStream content, string fileName)
		{
			Content = content;
			FileName = fileName;
		}
	}
}
