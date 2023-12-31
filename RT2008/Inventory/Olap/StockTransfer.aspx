﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockTransfer.aspx.cs"
    Inherits="RT2008.Inventory.Olap.StockTransfer" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraPivotGrid.Web" TagPrefix="dxpgw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divOptions" runat="server" visible="false">
        <div style="background-color: transparent; font: 10pt Tahoma; border-right: 0; border-top: 0;
            border-left: 0; padding: 6px 10px 6px 10px;">
            <asp:Label ID="lblFromTRN" runat="server" Text="From TRN# :" Width="110px"></asp:Label>
            <asp:TextBox ID="txtFromTRN" runat="server" Width="120px">0</asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblToTRN" runat="server" Text="To TRN# :" Width="110px"></asp:Label>
            <asp:TextBox ID="txtToTRN" runat="server" Width="120px">ZZZZZZZZZZZZ</asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblFromStk" runat="server" Text="From Stock Code :" Width="110px"></asp:Label>
            <asp:TextBox ID="txtFromStk" runat="server" Width="120px">0</asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblToStk" runat="server" Text="To Stock Code :" Width="110px"></asp:Label>
            <asp:TextBox ID="txtToStk" runat="server" Width="120px">ZZZZZZZZZZ</asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblFromData" runat="server" Text="From Data :" Width="110px"></asp:Label>
            <asp:TextBox ID="txtFromDate" runat="server" Width="120px"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="(dd/mm/yyyy)"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblToData" runat="server" Text="To Data :" Width="110px"></asp:Label>
            <asp:TextBox ID="txtToDate" runat="server" Width="120px"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="(dd/mm/yyyy)"></asp:Label>
        </div>
        <div style="background-color: transparent; font: 10pt Tahoma; border-right: 0; border-top: 0;
            border-left: 0; padding: 6px 10px 6px 10px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text="Confirmation Status" Width="135px"
                        Height="16px"></asp:Label>
                    <asp:Label ID="lblType0" runat="server" Text="Record Type"></asp:Label>
                    <br />
                    <asp:CheckBox ID="chkcompleted" runat="server" Text="Completed" Width="130px" />
                    <asp:CheckBox ID="chkPost" runat="server" Checked="True" Enabled="False" Text="Post Record" />
                    <br />
                    <asp:CheckBox ID="chkInCompleted" runat="server" Text="InCompleted" Width="130px" />
                    <asp:CheckBox ID="chkUnPost" runat="server" Text="Un-Post Record" />
                    <br />
                    <asp:CheckBox ID="chkUnprocessed" runat="server" Text="Unprocessed" Width="130px" />
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="background-color: transparent; font: 10pt Tahoma; border-right: 0; border-top: 0;
            border-left: 0; padding: 6px 10px 6px 10px;">
            <asp:Label ID="lblEmpty" runat="server" Width="130px"></asp:Label>
            <asp:Button ID="btnShow" runat="server" Text="Show" Width="75px" OnClick="btnShow_Click" />
        </div>
    </div>
    <div style="font: 10pt Tahoma; border-right: 0; border-top: 0; border-left: 0; padding: 0px 0px 0px 0px;"
        runat="server" id="divPivotGrid">
        <table border="0" cellpadding="3" cellspacing="0">
            <tr>
                <td colspan="4">
                    <strong>Export to:</strong>
                    <asp:DropDownList ID="listExportFormat" runat="server" Style="vertical-align: middle">
                        <asp:ListItem Selected="True">Pdf</asp:ListItem>
                        <asp:ListItem>Excel</asp:ListItem>
                        <asp:ListItem>Rtf</asp:ListItem>
                        <asp:ListItem>Text</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ImageButton ID="btnSaveAs" runat="server" ImageUrl="~/Resources/Icons/16x16/16_L_save.gif"
                        ToolTip="Export and save" Style="vertical-align: middle; width: 16px;" OnClick="btnSaveAs_Click"
                        AlternateText="Save" />
                    <asp:ImageButton ID="btnOpen" runat="server" ImageUrl="~/Resources/Icons/16x16/16_L_saveOpen.gif"
                        ToolTip="Export and open" Style="vertical-align: middle; width: 16px;" OnClick="btOpen_Click"
                        AlternateText="Open" />
                </td>
            </tr>
            <tr>
                <td rowspan="2" valign="top">
                    <strong>Export Options:</strong>
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintHeadersOnEveryPage" runat="server" Text="Print headers on every page" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintFilterHeaders" runat="server" Text="Print filter headers"
                        Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintColumnHeaders" runat="server" Text="Print column headers"
                        Checked="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="checkPrintRowHeaders" runat="server" Text="Print row headers" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintDataHeaders" runat="server" Text="Print data headers"
                        Checked="True" />
                </td>
            </tr>
        </table>
        <dxpgw:ASPxPivotGridExporter ID="olapStockTransferExporter" runat="server" ASPxPivotGridID="olapStockTransfer" />
        <dxwpg:ASPxPivotGrid ID="olapStockTransfer" runat="server" DataSourceID="olapSQLSource">
            <Styles CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass">
                <HeaderStyle>
                    <HoverStyle>
                        <BackgroundImage ImageUrl="~/App_Themes/Glass/PivotGrid/pgHeaderBackHot.gif" Repeat="RepeatX" />
                    </HoverStyle>
                    <BackgroundImage ImageUrl="~/App_Themes/Glass/PivotGrid/pgHeaderBack.gif" Repeat="RepeatX" />
                </HeaderStyle>
                <FilterAreaStyle>
                    <BackgroundImage ImageUrl="~/App_Themes/Glass/PivotGrid/pgFilterAreaBack.gif" Repeat="RepeatX" />
                </FilterAreaStyle>
                <FilterButtonPanelStyle>
                    <BackgroundImage ImageUrl="~/App_Themes/Glass/PivotGrid/pgFilterPanelBack.gif" Repeat="RepeatX" />
                </FilterButtonPanelStyle>
                <MenuStyle GutterWidth="0px" />
            </Styles>
            <OptionsLoadingPanel Text="Loading&amp;hellip;">
            </OptionsLoadingPanel>
            <OptionsPager RowsPerPage="100">
            </OptionsPager>
            <OptionsView ShowColumnTotals="true" ShowRowTotals="true" ShowColumnGrandTotals="true"
                ShowRowGrandTotals="true" ShowGrandTotalsForSingleValues="true" ShowTotalsForSingleValues="true" />
            <Images ImageFolder="~/App_Themes/Glass/{0}/">
            </Images>
        </dxwpg:ASPxPivotGrid>
        <asp:SqlDataSource ID="olapSQLSource" runat="server" ConnectionString="<%$ ConnectionStrings:SysDb %>"
            SelectCommand="apOlapStockTransfer" SelectCommandType="StoredProcedure" OnSelecting="olapSQLSource_OnSelecting">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtFromTRN" Name="fromNUM" PropertyName="Text" 
                    Type="String" />
                <asp:ControlParameter ControlID="txtToTRN" Name="toNUM" PropertyName="Text" 
                    Type="String" />
                <asp:ControlParameter ControlID="txtFromStk" Name="fromSTKCOCE" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtToStk" Name="toSTKCODE" PropertyName="Text" 
                    Type="String" />
                <asp:ControlParameter ControlID="txtFromData" Name="fromDate" 
                    PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="txtToData" Name="toDate" PropertyName="Text" 
                    Type="DateTime" />
                <asp:ControlParameter ControlID="chkcompleted" Name="ShowCompleted" 
                    PropertyName="Checked" Type="Boolean" />
                <asp:ControlParameter ControlID="chkInCompleted" Name="ShowIncompleted" 
                    PropertyName="Checked" Type="Boolean" />
                <asp:ControlParameter ControlID="chkUnprocessed" Name="ShowUnprocessed" 
                    PropertyName="Checked" Type="Boolean" />
                <asp:ControlParameter ControlID="chkPost" Name="ShowPostType" 
                    PropertyName="Checked" Type="Boolean" />
                <asp:ControlParameter ControlID="chkUnPost" Name="ShowUnPostType" 
                    PropertyName="Checked" Type="Boolean" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
