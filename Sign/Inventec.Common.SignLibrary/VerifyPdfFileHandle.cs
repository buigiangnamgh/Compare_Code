using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.X509;

namespace Inventec.Common.SignLibrary
{
	internal class VerifyPdfFileHandle
	{
		internal VerifyPdfFileHandle()
		{
		}

		internal List<VerifierADO> verify(PdfReader reader)
		{
			List<VerifierADO> list = new List<VerifierADO>();
			AcroFields acroFields = reader.AcroFields;
			foreach (string signatureName in acroFields.GetSignatureNames())
			{
				PdfPKCS7 pdfPKCS = acroFields.VerifySignature(signatureName);
				DateTime signDate = pdfPKCS.SignDate;
				Org.BouncyCastle.X509.X509Certificate[] signCertificateChain = pdfPKCS.SignCertificateChain;
				if (signCertificateChain == null || signCertificateChain.Length == 0)
				{
					return null;
				}
				Org.BouncyCastle.X509.X509Certificate x509Certificate = signCertificateChain[0];
				X509Certificate2 x509Certificate2 = new X509Certificate2();
				x509Certificate2.Import(x509Certificate.GetEncoded());
				string nameInfo = x509Certificate2.GetNameInfo(X509NameType.DnsName, false);
				string location = pdfPKCS.Location;
				VerifierADO verifierADO = new VerifierADO(x509Certificate, x509Certificate2, nameInfo, signDate, !pdfPKCS.Verify(), location);
				verifierADO.Comment = pdfPKCS.Reason;
				verifierADO.Location = pdfPKCS.Location;
				verifierADO.SignerSerialNumber = x509Certificate.SerialNumber.ToString(16);
				verifierADO.SignerDN = x509Certificate.SubjectDN.ToString();
				verifierADO.IsserDN = x509Certificate.IssuerDN.ToString();
				verifierADO.NotAfter = x509Certificate.NotAfter;
				verifierADO.NotBefore = x509Certificate.NotBefore;
				verifierADO.KeyLength = x509Certificate2.PublicKey.Key.KeySize;
				VerifierADO verifierADO2 = verifierADO;
				verifierADO2.SubjectDN = new SubjectDNADO(verifierADO2.SignerDN);
				if (x509Certificate2.Verify())
				{
					verifierADO2.Valid = true;
				}
				else
				{
					verifierADO2.Valid = false;
				}
				list.Add(verifierADO2);
			}
			return list;
		}

		internal List<VerifierADO> verify(string fileName)
		{
			PdfReader reader = new PdfReader(fileName);
			return verify(reader);
		}

		internal List<VerifierADO> verify(Stream stream)
		{
			PdfReader reader = new PdfReader(stream);
			return verify(reader);
		}
	}
}
