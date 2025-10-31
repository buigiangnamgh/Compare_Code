using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Common.Resource;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using Inventec.Desktop.Common.Modules;
using Inventec.UC.Paging;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.ViewImage
{
	public class FormImageTemp : FormBase
	{
		private Inventec.Desktop.Common.Modules.Module Module;

		private Action<List<HIS_TEXT_LIB>> SelectImageId;

		private int rowCount = 0;

		private int dataTotal = 0;

		private int startPage = 0;

		private long? DeparmentId = null;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private SimpleButton btnSave;

		private GridControl gridControlImage;

		private GridView gridViewImage;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private EmptySpaceItem emptySpaceItem1;

		private SimpleButton btnSearch;

		private TextEdit txtKeyWord;

		private PictureEdit pictureData;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem4;

		private LayoutControlItem layoutControlItem5;

		private GridColumn gridColumn1;

		private GridColumn gridColumn3;

		private BarManager barManager1;

		private Bar bar1;

		private BarButtonItem barBtnSearch;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private BarButtonItem barBtnSave;

		private UcPaging ucPaging1;

		private LayoutControlItem layoutControlItem6;

		public FormImageTemp(Inventec.Desktop.Common.Modules.Module _module, Action<List<HIS_TEXT_LIB>> selectImageId)
			: base(_module)
		{
			InitializeComponent();
			try
			{
				this.Module = _module;
				SelectImageId = selectImageId;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void FormImageTemp_Load(object sender, EventArgs e)
		{
			try
			{
				SetCaptionByLanguageKey();
				DeparmentId = WorkPlace.WorkPlaceSDO.FirstOrDefault((WorkPlaceSDO o) => o.RoomId == this.Module.RoomId).DepartmentId;
				FillDataToGrid();
				txtKeyWord.Focus();
				txtKeyWord.SelectAll();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void FillDataToGrid()
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			try
			{
				WaitingManager.Show();
				int num = (int)((ucPaging1.pagingGrid != null) ? ucPaging1.pagingGrid.PageSize : ConfigApplications.NumPageSize);
				GridPaging((object)new CommonParam((int?)0, (int?)num));
				CommonParam val = new CommonParam();
				val.Limit = rowCount;
				val.Count = dataTotal;
				ucPaging1.Init(new LoadDataDelegate(GridPaging), val, num);
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				WaitingManager.Hide();
			}
		}

		private void GridPaging(object param)
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				startPage = ((CommonParam)param).Start.GetValueOrDefault();
				int valueOrDefault = ((CommonParam)param).Limit.GetValueOrDefault();
				CommonParam val = new CommonParam((int?)startPage, (int?)valueOrDefault);
				ApiResultObject<List<HIS_TEXT_LIB>> val2 = null;
				HisTextLibFilter filter = new HisTextLibFilter();
				SetFilter(ref filter);
				gridViewImage.BeginUpdate();
				val2 = ((AdapterBase)new BackendAdapter(val)).GetRO<List<HIS_TEXT_LIB>>("api/HisTextLib/Get", HIS.Desktop.ApiConsumer.ApiConsumers.MosConsumer, (object)filter, (Action)SessionManager.ActionLostToken, val);
				if (val2 != null)
				{
					List<HIS_TEXT_LIB> data = val2.Data;
					if (data != null && data.Count > 0)
					{
						gridControlImage.DataSource = data;
						rowCount = ((data != null) ? data.Count : 0);
						dataTotal = ((((Result)val2).Param != null) ? ((Result)val2).Param.Count.GetValueOrDefault() : 0);
					}
					else
					{
						gridControlImage.DataSource = null;
						rowCount = ((data != null) ? data.Count : 0);
						dataTotal = ((((Result)val2).Param != null) ? ((Result)val2).Param.Count.GetValueOrDefault() : 0);
					}
				}
				gridViewImage.EndUpdate();
				SessionManager.ProcessTokenLost(val);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				gridViewImage.EndUpdate();
			}
		}

		private void SetFilter(ref HisTextLibFilter filter)
		{
			try
			{
				((FilterBase)filter).ORDER_FIELD = "MODIFY_TIME";
				((FilterBase)filter).ORDER_DIRECTION = "DESC";
				((FilterBase)filter).KEY_WORD = txtKeyWord.Text.Trim();
				filter.CAN_VIEW = true;
				filter.PUBLIC_DEPARTMENT_ID = DeparmentId;
				filter.LIB_TYPE_ID = 2L;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				FillDataToGrid();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			try
			{
				List<HIS_TEXT_LIB> list = new List<HIS_TEXT_LIB>();
				int[] selectedRows = gridViewImage.GetSelectedRows();
				if (selectedRows != null && selectedRows.Count() > 0)
				{
					int[] array = selectedRows;
					foreach (int rowHandle in array)
					{
						HIS_TEXT_LIB val = (HIS_TEXT_LIB)gridViewImage.GetRow(rowHandle);
						if (val != null)
						{
							list.Add(val);
						}
					}
				}
				if ((list.Count > 0 || MessageBox.Show(ResourceMessage.BanChuaChonLuocDo, ResourceMessage.ThongBao, MessageBoxButtons.OKCancel) == DialogResult.OK) && SelectImageId != null)
				{
					SelectImageId(list);
					((Form)this).Close();
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void barBtnSearch_ItemClick(object sender, ItemClickEventArgs e)
		{
			btnSearch_Click(null, null);
		}

		private void barBtnSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			btnSave_Click(null, null);
		}

		private void gridViewImage_RowClick(object sender, RowClickEventArgs e)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			try
			{
				HIS_TEXT_LIB val = (HIS_TEXT_LIB)gridViewImage.GetFocusedRow();
				if (val != null)
				{
					string s = Encoding.UTF8.GetString(val.CONTENT);
					byte[] buffer = System.Convert.FromBase64String(s);
					using (MemoryStream stream = new MemoryStream(buffer))
					{
						pictureData.Image = Image.FromStream(stream, true);
						return;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void gridViewImage_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			try
			{
				if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound)
				{
					HIS_TEXT_LIB val = (HIS_TEXT_LIB)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
					if (val != null && e.Column.FieldName == "STT")
					{
						e.Value = e.ListSourceRowIndex + 1 + startPage;
					}
				}
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
				ResourceLanguageManager.LanguageResource__FormImageTemp = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(FormImageTemp).Assembly);
				layoutControl1.Text = Inventec.Common.Resource.Get.Value("FormImageTemp.layoutControl1.Text", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				btnSearch.Text = Inventec.Common.Resource.Get.Value("FormImageTemp.btnSearch.Text", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				txtKeyWord.Properties.NullValuePrompt = Inventec.Common.Resource.Get.Value("FormImageTemp.txtKeyWord.Properties.NullValuePrompt", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				btnSave.Text = Inventec.Common.Resource.Get.Value("FormImageTemp.btnSave.Text", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				gridColumn1.Caption = Inventec.Common.Resource.Get.Value("FormImageTemp.gridColumn1.Caption", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				gridColumn3.Caption = Inventec.Common.Resource.Get.Value("FormImageTemp.gridColumn3.Caption", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				bar1.Text = Inventec.Common.Resource.Get.Value("FormImageTemp.bar1.Text", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				barBtnSearch.Caption = Inventec.Common.Resource.Get.Value("FormImageTemp.barBtnSearch.Caption", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				barBtnSave.Caption = Inventec.Common.Resource.Get.Value("FormImageTemp.barBtnSave.Caption", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
				((Control)(object)this).Text = Inventec.Common.Resource.Get.Value("FormImageTemp.Text", ResourceLanguageManager.LanguageResource__FormImageTemp, LanguageManager.GetCulture());
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
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			layoutControl1 = new LayoutControl();
			ucPaging1 = new UcPaging();
			btnSearch = new SimpleButton();
			txtKeyWord = new TextEdit();
			pictureData = new PictureEdit();
			btnSave = new SimpleButton();
			gridControlImage = new GridControl();
			gridViewImage = new GridView();
			gridColumn1 = new GridColumn();
			gridColumn3 = new GridColumn();
			layoutControlGroup1 = new LayoutControlGroup();
			layoutControlItem1 = new LayoutControlItem();
			layoutControlItem2 = new LayoutControlItem();
			emptySpaceItem1 = new EmptySpaceItem();
			layoutControlItem3 = new LayoutControlItem();
			layoutControlItem4 = new LayoutControlItem();
			layoutControlItem5 = new LayoutControlItem();
			layoutControlItem6 = new LayoutControlItem();
			barManager1 = new BarManager();
			bar1 = new Bar();
			barBtnSearch = new BarButtonItem();
			barBtnSave = new BarButtonItem();
			barDockControlTop = new BarDockControl();
			barDockControlBottom = new BarDockControl();
			barDockControlLeft = new BarDockControl();
			barDockControlRight = new BarDockControl();
			((ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((ISupportInitialize)txtKeyWord.Properties).BeginInit();
			((ISupportInitialize)pictureData.Properties).BeginInit();
			((ISupportInitialize)gridControlImage).BeginInit();
			((ISupportInitialize)gridViewImage).BeginInit();
			((ISupportInitialize)layoutControlGroup1).BeginInit();
			((ISupportInitialize)layoutControlItem1).BeginInit();
			((ISupportInitialize)layoutControlItem2).BeginInit();
			((ISupportInitialize)emptySpaceItem1).BeginInit();
			((ISupportInitialize)layoutControlItem3).BeginInit();
			((ISupportInitialize)layoutControlItem4).BeginInit();
			((ISupportInitialize)layoutControlItem5).BeginInit();
			((ISupportInitialize)layoutControlItem6).BeginInit();
			((ISupportInitialize)barManager1).BeginInit();
			((Control)this).SuspendLayout();
			layoutControl1.Controls.Add((Control)(object)ucPaging1);
			layoutControl1.Controls.Add(btnSearch);
			layoutControl1.Controls.Add(txtKeyWord);
			layoutControl1.Controls.Add(pictureData);
			layoutControl1.Controls.Add(btnSave);
			layoutControl1.Controls.Add(gridControlImage);
			layoutControl1.Dock = DockStyle.Fill;
			layoutControl1.Location = new Point(0, 29);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.Root = layoutControlGroup1;
			layoutControl1.Size = new Size(880, 408);
			layoutControl1.TabIndex = 0;
			layoutControl1.Text = "layoutControl1";
			((Control)(object)ucPaging1).Location = new Point(2, 360);
			((Control)(object)ucPaging1).Name = "ucPaging1";
			((Control)(object)ucPaging1).Size = new Size(436, 20);
			((Control)(object)ucPaging1).TabIndex = 9;
			btnSearch.Location = new Point(332, 2);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(106, 22);
			btnSearch.StyleController = layoutControl1;
			btnSearch.TabIndex = 8;
			btnSearch.Text = "Tìm (Ctrl F)";
			btnSearch.Click += btnSearch_Click;
			txtKeyWord.Location = new Point(2, 2);
			txtKeyWord.Name = "txtKeyWord";
			txtKeyWord.Properties.NullValuePrompt = "Từ khóa tìm kiếm";
			txtKeyWord.Properties.NullValuePromptShowForEmptyValue = true;
			txtKeyWord.Properties.ShowNullValuePromptWhenFocused = true;
			txtKeyWord.Size = new Size(326, 20);
			txtKeyWord.StyleController = layoutControl1;
			txtKeyWord.TabIndex = 7;
			pictureData.Location = new Point(442, 2);
			pictureData.Name = "pictureData";
			pictureData.Properties.ShowCameraMenuItem = CameraMenuItemVisibility.Auto;
			pictureData.Size = new Size(436, 378);
			pictureData.StyleController = layoutControl1;
			pictureData.TabIndex = 6;
			btnSave.Location = new Point(772, 384);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(106, 22);
			btnSave.StyleController = layoutControl1;
			btnSave.TabIndex = 5;
			btnSave.Text = "Chọn (Ctrl S)";
			btnSave.Click += btnSave_Click;
			gridControlImage.Location = new Point(2, 28);
			gridControlImage.MainView = gridViewImage;
			gridControlImage.Name = "gridControlImage";
			gridControlImage.Size = new Size(436, 328);
			gridControlImage.TabIndex = 4;
			gridControlImage.ViewCollection.AddRange(new BaseView[1] { gridViewImage });
			gridViewImage.Columns.AddRange(new GridColumn[2] { gridColumn1, gridColumn3 });
			gridViewImage.GridControl = gridControlImage;
			gridViewImage.Name = "gridViewImage";
			gridViewImage.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
			gridViewImage.OptionsSelection.MultiSelect = true;
			gridViewImage.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
			gridViewImage.OptionsView.ShowGroupPanel = false;
			gridViewImage.OptionsView.ShowIndicator = false;
			gridViewImage.RowClick += gridViewImage_RowClick;
			gridViewImage.CustomUnboundColumnData += gridViewImage_CustomUnboundColumnData;
			gridColumn1.Caption = "STT";
			gridColumn1.FieldName = "STT";
			gridColumn1.Name = "gridColumn1";
			gridColumn1.OptionsColumn.AllowEdit = false;
			gridColumn1.UnboundType = UnboundColumnType.Object;
			gridColumn1.Visible = true;
			gridColumn1.VisibleIndex = 1;
			gridColumn1.Width = 50;
			gridColumn3.Caption = "Tiêu đề";
			gridColumn3.FieldName = "TITLE";
			gridColumn3.Name = "gridColumn3";
			gridColumn3.OptionsColumn.AllowEdit = false;
			gridColumn3.Visible = true;
			gridColumn3.VisibleIndex = 2;
			gridColumn3.Width = 354;
			layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Items.AddRange(new BaseLayoutItem[7] { layoutControlItem1, layoutControlItem2, emptySpaceItem1, layoutControlItem3, layoutControlItem4, layoutControlItem5, layoutControlItem6 });
			layoutControlGroup1.Location = new Point(0, 0);
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			layoutControlGroup1.Size = new Size(880, 408);
			layoutControlGroup1.TextVisible = false;
			layoutControlItem1.Control = gridControlImage;
			layoutControlItem1.Location = new Point(0, 26);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new Size(440, 332);
			layoutControlItem1.TextSize = new Size(0, 0);
			layoutControlItem1.TextVisible = false;
			layoutControlItem2.Control = btnSave;
			layoutControlItem2.Location = new Point(770, 382);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Size = new Size(110, 26);
			layoutControlItem2.TextSize = new Size(0, 0);
			layoutControlItem2.TextVisible = false;
			emptySpaceItem1.AllowHotTrack = false;
			emptySpaceItem1.Location = new Point(0, 382);
			emptySpaceItem1.Name = "emptySpaceItem1";
			emptySpaceItem1.Size = new Size(770, 26);
			emptySpaceItem1.TextSize = new Size(0, 0);
			layoutControlItem3.Control = pictureData;
			layoutControlItem3.Location = new Point(440, 0);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new Size(440, 382);
			layoutControlItem3.TextSize = new Size(0, 0);
			layoutControlItem3.TextVisible = false;
			layoutControlItem4.Control = txtKeyWord;
			layoutControlItem4.Location = new Point(0, 0);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new Size(330, 26);
			layoutControlItem4.TextSize = new Size(0, 0);
			layoutControlItem4.TextVisible = false;
			layoutControlItem5.Control = btnSearch;
			layoutControlItem5.Location = new Point(330, 0);
			layoutControlItem5.Name = "layoutControlItem5";
			layoutControlItem5.Size = new Size(110, 26);
			layoutControlItem5.TextSize = new Size(0, 0);
			layoutControlItem5.TextVisible = false;
			layoutControlItem6.Control = (Control)(object)ucPaging1;
			layoutControlItem6.Location = new Point(0, 358);
			layoutControlItem6.Name = "layoutControlItem6";
			layoutControlItem6.Size = new Size(440, 24);
			layoutControlItem6.TextSize = new Size(0, 0);
			layoutControlItem6.TextVisible = false;
			barManager1.Bars.AddRange(new Bar[1] { bar1 });
			barManager1.DockControls.Add(barDockControlTop);
			barManager1.DockControls.Add(barDockControlBottom);
			barManager1.DockControls.Add(barDockControlLeft);
			barManager1.DockControls.Add(barDockControlRight);
			barManager1.Form = (Control)(object)this;
			barManager1.Items.AddRange(new BarItem[2] { barBtnSearch, barBtnSave });
			barManager1.MaxItemId = 2;
			bar1.BarName = "Tools";
			bar1.DockCol = 0;
			bar1.DockRow = 0;
			bar1.DockStyle = BarDockStyle.Top;
			bar1.LinksPersistInfo.AddRange(new LinkPersistInfo[2]
			{
				new LinkPersistInfo(barBtnSearch),
				new LinkPersistInfo(barBtnSave)
			});
			bar1.Text = "Tools";
			bar1.Visible = false;
			barBtnSearch.Caption = "Crtl F";
			barBtnSearch.Id = 0;
			barBtnSearch.ItemShortcut = new BarShortcut(Keys.F | Keys.Control);
			barBtnSearch.Name = "barBtnSearch";
			barBtnSearch.ItemClick += barBtnSearch_ItemClick;
			barBtnSave.Caption = "Ctrl S";
			barBtnSave.Id = 1;
			barBtnSave.ItemShortcut = new BarShortcut(Keys.S | Keys.Control);
			barBtnSave.Name = "barBtnSave";
			barBtnSave.ItemClick += barBtnSave_ItemClick;
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = DockStyle.Top;
			barDockControlTop.Location = new Point(0, 0);
			barDockControlTop.Size = new Size(880, 29);
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = DockStyle.Bottom;
			barDockControlBottom.Location = new Point(0, 437);
			barDockControlBottom.Size = new Size(880, 0);
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = DockStyle.Left;
			barDockControlLeft.Location = new Point(0, 29);
			barDockControlLeft.Size = new Size(0, 408);
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = DockStyle.Right;
			barDockControlRight.Location = new Point(880, 29);
			barDockControlRight.Size = new Size(0, 408);
			((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
			((ContainerControl)this).AutoScaleMode = AutoScaleMode.Font;
			((Form)this).ClientSize = new Size(880, 437);
			((Control)this).Controls.Add(layoutControl1);
			((Control)this).Controls.Add(barDockControlLeft);
			((Control)this).Controls.Add(barDockControlRight);
			((Control)this).Controls.Add(barDockControlBottom);
			((Control)this).Controls.Add(barDockControlTop);
			((Control)this).Name = "FormImageTemp";
			((Form)this).StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)this).Text = "Mẫu lược đồ phẫu thuật thủ thuật";
			((Form)this).Load += FormImageTemp_Load;
			((Control)this).Controls.SetChildIndex(barDockControlTop, 0);
			((Control)this).Controls.SetChildIndex(barDockControlBottom, 0);
			((Control)this).Controls.SetChildIndex(barDockControlRight, 0);
			((Control)this).Controls.SetChildIndex(barDockControlLeft, 0);
			((Control)this).Controls.SetChildIndex(layoutControl1, 0);
			((ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			((ISupportInitialize)txtKeyWord.Properties).EndInit();
			((ISupportInitialize)pictureData.Properties).EndInit();
			((ISupportInitialize)gridControlImage).EndInit();
			((ISupportInitialize)gridViewImage).EndInit();
			((ISupportInitialize)layoutControlGroup1).EndInit();
			((ISupportInitialize)layoutControlItem1).EndInit();
			((ISupportInitialize)layoutControlItem2).EndInit();
			((ISupportInitialize)emptySpaceItem1).EndInit();
			((ISupportInitialize)layoutControlItem3).EndInit();
			((ISupportInitialize)layoutControlItem4).EndInit();
			((ISupportInitialize)layoutControlItem5).EndInit();
			((ISupportInitialize)layoutControlItem6).EndInit();
			((ISupportInitialize)barManager1).EndInit();
			((Control)this).ResumeLayout(false);
			((Control)this).PerformLayout();
		}
	}
}
