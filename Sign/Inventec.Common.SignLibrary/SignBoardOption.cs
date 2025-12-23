using System.ComponentModel;

namespace Inventec.Common.SignLibrary
{
	internal enum SignBoardOption
	{
		[Description("Không sử dụng tính năng bảng ký")]
		NoUse = 1,
		[Description("Có sử dụng tính năng bảng ký")]
		Use
	}
}
