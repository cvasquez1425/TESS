<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Greenspoon.Tess.Pages.Reports" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="reports" runat="server" ContentPlaceHolderID="MainContent">
    <table width="99%">
        <tr>
            <td style="width:33%;">
                <h1>
                    <asp:Label ID="lblHeader" runat="server" /></h1>
            </td>
            <td style="width:33%; text-align:center;">
                <a id="btnReturn" runat="server" >Return to Record</a>
            </td>
            <td style="width:33%; text-align:right;">
                <asp:Label ID="lblParamLabel" runat="server" ForeColor="Red" />
                &nbsp;
                <asp:TextBox ID="txtParam" runat="server" />
            </td>

        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMsg" CssClass="error" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
                <asp:GridView ID="grdReport" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                    OnRowCommand="ReportGridView_RowCommand" Width="100%">
                    <AlternatingRowStyle BackColor="AliceBlue" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Eval("DisplayName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="PDF" runat="server" Height="25px" Width="25px" CommandArgument='<%# Eval("ReportNameLocation") %>'
                                    CommandName="PDF" ImageUrl="~/Images/icon-PDF.gif" ToolTip="Download in Adobe Acrobat PDF format" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="EXCEL" runat="server" Height="25px" Width="25px" CommandName="EXCEL"
                                    CommandArgument='<%# Eval("ReportNameLocation") %>' ImageUrl="~/Images/icon-EXCEL.jpg"
                                    ToolTip="Download in Microsoft Excel format" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="WORD" runat="server" Height="25px" Width="25px" CommandName="WORD"
                                    CommandArgument='<%# Eval("ReportNameLocation") %>' ImageUrl="~/Images/icon-WORD.gif"
                                    ToolTip="Download in Microsoft Word format" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
