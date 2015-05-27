<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="TrsExpandedView.aspx.cs" Inherits="Greenspoon.Tess.Pages.TrsExpandedView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                            <asp:Label ID="statusLbl" runat="server" Text="MasterID" Font-Bold="True"></asp:Label>&nbsp;&nbsp
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
                                Resale Foreclosure Master:
                            </td>
                            <td class="DataTextLong">
                                <%# Eval("TrId") %>
                            </td>
                            <td class="HeaderText">
                                Type:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("TypeId")%>
                            </td>
                            <td class="HeaderText">
                                Start Date:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("StartDate")%>
                            </td>
                            <td class="HeaderText">
                                Status:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("StatusId")%>
                            </td>
                        </tr>
                        <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                            <td class="HeaderText">
                                Master Id:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("ContractId")%>
                            </td>
                            <td class="HeaderText">
                                Contract Price Amt:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("ContPriceAmt")%>
                            </td>
                            <td class="HeaderText">
                                Mortgage Amt:
                            </td>
                            <td class="HeaderText">
                                <%# Eval("MortgageAmt")%>
                            </td>
                            <td class="HeaderText">
                                Document Date:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("DocPrepDate")%>
                            </td>
                        </tr>
                        <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                            <td class="HeaderText">
                                Extra Names:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("ExtraNames")%>
                            </td>
                            <td class="HeaderText">
                                Extra Pages:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("ExtraPages")%>
                            </td>
                            <td class="HeaderText">
                                By:&nbsp;
                                <%# Eval("CreatedBy")%>
                            </td>
                            <td class="HeaderText">
                                On: &nbsp;<%# Eval("CreatedDate")%>
                            </td>
                        </tr>
                        <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                            <td class="HeaderText">
                                Pending Buyer:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("IsTrPendingBuyer")%>
                            </td>
                            <td class="HeaderText">
                                Pending Seller:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("IsTrPendingSeller")%>
                            </td>
                            <td class="HeaderText">
                                Final Buyer:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("IsTrFinalBuyer")%>
                            </td>
                            <td class="HeaderText">
                                Final Seller:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("IsTrFinalSeller")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <div>
                            No Resale found for specified record
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
                        <asp:Label ID="statusLblBottom" runat="server" Font-Bold="True" Text="MasterID"></asp:Label>&nbsp;&nbsp
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <a id="btnReturnB" runat="server">Return to Record</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </ajax:UpdatePanel>
</asp:Content>
