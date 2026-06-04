using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
	public class EditorLoaderProcessor
	{
		internal static void InitComboCommon(Control cboEditor, object data, string valueMember, string displayMember, string displayMemberCode)
		{
			try
			{
				InitComboCommon(cboEditor, data, valueMember, displayMember, 0, displayMemberCode, 0);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		internal static void InitComboCommon(Control cboEditor, object data, string valueMember, string displayMember, int displayMemberWidth, string displayMemberCode, int displayMemberCodeWidth)
		{
			try
			{
				int num = 0;
				List<ColumnInfo> list = new List<ColumnInfo>();
				if (!string.IsNullOrEmpty(displayMemberCode))
				{
					list.Add(new ColumnInfo(displayMemberCode, "", (displayMemberCodeWidth > 0) ? displayMemberCodeWidth : 100, 1));
					num += ((displayMemberCodeWidth > 0) ? displayMemberCodeWidth : 100);
				}
				if (!string.IsNullOrEmpty(displayMember))
				{
					list.Add(new ColumnInfo(displayMember, "", (displayMemberWidth > 0) ? displayMemberWidth : 250, 2));
					num += ((displayMemberWidth > 0) ? displayMemberWidth : 250);
				}
				ControlEditorADO controlEditorADO = new ControlEditorADO(displayMember, valueMember, list, false, num);
				ControlEditorLoader.Load(cboEditor, data, controlEditorADO);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}
	}
}
