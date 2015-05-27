<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BatchForeclosure.ascx.cs"
    Inherits="Greenspoon.Tess.Controls.BatchForeclosure" %>
<table class="table" style="width: 100%;" cellspacing="0px" cellpadding="0px">
    <tr>
        <td style="border: none; text-align: left; width: 50%;">
            <span class="caption">Batch Foreclosure</span>
        </td>
        <td class="alt" style="width: 50%; border-bottom: none; text-align: right;">
            <asp:Panel DefaultButton="btnSearchFC" runat="server" ID="search">
                Foreclosure Batch Id:
                <asp:TextBox ID="txtSearchFCBatch" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSearchFCBatch"
                    ValidationExpression="^\d+$" ValidationGroup="flv" ErrorMessage="Search field excepts Batch Foreclosure number only. ex: ##">?</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearchFCBatch"
                    ValidationGroup="flv" ErrorMessage="Search field is required.">*</asp:RequiredFieldValidator>
                <asp:Button ID="btnSearchFC" runat="server" Text="Go" OnClick="btnSearchFC_Click"
                    ValidationGroup="flv" />
            </asp:Panel>
        </td>
    </tr>
</table>
<table class="table" style="width: 100%;" cellspacing="0px" cellpadding="0px">
    <tr>
        <td class="left">
            Project: [ ID ]
        </td>
        <td class="left">
            Phase:
        </td>
        <td class="left">
            Foreclosure Type:
        </td>
        <td class="left">
            Batch Number:
        </td>
        <td class="left" style="width: 0px; white-space:nowrap;">
            F K A:
        </td>
    </tr>
    <tr>
        <td class="alt">
            <asp:DropDownList ID="drpProject" runat="server" AutoPostBack="true" Width="250px"
                onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                onblur="this.style.position='';this.style.width='250px'" DataTextField="Name"
                DataValueField="Value" OnSelectedIndexChanged="drpProject_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rqdProj" runat="server" ControlToValidate="drpProject"
                ErrorMessage="Project is required field." ValidationGroup="BF">*</asp:RequiredFieldValidator>
        </td>
        <td class="alt">
            <asp:DropDownList ID="drpPhase" runat="server" Enabled="false" DataTextField="name"
                DataValueField="value">
            </asp:DropDownList>
        </td>
        <td class="alt">
            <asp:DropDownList ID="drpForeclosureType" runat="server" DataTextField="Name" DataValueField="Value">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvForeclosureType" runat="server" ControlToValidate="drpForeclosureType"
                ErrorMessage="Foreclosure Type is required field." ValidationGroup="BF">*</asp:RequiredFieldValidator>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtBatchKey" runat="server" />
<%--            <asp:RequiredFieldValidator ID="rfvBatchKey" runat="server" ControlToValidate="txtBatchKey"
                ErrorMessage="Batch Key is required field." ValidationGroup="BF">*</asp:RequiredFieldValidator>--%>
            <asp:RegularExpressionValidator ID="revBatchKey" runat="server" ControlToValidate="txtBatchKey"
                ValidationExpression="^\d+$" ValidationGroup="BF" ErrorMessage="Batch Key excepts numbers only. ex: ##">?</asp:RegularExpressionValidator>
        </td>
        <td class="alt" style="width: 0px">
            <asp:CheckBox ID="chkFKA" runat="server" TextAlign="Left" />
        </td>
    </tr>
    <tr>
        <td class="left">
            Status:
        </td>
        <td class="left">
            &nbsp;Judge: [ Division ]
        </td>
        <td class="left">
            File Date:
        </td>
        <td class="left">
            &nbsp;Case #:
        </td>
        <td class="left" style="width: 0px; white-space:nowrap;">
            L L C:
        </td>
    </tr>
    <tr>
        <td class="alt">
            <asp:DropDownList ID="drpStatus" runat="server" Width="250px" onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                onblur="this.style.position='';this.style.width='250px'" DataTextField="Name"
                DataValueField="Value">
            </asp:DropDownList>
        </td>
        <td class="alt">
            <asp:DropDownList ID="drpJudge" runat="server" Width="110px" Enabled="false" onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                onblur="this.style.position='';this.style.width='110px'" DataTextField="Name"
                DataValueField="Value">
            </asp:DropDownList>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtFileDate" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revFileDate" runat="server" ControlToValidate="txtFileDate"
                ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="BF" ErrorMessage="File Date field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtCaseNum" runat="server" />
        </td>
        <td class="alt" style="width: 0px">
            <asp:CheckBox ID="chkLLC" runat="server" TextAlign="Left" />
        </td>
    </tr>
    <tr>
        <td class="left">
            Processed Date:
        </td>
        <td class="left">
            Return Date:
        </td>
        <td class="left">
            HOA File Date:
        </td>
        <td class="left">
            Foreclosure Batch Key:
        </td>
        <td class="left" style="width: 0px">
            &nbsp;<asp:Label ID="lblCreatedBy" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="alt">
            <asp:TextBox ID="txtProcessed" runat="server" />
            <asp:RegularExpressionValidator ID="revProcessed" runat="server" ControlToValidate="txtProcessed"
                ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="BF" ErrorMessage="Processed field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtReturned" runat="server" />
            <asp:RegularExpressionValidator ID="revReturned" runat="server" ControlToValidate="txtReturned"
                ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="BF" ErrorMessage="Returned field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtHOAFile" runat="server" />
            <asp:RegularExpressionValidator ID="revHOA" runat="server" ControlToValidate="txtHOAFile"
                ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ValidationGroup="BF" ErrorMessage="HOA File field excepts date only. ex: MM/DD/YYYY">?</asp:RegularExpressionValidator>
        </td>
        <td class="left">
            <asp:Label ID="lblBatchId" runat="server" />
        </td>
        <td class="left" style="width: 0px">
            &nbsp;<asp:Label ID="lblCreateTime" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2" class="left" style="border-right: none;">
        </td>
        <td align="center" colspan="2" style="border-right: none; border-left: none">
            <asp:Label ID="lblMsg" runat="server" />
        </td>
        <td style="border-left: none;">
            <asp:Button ID="btnSaveBFC" runat="server" Text="Save" ValidationGroup="BF" OnClick="btnSaveBFC_Click" />
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="vsFC" runat="server" ShowSummary="false" ShowMessageBox="true"
    DisplayMode="BulletList" HeaderText="Batch Foreclosure cannot be saved." EnableViewState="false"
    ValidationGroup="BF" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
    ShowMessageBox="true" DisplayMode="BulletList" HeaderText="Batch Foreclosure cannot be searched."
    EnableViewState="false" ValidationGroup="flv" />
