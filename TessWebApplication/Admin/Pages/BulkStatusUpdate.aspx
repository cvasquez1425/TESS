<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkStatusUpdate.aspx.cs"
    Inherits="Greenspoon.Tess.Admin.Pages.BulkStatusUpdate" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BulkStatus" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .overlay
        {
            position: fixed;
            z-index: 98;
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
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress1.ClientID%>");
            updateProgress.style.display = "block";
        }
    </script>
    <div>
        <table class="table">
            <caption>
                Bulk Update</caption>
            <tr>
                <td>
                    Identifier:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpIden" runat="server" Width="50%">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">Escrow Key</asp:ListItem>
                        <asp:ListItem Value="2">Master ID</asp:ListItem>
                        <asp:ListItem Value="3">Cancel ID</asp:ListItem>
                        <asp:ListItem Value="4">Dev_K</asp:ListItem>
                        <asp:ListItem Value="5">Client Batch</asp:ListItem>  <%--August 2014--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Project:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpProject" runat="server" Width="70%" DataTextField="Name"
                        DataValueField="Value">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Upload Excel:
                </td>
                <td class="alt">
                    <asp:FileUpload runat="server" Width="60%" ID="excelUpld" />
                </td>
            </tr>
            <tr>
                <td>
                    Identifier Keys:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtKeys" Height="50px" Width="70%" />
                </td>
            </tr>
            <tr>
                <td>
                    Status:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpStatusMaster" runat="server" Width="70%" DataTextField="Name"
                        DataValueField="Value" AutoPostBack="False" />
                </td>
            </tr>
            <tr>
                <td>
                    Original County:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpOriginalCounty" runat="server" Width="70%" DataTextField="Name"
                        DataValueField="Value" />
                </td>
            </tr>
            <tr>
                <td>
                    County:
                </td>
                <td class="alt">
                    <asp:DropDownList ID="drpCounty" runat="server" DataTextField="Name" DataValueField="Value"
                        Width="70%" />
                </td>
            </tr>
            <tr>
                <td>
                    Comments:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtComments" Height="50px" Width="70%" />
                </td>
            </tr>
            <tr>
                <td>
                    Book:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtBook" MaxLength="15"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Page:
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtPage"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTotalDeedPages" runat="server" ControlToValidate="txtPage"
                        ValidationExpression="^\d+$" ValidationGroup="st">?</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Assign #
                </td>
                <td class="alt">
                    <asp:TextBox runat="server" ID="txtAssignNum" MaxLength="10"></asp:TextBox>
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
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right; white-space: nowrap;">
                    <asp:CheckBox ID="chkIsComment"  runat="server" Enabled="False" 
                        Visible="False" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblBatchMsg" runat="server" EnableViewState="false" Font-Size="XX-Small" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSaveBatch" runat="server" Text="Save"
                        OnClientClick="showProgress()" ValidationGroup="st" OnClick="btnSaveBatch_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False"
                        UseSubmitBehavior="False" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
    </div>
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
