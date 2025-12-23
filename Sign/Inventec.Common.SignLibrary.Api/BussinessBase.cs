using EMR.EFMODEL.DataModels;
using Inventec.Common.Integrate;
using Inventec.Common.SignLibrary.ADO;

namespace Inventec.Common.SignLibrary.Api
{
	internal abstract class BussinessBase
	{
		protected string deviceSignPadName = "";

		protected CommonParam param { get; set; }

		protected InputADO inputADOWorking { get; set; }

		protected bool IsSignParanel { get; set; }

		protected string Src { get; set; }

		protected EMR_TREATMENT Treatment { get; set; }

		protected EMR_SIGNER Signer { get; set; }

		protected byte[] SignPadImageData { get; set; }

		protected bool IsUsingSignPad { get; set; }

		protected string TokenCode { get; set; }

		protected FileType FileType { get; set; }

		protected bool IsUsingSignPadBefore { get; set; }

		internal BussinessBase()
		{
			param = new CommonParam();
		}

		internal BussinessBase(CommonParam paramBusiness)
		{
			param = ((paramBusiness != null) ? paramBusiness : new CommonParam());
		}

		internal BussinessBase(bool isSignParanel)
		{
			IsSignParanel = isSignParanel;
		}

		internal BussinessBase(CommonParam paramBusiness, InputADO inputADO)
		{
			param = ((paramBusiness != null) ? paramBusiness : new CommonParam());
			inputADOWorking = inputADO;
		}

		internal BussinessBase(CommonParam paramBusiness, InputADO inputADO, string src)
		{
			param = ((paramBusiness != null) ? paramBusiness : new CommonParam());
			inputADOWorking = inputADO;
			Src = src;
		}

		internal BussinessBase(CommonParam paramBusiness, InputADO inputADO, string src, bool isSignParanel)
		{
			param = ((paramBusiness != null) ? paramBusiness : new CommonParam());
			inputADOWorking = inputADO;
			Src = src;
			IsSignParanel = isSignParanel;
		}

		internal BussinessBase(CommonParam paramBusiness, InputADO inputADO, string src, bool isSignParanel, EMR_TREATMENT treatment, EMR_SIGNER singer, string tokenCode)
		{
			param = ((paramBusiness != null) ? paramBusiness : new CommonParam());
			inputADOWorking = inputADO;
			Src = src;
			IsSignParanel = isSignParanel;
			Treatment = treatment;
			Signer = singer;
			TokenCode = tokenCode;
		}
	}
}
