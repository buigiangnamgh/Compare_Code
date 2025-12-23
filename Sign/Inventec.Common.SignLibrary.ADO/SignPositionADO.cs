using System.Collections.Generic;
using Inventec.Common.SignFile;
using iTextSharp.text;

namespace Inventec.Common.SignLibrary.ADO
{
	internal class SignPositionADO
	{
		public Rectangle Reactanle { get; set; }

		public int PageNUm { get; set; }

		public string Text { get; set; }

		public int TypeDisplay { get; set; }

		public bool? IsDisplaySignature { get; set; }

		public int SizeFont { get; set; }

		public float WidthRectangle { get; set; }

		public float HeightRectangle { get; set; }

		public Constans.TEXT_POSITON TextPosition { get; set; }

		public string Signer { get; set; }

		public List<SignPositionADO> SignPositionAutos { get; set; }
	}
}
