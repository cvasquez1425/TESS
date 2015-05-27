<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inventory.ascx.cs" Inherits="Greenspoon.Tess.Controls.Inventory" %>
<div>
    <script type="text/javascript">
        function Inventory_Updated() {
           tb_remove();
            $('#<%= btnLoadInventories.ClientID %>').click();
        }
    </script>
</div>
<div class="divSubForm">
    <asp:Button ID="btnLoadInventories" runat="server" Style="display: none" OnClick="Page_Load" />
    <table style="padding: 0px; margin: 0px;" width="97%">
        <tr>
            <td style="width: 50%;">
                <a id="btnShowPopupInv" title="Add Inventory" runat="server" enableviewstate="false"
                    class="thickbox" visible="false">+ Add Inventory</a>
            </td>
            <td style="width: 50%; text-align: right;">
                <a id="btnExpandedView" runat="server" visible="false" enableviewstate="false">Expanded
                    View</a>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvInventory" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" EnableViewState="false">
        <Columns>
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEditInv" runat="server" visible='<%# this.CanEdit %>' class="thickbox"
                        title="Edit Inventory" href='<%# string.Format("~/Pages/Inventory.aspx?a=e&id={0}&cid={1}&TB_iframe=true&height=380&width=500",Eval("ContractIntervalId"), Eval("ContractId") ) %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Contract Inventory"
                            style="height: 10px; width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Building" HeaderText="Building" ReadOnly="true" />
            <asp:BoundField DataField="Unit" HeaderText="Unit" ReadOnly="true" />
            <asp:BoundField DataField="Week" HeaderText="Week" ReadOnly="true" />
            <asp:BoundField DataField="Year" HeaderText="Year" ReadOnly="true" />
            <asp:BoundField DataField="ABT" HeaderText="A/B/T" ReadOnly="true" />
            <asp:BoundField DataField="Floor" HeaderText="Floor" ReadOnly="true" />
            <asp:BoundField DataField="Bedroom" HeaderText="Bedroom" ReadOnly="true" />
        </Columns>
    </asp:GridView>
</div>
