using System;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.Integrate;

namespace Inventec.Common.SignLibrary.SignBoard
{
	internal class SignBoardNoUseBehavior : BusinessBase, ISignBoard
	{
		private InputADO entity;

		internal SignBoardNoUseBehavior(CommonParam param, InputADO inputADOWorking)
		{
			entity = inputADOWorking;
		}

		byte[] ISignBoard.Run()
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
