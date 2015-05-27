<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateByEscrowKey.aspx.cs"
    Inherits="Greenspoon.Tess.Admin.Pages.UpdateByEscrowKey" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="UpdateByEscrowKey" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <caption>
                Batch Update by Escrow Key</caption>
            <tr>
                <td>
                    Status:
                </td>
                <td colspan="3" class="alt">
                    <asp:DropDownList ID="drpStatusMaster" runat="server" Width="70%" DataTextField="Name"
                        DataValueField="Value" AutoPostBack="True" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="drpStatusMaster" ID="rqdStat"
                        ValidationGroup="st">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Invoice:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtInvoice" MaxLength="20"></asp:TextBox>
                </td>
                <td>
                    Book:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtBook" MaxLength="15"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Effective Date:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtEffDate"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revMD" runat="server" ControlToValidate="txtEffDate"
                        SetFocusOnError="True" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="st"
                        ErrorMessage="Mortgage Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
                </td>
                <td>
                    Page:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtPage"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTotalDeedPages" runat="server" ControlToValidate="txtPage"
                        ValidationExpression="^\d+$" ValidationGroup="st">?</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Document Recorded:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtDocRec"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rqddr" runat="server" ControlToValidate="txtDocRec"
                        SetFocusOnError="True" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="st"
                        ErrorMessage="Mortgage Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
                </td>
                <td>
                    Assign # :
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtAssignNum" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Batch Key:
                </td>
                <td class="alt" colspan="3">
                    <asp:TextBox runat="server" ID="txtBatchKey" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBatchKey" ID="rqdBk"
                        SetFocusOnError="True" ValidationGroup="st">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rrbk" runat="server" ControlToValidate="txtBatchKey"
                        ValidationExpression="^\d+$" ValidationGroup="st">?</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    County:
                </td>
                <td colspan="3" class="alt">
                    <asp:DropDownList ID="drpCounty" runat="server" DataTextField="Name" DataValueField="Value"
                        Width="70%" />
                </td>
            </tr>
            <tr>
                <td>
                    Original County:
                </td>
                <td class="alt" colspan="3">
                    <asp:DropDownList ID="drpOriginalCounty" runat="server" Width="70%" DataTextField="Name"
                        DataValueField="Value" />
                </td>
            </tr>
            <tr>
                <td>
                    Comments:
                </td>
                <td colspan="3" class="alt">
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtComments" Height="50px" Width="70%" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: right;">
                    <asp:CheckBox ID="chkIsComment"  runat="server" Enabled="False" 
                        Visible="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblBatchMsg" runat="server" EnableViewState="false" Font-Size="XX-Small" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSaveBatch" runat="server" Text="Save"
                        ValidationGroup="st" OnClick="btnSaveBatch_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False"
                        OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
