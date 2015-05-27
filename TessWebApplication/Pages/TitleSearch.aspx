<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TitleSearch.aspx.cs" MasterPageFile="~/MasterPages/SingleForm.Master"
    Inherits="Greenspoon.Tess.Pages.TitleSearch" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="TitleSearch" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(function () {
            $("#MainContent_txtProjectIdDb").attr("disabled", "disabled");
            $("#MainContent_txtPhaseId").attr("disabled", "disabled");

            $('select[id$=drpProject]').change(function () {
                var $selectedvalue = $('select[id$=drpProject]').val();         //selected value
                if ($selectedvalue > 0) {
                    $("#MainContent_txtProjectIdDb").removeAttr("disabled");
                    $("#MainContent_txtPhaseId").removeAttr("disabled");
                }
                else {
                    $("#MainContent_txtProjectIdDb").attr("disabled", "disabled");
                    $("#MainContent_txtPhaseId").attr("disabled", "disabled");
                }
                //            alert($selectedvalue);
            });
        });
    </script>
    <div style="width: 100%; text-align: center; overflow: auto; background: white !important;">
        <div style="width: 98%; display: block; margin-left: auto; margin-right: auto;">
            <table class="table">
                <tr>
                    <td>
                        Project:
                    </td>
                    <td colspan="3" class="alt">
                        <asp:DropDownList ID="drpProject" runat="server" Width="450px" DataTextField="Name"
                            DataValueField="Value" TabIndex="1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Project Id Database:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtProjectIdDb" runat="server" TabIndex="9"></asp:TextBox>
                    </td>
                    <td>
                        Phase Id:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtPhaseId" runat="server" TabIndex="17"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtName" runat="server" TabIndex="10" />
                    </td>
                    <td>
                        Building Code:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtBuildingCode" runat="server" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Master ID:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtMasterId" runat="server" TabIndex="11" />
                        <asp:RegularExpressionValidator ID="revMasterId" runat="server" ControlToValidate="txtMasterId"
                            ValidationExpression="^\d+$" ValidationGroup="ts">##</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        Unit NUmber:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtUnitNumber" runat="server" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Dev K No:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtDevKNo" runat="server" TabIndex="12"></asp:TextBox>
                    </td>
                    <td>
                        WEEK:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtWeeks" runat="server" TabIndex="4"></asp:TextBox>
                        <asp:RangeValidator runat="server" ErrorMessage="Week Values Must be Between 1 and 52"
                            Text="*" ValidationGroup="ts" MinimumValue="1" MaximumValue="52" ControlToValidate="txtWeeks"
                            SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtWeeks" ValidationExpression="^\d+$"
                            ErrorMessage="Only Numeric Values are allowed" Text="*" ValidationGroup="ts"
                            SetFocusOnError="True">
                        </asp:RegularExpressionValidator>
<%--                        <asp:DropDownList ID="drpWeeks" runat="server" DataTextField="Name" DataValueField="Value"
                            TabIndex="4">
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Zip:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtZip" runat="server" TabIndex="13" />
                    </td>
                    <td>
                        Year:
                    </td>
                    <td class="alt">
                        <asp:DropDownList ID="drpYears" runat="server" DataTextField="Name" DataValueField="Value"
                            TabIndex="6" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Active Master:
                    </td>
                    <td class="alt" style="height: 25px">
                        <asp:CheckBox ID="chkActive" runat="server" TabIndex="14" />
                    </td>
                    <td class="style1">
                        Deed Book:
                    </td>
                    <td class="alt" style="height: 25px">
                        <asp:TextBox ID="txtDeedBook" runat="server" TabIndex="7"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Alt Bldg:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtAltBldg" runat="server" TabIndex="15"></asp:TextBox>
                    </td>
                    <td>
                        Deed Page:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtDeedPage" runat="server" TabIndex="8" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Color:
                    </td>
                    <td class="alt">
                        <asp:TextBox ID="txtColor" runat="server" TabIndex="16"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td class="alt">
                        &nbsp;
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
                                        OnClick="btnSearch_Click" TabIndex="5" />&nbsp;
                                </td>
                                <td align="right" style="border: 0px none white;">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="false" OnClick="btnReset_Click"
                                        TabIndex="18" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divSubForm" style="height: 280px;">
            <div style="width: 98%; display: block; margin-left: auto; margin-right: auto; background: white !important;">
                <asp:ListView runat="server" ID="lvData" EnableViewState="true" DataKeyNames="contract_id"
                    OnItemCommand="TitleSearchListView_OnItemCommand" OnItemDataBound="lvData_ItemDataBound">
                    <LayoutTemplate>
                        <table class="LV_TableTS">
                            <thead>
                            </thead>
                            <tbody id="itemPlaceholder" runat="server" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                            <td class="HeaderText">
                                Project:
                            </td>
                            <td class="DataTextLong">
                                <%# Eval("project_name")%>
                            </td>
                            <td class="HeaderText">
                                Phase:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("phase_name")%>
                            </td>
                            <td class="HeaderText">
                                Master ID:
                            </td>
                            <td class="DataTextShort">
                                <asp:LinkButton runat="server" ID="lnkContractId" Text='<%# Eval("contract_id")%>'
                                    CommandName="GoToBatchEscrow" CommandArgument=' <%# Eval("batch_escrow_id")%>' />
                            </td>
                            <td class="HeaderText">
                                Cancelled:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("cancelled") %>
                            </td>
                            <td class="HeaderText">
                                Bankrupt:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("bankrupt")%>
                            </td>
                            <td class="HeaderText">
                                FC Hold:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("FCHold")%>
                            </td>
                        </tr>
                        <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                            <td class="HeaderText">
                                Name:
                            </td>
                            <td class="DataTextLong">
                                <%# Eval("first_name")%>
                                <%# Eval("last_name")%>
                            </td>
                            <td class="HeaderText">
                                Deed Book:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("deed_book")%>
                            </td>
                            <td class="HeaderText">
                                Deed Page:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("deed_page")%>
                            </td>
                            <td class="HeaderText">
                                Mort Page:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("mortgage_page")%>
                            </td>
                            <td class="HeaderText">
                                Fee Int:
                            </td>
                            <td class="DataTextShort">
                                <%-- <%# Eval("FeeInterest")%>--%>
                            </td>
                            <td class="HeaderText">
                                Rec Date:
                            </td>
                            <td class="DataTextShort">
                                <%# DataBinder.Eval(Container.DataItem, "deed_recording_date", "{0:M/d/yyyy}")%>
                            </td>
                        </tr>
                        <tr class="<%# Container.DisplayIndex % 2 == 0 ? "" : "a" %>">
                            <td class="HeaderText">
                                Building:
                            </td>
                            <td class="DataTextLong">
                                <%# Eval("building_code")%>
                                &nbsp;
                                <%# Eval("countywebsite")%>
                            </td>
                            <td class="HeaderText">
                                Alt Bldg:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("alternate_building")%>
                            </td>
                            <td class="HeaderText">
                                Floor:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("number_of_floors")%>
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
                        <tr>
                            <td class="HeaderText">
                                Active:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("Active")%>
                            </td>
                            <td class="HeaderText">
                                Partial Week:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("partial_week")%>
                            </td>
                            <td class="HeaderText">
                                Color:
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("color")%>
                            </td>
                            <td style="width: 100%; text-align: center;" colspan="5">
                                <%--                                <b>DEVELOPER OWNED</b> =--%>
                                <asp:Label ID="gvgLabel" runat="server" Text='<%#Eval("isGVG") %>' />
                                <%--                                <%# Eval("isGVG")%>--%>
                            </td>
                            <td class="DataTextShort">
                                <%# Eval("statsummarylink") %>
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
</asp:Content>
