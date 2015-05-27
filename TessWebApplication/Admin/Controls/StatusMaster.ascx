<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatusMaster.ascx.cs"
    EnableViewState="false" Inherits="Greenspoon.Tess.Admin.Controls.StatusMaster" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Status" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditStatusMaster.aspx?a=n&TB_iframe=true&height=550&width=600") %>'
            class="thickbox">+ Add Status</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Group Name">
                <ItemTemplate>
                    <%# Eval("status_group.status_group_name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="status_master_id" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="status_master_name" HeaderText="Status Master Name" ReadOnly="true" />
            <asp:BoundField DataField="status_master_active" HeaderText="Active" ReadOnly="true" />
<%--        RIQ-307    Rename Status Master Labels />--%>
            <asp:BoundField DataField="is_comment" HeaderText="Comment Required" ReadOnly="true" />
            <asp:BoundField DataField="is_deleted_allowed" HeaderText="Delete Blocked" ReadOnly="true" />
<%--//             RIQ-289 CVJan2013 
//            <asp:BoundField DataField="req_datestamp" HeaderText="Date Stamp ?" ReadOnly="true" />
//            <asp:BoundField DataField="req_eff_date" HeaderText="Eff Date?" ReadOnly="true" />
//            <asp:BoundField DataField="req_rec_date" HeaderText="Rec Date?" ReadOnly="true" />
//            <asp:BoundField DataField="req_book" HeaderText="Book ?" ReadOnly="true" />
//            <asp:BoundField DataField="req_page" HeaderText="Page ?" ReadOnly="true" />
//            <asp:BoundField DataField="req_batch" HeaderText="Batch?" ReadOnly="true" />
//            <asp:BoundField DataField="req_county_name" HeaderText="County ?" ReadOnly="true" />
//            <asp:BoundField DataField="req_assign_num" HeaderText="Assign Num ?" ReadOnly="true" />
//            <asp:BoundField DataField="req_original_county" HeaderText="Org County?" ReadOnly="true" />   
--%>        <asp:BoundField DataField="is_legal_name_required" HeaderText="Legal Name Required" ReadOnly="true" />
            <asp:BoundField DataField="is_cancel_escrow" HeaderText="Cancel Escrow Required" ReadOnly="true" />
            <asp:BoundField DataField="createdby" HeaderText="Created By" ReadOnly="true" />
            <asp:BoundField DataField="createddate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/d/yyyy}" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("status_master_id", "Edit Status Master ID: {0} ")%>'
                        href='<%#  Eval("status_master_id", "~/Admin/Pages/AddEditStatusMaster.aspx?a=e&id={0}&TB_iframe=true&height=550&width=600") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Partner" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="A1" title="Add Status" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditStatusMaster.aspx?a=n&TB_iframe=true&height=550&width=600") %>'
            class="thickbox">+ Add Status</a>
    </div>
</div>
