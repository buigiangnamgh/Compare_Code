using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

namespace HIS.Desktop.Plugins.RegisterV2
{
	internal class LayoutControlUtil
	{
		internal static LayoutControlItem AddToLayout(UserControl ucControl, bool textVisible, Size size, Size textSize, SizeConstraintsType sizeConstraintsType, Size maxSize, Size minSize)
		{
			int num = 0;
			return new LayoutControlItem
			{
				Control = ucControl,
				Name = string.Format("{0}{1}", ucControl.Name, num),
				TextVisible = textVisible,
				Size = size,
				TextSize = textSize,
				SizeConstraintsType = sizeConstraintsType,
				MaxSize = maxSize,
				MinSize = minSize
			};
		}

		internal static void Move(LayoutControlItem itemMove, LayoutControlItem itemCenter, InsertType insertType)
		{
			itemMove.Move(itemCenter, insertType);
		}
	}
}
