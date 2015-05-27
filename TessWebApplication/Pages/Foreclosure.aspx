<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Foreclosure.aspx.cs" Inherits="Greenspoon.Tess.Pages.Foreclosure" %>

<%@ Register Src="../Controls/BatchForeclosure.ascx" TagName="BatchForeclosure" TagPrefix="uc1" %>
<asp:Content ID="FC" runat="server" ContentPlaceHolderID="MainContent">
    <div id="divMsg" runat="server" enableviewstate="false" class="error" style="text-align: center;
        padding: 0px; margin: 0px;">
    </div>
    <asp:UpdatePanel ID="updBatchForeclosure" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc1:BatchForeclosure ID="ucBatchForeclosure" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updForeclosure" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divForeclosure" runat="server" visible="false">
                <script type="text/javascript">
                    function foreclosure_updated() {
                        //  close the popup
                        tb_remove();
                        //  refresh the update panel so we can view the changes  
                        $('#<%= this.btnLoadContracts.ClientID %>').click();
                    }
                </script>
                <asp:Button ID="btnLoadContracts" runat="server" Style="display: none" OnClick="btnLoadContracts_Click" />
                <a id="btnAddContractId" runat="server" class="thickbox" title="Add Contract to Foreclosure">
                    + Add Master IDs</a>
                <table style="width: 100%; padding: 0px; margin: 0px;">
                    <tr>
                        <td style="width: 60%; vertical-align: top;">
                            <table class="table">
                                <tr>
                                    <td>
                                        Master ID:
                                    </td>
                                    <td class="alt" style="width: 60%">
                                        <asp:DropDownList ID="drpContract" runat="server" AutoPostBack="true" DataTextField="Name"
                                            DataValueField="Value" OnSelectedIndexChanged="drpContract_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvContract" runat="server" ControlToValidate="drpContract"
                                            ErrorMessage="Sorry, there are no contract in this batch." ValidationGroup="f">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        Dev K:
                                    </td>
                                    <td class="left" style="width: 40%">
                                        <asp:Label ID="lblDevK" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interest %:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtInterestPct" runat="server"></asp:TextBox>
<%--                                        <asp:RequiredFieldValidator ID="rfvInterestPct" runat="server" ControlToValidate="txtInterestPct"
                                            ErrorMessage="Interest % is required field." ValidationGroup="f">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revIntererstPct" runat="server" ControlToValidate="txtInterestPct"
                                            ValidationExpression="^\d*\.?\d*$" ErrorMessage="Interest % field excepts integer or double value. ex: ## or ##.##"
                                            ValidationGroup="f">?</asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Mortgage Book:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblMortBook" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;Default Balance:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtDefaultBalance" runat="server"></asp:TextBox>
<%--                                        <asp:RequiredFieldValidator ID="rfvDefaultBalance" runat="server" ControlToValidate="txtDefaultBalance"
                                            ErrorMessage="Default Balance is required field." ValidationGroup="f">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revDefaultBalance" runat="server" ControlToValidate="txtDefaultBalance"
                                            ValidationExpression="^(-)?\d+(\.\d\d)?$" ErrorMessage="Default Balance field excepts integer or double value. ex: ## or ##.##"
                                            ValidationGroup="f">?</asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Mortgage Page:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblMortPage" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;Default Date:
                                    </td>
                                    <td class="alt">
                                        <asp:TextBox ID="txtDefaultDate" runat="server"></asp:TextBox>
<%--                                        <asp:RequiredFieldValidator ID="rfvDefaultDate" runat="server" ControlToValidate="txtDefaultDate"
                                            ErrorMessage="Default Date is required field." ValidationGroup="f">*</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revDD" runat="server" ControlToValidate="txtDefaultDate"
                                            ErrorMessage="Default Date field excepts date only. ex: MM/DD/YYYY" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"
                                            ValidationGroup="f">?</asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        Mortgage Date:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblMortDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;On Hold:
                                    </td>
                                    <td class="alt">
                                        <asp:CheckBox ID="chkOnHold" runat="server" />
                                    </td>
                                    <td>
                                        Deed Book:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblDeedBook" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;Bankrupt:
                                    </td>
                                    <td class="alt">
                                        <asp:CheckBox ID="chkBankrupt" runat="server" />
                                    </td>
                                    <td>
                                        Deed Page:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblDeedPage" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;Active:
                                    </td>
                                    <td class="alt">
                                        <asp:CheckBox ID="chkActive" runat="server" />
                                    </td>
                                    <td>
                                        Deed Date:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblDeedDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="3">
                                        Status Comments:
                                    </td>
                                    <td class="alt" rowspan="3">
                                        <asp:TextBox ID="txtStatusComments" runat="server" Enabled="false" TextMode="MultiLine"
                                            Height="50px" Width="90%"></asp:TextBox>
                                    </td>
                                    <td>
                                        Vesting:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblVesting" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Points Group:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblPointsGroup" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Points:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblPoints" runat="server"></asp:Label>
                                    </td>
                                </tr>
                               
                            </table>
                            <table class="table" style="border-top:none;">
                                 <tr>
                                    <td style="border-right: none; border-top:none; width: 65%;">
                                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" />
                                    </td>
                                    <td style="border-left: none; border-top:none; white-space: nowrap; width: 35%">
                                        <div style="float: right;">
                                            <ul id="dropdownButton">
                                                <li style="padding: 0 5px 5px 5px;">
                                                    <div class="arrow-down">
                                                    </div>
                                                    <ul id="help">
                                                        <li>
                                                            <asp:LinkButton ID="lnkSave" runat="server" Text="Save" ValidationGroup="f" OnClick="btnSave_Click" />
                                                        </li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </div>
                                        <div style="float: right; padding-top: 8px;">
                                            <asp:LinkButton ID="btnSaveNext" runat="server" Text="Save & Next" ValidationGroup="f"
                                                OnClick="btnSave_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 40%; vertical-align: top;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Inventory:</legend>
                                            <asp:PlaceHolder ID="plcInventory" runat="server"></asp:PlaceHolder>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Status</legend>
                                            <asp:PlaceHolder ID="plcStatus" runat="server"></asp:PlaceHolder>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div style="width: 100%">
                    <fieldset>
                        <legend>Legal Names: </legend>
                        <asp:PlaceHolder ID="plcLegalNames" runat="server"></asp:PlaceHolder>
                    </fieldset>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" DisplayMode="BulletList" HeaderText="Foreclosure cannot be saved."
        EnableViewState="false" ValidationGroup="f" />
</asp:Content>
