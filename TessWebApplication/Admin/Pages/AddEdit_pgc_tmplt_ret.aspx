<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEdit_pgc_tmplt_ret.aspx.cs"
    Inherits="Greenspoon.Tess.Admin.Pages.AddEdit_pgc_tmplt_ret" MasterPageFile="~/MasterPages/SingleForm.Master" %>

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
                    Calc Field:
                </td>
                <td class="alt">
                   <asp:TextBox ID="txtCalcField" runat="server" MaxLength="50" />
                   <asp:RequiredFieldValidator ID="rqdCalcFiled" ControlToValidate="txtCalcField" runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                Calc Type:
                </td>
                <td class="alt">
                   <asp:TextBox ID="txtCalcType" runat="server" MaxLength="5" />
                </td>
            </tr>
            <tr>
                <td>
                 Basis Table:
                </td>
                <td class="alt">
                   <asp:TextBox ID="txtBasisTable" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                 Basis Field:
                </td>
                <td class="alt">
                   <asp:TextBox ID="txtBasisField" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Calc Value:
                </td>
                <td class="alt">
                <asp:TextBox ID="txtCalcValue" runat="server" MaxLength="50" />
                     <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtCalcValue" Type="Double"
                        MinimumValue="0" MaximumValue="99999999" runat="server">##.## Only</asp:RangeValidator>
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
