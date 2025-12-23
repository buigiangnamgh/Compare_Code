using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using EMR.SDO;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;
using Inventec.Common.SignLibrary.DTO;
using Inventec.Common.SignLibrary.Integrate;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Inventec.Common.SignLibrary
{
	public class frmAttachMents : Form
	{
		private string DocumentCode;

		private string LoginName;

		private V_EMR_DOCUMENT Document;

		private string[] fullfileNameAttack;

		private AttackADO fileNameAttack;

		private List<AttackADO> ListfileNameAttack = new List<AttackADO>();

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControl layoutControl2;

		private SimpleButton btnSave;

		private SimpleButton btnChooseFile;

		private LayoutControl layoutControl3;

		private GridControl gridControl2;

		private GridView gridView2;

		private GridColumn gridColumn4;

		private GridColumn gridColumn5;

		private RepositoryItemButtonEdit btnG_DELETE;

		private GridColumn gridColumn6;

		private GridColumn gridColumn7;

		private GridColumn gridColumn8;

		private LayoutControlGroup layoutControlGroup2;

		private LayoutControlItem layoutControlItem4;

		private GridControl gridControl1;

		private GridView gridView1;

		private GridColumn gridColumn1;

		private GridColumn gridColumn2;

		private RepositoryItemButtonEdit btnGDELETE;

		private GridColumn gridColumn3;

		private LayoutControlGroup Root;

		private LayoutControlItem layoutControlItem2;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem5;

		private LayoutControlItem layoutControlItem6;

		private EmptySpaceItem emptySpaceItem1;

		private EmptySpaceItem emptySpaceItem2;

		private LayoutControlItem layoutControlItem1;

		private BarManager barManager1;

		private Bar bar2;

		private BarButtonItem bbtnChooseFile;

		private BarButtonItem bbtnSave;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		public frmAttachMents()
		{
			InitializeComponent();
		}

		public frmAttachMents(string document)
		{
			InitializeComponent();
			try
			{
				DocumentCode = document;
				LoginName = GlobalStore.LoginName;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void frmAttachMents_Load(object sender, EventArgs e)
		{
			try
			{
				LoadKeysFromlanguage();
				SetDefaultData();
				loadgridView2(Document.ID);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void SetDefaultData()
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			try
			{
				CommonParam commonParam = new CommonParam();
				EmrDocumentViewFilter val = new EmrDocumentViewFilter();
				val.DOCUMENT_CODE__EXACT = DocumentCode;
				List<V_EMR_DOCUMENT> list = GlobalStore.EmrConsumer.Get<List<V_EMR_DOCUMENT>>("api/EmrDocument/GetView", commonParam, val, new object[0]);
				if (list != null && list.Count > 0)
				{
					Document = list.FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void LoadKeysFromlanguage()
		{
		}

		private void loadgridView2(long documentID)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Expected O, but got Unknown
			try
			{
				CommonParam commonParam = new CommonParam();
				EmrAttachmentFilter val = new EmrAttachmentFilter();
				((FilterBase)val).ORDER_DIRECTION = "ASC";
				((FilterBase)val).ORDER_FIELD = "NUM_ORDER";
				val.DOCUMENT_ID = documentID;
				List<EMR_ATTACHMENT> dataSource = GlobalStore.EmrConsumer.Get<List<EMR_ATTACHMENT>>("api/EmrAttachment/Get", commonParam, val, new object[0]);
				WaitingManager.Hide();
				gridView2.BeginUpdate();
				gridView2.GridControl.DataSource = null;
				gridView2.GridControl.DataSource = dataSource;
				gridView2.EndUpdate();
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
		}

		private void gridView2_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			try
			{
				EMR_ATTACHMENT val = (EMR_ATTACHMENT)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
				if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound && val != null)
				{
					if (e.Column.FieldName == "STT")
					{
						e.Value = e.ListSourceRowIndex + 1;
					}
					else if (e.Column.FieldName == "CREATE_TIME_STR")
					{
						try
						{
							e.Value = DateTimeConvert.TimeNumberToTimeString(val.CREATE_TIME.GetValueOrDefault());
						}
						catch (Exception ex)
						{
							LogSystem.Error(ex);
						}
					}
				}
				gridControl2.RefreshDataSource();
			}
			catch (Exception ex2)
			{
				LogSystem.Error(ex2);
			}
		}

		private void btnG_DELETE_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			try
			{
				CommonParam commonParam = new CommonParam();
				EMR_ATTACHMENT val = (EMR_ATTACHMENT)gridView2.GetFocusedRow();
				if (MessageBox.Show("Bạn có muốn xóa dữ liệu", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes && val != null)
				{
					bool flag = false;
					flag = GlobalStore.EmrConsumer.Post<bool>("api/EmrAttachment/Delete", commonParam, val.ID, new object[0]);
					if (flag)
					{
						loadgridView2(Document.ID);
					}
					MessageManager.Show(this, commonParam, flag);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnChooseFile_Click(object sender, EventArgs e)
		{
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Multiselect = true;
				openFileDialog.Filter = "Ảnh(*.jpg, *.Png, *.jpeg, *.bmp)|*.jpg;*.png;*.jpeg;*.bmp|pdf(*.pdf)|*.pdf";
				openFileDialog.DefaultExt = ".jpg;.png;.jpeg;.bmp";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					fullfileNameAttack = openFileDialog.FileNames;
					if (fullfileNameAttack != null)
					{
						string[] array = fullfileNameAttack;
						foreach (string text in array)
						{
							int num = text.LastIndexOf("\\");
							int num2 = text.LastIndexOf(".");
							fileNameAttack = new AttackADO();
							fileNameAttack.FILE_NAME = text.Substring((num > 0) ? (num + 1) : num);
							((EMR_ATTACHMENT)fileNameAttack).EXTENSION = text.Substring((num2 > 0) ? (num2 + 1) : num2);
							string extension = Path.GetExtension(text);
							if ((extension ?? "").ToLower() == ".pdf")
							{
								string joinPdfFilePath = "";
								PdfReader val = new PdfReader(text);
								float oginalHeight = val.GetPageSize(1).Height;
								PdfDocumentProcess.SplitOnePageToImageAndJoinToNewOnePdf(text, oginalHeight, ref joinPdfFilePath);
								LogSystem.Debug("joinPdfPathFile:" + joinPdfFilePath);
								fileNameAttack.Base64Data = Utils.FileToBase64String(joinPdfFilePath);
								fileNameAttack.FullName = joinPdfFilePath;
							}
							else
							{
								fileNameAttack.FullName = text;
								fileNameAttack.Base64Data = Utils.FileToBase64String(text);
							}
							ListfileNameAttack.Add(fileNameAttack);
						}
					}
				}
				gridView1.BeginUpdate();
				gridView1.GridControl.DataSource = ListfileNameAttack;
				gridView1.EndUpdate();
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			try
			{
				if (!e.IsGetData || e.Column.UnboundType == UnboundColumnType.Bound)
				{
					return;
				}
				AttackADO attackADO = (AttackADO)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
				if (attackADO != null)
				{
					if (e.Column.FieldName == "STT")
					{
						e.Value = e.ListSourceRowIndex + 1;
					}
					else if (e.Column.FieldName == "FILE_NAME")
					{
						e.Value = attackADO.FILE_NAME;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnGDELETE_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				AttackADO item = (AttackADO)gridView1.GetFocusedRow();
				if (MessageBox.Show("Bạn có muốn xóa dữ liệu không", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					ListfileNameAttack.Remove(item);
					gridView1.BeginUpdate();
					gridView1.GridControl.DataSource = ((ListfileNameAttack != null) ? ListfileNameAttack.ToList() : null);
					gridView1.EndUpdate();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			try
			{
				List<AttackADO> list = new List<AttackADO>();
				if (ListfileNameAttack != null && ListfileNameAttack.Count > 0)
				{
					string text = "";
					foreach (AttackADO item in ListfileNameAttack)
					{
						EmrAttachmentSDO data = new EmrAttachmentSDO();
						data.DocumentId = Document.ID;
						data.Extension = ((EMR_ATTACHMENT)item).EXTENSION;
						data.Base64Data = item.Base64Data;
						data.AttachmentName = item.FILE_NAME;
						string output = GeneratePdfFileFromImage(item.FullName);
						FileHolder fileHolder = new FileHolder();
						fileHolder.FileName = output;
						fileHolder.Content = GetMemoryStreamFileData(output);
						if (data == null)
						{
							continue;
						}
						CommonParam commonParam = new CommonParam();
						EMR_ATTACHMENT apiData = GlobalStore.EmrConsumer.PostWithFile<EMR_ATTACHMENT>("api/EmrAttachment/CreateWithFile", commonParam, data, new List<FileHolder> { fileHolder }, new object[0]);
						if (apiData == null)
						{
							list.Add(item);
							text = text + data.AttachmentName + ",";
							LogSystem.Debug("Goi api tao van ban " + ((apiData != null) ? "thanh cong" : "that bai") + "____Du lieu dau vao:" + LogUtil.TraceData(LogUtil.GetMemberName<EmrAttachmentSDO>((Expression<Func<EmrAttachmentSDO>>)(() => data)), (object)data) + "____" + LogUtil.TraceData(LogUtil.GetMemberName<string>((Expression<Func<string>>)(() => output)), (object)output) + "____Ket qua tra ve:" + LogUtil.TraceData(LogUtil.GetMemberName<EMR_ATTACHMENT>((Expression<Func<EMR_ATTACHMENT>>)(() => apiData)), (object)apiData) + "___" + LogUtil.TraceData(LogUtil.GetMemberName<CommonParam>((Expression<Func<CommonParam>>)(() => commonParam)), (object)commonParam));
						}
					}
					ListfileNameAttack = list;
					gridView1.BeginUpdate();
					gridView1.GridControl.DataSource = ListfileNameAttack;
					gridView1.EndUpdate();
					loadgridView2(Document.ID);
					if (!string.IsNullOrEmpty(text))
					{
						MessageBox.Show("Các tập tin đính kèm sau không lưu được: " + text);
					}
				}
				else
				{
					MessageBox.Show("Bạn chưa chọn tập tin đính kèm");
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private MemoryStream GetMemoryStreamFileData(string outFile)
		{
			MemoryStream memoryStream = null;
			try
			{
				if (!string.IsNullOrEmpty(outFile))
				{
					memoryStream = new MemoryStream();
					using (FileStream fileStream = new FileStream(outFile, FileMode.Open, FileAccess.Read))
					{
						byte[] buffer = new byte[fileStream.Length];
						fileStream.Read(buffer, 0, (int)fileStream.Length);
						memoryStream.Write(buffer, 0, (int)fileStream.Length);
					}
					memoryStream.Position = 0L;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
				memoryStream = null;
			}
			return memoryStream;
		}

		private string GeneratePdfFileFromImage(string filename)
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Expected O, but got Unknown
			try
			{
				Image instance = Image.GetInstance(Image.FromFile(filename), BaseColor.BLACK);
				using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					Document val = new Document((Rectangle)(object)instance);
					try
					{
						PdfWriter instance2 = PdfWriter.GetInstance(val, (Stream)fileStream);
						try
						{
							val.Open();
							instance.SetAbsolutePosition(0f, 0f);
							instance2.DirectContent.AddImage(instance);
							val.Close();
						}
						finally
						{
							if (instance2 != null)
							{
								((IDisposable)instance2).Dispose();
							}
						}
					}
					finally
					{
						if (val != null)
						{
							((IDisposable)val).Dispose();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
			return filename;
		}

		private void bbtnChooseFile_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				btnChooseFile_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void bbtnSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				btnSave_Click(null, null);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventec.Common.SignLibrary.frmAttachMents));
			DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceHovered = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearancePressed = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceDisabled = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceHovered2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearancePressed2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject appearanceDisabled2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.btnChooseFile = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
			this.gridControl2 = new DevExpress.XtraGrid.GridControl();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnG_DELETE = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnGDELETE = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.barManager1 = new DevExpress.XtraBars.BarManager();
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.bbtnChooseFile = new DevExpress.XtraBars.BarButtonItem();
			this.bbtnSave = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControl2).BeginInit();
			this.layoutControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControl3).BeginInit();
			this.layoutControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.gridControl2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.btnG_DELETE).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.btnGDELETE).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.layoutControl2);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 22);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(878, 527);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.layoutControl2.Controls.Add(this.btnSave);
			this.layoutControl2.Controls.Add(this.btnChooseFile);
			this.layoutControl2.Controls.Add(this.layoutControl3);
			this.layoutControl2.Controls.Add(this.gridControl1);
			this.layoutControl2.Location = new System.Drawing.Point(2, 2);
			this.layoutControl2.Name = "layoutControl2";
			this.layoutControl2.Root = this.Root;
			this.layoutControl2.Size = new System.Drawing.Size(874, 523);
			this.layoutControl2.TabIndex = 4;
			this.layoutControl2.Text = "layoutControl2";
			this.btnSave.Location = new System.Drawing.Point(717, 17);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(155, 22);
			this.btnSave.StyleController = this.layoutControl2;
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Lưu (Ctrl S)";
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.btnChooseFile.Location = new System.Drawing.Point(561, 17);
			this.btnChooseFile.Name = "btnChooseFile";
			this.btnChooseFile.Size = new System.Drawing.Size(152, 22);
			this.btnChooseFile.StyleController = this.layoutControl2;
			this.btnChooseFile.TabIndex = 6;
			this.btnChooseFile.Text = "Chọn tập tin (Ctrl C)";
			this.btnChooseFile.Click += new System.EventHandler(btnChooseFile_Click);
			this.layoutControl3.Controls.Add(this.gridControl2);
			this.layoutControl3.Location = new System.Drawing.Point(2, 263);
			this.layoutControl3.Name = "layoutControl3";
			this.layoutControl3.Root = this.layoutControlGroup2;
			this.layoutControl3.Size = new System.Drawing.Size(870, 258);
			this.layoutControl3.TabIndex = 5;
			this.layoutControl3.Text = "layoutControl3";
			this.gridControl2.Location = new System.Drawing.Point(2, 2);
			this.gridControl2.MainView = this.gridView2;
			this.gridControl2.Name = "gridControl2";
			this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[1] { this.btnG_DELETE });
			this.gridControl2.Size = new System.Drawing.Size(866, 254);
			this.gridControl2.TabIndex = 4;
			this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView2 });
			this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[5] { this.gridColumn4, this.gridColumn5, this.gridColumn6, this.gridColumn7, this.gridColumn8 });
			this.gridView2.GridControl = this.gridControl2;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsView.ColumnAutoWidth = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			this.gridView2.OptionsView.ShowIndicator = false;
			this.gridView2.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView2_CustomUnboundColumnData);
			this.gridColumn4.Caption = "STT";
			this.gridColumn4.FieldName = "STT";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 0;
			this.gridColumn4.Width = 40;
			this.gridColumn5.Caption = "DELETE";
			this.gridColumn5.ColumnEdit = this.btnG_DELETE;
			this.gridColumn5.FieldName = "DELETE";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.ShowCaption = false;
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 1;
			this.gridColumn5.Width = 30;
			this.btnG_DELETE.AutoHeight = false;
			this.btnG_DELETE.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("btnG_DELETE.Buttons"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, appearanceHovered, appearancePressed, appearanceDisabled, "", null, null, true)
			});
			this.btnG_DELETE.Name = "btnG_DELETE";
			this.btnG_DELETE.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.btnG_DELETE.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnG_DELETE_ButtonClick);
			this.gridColumn6.Caption = "Tên";
			this.gridColumn6.FieldName = "ATTACHMENT_NAME";
			this.gridColumn6.Name = "gridColumn6";
			this.gridColumn6.Visible = true;
			this.gridColumn6.VisibleIndex = 2;
			this.gridColumn6.Width = 500;
			this.gridColumn7.Caption = "Thời gian tạo";
			this.gridColumn7.FieldName = "CREATE_TIME_STR";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn7.Visible = true;
			this.gridColumn7.VisibleIndex = 3;
			this.gridColumn7.Width = 170;
			this.gridColumn8.Caption = "Người tạo";
			this.gridColumn8.FieldName = "CREATOR";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 4;
			this.gridColumn8.Width = 124;
			this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
			this.layoutControlGroup2.GroupBordersVisible = false;
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[1] { this.layoutControlItem4 });
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Size = new System.Drawing.Size(870, 258);
			this.layoutControlGroup2.TextVisible = false;
			this.layoutControlItem4.Control = this.gridControl2;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(870, 258);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			this.gridControl1.Location = new System.Drawing.Point(2, 43);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[1] { this.btnGDELETE });
			this.gridControl1.Size = new System.Drawing.Size(870, 216);
			this.gridControl1.TabIndex = 4;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridView1 });
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[3] { this.gridColumn1, this.gridColumn2, this.gridColumn3 });
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.MultiSelect = true;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.OptionsView.ShowIndicator = false;
			this.gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
			this.gridColumn1.Caption = "STT";
			this.gridColumn1.FieldName = "STT";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 40;
			this.gridColumn2.Caption = "DELETE";
			this.gridColumn2.ColumnEdit = this.btnGDELETE;
			this.gridColumn2.FieldName = "DELETE";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.ShowCaption = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 30;
			this.btnGDELETE.AutoHeight = false;
			this.btnGDELETE.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, (System.Drawing.Image)resources.GetObject("btnGDELETE.Buttons"), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, appearanceHovered2, appearancePressed2, appearanceDisabled2, "", null, null, true)
			});
			this.btnGDELETE.Name = "btnGDELETE";
			this.btnGDELETE.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.btnGDELETE.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnGDELETE_ButtonClick);
			this.gridColumn3.Caption = "Tên";
			this.gridColumn3.FieldName = "FILE_NAME";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn3.Visible = true;
			this.gridColumn3.VisibleIndex = 2;
			this.gridColumn3.Width = 812;
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[6] { this.layoutControlItem2, this.layoutControlItem3, this.layoutControlItem5, this.layoutControlItem6, this.emptySpaceItem1, this.emptySpaceItem2 });
			this.Root.Location = new System.Drawing.Point(0, 0);
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(874, 523);
			this.Root.TextVisible = false;
			this.layoutControlItem2.Control = this.gridControl1;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 41);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(874, 220);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.layoutControlItem3.Control = this.layoutControl3;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 261);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(874, 262);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			this.layoutControlItem5.Control = this.btnChooseFile;
			this.layoutControlItem5.Location = new System.Drawing.Point(559, 15);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(156, 26);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			this.layoutControlItem6.Control = this.btnSave;
			this.layoutControlItem6.Location = new System.Drawing.Point(715, 15);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Size = new System.Drawing.Size(159, 26);
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 15);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(559, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(874, 15);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[1] { this.layoutControlItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(878, 527);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.layoutControl2;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(878, 527);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar2 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[2] { this.bbtnChooseFile, this.bbtnSave });
			this.barManager1.MainMenu = this.bar2;
			this.barManager1.MaxItemId = 2;
			this.bar2.BarName = "Main menu";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnChooseFile),
				new DevExpress.XtraBars.LinkPersistInfo(this.bbtnSave)
			});
			this.bar2.OptionsBar.MultiLine = true;
			this.bar2.OptionsBar.UseWholeRow = true;
			this.bar2.Text = "Main menu";
			this.bar2.Visible = false;
			this.bbtnChooseFile.Caption = "Chọn tập tin (Ctrl C)";
			this.bbtnChooseFile.Id = 0;
			this.bbtnChooseFile.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control);
			this.bbtnChooseFile.Name = "bbtnChooseFile";
			this.bbtnChooseFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnChooseFile_ItemClick);
			this.bbtnSave.Caption = "Lưu (Ctrl S)";
			this.bbtnSave.Id = 1;
			this.bbtnSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
			this.bbtnSave.Name = "bbtnSave";
			this.bbtnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bbtnSave_ItemClick);
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(878, 22);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 549);
			this.barDockControlBottom.Size = new System.Drawing.Size(878, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 22);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 527);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(878, 22);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 527);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(878, 549);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmAttachMents";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tập tin đính kèm";
			base.Load += new System.EventHandler(frmAttachMents_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControl2).EndInit();
			this.layoutControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControl3).EndInit();
			this.layoutControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.gridControl2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.btnG_DELETE).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.btnGDELETE).EndInit();
			((System.ComponentModel.ISupportInitialize)this.Root).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
