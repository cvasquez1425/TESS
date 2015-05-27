<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TitleCompany.ascx.cs"
    EnableViewState="false" Inherits="Greenspoon.Tess.Admin.Controls.TitleCompany" %>
<div style="width: 90%; text-align: right; float: right; border: none;">
    <a id="btnAdd" title="Add Title Company" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditTitleCompany.aspx?a=n&TB_iframe=true&height=200&width=500") %>'
        class="thickbox">+ Add Title Company</a>
</div>
<asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
    EmptyDataText="No Data" Width="100%">
    <Columns>
        <asp:BoundField DataField="pol_prefix" HeaderText="Pool Prefix" ReadOnly="true" />
        <asp:BoundField DataField="title_company_name" HeaderText="Title Company Name" ReadOnly="true" />
        <asp:BoundField DataField="title_company_active" HeaderText="Active" ReadOnly="true" />
        <asp:BoundField DataField="createddate" HeaderText="Created By" ReadOnly="true" />
        <asp:BoundField DataField="createdby" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/dd/yyyy}" />
        <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
            <ItemTemplate>
                <a id="btnEditPartner" runat="server" class="thickbox" title='<%# Eval("pol_prefix", "Edit: {0} ")%>'
                    href='<%#  Eval("title_company_id", "~/Admin/Pages/AddEditTitleCompany.aspx?a=e&id={0}&TB_iframe=true&height=200&width=500") %>'>
                    <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Partner" style="height: 10px;
                        width: 10px;" />
                </a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
