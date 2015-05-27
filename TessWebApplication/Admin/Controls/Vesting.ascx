<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Vesting.ascx.cs" EnableViewState="false"
    Inherits="Greenspoon.Tess.Admin.Controls.Vesting1" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Vesting" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditVesting.aspx?a=n&TB_iframe=true&height=200&width=500") %>'
            class="thickbox">+ Add Vesting</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="vesting_type" HeaderText="Vesting Type" ReadOnly="true" />
            <asp:BoundField DataField="vesting_active" HeaderText="Avtive" ReadOnly="true" />
            <asp:BoundField DataField="createdby" HeaderText="Created By" ReadOnly="true" />
            <asp:BoundField DataField="createddate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/dd/yyyy}" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("vesting_id", "Edit: {0} ")%>'
                        href='<%#  Eval("vesting_id", "~/Admin/Pages/AddEditVesting.aspx?a=e&id={0}&TB_iframe=true&height=200&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Vesting" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
