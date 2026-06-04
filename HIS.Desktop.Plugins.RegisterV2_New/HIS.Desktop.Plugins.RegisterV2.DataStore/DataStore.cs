using System;
using System.Collections.Generic;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RegisterV2.DataStore
{
	public class DataStore
	{
		internal static List<HIS_TRAN_PATI_FORM> TranPatiForms { get; set; }

		internal static List<HIS_ICD> Icds { get; set; }

		public static void LoadDataStore()
		{
			try
			{
				TranPatiForms = BackendDataWorker.Get<HIS_TRAN_PATI_FORM>();
				Icds = BackendDataWorker.Get<HIS_ICD>();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
