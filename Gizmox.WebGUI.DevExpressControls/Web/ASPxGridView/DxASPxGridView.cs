#region Using

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Extensibility;
using Gizmox.WebGUI.Forms.Design;
using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.Data;
using System.Web.UI;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using DevExpress.Web.Internal;
using DevExpress.Web.Rendering;
using System.Drawing;
using DevExpress.Utils.Design;
using DevExpress.Web.Design;
using Gizmox.WebGUI.DevExpressControls.Web.ASPxClasses;

#endregion

namespace Gizmox.WebGUI.DevExpressControls.Web.ASPxGridView
{
    /// <summary>Encapsulates the methods and properties used for the ASPxGridView control.</summary>
    [DesignTimeController("Gizmox.WebGUI.Forms.Design.PlaceHolderController, Gizmox.WebGUI.Forms.Design, Version=3.0.5701.0, Culture=neutral, PublicKeyToken=dd2a1fd4d120c769")]
    [ClientController("Gizmox.WebGUI.Client.Controllers.PlaceHolderController, Gizmox.WebGUI.Client, Version=3.0.5701.0, Culture=neutral, PublicKeyToken=0fb8f99bd6cd7e23")]
    [ToolboxItem(true)]
    public class DxASPxGridView : DxASPxDataWebControl
    {
        #region C'tor

        public DxASPxGridView()
        {

        }
                
        #endregion

        #region Methods

        public void AddNewRow()
        {
            this.HostedASPxGridView.AddNewRow();
        }

        public void AutoFilterByColumn(GridViewColumn column, string value)
        {
            this.HostedASPxGridView.AutoFilterByColumn(column, value);
        }

        public virtual void BeginUpdate()
        {
            this.HostedASPxGridView.BeginUpdate();
        }

        public void CancelEdit()
        {
            this.HostedASPxGridView.CancelEdit();
        }

        public void ClearSort()
        {
            this.HostedASPxGridView.ClearSort();
        }

        public void CollapseAll()
        {
            this.HostedASPxGridView.CollapseAll();
        }

        public void CollapseRow(int visibleIndex, bool recursive)
        {
            this.HostedASPxGridView.CollapseRow(visibleIndex, recursive);
        }

        public void DeleteRow(int visibleIndex)
        {
            this.HostedASPxGridView.DeleteRow(visibleIndex);
        }

        public void Dispose()
        {
            this.HostedASPxGridView.Dispose();
        }

        public void DoRowValidation()
        {
            this.HostedASPxGridView.DoRowValidation();
        }

        public virtual void EndUpdate()
        {
            this.HostedASPxGridView.EndUpdate();
        }

        public void ExpandAll()
        {
            this.HostedASPxGridView.ExpandAll();
        }

        public void ExpandRow(int visibleIndex, bool recursive)
        {
            this.HostedASPxGridView.ExpandRow(visibleIndex, recursive);
        }

        public System.Web.UI.Control FindDetailRowTemplateControl(int visibleIndex, string id)
        {
            return this.HostedASPxGridView.FindDetailRowTemplateControl(visibleIndex, id);
        }

        public System.Web.UI.Control FindEditFormTemplateControl(string id)
        {
            return this.HostedASPxGridView.FindEditFormTemplateControl(id);
        }

        public System.Web.UI.Control FindEditRowCellTemplateControl(GridViewDataColumn gridViewDataColumn, string id)
        {
            return this.HostedASPxGridView.FindEditRowCellTemplateControl(gridViewDataColumn, id);
        }

        public System.Web.UI.Control FindEmptyDataRowTemplateControl(string id)
        {
            return this.HostedASPxGridView.FindEmptyDataRowTemplateControl(id);
        }

        public System.Web.UI.Control FindGroupRowTemplateControl(int visibleIndex, string id)
        {
            return this.HostedASPxGridView.FindGroupRowTemplateControl(visibleIndex, id);
        }

        public System.Web.UI.Control FindHeaderTemplateControl(GridViewColumn column, string id)
        {
            return this.HostedASPxGridView.FindHeaderTemplateControl(column, id);
        }

        public System.Web.UI.Control FindPreviewRowTemplateControl(int visibleIndex, string id)
        {
            return this.HostedASPxGridView.FindPreviewRowTemplateControl(visibleIndex, id);
        }

        public System.Web.UI.Control FindRowCellTemplateControl(int visibleIndex, GridViewDataColumn gridViewDataColumn, string id)
        {
            return this.HostedASPxGridView.FindRowCellTemplateControl(visibleIndex, gridViewDataColumn, id);
        }

        public System.Web.UI.Control FindRowCellTemplateControlByKey(object rowKey, GridViewDataColumn gridViewDataColumn, string id)
        {
            return this.HostedASPxGridView.FindRowCellTemplateControlByKey(rowKey, gridViewDataColumn, id);
        }

        public System.Web.UI.Control FindRowTemplateControl(int visibleIndex, string id)
        {
            return this.HostedASPxGridView.FindRowTemplateControl(visibleIndex, id);
        }

        public System.Web.UI.Control FindRowTemplateControlByKey(object rowKey, string id)
        {
            return this.HostedASPxGridView.FindRowTemplateControlByKey(rowKey, id);
        }

        public System.Web.UI.Control FindStatusBarTemplateControl(string id)
        {
            return this.HostedASPxGridView.FindStatusBarTemplateControl(id);
        }

        public System.Web.UI.Control FindTitleTemplateControl(string id)
        {
            return this.HostedASPxGridView.FindTitleTemplateControl(id);
        }

        public int FindVisibleIndexByKeyValue(object keyValue)
        {
            return this.HostedASPxGridView.FindVisibleIndexByKeyValue(keyValue);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<GridViewColumn> GetColumnsShownInHeaders()
        {
            return this.HostedASPxGridView.GetColumnsShownInHeaders();
        }

        public List<object> GetCurrentPageRowValues(params string[] fieldNames)
        {
            return this.HostedASPxGridView.GetCurrentPageRowValues(fieldNames);
        }

        public static object GetDetailRowKeyValue(System.Web.UI.Control control)
        {
            return DevExpress.Web.ASPxGridView.GetDetailRowKeyValue(control);
        }

        public static object GetDetailRowValues(System.Web.UI.Control control, params string[] fieldNames)
        {
            return DevExpress.Web.ASPxGridView.GetDetailRowValues(control, fieldNames);
        }

        public string GetGroupRowSummaryText(int visibleIndex)
        {
            return this.HostedASPxGridView.GetGroupRowSummaryText(visibleIndex);
        }

        public object GetGroupSummaryValue(int visibleIndex, ASPxSummaryItem item)
        {
            return this.HostedASPxGridView.GetGroupSummaryValue(visibleIndex, item);
        }

        public object GetMasterRowFieldValues(params string[] fieldNames)
        {
            return this.HostedASPxGridView.GetMasterRowFieldValues(fieldNames);
        }

        public object GetMasterRowKeyValue()
        {
            return this.HostedASPxGridView.GetMasterRowKeyValue();
        }

        public virtual string GetPreviewText(int visibleIndex)
        {
            return this.HostedASPxGridView.GetPreviewText(visibleIndex);
        }

        public object GetRowValues(int visibleIndex, params string[] fieldNames)
        {
            return this.HostedASPxGridView.GetRowValues(visibleIndex, fieldNames);
        }

        public object GetRowValuesByKeyValue(object keyValue, params string[] fieldNames)
        {
            return this.HostedASPxGridView.GetRowValuesByKeyValue(keyValue, fieldNames);
        }

        public List<object> GetSelectedFieldValues(params string[] fieldNames)
        {
            return this.HostedASPxGridView.GetSelectedFieldValues(fieldNames);
        }

        public ReadOnlyCollection<GridViewDataColumn> GetSortedColumns()
        {
            return this.HostedASPxGridView.GetSortedColumns();
        }

        public object GetTotalSummaryValue(ASPxSummaryItem item)
        {
            return this.HostedASPxGridView.GetTotalSummaryValue(item);
        }

        public int GroupBy(GridViewColumn column)
        {
            return this.HostedASPxGridView.GroupBy(column);
        }

        public int GroupBy(GridViewColumn column, int value)
        {
            return this.HostedASPxGridView.GroupBy(column, value);
        }

        public bool IsAllowGroup(GridViewColumn column)
        {
            return this.HostedASPxGridView.IsAllowGroup(column);
        }

        public bool IsAllowSort(GridViewColumn column)
        {
            return this.HostedASPxGridView.IsAllowSort(column);
        }

        public virtual bool IsReadOnly(GridViewDataColumn column)
        {
            return this.HostedASPxGridView.IsReadOnly(column);
        }

        public bool IsRowExpanded(int visibleIndex)
        {
            return this.HostedASPxGridView.IsRowExpanded(visibleIndex);
        }

        public virtual void LoadClientLayout(string layoutData)
        {
            this.HostedASPxGridView.LoadClientLayout(layoutData);
        }

        public virtual string SaveClientLayout()
        {
            return this.HostedASPxGridView.SaveClientLayout();
        }

        public ColumnSortOrder SortBy(GridViewColumn column, ColumnSortOrder value)
        {
            return this.HostedASPxGridView.SortBy(column, value);
        }

        public int SortBy(GridViewColumn column, int value)
        {
            return this.HostedASPxGridView.SortBy(column, value);
        }

        public void StartEdit(int visibleIndex)
        {
            this.HostedASPxGridView.StartEdit(visibleIndex);
        }

        public void UnGroup(GridViewColumn column)
        {
            this.HostedASPxGridView.UnGroup(column);
        }

        public void UpdateEdit()
        {
            this.HostedASPxGridView.UpdateEdit();
        }
        
        #endregion

        #region Events

    [Category("Events")]
    public event ASPxGridViewEditorCreateEventHandler AutoFilterCellEditorCreate
    {
        add
        {
            this.AddEventHandler("AutoFilterCellEditorCreate", value);
        }
        remove
        {
            this.RemoveEventHandler("AutoFilterCellEditorCreate", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewEditorEventHandler AutoFilterCellEditorInitialize
    {
        add
        {
            this.AddEventHandler("AutoFilterCellEditorInitialize", value);
        }
        remove
        {
            this.RemoveEventHandler("AutoFilterCellEditorInitialize", value);
        }
    }

    [Category("Events")]
    public event EventHandler BeforePerformDataSelect
    {
        add
        {
            this.AddEventHandler("BeforePerformDataSelect", value);
        }
        remove
        {
            this.RemoveEventHandler("BeforePerformDataSelect", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewEditorEventHandler CellEditorInitialize
    {
        add
        {
            this.AddEventHandler("CellEditorInitialize", value);
        }
        remove
        {
            this.RemoveEventHandler("CellEditorInitialize", value);
        }
    }

    [Category("Events")]
    public event ASPxClientLayoutHandler ClientLayout
    {
        add
        {
            this.AddEventHandler("ClientLayout", value);
        }
        remove
        {
            this.RemoveEventHandler("ClientLayout", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewCustomCallbackEventHandler CustomCallback
    {
        add
        {
            this.AddEventHandler("CustomCallback", value);
        }
        remove
        {
            this.RemoveEventHandler("CustomCallback", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewColumnDisplayTextEventHandler CustomColumnDisplayText
    {
        add
        {
            this.AddEventHandler("CustomColumnDisplayText", value);
        }
        remove
        {
            this.RemoveEventHandler("CustomColumnDisplayText", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewCustomDataCallbackEventHandler CustomDataCallback
    {
        add
        {
            this.AddEventHandler("CustomDataCallback", value);
        }
        remove
        {
            this.RemoveEventHandler("CustomDataCallback", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewCustomErrorTextEventHandler CustomErrorText
    {
        add
        {
            this.AddEventHandler("CustomErrorText", value);
        }
        remove
        {
            this.RemoveEventHandler("CustomErrorText", value);
        }
    }

    [Category("Events")]
    public event CustomSummaryEventHandler CustomSummaryCalculate
    {
        add
        {
            this.AddEventHandler("CustomSummaryCalculate", value);
        }
        remove
        {
            this.RemoveEventHandler("CustomSummaryCalculate", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewColumnDataEventHandler CustomUnboundColumnData
    {
        add
        {
            this.AddEventHandler("CustomUnboundColumnData", value);
        }
        remove
        {
            this.RemoveEventHandler("CustomUnboundColumnData", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewDetailRowButtonEventHandler DetailRowGetButtonVisibility
    {
        add
        {
            this.AddEventHandler("DetailRowGetButtonVisibility", value);
        }
        remove
        {
            this.RemoveEventHandler("DetailRowGetButtonVisibility", value);
        }
    }

    [Category("Events")]
    public event EventHandler DetailsChanged
    {
        add
        {
            this.AddEventHandler("DetailsChanged", value);
        }
        remove
        {
            this.RemoveEventHandler("DetailsChanged", value);
        }
    }

    [Category("Events")]
    public event EventHandler FocusedRowChanged
    {
        add
        {
            this.AddEventHandler("FocusedRowChanged", value);
        }
        remove
        {
            this.RemoveEventHandler("FocusedRowChanged", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewTableDataCellEventHandler HtmlDataCellPrepared
    {
        add
        {
            this.AddEventHandler("HtmlDataCellPrepared", value);
        }
        remove
        {
            this.RemoveEventHandler("HtmlDataCellPrepared", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewTableRowEventHandler HtmlRowCreated
    {
        add
        {
            this.AddEventHandler("HtmlRowCreated", value);
        }
        remove
        {
            this.RemoveEventHandler("HtmlRowCreated", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewTableRowEventHandler HtmlRowPrepared
    {
        add
        {
            this.AddEventHandler("HtmlRowPrepared", value);
        }
        remove
        {
            this.RemoveEventHandler("HtmlRowPrepared", value);
        }
    }

    [Category("Events")]
    public event ASPxDataInitNewRowEventHandler InitNewRow
    {
        add
        {
            this.AddEventHandler("InitNewRow", value);
        }
        remove
        {
            this.RemoveEventHandler("InitNewRow", value);
        }
    }

    [Category("Events")]
    public event EventHandler PageIndexChanged
    {
        add
        {
            this.AddEventHandler("PageIndexChanged", value);
        }
        remove
        {
            this.RemoveEventHandler("PageIndexChanged", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewAutoFilterEventHandler ProcessColumnAutoFilter
    {
        add
        {
            this.AddEventHandler("ProcessColumnAutoFilter", value);
        }
        remove
        {
            this.RemoveEventHandler("ProcessColumnAutoFilter", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewRowCommandEventHandler RowCommand
    {
        add
        {
            this.AddEventHandler("RowCommand", value);
        }
        remove
        {
            this.RemoveEventHandler("RowCommand", value);
        }
    }

    [Category("Events")]
    public event ASPxDataDeletedEventHandler RowDeleted
    {
        add
        {
            this.AddEventHandler("RowDeleted", value);
        }
        remove
        {
            this.RemoveEventHandler("RowDeleted", value);
        }
    }

    [Category("Events")]
    public event ASPxDataDeletingEventHandler RowDeleting
    {
        add
        {
            this.AddEventHandler("RowDeleting", value);
        }
        remove
        {
            this.RemoveEventHandler("RowDeleting", value);
        }
    }

    [Category("Events")]
    public event ASPxDataInsertedEventHandler RowInserted
    {
        add
        {
            this.AddEventHandler("RowInserted", value);
        }
        remove
        {
            this.RemoveEventHandler("RowInserted", value);
        }
    }

    [Category("Events")]
    public event ASPxDataInsertingEventHandler RowInserting
    {
        add
        {
            this.AddEventHandler("RowInserting", value);
        }
        remove
        {
            this.RemoveEventHandler("RowInserting", value);
        }
    }

    [Category("Events")]
    public event ASPxDataUpdatedEventHandler RowUpdated
    {
        add
        {
            this.AddEventHandler("RowUpdated", value);
        }
        remove
        {
            this.RemoveEventHandler("RowUpdated", value);
        }
    }

    [Category("Events")]
    public event ASPxDataUpdatingEventHandler RowUpdating
    {
        add
        {
            this.AddEventHandler("RowUpdating", value);
        }
        remove
        {
            this.RemoveEventHandler("RowUpdating", value);
        }
    }

    [Category("Events")]
    public event ASPxDataValidationEventHandler RowValidating
    {
        add
        {
            this.AddEventHandler("RowValidating", value);
        }
        remove
        {
            this.RemoveEventHandler("RowValidating", value);
        }
    }

    [Category("Events")]
    public event EventHandler SelectionChanged
    {
        add
        {
            this.AddEventHandler("SelectionChanged", value);
        }
        remove
        {
            this.RemoveEventHandler("SelectionChanged", value);
        }
    }

    [Category("Events")]
    public event ASPxStartRowEditingEventHandler StartRowEditing
    {
        add
        {
            this.AddEventHandler("StartRowEditing", value);
        }
        remove
        {
            this.RemoveEventHandler("StartRowEditing", value);
        }
    }

    [Category("Events")]
    public event ASPxGridViewSummaryDisplayTextEventHandler SummaryDisplayText
    {
        add
        {
            this.AddEventHandler("SummaryDisplayText", value);
        }
        remove
        {
            this.RemoveEventHandler("SummaryDisplayText", value);
        }
    }

	#endregion    

        #region Properties

        [AutoFormatDisable, Description("Gets or sets whether columns are automatically created for all fields in the underlying data source."), DefaultValue(true), Category("Data")]
        public bool AutoGenerateColumns
        {
            get
            {
                return (bool)this.GetProperty("AutoGenerateColumns");
            }
            set
            {
                this.SetProperty("AutoGenerateColumns", value);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get
            {
                return (Color)this.GetProperty("BackColor");
            }
            set
            {
                this.SetProperty("BackColor", value);
            }
        }

        [DefaultValue(""), Description("Gets or sets the ASPxGridView's client programmatic identifier."), Category("Client-Side"), AutoFormatDisable]
        public string ClientInstanceName
        {
            get
            {
                return (string)this.GetProperty("ClientInstanceName");
            }
            set
            {
                this.SetProperty("ClientInstanceName", value);
            }
        }

        [Description("Gets an object that lists the client-side events specific to the ASPxGridView."), AutoFormatDisable, Category("Client-Side"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), MergableProperty(false)]
        public GridViewClientSideEvents ClientSideEvents
        {
            get
            {
                return (GridViewClientSideEvents)this.GetProperty("ClientSideEvents");
            }
        }

        [Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DefaultValue((string)null), Description("Provides access to an ASPxGridView's column collection."), MergableProperty(false), PersistenceMode(PersistenceMode.InnerProperty), TypeConverter(typeof(UniversalCollectionTypeConverter)), AutoFormatDisable]
        public GridViewColumnCollection Columns
        {
            get
            {
                return (GridViewColumnCollection)this.GetProperty("Columns");
            }
        }

        [Browsable(false)]
        public override string CssFilePath
        {
            get
            {
                return (string)this.GetProperty("CssFilePath");
            }
            set
            {
                this.SetProperty("CssFilePath", value);
            }
        }

        [Browsable(false)]
        public override string CssPostfix
        {
            get
            {
                return (string)this.GetProperty("CssPostfix");
            }
            set
            {
                this.SetProperty("CssPostfix", value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public WebDataProxy DataBoundProxy
        {
            get
            {
                return (WebDataProxy)this.GetProperty("DataBoundProxy");
            }
        }

        [Browsable(false)]
        public WebDataDetailRows DetailRows
        {
            get
            {
                return (WebDataDetailRows)this.GetProperty("DetailRows");
            }
        }

        [Browsable(false)]
        public int EditingRowVisibleIndex
        {
            get
            {
                return (int)this.GetProperty("EditingRowVisibleIndex");
            }
        }

        [Category("Behavior"), Description("Gets or sets a value that specifies whether the callback or postback technology is used to manage round trips to the server."), DefaultValue(true), AutoFormatDisable]
        public bool EnableCallBacks
        {
            get
            {
                return (bool)this.GetProperty("EnableCallBacks");
            }
            set
            {
                this.SetProperty("EnableCallBacks", value);
            }
        }

        [Browsable(false)]
        public override bool EnableDefaultAppearance
        {
            get
            {
                return (bool)this.GetProperty("EnableDefaultAppearance");
            }
            set
            {
                this.SetProperty("EnableDefaultAppearance", value);
            }
        }

        [DefaultValue(true), AutoFormatDisable, Category("Behavior"), Description("Gets or sets whether rows cache is enabled.")]
        public bool EnableRowsCache
        {
            get
            {
                return (bool)this.GetProperty("EnableRowsCache");
            }
            set
            {
                this.SetProperty("EnableRowsCache", value);
            }
        }

        [DefaultValue(""), Browsable(false)]
        public string FilterExpression
        {
            get
            {
                return (string)this.GetProperty("FilterExpression");
            }
            set
            {
                this.SetProperty("FilterExpression", value);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FocusedRowIndex
        {
            get
            {
                return (int)this.GetProperty("FocusedRowIndex");
            }
            set
            {
                this.SetProperty("FocusedRowIndex", value);
            }
        }

        [Browsable(false)]
        public int GroupCount
        {
            get
            {
                return (int)this.GetProperty("GroupCount");
            }
        }

        [AutoFormatDisable, DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Provides access to group summary items."), TypeConverter(typeof(UniversalCollectionTypeConverter)), PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), MergableProperty(false), Category("Data")]
        public ASPxSummaryItemCollection GroupSummary
        {
            get
            {
                return (ASPxSummaryItemCollection)this.GetProperty("GroupSummary");
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Unit Height
        {
            get
            {
                return (Unit)this.GetProperty("Height");
            }
            set
            {
                this.SetProperty("Height", value);
            }
        }

        [Description("Provides access to the settings that define images displayed within the ASPxGridView's elements."), Category("Images"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridViewImages Images
        {
            get
            {
                return (GridViewImages)this.GetProperty("Images");
            }
        }

        [Description("Provides access to the settings that define images displayed within the ASPxGridView's editors."), Category("Images"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public GridViewEditorImages ImagesEditors
        {
            get
            {
                return (GridViewEditorImages)this.GetProperty("ImagesEditors");
            }
        }

        [Browsable(false)]
        public bool IsEditing
        {
            get
            {
                return (bool)this.GetProperty("IsEditing");
            }
        }

        [Browsable(false)]
        public virtual bool IsLockUpdate
        {
            get
            {
                return (bool)this.GetProperty("IsLockUpdate");
            }
        }

        [Browsable(false)]
        public bool IsNewRowEditing
        {
            get
            {
                return (bool)this.GetProperty("IsNewRowEditing");
            }
        }

        //[DefaultValue(""), Description("Gets or sets the name of the data source key field."), Category("Data"), TypeConverter(typeof(GridViewFieldConverter)), AutoFormatDisable]
        //public string KeyFieldName
        //{
        //    get
        //    {
        //        return (string)this.GetProperty("KeyFieldName");
        //    }
        //    set
        //    {
        //        this.SetProperty("KeyFieldName", value);
        //    }
        //}

        [Browsable(false)]
        public int PageCount
        {
            get
            {
                return (int)this.GetProperty("PageCount");
            }
        }

        [Browsable(false), DefaultValue(0), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PageIndex
        {
            get
            {
                return (int)this.GetProperty("PageIndex");
            }
            set
            {
                this.SetProperty("PageIndex", value);
            }
        }

        //[Description("Gets or sets the name of the data source field whose values are displayed within preview rows."), TypeConverter(typeof(GridViewFieldConverter)), DefaultValue(""), Category("Data"), AutoFormatDisable]
        //public string PreviewFieldName
        //{
        //    get
        //    {
        //        return (string)this.GetProperty("PreviewFieldName");
        //    }
        //    set
        //    {
        //        this.SetProperty("PreviewFieldName", value);
        //    }
        //}

        [Browsable(false)]
        public WebDataSelection Selection
        {
            get
            {
                return (WebDataSelection)this.GetProperty("Selection");
            }
        }

        [Description("Provides access to the ASPxGridView's display options."), AutoFormatDisable, Category("Settings"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ASPxGridViewSettings Settings
        {
            get
            {
                return (ASPxGridViewSettings)this.GetProperty("Settings");
            }
        }

        [Category("Settings"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Provides access to the control's behavior settings."), AutoFormatDisable, PersistenceMode(PersistenceMode.InnerProperty)]
        public ASPxGridViewBehaviorSettings SettingsBehavior
        {
            get
            {
                return (ASPxGridViewBehaviorSettings)this.GetProperty("SettingsBehavior");
            }
        }

        [AutoFormatDisable, DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Provides access to the control's cookie settings."), Category("Settings"), PersistenceMode(PersistenceMode.InnerProperty)]
        public ASPxGridViewCookiesSettings SettingsCookies
        {
            get
            {
                return (ASPxGridViewCookiesSettings)this.GetProperty("SettingsCookies");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Provides access to the Customization Window's settings."), Category("Settings"), AutoFormatDisable, PersistenceMode(PersistenceMode.InnerProperty)]
        public ASPxGridViewCustomizationWindowSettings SettingsCustomizationWindow
        {
            get
            {
                return (ASPxGridViewCustomizationWindowSettings)this.GetProperty("SettingsCustomizationWindow");
            }
        }

        [Description("Provides access to the ASPxGridView's master-detail options."), AutoFormatDisable, PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Settings")]
        public ASPxGridViewDetailSettings SettingsDetail
        {
            get
            {
                return (ASPxGridViewDetailSettings)this.GetProperty("SettingsDetail");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Provides access to the ASPxGridView's editing settings."), Category("Settings"), AutoFormatDisable, PersistenceMode(PersistenceMode.InnerProperty)]
        public ASPxGridViewEditingSettings SettingsEditing
        {
            get
            {
                return (ASPxGridViewEditingSettings)this.GetProperty("SettingsEditing");
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Provides access to the Loading Panel's settings."), Category("Settings"), AutoFormatDisable]
        public ASPxGridViewLoadingPanelSettings SettingsLoadingPanel
        {
            get
            {
                return (ASPxGridViewLoadingPanelSettings)this.GetProperty("SettingsLoadingPanel");
            }
        }

        [Category("Settings"), Description("Provides access to the Pager's settings."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public ASPxGridViewPagerSettings SettingsPager
        {
            get
            {
                return (ASPxGridViewPagerSettings)this.GetProperty("SettingsPager");
            }
        }

        [Description("Provides access to the control's text settings."), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), AutoFormatDisable, Category("Settings")]
        public ASPxGridViewTextSettings SettingsText
        {
            get
            {
                return (ASPxGridViewTextSettings)this.GetProperty("SettingsText");
            }
        }

        [Browsable(false)]
        public int SortCount
        {
            get
            {
                return (int)this.GetProperty("SortCount");
            }
        }

        [Description("Provides access to the style settings that control the appearance of the ASPxGridView elements."), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Styles")]
        public GridViewStyles Styles
        {
            get
            {
                return (GridViewStyles)this.GetProperty("Styles");
            }
        }

        [Category("Styles"), Description("Provides access to style settings used to paint ASPxGridView's editors."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public GridViewEditorStyles StylesEditors
        {
            get
            {
                return (GridViewEditorStyles)this.GetProperty("StylesEditors");
            }
        }

        [Description("Provides access to the style settings that control the appearance of the Pager displayed within the ASPxGridView."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty)]
        public GridViewPagerStyles StylesPager
        {
            get
            {
                return (GridViewPagerStyles)this.GetProperty("StylesPager");
            }
        }

        [Category("Templates"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false), PersistenceMode(PersistenceMode.InnerProperty)]
        public GridViewTemplates Templates
        {
            get
            {
                return (GridViewTemplates)this.GetProperty("Templates");
            }
        }

        [MergableProperty(false), DefaultValue((string)null), AutoFormatDisable, TypeConverter(typeof(UniversalCollectionTypeConverter)), Category("Data"), Description("Provides access to total summary items."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public ASPxSummaryItemCollection TotalSummary
        {
            get
            {
                return (ASPxSummaryItemCollection)this.GetProperty("TotalSummary");
            }
        }

        [Browsable(false)]
        public ReadOnlyCollection<GridViewColumn> VisibleColumns
        {
            get
            {
                return (ReadOnlyCollection<GridViewColumn>)this.GetProperty("VisibleColumns");
            }
        }

        [Browsable(false)]
        public int VisibleRowCount
        {
            get
            {
                return (int)this.GetProperty("VisibleRowCount");
            }
        }

        #endregion

        #region AspControl Box members

        /// <summary>
        /// Return 
        /// </summary>
        protected override Type HostedControlType
        {
            get
            {
                return typeof(DevExpress.Web.ASPxGridView);
            }
        }

        /// <summary>
        /// Returns the hosted DevExpress aspx grid.
        /// </summary>
        protected DevExpress.Web.ASPxGridView HostedASPxGridView
        {
            get
            {
                return (DevExpress.Web.ASPxGridView)base.HostedControl;
            }
        }

        //protected override bool IsStatelessHostedControl
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        #endregion
    }
}
