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
				string[] array3 = array2;
				foreach (string text in array3)
				{
					if (string.IsNullOrEmpty(text))
					{
						continue;
					}
					string[] array4 = text.Split(new string[1] { "=" }, StringSplitOptions.None);
					if (array4 != null && array4.Length > 1)
					{
						if (array4[0] == "C")
						{
							C = array4[1];
						}
						else if (array4[0] == "ST")
						{
							ST = array4[1];
						}
						else if (array4[0] == "L")
						{
							L = array4[1];
						}
						else if (array4[0] == "O")
						{
							O = array4[1];
						}
						else if (array4[0] == "CN")
						{
							CN = array4[1];
						}
						else if (array4[0] == "E")
						{
							E = array4[1];
						}
						else if (array4[0] == "OU")
						{
							OU = array4[1];
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
