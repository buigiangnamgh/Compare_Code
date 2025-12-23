using System;
using System.Linq.Expressions;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;

namespace Inventec.Common.SignLibrary.SignBoard
{
	internal class SignBoardFactory
	{
		internal static ISignBoard MakeISignBoard(CommonParam param, InputADO inputADOWorking, SignBoardOption signBoardOption)
		{
			ISignBoard signBoard = null;
			try
			{
				switch (signBoardOption)
				{
				case SignBoardOption.NoUse:
					signBoard = new SignBoardNoUseBehavior(param, inputADOWorking);
					break;
				case SignBoardOption.Use:
					signBoard = new SignBoardUseBehavior(param, inputADOWorking);
					break;
				}
				if (signBoard == null)
				{
					throw new NullReferenceException();
				}
			}
			catch (NullReferenceException ex)
			{
				LogSystem.Error("Factory khong khoi tao duoc doi tuong." + LogUtil.TraceData(LogUtil.GetMemberName<SignBoardOption>((Expression<Func<SignBoardOption>>)(() => signBoardOption)), (object)signBoardOption), (Exception)ex);
				signBoard = null;
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
				signBoard = null;
			}
			return signBoard;
		}
	}
}
