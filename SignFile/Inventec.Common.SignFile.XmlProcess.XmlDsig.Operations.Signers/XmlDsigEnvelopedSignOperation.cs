using System.Security.Cryptography.Xml;
using System.Xml;
using Inventec.Common.SignFile.XmlProcess.Common.Exceptions;

namespace Inventec.Common.SignFile.XmlProcess.XmlDsig.Operations.Signers
{
	internal class XmlDsigEnvelopedSignOperation : XmlDsigSignOperation
	{
		protected override void CreateAndAddReferenceTo(SignedXml signedXml, XmlDocument document, string inputPath, string xpathToNodeToSign)
		{
			if (signedXml == null)
			{
				throw new InvalidParameterException("Signed Xml cannot be null");
			}
			if (xpathToNodeToSign == null)
			{
				xpathToNodeToSign = "";
			}
			Reference reference = new Reference();
			reference.Uri = xpathToNodeToSign;
			Reference reference2 = reference;
			reference2.AddTransform(new XmlDsigEnvelopedSignatureTransform());
			signedXml.AddReference(reference2);
		}

		protected override XmlDocument BuildFinalSignedXmlDocument(XmlDocument inputXml, XmlElement signatureXml)
		{
			XmlElement documentElement = inputXml.DocumentElement;
			if (documentElement != null)
			{
				documentElement.AppendChild(inputXml.ImportNode(signatureXml, true));
			}
			return inputXml;
		}
	}
}
