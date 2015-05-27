<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CancelExtra.ascx.cs"
    Inherits="Greenspoon.Tess.Controls.CancelExtra" %>
<div>
    <script type="text/javascript">
        function CancelExtra_Update() {
            //  close the popup
            tb_remove();
            //  refresh the update panel so we can view the changes  
            $('#<%= this.btnLoadCancelExtra.ClientID %>').click();
        }
    </script>
</div>
<div class="divSubForm" style="height: 90px !important;">
    <asp:Button ID="btnLoadCancelExtra" runat="server" Style="display: none" OnClick="Page_Load" />
    <asp:GridView ID="gvCancelExtra" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data"
        AllowSorting="true" Width="100%">
        <Columns>
            <asp:BoundField DataField="CancelExtraTypeValue" HeaderText="Type" ReadOnly="true" />
            <asp:BoundField DataField="Names" HeaderText="Names" ReadOnly="true" />
            <asp:BoundField DataField="Pages" HeaderText="Pages" ReadOnly="true" />
            <asp:BoundField DataField="CancelId" HeaderText="Cancel Id" ReadOnly="true" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEditName" runat="server" class="thickbox" title="Edit Cancel Extra" href='<%#  Eval("CancelExtraId", "~/Pages/CancelExtra.aspx?a=e&form=bc&id={0}&TB_iframe=true&height=200&width=400") %>'>
                        <img src="../Images/Edit.gif" alt="Edit Cancel Extra" style="height: 10px; width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<div style="width: 30%; text-align: center; float: right; border: none;">
    <a id="btnShowCancelExtraAdd" visible="true" title="Add Cancel Extra" runat="server"
        class="thickbox">+ Add Cancel Extra</a>
</div>
<div style="width: 30%; text-align: center; float: left; border: none;">
    <a id="btnShowCurrentValueAdd" visible="true" title="Add Current Value" runat="server"
        class="thickbox">+ Add Current Value</a>
</div>
