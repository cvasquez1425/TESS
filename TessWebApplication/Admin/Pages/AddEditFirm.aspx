<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditFirm.aspx.cs" Inherits="Greenspoon.Tess.Admin.Pages.AddEditFirm"
    MasterPageFile="~/MasterPages/SingleForm.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="firm" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td>
                Developer Group Id:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtDeveloperGroupId" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Code:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Designation:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmDesignation" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Logo:
            </td>
            <td class="alt">
            </td>
        </tr>
        <tr>
            <td>
                Firm Name:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmName" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Address 1:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmAddress1" runat="server" Height="50px" 
                    TextMode="MultiLine" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Address 2:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmAddress2" runat="server" Height="50px" 
                    TextMode="MultiLine" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Reply TO:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmReplyTo" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Agent:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmAgent" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Agent Title:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmAgentTitle" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Bar Number:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmBarNumber" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Firm Gender:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirmGender" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                firm Agent Sig:
            </td>
            <td class="alt">
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
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="j"
                        OnClick="btnSave_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
