<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatusExpandedReadOnlyView.aspx.cs"
    Inherits="Greenspoon.Tess.Pages.StatusExpandedReadOnlyView" MasterPageFile="~/MasterPages/SingleForm.Master" %>

<asp:Content ID="Status" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ListView runat="server" ID="lvData" DataKeyNames="StatusId" EnableViewState="false">
        <LayoutTemplate>
            <table class="LV_Table">
                <thead>
                </thead>
                <tbody id="itemPlaceholder" runat="server" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                <td class="HeaderText">
                    Status Master:
                </td>
                <td class="DataTextLong">
                    <%# Eval("StatusMasterName") %>
                    [<%# Eval(" StatusMasterId")%>]
                </td>
                <td class="HeaderText">
                    County:
                </td>
                <td class="DataTextShort">
                    <%# Eval("CountyName")%>
                </td>
                <td class="HeaderText">
                    Rec Date:
                </td>
                <td class="DataTextShort">
                    <%# Eval("RecDate") %>
                </td>
                <td class="HeaderText">
                    Assignment #:
                </td>
                <td class="DataTextShort">
                    <%# Eval("AssignmentNumber")%>
                </td>
            </tr>
            <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                <td class="HeaderText">
                    Legal Name:
                </td>
                <td class="DataTextLong">
                    <%# Eval("LegalName") %>
                </td>
                <td class="HeaderText">
                    Org County:
                </td>
                <td class="DataTextShort">
                    <%# Eval("OriginalCountyName")%>
                </td>
                <td class="HeaderText">
                    Book:
                </td>
                <td class="DataTextShort">
                    <%# Eval("Book") %>
                </td>
                <td class="HeaderText">
                    Effective Date:
                </td>
                <td class="DataTextShort">
                    <%# Eval("EffectiveDate")%>
                </td>
            </tr>
            <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                <td class="HeaderText">
                    Comment:
                </td>
                <td rowspan="2" style="clear: both; white-space: normal !important;">
                    <%# Eval("Comment")%>
                </td>
                <td class="HeaderText">
                    Invoice:
                </td>
                <td class="DataTextShort">
                    <%# Eval("Invoice") %>
                </td>
                <td class="HeaderText">
                    Page:
                </td>
                <td class="DataTextShort">
                    <%# Eval("Page") %>
                </td>
                <td class="HeaderText">
                    Batch:
                </td>
                <td class="DataTextShort">
                    <%# Eval("Batch")%>
                </td>
            </tr>
            <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                <td>
                    &nbsp;
                </td>
                <td class="HeaderText">
                    Upload Batch:
                </td>
                <td class="DataTextShort">
                    <%# Eval("UploadeBatchId")%>&nbsp;
                </td>
                <td class="HeaderText">
                    Active:
                </td>
                <td class="DataTextShort">
                    <%# Eval("Active")%>
                </td>
                <td class="HeaderText">
                    By:&nbsp;
                    <%# Eval("CreatedBy")%>
                </td>
                <td class="HeaderText" style="white-space: normal;">
                    On: &nbsp;<%# Eval("CreatedDate")%>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div>
                No status found for specified record
            </div>
        </EmptyDataTemplate>
        <ItemSeparatorTemplate>
            <td colspan="12" style="height: 1px; background-color: #F2F2F2">
                &nbsp;
            </td>
        </ItemSeparatorTemplate>
    </asp:ListView>
</asp:Content>
