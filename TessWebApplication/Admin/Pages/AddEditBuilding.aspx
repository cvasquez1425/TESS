<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditBuilding.aspx.cs"
    Inherits="Greenspoon.Tess.Admin.Pages.AddEditBuilding" MasterPageFile="~/MasterPages/SingleForm.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="firm" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <caption>
            Add Building</caption>
        <tr>
            <td>
                Building Name:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtBuildingName" runat="server" Width="70%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqdBN" runat="server" ControlToValidate="txtBuildingName"
                    ValidationGroup="j">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Building Code:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtBuildingCode" runat="server" Width="70%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBuildingCode"
                    ValidationGroup="j">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="text-align: right; width: 90%; padding: 0px; margin: 0px;">
                    <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="red" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="j"
                        OnClick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CausesValidation="False"
                        OnClick="btnCancel_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
