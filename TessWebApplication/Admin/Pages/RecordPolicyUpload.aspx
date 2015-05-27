<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="RecordPolicyUpload.aspx.cs" Inherits="Greenspoon.Tess.Admin.Pages.RecordPolicyUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BulkRecord" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td style="border: none; text-align: left; width: 50%;">
                <span class="caption">Myrtle Beach Bulk Update</span>
            </td>
            <td class="alt" style="width: 50%; border-bottom: none; text-align: right;">
                <asp:Panel DefaultButton="btnSearchEK" runat="server" ID="search">
                    Escrow Key:
                    <asp:TextBox ID="txtEscrowKey" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCancel" runat="server" ControlToValidate="txtEscrowKey"
                        ValidationExpression="^\d+$" ValidationGroup="ecs" ErrorMessage="Search field excepts Escrow Ke ID (Ineteger) only. ex: ##">?</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEscrowKey"
                        ValidationGroup="ecs" ErrorMessage="Search field is required.">*</asp:RequiredFieldValidator>
                    <asp:Button ID="btnSearchEK" runat="server" Text="Go" OnClick="btnSearchBC_Click"
                        ValidationGroup="ecs" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel runat ="server" ID="Panel1">
    <table class="table">
        <tr>
            <td>
                Recording Date:
            </td>
            <td colspan="3" class="alt">
                <asp:TextBox runat="server" ID="txtRecordDate" OnTextChanged="txtRecordDate_TextChanged"
                    AutoPostBack="True" TabIndex="1" />
                <asp:RequiredFieldValidator runat="server" ID="tqdrd" ControlToValidate="txtRecordDate"
                    ValidationGroup="ri">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revRD" runat="server" ControlToValidate="txtRecordDate"
                    ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="ri" >?</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Deed Book:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtDeedBook" OnTextChanged="txtDeedBook_TextChanged"
                    MaxLength="20" AutoPostBack="True" TabIndex="2" />
<%--                <asp:RequiredFieldValidator runat="server" ID="riTxt" ControlToValidate="txtDeedBook"
                    ValidationGroup="ri">*</asp:RequiredFieldValidator>--%>
            </td>
            <td>
                Mortgage Book:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtMortgageBook" OnTextChanged="txtMortgageBook_TextChanged"
                    MaxLength="20" AutoPostBack="True" TabIndex="3" />
            </td>
        </tr>
    </table>
</asp:Panel>
    <br />
    <asp:GridView ID="gvRecInfo" runat="server" Font-Size="XX-Small" AutoGenerateColumns="False"
        EmptyDataText="No Records Found." Width="905px">
        <Columns>
            <asp:TemplateField HeaderText="Master ID">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblMasterId" Text='<%# Eval("MasterId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="true" />
<%--            <asp:BoundField DataField="Share" HeaderText="Share" ReadOnly="true" />--%>
            <asp:TemplateField HeaderText="Year">
                <ItemStyle Width="50px"></ItemStyle>
                <ItemTemplate>
                    <a class="info" href="#">
                        <%# GetDisplay((string[])Eval("Year"))%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit">
                <ItemStyle Width="50px"></ItemStyle>
                <ItemTemplate>
                    <a class="info" href="#">
                        <%# GetDisplay((string[])Eval("Units"))%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Weeks">
                <ItemStyle Width="50px"></ItemStyle>
                <ItemTemplate>
                    <a class="info" href="#">
                        <%# GetDisplay((string[])Eval("Weeks"))%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deed Rec Date">
                <ItemStyle Width="50px" Font-Size="Small"></ItemStyle>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtRecordingDate" Style="width: 70px" TabIndex="<%# TabIndex %>"
                        MaxLength="20" Text='<%# Eval("RecordingDate") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deed Book">
                <ItemStyle Width="50px" Font-Size="Small"></ItemStyle>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtBook" Style="width: 40px" TabIndex="<%# TabIndex  %>"
                        MaxLength="20" Text='<%# Eval("DeedBook") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deed Page">
                <ItemStyle Width="50px" Font-Size="Small"></ItemStyle>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtDeedPage" Style="width: 40px" TabIndex="<%# TabIndex %>"
                        MaxLength="20" Text='<%# Eval("DeedPage") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mort Rec Date">
                <ItemStyle Width="50px" Font-Size="Small"></ItemStyle>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtMortRecordingDate" Style="width: 70px" TabIndex="<%# TabIndex %>"
                        MaxLength="20" Text='<%# Eval("MortRecordingDate") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mtg Book">
                <ItemStyle Width="50px" Font-Size="Small"></ItemStyle>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtMortBook" Style="width: 40px" TabIndex="<%# TabIndex %>"
                        MaxLength="20" Text='<%# Eval("MortgageBook") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mtg Page">
                <ItemStyle Width="50px" Font-Size="Small"></ItemStyle>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtMortPage" Style="width: 40px" TabIndex="<%# TabIndex %>"
                        MaxLength="20" Text='<%# Eval("MtgPage") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Owner Pol">
                <ItemStyle Width="50px" Font-Size="Small"></ItemStyle>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtOwnerPol" TabIndex="<%# TabIndex %>" MaxLength="20"
                        Text='<%# Eval("OwnerPolicy") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lender Pol">
                <ItemStyle Width="50px" Font-Size="Small"></ItemStyle>
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtLenderPol" TabIndex="<%# TabIndex %>" MaxLength="20"
                        Text='<%# Eval("LenderPolicy") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <table style="width: 95%;">
        <tr>
            <td style="width: 95%; text-align: center;">
                <asp:Label runat="server" ID="lblMsg" EnableViewState="False" />
            </td>
            <td style="width: 5%;">
                <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click"
                    Enabled="False" ValidationGroup="ri" />
            </td>
        </tr>
    </table>

    <script src="../../Scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#MainContent_txtMortgageBook").change(function () 
            {
                var $txtMortPage = $('input[name*="txtMortgageBook"]');
                $('input[name*="txtMortgageBook"]').val() == null || ""
                {
                    $txtMortPage.focus();
                    return false;
                }
                            
            });
        });

        // MainContent_btnUpdate Event handler for the btnUpdate
//        $("#MainContent_btnUpdate").on("Click", function () {
//            var $txtMortPage = $('input[name*="txtMortgageBook"]');
//            $('input[name*="txtMortgageBook"]').val() == null || ""
//            {
//                $txtMortPage.focus();
//                return false;
//            }
//        });

        function blkScanConfirm() {
            var $txtDeedPage = $('input[name*="txtDeedPage"]');
            var $txtMortPage = $('input[name*="txtMortPage"]');
            var $display = false;
            var $displayMortPage = false;
            $('input[name*="txtDeedPage"]').each(function (index) {
                var $messagebox = $(this).val() != "" ? true : false;
                var $msgboxNull = $(this).val() === null ? true : false;
                if (!$messagebox || $msgboxNull) {
                    $display = true;
                    return false;   // exit the loop
                }
            });
            if ($display) {
                var $confirm = confirm("Invalid Deed Page, Do you want to proceed?");
                if ($confirm) return true; else return false;
            }

            $('input[name*="txtMortPage"]').each(function (index) {
                var $messagebox = $(this).val() != "" ? true : false;
                var $msgboxNull = $(this).val() === null ? true : false;
                if (!$messagebox || $msgboxNull) {
                    $displayMortPage = true;
                    return false;   // exit the loop
                }
            });
            if ($displayMortPage) {
                var $confirm = confirm("Invalid Mortgage Page, Do you want to proceed?");
                if ($confirm) return true; else return false;
            }
        }
    </script>
</asp:Content>
