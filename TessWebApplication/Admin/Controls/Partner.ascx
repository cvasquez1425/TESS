<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Partner.ascx.cs" EnableViewState="false"
    Inherits="Greenspoon.Tess.Admin.Controls.Partner" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Partner" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditPartner.aspx?a=n&TB_iframe=true&height=200&width=500") %>'
            class="thickbox">+ Add Partner</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="true" />
            <asp:BoundField DataField="PartnerName" HeaderText="Partner Name" ReadOnly="true" />
            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ReadOnly="true" />
            <asp:BoundField DataField="CreatedDate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/dd/yyyy}" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEditPartner" runat="server" class="thickbox" title='<%# Eval("PartnerName", "Edit: {0} ")%>'
                        href='<%#  Eval("PartnerId", "~/Admin/Pages/AddEditPartner.aspx?a=e&id={0}&TB_iframe=true&height=200&width=600") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Partner" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
