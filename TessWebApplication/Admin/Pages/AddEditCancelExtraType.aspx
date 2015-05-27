<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCancelExtraType.aspx.cs"
    Inherits="Greenspoon.Tess.Admin.Pages.AddEditCancelExtraType" MasterPageFile="~/MasterPages/SingleForm.Master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="partner" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <tr>
                <td style="width: 40%">
                    Cancel Extra Type Value:
                </td>
                <td class="alt" style="width: 60%;">
                    <asp:TextBox ID="txtCancelExtraTypeValue" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="et" ControlToValidate="txtCancelExtraTypeValue"
                        runat="server">*</asp:RequiredFieldValidator>
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
                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="et"
                            OnClick="btnSave_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
