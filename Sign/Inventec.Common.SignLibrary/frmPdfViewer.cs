using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EMR.EFMODEL.DataModels;
using EMR.TDO;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;

namespace Inventec.Common.SignLibrary
{
	internal class frmPdfViewer : Form
	{
		private InputADO inputADO;

		private string inputFile;

		private Stream inputStream;

		private byte[] inputByte;

		private FileType fileType;

		private bool isSignNow;

		private bool isPrintNow;

		private Action<string> actionAfterSigned;

		private UCViewer ucViewer;

		private FileADO fileADOMain = null;

		private FileADO fileADOJson = null;

		private FileADO fileADOXml = null;

		private IContainer components = null;

		private EMR_SIGNER Signer { get; set; }

		private EMR_TREATMENT Treatment { get; set; }

		private string TokenCode { get; set; }

		public frmPdfViewer(string inputFile, FileType fileType, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode)
		{
			InitializeComponent();
			this.inputADO = inputADO;
			this.inputFile = inputFile;
			this.fileType = fileType;
			Signer = signer;
			Treatment = treatment;
			TokenCode = tokenCode;
		}

		public frmPdfViewer(string inputFile, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode)
		{
			InitializeComponent();
			this.inputADO = inputADO;
			this.inputFile = inputFile;
			fileType = FileType.Pdf;
			Signer = signer;
			Treatment = treatment;
			TokenCode = tokenCode;
		}

		public frmPdfViewer(Stream inputStream, InputADO inputADO, EMR_SIGNER singer, EMR_TREATMENT treatment, string tokenCode)
		{
			InitializeComponent();
			this.inputADO = inputADO;
			this.inputStream = inputStream;
			fileType = FileType.Pdf;
			Signer = singer;
			Treatment = treatment;
			TokenCode = tokenCode;
		}

		public frmPdfViewer(byte[] inputByte, FileType fileType, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode)
		{
			InitializeComponent();
			this.inputADO = inputADO;
			this.inputByte = inputByte;
			this.fileType = fileType;
			Signer = signer;
			Treatment = treatment;
			TokenCode = tokenCode;
		}

		public frmPdfViewer(byte[] inputByte, FileType fileType, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode, bool isSignNow, Action<string> actionAfterSigned, bool printNow = false)
		{
			InitializeComponent();
			this.inputADO = inputADO;
			Signer = signer;
			Treatment = treatment;
			this.inputByte = inputByte;
			this.fileType = fileType;
			this.isSignNow = isSignNow;
			isPrintNow = printNow;
			this.actionAfterSigned = actionAfterSigned;
			TokenCode = tokenCode;
		}

		public frmPdfViewer(string inputFile, InputADO inputADO, EMR_SIGNER signer, EMR_TREATMENT treatment, string tokenCode, bool isSignNow, Action<string> actionAfterSigned, bool printNow = false)
		{
			InitializeComponent();
			this.inputADO = inputADO;
			this.inputFile = inputFile;
			fileType = FileType.Pdf;
			this.isSignNow = isSignNow;
			isPrintNow = printNow;
			Signer = signer;
			Treatment = treatment;
			this.actionAfterSigned = actionAfterSigned;
			TokenCode = tokenCode;
		}

		public void UpdateExtFileType(FileADO _fileADOMain, FileADO _fileADOJson, FileADO _fileADOXml)
		{
			fileADOMain = _fileADOMain;
			fileADOJson = _fileADOJson;
			fileADOXml = _fileADOXml;
		}

		private void frmPdfViewer_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(inputFile))
			{
				ucViewer = new UCViewer(inputFile, fileType, inputADO, Signer, Treatment, TokenCode, CloseFormProcess, isSignNow, isPrintNow, CloseFormAfterSign);
				ucViewer.UpdateExtFileType(fileADOMain, fileADOJson, fileADOXml);
				ucViewer.Dock = DockStyle.Fill;
				base.Controls.Add(ucViewer);
			}
			else if (inputStream != null && inputStream.Length > 0)
			{
				ucViewer = new UCViewer(inputStream, inputADO, Signer, Treatment, TokenCode);
				ucViewer.UpdateExtFileType(fileADOMain, fileADOJson, fileADOXml);
				ucViewer.Dock = DockStyle.Fill;
				base.Controls.Add(ucViewer);
			}
			else if (inputByte != null && inputByte.Length != 0)
			{
				ucViewer = new UCViewer(inputByte, fileType, inputADO, Signer, Treatment, TokenCode, CloseFormProcess, isSignNow, isPrintNow, CloseFormAfterSign);
				ucViewer.UpdateExtFileType(fileADOMain, fileADOJson, fileADOXml);
				ucViewer.Dock = DockStyle.Fill;
				base.Controls.Add(ucViewer);
			}
		}

		internal DocumentTDO GetCurrentDocument()
		{
			return (ucViewer != null) ? ucViewer.GetCurrentDocument() : null;
		}

		private void CloseFormProcess(string outputFile)
		{
			if (actionAfterSigned != null)
			{
				actionAfterSigned(outputFile);
			}
			Close();
		}

		private void CloseFormAfterSign(bool signed)
		{
			Close();
		}

		private void DisposeVariable()
		{
			try
			{
				LogSystem.Debug("frmPdfViewer.DisposeVariable.1");
				try
				{
					if (ucViewer != null)
					{
						try
						{
							if (GetCurrentDocument() != null)
							{
								inputADO.DocumentCode = GetCurrentDocument().DocumentCode;
							}
						}
						catch (Exception ex)
						{
							LogSystem.Warn(ex);
						}
						ucViewer.DisposeVariable(null, null);
					}
					ucViewer = null;
				}
				catch (Exception ex2)
				{
					LogSystem.Warn(ex2);
				}
				Dispose(true);
				LogSystem.Debug("frmPdfViewer.DisposeVariable.2");
			}
			catch (Exception ex3)
			{
				LogSystem.Warn(ex3);
			}
			try
			{
				inputADO = null;
				inputFile = null;
				if (inputStream != null)
				{
					inputStream.Close();
				}
				inputStream = null;
				inputByte = null;
				actionAfterSigned = null;
				Signer = null;
				Treatment = null;
				TokenCode = null;
			}
			catch (Exception ex4)
			{
				LogSystem.Warn(ex4);
			}
		}

		private void frmPdfViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				DisposeVariable();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(879, 521);
			base.Name = "frmPdfViewer";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Văn bản điện tử";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmPdfViewer_FormClosing);
			base.Load += new System.EventHandler(frmPdfViewer_Load);
			base.ResumeLayout(false);
		}
	}
}
