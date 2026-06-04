using System;
using System.Configuration;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.RegisterV2.Config
{
	internal class GateAndStepCFG
	{
		private static string value = ConfigurationSettings.AppSettings["HIS.Desktop.Plugins.RegisterV2.GateAndStep"];

		private static string gate_Number = null;

		private static string step_Number = null;

		public static string GateNumber
		{
			get
			{
				if (gate_Number == null)
				{
					gate_Number = GetValue(0);
				}
				return gate_Number;
			}
			set
			{
				gate_Number = value;
			}
		}

		public static string StepNumber
		{
			get
			{
				if (step_Number == null)
				{
					step_Number = GetValue(1);
				}
				return step_Number;
			}
			set
			{
				step_Number = value;
			}
		}

		private static string GetValue(int index)
		{
			string result = "";
			try
			{
				if (!string.IsNullOrEmpty(value))
				{
					string[] array = value.Split(':');
					if (array != null && array.Length > 1)
					{
						result = array[index];
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				result = "";
			}
			return result;
		}
	}
}
