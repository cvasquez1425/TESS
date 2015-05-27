<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BatchEscrow.ascx.cs"
    Inherits="Greenspoon.Tess.Controls.BatchEscrow" %>
<table class="table" style="width: 100%;">
    <caption>
        Batch Escrow</caption>
    <tr>
        <td class="left" colspan="2">
            Project:
        </td>
        <td class="left">
            Phase:
        </td>
        <td class="left" style="white-space: nowrap; width: 0%;">
            Title Insurance:
        </td>
        <td class="left">
            Escrow Key:&nbsp;
            <asp:Label ID="lblBatchId" runat="server" Font-Bold="true" ForeColor="red" />
        </td>
    </tr>
    <tr>
        <td class="alt" colspan="2">
            <asp:DropDownList ID="drpProject" runat="server" AutoPostBack="true" Width="250px"
                onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                onblur="this.style.position='';this.style.width='250px'" DataTextField="Name"
                DataValueField="Value" OnSelectedIndexChanged="drpProject_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rqdProj" runat="server" ControlToValidate="drpProject"
                ErrorMessage="Project is required field." ValidationGroup="BE">*</asp:RequiredFieldValidator>
        </td>
        <td class="alt">
            <asp:DropDownList ID="drpPhase" runat="server" Width="150px" Enabled="false" DataTextField="name"
                DataValueField="value">
            </asp:DropDownList>
        </td>
        <td class="alt">
            <asp:DropDownList ID="drpbatTC" runat="server">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Text="Yes" Value="True" />
                <asp:ListItem Text="No" Value="False" />
            </asp:DropDownList>
        </td>
        <td class="left">
            <div style="width: 35%; display: inline-block; white-space: nowrap;">
                Batch:
                <asp:Label ID="lblBatchEscrowKey" runat="server" Font-Bold="true" />
            </div>
            <div style="width: 50%; display: inline-block; white-space: nowrap; text-align: right">
                <asp:CheckBox runat="server" ID="chkNonEscrow" Text=" Non Escrow" 
                    TextAlign="Right" ClientIDMode="Static" /> <%--Using HTML 5 and JQuery in web forms from CODE Magazine - Simplify your JavaScript/JQuery programmming by using ClientIDMode to Static--%>
                <asp:CheckBox runat="server" ID="chkCashOut" Text=" Cash Out" TextAlign="Right" />
            </div>
        </td>
    </tr>
    <tr>
        <td class="left" colspan="2">
            Partner:
        </td>
        <td class="left">
            Total Deed Pages:
        </td>
        <td class="left">
            Total Note Pages:
        </td>
        <td class="left">
            By:
            <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
            &nbsp;On:
            <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="alt" colspan="2">
            <asp:DropDownList ID="drpPartner" runat="server" Width="250px" Enabled="False" onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                onblur="this.style.position='';this.style.width='250px'" DataTextField="Name"
                DataValueField="Value">
            </asp:DropDownList>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtTotalDeedPages" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revTotalDeedPages" runat="server" ControlToValidate="txtTotalDeedPages"
                ValidationExpression="^\d+$" ValidationGroup="BE">?</asp:RegularExpressionValidator>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtTotalDeedNotes" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtTotalDeedNotes" runat="server" ControlToValidate="txtTotalDeedNotes"
                ValidationExpression="^\d+$" ValidationGroup="BE">?</asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:Label ID="lblBatchMsg" runat="server" EnableViewState="false" Font-Size="XX-Small" />&nbsp
<%--            RIQ-296--%>
            <asp:Button ID="btnOwnerPolUpld" runat="server"  
                OnClick="btnOwnerPolUpld_Click" Text="LV Owner Policy" ValidationGroup="BE" />&nbsp
            <asp:Button ID="btnSaveBatch" runat="server" OnClick="SavetBatch_OnClick" Text="Save"
                ValidationGroup="BE" />&nbsp;&nbsp
            <asp:HiddenField ID="HiddenField1" runat="server" />          
            <asp:HiddenField ID="chkNonEscrowHidden" runat="server" ClientIDMode="Static" />
        </td>        
    </tr>
</table>
<asp:ValidationSummary ID="vsBE" runat="server" ShowSummary="false" ShowMessageBox="true"
    DisplayMode="BulletList" HeaderText="Batch Escrow cannot be saved." EnableViewState="false"
    ValidationGroup="BE" />
<script type="text/javascript">
         function ownPolConfirm() {
             var s = confirm("Do you want to Run the Owner Policy!");
             if (s == true) {
                 return true;
             }
             else {
                 return false;
             }
         }

         //txtTotalDeedNotes The projects are 93-Cedar Ridge; 94-Logger Point; 95-Emerald Point; and 110 Branson Woods. 02/26/2014
         // The default for these escrow batches is 2 for the "Total Deed Pages" and 2 for the "total Note Pages". But for a Cash Out deal these projects need "0" for "Total Note Pages
         function chkCashOutChanges() {
             // If you find that you are having issues actually referencing your Controls, don't forget that you can use the ClientID property to resolve the appropriate client-side ID for the element as well : 
             var $hiddenValue = document.getElementById("<%= HiddenField1.ClientID %>").value;
             var $drpProjectId = $("#MainContent_ucBatchEscrow_drpProject").val();
             var $chkCashDeedNote = $("#MainContent_ucBatchEscrow_chkCashOut").is(":checked");
             //if ($chkCashDeedNote == true && $drpProjectId == 93 || $chkCashDeedNote == true && $drpProjectId == 94 || $chkCashDeedNote == true && $drpProjectId == 95 || $chkCashDeedNote == true && $drpProjectId == 110) {
             if ($chkCashDeedNote == true) {
                 $("#MainContent_ucBatchEscrow_txtTotalDeedNotes").val("0");
             }
             else {
                 $("#MainContent_ucBatchEscrow_txtTotalDeedNotes").val($hiddenValue);
             }
         }
         //  Work Order 28262  Create Pop-up when Non-Escrow is Unchecked to confirm  chkNonEscrow ( MainContent_ucBatchEscrow_chkNonEscrow  )
         //                    Carlos, Brian has put in place a procedure to put a check in the Non-Escrow Box when Status Code 10 is saved in the Status section.  
         //                    Now we need you to create a Pop-up box WHEN the Non-Escrow box is unchecked stating:  “Are you sure this is NO LONGER a NON-ESCROW batch?” Yes or No.  Yes is to uncheck and save the batch. No is to remain as is.
         function chkNonEscrowBatch() {
              // ClientIDMode to Static to simplify JavaScript/JQuery programming.  Take a look at the chkNonEscrow check box where the ClientIDMode = Static
             //var $nonEscrowChk = $("#MainContent_ucBatchEscrow_chkNonEscrow").is(":checked");
             var $nonEscrowChk = $("#chkNonEscrow").is(":checked");

             var $nonEscrowHidden = $("#chkNonEscrowHidden").val();
             //alert($nonEscrowHidden);

             if ($nonEscrowChk == false && $nonEscrowHidden == 1) {
                 var s = confirm("Are you sure this is NO LONGER a NON-ESCROW batch?");
                 if (s == true) {
                     //$("#MainContent_ucBatchEscrow_chkNonEscrow").prop("checked", false);
                     $("#chkNonEscrow").prop("checked", false);
                     return true;
                 }
                 else {
                     //$("#MainContent_ucBatchEscrow_chkNonEscrow").prop("checked", true);
                     $("#chkNonEscrow").prop("checked", true);
                     return false;
                 }
                 //                            alert("false unchecked.");
             }
         }
 </script>