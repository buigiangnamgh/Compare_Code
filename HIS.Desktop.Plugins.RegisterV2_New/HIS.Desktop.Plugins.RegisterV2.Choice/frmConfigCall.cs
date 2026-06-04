using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using HIS.Desktop.Library.CacheClient;
using HIS.Desktop.LocalStorage.Location;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.RegisterV2.Choice
{
	public class frmConfigCall : Form
	{
		private const string moduleLink = "HIS.Desktop.Plugins.RegisterV2.frmConfigCall";

		private const long PRIORITY_TRUE = 1L;

		private ControlStateWorker controlStateWorker;

		private List<ControlStateRDO> currentControlStateRDO;

		private Action<bool> IsReload;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private SimpleButton btnSave;

		private TextEdit txtConfig;

		private LayoutControlGroup layoutControlGroup1;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private BarManager barManager1;

		private Bar bar1;

		private BarButtonItem barButtonItem1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		public frmConfigCall(Action<bool> IsReload)
		{
			InitializeComponent();
			base.Icon = Icon.ExtractAssociatedIcon(Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
			this.IsReload = IsReload;
		}

		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				btnSave_Click(null, null);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void SetTextToGate(string obj)
		{
			try
			{
				TextEdit textEdit = txtConfig;
				textEdit.Text = textEdit.Text + " " + obj;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				ControlStateRDO controlStateRDO = ((currentControlStateRDO != null && currentControlStateRDO.Count > 0) ? currentControlStateRDO.Where((ControlStateRDO o) => o.KEY == "HIS.Desktop.Plugins.RegisterV2.frmConfigCall" && o.MODULE_LINK == "HIS.Desktop.Plugins.RegisterV2.frmConfigCall").FirstOrDefault() : null);
				if (controlStateRDO != null)
				{
					controlStateRDO.VALUE = txtConfig.Text;
				}
				else
				{
					controlStateRDO = new ControlStateRDO();
					controlStateRDO.KEY = "HIS.Desktop.Plugins.RegisterV2.frmConfigCall";
					controlStateRDO.VALUE = txtConfig.Text.Trim();
					controlStateRDO.MODULE_LINK = "HIS.Desktop.Plugins.RegisterV2.frmConfigCall";
					if (currentControlStateRDO == null)
					{
						currentControlStateRDO = new List<ControlStateRDO>();
					}
					currentControlStateRDO.Add(controlStateRDO);
				}
				controlStateWorker.SetData(currentControlStateRDO);
				IsReload(true);
				Close();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void frmConfigCall_Load(object sender, EventArgs e)
		{
			try
			{
				controlStateWorker = new ControlStateWorker();
				currentControlStateRDO = controlStateWorker.GetData("HIS.Desktop.Plugins.RegisterV2.frmConfigCall");
				if (currentControlStateRDO == null || currentControlStateRDO.Count <= 0)
				{
					return;
				}
				foreach (ControlStateRDO item in currentControlStateRDO)
				{
					if (item.KEY == "HIS.Desktop.Plugins.RegisterV2.frmConfigCall")
					{
						txtConfig.Text = item.VALUE;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void txtConfig_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.F1)
				{
					frmKeyConfig frmKeyConfig2 = new frmKeyConfig(new Action<string>(SetTextToGate));
					frmKeyConfig2.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Warn(ex);
			}
		}

		private void frmConfigCall_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				currentControlStateRDO = null;
				controlStateWorker = null;
				barButtonItem1.ItemClick -= new ItemClickEventHandler(barButtonItem1_ItemClick);
				btnSave.Click -= new EventHandler(btnSave_Click);
				txtConfig.PreviewKeyDown -= new PreviewKeyDownEventHandler(txtConfig_PreviewKeyDown);
				base.Load -= new EventHandler(frmConfigCall_Load);
				barDockControlRight = null;
				barDockControlLeft = null;
				barDockControlBottom = null;
				barDockControlTop = null;
				barButtonItem1 = null;
				bar1 = null;
				barManager1 = null;
				emptySpaceItem1 = null;
				layoutControlItem2 = null;
				layoutControlItem1 = null;
				layoutControlGroup1 = null;
				txtConfig = null;
				btnSave = null;
				layoutControl1 = null;
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
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.txtConfig = new DevExpress.XtraEditors.TextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.barManager1 = new DevExpress.XtraBars.BarManager();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.txtConfig.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.btnSave);
			this.layoutControl1.Controls.Add(this.txtConfig);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 29);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(575, 25);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.btnSave.Location = new System.Drawing.Point(435, 26);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(121, 22);
			this.btnSave.StyleController = this.layoutControl1;
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Đồng ý (Ctrl S)";
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.txtConfig.Location = new System.Drawing.Point(127, 2);
			this.txtConfig.Name = "txtConfig";
			this.txtConfig.Size = new System.Drawing.Size(429, 20);
			this.txtConfig.StyleController = this.layoutControl1;
			this.txtConfig.TabIndex = 4;
			this.txtConfig.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(txtConfig_PreviewKeyDown);
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[3] { this.layoutControlItem1, this.layoutControlItem2, this.emptySpaceItem1 });
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(558, 50);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.layoutControlItem1.Control = this.txtConfig;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.OptionsToolTip.ToolTip = "Cấu hình nội dung gọi bệnh nhân. Nhấn F1 để xem thông tin hướng dẫn cấu hình";
			this.layoutControlItem1.Size = new System.Drawing.Size(558, 24);
			this.layoutControlItem1.Text = "Nội dung thông báo:";
			this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem1.TextSize = new System.Drawing.Size(120, 20);
			this.layoutControlItem1.TextToControlDistance = 5;
			this.layoutControlItem2.Control = this.btnSave;
			this.layoutControlItem2.Location = new System.Drawing.Point(433, 24);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(125, 26);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 38);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(433, 26);
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[1] { this.barButtonItem1 });
			this.barManager1.MaxItemId = 2;
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)
			});
			this.bar1.Text = "Tools";
			this.bar1.Visible = false;
			this.barButtonItem1.Caption = "barButtonItem1";
			this.barButtonItem1.Id = 0;
			this.barButtonItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
			this.barButtonItem1.Name = "barButtonItem1";
			this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem1_ItemClick);
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(575, 29);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 54);
			this.barDockControlBottom.Size = new System.Drawing.Size(575, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 25);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(575, 29);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 25);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(575, 54);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmConfigCall";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thiết lập thông báo";
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmConfigCall_FormClosed);
			base.Load += new System.EventHandler(frmConfigCall_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.txtConfig.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.emptySpaceItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
