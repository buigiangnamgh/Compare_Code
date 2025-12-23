using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Inventec.Common.SignFile.XmlProcess.Common;

namespace Inventec.Common.SignFile.XmlProcess.XmlDsig.Common
{
	public class XmlDsigSignParameters
	{
		private List<XmlPropertyDescriptor> _properties = new List<XmlPropertyDescriptor>();

		private List<Converter<XmlDocument, XmlElement>> _propertyBuilders = new List<Converter<XmlDocument, XmlElement>>();

		public bool IncludeCertificateInSignature { get; set; }

		public string InputPath { get; set; }

		public XmlDocument InputXml { get; set; }

		public string XPathNodeToSign { get; set; }

		public string PinCode { get; set; }

		public string OutputPath { get; set; }

		public GetHSMServerResponseData DlgGetHSMServerResponseData { get; set; }

		public X509Certificate2 SignatureCertificate { get; set; }

		public XmlDsigSignatureFormat SignatureFormat { get; set; }

		public bool IncludeTimestamp { get; set; }

		public List<XmlPropertyDescriptor> Properties
		{
			get
			{
				return _properties;
			}
			set
			{
				_properties = value;
			}
		}

		public List<Converter<XmlDocument, XmlElement>> PropertyBuilders
		{
			get
			{
				return _propertyBuilders;
			}
			set
			{
				_propertyBuilders = value;
			}
		}

		public override string ToString()
		{
			string[] array = new string[26]
			{
				"Parameters: ",
				Environment.NewLine,
				"\t IncludeCertificateInSignature: ",
				IncludeCertificateInSignature.ToString(),
				Environment.NewLine,
				"\t InputPath: ",
				InputPath,
				Environment.NewLine,
				"\t InputXml: ",
				InputXml.OuterXml,
				Environment.NewLine,
				"\t XPathNodeToSign: ",
				XPathNodeToSign,
				Environment.NewLine,
				"\t OutputPath: ",
				OutputPath,
				Environment.NewLine,
				"\t SignatureCertificate: ",
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null
			};
			X509Certificate2 signatureCertificate = SignatureCertificate;
			array[18] = ((signatureCertificate != null) ? signatureCertificate.ToString() : null);
			array[19] = Environment.NewLine;
			array[20] = "\t SignatureFormat: ";
			array[21] = SignatureFormat.ToString();
			array[22] = Environment.NewLine;
			array[23] = "\t IncludeTimestamp: ";
			array[24] = IncludeTimestamp.ToString();
			array[25] = Environment.NewLine;
			return string.Concat(array);
		}
	}
}
