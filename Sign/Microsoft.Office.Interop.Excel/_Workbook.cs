using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	[ComImport]
	[TypeIdentifier]
	[Guid("000208DA-0000-0000-C000-000000000046")]
	[CompilerGenerated]
	public interface _Workbook
	{
		void _VtblGap1_20();

		[DispId(277)]
		[LCIDConversion(3)]
		void Close([Optional][In][MarshalAs(UnmanagedType.Struct)] object SaveChanges, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Filename, [Optional][In][MarshalAs(UnmanagedType.Struct)] object RouteWorkbook);

		void _VtblGap2_216();

		[DispId(2493)]
		void ExportAsFixedFormat([In] XlFixedFormatType Type, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Filename, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Quality, [Optional][In][MarshalAs(UnmanagedType.Struct)] object IncludeDocProperties, [Optional][In][MarshalAs(UnmanagedType.Struct)] object IgnorePrintAreas, [Optional][In][MarshalAs(UnmanagedType.Struct)] object From, [Optional][In][MarshalAs(UnmanagedType.Struct)] object To, [Optional][In][MarshalAs(UnmanagedType.Struct)] object OpenAfterPublish, [Optional][In][MarshalAs(UnmanagedType.Struct)] object FixedFormatExtClassPtr);
	}
}
