<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditUnit.aspx.cs" Inherits="Greenspoon.Tess.Admin.Pages.AddEditUnit"
    MasterPageFile="~/MasterPages/SingleForm.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="firm" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
         <caption>
            Add Unit</caption>
        <tr>
            <td>
                Unit Number:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtUnitNum" runat="server" MaxLength="150"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqdUNum" runat="server" ControlToValidate="txtUnitNum"
                    ValidationGroup="j">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Number of Bedrooms:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtNumBed" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Description:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" TextMode="MultiLine" Width="60%" Height="40px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="text-align: right; width: 90%; padding: 0px; margin: 0px;">
                    <asp:Label ID="lblMsg" runat="server" EnableViewState="false" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="j"
                        OnClick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" CausesValidation="False"
                        OnClick="btnCancel_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
