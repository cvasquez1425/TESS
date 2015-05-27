<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditJudge.aspx.cs" MasterPageFile="~/MasterPages/SingleForm.Master"
    Inherits="Greenspoon.Tess.Admin.Pages.AddEditJudge" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="partner" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <tr>
                <td>
                    County:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpCounty" runat="server" DataTextField="Name" DataValueField="Value"
                        Width="80%">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="j" ControlToValidate="drpCounty"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Room:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtRoom" runat="server" Width="80%" />
                </td>
            </tr>
            <tr>
                <td>
                    Division:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtDivision" runat="server" Width="80%" />
                </td>
            </tr>
            <tr>
                <td>
                    Judge Name:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtJudgeName" runat="server" Width="80%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="j" ControlToValidate="txtJudgeName"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Judge Last Name:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtJudgeLastName" runat="server" Width="80%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="j" ControlToValidate="txtJudgeLastName"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Phone:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtPhone" runat="server" Width="80%" />
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
                    Document Group:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpDocumentGroup" runat="server" DataTextField="Name" DataValueField="Value"
                        Width="80%">
                    </asp:DropDownList>
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
    </div>
</asp:Content>
