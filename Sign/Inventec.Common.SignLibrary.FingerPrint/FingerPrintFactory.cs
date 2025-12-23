using System;
using System.Linq.Expressions;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;

namespace Inventec.Common.SignLibrary.FingerPrint
{
	internal class FingerPrintFactory
	{
		internal static IFingerPrint MakeISignBoard(CommonParam param, InputADO inputADOWorking, SignBoardOption signBoardOption)
		{
			IFingerPrint fingerPrint = null;
			try
			{
				switch (signBoardOption)
				{
				case SignBoardOption.NoUse:
					fingerPrint = new FingerPrintNoUseBehavior(param, inputADOWorking);
					break;
				case SignBoardOption.Use:
					fingerPrint = new FingerPrintUseBehavior(param, inputADOWorking);
					break;
				}
				if (fingerPrint == null)
				{
					throw new NullReferenceException();
				}
			}
			catch (NullReferenceException ex)
			{
				LogSystem.Error("Factory khong khoi tao duoc doi tuong." + LogUtil.TraceData(LogUtil.GetMemberName<SignBoardOption>((Expression<Func<SignBoardOption>>)(() => signBoardOption)), (object)signBoardOption), (Exception)ex);
				fingerPrint = null;
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
				fingerPrint = null;
			}
			return fingerPrint;
		}
	}
}
