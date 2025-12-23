using System;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Integrate;

namespace Inventec.Common.SignLibrary.FingerPrint
{
	internal class FingerPrintNoUseBehavior : BusinessBase, IFingerPrint
	{
		private InputADO entity;

		internal FingerPrintNoUseBehavior(CommonParam param, InputADO inputADOWorking)
		{
			entity = inputADOWorking;
		}

		byte[] IFingerPrint.Run()
		{
			try
			{
				return null;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				base.param.HasException = true;
				return null;
			}
		}
	}
}
