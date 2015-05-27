<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="Greenspoon.Tess.Pages.Inventory"
    MasterPageFile="~/MasterPages/SingleForm.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="status" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <caption>
            Contract Inventory</caption>
        <tr>
            <td style="width: 30%">
                Building:
            </td>
            <td class="alt" style="border-right: 0px; width: 50%">
                <asp:DropDownList ID="drpBuilding" runat="server" Width="90%" DataTextField="Name"
                    TabIndex="1" DataValueField="Value" AutoPostBack="true" OnSelectedIndexChanged="drpBuilding_SelectedIndexChanged" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="drpBuilding"
                    ValidationGroup="i">*</asp:RequiredFieldValidator>
            </td>
            <td class="alt" style="border-left: 0px; width: 20%">
                <asp:ImageButton runat="server" ID="btnAddBuilding" ImageUrl="../Images/plus.png" TabIndex="-1"
                    AlternateText="Add" ToolTip="Add Building" CausesValidation="False" Height="20px"
                    Width="20px" OnClick="btnAddEditBuilding_Click" />
                        &nbsp;&nbsp;
<%--                        Change Request from Brian Nov 15 2013--%>
<%--                <asp:ImageButton runat="server" ID="btnEditBuilding" ImageUrl="../Images/Edit.gif" AlternateText="Add" TabIndex="-1" 
                    ToolTip="Edit Building" CausesValidation="False" Height="20px" Width="20px" OnClick="btnAddEditBuilding_Click" />--%>
            </td>
        </tr>
        <tr>
            <td>
                Unit:
            </td>
            <td class="alt" style="border-right: 0px;">
                <asp:DropDownList ID="drpUnit" runat="server" AutoPostBack="true" DataTextField="Name"
                    TabIndex="2" DataValueField="Value" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged" />
                <asp:RequiredFieldValidator runat="server" ID="rfvUnit" ControlToValidate="drpUnit"
                    ValidationGroup="i">*</asp:RequiredFieldValidator>
            </td>
            <td class="alt" style="border-left: 0px;">
                <asp:ImageButton runat="server" ID="btnAddUnit" ImageUrl="../Images/plus.png" AlternateText="Add" TabIndex="-1"
                    ToolTip="Add Unit" CausesValidation="False" Height="20px" Width="20px" OnClick="btnAddEditUnit_Click" />
                    &nbsp;&nbsp;
                    <%-- Change Request 11/15/2015--%>
<%--                <asp:ImageButton runat="server" ID="btnEditUnit" ImageUrl="../Images/Edit.gif" AlternateText="Add" TabIndex="-1" 
                    ToolTip="Edit Unit" CausesValidation="False" Height="20px" Width="20px" OnClick="btnAddEditUnit_Click" />--%>
            </td>
        </tr>
        <tr>
            <td>
                Weeks:
            </td>
            <td class="alt" colspan="2">
                <asp:DropDownList ID="drpWeeks" runat="server" DataTextField="Name" DataValueField="Value"
                    TabIndex="3" />
            </td>
        </tr>
        <tr>
            <td>
                Years:
            </td>
            <td class="alt" colspan="2">
                <asp:DropDownList ID="drpYears" runat="server" DataTextField="Name" DataValueField="Value"
                    TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="drpYears_SelectedIndexChanged" />
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="drpYears"
                    ValidationGroup="i">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                A/B/T:
            </td>
            <td class="alt" colspan="2">
                <asp:Label ID="lblABT" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Floor:
            </td>
            <td class="alt" colspan="2">
                <asp:Label ID="lblFloor" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Bedrooms:
            </td>
            <td class="alt" colspan="2">
                <asp:Label ID="lblBedroom" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Active:
            </td>
            <td class="alt" colspan="2">
                <asp:CheckBox runat="server" ID="chkActive" TabIndex="5" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table style="clear: both; width: 95%;">
                    <tr>
                        <td style="border: 0 none white;">
                            <asp:Label ID="lblMsg" EnableViewState="false" runat="server" ForeColor="red" />
                        </td>
                        <td style="border: 0 none white;">
<%--                        <td style="border: 0 none white;">
                            <div style="float: right;">
                                <ul id="dropdownButton">
                                    <li style="padding: 0 5px 5px 5px;">
                                        <div class="arrow-down">
                                        </div>
                                        <ul id="help" style="z-index: 1000 !important;">
                                            <li>--%>
                                                <asp:LinkButton ID="btnSaveNew" runat="server" 
                                Text="Save & New" OnClick="Save_Click"
                                                    CausesValidation="true" ValidationGroup="i" 
                                TabIndex="6" />&nbsp;&nbsp;
<%--                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>--%>
<%--                            <div style="float: right; padding-top: 8px;">--%>
                                <asp:LinkButton ID="btnSaveClose" runat="server" Text="Save & Close" OnClick="Save_Click"
                                    TabIndex="7" CausesValidation="true" ValidationGroup="i" />
<%--                            </div>
                            <div class="clear">--%>
<%--                            </div>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div class="divSubForm">
                    <asp:GridView ID="gvInventory" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
                        EmptyDataText="No Data">
                        <Columns>
                            <asp:BoundField DataField="Building" HeaderText="Building" ReadOnly="true" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" ReadOnly="true" />
                            <asp:BoundField DataField="Week" HeaderText="Week" ReadOnly="true" />
                            <asp:BoundField DataField="Year" HeaderText="Year" ReadOnly="true" />
                            <asp:BoundField DataField="ABT" HeaderText="A/B/T" ReadOnly="true" />
                            <asp:BoundField DataField="Floor" HeaderText="Floor" ReadOnly="true" />
                            <asp:BoundField DataField="Bedroom" HeaderText="Bedroom" ReadOnly="true" />
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
