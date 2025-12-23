using System;

namespace Inventec.Common.Integrate
{
	public class ApiResultObject<T> : Result
	{
		public T Data { get; set; }

		public ApiResultObject()
		{
		}

		public ApiResultObject(T data)
		{
			Data = data;
		}

		public ApiResultObject(T data, bool success)
		{
			Data = data;
			base.Success = success;
		}

		public void SetValue(T data, bool success, CommonParam param)
		{
			Data = data;
			base.Success = success;
			base.Param = param;
		}

		public ResultObject ConvertToResultObject()
		{
			ResultObject resultObject = new ResultObject();
			try
			{
				resultObject.Data = Data;
				resultObject.Success = base.Success;
				resultObject.Total = ((base.Param != null) ? base.Param.Count : new int?(0));
			}
			catch (Exception)
			{
				resultObject = new ResultObject();
			}
			return resultObject;
		}
	}
}
