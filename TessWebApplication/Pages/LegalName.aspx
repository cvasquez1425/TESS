<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LegalName.aspx.cs" Inherits="Greenspoon.Tess.Pages.LegalName"
    MasterPageFile="~/MasterPages/SingleForm.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Contract Legal Name</title>
</asp:Content>
<asp:Content ID="reports" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <caption>
            Contract Legal Names</caption>
        <tr>
            <td>
                Primary:
            </td>
            <td class="alt">
                <asp:CheckBox ID="chkPrimary" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Last Name:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtLastName" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtLastName" runat="server"
                    ValidationGroup="ln">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                First Name:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtFirstName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Address 1:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtAddress1" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Address 2-3:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtAddress2" runat="server" Width="100px"></asp:TextBox>
                -
                <asp:TextBox ID="txtAddress3" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Country:
            </td>
            <td class="alt">
                <asp:DropDownList ID="drpCountryList" runat="server" Width="210px" DataTextField="Name"
                    DataValueField="Value">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Zip:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtZip" runat="server" Width="200px" AutoPostBack="True" 
                    ontextchanged="txtZip_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                City:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtCity" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                State:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtState" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Email:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Order:
            </td>
            <td class="alt">
                <asp:TextBox ID="txtOrder" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td>
                Dismiss:
            </td>
            <td class="alt">
                <asp:CheckBox ID="chkDismiss" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td>
                Active:
            </td>
            <td class="alt">
                <asp:CheckBox ID="chkActive" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Created By:
            </td>
            <td class="alt">
                <asp:Label ID="lblCreateBy" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Create Date:
            </td>
            <td class="alt">
                <asp:Label ID="lblCreateDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="clear: both; width: 95%;">
                    <tr>
                        <td style="border: 0 none white;">
                            <asp:Label ID="lblMsg" EnableViewState="false" runat="server" />
                        </td>
                        <td style="border: 0 none white;">
                            <asp:LinkButton ID="btnSaveClose" runat="server" Text="Save & Close" OnClick="Save_Click"
                                CausesValidation="true" ValidationGroup="ln" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnSaveNew" runat="server" Text="Save & New" OnClick="Save_Click"
                                CausesValidation="true" ValidationGroup="ln" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <div style="width: 100%;">
        <div style="float: right; margin-right: 30px;">
            <asp:CheckBox runat="server" ID="chkShowAllLN" AutoPostBack="True" Text="  Show All"
                TextAlign="Right" />
        </div>
        <asp:PlaceHolder ID="plcLegalNames" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>
