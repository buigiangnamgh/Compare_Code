namespace Inventec.Common.Integrate
{
	public class ResultObject
	{
		public object Data { get; set; }

		public int? Total { get; set; }

		public bool Success { get; set; }

		public string Message { get; set; }

		public void SetValue(object resultData, string message, bool success, int? total)
		{
			Data = resultData;
			Message = message;
			Success = success;
			Total = total;
		}
	}
}
