using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using Inventec.Common.Integrate;
using Inventec.Common.Logging;
using Inventec.Common.SignLibrary.ADO;

namespace Inventec.Common.SignLibrary
{
	public class FormEmrSign : Form
	{
		private string DocumentCode;

		private string LoginName;

		private long MaxOrder;

		private long MinOrder;

		private V_EMR_DOCUMENT Document;

		private List<ListSignConfigADO> ListDataSign;

		private IContainer components = null;

		private LayoutControl layoutControl1;

		private LayoutControlGroup layoutControlGroup1;

		private GridControl gridControlSign;

		private GridView gridViewSign;

		private GridColumn Gc_Stt;

		private LayoutControlItem layoutControlItem2;

		private GridColumn Gc_Delete;

		private GridColumn Gc_Up;

		private GridColumn Gc_Down;

		private GridColumn Gc_Signer;

		private GridColumn Gc_Title;

		private GridColumn Gc_Department;

		private GridColumn Gc_SignTime;

		private GridColumn Gc_RejectTime;

		private GridColumn Gc_RejectReason;

		private GridColumn Gc_Add;

		private BarManager barManager1;

		private Bar bar1;

		private BarButtonItem barButtonSave;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private RepositoryItemTextEdit repositoryItemText;

		private RepositoryItemButtonEdit repositoryItemCboFlow;

		private RepositoryItemTextEdit repositoryItemText_Enable;

		public FormEmrSign()
		{
			InitializeComponent();
		}

		public FormEmrSign(string documentCode)
		{
			InitializeComponent();
			try
			{
				DocumentCode = documentCode;
				LoginName = GlobalStore.LoginName;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}

		private void FormEmrSign_Load(object sender, EventArgs e)
		{
			try
			{
				LoadKeysFromlanguage();
				SetDefaultData();
				FillDataToControl();
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
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

		private void FillDataToControl()
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			try
			{
				WaitingManager.Show();
				ListDataSign = new List<ListSignConfigADO>();
				CommonParam commonParam = new CommonParam();
				EmrSignViewFilter val = new EmrSignViewFilter();
				val.DOCUMENT_ID = ((Document != null) ? Document.ID : 0);
				List<V_EMR_SIGN> list = GlobalStore.EmrConsumer.Get<List<V_EMR_SIGN>>("api/EmrSign/GetView", commonParam, val, new object[0]);
				if (list != null && list.Count > 0)
				{
					ListDataSign = new List<ListSignConfigADO>();
					foreach (V_EMR_SIGN item in list)
					{
						ListSignConfigADO listSignConfigADO = new ListSignConfigADO(item);
						listSignConfigADO.IdRow = ((V_EMR_SIGN)listSignConfigADO).NUM_ORDER;
						ListDataSign.Add(listSignConfigADO);
					}
					if (ListDataSign != null && ListDataSign.Count > 0)
					{
						MaxOrder = ListDataSign.Max((ListSignConfigADO o) => o.IdRow);
						MinOrder = MaxOrder;
						List<ListSignConfigADO> list2 = ListDataSign.Where((ListSignConfigADO o) => !((V_EMR_SIGN)o).SIGN_TIME.HasValue && !((V_EMR_SIGN)o).REJECT_TIME.HasValue).ToList();
						if (list2 != null && list2.Count > 0)
						{
							MinOrder = list2.Min((ListSignConfigADO m) => m.IdRow);
						}
						ListDataSign = ListDataSign.OrderBy((ListSignConfigADO o) => o.IdRow).ToList();
					}
				}
				WaitingManager.Hide();
				gridControlSign.BeginUpdate();
				gridControlSign.DataSource = null;
				gridControlSign.DataSource = ListDataSign;
				gridControlSign.EndUpdate();
				WaitingManager.Hide();
			}
			catch (Exception ex)
			{
				WaitingManager.Hide();
				LogSystem.Error(ex);
			}
		}

		private void LoadKeysFromlanguage()
		{
		}

		private void gridViewSign_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			try
			{
				if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound)
				{
					ListSignConfigADO listSignConfigADO = (ListSignConfigADO)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
					if (listSignConfigADO != null && e.Column.FieldName == "STT")
					{
						e.Value = e.ListSourceRowIndex + 1;
					}
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventec.Common.SignLibrary.FormEmrSign));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject17 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject18 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject19 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject20 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject21 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject22 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject23 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject24 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject25 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject26 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject27 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject28 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject29 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject30 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject31 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject32 = new DevExpress.Utils.SerializableAppearanceObject();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.gridControlSign = new DevExpress.XtraGrid.GridControl();
			this.gridViewSign = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Gc_Stt = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_Delete = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_Up = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_Down = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_Signer = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_Title = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_Department = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_SignTime = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_RejectTime = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_RejectReason = new DevExpress.XtraGrid.Columns.GridColumn();
			this.Gc_Add = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemText = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemCboFlow = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repositoryItemText_Enable = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.barManager1 = new DevExpress.XtraBars.BarManager();
			this.bar1 = new DevExpress.XtraBars.Bar();
			this.barButtonSave = new DevExpress.XtraBars.BarButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.gridControlSign).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.gridViewSign).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemText).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCboFlow).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemText_Enable).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
			base.SuspendLayout();
			this.layoutControl1.Controls.Add(this.gridControlSign);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 36);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(794, 265, 250, 350);
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1320, 534);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			this.gridControlSign.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
			this.gridControlSign.Location = new System.Drawing.Point(2, 2);
			this.gridControlSign.MainView = this.gridViewSign;
			this.gridControlSign.Margin = new System.Windows.Forms.Padding(4);
			this.gridControlSign.Name = "gridControlSign";
			this.gridControlSign.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[3] { this.repositoryItemText, this.repositoryItemCboFlow, this.repositoryItemText_Enable });
			this.gridControlSign.Size = new System.Drawing.Size(1316, 530);
			this.gridControlSign.TabIndex = 5;
			this.gridControlSign.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1] { this.gridViewSign });
			this.gridViewSign.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[11]
			{
				this.Gc_Stt, this.Gc_Delete, this.Gc_Up, this.Gc_Down, this.Gc_Signer, this.Gc_Title, this.Gc_Department, this.Gc_SignTime, this.Gc_RejectTime, this.Gc_RejectReason,
				this.Gc_Add
			});
			this.gridViewSign.DetailHeight = 431;
			this.gridViewSign.GridControl = this.gridControlSign;
			this.gridViewSign.Name = "gridViewSign";
			this.gridViewSign.OptionsView.ShowGroupPanel = false;
			this.gridViewSign.OptionsView.ShowIndicator = false;
			this.gridViewSign.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridViewSign_CustomUnboundColumnData);
			this.Gc_Stt.Caption = "STT";
			this.Gc_Stt.FieldName = "STT";
			this.Gc_Stt.MinWidth = 27;
			this.Gc_Stt.Name = "Gc_Stt";
			this.Gc_Stt.OptionsColumn.AllowEdit = false;
			this.Gc_Stt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.Gc_Stt.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.Gc_Stt.Visible = true;
			this.Gc_Stt.VisibleIndex = 0;
			this.Gc_Stt.Width = 40;
			this.Gc_Delete.Caption = "Delete";
			this.Gc_Delete.FieldName = "DELETE";
			this.Gc_Delete.MinWidth = 27;
			this.Gc_Delete.Name = "Gc_Delete";
			this.Gc_Delete.OptionsColumn.ShowCaption = false;
			this.Gc_Delete.OptionsFilter.AllowFilter = false;
			this.Gc_Delete.Width = 33;
			this.Gc_Up.Caption = "Up";
			this.Gc_Up.FieldName = "UP";
			this.Gc_Up.MinWidth = 27;
			this.Gc_Up.Name = "Gc_Up";
			this.Gc_Up.OptionsColumn.ShowCaption = false;
			this.Gc_Up.OptionsFilter.AllowFilter = false;
			this.Gc_Up.Width = 33;
			this.Gc_Down.Caption = "Down";
			this.Gc_Down.FieldName = "DOWN";
			this.Gc_Down.MinWidth = 27;
			this.Gc_Down.Name = "Gc_Down";
			this.Gc_Down.OptionsColumn.ShowCaption = false;
			this.Gc_Down.OptionsFilter.AllowFilter = false;
			this.Gc_Down.Width = 33;
			this.Gc_Signer.Caption = "Người ký";
			this.Gc_Signer.FieldName = "Signer";
			this.Gc_Signer.MinWidth = 27;
			this.Gc_Signer.Name = "Gc_Signer";
			this.Gc_Signer.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.Gc_Signer.Visible = true;
			this.Gc_Signer.VisibleIndex = 1;
			this.Gc_Signer.Width = 200;
			this.Gc_Title.Caption = "Chức danh/Quan hệ";
			this.Gc_Title.FieldName = "TITLE";
			this.Gc_Title.MinWidth = 27;
			this.Gc_Title.Name = "Gc_Title";
			this.Gc_Title.Visible = true;
			this.Gc_Title.VisibleIndex = 2;
			this.Gc_Title.Width = 200;
			this.Gc_Department.Caption = "Đơn vị";
			this.Gc_Department.FieldName = "DEPARTMENT_NAME";
			this.Gc_Department.MinWidth = 27;
			this.Gc_Department.Name = "Gc_Department";
			this.Gc_Department.Visible = true;
			this.Gc_Department.VisibleIndex = 3;
			this.Gc_Department.Width = 209;
			this.Gc_SignTime.Caption = "Thời gian ký";
			this.Gc_SignTime.FieldName = "SIGN_TIME_STR";
			this.Gc_SignTime.MinWidth = 27;
			this.Gc_SignTime.Name = "Gc_SignTime";
			this.Gc_SignTime.OptionsColumn.AllowEdit = false;
			this.Gc_SignTime.Visible = true;
			this.Gc_SignTime.VisibleIndex = 4;
			this.Gc_SignTime.Width = 160;
			this.Gc_RejectTime.Caption = "Ngày từ chối";
			this.Gc_RejectTime.FieldName = "REJECT_TIME_STR";
			this.Gc_RejectTime.MinWidth = 27;
			this.Gc_RejectTime.Name = "Gc_RejectTime";
			this.Gc_RejectTime.OptionsColumn.AllowEdit = false;
			this.Gc_RejectTime.Visible = true;
			this.Gc_RejectTime.VisibleIndex = 5;
			this.Gc_RejectTime.Width = 160;
			this.Gc_RejectReason.Caption = "Lý do từ chối";
			this.Gc_RejectReason.FieldName = "REJECT_REASON";
			this.Gc_RejectReason.MinWidth = 27;
			this.Gc_RejectReason.Name = "Gc_RejectReason";
			this.Gc_RejectReason.Visible = true;
			this.Gc_RejectReason.VisibleIndex = 6;
			this.Gc_RejectReason.Width = 267;
			this.Gc_Add.Caption = "Add";
			this.Gc_Add.FieldName = "ADD";
			this.Gc_Add.MinWidth = 27;
			this.Gc_Add.Name = "Gc_Add";
			this.Gc_Add.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
			this.Gc_Add.OptionsColumn.ShowCaption = false;
			this.Gc_Add.OptionsFilter.AllowFilter = false;
			this.Gc_Add.Width = 67;
			this.repositoryItemText.AutoHeight = false;
			this.repositoryItemText.Name = "repositoryItemText";
			this.repositoryItemText.ReadOnly = true;
			this.repositoryItemCboFlow.AutoHeight = false;
			this.repositoryItemCboFlow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton()
			});
			this.repositoryItemCboFlow.Name = "repositoryItemCboFlow";
			this.repositoryItemText_Enable.AutoHeight = false;
			this.repositoryItemText_Enable.Name = "repositoryItemText_Enable";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[1] { this.layoutControlItem2 });
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1320, 534);
			this.layoutControlGroup1.TextVisible = false;
			this.layoutControlItem2.Control = this.gridControlSign;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1320, 534);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[1] { this.bar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[1] { this.barButtonSave });
			this.barManager1.MaxItemId = 1;
			this.bar1.BarName = "Tools";
			this.bar1.DockCol = 0;
			this.bar1.DockRow = 0;
			this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSave)
			});
			this.bar1.Text = "Tools";
			this.bar1.Visible = false;
			this.barButtonSave.Caption = "Ctrl S";
			this.barButtonSave.Id = 0;
			this.barButtonSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
			this.barButtonSave.Name = "barButtonSave";
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
			this.barDockControlTop.Size = new System.Drawing.Size(1320, 36);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 570);
			this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
			this.barDockControlBottom.Size = new System.Drawing.Size(1320, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 36);
			this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 534);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(1320, 36);
			this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 534);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1320, 570);
			base.Controls.Add(this.layoutControl1);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "FormEmrSign";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Danh sách ký";
			base.Load += new System.EventHandler(FormEmrSign_Load);
			((System.ComponentModel.ISupportInitialize)this.layoutControl1).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.gridControlSign).EndInit();
			((System.ComponentModel.ISupportInitialize)this.gridViewSign).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemText).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemCboFlow).EndInit();
			((System.ComponentModel.ISupportInitialize)this.repositoryItemText_Enable).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlGroup1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
