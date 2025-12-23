using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.X509;

namespace Inventec.Common.SignLibrary
{
	internal class VerifierADO
	{
		private string comment;

		private string location;

		private DateTime date;

		private List<InvalidReasonADO> invalidReasonList;

		private string isserDN;

		private string keyAlgorithm;

		private int keyLength;

		private bool modified;

		private DateTime notAfter;

		private DateTime notBefore;

		private string signerDN;

		private string signerLocation;

		private string signerName;

		private string signerOrganization;

		private string signerOrganizationUnit;

		private string signerSerialNumber;

		private bool valid;

		private X509Certificate2 x509Cert2;

		private X509Certificate x509CertBC;

		public SubjectDNADO SubjectDN { get; set; }

		public string Comment
		{
			get
			{
				return comment;
			}
			set
			{
				comment = value;
			}
		}

		public string Location
		{
			get
			{
				return location;
			}
			set
			{
				location = value;
			}
		}

		public DateTime Date
		{
			get
			{
				return date;
			}
			set
			{
				date = value;
			}
		}

		public List<InvalidReasonADO> InvalidReasonList
		{
			get
			{
				return invalidReasonList;
			}
			set
			{
				invalidReasonList = value;
			}
		}

		public string IsserDN
		{
			get
			{
				return isserDN;
			}
			set
			{
				isserDN = value;
			}
		}

		public string KeyAlgorithm
		{
			get
			{
				return keyAlgorithm;
			}
			set
			{
				keyAlgorithm = value;
			}
		}

		public int KeyLength
		{
			get
			{
				return keyLength;
			}
			set
			{
				keyLength = value;
			}
		}

		public bool Modified
		{
			get
			{
				return modified;
			}
			set
			{
				modified = value;
			}
		}

		public DateTime NotAfter
		{
			get
			{
				return notAfter;
			}
			set
			{
				notAfter = value;
			}
		}

		public DateTime NotBefore
		{
			get
			{
				return notBefore;
			}
			set
			{
				notBefore = value;
			}
		}

		public string SignerDN
		{
			get
			{
				return signerDN;
			}
			set
			{
				signerDN = value;
			}
		}

		public string SignerLocation
		{
			get
			{
				return signerLocation;
			}
			set
			{
				signerLocation = value;
			}
		}

		public string SignerName
		{
			get
			{
				return signerName;
			}
			set
			{
				signerName = value;
			}
		}

		public string SignerOrganization
		{
			get
			{
				return signerOrganization;
			}
			set
			{
				signerOrganization = value;
			}
		}

		public string SignerOrganizationUnit
		{
			get
			{
				return signerOrganizationUnit;
			}
			set
			{
				signerOrganizationUnit = value;
			}
		}

		public string SignerSerialNumber
		{
			get
			{
				return signerSerialNumber;
			}
			set
			{
				signerSerialNumber = value;
			}
		}

		public bool Valid
		{
			get
			{
				return valid;
			}
			set
			{
				valid = value;
			}
		}

		public X509Certificate2 X509Cert2
		{
			get
			{
				return x509Cert2;
			}
			set
			{
				x509Cert2 = value;
			}
		}

		public X509Certificate X509CertBC
		{
			get
			{
				return x509CertBC;
			}
			set
			{
				x509CertBC = value;
			}
		}

		public VerifierADO(X509Certificate cert, X509Certificate2 cert2, string signerName, DateTime date, bool modified, string location)
		{
			x509Cert2 = cert2;
			x509CertBC = cert;
			this.signerName = signerName;
			signerOrganization = "";
			signerOrganizationUnit = "";
			signerLocation = location;
			this.modified = modified;
			valid = false;
			comment = "";
			this.date = date;
			keyLength = 1024;
		}
	}
}
