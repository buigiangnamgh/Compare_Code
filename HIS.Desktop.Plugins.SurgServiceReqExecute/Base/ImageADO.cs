using System.Drawing;
using System.IO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
	public class ImageADO : HIS_SERE_SERV_FILE
	{
		public int ImageIndex { get; set; }

		public bool IsChecked { get; set; }

		public string FileName { get; set; }

		public Image IMAGE_DISPLAY { get; set; }

		public int? STTImage { get; set; }

		public Stream streamImage { get; set; }
	}
}
