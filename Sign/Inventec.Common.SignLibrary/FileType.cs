using System.ComponentModel;

namespace Inventec.Common.SignLibrary
{
	public enum FileType
	{
		[Description("Pdf")]
		Pdf,
		[Description("Xls")]
		Xls,
		[Description("Xlsx")]
		Xlsx,
		[Description("Rdlc")]
		Rdlc,
		[Description("Doc")]
		Doc,
		[Description("Docx")]
		Docx,
		[Description("Html")]
		Html,
		[Description("Rtf")]
		Rtf,
		[Description("Xml")]
		Xml,
		[Description("Json")]
		Json
	}
}
