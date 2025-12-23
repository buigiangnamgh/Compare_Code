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
				PdfPKCS7 val = acroFields.VerifySignature(signatureName);
				DateTime signDate = val.SignDate;
				X509Certificate[] signCertificateChain = val.SignCertificateChain;
				if (signCertificateChain == null || signCertificateChain.Length == 0)
				{
					return null;
				}
				X509Certificate val2 = signCertificateChain[0];
				X509Certificate2 x509Certificate = new X509Certificate2();
				x509Certificate.Import(val2.GetEncoded());
				string nameInfo = x509Certificate.GetNameInfo(X509NameType.DnsName, false);
				string location = val.Location;
				VerifierADO verifierADO = new VerifierADO(val2, x509Certificate, nameInfo, signDate, !val.Verify(), location)
				{
					Comment = val.Reason,
					Location = val.Location,
					SignerSerialNumber = val2.SerialNumber.ToString(16),
					SignerDN = ((object)val2.SubjectDN).ToString(),
					IsserDN = ((object)val2.IssuerDN).ToString(),
					NotAfter = val2.NotAfter,
					NotBefore = val2.NotBefore,
					KeyLength = x509Certificate.PublicKey.Key.KeySize
				};
				verifierADO.SubjectDN = new SubjectDNADO(verifierADO.SignerDN);
				if (x509Certificate.Verify())
				{
					verifierADO.Valid = true;
				}
				else
				{
					verifierADO.Valid = false;
				}
				list.Add(verifierADO);
			}
			return list;
		}

		internal List<VerifierADO> verify(string fileName)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			PdfReader reader = new PdfReader(fileName);
			return verify(reader);
		}

		internal List<VerifierADO> verify(Stream stream)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Expected O, but got Unknown
			PdfReader reader = new PdfReader(stream);
			return verify(reader);
		}
	}
}
