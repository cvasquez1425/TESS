<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEdit_pgc_tmplt_stamp_sn.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEdit_pgc_tmplt_stamp_sn" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="closed20" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <tr>
                <td>
                    Project Group ID:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpProjectGroupId" runat="server" DataTextField="Name" DataValueField="Value" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="drpProjectGroupId"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Flat Rate:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtFlatRate" runat="server" />
                    <asp:RangeValidator ControlToValidate="txtFlatRate" Type="Currency" MinimumValue="0"
                        MaximumValue="99999999" runat="server">##.## Only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Calc Multiplier Table:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcMultiplierTable" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Multiplier Field:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcMultiplierField" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Field Type 1:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtFieldType1" runat="server" MaxLength="20" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Exec Table 1:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcExecTable1" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Exec Field 1:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcExecField1" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Exec Divisor 1:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcExecDivisor1" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCalcExecDivisor1"
                        ValidationExpression="^\d+$">## Only</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Calc Exec Rounding 1:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcExecRounding1" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCalcExecRounding1"
                        ValidationExpression="^\d+$">## Only</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="text-align: right; width: 90%; padding: 0px; margin: 0px;">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" />
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" OnClick="btnSave_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
