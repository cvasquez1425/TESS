<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEdit_pgc_tmplt_tang_tax_mort.aspx.cs"
    Inherits="Greenspoon.Tess.Admin.Pages.AddEdit_pgc_tmplt_tang_tax_mort" MasterPageFile="~/MasterPages/SingleForm.Master" %>

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
                    Calc Basis Table:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcBasisTable" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Basis Field:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcBasisField" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Project Field Type:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtProjectFieldType" runat="server" MaxLength="20" />
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
                    <asp:TextBox ID="txtCalcMultiplierField" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Rounding:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtRounding" runat="server" MaxLength="50" />
                    <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtRounding" Type="Integer"
                        MinimumValue="0" MaximumValue="99999999" runat="server">## only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Flat Rate:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtFlatRate" runat="server" />
                    <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtFlatRate" Type="Currency" MinimumValue="0"
                        MaximumValue="99999999" runat="server">##.## Only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Include Rounding Calc:
                </td>
                <td class="alt">
                    <asp:CheckBox ID="chkIncludeRoundingCalc" runat="server" />
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
