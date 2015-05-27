<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Year.ascx.cs" Inherits="Greenspoon.Tess.Admin.Controls.Year" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Year" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditYear.aspx?a=n&TB_iframe=true&height=200&width=500") %>'
            class="thickbox">+ Add year</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="year_name" HeaderText="Year Name" ReadOnly="true" />
            <asp:BoundField DataField="year_active" HeaderText="Year Active" ReadOnly="true" />
            <asp:BoundField DataField="a_b_t" HeaderText="A B T" ReadOnly="true" />
            <asp:BoundField DataField="year_name_abbrev" HeaderText="Name Abbrev" ReadOnly="true" />
            <asp:BoundField DataField="createdby" HeaderText="Created By" ReadOnly="true" />
            <asp:BoundField DataField="createddate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/d/yyyy}" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("year_name", "Edit: {0} ")%>'
                        href='<%#  Eval("cont_year_master_id", "~/Admin/Pages/AddEditYear.aspx?a=e&id={0}&TB_iframe=true&height=200&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Year" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
