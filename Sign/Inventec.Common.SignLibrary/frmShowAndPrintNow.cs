using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraPdfViewer.Bars;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Inventec.Common.SignLibrary
{
	public class frmShowAndPrintNow : Form
	{
		private string outputFile;

		private PdfReader readerWorking;

		private List<VerifierADO> verifiers;

		private InputADO inputADO;

		private Stream currentStream;

		private short printNumberCopies;

		private string printFilePath = "";

		private string outputPdfPathTemp;

		private string outputPdfPath = "";

		private IContainer components = null;

		private PdfViewer pdfViewer1;

		private BarManager barManager1;

		private PdfCommandBar pdfCommandBar1;

		private BarButtonItem bbtnPrint;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private PdfZoom10CheckItem pdfZoom10CheckItem1;

		private PdfZoom25CheckItem pdfZoom25CheckItem1;

		private PdfZoom50CheckItem pdfZoom50CheckItem1;

		private PdfZoom75CheckItem pdfZoom75CheckItem1;

		private PdfZoom100CheckItem pdfZoom100CheckItem1;

		private PdfZoom125CheckItem pdfZoom125CheckItem1;

		private PdfZoom150CheckItem pdfZoom150CheckItem1;

		private PdfZoom200CheckItem pdfZoom200CheckItem1;

		private PdfZoom400CheckItem pdfZoom400CheckItem1;

		private PdfZoom500CheckItem pdfZoom500CheckItem1;

		private PdfSetActualSizeZoomModeCheckItem pdfSetActualSizeZoomModeCheckItem1;

		private PdfSetPageLevelZoomModeCheckItem pdfSetPageLevelZoomModeCheckItem1;

		private PdfSetFitWidthZoomModeCheckItem pdfSetFitWidthZoomModeCheckItem1;

		private PdfSetFitVisibleZoomModeCheckItem pdfSetFitVisibleZoomModeCheckItem1;

		private BarStaticItem bbtnConfigBussinessMenu;

		private BarStaticItem bbtnAttackMentsMenu;

		private BarButtonItem bbtnConfigBussinessMenu1;

		private BarSubItem barSubItem1;

		private BarButtonItem bbtnAttackMentsMenu1;

		private RepositoryItemCheckEdit repositoryItemCheckEdit1;

		public frmShowAndPrintNow(string outputFile, InputADO inputADO, short printNumberCopies)
		{
			InitializeComponent();
			this.outputFile = outputFile;
			this.inputADO = inputADO;
			this.printNumberCopies = printNumberCopies;
		}

		private void frmShowAndPrintNow1_Load(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(outputFile))
				{
					readerWorking = new PdfReader(outputFile);
					int num = 1;
					num = readerWorking.NumberOfPages;
					iTextSharp.text.Rectangle pageSizeWithRotation = readerWorking.GetPageSizeWithRotation(readerWorking.NumberOfPages);
					int numberOfPages = readerWorking.NumberOfPages;
					outputPdfPathTemp = Utils.GenerateTempFileWithin();
					outputPdfPath = "";
					ProcessInsertSignInformationPage(outputPdfPathTemp, ref outputPdfPath, ref num);
					printFilePath = "";
					if (!string.IsNullOrEmpty(outputPdfPath) && File.Exists(outputPdfPath))
					{
						printFilePath = outputPdfPath;
					}
					else
					{
						printFilePath = outputFile;
					}
					pdfViewer1.DetachStreamAfterLoadComplete = true;
					pdfViewer1.LoadDocument(printFilePath);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void ProcessInsertSignInformationPage(string outputPdfPathTemp, ref string outputPdfPath, ref int pageCount)
		{
			try
			{
				if (inputADO.IsPrintOnlyContent || verifiers == null || verifiers.Count <= 0)
				{
					return;
				}
				FileStream fileStream = File.Open(outputPdfPathTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				Document document = new Document(readerWorking.GetPageSizeWithRotation(readerWorking.NumberOfPages));
				PdfWriter instance = PdfWriter.GetInstance(document, fileStream);
				document.Open();
				PdfPTable element = AddPdfPTable();
				document.Add(element);
				document.Close();
				List<int> list = new List<int>();
				for (int i = 0; i <= readerWorking.NumberOfPages; i++)
				{
					list.Add(i);
				}
				outputPdfPath = Utils.GenerateTempFileWithin();
				currentStream = File.Open(outputPdfPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				PdfConcatenate pdfConcatenate = new PdfConcatenate(currentStream);
				PdfReader pdfReader = null;
				if (!string.IsNullOrEmpty(outputFile))
				{
					pdfReader = new PdfReader(outputFile);
				}
				pdfReader.SelectPages(list);
				pdfConcatenate.AddPages(pdfReader);
				pdfReader.Close();
				pdfReader = new PdfReader(outputPdfPathTemp);
				pdfReader.SelectPages(new List<int> { 0, 1 });
				pdfConcatenate.AddPages(pdfReader);
				try
				{
					fileStream.Close();
				}
				catch
				{
				}
				try
				{
					pdfReader.Close();
				}
				catch
				{
				}
				try
				{
					pdfConcatenate.Close();
				}
				catch
				{
				}
				try
				{
					readerWorking.Close();
				}
				catch
				{
				}
				try
				{
					if (File.Exists(outputPdfPathTemp))
					{
						File.Delete(outputPdfPathTemp);
					}
				}
				catch
				{
				}
				readerWorking = new PdfReader(outputPdfPath);
				pageCount++;
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private PdfPTable AddPdfPTable()
		{
			PdfPTable pdfPTable = new PdfPTable(7);
			pdfPTable.SetTotalWidth(new float[7] { 7f, 20f, 20f, 15f, 30f, 30f, 30f });
			iTextSharp.text.Font font = new iTextSharp.text.Font(Utils.GetBaseFont(), 9f, 0);
			iTextSharp.text.Font font2 = new iTextSharp.text.Font(Utils.GetBaseFont(), 9f, 1);
			PdfPCell pdfPCell = new PdfPCell();
			pdfPCell.AddElement(new Paragraph("STT", font2));
			pdfPTable.AddCell(pdfPCell);
			PdfPCell pdfPCell2 = new PdfPCell();
			pdfPCell2.AddElement(new Paragraph("Người ký 1", font2));
			pdfPTable.AddCell(pdfPCell2);
			PdfPCell pdfPCell3 = new PdfPCell();
			pdfPCell3.AddElement(new Paragraph("Thời gian ký", font2));
			pdfPTable.AddCell(pdfPCell3);
			PdfPCell pdfPCell4 = new PdfPCell();
			pdfPCell4.AddElement(new Paragraph("Hạn CT", font2));
			pdfPTable.AddCell(pdfPCell4);
			PdfPCell pdfPCell5 = new PdfPCell();
			pdfPCell5.AddElement(new Paragraph("Đơn vị", font2));
			pdfPTable.AddCell(pdfPCell5);
			PdfPCell pdfPCell6 = new PdfPCell();
			pdfPCell6.AddElement(new Paragraph("Chức danh 1", font2));
			pdfPTable.AddCell(pdfPCell6);
			PdfPCell pdfPCell7 = new PdfPCell();
			pdfPCell7.AddElement(new Paragraph("Ý kiến của người ký", font2));
			pdfPTable.AddCell(pdfPCell7);
			int num = 1;
			if (verifiers != null && verifiers.Count > 0)
			{
				foreach (VerifierADO verifier in verifiers)
				{
					PdfPCell pdfPCell8 = new PdfPCell();
					pdfPCell8.AddElement(new Chunk(num.ToString(), font));
					pdfPTable.AddCell(pdfPCell8);
					PdfPCell pdfPCell9 = new PdfPCell();
					pdfPCell9.AddElement(new Chunk(verifier.SignerName, font));
					pdfPTable.AddCell(pdfPCell9);
					PdfPCell pdfPCell10 = new PdfPCell();
					pdfPCell10.AddElement(new Chunk(verifier.Date.ToString("dd/MM/yyyy HH:mm:ss"), font));
					pdfPTable.AddCell(pdfPCell10);
					PdfPCell pdfPCell11 = new PdfPCell();
					pdfPCell11.AddElement(new Chunk(verifier.NotAfter.ToString("dd/MM/yyyy"), font));
					pdfPTable.AddCell(pdfPCell11);
					string content = "";
					string content2 = "";
					if (!string.IsNullOrEmpty(verifier.Location))
					{
						string[] array = verifier.Location.Split(new string[1] { "|" }, StringSplitOptions.None);
						if (array.Length == 2)
						{
							content = array[0];
							content2 = array[1];
						}
					}
					PdfPCell pdfPCell12 = new PdfPCell();
					pdfPCell12.AddElement(new Chunk(content, font));
					pdfPTable.AddCell(pdfPCell12);
					PdfPCell pdfPCell13 = new PdfPCell();
					pdfPCell13.AddElement(new Chunk(content2, font));
					pdfPTable.AddCell(pdfPCell13);
					PdfPCell pdfPCell14 = new PdfPCell();
					pdfPCell14.AddElement(new Chunk(verifier.Comment, font));
					pdfPTable.AddCell(pdfPCell14);
					num++;
				}
			}
			return pdfPTable;
		}

		private bool VerifyPdfInputFile(ref string message)
		{
			VerifyPdfFileHandle verifyPdfFileHandle = new VerifyPdfFileHandle();
			try
			{
				verifiers = (from o in verifyPdfFileHandle.verify(readerWorking)
					orderby o.Date
					select o).ToList();
			}
			catch
			{
				verifiers = null;
			}
			if (verifiers == null)
			{
				message = "File cần xác thực không hợp lệ";
				return false;
			}
			if (verifiers.Count == 0)
			{
				message = "File đã được chọn không tìm thấy chữ ký số";
				return false;
			}
			return true;
		}

		private void bbtnPrint_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				PrintLibProcess.SimplePrint(printFilePath, printNumberCopies, inputADO.PrinterDefault, inputADO.PaperSizeDefault);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventec.Common.SignLibrary.frmShowAndPrintNow));
			this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
			this.barManager1 = new DevExpress.XtraBars.BarManager();
			this.pdfCommandBar1 = new DevExpress.XtraPdfViewer.Bars.PdfCommandBar();
			this.bbtnPrint = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.pdfZoom10CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom10CheckItem();
			this.pdfZoom25CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom25CheckItem();
			this.pdfZoom50CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom50CheckItem();
			this.pdfZoom75CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom75CheckItem();
			this.pdfZoom100CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom100CheckItem();
			this.pdfZoom125CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom125CheckItem();
			this.pdfZoom150CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom150CheckItem();
			this.pdfZoom200CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom200CheckItem();
			this.pdfZoom400CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom400CheckItem();
			this.pdfZoom500CheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfZoom500CheckItem();
			this.pdfSetActualSizeZoomModeCheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfSetActualSizeZoomModeCheckItem();
			this.pdfSetPageLevelZoomModeCheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfSetPageLevelZoomModeCheckItem();
			this.pdfSetFitWidthZoomModeCheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfSetFitWidthZoomModeCheckItem();
			this.pdfSetFitVisibleZoomModeCheckItem1 = new DevExpress.XtraPdfViewer.Bars.PdfSetFitVisibleZoomModeCheckItem();
			this.bbtnConfigBussinessMenu = new DevExpress.XtraBars.BarStaticItem();
			this.bbtnAttackMentsMenu = new DevExpress.XtraBars.BarStaticItem();
			this.bbtnConfigBussinessMenu1 = new DevExpress.XtraBars.BarButtonItem();
			this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
			this.bbtnAttackMentsMenu1 = new DevExpress.XtraBars.BarButtonItem();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).BeginInit();
			base.SuspendLayout();
			this.pdfViewer1.DetachStreamAfterLoadComplete = true;
			this.pdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pdfViewer1.Location = new System.Drawing.Point(0, 31);
			this.pdfViewer1.Name = "pdfViewer1";
			this.pdfViewer1.Size = new System.Drawing.Size(912, 489);
			this.pdfViewer1.TabIndex = 0;
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.pdfCommandBar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[20]
			{
				this.pdfZoom10CheckItem1, this.pdfZoom25CheckItem1, this.pdfZoom50CheckItem1, this.pdfZoom75CheckItem1, this.pdfZoom100CheckItem1, this.pdfZoom125CheckItem1, this.pdfZoom150CheckItem1, this.pdfZoom200CheckItem1, this.pdfZoom400CheckItem1, this.pdfZoom500CheckItem1,
				this.pdfSetActualSizeZoomModeCheckItem1, this.pdfSetPageLevelZoomModeCheckItem1, this.pdfSetFitWidthZoomModeCheckItem1, this.pdfSetFitVisibleZoomModeCheckItem1, this.bbtnPrint, this.bbtnConfigBussinessMenu, this.bbtnAttackMentsMenu, this.bbtnConfigBussinessMenu1, this.barSubItem1, this.bbtnAttackMentsMenu1
			});
			this.barManager1.MaxItemId = 54;
			this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[1] { this.repositoryItemCheckEdit1 });
			this.pdfCommandBar1.BarName = "";
			this.pdfCommandBar1.Control = this.pdfViewer1;
			this.pdfCommandBar1.DockCol = 0;
			this.pdfCommandBar1.DockRow = 0;
			this.pdfCommandBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.pdfCommandBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnPrint)
			});
			this.pdfCommandBar1.Offset = 1;
			this.pdfCommandBar1.Text = "";
			this.bbtnPrint.Caption = "In";
			this.bbtnPrint.Glyph = (System.Drawing.Image)resources.GetObject("bbtnPrint.Glyph");
			this.bbtnPrint.Id = 29;
			this.bbtnPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.P | System.Windows.Forms.Keys.Control);
			this.bbtnPrint.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnPrint.LargeGlyph");
			this.bbtnPrint.Name = "bbtnPrint";
			this.bbtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnPrint_ItemClick);
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(912, 31);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 520);
			this.barDockControlBottom.Size = new System.Drawing.Size(912, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 489);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(912, 31);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 489);
			this.pdfZoom10CheckItem1.Id = 10;
			this.pdfZoom10CheckItem1.Name = "pdfZoom10CheckItem1";
			this.pdfZoom25CheckItem1.Id = 11;
			this.pdfZoom25CheckItem1.Name = "pdfZoom25CheckItem1";
			this.pdfZoom50CheckItem1.Id = 12;
			this.pdfZoom50CheckItem1.Name = "pdfZoom50CheckItem1";
			this.pdfZoom75CheckItem1.Id = 13;
			this.pdfZoom75CheckItem1.Name = "pdfZoom75CheckItem1";
			this.pdfZoom100CheckItem1.Id = 14;
			this.pdfZoom100CheckItem1.Name = "pdfZoom100CheckItem1";
			this.pdfZoom125CheckItem1.Id = 15;
			this.pdfZoom125CheckItem1.Name = "pdfZoom125CheckItem1";
			this.pdfZoom150CheckItem1.Id = 16;
			this.pdfZoom150CheckItem1.Name = "pdfZoom150CheckItem1";
			this.pdfZoom200CheckItem1.Id = 17;
			this.pdfZoom200CheckItem1.Name = "pdfZoom200CheckItem1";
			this.pdfZoom400CheckItem1.Id = 18;
			this.pdfZoom400CheckItem1.Name = "pdfZoom400CheckItem1";
			this.pdfZoom500CheckItem1.Id = 19;
			this.pdfZoom500CheckItem1.Name = "pdfZoom500CheckItem1";
			this.pdfSetActualSizeZoomModeCheckItem1.Id = 20;
			this.pdfSetActualSizeZoomModeCheckItem1.Name = "pdfSetActualSizeZoomModeCheckItem1";
			this.pdfSetPageLevelZoomModeCheckItem1.Id = 21;
			this.pdfSetPageLevelZoomModeCheckItem1.Name = "pdfSetPageLevelZoomModeCheckItem1";
			this.pdfSetFitWidthZoomModeCheckItem1.Id = 22;
			this.pdfSetFitWidthZoomModeCheckItem1.Name = "pdfSetFitWidthZoomModeCheckItem1";
			this.pdfSetFitVisibleZoomModeCheckItem1.Id = 23;
			this.pdfSetFitVisibleZoomModeCheckItem1.Name = "pdfSetFitVisibleZoomModeCheckItem1";
			this.bbtnConfigBussinessMenu.Caption = "Thiết lập nghiệp vụ ký";
			this.bbtnConfigBussinessMenu.Glyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu.Glyph");
			this.bbtnConfigBussinessMenu.Id = 48;
			this.bbtnConfigBussinessMenu.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu.LargeGlyph");
			this.bbtnConfigBussinessMenu.Name = "bbtnConfigBussinessMenu";
			this.bbtnConfigBussinessMenu.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bbtnAttackMentsMenu.Caption = "Tập tin đính kèm";
			this.bbtnAttackMentsMenu.Glyph = (System.Drawing.Image)resources.GetObject("bbtnAttackMentsMenu.Glyph");
			this.bbtnAttackMentsMenu.Id = 49;
			this.bbtnAttackMentsMenu.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnAttackMentsMenu.LargeGlyph");
			this.bbtnAttackMentsMenu.Name = "bbtnAttackMentsMenu";
			this.bbtnAttackMentsMenu.TextAlignment = System.Drawing.StringAlignment.Near;
			this.bbtnConfigBussinessMenu1.Caption = "Thiết lập nghiệp vụ ký";
			this.bbtnConfigBussinessMenu1.Glyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu1.Glyph");
			this.bbtnConfigBussinessMenu1.Id = 51;
			this.bbtnConfigBussinessMenu1.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnConfigBussinessMenu1.LargeGlyph");
			this.bbtnConfigBussinessMenu1.Name = "bbtnConfigBussinessMenu1";
			this.barSubItem1.Caption = "barSubItem1";
			this.barSubItem1.Id = 52;
			this.barSubItem1.Name = "barSubItem1";
			this.bbtnAttackMentsMenu1.Caption = "Tập tin đính kèm";
			this.bbtnAttackMentsMenu1.Glyph = (System.Drawing.Image)resources.GetObject("bbtnAttackMentsMenu1.Glyph");
			this.bbtnAttackMentsMenu1.Id = 53;
			this.bbtnAttackMentsMenu1.LargeGlyph = (System.Drawing.Image)resources.GetObject("bbtnAttackMentsMenu1.LargeGlyph");
			this.bbtnAttackMentsMenu1.Name = "bbtnAttackMentsMenu1";
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(912, 520);
			base.Controls.Add(this.pdfViewer1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmShowAndPrintNow";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "In ngay";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new System.EventHandler(frmShowAndPrintNow1_Load);
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
