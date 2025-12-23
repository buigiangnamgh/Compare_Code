using System.ComponentModel;

namespace Inventec.Common.SignLibrary
{
	public enum OptionPrintType
	{
		[Description("Print with DevLib")]
		DevLib,
		[Description("Print with AposePdf")]
		PdfAposeLib,
		[Description("Print with call Exe print service")]
		CallExeLib
	}
}
