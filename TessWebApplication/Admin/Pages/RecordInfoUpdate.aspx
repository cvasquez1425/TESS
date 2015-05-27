<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordInfoUpdate.aspx.cs"
    Inherits="Greenspoon.Tess.Admin.Pages.RecordInfoUpdate" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BulkRecord" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td style="border: none; text-align: left; width: 50%;">
                <span class="caption">Batch Recording Info Update</span>
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
    <table class="table">
        <tr>
            <td>
                Recording Date:
            </td>
            <td colspan="3" class="alt">
                <asp:TextBox runat="server" ID="txtRecordDate" TabIndex="1" />
                <asp:RequiredFieldValidator runat="server" ID="tqdrd" ControlToValidate="txtRecordDate"
                    ValidationGroup="ri">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revRD" runat="server" ControlToValidate="txtRecordDate"
                    ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="ri">?</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                Deed Book:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtDeedBook" MaxLength="20" TabIndex="2" />
                <asp:RequiredFieldValidator runat="server" ID="rqddb" ControlToValidate="txtDeedBook"
                    ValidationGroup="ri">*</asp:RequiredFieldValidator>
            </td>
            <td>
                Mortgage Book:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtMortgageBook" MaxLength="20"  TabIndex="3"/>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvRecInfo" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Records Found." onrowdatabound="gvRecInfo_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Master ID">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblMasterId" Text='<%# Eval("MasterId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="true" />
            <asp:BoundField DataField="Share" HeaderText="Share" ReadOnly="true" />
            <asp:TemplateField HeaderText="Year">
                <ItemStyle Width="100px"></ItemStyle>
                <ItemTemplate>
                    <a class="info" href="#">
                        <%# GetDisplay((string[])Eval("Year"))%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit">
                <ItemStyle Width="100px"></ItemStyle>
                <ItemTemplate>
                    <a class="info" href="#">
                        <%# GetDisplay((string[])Eval("Units"))%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Weeks">
                <ItemStyle Width="100px"></ItemStyle>
                <ItemTemplate>
                    <a class="info" href="#">
                        <%# GetDisplay((string[])Eval("Weeks"))%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deed Book">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtBook" TabIndex="<%# 3 + (2 * Container.DisplayIndex ) %>" MaxLength="20" Text='<%# Eval("DeedBook") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deed Page">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtDeedPage" TabIndex="<%# 4 + (2 * Container.DisplayIndex ) %>" MaxLength="20" Text='<%# Eval("DeedPage") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mtg Book">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtMortBook" TabIndex="<%# 5 + (2 * Container.DisplayIndex ) %>" MaxLength="20" Text='<%# Eval("MortgageBook") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mtg Page">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtMortPage" TabIndex="<%# 5 + (2 * Container.DisplayIndex ) %>" MaxLength="20" Text='<%# Eval("MtgPage") %>'>></asp:TextBox>
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
            //          Book MainContent_txtDeedBook
            $("#MainContent_txtDeedBook").change(function () {
                var $books = $(this).val() != "" ? true : false;
                var text = "#MainContent_gvRecInfo_txtBook_";
                if ($books == true) {
                    var i;
                    var $txtBook = $(this).val();
                    for (i = 0; i < 100; i++) {
                        text += i;
                        var name = $(text);
                        var $nullName = name.val() != null ? true : false;
                        if ($nullName == true) {
                            $(text).val($txtBook);
                            text = "";
                            text = "#MainContent_gvRecInfo_txtBook_";
                        }
                        else {
                            break;
                        }
                    }
                }
            });
        });

        $(function () {
            //          Book MainContent_txtMortBook    MainContent_gvRecInfo_txtMortBook_0
            $("#MainContent_txtMortgageBook").change(function () {
                var $books = $(this).val() != "" ? true : false;
                var text = "#MainContent_gvRecInfo_txtMortBook_";
                if ($books == true) {
                    var i;
                    var $txtMtgBook = $(this).val();
                    for (i = 0; i < 100; i++) {
                        text += i;
                        var name = $(text);
                        var $nullName = name.val() != null ? true : false;
                        if ($nullName == true) {
                            $(text).val($txtMtgBook);
                            text = "";
                            text = "#MainContent_gvRecInfo_txtMortBook_";
                        }
                        else {
                            break;
                        }
                    }
                }
            });
        });
        
        //$('#txtTotal').attr('disabled', 'disabled');
        // RIQ-322 modal dialog box/message box "Enter a valid Deed Page or Mtg Page"
        //input name="ctl00$MainContent$gvRecInfo$ctl02$txtDeedPage" type="text" value="1353" maxlength="20" id="MainContent_gvRecInfo_txtDeedPage_0"
        function blkScanConfirm() {
            var $txtDeedPage = $('input[name*="txtDeedPage"]');
            var $txtMortPage = $('input[name*="txtMortPage"]');
            var $txtBook = $('input[name*="txtBook"]');
            var $txtMortBook = $('input[name*="txtMortBook"]');
            var $display = false;
            var $displayMortPage = false;
            var $displayBook = false;
            var $displayMortgageBook = false;

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
            // Deed Book
            $('input[name*="txtBook"]').each(function (index) {
                var $messagebox = $(this).val() != "" ? true : false;
                var $msgboxNull = $(this).val() === null ? true : false;
                if (!$messagebox || $msgboxNull) {
                    $displayBook = true;
                    return false;   // exit the loop
                }
            });
            if ($displayBook) {
                var $confirm = confirm("Invalid Deed Book, Do you want to proceed?");
                if ($confirm) return true; else return false;
            }
            // Mtg Book
            $('input[name*="txtMortBook"]').each(function (index) {
                var $messagebox = $(this).val() != "" ? true : false;
                var $msgboxNull = $(this).val() === null ? true : false;
                if (!$messagebox || $msgboxNull) {
                    $displayMortgageBook = true;
                    return false;   // exit the loop
                }
            });
            if ($displayMortgageBook) {
                var $confirm = confirm("Invalid Mortgage Book, Do you want to proceed?");
                if ($confirm) return true; else return false;
            }
        }
    </script>
</asp:Content>
