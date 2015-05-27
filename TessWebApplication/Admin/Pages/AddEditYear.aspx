<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditYear.aspx.cs" Inherits="Greenspoon.Tess.Admin.Pages.AddEditYear" MasterPageFile="~/MasterPages/SingleForm.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="firm" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td>
                Year Name:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtYearName" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqpYearName" ControlToValidate="txtYearName" runat="server" ValidationGroup="y">*</asp:RequiredFieldValidator>
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
                A B T:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtABT" runat="server" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Name Abbreviation:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtNameAbbrev" runat="server" MaxLength="1"></asp:TextBox>
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
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="y"
                        OnClick="btnSave_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
