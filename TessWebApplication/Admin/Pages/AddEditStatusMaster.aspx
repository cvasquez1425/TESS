<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditStatusMaster.aspx.cs"
    MasterPageFile="~/MasterPages/SingleForm.Master" Inherits="Greenspoon.Tess.Admin.Pages.AddEditStatusMaster" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="CntStatusMaster" runat="server" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr>
            <td style="width:40%; white-space:nowrap;">Status Group :</td>
            <td style="width:60%;" class="alt">
                <asp:DropDownList ID="drpStatusGroup" runat="server" Width="80%" DataTextField="Name" DataValueField="Value">
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Status Master Name :</td>
            <td class="alt">
                <asp:TextBox ID="txtStatusMasterName" runat="server" Width="80%"></asp:TextBox>
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Status Active :</td>
            <td class="alt">
                <asp:CheckBox ID="chkActive" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Comment Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkCommentReg" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Delete Blocked ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkDeletedReq" runat="server" />
             </td>
        </tr>
<%--    <tr style="width:40%; white-space:nowrap;">                          RIQ-289 CVJan2013
            <td>Date Stamp Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkDateStampReq" runat="server" />
             </td>
        </tr>

       <tr>
            <td style="width:40%; white-space:nowrap;">Effective Date REquired ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkEffDateReg" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Record Date Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkRecordDateReq" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Book Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkBookReq" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Page Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkPageReq" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Batch Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkBatchReq" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">County Name Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkCountyNameReq" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Assignment Number Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkAssignmentNumReq" runat="server" />
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Original County Name Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkOrgCountyNameReq" runat="server" />
             </td>
        </tr>
--%>    <tr>
            <td style="width:40%; white-space:nowrap;">Legal Name Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkLegalName" runat="server" />
<%-- RIQ-308               <asp:TextBox ID="txtNext" runat="server"></asp:TextBox>--%>
             </td>
        </tr>
         <tr>
            <td style="width:40%; white-space:nowrap;">Cancel Escrow Required ? :</td>
            <td class="alt">
                <asp:CheckBox ID="chkCancelEscrow" runat="server" />
<%-- RIQ-308      <asp:TextBox ID="txtInterval" runat="server"></asp:TextBox>--%>
             </td>
        </tr>
        <tr>
            <td style="width:40%; white-space:nowrap;">
                Create Date :
            </td>
            <td class="left">
                <asp:Label ID="lblCreateDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width:40%; white-space:nowrap;">
                Created By :
            </td>
            <td class="left">
                <asp:Label ID="lblCreateBy" runat="server" />
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
            <td colspan="2">
                <div style="text-align: right; width: 90%; padding: 0px; margin: 0px;">
                    <asp:Label ID="lblMsg" runat="server" EnableViewState="false" />
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" ValidationGroup="d"
                        OnClick="btnSave_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
