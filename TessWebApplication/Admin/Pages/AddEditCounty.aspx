<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCounty.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEditCounty" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="county" runat="server" ContentPlaceHolderID="MainContent">
    <div style="overflow: auto; display: inline-block; height: 550px; padding: 0px; margin: 0px;">
        <table class="table">
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    County Name:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtCountyName" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    News:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtNews" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    County Circuit:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtCircuit" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    News Address 1:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtNewsAddress1" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Address 1:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtAddress1" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    News Address 2:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtNewsAddress2" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Address 2:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtAddress2" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    News City/State/Zip:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtNewsCityStZip" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    City:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtCity" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    GSA:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtGSA" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    State:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:DropDownList ID="drpState" runat="server" DataTextField="Name" DataValueField="Value"
                        Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Additional File:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtAdditionalFile" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Zip:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtZip" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Sale Stamp:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtSaleStamp" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Clerk:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtClerk" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    County Percent:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtCountyPercent" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Phone 1:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtPhone1" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Per Bid:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtPerBid" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Phone 2:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtPhone2" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Sale Type:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtSaleType" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Phone 3:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtPhone3" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Effective Date:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtEffective" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Defense TExt:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtDefenseText" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Alias Letter:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtAliasLetter" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Disability Text:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtDisabilityText" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    clerk Letter:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtClerkLetter" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Proof:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtProof" runat="server" Height="100px" TextMode="MultiLine" Width="90%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    2nd Default Letter:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtSecondDefaultLetter" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Publication:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtPublication" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Clerk Sig Text:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtClerkSigText" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Forward:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtForward" runat="server" Height="100px" TextMode="MultiLine" Width="90%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    NOA Letter P6:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtNOALetterP6" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    base Count:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtBaseCount" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Web Site:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtWebSite" runat="server" Width="85%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Base File:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtBaseFile" runat="server" Width="85%" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Web Comments:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:TextBox ID="txtWebComments" runat="server" Height="100px" TextMode="MultiLine"
                        Width="90%" />
                </td>
            </tr>
            <tr>
                <td style="width: 0%; white-space: nowrap;">
                    Created By:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:Label ID="lblCreateBy" runat="server" />
                </td>
                <td style="width: 0%; white-space: nowrap;">
                    Create Date:
                </td>
                <td style="width: 50%;" class="alt">
                    <asp:Label ID="lblCreateDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div style="text-align: right; width: 90%; padding: 0px; margin: 0px;">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false" />
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="c"
                            OnClick="btnSave_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
