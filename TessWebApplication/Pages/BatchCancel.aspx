<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchCancel.aspx.cs" Inherits="Greenspoon.Tess.Pages.BatchCancel" %>

<%@ Register Src="../Controls/BatchCancel.ascx" TagName="BatchCancel" TagPrefix="uc1" %>
<asp:Content ID="BC" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="updBatchCancel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc1:BatchCancel ID="ucBatchCancel" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updCancel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divCancel" runat="server" visible="false">
                <script type="text/javascript">
                    function cancel_updated() {
                        //  close the popup
                        tb_remove();
                        //  refresh the update panel so we can view the changes  
                        $('#<%= this.btnLoadContracts.ClientID %>').click();
                    }
                    // CV102012
                    function blkScanConfirm() {
                        var r = confirm("Do you want to Bulk Scan to TESS!");
                        if (r == true) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }

                    function stop(name) {
                        alert(name);
                    }
                    // Change Request Erica Candeller Sep 19, 2014
                    function disableEnterKey() {
                        if (event.keyCode == 13) {
                            event.keyCode = 9; //return the tab key
                            event.cancelBubble = true;
                        }
                    }   
                </script>
                <div style="width: 100%">
                    <fieldset>
                        <legend>Legal Names: </legend>
                        <asp:PlaceHolder ID="plcLegalNames" runat="server"></asp:PlaceHolder>
                        <%-- RIQ-279 --%>
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
                <asp:Button ID="btnLoadContracts" runat="server" Style="display: none" OnClick="btnLoadContracts_Click" />
                <a id="btnAddContractId" runat="server" class="thickbox" title="Add Contract to Cancel">
                    + Add Master IDs</a> &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkAddMasterId" Text="+ Search and Add Master IDs" runat="server"
                    OnClick="lnkGotoCancelSearch_Click" />
                <table style="width: 100%; padding: 0px; margin: 0px;">
                    <tr>
                        <td style="width: 60%; vertical-align: top;">
                            <%--Main Cancel Table--%>
                            <table class="table">
                                <tr>
                                    <td colspan="4">
                                        <asp:PlaceHolder ID="plcCurrentValue" runat="server"></asp:PlaceHolder>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:PlaceHolder ID="plcCancelExtra" runat="server"></asp:PlaceHolder>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50% !important; border-bottom: none;">
                                        Master ID:
                                    </td>
                                    <td class="alt" style="width: 50% !important; border-bottom: none">
                                        <asp:DropDownList ID="drpContract" runat="server" AutoPostBack="true" DataTextField="Name"
                                            onkeydown="disableEnterKey();" DataValueField="Value" OnSelectedIndexChanged="drpContract_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvContract" runat="server" ControlToValidate="drpContract"
                                            ErrorMessage="Sorry, there are no contract in this batch." ValidationGroup="c">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        Dev K#:
                                    </td>
                                    <td class="alt">
                                        <asp:Label runat="server" ID="lblDevK"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Death:
                                    </td>
                                    <td class="alt" style="width: 50% !important; white-space: nowrap !important;">
                                        <asp:TextBox runat="server" ID="txtDeath" onkeydown="disableEnterKey();" />
                                        <asp:RangeValidator ErrorMessage="Death field allows currency value upto 999.99"
                                            ControlToValidate="txtDeath" MinimumValue="0" MaximumValue="999.99" Type="Currency"
                                            ValidationGroup="c" runat="server">?</asp:RangeValidator>
                                    </td>
                                    <td>
                                        CMA:
                                    </td>
                                    <td class="alt" style="width: 50% !important; white-space: nowrap !important;">
                                        <asp:TextBox runat="server" ID="txtCMA" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="txtCMARegularExpressionValidator" runat="server"
                                            ErrorMessage="Only Numbers With 2 Decimals Allowed" Text="?" ControlToValidate="txtCMA"
                                            ValidationGroup="c" ValidationExpression="^\d{1,8}\.?[0-9]{0,2}"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Current Value:
                                    </td>
                                    <td class="alt" style="width: 50% !important; white-space: nowrap !important;">
                                        <asp:TextBox runat="server" ID="txtPurchasePrice" onkeydown="disableEnterKey();" />
                                        <asp:RangeValidator ID="RangeValidator2" ErrorMessage="Current Value field allows currency value upto 999999999.99"
                                            ControlToValidate="txtPurchasePrice" MinimumValue="0" MaximumValue="999999999.99"
                                            Type="Currency" ValidationGroup="c" runat="server">?</asp:RangeValidator>
                                    </td>
                                    <td>
                                        Non Tax:
                                    </td>
                                    <td class="alt" style="width: 50% !important; white-space: nowrap !important;">
                                        <asp:TextBox runat="server" ID="txtNonTax" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="txtNonTaxRegularExpressionValidator" runat="server"
                                            ErrorMessage="Only Numbers With 2 Decimals Allowed" Text="?" ControlToValidate="txtNonTax"
                                            ValidationExpression="^\d{1,8}\.?[0-9]{0,2}" ValidationGroup="c"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mortgage Book:
                                    </td>
                                    <td class="left" style="width: 50% !important; white-space: nowrap !important;">
                                        <asp:TextBox ID="txtMortBook" runat="server" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td style="width: 0% !important; white-space: nowrap !important;">
                                        Deed Book:
                                    </td>
                                    <td class="left" style="width: 50% !important; white-space: nowrap !important;">
                                        <asp:TextBox ID="txtDeedBook" runat="server" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mortgage Page:
                                    </td>
                                    <td class="left">
                                        <asp:TextBox ID="txtMortPage" runat="server" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td>
                                        Deed Page:
                                    </td>
                                    <td class="left">
                                        <asp:TextBox ID="txtDeedPage" runat="server" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mortgage Date:
                                    </td>
                                    <td class="left">
                                        <asp:TextBox ID="txtMortDate" runat="server" onkeydown="disableEnterKey();" />
                                    </td>
                                    <td>
                                        Deed Date:
                                    </td>
                                    <td class="left">
                                        <asp:TextBox ID="txtDeedDate" runat="server" onkeydown="disableEnterKey();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Points Group:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblPointsGroup" runat="server" onkeydown="disableEnterKey();"></asp:Label>
                                    </td>
                                    <td>
                                        Points:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblPoints" runat="server" onkeydown="disableEnterKey();"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vesting:
                                    </td>
                                    <td class="left">
                                        <asp:Label ID="lblVesting" runat="server" onkeydown="disableEnterKey();"></asp:Label>
                                    </td>
                                    <td>
                                        Active:
                                    </td>
                                    <td class="alt">
                                        <div runat="server" id="activeBG" onkeydown="disableEnterKey();">
                                            <asp:CheckBox ID="chkActive" runat="server" onkeydown="disableEnterKey();" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Non Comply:
                                    </td>
                                    <td class="left">
                                        <asp:TextBox ID="txtNonComply" runat="server" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="valNonComply0" runat="server" ControlToValidate="txtNonComply"
                                            ErrorMessage="Non Comply field excepts number only" SetFocusOnError="True" ValidationExpression="^\d+$"
                                            ValidationGroup="c">?</asp:RegularExpressionValidator>
                                    </td>
                                    <%--                                    <td colspan="2">
                                        &nbsp;
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="lblCancelType" runat="server" Text="Affidavit of Rev:"></asp:Label>
                                    </td>
                                    <td class="left">
                                        <asp:TextBox ID="txtReverter" runat="server" onkeydown="disableEnterKey();" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtReverter"
                                            ErrorMessage="Values 1 or 2 allowed in the Invoice-Affidavit of Reverter field"
                                            SetFocusOnError="True" ValidationExpression="[12]" ValidationGroup="c">?</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="border-right: none;">
                                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" />
                                    </td>
                                    <td style="border-left: none;">
                                        <asp:LinkButton ID="lnkSave" runat="server" Text="Save" ValidationGroup="c" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 40%; vertical-align: top;">
                            <table style="width: 101%;">
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
                                            </div>
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
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" DisplayMode="BulletList" HeaderText="Cancel cannot be saved."
        EnableViewState="false" ValidationGroup="c" />
    <%--End of content page--%>
</asp:Content>
