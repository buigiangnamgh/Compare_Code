using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Base;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Desktop.Common.LanguageManager;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.ViewImage
{
	public class FormViewImage : Form
	{
		private ImageADO ImagePreview;

		private List<ImageADO> ListImage;

		private string currentItem = "";

		private int zoom = 40;

		private float zoomSpeedFactor = 0.02f;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private PictureEdit PictureEdit;

		private LayoutControlItem layoutControlItem1;

		private TileControl tileControl;

		private LayoutControlItem layoutControlItem2;

		private TileGroup tileGroup2;

		private LayoutControl layoutControl2;

		private SimpleButton btnPreview;

		private SimpleButton btnNext;

		private SimpleButton btnZoomIn;

		private SimpleButton btnZoomOut;

		private SimpleButton btnLast;

		private SimpleButton btnFirst;

		private LayoutControlGroup Root;

		private LayoutControlItem layoutControlItem4;

		private LayoutControlItem layoutControlItem5;

		private LayoutControlItem layoutControlItem6;

		private LayoutControlItem layoutControlItem7;

		private LayoutControlItem layoutControlItem8;

		private LayoutControlItem layoutControlItem9;

		private LayoutControlItem layoutControlItem3;

		private EmptySpaceItem emptySpaceItem1;

		private EmptySpaceItem emptySpaceItem2;

		public FormViewImage()
		{
			InitializeComponent();
		}

		public FormViewImage(List<ImageADO> listImage, ImageADO imagePreview)
			: this()
		{
			try
			{
				ImagePreview = imagePreview;
				currentItem = ImagePreview.FileName;
				ListImage = listImage;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void FormViewImage_Load(object sender, EventArgs e)
		{
			try
			{
				SetCaptionByLanguageKey();
				ProcessListDataImage();
				ProcessImagePreview();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ProcessListDataImage()
		{
			try
			{
				if (ListImage == null || ListImage.Count <= 0)
				{
					return;
				}
				int num = 0;
				foreach (ImageADO item in ListImage)
				{
					item.ImageIndex = num++;
					TileItem tileItem = new TileItem();
					tileItem.Image = item.IMAGE_DISPLAY;
					tileItem.ImageScaleMode = TileItemImageScaleMode.Stretch;
					tileItem.Name = item.FileName;
					tileItem.ItemClick += TileItemClick;
					if (ImagePreview != null && item == ImagePreview)
					{
						tileItem.Checked = true;
					}
					tileGroup2.Items.Add(tileItem);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void TileItemClick(object sender, TileItemEventArgs e)
		{
			try
			{
				foreach (ITileItem item in tileGroup2.Items)
				{
					item.Checked = false;
				}
				PictureEdit.Image = e.Item.Image;
				PictureEdit.Properties.ZoomPercent = 100.0;
				PictureEdit.Properties.SizeMode = PictureSizeMode.Squeeze;
				e.Item.Checked = true;
				currentItem = e.Item.Name;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void ProcessImagePreview()
		{
			try
			{
				if (ImagePreview != null)
				{
					PictureEdit.Image = ImagePreview.IMAGE_DISPLAY;
					PictureEdit.Properties.SizeMode = PictureSizeMode.Clip;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnZoomIn_Click(object sender, EventArgs e)
		{
			try
			{
				PictureEdit.Properties.ZoomPercent = PictureEdit.Properties.ZoomPercent + (double)zoom;
				PictureEdit.Properties.SizeMode = PictureSizeMode.Clip;
				PictureEdit.Properties.PictureAlignment = ContentAlignment.MiddleCenter;
				PictureEdit.Refresh();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnZoomOut_Click(object sender, EventArgs e)
		{
			try
			{
				PictureEdit.Properties.ZoomPercent = PictureEdit.Properties.ZoomPercent - (double)zoom;
				PictureEdit.Properties.SizeMode = PictureSizeMode.Clip;
				PictureEdit.Properties.PictureAlignment = ContentAlignment.MiddleCenter;
				PictureEdit.Refresh();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (ITileItem item in tileGroup2.Items)
				{
					item.Checked = false;
				}
				currentItem = tileGroup2.Items[0].Name;
				PictureEdit.Image = tileGroup2.Items[0].Image;
				PictureEdit.Properties.ZoomPercent = 100.0;
				PictureEdit.Properties.SizeMode = PictureSizeMode.Clip;
				tileGroup2.Items[0].Checked = true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (ITileItem item in tileGroup2.Items)
				{
					item.Checked = false;
				}
				PictureEdit.Image = tileGroup2.Items[tileGroup2.Items.Count - 1].Image;
				currentItem = tileGroup2.Items[tileGroup2.Items.Count - 1].Name;
				PictureEdit.Properties.ZoomPercent = 100.0;
				PictureEdit.Properties.SizeMode = PictureSizeMode.Clip;
				tileGroup2.Items[tileGroup2.Items.Count - 1].Checked = true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnPreview_Click(object sender, EventArgs e)
		{
			try
			{
				ImageADO imageADO = null;
				foreach (ImageADO item in ListImage)
				{
					if (currentItem == item.FileName)
					{
						imageADO = ListImage[item.ImageIndex - 1];
					}
				}
				currentItem = imageADO.FileName;
				foreach (ITileItem item2 in tileGroup2.Items)
				{
					item2.Checked = false;
					if (imageADO.FileName == ((TileItem)item2).Name)
					{
						item2.Checked = true;
					}
				}
				PictureEdit.Image = imageADO.IMAGE_DISPLAY;
				PictureEdit.Properties.SizeMode = PictureSizeMode.Clip;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			try
			{
				ImageADO imageADO = null;
				foreach (ImageADO item in ListImage)
				{
					if (currentItem == item.FileName)
					{
						imageADO = ListImage[item.ImageIndex + 1];
					}
				}
				currentItem = imageADO.FileName;
				foreach (ITileItem item2 in tileGroup2.Items)
				{
					item2.Checked = false;
					if (imageADO.FileName == ((TileItem)item2).Name)
					{
						item2.Checked = true;
					}
				}
				PictureEdit.Image = imageADO.IMAGE_DISPLAY;
				PictureEdit.Properties.SizeMode = PictureSizeMode.Clip;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void PictureEdit_Properties_MouseWheel(object sender, MouseEventArgs e)
		{
			try
			{
				PictureEdit.Properties.ZoomPercent += (float)e.Delta * zoomSpeedFactor;
				DXMouseEventArgs.GetMouseArgs(e).Handled = true;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetCaptionByLanguageKey()
		{
			try
			{
				ResourceLanguageManager.LanguageResource__FormViewImage = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(FormViewImage).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("FormViewImage.layoutControl1.Text", ResourceLanguageManager.LanguageResource__FormViewImage, LanguageManager.GetCulture());
				layoutControl2.Text = Inventec.Common.Resource.Get.Value("FormViewImage.layoutControl2.Text", ResourceLanguageManager.LanguageResource__FormViewImage, LanguageManager.GetCulture());
				tileControl.Text = Inventec.Common.Resource.Get.Value("FormViewImage.tileControl.Text", ResourceLanguageManager.LanguageResource__FormViewImage, LanguageManager.GetCulture());
				PictureEdit.Properties.NullText = Inventec.Common.Resource.Get.Value("FormViewImage.PictureEdit.Properties.NullText", ResourceLanguageManager.LanguageResource__FormViewImage, LanguageManager.GetCulture());
				Text = Inventec.Common.Resource.Get.Value("FormViewImage.Text", ResourceLanguageManager.LanguageResource__FormViewImage, LanguageManager.GetCulture());
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HIS.Desktop.Plugins.SurgServiceReqExecute.ViewImage.FormViewImage));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
			this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
			this.btnNext = new DevExpress.XtraEditors.SimpleButton();
			this.btnZoomIn = new DevExpress.XtraEditors.SimpleButton();
			this.btnZoomOut = new DevExpress.XtraEditors.SimpleButton();
			this.btnLast = new DevExpress.XtraEditors.SimpleButton();
			this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
			this.tileControl = new DevExpress.XtraEditors.TileControl();
			this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
			this.PictureEdit = new DevExpress.XtraEditors.PictureEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.layoutControl2).BeginInit();
			this.layoutControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem9).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.PictureEdit.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.layoutControl2);
			this.layoutControl1.Controls.Add(this.tileControl);
			this.layoutControl1.Controls.Add(this.PictureEdit);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(560, 461);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.layoutControl2.Controls.Add(this.btnPreview);
			this.layoutControl2.Controls.Add(this.btnNext);
			this.layoutControl2.Controls.Add(this.btnZoomIn);
			this.layoutControl2.Controls.Add(this.btnZoomOut);
			this.layoutControl2.Controls.Add(this.btnLast);
			this.layoutControl2.Controls.Add(this.btnFirst);
			this.layoutControl2.Location = new System.Drawing.Point(2, 413);
			this.layoutControl2.Name = "layoutControl2";
			this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(810, 325, 250, 350);
			this.layoutControl2.Root = this.Root;
			this.layoutControl2.Size = new System.Drawing.Size(556, 46);
			this.layoutControl2.TabIndex = 6;
			this.layoutControl2.Text = "layoutControl2";
			this.btnPreview.Image = (System.Drawing.Image)resources.GetObject("btnPreview.Image");
			this.btnPreview.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnPreview.Location = new System.Drawing.Point(264, 12);
			this.btnPreview.Name = "btnPreview";
			this.btnPreview.Size = new System.Drawing.Size(26, 22);
			this.btnPreview.StyleController = this.layoutControl2;
			this.btnPreview.TabIndex = 9;
			this.btnPreview.Click += new System.EventHandler(btnPreview_Click);
			this.btnNext.Image = (System.Drawing.Image)resources.GetObject("btnNext.Image");
			this.btnNext.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnNext.Location = new System.Drawing.Point(294, 12);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(26, 22);
			this.btnNext.StyleController = this.layoutControl2;
			this.btnNext.TabIndex = 8;
			this.btnNext.Click += new System.EventHandler(btnNext_Click);
			this.btnZoomIn.Image = (System.Drawing.Image)resources.GetObject("btnZoomIn.Image");
			this.btnZoomIn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnZoomIn.Location = new System.Drawing.Point(354, 12);
			this.btnZoomIn.Name = "btnZoomIn";
			this.btnZoomIn.Size = new System.Drawing.Size(26, 22);
			this.btnZoomIn.StyleController = this.layoutControl2;
			this.btnZoomIn.TabIndex = 7;
			this.btnZoomIn.Click += new System.EventHandler(btnZoomIn_Click);
			this.btnZoomOut.Image = (System.Drawing.Image)resources.GetObject("btnZoomOut.Image");
			this.btnZoomOut.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnZoomOut.Location = new System.Drawing.Point(384, 12);
			this.btnZoomOut.Name = "btnZoomOut";
			this.btnZoomOut.Size = new System.Drawing.Size(26, 22);
			this.btnZoomOut.StyleController = this.layoutControl2;
			this.btnZoomOut.TabIndex = 6;
			this.btnZoomOut.Click += new System.EventHandler(btnZoomOut_Click);
			this.btnLast.Image = (System.Drawing.Image)resources.GetObject("btnLast.Image");
			this.btnLast.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnLast.Location = new System.Drawing.Point(324, 12);
			this.btnLast.Name = "btnLast";
			this.btnLast.Size = new System.Drawing.Size(26, 22);
			this.btnLast.StyleController = this.layoutControl2;
			this.btnLast.TabIndex = 5;
			this.btnLast.Click += new System.EventHandler(btnLast_Click);
			this.btnFirst.Appearance.Image = (System.Drawing.Image)resources.GetObject("btnFirst.Appearance.Image");
			this.btnFirst.Appearance.Options.UseImage = true;
			this.btnFirst.Image = (System.Drawing.Image)resources.GetObject("btnFirst.Image");
			this.btnFirst.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			this.btnFirst.Location = new System.Drawing.Point(234, 12);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(26, 22);
			this.btnFirst.StyleController = this.layoutControl2;
			this.btnFirst.TabIndex = 4;
			this.btnFirst.Click += new System.EventHandler(btnFirst_Click);
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[8] { this.layoutControlItem4, this.layoutControlItem5, this.layoutControlItem6, this.layoutControlItem7, this.layoutControlItem8, this.layoutControlItem9, this.emptySpaceItem1, this.emptySpaceItem2 });
			this.Root.Location = new System.Drawing.Point(0, 0);
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(556, 46);
			this.Root.TextVisible = false;
			this.layoutControlItem4.Control = this.btnFirst;
			this.layoutControlItem4.Location = new System.Drawing.Point(222, 0);
			this.layoutControlItem4.MaxSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem4.MinSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(30, 26);
			this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			this.layoutControlItem5.Control = this.btnLast;
			this.layoutControlItem5.Location = new System.Drawing.Point(312, 0);
			this.layoutControlItem5.MaxSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem5.MinSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(30, 26);
			this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			this.layoutControlItem6.Control = this.btnZoomOut;
			this.layoutControlItem6.Location = new System.Drawing.Point(372, 0);
			this.layoutControlItem6.MaxSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem6.MinSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Size = new System.Drawing.Size(30, 26);
			this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			this.layoutControlItem7.Control = this.btnZoomIn;
			this.layoutControlItem7.Location = new System.Drawing.Point(342, 0);
			this.layoutControlItem7.MaxSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem7.MinSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Size = new System.Drawing.Size(30, 26);
			this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem7.TextVisible = false;
			this.layoutControlItem8.Control = this.btnNext;
			this.layoutControlItem8.Location = new System.Drawing.Point(282, 0);
			this.layoutControlItem8.MaxSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem8.MinSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem8.Name = "layoutControlItem8";
			this.layoutControlItem8.Size = new System.Drawing.Size(30, 26);
			this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem8.TextVisible = false;
			this.layoutControlItem9.Control = this.btnPreview;
			this.layoutControlItem9.Location = new System.Drawing.Point(252, 0);
			this.layoutControlItem9.MaxSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem9.MinSize = new System.Drawing.Size(30, 26);
			this.layoutControlItem9.Name = "layoutControlItem9";
			this.layoutControlItem9.Size = new System.Drawing.Size(30, 26);
			this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem9.TextVisible = false;
			this.tileControl.DragSize = new System.Drawing.Size(0, 0);
			this.tileControl.Groups.Add(this.tileGroup2);
			this.tileControl.ItemPadding = new System.Windows.Forms.Padding(0);
			this.tileControl.ItemSize = 60;
			this.tileControl.Location = new System.Drawing.Point(2, 2);
			this.tileControl.Name = "tileControl";
			this.tileControl.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tileControl.Padding = new System.Windows.Forms.Padding(2);
			this.tileControl.RowCount = 1;
			this.tileControl.Size = new System.Drawing.Size(100, 407);
			this.tileControl.TabIndex = 5;
			this.tileControl.Text = "tileControl1";
			this.tileGroup2.Name = "tileGroup2";
			this.PictureEdit.Location = new System.Drawing.Point(106, 2);
			this.PictureEdit.Name = "PictureEdit";
			this.PictureEdit.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
			this.PictureEdit.Properties.AllowScrollViaMouseDrag = true;
			this.PictureEdit.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
			this.PictureEdit.Properties.NullText = " ";
			this.PictureEdit.Properties.ShowScrollBars = true;
			this.PictureEdit.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.True;
			this.PictureEdit.Properties.ZoomingOperationMode = DevExpress.XtraEditors.Repository.ZoomingOperationMode.ControlMouseWheel;
			this.PictureEdit.Properties.MouseWheel += new System.Windows.Forms.MouseEventHandler(PictureEdit_Properties_MouseWheel);
			this.PictureEdit.Size = new System.Drawing.Size(452, 407);
			this.PictureEdit.StyleController = this.layoutControl1;
			this.PictureEdit.TabIndex = 4;
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3] { this.layoutControlItem1, this.layoutControlItem2, this.layoutControlItem3 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(560, 461);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.Control = this.PictureEdit;
			this.layoutControlItem1.Location = new System.Drawing.Point(104, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(456, 411);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			this.layoutControlItem2.Control = this.tileControl;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.MaxSize = new System.Drawing.Size(104, 0);
			this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 24);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(104, 411);
			this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.layoutControlItem3.Control = this.layoutControl2;
			this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 411);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(560, 50);
			this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.SupportHorzAlignment;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(222, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.emptySpaceItem2.AllowHotTrack = false;
			this.emptySpaceItem2.Location = new System.Drawing.Point(402, 0);
			this.emptySpaceItem2.Name = "emptySpaceItem2";
			this.emptySpaceItem2.Size = new System.Drawing.Size(134, 26);
			this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(560, 461);
			base.Controls.Add(this.layoutControl1);
			base.Name = "FormViewImage";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " ";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new System.EventHandler(FormViewImage_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.layoutControl2).EndInit();
			this.layoutControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.Root).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem9).EndInit();
			((System.ComponentModel.ISupportInitialize)this.PictureEdit.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem2).EndInit();
			base.ResumeLayout(false);
		}
	}
}
