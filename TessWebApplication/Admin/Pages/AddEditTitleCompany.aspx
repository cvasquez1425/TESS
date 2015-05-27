<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditTitleCompany.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEditTitleCompany" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="partner" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <tr>
                <td>
                    Pol Prefix:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtPolPrefix" runat="server" Width="280px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPolPrefix"
                        ValidationGroup="te" runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Title Company Name:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtTitleCompanyName" runat="server" Width="280px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitleCompanyName"
                        ValidationGroup="te" runat="server">*</asp:RequiredFieldValidator>
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
                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="te"
                            OnClick="btnSave_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
