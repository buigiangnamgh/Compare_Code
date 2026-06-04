using System;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RegisterV2.ADO
{
	public class ExecuteRoomADO
	{
		public string EXECUTE_ROOM_NAME_1 { get; set; }

		public long TOTAL_OPEN_1 { get; set; }

		public long TOTAL_TODAY_1 { get; set; }

		public long MAX_BY_DAY_1 { get; set; }

		public string AMOUNT_1 { get; set; }

		public string EXECUTE_ROOM_NAME_2 { get; set; }

		public long TOTAL_OPEN_2 { get; set; }

		public long TOTAL_TODAY_2 { get; set; }

		public long MAX_BY_DAY_2 { get; set; }

		public string AMOUNT_2 { get; set; }

		public string EXECUTE_ROOM_NAME_3 { get; set; }

		public long TOTAL_OPEN_3 { get; set; }

		public long TOTAL_TODAY_3 { get; set; }

		public long MAX_BY_DAY_3 { get; set; }

		public string AMOUNT_3 { get; set; }

		public string EXECUTE_ROOM_NAME_4 { get; set; }

		public long TOTAL_OPEN_4 { get; set; }

		public long TOTAL_TODAY_4 { get; set; }

		public long MAX_BY_DAY_4 { get; set; }

		public string AMOUNT_4 { get; set; }

		public string EXECUTE_ROOM_NAME_5 { get; set; }

		public long TOTAL_OPEN_5 { get; set; }

		public long TOTAL_TODAY_5 { get; set; }

		public long MAX_BY_DAY_5 { get; set; }

		public string AMOUNT_5 { get; set; }

		public void SetValueRoom(V_HIS_EXECUTE_ROOM_1 data, int num)
		{
			try
			{
				if (data != null)
				{
					switch (num)
					{
					case 1:
						EXECUTE_ROOM_NAME_1 = data.EXECUTE_ROOM_NAME;
						TOTAL_OPEN_1 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ.GetValueOrDefault());
						TOTAL_TODAY_1 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ.GetValueOrDefault());
						MAX_BY_DAY_1 = data.MAX_REQUEST_BY_DAY.GetValueOrDefault();
						AMOUNT_1 = TOTAL_OPEN_1 + "/" + TOTAL_TODAY_1 + "(" + MAX_BY_DAY_1 + ")";
						break;
					case 2:
						EXECUTE_ROOM_NAME_2 = data.EXECUTE_ROOM_NAME;
						TOTAL_OPEN_2 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ.GetValueOrDefault());
						TOTAL_TODAY_2 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ.GetValueOrDefault());
						MAX_BY_DAY_2 = data.MAX_REQUEST_BY_DAY.GetValueOrDefault();
						AMOUNT_2 = TOTAL_OPEN_2 + "/" + TOTAL_TODAY_2 + "(" + MAX_BY_DAY_2 + ")";
						break;
					case 3:
						EXECUTE_ROOM_NAME_3 = data.EXECUTE_ROOM_NAME;
						TOTAL_OPEN_3 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ.GetValueOrDefault());
						TOTAL_TODAY_3 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ.GetValueOrDefault());
						MAX_BY_DAY_3 = data.MAX_REQUEST_BY_DAY.GetValueOrDefault();
						AMOUNT_3 = TOTAL_OPEN_3 + "/" + TOTAL_TODAY_3 + "(" + MAX_BY_DAY_3 + ")";
						break;
					case 4:
						EXECUTE_ROOM_NAME_4 = data.EXECUTE_ROOM_NAME;
						TOTAL_OPEN_4 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ.GetValueOrDefault());
						TOTAL_TODAY_4 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ.GetValueOrDefault());
						MAX_BY_DAY_4 = data.MAX_REQUEST_BY_DAY.GetValueOrDefault();
						AMOUNT_4 = TOTAL_OPEN_4 + "/" + TOTAL_TODAY_4 + "(" + MAX_BY_DAY_4 + ")";
						break;
					case 5:
						EXECUTE_ROOM_NAME_5 = data.EXECUTE_ROOM_NAME;
						TOTAL_OPEN_5 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ.GetValueOrDefault());
						TOTAL_TODAY_5 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ.GetValueOrDefault());
						MAX_BY_DAY_5 = data.MAX_REQUEST_BY_DAY.GetValueOrDefault();
						AMOUNT_5 = TOTAL_OPEN_5 + "/" + TOTAL_TODAY_5 + "(" + MAX_BY_DAY_5 + ")";
						break;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}
	}
}
