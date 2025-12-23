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
			return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			});
		}

		internal static T DeserializeObject<T>(string data)
		{
			return JsonConvert.DeserializeObject<T>(data);
		}
	}
}
