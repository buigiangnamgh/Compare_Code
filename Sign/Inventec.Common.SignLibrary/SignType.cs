using System.ComponentModel;

namespace Inventec.Common.SignLibrary
{
	public enum SignType
	{
		[Description("Ký sử dụng USB Token")]
		USB = 1,
		[Description("Ký sử dụng HSM server")]
		HMS = 2,
		[Description("Ký sử dụng HSM server")]
		HSM = 2,
		[Description("Chọn loại ký, mặc đinh ký Usb token")]
		OptionDefaultUsb = 3,
		[Description("Chọn loại ký, mặc đinh ký Hsm")]
		OptionDefaultHsm = 4
	}
}
