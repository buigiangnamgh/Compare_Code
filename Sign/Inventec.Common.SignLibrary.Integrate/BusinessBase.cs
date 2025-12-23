using System;
using Inventec.Common.Integrate;

namespace Inventec.Common.SignLibrary.Integrate
{
	internal abstract class BusinessBase : EntityBaseAdapter
	{
		protected CommonParam param { get; set; }

		internal BusinessBase()
		{
			param = new CommonParam();
			try
			{
			}
			catch (Exception)
			{
			}
		}

		internal BusinessBase(CommonParam paramBusiness)
		{
			param = ((paramBusiness != null) ? paramBusiness : new CommonParam());
			try
			{
			}
			catch (Exception)
			{
			}
		}
	}
}
