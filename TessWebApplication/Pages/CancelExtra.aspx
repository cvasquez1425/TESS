<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancelExtra.aspx.cs" Inherits="Greenspoon.Tess.Pages.CancelExtra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cancel Extra</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="overflow: hidden">
    <div>
        <table class="table">
            <caption>
                Cancel Extra</caption>
            <tr>
                <td>
                    Extra Type:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpExtraType" runat="server" DataTextField="Name" DataValueField="Value">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvExtraType" runat="server" ValidationGroup="ce" ControlToValidate="drpExtraType">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Names:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revNames" runat="server" ValidationGroup="ce" ControlToValidate="txtNames" Font-Size="X-Small"
                        ValidationExpression="^\d+$"  >Number Only</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Pages:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtPages" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revPages" runat="server" ValidationGroup="ce" ControlToValidate="txtPages" Font-Size="X-Small"
                        ValidationExpression="^\d+$"  >Number Only</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Cancel ID:
                </td>
                <td class="alt">
                    <asp:Label ID="lblCancelId" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblMsg" EnableViewState="false" runat="server"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click" ValidationGroup="ce" CausesValidation="true" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
