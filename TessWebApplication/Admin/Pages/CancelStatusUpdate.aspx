<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancelStatusUpdate.aspx.cs"
    Inherits="Greenspoon.Tess.Admin.Pages.CancelStatusUpdate" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BulkStat" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td style="border: none; text-align: left; width: 50%;">
                <span class="caption">Cancel Status Update</span>
            </td>
            <td class="alt" style="width: 50%; border-bottom: none; text-align: right;">
                <asp:Panel DefaultButton="btnSearchEK" runat="server" ID="search">
                    Cancel Batch Key:
                    <asp:TextBox ID="txtBatchCancelKey" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCancel" runat="server" ControlToValidate="txtBatchCancelKey"
                        ValidationExpression="^\d+$" ValidationGroup="ecs" ErrorMessage="Search field excepts Escrow Ke ID (Ineteger) only. ex: ##">?</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtBatchCancelKey"
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
                Status
            </td>
            <td class="alt" colspan="3">
                <asp:DropDownList ID="drpStatusMaster" runat="server" Width="70%" DataTextField="Name"
                    DataValueField="Value" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="drpStatusMaster" ID="rqdStat"
                    ValidationGroup="st">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Invoice:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtInvoice"></asp:TextBox>
            </td>
            <td>
                Book:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtBook" MaxLength="15"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBook" ID="RequiredFieldValidator1"
                    ValidationGroup="st">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Effective Date:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtEffDate"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revMD" runat="server" ControlToValidate="txtEffDate"
                    SetFocusOnError="True" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="st"
                    ErrorMessage="Mortgage Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
            </td>
            <td>
                Assign #:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtAssignNum" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Document Recorded:
            </td>
            <td class="alt">
                <asp:TextBox runat="server" ID="txtDocRec"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rqddr" runat="server" ControlToValidate="txtDocRec"
                    SetFocusOnError="True" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="st"
                    ErrorMessage="Mortgage Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDocRec" ID="RequiredFieldValidator2"
                    ValidationGroup="st">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label runat="server" Visible="False">Project Id:</asp:Label>
            </td>
            <td class="alt">
                <asp:HiddenField ID="txtProjectId" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                County:
            </td>
            <td class="alt" colspan="3">
                <asp:DropDownList ID="drpCounty" runat="server" DataTextField="Name" DataValueField="Value"
                    Width="70%" />
            </td>
        </tr>
        <tr>
            <td>
                Original County:
            </td>
            <td class="alt" colspan="3">
                <asp:DropDownList ID="drpOriginalCounty" runat="server" Width="70%" DataTextField="Name"
                    DataValueField="Value" />
            </td>
        </tr>
        <tr>
            <td>
                Comments:
            </td>
            <td class="alt" colspan="3">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtComments" Height="50px" Width="70%" /><br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblVegasProject" runat="server" Visible="False">Vegas Project:</asp:Label>
            </td>
            <td class="alt" colspan="3">
                <asp:HiddenField ID="chkIsVegasProject" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvRecInfo" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Records Found.">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="lnPId" Value='<%# Eval("LegalNameId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
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
            <%--            Cancel Status Update changes Jan 8, 2015--%>
            <asp:TemplateField HeaderText="Doc Recorded">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtDocRecorded" MaxLength="20" TabIndex="<%# Container.DisplayIndex + 1 %>"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDocRecorded" ID="RequiredFieldValidator4"
                        ValidationGroup="st">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revDocRecorded" runat="server" ControlToValidate="txtDocRecorded"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="ecs">?</asp:RegularExpressionValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Book">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtBookGrid" MaxLength="20" TabIndex="<%# Container.DisplayIndex + 1 %>"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBookGrid" ID="RequiredFieldValidator3"
                        ValidationGroup="st">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revBook" runat="server" ControlToValidate="txtBookGrid"
                        ValidationExpression="^\d+$" ValidationGroup="ecs">?</asp:RegularExpressionValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Page">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtPage" MaxLength="20" TabIndex="<%# Container.DisplayIndex + 1 %>"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCancel" runat="server" ControlToValidate="txtPage"
                        ValidationExpression="^\d+$" ValidationGroup="ecs">?</asp:RegularExpressionValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comment">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="txtCommentGrid" MaxLength="50" TabIndex="<%# Container.DisplayIndex + 1 %>"></asp:TextBox>
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
                    Enabled="False" ValidationGroup="st" />
            </td>
        </tr>
    </table>
    <script src="../../Scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            // Comments on the GridView
            $("#MainContent_txtComments").change(function () {
                var $comments = $(this).val() != "" ? true : false;
                var text = "#MainContent_gvRecInfo_txtCommentGrid_";
                if ($comments == true) {
                    var i;
                    var $txtComments = $(this).val();
                    for (i = 0; i < 100; i++) {
                        text += i;
                        var name = $(text);
                        var $nullName = name.val() != null ? true : false;
                        if ($nullName == true) {
                            $(text).val($txtComments);
                            text = "";
                            text = "#MainContent_gvRecInfo_txtCommentGrid_";
                        }
                        else {
                            break;
                        }
                    }
                }
            });
            //          Book
            $("#MainContent_txtBook").change(function () {
                var $books = $(this).val() != "" ? true : false;
                var text = "#MainContent_gvRecInfo_txtBookGrid_";
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
                            text = "#MainContent_gvRecInfo_txtBookGrid_";
                        }
                        else {
                            break;
                        }
                    }
                }
            });
            //          Doc Recorded
            $("#MainContent_txtDocRec").change(function () {
                var $docRecorded = $(this).val() != "" ? true : false;
                var text = "#MainContent_gvRecInfo_txtDocRecorded_";
                if ($docRecorded == true) {
                    var i;
                    var $txtDocRecorded = $(this).val();
                    for (i = 0; i < 100; i++) {
                        text += i;
                        var name = $(text);
                        var $nullName = name.val() != null ? true : false;
                        if ($nullName == true) {
                            $(text).val($txtDocRecorded);
                            text = "";
                            text = "#MainContent_gvRecInfo_txtDocRecorded_";
                        }
                        else {
                            break;
                        }
                    }
                }
            });
            //        MainContent_txtProjectId    for County = 16 [82, 129, 130] - May 05, 2015
            var $txtCountyId = $("#MainContent_txtProjectId").val();
            //var txtProjectIdArr = [82, 129, 130];
            var txtProjectIdArr = [16];
            $.each(txtProjectIdArr, function (index, value) {
                if ($txtCountyId == value) {
                    var text = "#MainContent_gvRecInfo_txtPage_";
                    var i;
                    var $txtBook = 0;
                    for (i = 0; i < 100; i++) {
                        text += i;
                        var name = $(text);
                        var $nullName = name.val() != null ? true : false;
                        if ($nullName == true) {
                            $(text).val($txtBook);
                            $(text).prop("disabled", true);
                            text = "";
                            text = "#MainContent_gvRecInfo_txtPage_";
                        }
                        else {
                            break;
                        }
                    }
                }
            });
            //});

            //    I was wondering if you could fix this on the cancel side like you did for the Vegas projects on the regular bulk update. 
            //    I attached an example. The doc recorded field and the book field is always the same for the entire batch, so it would save time to only tab to the “page” field for the bulk cancel update. 
            //    We have no reason to use the “comment” field either. Could you fix the tabbing to only go to the page field for the Vegas projects? (116, 118, 114, 66)

            //       Las Vegas Project [66, 116, 118, 114] 04/27/2015. I didn't want to use the new field created by Brian is_lv_project
            var $txtProjId = $("#MainContent_chkIsVegasProject").val();
            var txtProjIdArr = ["True"];
            $.each(txtProjIdArr, function (index, value) {
                if ($txtProjId == value) {
                    var text = "#MainContent_gvRecInfo_txtDocRecorded_";
                    var textBook = "#MainContent_gvRecInfo_txtBookGrid_";
                    var textComment = "#MainContent_gvRecInfo_txtCommentGrid_";

//                    var $textPage = "#MainContent_gvRecInfo_txtPage_0";
//                    var $nullNamePage = $textPage.val() != null ? true : false;
//                    if ($nullNamePage == true) {
//                        ($textPage).prop("focus", true);
//                    }

                    var i;
                    for (i = 0; i < 100; i++) {
                        text += i;
                        textBook += i;
                        textComment += i;
                        var name = $(text);
                        var nameBook = $(textBook);
                        var nameComment = $(textComment);

                        var $nullName = name.val() != null ? true : false;
                        var $nullNameBook = nameBook.val() != null ? true : false;
                        var $nullNameComment = nameComment.val() != null ? true : false;

                        if ($nullName == true) {
                            $(text).prop("tabindex", "-1");
                            text = "";
                            text = "#MainContent_gvRecInfo_txtDocRecorded_";
                        }
                        else {
                            break;
                        }

                        if ($nullNameBook == true) {
                            $(textBook).prop("tabindex", "-1");
                            textBook = "";
                            textBook = "#MainContent_gvRecInfo_txtBookGrid_";
                        }
                        else {
                            break;
                        }

                        if ($nullNameComment == true) {
                            $(textComment).prop("tabindex", "-1");
                            textComment = "";
                            textComment = "#MainContent_gvRecInfo_txtCommentGrid_";
                        }
                        else {
                            break;
                        }

                    }
                }
            });

        });       //function

        function blkScanConfirm() {
            var $txtDeedPage = $('input[name*="txtPage"]');
            var $display = false;
            var $displayPage = false;

            // DeedPage
            $('input[name*="txtPage"]').each(function (index) {
                var $messagebox = $(this).val() != "" ? true : false;
                var $msgboxNull = $(this).val() === null ? true : false;
                if (!$messagebox || $msgboxNull) {
                    $displayPage = true;
                    return false;   // exit the loop
                }
            });
            if ($displayPage) {
                var $confirm = confirm("Invalid Page, Do you want to proceed?");
                if ($confirm) return true; else return false;
            }

        }
    </script>
</asp:Content>
