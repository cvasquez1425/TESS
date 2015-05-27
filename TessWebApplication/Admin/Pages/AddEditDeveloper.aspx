<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditDeveloper.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEditDeveloper" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="developer" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td>
                Developer Group:
            </td>
            <td class="alt">
                <asp:DropDownList ID="drpDevGroup" runat="server" DataTextField="Name" DataValueField="Value"
                        Width="85%" />
            </td>
        </tr>
        <tr>
            <td>
                Developer Master:
            </td>
            <td class="alt">
                <asp:DropDownList ID="drpDevMaster" runat="server" DataTextField="Name" DataValueField="Value"
                        Width="85%" />
            </td>
        </tr>
        <tr>
            <td>
                Developer Name:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtDevName" runat="server" Width="70%" MaxLength="50" />
                <asp:RequiredFieldValidator ID="rqdDevName" runat="server" ControlToValidate="txtDevName" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Address 1:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtAddress1" runat="server" Width="70%" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Address 2:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtAddress2" runat="server" Width="70%" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Address 3:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtAddress3" runat="server" Width="70%" MaxLength="50"/>
            </td>
        </tr>
        <tr>
            <td>
                Alt Address 1:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtAltAddress1" runat="server" Width="70%" MaxLength="50"/>
            </td>
        </tr>
        <tr>
            <td>
                Alt Address 2:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtAltAddress2" runat="server" Width="70%" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Alt Address 3:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtAltAddress3" runat="server" Width="70%" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Reassign:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtReassign" runat="server" />
                <asp:RegularExpressionValidator ID="revReassign" ControlToValidate="txtReassign"
                    ValidationGroup="d" runat="server" ValidationExpression="^(-)?\d+(\.\d\d)?$">##.##</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Developer PG 2:
            </td>
            <td class="alt">
                <asp:CheckBox ID="chkDevPG2" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Intro Atty:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtIntroAtty" runat="server" />
                <asp:RegularExpressionValidator ID="revIntroAtty" runat="server" ValidationExpression="^\d+$"
                    ControlToValidate="txtIntroAtty" ValidationGroup="d">##</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Billing Atty:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtBillingAtty" runat="server" />
                <asp:RegularExpressionValidator ID="revBillingAtty" runat="server" ValidationExpression="^\d+$"
                    ControlToValidate="txtBillingAtty" ValidationGroup="d">##</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Developer Text:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtDevText" runat="server" Height="65px" TextMode="MultiLine" Width="70%" MaxLength="300" />
            </td>
        </tr>
        <tr>
            <td>
                Active:
            </td>
            <td class="alt">
                <asp:CheckBox ID="chkActive" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Create Date:
            </td>
            <td class="left">
                <asp:Label ID="lblCreateDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Created By:
            </td>
            <td class="left">
                <asp:Label ID="lblCreateBy" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="text-align: right; width: 90%; padding: 0px; margin: 0px;">
                    <asp:Label ID="lblMsg" runat="server" EnableViewState="false" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="d"
                        OnClick="btnSave_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
