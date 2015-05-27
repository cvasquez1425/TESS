<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancelSearch.aspx.cs" Inherits="Greenspoon.Tess.Pages.CancelSearch" %>

<asp:content id="Content1" runat="server" contentplaceholderid="HeadContent">
</asp:content>
<asp:content id="CancelSearch" runat="server" contentplaceholderid="MainContent">
    <script type="text/javascript">
        // Change Request Erica Candeller Sep 19, 2014
        function disableEnterKey() {
            if (event.keyCode == 13) {
                event.keyCode = 9; //return the tab key
                event.cancelBubble = true;
            }
        }
    </script>
    <div style="width: 100%; text-align: center; overflow: auto; background: white !important;">
        <div style="width: 98%; display: block; margin-left: auto; margin-right: auto;">
            <div style="text-align: right; width: 100%">
                <a id="hrefReturnToRecord" title="Return to Cancel Bath Record" runat="server">Return
                    to Record</a>
            </div>
            <table class="table">
                <tr>
                    <td>
                        Project:
                    </td>
                    <td colspan="2" class="alt">
                        <asp:DropDownList ID="drpProject" runat="server" Width="450px" DataTextField="Name" Enabled="False"
                            DataValueField="Value">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: center;">
                        <strong>
                            <asp:Label ID="lblBatchCancelID" runat="server" />
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtName" runat="server" onkeydown="disableEnterKey();"/>
                    </td>
                    <td>
                        Zip:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtZip" runat="server" onkeydown="disableEnterKey();"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Master ID:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtMasterId" runat="server" onkeydown="disableEnterKey();"/>
                        <asp:RegularExpressionValidator ID="revMasterId" runat="server" ControlToValidate="txtMasterId"
                            ValidationExpression="^\d+$" ValidationGroup="ts">##</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        Active Master:
                    </td>
                    <td class="alt">
                        <asp:CheckBox ID="chkActive" runat="server" onkeydown="disableEnterKey();"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Dev K No:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtDevKNo" runat="server" onkeydown="disableEnterKey();"></asp:TextBox>
                    </td>
                    <td>
                        Alt Bldg:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtAltBldg" runat="server" onkeydown="disableEnterKey();"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Week:
                    </td>
                    <td class="alt">
                        <asp:DropDownList ID="drpWeeks" runat="server" DataTextField="Name" DataValueField="Value" onkeydown="disableEnterKey();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Year:
                    </td>
                    <td class="alt">
                        <asp:DropDownList ID="drpYears" runat="server" DataTextField="Name" DataValueField="Value" onkeydown="disableEnterKey();"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Unit Number:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtUnitNumber" runat="server" onkeydown="disableEnterKey();"></asp:TextBox>
                    </td>
                    <td>
                        Building Code:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtBuildingCode" runat="server" onkeydown="disableEnterKey();"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMsg" CssClass="error" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td align="left" style="border: 0px none white; text-align: left;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Find" CausesValidation="true" ValidationGroup="ts"
                                        OnClick="btnSearch_Click" />&nbsp;
                                </td>
                                <td align="right" style="border: 0px none white;">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" OnClick="btnReset_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="divSubForm" style="height: 280px;">
            <div style="width: 98%; display: block; margin-left: auto; margin-right: auto; background: white !important;">
                <asp:ListView runat="server" ID="lvData" EnableViewState="true" DataKeyNames="contract_id"
                    OnItemCommand="CancelSearchListView_OnItemCommand">
                    <LayoutTemplate>
                        <table class="LV_Table">
                            <thead>
                            </thead>
                            <tbody id="itemPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                            <td rowspan="2">
                             
                                <asp:LinkButton ID="lnkAddToCancel" runat="server" Text="Add" 
                                    CommandName="AddToCancelBatch" CommandArgument=' <%# Eval("batch_escrow_id")%>'  />
                            </td>
                            <td class="HeaderText">
                                Name:
                            </td>
                            <td class="DataTextLong">
                                <%# Eval("first_name")%>
                                <%# Eval("last_name")%>
                            </td>
                            <td class="DataTextShort">
                                Active: <%# Eval("Active")%>
                            </td>
                            <td class="HeaderText">
                                Building:
                            </td>
                            <td class="DataTextLong" style="white-space: nowrap;">
                                <%# Eval("building_name")%>
                                (<%# Eval("building_code")%>)
                            </td>
                            </td>
                            <td class="HeaderText">
                                Unit:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("unit_number")%>
                            </td>
                            <td class="HeaderText">
                                Week:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("week_number")%>
                            </td>
                            <td class="HeaderText">
                                Year:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("year_name")%>
                            </td>
                        </tr>
                        <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                            <td colspan="2" style="text-align: left;">
                                <a class="thickbox" title='<%# string.Format("Contract Status Detail for: {0} {1}", Eval("first_name"), Eval("last_name"))%>'
                                    href='<%# string.Format("StatusExpandedReadOnlyView.aspx?cid={0}&TB_iframe=true&height=500&width=800" , Eval("contract_id")) %>'>
                                    Show contract <%# Eval("contract_id")%> status
                                </a>
                            </td>
                            <td class="HeaderText">
                                Mort Amount:
                            </td>
                            <td class="DataTextShort">
                                <%# DataBinder.Eval(Container.DataItem, "mortgage_amt", "{0:C}")%>
                            </td>
                            <td class="HeaderText">
                                Mort Date:
                            </td>
                            <td class="DataTextShort">
                                <%# DataBinder.Eval(Container.DataItem, "mortgage_date", "{0:M/d/yyyy}")%>
                            </td>
                            <td class="HeaderText">
                                Mort Book:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("mortgage_book")%>
                            </td>
                            <td class="HeaderText">
                                Mort Page:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("mortgage_page")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <div>
                            No record found for title search. Please try again.
                        </div>
                    </EmptyDataTemplate>
                     <ItemSeparatorTemplate>
                        <td colspan="12" style="height: 1px; background-color: #F2F2F2">
                            &nbsp;
                        </td>
                    </ItemSeparatorTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
</asp:content>
