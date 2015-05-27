<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditPhaseDetail.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEditPhaseDetail" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="CntStatusMaster" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td style="width:10%; white-space:nowrap;">
                Project Name:</td>
            <td class="alt" style="width:90%;">
                <asp:DropDownList ID="drpProject" runat="server" Width="90%" DataTextField="Name" DataValueField="Value">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rqdProject" runat="server" ControlToValidate="drpProject" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width:30%; white-space:nowrap;">
                Phase Name:</td>
            <td class="alt">
                <asp:DropDownList ID="drpPhaseName" runat="server" Width="90%" DataTextField="Name" DataValueField="Value">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rqdPhaseName" runat="server" ControlToValidate="drpPhaseName" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width:10%; white-space:nowrap;">
                Phs or BOOK:</td>
            <td class="alt">
                <asp:TextBox ID="txtPhsOrBook" runat="server" MaxLength="10" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:10%; white-space:nowrap;">
                PHS or Page:</td>
            <td class="alt">
                <asp:TextBox ID="txtPhsOrPage" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:10%; white-space:nowrap;">
                Create Date :
            </td>
            <td class="left">
                <asp:Label ID="lblCreateDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width:10%; white-space:nowrap;">
                Created By :
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
