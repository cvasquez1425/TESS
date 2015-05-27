<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatusExpandedView.aspx.cs"
    Inherits="Greenspoon.Tess.Pages.StatusExpandedView" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Status" runat="server" ContentPlaceHolderID="MainContent">
    <ajax:UpdatePanel ID="updStat" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <script type="text/javascript">
                    function Status_Updated() {
                        //  close the popup
                        tb_remove();
                        //  refresh the update panel so we can view the changes  
                        $('#<%= this.btnLoadStatusForm.ClientID %>').click();
                    }
                </script>
                <asp:Button ID="btnLoadStatusForm" runat="server" Style="display: none" OnClick="Reload_Page" />
                <table style="padding: 0px; margin: 0px;" width="99%">
                    <tr>
                        <td style="width: 50%;">
                            <a id="btnNewStatus" title="Add Status" runat="server" class="thickbox" visible="false"
                                enableviewstate="false">+ Add Status</a>&nbsp;&nbsp&nbsp&nbsp
                            <asp:Label ID="statusLbl" runat="server" Text="MasterID" Font-Bold="True"></asp:Label>&nbsp;&nbsp
                            <asp:LinkButton ID="btnPrintBOReportTop" runat="server" Text="Print Status Report" OnClick="btnPrintBOReportTop_Click"></asp:LinkButton>
<%--                        <a id="btnPrintBOReportTop" title="Print Report" runat="server" class="thickbox" visible="false" enableviewstate="false">Print Status Report</a>--%>
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <a id="btnReturn" runat="server">Return to Record</a>
                        </td>
                    </tr>
                </table>
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
                                <a id="btnEditStatus" runat="server" class="thickbox" title='<%# Eval("StatusId", "Edit Record {0} ")%>'
                                    href='<%# string.Format("~/Pages/status.aspx?a=e&id={0}&cid={1}&form={2}&TB_iframe=true&height=600&width=550" , Eval("StatusId"), Eval("ContractId"), this.FormName ) %>'>
                                    Edit</a>
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
                            <td class="HeaderText">
                                On: &nbsp;<%# Eval("CreatedDate")%></td>
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
            </div>
            <table style="padding: 0px; margin: 0px;" width="99%">
                <tr>
                    <td style="width: 50%;">
                        <a id="btnNewStatusB" title="Add Status" runat="server" class="thickbox" visible="false"
                            enableviewstate="false">+ Add Status</a>&nbsp;&nbsp;&nbsp
                        <asp:Label ID="statusLblBottom" runat="server" Font-Bold="True" Text="MasterID"></asp:Label>&nbsp;&nbsp
                            <asp:LinkButton ID="btnPrintBOReportBottom" runat="server" Text="Print Status Report" OnClick="btnPrintBOReportBottom_Click"></asp:LinkButton>
<%--                        <a id="btnPrintBOReportBottom" runat="server" title="Print Report" class="thickbox" enableviewstate="false" visible="False">Print Status Report</a>--%>
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <a id="btnReturnB" runat="server">Return to Record</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </ajax:UpdatePanel>
</asp:Content>
