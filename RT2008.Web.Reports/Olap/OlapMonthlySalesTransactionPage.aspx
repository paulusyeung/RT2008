﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OlapMonthlySalesTransactionPage.aspx.cs"
    Inherits="RT2008.Web.Reports.Olap.OlapMonthlySalesTransactionPage" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraPivotGrid.Web" TagPrefix="dxpgw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        html,body 
        {
            height:99%;
        }

        #Container 
        {
            height:99%;
            min-height:99%;
        }

        html>body #outer 
        {
            height:auto
        }
    </style>
    <script type="text/javascript">
        function waitcursor() {
            document.body.style.cursor = "wait";
            return true;
        }
    </script>
</head>
<body><div id="Container">
    <form id="form1" runat="server" onsubmit="return waitcursor();">
    <div id="divOptions" style="font: 10pt Tahoma; border-right: 0; border-top: 0; border-left: 0;
        padding: 0px 0px 0px 0px;" runat="server">
        <strong>Year:&nbsp;&nbsp; </strong>&nbsp;<asp:DropDownList ID="ddlYear" runat="server">
        </asp:DropDownList>
        <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Month:&nbsp; </strong>&nbsp;<asp:DropDownList ID="ddlMonth" runat="server">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" 
            Font-Bold="True" Width="66px" />
        <br />
    </div>
    <div id="divTable" style="font: 10pt Tahoma; border-right: 0; border-top: 0; border-left: 0;
        padding: 0px 0px 0px 0px;" runat="server" visible="false">
        <table border="0" cellpadding="3" cellspacing="0">
            <tr>
                <td>
                    <strong>Export Options:</strong>
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintHeadersOnEveryPage" runat="server" Text="Print headers on every page" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintFilterHeaders" runat="server" Text="Print filter headers"
                        Checked="true" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintColumnHeaders" runat="server" Text="Print column headers"
                        Checked="true" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintRowHeaders" runat="server" Text="Print row headers" Checked="true" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintDataHeaders" runat="server" Text="Print data headers"
                        Checked="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>Export to:</strong>
                    <asp:DropDownList ID="listExportFormat" runat="server" Style="vertical-align: middle">
                        <asp:ListItem Selected="True">Pdf</asp:ListItem>
                        <asp:ListItem>Excel</asp:ListItem>
                        <asp:ListItem>Rtf</asp:ListItem>
                        <asp:ListItem>Test</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ImageButton ID="btnSaveAs" runat="server" ImageUrl="~/Resources/Icons/16x16/16_L_save.gif"
                        ToolTip="Export and save" Style="vertical-align: middle" OnClick="btnSaveAs_Click"
                        AlternateText="Save" />
                    <asp:ImageButton ID="btnOpen" runat="server" ImageUrl="~/Resources/Icons/16x16/16_L_saveOpen.gif"
                        ToolTip="Export and open" Style="vertical-align: middle" OnClick="btOpen_Click"
                        AlternateText="Open" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divPivotGrid" style="font: 10pt Tahoma; border-right: 0; border-top: 0;
        border-left: 0; padding: 0px 0px 0px 0px;" runat="server">
        <dxpgw:ASPxPivotGridExporter ID="olapMonSalesTraExporter" runat="server" ASPxPivotGridID="olapMonSalesTra" />
        <dxwpg:ASPxPivotGrid ID="olapMonSalesTra" runat="server" DataSourceID="olapSQLSource">
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
            <Images ImageFolder="~/App_Themes/Glass/{0}/">
            </Images>
            <OptionsPager RowsPerPage="30">
            </OptionsPager>
        </dxwpg:ASPxPivotGrid>
        <asp:SqlDataSource ID="olapSQLSource" runat="server" ConnectionString="<%$ ConnectionStrings:SysDb %>"
            SelectCommand="SELECT [STATUS], [TxDate], [TxType], [SHOP], [STKCODE], [APPENDIX1], [APPENDIX2], [APPENDIX3], [CLASS1], [CLASS2], [CLASS3], [CLASS4], [CLASS5], [CLASS6], [AMOUNT], [QTY], [YEAR], [DAY], [MONTH] FROM [vwWebReport_OlapSales] WHERE (([YEAR] = @YEAR) AND ([MONTH] = @MONTH))">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlYear" Name="YEAR" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="ddlMonth" Name="MONTH" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form></div>
</body>
</html>
