using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Inventec.Common.Integrate
{
	internal class JsonConvertUtil
	{
		internal static string SerializeObjectJS(object data)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(data);
		}

		internal static T DeserializeObjectJS<T>(string data)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Deserialize<T>(data);
		}

		internal static string SerializeObject(object data)
		{
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			return JsonConvert.SerializeObject(data, (Formatting)1, new JsonSerializerSettings
			{
				NullValueHandling = (NullValueHandling)1
			});
		}

		internal static T DeserializeObject<T>(string data)
		{
			return JsonConvert.DeserializeObject<T>(data);
		}
	}
}
