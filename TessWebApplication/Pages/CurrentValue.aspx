<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrentValue.aspx.cs" Inherits="Greenspoon.Tess.Pages.CurrentValue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Current Value</title>
        <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="overflow: hidden">
    <div>
        <asp:GridView ID="gvCurrentValue" runat="server" AutoGenerateColumns="False" EmptyDataText="No Data"
            AllowSorting="true" Font-Size="Medium">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="CancelId" Text='<%# Eval("CancelId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Master ID">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ContractId" Text='<%# Eval("ContractId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="LastName" Text='<%# Eval("LastName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Current Value">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCurrentValue" runat="server" Text='<%# Eval("CurrentValue") %>'></asp:TextBox>
<%--                            regular expression which allow both decimals as well as integers  --%>
                        <asp:RegularExpressionValidator ID="revCurrentValue" runat="server" ControlToValidate="txtCurrentValue"
                            ValidationExpression="^[1-9]\d*(\.\d+)?$" ValidationGroup="ce">?</asp:RegularExpressionValidator>     
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table style="width: 95%;">
            <tr>
                <td style="width: 95%; text-align: center;">
                    <asp:Label ID="lblMsg" runat="server" EnableViewState="False" />
                </td>
                <td style="width: 5%;">
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click" ValidationGroup="ce"
                        CausesValidation="true" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
