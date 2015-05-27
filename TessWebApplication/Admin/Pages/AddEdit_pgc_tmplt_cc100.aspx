<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEdit_pgc_tmplt_cc100.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEdit_pgc_tmplt_cc100" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="closed20" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <tr>
                <td>
                    Criteria Basis Table
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaBasisTable" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Criteria Basis Field:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaBasisField" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Criteria Start Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaStartAmount" runat="server" MaxLength="11" />
                    <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtCriteriaStartAmount"
                        Type="Currency" MinimumValue="0" MaximumValue="99999999.00" runat="server">##.## only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Criteria End Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaEndAmount" runat="server" MaxLength="11" />
                    <asp:RangeValidator ID="RangeValidator3" ControlToValidate="txtCriteriaEndAmount"
                        Type="Currency" MinimumValue="0" MaximumValue="99999999.00" runat="server">##.## Only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Include:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtInclude" runat="server" MaxLength="5" />
                </td>
            </tr>
            <tr>
                <td>
                    Percentage
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtPercentage" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="[-+]?[0-9]*\.?[0-9]+"
                        ControlToValidate="txtPercentage" runat="server">##.## Only</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Flat Rate:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtFlatRate" runat="server" />
                    <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtFlatRate" Type="Currency"
                        MinimumValue="0" MaximumValue="999.00" runat="server">##.## Only</asp:RangeValidator>
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
                    Calc Basis Field
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcBasisField" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Rounding:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtRounding" runat="server" MaxLength="9" />
                    <asp:RangeValidator ID="RangeValidator4" ControlToValidate="txtRounding" Type="Currency"
                        MinimumValue="0" MaximumValue="99999.00" runat="server">##.## Only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Project Group ID:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpProjectGroupId" runat="server" DataTextField="Name" DataValueField="Value" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="drpProjectGroupId"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Effective Date From:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtEffectiveDateFrom" runat="server" MaxLength="9" />
                    <asp:RegularExpressionValidator ID="revRD" runat="server" ControlToValidate="txtEffectiveDateFrom"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$">?</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Effective Date To:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtEffectiveDateTo" runat="server" MaxLength="9" />
                    <asp:RegularExpressionValidator ID="revEffDT" runat="server" ControlToValidate="txtEffectiveDateTo"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$">?</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Include Rounding Calc
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
