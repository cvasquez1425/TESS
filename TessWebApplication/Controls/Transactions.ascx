<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Transactions.ascx.cs" EnableViewState="false"
    Inherits="Greenspoon.Tess.Controls.Transactions" %>
<div>
    <script type="text/javascript">
        function Transaction_Updated() {
            //  close the popup
            tb_remove();
            //  refresh the update panel so we can view the changes  
            $('#<%= this.btnLoadTransactions.ClientID %>').click();
        }
    </script>
</div>
<div class="divSubForm">
    <asp:Button ID="btnLoadTransactions" runat="server" Style="display: none" OnClick="Page_Load" />
    <table style="padding: 0px; margin: 0px;" width="97%">
        <tr>
            <td style="width: 100%;">
                <a id="btnShowPopupTran" title="Add Transaction" runat="server" class="thickbox"
                    visible="false">+ Add Transaction</a>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvTransactions" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false" 
        EmptyDataText="No Data">
        <Columns>
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEditTrn" runat="server" visible='<%# this.CanEdit %>' class="thickbox"
                        title="Edit Transaction" href='<%#  Eval("ContractAmountId", "~/Pages/Transactions.aspx?a=e&id={0}&TB_iframe=true&height=200&width=400") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Contract Transaction"
                            style="height: 10px; width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="AmountTypeDesc" HeaderText="Amount Type" ReadOnly="true" ItemStyle-Width="25%" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="true" ItemStyle-Width="25%" />
            <asp:BoundField DataField="CreatedDate" HeaderText="Date" ReadOnly="true" ItemStyle-Width="25%" />
            <asp:BoundField DataField="CreatedBy" HeaderText="User" ReadOnly="true" ItemStyle-Width="25%" />
        </Columns>
    </asp:GridView>
</div>
