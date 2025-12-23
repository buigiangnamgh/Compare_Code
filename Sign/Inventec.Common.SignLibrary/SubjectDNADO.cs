using System;

namespace Inventec.Common.SignLibrary
{
	internal class SubjectDNADO
	{
		public string ST { get; set; }

		public string E { get; set; }

		public string C { get; set; }

		public string O { get; set; }

		public string OU { get; set; }

		public string L { get; set; }

		public string CN { get; set; }

		public SubjectDNADO()
		{
		}

		public SubjectDNADO(string inputSubjectDN)
		{
			try
			{
				if (string.IsNullOrEmpty(inputSubjectDN))
				{
					return;
				}
				string[] array = inputSubjectDN.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
				if (array == null || array.Length == 0)
				{
					return;
				}
				string[] array2 = array;
				foreach (string text in array2)
				{
					if (string.IsNullOrEmpty(text))
					{
						continue;
					}
					string[] array3 = text.Split(new string[1] { "=" }, StringSplitOptions.None);
					if (array3 != null && array3.Length > 1)
					{
						if (array3[0] == "C")
						{
							C = array3[1];
						}
						else if (array3[0] == "ST")
						{
							ST = array3[1];
						}
						else if (array3[0] == "L")
						{
							L = array3[1];
						}
						else if (array3[0] == "O")
						{
							O = array3[1];
						}
						else if (array3[0] == "CN")
						{
							CN = array3[1];
						}
						else if (array3[0] == "E")
						{
							E = array3[1];
						}
						else if (array3[0] == "OU")
						{
							OU = array3[1];
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
