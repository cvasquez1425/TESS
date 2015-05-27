<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryExpandedView.aspx.cs"
    Inherits="Greenspoon.Tess.Pages.InventoryExpandedView" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Status" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updInv">
        <ContentTemplate>
            <div>
                <script type="text/javascript">
                    function Inventory_Updated() {
                        //  close the popup
                        tb_remove();
                        //  refresh the update panel so we can view the changes  
                        $('#<%= this.btnLoadInventories.ClientID %>').click();
                    }
                </script>
                <asp:Button ID="btnLoadInventories" runat="server" Style="display: none" OnClick="Page_Load" />
                <table style="padding: 0px; margin: 0px;" width="99%">
                    <tr>
                        <td style="width: 50%;">
                            <a id="btnShowPopupInv" title="Add Inventory" runat="server" class="thickbox" visible="false">
                                + Add Inventory</a>
                        </td>
                        <td style="white-space:nowrap;">
                            <a id="btnNewLegalName" runat="server" title="Add Legal Name" class="thickbox" visible="false">+ Add Legal Name</a>
                        </td>
                        <td style="width: 50%; text-align: right;">
                            <a id="btnReturn" runat="server">Return to Record</a>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvInventory" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
                    Width="99%" EmptyDataText="No Data" EnableViewState="false">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                            <ItemTemplate>
                                <a id="btnEditInv" runat="server" class="thickbox"
                                    title="Edit Inventory" href='<%# string.Format("~/Pages/Inventory.aspx?a=e&id={0}&cid={1}&TB_iframe=true&height=340&width=500",Eval("ContractIntervalId"), Eval("ContractId") ) %>'>
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
            <br />
            <div style="width: 100%">
                    <fieldset>
                        <legend>Legal Names: </legend>
                        <asp:PlaceHolder ID="plcLegalNames" runat="server"></asp:PlaceHolder>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
