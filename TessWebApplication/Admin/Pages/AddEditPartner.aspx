<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditPartner.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEditPartner" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="partner" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <tr>
                <td>
                    Project Name:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpProject" runat="server" DataTextField="Name" DataValueField="Value"
                        Width="90%">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="pt" ControlToValidate="drpProject"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Partner Name:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtPartnerName" runat="server" Width="85%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="pt" ControlToValidate="txtPartnerName"
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
                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="pt" OnClick="btnSave_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
