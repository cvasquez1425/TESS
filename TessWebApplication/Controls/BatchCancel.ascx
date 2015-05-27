<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BatchCancel.ascx.cs"
    Inherits="Greenspoon.Tess.Controls.BatchCancel" %>
<%--Batch Cancel Search Area--%>
<table class="table" style="width: 100%;" cellspacing="0px" cellpadding="0px">
    <tr>
        <td style="border: none; text-align: left; width: 50%;">
            <span class="caption">Batch Cancel</span>
        </td>
        <td class="alt" style="width: 50%; border-bottom: none; text-align: right;">
            <asp:Panel DefaultButton="btnSearchBC" runat="server" ID="search">
                Cancel Key:
                <asp:TextBox ID="txtSearchCancelBatch" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revCancel" runat="server" ControlToValidate="txtSearchCancelBatch"
                    ValidationExpression="^\d+$" ValidationGroup="bcs" ErrorMessage="Search field excepts Batch Cancel ID (Ineteger) only. ex: ##">?</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearchCancelBatch"
                    ValidationGroup="bcs" ErrorMessage="Search field is required.">*</asp:RequiredFieldValidator>
                <asp:Button ID="btnSearchBC" runat="server" Text="Go" OnClick="btnSearchBC_Click"
                    ValidationGroup="bcs" />
            </asp:Panel>
        </td>
    </tr>
</table>
<%--Batch Cancel Body Area--%>
<table class="table" style="width: 100%;" cellspacing="0px" cellpadding="0px">
    <tr>
        <td class="left">
            Project: [ ID ]
        </td>
        <td class="left">
            Cancel Type:
        </td>
        <td class="left">
            Cancel Key:
        </td>
        <td>
        
        </td>
    </tr>
    <tr>
        <td class="alt">
            <asp:DropDownList ID="drpProject" runat="server" Width="250px" onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                onblur="this.style.position='';this.style.width='250px'" DataTextField="Name"
                DataValueField="Value">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rqdProj" runat="server" ControlToValidate="drpProject"
                ErrorMessage="Project is required field." ValidationGroup="bc">*</asp:RequiredFieldValidator>
        </td>
        <td class="alt">
            <asp:DropDownList ID="drpCancelType" runat="server" DataTextField="Name" DataValueField="Value"
                Width="70%" AutoPostBack="true" OnSelectedIndexChanged="drpCancelType_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCancelType" runat="server" ControlToValidate="drpCancelType"
                ErrorMessage="Cancel Type is required field." ValidationGroup="bc">*</asp:RequiredFieldValidator>
        </td>
        <td class="alt">
            <asp:Label ID="lblBatchId" runat="server"></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="left">
            Parent Project Id:
        </td>
        <td class="left">
            Extra Recording:
        </td>
        <td class="left">
            Extra Pages:
        </td>
        <td class="left">
            Cancel Number:
        </td>
<%--        <td> RIQ-306
        
        </td>--%>
    </tr>
    <tr>
        <td class="alt">
            <asp:DropDownList ID="drpParentProject" runat="server" AutoPostBack="true" Width="250px" Enabled="false"
                onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                onblur="this.style.position='';this.style.width='250px'" DataTextField="Name"
                DataValueField="Value">
            </asp:DropDownList>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtExtraRecording" runat="server" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtExtraRecording"
                ValidationExpression="^\d+$" ValidationGroup="bc" ErrorMessage="Extra Recording excepts numbers only. ex: ##">?</asp:RegularExpressionValidator>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtExtraPages" runat="server" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtExtraPages"
                ValidationExpression="^\d+$" ValidationGroup="bc" ErrorMessage="Extra pages excepts numbers only. ex: ##">?</asp:RegularExpressionValidator>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtCancelNumber" runat="server" Enabled="false" />
            <asp:RegularExpressionValidator ID="revBatchKey" runat="server" ControlToValidate="txtCancelNumber"
                ValidationExpression="^\d+$" ValidationGroup="bc" ErrorMessage="Batch number excepts numbers only. ex: ##">?</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="left">
            Cancel Status:
        </td>
        <td class="left">
            Extra Names:
        </td>
        <td>
            By:
            <asp:Label ID="lblCreatedBy" runat="server" />
            &nbsp; &nbsp; &nbsp; &nbsp;On:
            <asp:Label ID="lblCreatedDate" runat="server" />
        </td>
        <td>
        
        </td>
    </tr>
    <tr>
        <td class="alt">
            <asp:DropDownList ID="drpCancelStatus" runat="server" Width="250px" onmousedown="if($.browser.msie){this.style.position='relative';this.style.width='auto'}"
                onblur="this.style.position='';this.style.width='250px'" DataTextField="Name"
                DataValueField="Value">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCancelStatus" runat="server" ControlToValidate="drpCancelStatus"
                ErrorMessage="Status is a required field." ValidationGroup="bc">*</asp:RequiredFieldValidator>
        </td>
        <td class="alt">
            <asp:TextBox ID="txtExtraNames" runat="server" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtExtraNames"
                ValidationExpression="^\d+$" ValidationGroup="bc" ErrorMessage="Extra names excepts numbers only. ex: ##">?</asp:RegularExpressionValidator>
        </td>
        <td>
        
        </td>
        <td style="text-align: right;">
            <asp:Label ID="lblMsg" runat="server" />&nbsp;&nbsp;
            <asp:Button ID="btnSaveBC" runat="server" Text="Save" ValidationGroup="bc" OnClick="btnSaveBC_Click" />
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="vsFC" runat="server" ShowSummary="false" ShowMessageBox="true"
    DisplayMode="BulletList" HeaderText="Batch Cancel cannot be saved." EnableViewState="false"
    ValidationGroup="bc" />
<!-- Batch Cancel validation group bcs: batch cancel search-->
<asp:ValidationSummary ID="vsSearch" runat="server" ShowSummary="false" ShowMessageBox="true"
    DisplayMode="BulletList" HeaderText="Batch Cancel cannot be searched." EnableViewState="false"
    ValidationGroup="bcs" />
