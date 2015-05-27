<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchEscrow.aspx.cs" Inherits="Greenspoon.Tess.Pages.BatchEscrow" %>

<%@ Register Src="../Controls/BatchEscrow.ascx" TagName="BatchEscrow" TagPrefix="uc4" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BE" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .overlay
        {
            position: fixed;
            z-index: 100;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #aaa;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }
        .overlayContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
            white-space: nowrap;
        }
        .overlayContent img
        {
            width: 80px;
            height: 80px;
        }
    </style>
    <div id="divMsg" runat="server" enableviewstate="false">
    </div>
    <div id="divBatch" runat="server">
        <asp:UpdatePanel ID="updBatch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc4:BatchEscrow ID="ucBatchEscrow" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="updContract" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script type="text/javascript">
                function disableEnterKey() {
                    if (event.keyCode == 13) {
                        event.keyCode = 9; //return the tab key
                        event.cancelBubble = true;
                    }
                }                    // CV082012
                function blkScanConfirm() {
                    var r = confirm("Do you want to Bulk Scan to TESS!");
                    if (r == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                function ownPolConfirm() {
                    var s = confirm("Do you want to Run the Owner Policy!");
                    if (s == true) {
                        return true;
                        showProgress();
                    }
                    else {
                        return false;
                    }
                }    // CV01112012
                function showProgress() {
                    var updateProgress = $get("<%= UpdateProgress1.ClientID%>");
                    updateProgress.style.display = "block";
                }

                var $drpProjectId = $("#MainContent_ucBatchEscrow_drpProject").val();
                var $chkCash = $("#MainContent_ucBatchEscrow_chkCashOut");
                $chkCash.change(function () {
                    if ($(this).is(":checked")) {
                        $("#MainContent_txtAmountFinanced").attr("disabled", "disabled");
                        $("#MainContent_txtMortDate").attr("disabled", "disabled");
                        $("#MainContent_txtMortRecDate").attr("disabled", "disabled");
                        $("#MainContent_txtMortBook").attr("disabled", "disabled");
                        $("#MainContent_txtMortPage").attr("disabled", "disabled");
                        $("#MainContent_txtExtraMortPages").attr("disabled", "disabled");
                    }
                    else {
                        $("#MainContent_txtAmountFinanced").removeAttr("disabled");
                        $("#MainContent_txtMortDate").removeAttr("disabled");
                        $("#MainContent_txtMortRecDate").removeAttr("disabled");
                        $("#MainContent_txtMortBook").removeAttr("disabled");
                        $("#MainContent_txtMortPage").removeAttr("disabled");
                        $("#MainContent_txtExtraMortPages").removeAttr("disabled");
                    }
                });
            </script>
            <div id="divContract" runat="server">
                <%--RIQ-299--%>
                <div class="lglName">
                    <fieldset>
                        <legend>Legal Names: </legend>
                        <asp:PlaceHolder ID="plcLegalNames" runat="server"></asp:PlaceHolder>
                        <div style="float: right;">
                            <asp:CheckBox runat="server" ID="chkShowAllLN" AutoPostBack="True" Text="  Show All"
                                TextAlign="Right" />
                        </div>
                    </fieldset>
                </div>
                <table class="table">
                    <tr>
                        <td style="border: none; white-space: nowrap; width: 100%">
                            <div style="float: left; font-weight: bold;">
                                &nbsp<asp:LinkButton ID="LnkFirst" runat="server" Text="≤ First" OnClick="LnkFirstClick"
                                    Enabled="False" EnableViewState="False"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="lnkPrevious" runat="server" Text="<< Previous" OnClick="LnkPreviousClick"
                                    Enabled="false" EnableViewState="false"></asp:LinkButton>
                                &nbsp; &nbsp;
                                <asp:Label ID="lblNumPage" runat="server" EnableViewState="False" />
                                &nbsp; &nbsp;
                                <asp:LinkButton ID="lnkNext" runat="server" Text="Next >>" OnClick="LnkNextClick"
                                    Enabled="false" EnableViewState="false"></asp:LinkButton>
                                &nbsp<asp:LinkButton ID="LnkLast" runat="server" Text="Last ≥" OnClick="LnkLastClick"
                                    Enabled="False" EnableViewState="False"></asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td style="width: 60%; vertical-align: top;">
                            <br />
                            <table class="table" cellspacing="0px" cellpadding="0px">
                                <tr>
                                    <td style="width: 0% !important; white-space: nowrap !important; text-align: right !important;">
                                        Master ID:
                                    </td>
                                    <td class="alt" style="width: 50% !important;">
                                        <asp:DropDownList ID="drpMasterId" runat="server" AutoPostBack="True" DataTextField="Name"
                                            onkeydown="disableEnterKey();" DataValueField="Value" OnSelectedIndexChanged="drpMasterId_SelectedIndexChanged"
                                            TabIndex="8">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Non Comply:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtNonComply" runat="server" TabIndex="10" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="valNonComply" runat="server" ControlToValidate="txtNonComply"
                                            ErrorMessage="Non Comply field excepts number only" SetFocusOnError="True" ValidationExpression="^\d+$"
                                            ValidationGroup="cn">?</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        File Open:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtFileOpen" runat="server" TabIndex="10" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="revFO" runat="server" ControlToValidate="txtFileOpen"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="cn" ErrorMessage="File Open field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Client Batch #:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtClientBatch" runat="server" TabIndex="11" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Dev K#:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtDevK" runat="server" TabIndex="12" onkeydown="disableEnterKey();" />
                                        <%--<asp:RequiredFieldValidator runat="server" ID="rqdevk" ControlToValidate="txtDevK"
                                            ValidationGroup="cn" ErrorMessage="Dev K is Required.">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>
                                        Share:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtShare" runat="server" MaxLength="5" TabIndex="13" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Price:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtPurchasePrice" runat="server" TabIndex="14" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="revBeAmount" runat="server" ControlToValidate="txtPurchasePrice"
                                            ErrorMessage="Purchase Price field accepts currency value only. ie: ##.##" ValidationGroup="cn"
                                            ValidationExpression="^\d*\.?\d*$">?</asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Amount Financed:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtAmountFinanced" runat="server" TabIndex="15" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmountFinanced"
                                            ErrorMessage="Purchase Price field accepts currency value only. ie: ##.##" ValidationGroup="cn"
                                            ValidationExpression="^\d*\.?\d*$">?</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Contract Date:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtContractDate" runat="server" TabIndex="16" onkeydown="disableEnterKey();" />
                                        <%--  <asp:RequiredFieldValidator runat="server" ID="rqdcondate" ControlToValidate="txtContractDate"
                                            ValidationGroup="cn" ErrorMessage="Contract Date is required.">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revCD" runat="server" ControlToValidate="txtContractDate"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="cn" ErrorMessage="Contract Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Mort Date:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtMortDate" runat="server" TabIndex="17" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMortDate"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="cn" ErrorMessage="Mortgage Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ex Deed Pages:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtExtraPages" runat="server" TabIndex="18"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revExPages" runat="server" ControlToValidate="txtExtraPages"
                                            ValidationExpression="^\d+$" ValidationGroup="cn">?</asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Ex Mort Pages:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtExtraMortPages" runat="server" TabIndex="19"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtExtraMortPages"
                                            ValidationExpression="^\d+$" ValidationGroup="cn">?</asp:RegularExpressionValidator>
                                    </td>
                                    <%--                                    <td>
                                        Policy #:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtPolicyNum" runat="server" TabIndex="19" onkeydown="disableEnterKey();" />
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        Cancel:
                                    </td>
                                    <td class="alt">
                                        <asp:CheckBox runat="server" ID="chkCancel" Enabled="False" />
                                    </td>
                                    <%--<td>
                                        Ex Mort Pages:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtExtraMortPages" runat="server" TabIndex="18"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtExtraMortPages"
                                            ValidationExpression="^\d+$" ValidationGroup="cn">?</asp:RegularExpressionValidator>
                                    </td>--%>
                                    <td>
                                        Active:
                                    </td>
                                    <td class="alt">
                                        <div runat="server" id="activeBG" style="vertical-align: middle; padding: 4px 4px 4px 4px;">
                                            <asp:CheckBox ID="chkActive" runat="server" onkeydown="disableEnterKey();" TabIndex="9"
                                                Enabled="False" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Season:
                                    </td>
                                    <td class="alt">
                                        <asp:DropDownList runat="server" ID="drpSeason" DataTextField="Name" DataValueField="Value"
                                            TabIndex="20" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td>
                                        Fixed/Float:
                                    </td>
                                    <td class="alt">
                                        <asp:DropDownList ID="drpFixFlt" runat="server" TabIndex="21" onkeydown="disableEnterKey();">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Fixed</asp:ListItem>
                                            <asp:ListItem>Float</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vesting:
                                    </td>
                                    <td class="alt">
                                        <asp:DropDownList ID="drpVesting" runat="server" onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                                            onblur="this.style.position='';this.style.width='110px'" DataTextField="Name"
                                            DataValueField="Value" TabIndex="22" onkeydown="disableEnterKey();">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Marital Status:
                                    </td>
                                    <td class="alt">
                                        <asp:DropDownList ID="drpMarStat" runat="server" DataTextField="Name" DataValueField="Value"
                                            TabIndex="23" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Inter spousal:
                                    </td>
                                    <td class="alt">
                                        <asp:DropDownList ID="drpIntSp" runat="server" TabIndex="24" onkeydown="disableEnterKey();">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Gender:
                                    </td>
                                    <td class="alt">
                                        <asp:DropDownList ID="drpGender" runat="server" DataTextField="Name" DataValueField="Value"
                                            TabIndex="25" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Initial Fee:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtIniFee" runat="server" TabIndex="26" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td>
                                        Partial Week:
                                    </td>
                                    <td class="alt">
                                        <asp:DropDownList ID="drpPartWeek" runat="server" DataTextField="id" DataValueField="id"
                                            TabIndex="27" onkeydown="disableEnterKey();">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Alt Build:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtAltBld" runat="server" TabIndex="28" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td>
                                        Policy #:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtPolicyNum" runat="server" TabIndex="19" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bankrupt:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblBankcrupt" runat="server" TabIndex="501" />
                                    </td>
                                    <td>
                                        Foreclosure Date:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblForeclosureDate" runat="server" TabIndex="502" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deed Date:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtDeedDate" runat="server" TabIndex="30" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="revDD" runat="server" ControlToValidate="txtDeedDate"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="cn" ErrorMessage="Deed Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Mort Rec Date:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtMortRecDate" runat="server" TabIndex="33" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="revMD" runat="server" ControlToValidate="txtMortRecDate"
                                            ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="cn" ErrorMessage="Mortgage Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deed Book:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtDeedBook" runat="server" TabIndex="31" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td>
                                        Mort Book:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtMortBook" runat="server" TabIndex="34" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deed Page:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtDeedPage" runat="server" TabIndex="32" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td>
                                        Mort Page:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtMortPage" runat="server" TabIndex="35" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <%--RIQ-296--%>
                                <tr>
                                    <td>
                                        Points Group:
                                    </td>
                                    <td class="alt">
                                        <asp:DropDownList ID="drpPointGroup" runat="server" DataTextField="Name" DataValueField="Value"
                                            TabIndex="36" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td>
                                        Points:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtPoints" runat="server" TabIndex="37" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Color:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtColor" runat="server" TabIndex="38" MaxLength="50" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Opt Out:
                                    </td>
                                    <td class="alt">
                                        <asp:CheckBox runat="server" ID="chkOptOut" TabIndex="39" />
                                    </td>
                                    <td>
                                        Resend:
                                    </td>
                                    <td class="alt">
                                        <asp:CheckBox runat="server" ID="chkResend" TabIndex="40" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="padding: 0px; margin: 0px;">
                                        <table style="width: 100%; padding: 0px; margin: 0px;" class="clear">
                                            <tr>
                                                <td class="left;" style="border: none; width: 60%; text-align: center;">
                                                    <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
                                                </td>
                                                <td style="border: none; width: 40%; white-space: nowrap;">
                                                    <%--          RIQ-300                                   <div style="float: right;">
                                                        <ul id="dropdownButton">
                                                            <li style="padding: 0 5px 5px 5px;">
                                                                <div class="arrow-down">
                                                                </div>
                                                                <ul id="help" style="z-index: 1000 !important;">
                                                                    <li>
                                                                        <asp:LinkButton ID="btnSaveContract" runat="server" Text="Save" OnClick="SaveClick"
                                                                            ValidationGroup="cn" />
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="btnNewContract" runat="server" Text="New" OnClick="SaveClick"
                                                                            CausesValidation="false" />
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                    </div>--%>
                                                    <div style="float: right; padding-top: 8px;">
                                                        <asp:LinkButton ID="btnSaveContract" runat="server" Text="Save" OnClick="SaveClick"
                                                            ValidationGroup="cn" />&nbsp;&nbsp
                                                    </div>
                                                    <div style="float: right; padding-top: 8px;">
                                                        <asp:LinkButton ID="btnNewContract" runat="server" Text="New" OnClick="SaveClick"
                                                            CausesValidation="false" />&nbsp;&nbsp
                                                    </div>
                                                    <div style="float: right; padding-top: 8px;">
                                                        <asp:LinkButton ID="btnSaveNewContract" runat="server" Text="Save & New" OnClick="SaveClick"
                                                            ValidationGroup="cn" TabIndex="38" />&nbsp;&nbsp
                                                    </div>
                                                    <div class="clear">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top; width: 40%;">
                            <table style="width: 100%; padding: 0px; margin: 0px;">
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Inventory:</legend>
                                            <asp:PlaceHolder ID="plcInventory" runat="server" />
                                        </fieldset>
                                    </td>
                                </tr>
                                <!-- cv092012 WorldGate Developer Changes-->
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Start-End Period:</legend>Start:
                                            <asp:TextBox ID="txtStartPeriod" runat="server"></asp:TextBox>&nbsp;&nbsp
                                            <asp:RegularExpressionValidator ID="StartPeriodRegularExpressionValidator" runat="server"
                                                ErrorMessage="Enter a valid number between 0 to 999." ControlToValidate="txtStartPeriod"
                                                ValidationExpression="^\d{1,3}" Text="?" ValidationGroup="cn"></asp:RegularExpressionValidator>&nbsp
                                            End:&nbsp<asp:TextBox ID="txtEndPeriod" runat="server" ValidationGroup="cn"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="EndPeriodRegularExpressionValidator" runat="server"
                                                ErrorMessage="Enter a valid number between 0 to 999." ControlToValidate="txtEndPeriod"
                                                ValidationExpression="^\d{1,3}" Text="?" ValidationGroup="cn">
                                            </asp:RegularExpressionValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Start Period Must be Greater Than End Period!"
                                                ControlToCompare="txtStartPeriod" ControlToValidate="txtEndPeriod" Operator="GreaterThanEqual"
                                                ValidationGroup="cn" Type="Integer" ForeColor="Red"></asp:CompareValidator>
                                        </fieldset>
                                    </td>
                                </tr>
                                <!-- Project 130 Williamsburg Plantation Unit Level Choices DropDown Control -->
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Project Unit Level:</legend>Unit Level:
                                            <asp:DropDownList ID="drpUnitLevel" runat="server" DataTextField="Name" DataValueField="Value" />
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Status:</legend>
                                            <asp:PlaceHolder ID="plcStatus" runat="server"></asp:PlaceHolder>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Documents:</legend>
                                            <div class="divSubForm">
                                                <div style="float: right; padding: 0px 10px 0px 0px;">
                                                    <asp:LinkButton ID="btnShowImage" runat="server" Visible="true" CausesValidation="false"
                                                        Text="Load Docs" OnClick="BtnShowImageClick"></asp:LinkButton>
                                                </div>
                                                <asp:GridView ID="gvScannedImages" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
                                                    EmptyDataText="No docs found." Visible="false">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                                                            <ItemTemplate>
                                                                <a href="<%# GetDocHandlerUrl(Eval("doctype").ToString(), Eval("DocLocation").ToString()) %>">
                                                                    <img src="../Images/Icon_Download.gif" alt="download doc" width="15px" height="15px" />
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="docnum" HeaderText="Document Number" ReadOnly="true" />
                                                        <asp:BoundField DataField="docname" HeaderText="Document Name" ReadOnly="true" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label runat="server" ID="lblTest"></asp:Label>
                                            </div>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>TS Resales:</legend>
                                            <asp:PlaceHolder ID="plcResale" runat="server"></asp:PlaceHolder>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label runat="server" ID="lblbsMsg" EnableViewState="False" Font-Bold="True" />
                                        &nbsp;&nbsp;
                                        <asp:Button runat="server" ID="btnBSUpld" Text="Bulk Scan" OnClick="btnBSUpld_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label runat="server" ID="lblbsMsgs" EnableViewState="False" Font-Bold="True" />
                                        &nbsp;&nbsp;
                                        <asp:Button runat="server" ID="btnOPUpld" Text="Owner Policy" OnClick="btnOPUpld_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <ajax:PostBackTrigger ControlID="gvScannedImages" />
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvScannedImages"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsBE" runat="server" ShowSummary="false" ShowMessageBox="true"
        DisplayMode="BulletList" HeaderText="Contract form cannot be saved." EnableViewState="false"
        ValidationGroup="cn" />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <h2 style="white-space: nowrap;">
                    Please Wait...</h2>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
