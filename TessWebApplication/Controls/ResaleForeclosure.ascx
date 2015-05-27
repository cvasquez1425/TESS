<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResaleForeclosure.ascx.cs" Inherits="Greenspoon.Tess.Controls.ResaleForeclosure" %>

<div class="divSubForm">
    <asp:Button ID="btnLoadStatusForm" runat="server" Style="display: none" OnClick="Page_Load" />
    <table style="padding: 0px; margin: 0px;" width="97%">
        <tr>
            <td style="width: 100%; text-align: left;">
                <a id="btnExpandedView" runat="server" visible="false" enableviewstate="false">Expanded
                    View</a>
            </td>
        </tr>
    </table>
<asp:GridView ID="gvResale" runat="server" AutoGenerateColumns="False" EnableViewState="false">
    <Columns>
        <asp:BoundField DataField="TrId" HeaderText="Resale ID" ReadOnly="True" />
        <asp:BoundField DataField="TypeId" HeaderText="Type Name" ReadOnly="True" />
        <asp:BoundField DataField="StartDate" HeaderText="Start Date" ReadOnly="True" />
        <asp:BoundField DataField="StatusId" HeaderText="Status" ReadOnly="True" />
        <asp:BoundField DataField="ContractId" HeaderText="Master ID" ReadOnly="True" />
        <asp:BoundField DataField="ContPriceAmt" HeaderText="Contractual Amt" ReadOnly="True" />
<%--        <asp:BoundField DataField="MortgageAmt" HeaderText="Mortgage Amt" ReadOnly="True" />
        <asp:BoundField DataField="DocPrepDate" HeaderText="Document Date" ReadOnly="True" />
        <asp:BoundField DataField="ExtraPages" HeaderText="Extra Pages" ReadOnly="True" />
        <asp:BoundField DataField="ExtraNames" HeaderText="Extra Names" ReadOnly="True" />--%>
    </Columns>
    <EmptyDataTemplate>
        No data to display.
    </EmptyDataTemplate>
</asp:GridView>
<%--    <table style="padding: 0px; margin: 0px;" width="97%">
        <tr>
            <td style="width: 100%; text-align: left;">
                <a id="btnExpandedViewB" runat="server" visible="false" enableviewstate="false">Expanded
                    View</a>
            </td>
        </tr>
    </table>--%>
</div>
<div>
    <script type="text/javascript">
    </script>
</div>
