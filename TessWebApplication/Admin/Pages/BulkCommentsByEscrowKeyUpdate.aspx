<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="BulkCommentsByEscrowKeyUpdate.aspx.cs" Inherits="Greenspoon.Tess.Admin.Pages.BulkCommentsByEscrowKeyUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table class="table">
            <caption>
                Bulk Update Comments By Escrow Key
            </caption>
            <tr>
                <td>
                    Status Code:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpStatusMaster" runat="server" DataValueField="Value" DataTextField="Name"
                        Width="70%" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td>
                    County:
                </td>
                <td class="alt">
                    <asp:DropDownList Style="width: 200px" ID="drpOriginalCounty" runat="server" DataValueField="Value"
                        DataTextField="Name" Width="70%" TabIndex="2">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Effective Date:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtEffectiveDate" TabIndex="3" />
                    <asp:RequiredFieldValidator ControlToValidate="txtEffectiveDate" runat="server" ID="tqdrd"
                        ValidationGroup="ri">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revRD" runat="server" ControlToValidate="txtEffectiveDate"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="ri">?</asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Button ID="btnAddEK" runat="server" Text="Go" ValidationGroup="ri" OnClick="btnAddEscComments_Click"
                        TabIndex="4" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvEscInfo" runat="server" AutoGenerateColumns="False" Font-Size="XX-Small"
            Width="914px" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="Escrow Key">
                    <ItemTemplate>
                        <asp:TextBox ID="txtEscrowKey" runat="server" />
                        <asp:RegularExpressionValidator ID="revEsc" runat="server" ControlToValidate="txtEscrowKey"
                            ValidationGroup="ri" ValidationExpression="^\d+$" TabIndex="-1">?</asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comment">
                    <ItemTemplate>
                        <asp:TextBox Style="width: 500px" ID="txtComments" runat="server" />
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <FooterTemplate>
                        <asp:Button ID="btnAdd" runat="server" Text="Add New Row" OnClick="btnAdd_Click1" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <table style="width: 95%;">
            <tr>
                <td style="width: 95%; text-align: center;">
                    <asp:Label runat="server" ID="lblMsg" EnableViewState="False" />
                </td>
                <td style="width: 5%">
                    <asp:Button ID="btnClear" runat="server" Text="Reset" OnClick="btnClear_Click" ValidationGroup="ri"
                        Enabled="False" />
                </td>
                <td style="width: 5%;">
                    <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click"
                        Enabled="False" ValidationGroup="ri" />
                </td>
            </tr>
        </table>
    </div>
    <script src="../../Scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //            Default Cursor to Status Code Enhancements 2015 item 2
            //            ThickBox displays the iframe's container by calling a method in the onload event of the iframe element. 
            //            Since the iframe is hidden on the parent page until after the iframe's content is loaded you cannot set the focus simply using $(document).ready(..focus);. 
            //            The easiest way I've found to get around this is to use setTimeout to delay the function call that sets the focus until after the iframe is displayed:
            //            setTimeout(function () {
            //                $('#MainContent_drpStatusMaster').focus();
            //            }, 200);
        });
    </script>
</asp:Content>
