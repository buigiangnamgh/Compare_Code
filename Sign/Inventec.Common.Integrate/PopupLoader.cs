using System;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace Inventec.Common.Integrate
{
	public class PopupLoader
	{
		public static void SelectFirstRowPopup(LookUpEdit cbo)
		{
			try
			{
				if (cbo != null && cbo.IsPopupOpen)
				{
					PopupLookUpEditForm popupLookUpEditForm = ((IPopupControl)cbo).PopupWindow as PopupLookUpEditForm;
					if (popupLookUpEditForm != null)
					{
						popupLookUpEditForm.SelectedIndex = 0;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public static void SelectFirstRowPopup(GridLookUpEdit cbo)
		{
			try
			{
				if (cbo != null && cbo.IsPopupOpen)
				{
					cbo.Properties.View.FocusedRowHandle = 0;
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
