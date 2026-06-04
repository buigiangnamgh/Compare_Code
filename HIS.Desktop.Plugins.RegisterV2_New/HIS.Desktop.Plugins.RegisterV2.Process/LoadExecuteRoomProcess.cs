using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using HIS.Desktop.Plugins.RegisterV2.ADO;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;

namespace HIS.Desktop.Plugins.RegisterV2.Process
{
	internal class LoadExecuteRoomProcess
	{
		internal static List<ExecuteRoomADO> listAdo = new List<ExecuteRoomADO>();

		private System.Windows.Forms.Timer timerLoadExecuteRoom;

		internal LoadExecuteRoomProcess()
		{
			try
			{
				timerLoadExecuteRoom = new System.Windows.Forms.Timer();
				int value = 300000;
				if (AppConfigs.DangKyTiepDonThoiGianLoadDanhSachPhongKham > 0)
				{
					value = (int)AppConfigs.DangKyTiepDonThoiGianLoadDanhSachPhongKham;
				}
				timerLoadExecuteRoom.Interval = Convert.ToInt32(value);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void timerLoadExecuteRoom_Tick()
		{
			try
			{
				CreateThreadLoadExecuteRoom();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void LoadDataExecuteRoomInfo()
		{
			try
			{
				HisExecuteRoomView1Filter hisExecuteRoomView1Filter = new HisExecuteRoomView1Filter();
				hisExecuteRoomView1Filter.IS_EXAM = true;
				hisExecuteRoomView1Filter.BRANCH_ID = WorkPlace.GetBranchId();
				List<V_HIS_EXECUTE_ROOM_1> listData = new BackendAdapter(new CommonParam()).Get<List<V_HIS_EXECUTE_ROOM_1>>("api/HisExecuteRoom/GetView1", ApiConsumers.MosConsumer, hisExecuteRoomView1Filter, null);
				listData = GetListRoom(listData);
				if (listData == null || listData.Count <= 0)
				{
					return;
				}
				listData = listData.OrderBy((V_HIS_EXECUTE_ROOM_1 o) => o.EXECUTE_ROOM_CODE).ToList();
				listAdo.Clear();
				int num = 5;
				int num2 = 0;
				for (int num3 = listData.Count; num3 > 0; num3 -= num)
				{
					int count = ((num3 <= num) ? num3 : num);
					List<V_HIS_EXECUTE_ROOM_1> list = listData.Skip(num2).Take(count).ToList();
					int num4 = 1;
					ExecuteRoomADO executeRoomADO = new ExecuteRoomADO();
					foreach (V_HIS_EXECUTE_ROOM_1 item in list)
					{
						executeRoomADO.SetValueRoom(item, num4);
						num4++;
					}
					listAdo.Add(executeRoomADO);
					num2 += num;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		internal void CreateThreadLoadExecuteRoom()
		{
			try
			{
				Thread thread = new Thread(new ThreadStart(LoadDataExecuteRoomInfo));
				try
				{
					thread.Start();
				}
				catch (Exception ex)
				{
					LogSystem.Error(ex);
					thread.Abort();
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
			}
		}

		private List<V_HIS_EXECUTE_ROOM_1> GetListRoom(List<V_HIS_EXECUTE_ROOM_1> listData)
		{
			List<V_HIS_EXECUTE_ROOM_1> list = new List<V_HIS_EXECUTE_ROOM_1>();
			try
			{
				if (listData == null || listData.Count <= 0)
				{
					return list;
				}
				if (HisConfigCFG.ExecuteRoomShow != null && HisConfigCFG.ExecuteRoomShow.Count > 0)
				{
					foreach (string item in HisConfigCFG.ExecuteRoomShow)
					{
						if (!string.IsNullOrWhiteSpace(item))
						{
							V_HIS_EXECUTE_ROOM_1 v_HIS_EXECUTE_ROOM_ = listData.FirstOrDefault((V_HIS_EXECUTE_ROOM_1 o) => o.EXECUTE_ROOM_CODE == item);
							if (v_HIS_EXECUTE_ROOM_ != null)
							{
								list.Add(v_HIS_EXECUTE_ROOM_);
							}
						}
					}
				}
				else
				{
					list = listData;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				list = new List<V_HIS_EXECUTE_ROOM_1>();
			}
			return list;
		}
	}
}
