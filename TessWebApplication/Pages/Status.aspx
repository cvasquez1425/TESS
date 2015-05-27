<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Status.aspx.cs" 
MasterPageFile="~/MasterPages/SingleForm.Master"
    Inherits="Greenspoon.Tess.Pages.Status" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .exceeded
        {
            background-color: #FBEFEF;
        }
    </style>
    <script src="../Scripts/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var limit = 199;
            var tb = $('textarea[id$=txtComment]');
            $(tb).keyup(function () {
                var len = $(this).val().length;
                if (len > limit) {
                    //this.value = this.value.substring(0, 100);
                    $(this).addClass('exceeded');
                    $('#spn').text(len - limit + " characters exceeded");
                }
                else {
                    $(this).removeClass('exceeded');
                    $('#spn').text(limit - len + " characters left");
                    $('#error').text("");
                }
            });

            $('a[id$=btnSave]').click(function (e) {
                var len = $(tb).val().length;
                var n = $("input[id$=chkIsComment]:checked").length;  //RIQ-289 CVJan2013
                if (len > limit) {
                    $('#error').text("Comment field characters exceeded");
                    e.preventDefault();
                }
                if (n > 0 && len <= 0) {
                    $('#error').text("Comment is a required field for this STATUS MASTER CODE");
                    e.preventDefault();
                }
            });
//            Default Cursor to Status Code Enhancements 2015 item 2
//            ThickBox displays the iframe's container by calling a method in the onload event of the iframe element. 
//            Since the iframe is hidden on the parent page until after the iframe's content is loaded you cannot set the focus simply using $(document).ready(..focus);. 
//            The easiest way I've found to get around this is to use setTimeout to delay the function call that sets the focus until after the iframe is displayed:
            setTimeout(function () {
                $('#MainContent_drpStatusMaster').focus();
            }, 200);
        });       // end of JQuery
    </script>
</asp:Content>
<asp:Content ID="status" runat="server" ContentPlaceHolderID="MainContent">
    <div style="overflow: hidden !important;">
        <table class="table">
            <caption>
                Contract Status</caption>
            <tr>
                <td>
                    Status Master:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpStatusMaster" runat="server" Width="90%" DataTextField="Name"
                        DataValueField="Value" AutoPostBack="True" TabIndex="1" />
                    <asp:RequiredFieldValidator ID="rfvStatusMaster" runat="server" ValidationGroup="s"
                        ControlToValidate="drpStatusMaster">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    County:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpCounty" runat="server" Width="80%" DataTextField="Name"
                        DataValueField="Value" TabIndex="2">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Original County:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpOriginalCounty" runat="server" Width="80%" DataTextField="Name"
                        DataValueField="Value" TabIndex="3" />
                </td>
            </tr>
            <tr>
                <td>
                    Legal Name:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpLegalName" runat="server" Width="80%" DataTextField="Name"
                        DataValueField="Value" TabIndex="4" />
                </td>
            </tr>
            <tr>
                <td>
                    Invoice:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtInvoice" runat="server" MaxLength="20" TabIndex="5" />
                </td>
            </tr>
            <tr>
                <td>
                    Record Date:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtRecordDate" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revtxtRecordDate" runat="server" ControlToValidate="txtRecordDate"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="s">MM/DD/YYYY</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Book:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtBook" runat="server" MaxLength="15" TabIndex="7" />
                </td>
            </tr>
            <tr>
                <td>
                    Page:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtPage" runat="server" TabIndex="8" />
                    <asp:RegularExpressionValidator ID="revPage" runat="server" ControlToValidate="txtPage"
                        ValidationExpression="^\d+$" ValidationGroup="s">##</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Assignment Number:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtAssignmentNumber" runat="server" MaxLength="10" 
                        TabIndex="9" />
                </td>
            </tr>
            <tr>
                <td>
                    Effective Date:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" TabIndex="10" />
                    <asp:RegularExpressionValidator ID="revEffectiveDate" runat="server" ControlToValidate="txtEffectiveDate"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="s">MM/DD/YYYY</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Comments:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtComment" runat="server" Height="80px" TextMode="MultiLine" 
                        Width="80%" TabIndex="11" /><br />
                    (Max 200 characters) &nbsp;<span id="spn"></span>
                </td>
            </tr>
            <tr>
                <td>
                    Batch:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtBatch" runat="server" TabIndex="12" />
                    <asp:RegularExpressionValidator ID="revBatch" runat="server" ControlToValidate="txtBatch"
                        ValidationExpression="^\d+$" ValidationGroup="s">##</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Upload Batch Id:
                </td>
                <td class="alt">
                    <asp:TextBox ID="txtUploadBatchId" runat="server" TabIndex="13" />
                    <asp:RegularExpressionValidator ID="revBatchId" runat="server" ControlToValidate="txtUploadBatchId"
                        ValidationExpression="^\d+$" ValidationGroup="s">##</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Valid:
                </td>
                <td class="alt">
                    <asp:CheckBox ID="chkActive" runat="server" TabIndex="14" />
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
                <td colspan="2" style="horiz-align: center;">
                    <asp:CheckBox ID="chkIsComment"  runat="server" Enabled="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMsg" EnableViewState="false" runat="server" />
                    &nbsp;&nbsp; <span id="error"></span>&nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click" CausesValidation="true"
                        ValidationGroup="s" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
