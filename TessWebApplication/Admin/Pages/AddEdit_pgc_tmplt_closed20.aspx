<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEdit_pgc_tmplt_closed20.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEdit_pgc_tmplt_closed20" %>

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
                    Criteria Basis Table:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaBasisTable" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Criteria Basis Field:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaBasisField" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Criteria Start Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaStartAmount" runat="server" />
                    <asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtCriteriaStartAmount" Type="Currency" MinimumValue="0"
                        MaximumValue="99999999.00" runat="server">##.## only</asp:RangeValidator>

                </td>
            </tr>
            <tr>
                <td>
                    Criteria End Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaEndAmount" runat="server" />
                    <asp:RangeValidator ID="RangeValidator3" ControlToValidate="txtCriteriaEndAmount" Type="Currency" MinimumValue="0"
                        MaximumValue="99999999.00" runat="server">##.## only</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Include:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtInclude" runat="server" MaxLength="5" />
                    <asp:RequiredFieldValidator ControlToValidate="txtInclude" runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Calc Basis Table:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcBasisTable" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc basis Field:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcBasisField" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Project Field Type:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtProjectFieldType" runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="txtProjectFieldType" runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Flat Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtFlatAmt" runat="server" />
                    <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtFlatAmt" Type="Currency" MinimumValue="0"
                        MaximumValue="99999999.00" runat="server">##.## only</asp:RangeValidator>
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
