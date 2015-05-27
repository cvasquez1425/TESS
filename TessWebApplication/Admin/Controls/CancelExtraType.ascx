<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CancelExtraType.ascx.cs"
    EnableViewState="false" Inherits="Greenspoon.Tess.Admin.Controls.CancelExtraType" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Cancel Extra Type" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditCancelExtraType.aspx?a=n&TB_iframe=true&height=200&width=500") %>'
            class="thickbox">+ Add Cancel Extra Type</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="cancel_extra_type_value" HeaderText="Cancel Extra Type Value"
                ReadOnly="true" />
            <asp:BoundField DataField="createdby" HeaderText="Created By" ReadOnly="true" />
            <asp:BoundField DataField="createddate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/dd/yyyy}" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEditPartner" runat="server" class="thickbox" title='<%# Eval("cancel_extra_type_value", "Edit: {0} ")%>'
                        href='<%#  Eval("cancel_extra_type_id", "~/Admin/Pages/AddEditCancelExtraType.aspx?a=e&id={0}&TB_iframe=true&height=200&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Partner" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
