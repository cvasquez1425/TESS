<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEdit_pgc_tmplt_premium.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEdit_pgc_tmplt_premium" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="partner" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <tr>
                <td>
                    Project Group ID:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpProjectGroupId" runat="server" DataTextField="Name" DataValueField="Value" />
                    <asp:RequiredFieldValidator ControlToValidate="drpProjectGroupId" runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Calc Basis Field:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcBasisField" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Basis Table:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtBasisTable" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Basis Field Multiplier:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtBasisFieldMultiplier" runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="txtBasisFieldMultiplier" runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Criteria Start Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaStartAmount" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Criteria End Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCriteriaEndAmount" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Type:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcType" runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="txtCalcType" runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Calc Exec Table:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcExecTable" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Calc Exec Field:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtCalcExecField" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Flat Amount:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtFlatAmount" runat="server" />
                    <asp:RangeValidator ControlToValidate="txtFlatAmount" Type="Currency" MinimumValue="0"
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
