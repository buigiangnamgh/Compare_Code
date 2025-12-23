using System;
using System.Collections.Generic;

namespace Inventec.Common.Integrate
{
	public static class Worker
	{
		private static Dictionary<Type, object> dic = new Dictionary<Type, object>();

		private static object thisLock = new object();

		public static object Get<T>() where T : class
		{
			Type typeFromHandle = typeof(T);
			object value = null;
			lock (thisLock)
			{
				if (!dic.TryGetValue(typeFromHandle, out value))
				{
					value = Activator.CreateInstance(typeFromHandle);
					dic.Add(typeFromHandle, value);
				}
			}
			return value;
		}
	}
}
