<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEdit_pgc_tmplt_rpt_title.aspx.cs" MasterPageFile="~/MasterPages/SingleForm.Master"
    Inherits="Greenspoon.Tess.Admin.Pages.AddEdit_pgc_tmplt_rpt_title" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="RPTTitle" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
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
                    Calc Field:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcField" runat="server" MaxLength="50" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCalcField"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Criteria Start Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaStartAmount" runat="server" />
                    <asp:RegularExpressionValidator ID="revStartAmount" runat="server" ControlToValidate="txtCriteriaStartAmount"
                        ValidationExpression="^\d*\.?\d*$">## Only</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Criteria End Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaEndAmount" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCriteriaEndAmount"
                        ValidationExpression="^\d*\.?\d*$">## Only</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Field Type:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtFieldType" runat="server" MaxLength="50" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFieldType"
                        runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Calc Value:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcValue" runat="server" MaxLength="6" />
                    <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtCalcValue" Type="Currency"
                        MinimumValue="0" MaximumValue="999.00" runat="server">##.## Only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Flat Value:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtFlatValue" runat="server" MaxLength="6" />
                    <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtFlatValue" Type="Currency"
                        MinimumValue="0" MaximumValue="999.00" runat="server">##.## Only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Use Base:
                </td>
                <td class="alt">
                    <asp:CheckBox ID="chkUseBase" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Basis:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcBasis" runat="server" MaxLength="50" />
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
