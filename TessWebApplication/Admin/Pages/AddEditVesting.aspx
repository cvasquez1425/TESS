<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditVesting.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEditVesting" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="vesting" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td>
                Vesting Type:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtVestingType" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtVestingType" ID="rfvVestingType"
                    ValidationGroup="v" runat="server">*</asp:RequiredFieldValidator>
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
                <asp:Label ID="lblCreateDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Created By:
            </td>
            <td class="left">
                <asp:Label ID="lblCreateBy" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="text-align: right; width: 90%; padding: 0px; margin: 0px;">
                    <asp:Label ID="lblMsg" runat="server" EnableViewState="false" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="v"
                        OnClick="btnSave_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
