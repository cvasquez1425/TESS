<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Theme="Default"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Pages.Transactions" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="transaction" runat="server" ContentPlaceHolderID="MainContent">
    <div style="overflow: hidden !important;">
        <table class="table">
            <caption>
                Contract Transaction</caption>
            <tr>
                <td>
                    Amount Type:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpAmoutType" runat="server" DataTextField="Name" DataValueField="Value" Width="180px"
                       >
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvAmountType" ControlToValidate="drpAmoutType" ValidationGroup="t"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtAmount" runat="server"  />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAmount"
                        ValidationGroup="t" runat="server">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revDefaultBalance" runat="server" ControlToValidate="txtAmount"
                        ValidationExpression="^(-)?\d+(\.\d\d)?$" ValidationGroup="t">##.##</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Effective From:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtEffFrm" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEffFrm"
                        ValidationGroup="t" runat="server">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEffFrm"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="t">MM/DD/YYYY</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Effective To:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtEffTo" runat="server"  />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEffTo"
                        ValidationGroup="t" runat="server">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revFO" runat="server" ControlToValidate="txtEffTo"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="t">MM/DD/YYYY</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Create Date:
                </td>
                <td class="alt">
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    User:
                </td>
                <td class="alt">
                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblMsg" EnableViewState="false" runat="server"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click" CausesValidation="true"
                        ValidationGroup="t" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
