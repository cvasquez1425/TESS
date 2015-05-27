<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Firm.ascx.cs" EnableViewState="false"
    Inherits="Greenspoon.Tess.Admin.Controls.Firm" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Firm" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditFirm.aspx?a=n&TB_iframe=true&height=550&width=600") %>'
            class="thickbox">+ Add Firm</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="developer_group_id" HeaderText="Group ID" ReadOnly="true" />
            <asp:BoundField DataField="firm_code" HeaderText="Code" ReadOnly="true" />
            <asp:BoundField DataField="firm_designation" HeaderText="Designation" ReadOnly="true" />
            <asp:BoundField DataField="firm_name" HeaderText="Name" ReadOnly="true" />
            <asp:BoundField DataField="firm_address1" HeaderText="Address 1" ReadOnly="true" />
            <asp:BoundField DataField="firm_address2" HeaderText="Address 2" ReadOnly="true" />
            <asp:BoundField DataField="firm_replyto" HeaderText="Reply To" ReadOnly="true" />
            <asp:BoundField DataField="firm_agent" HeaderText="Agent" ReadOnly="true" />
            <asp:BoundField DataField="firm_agent_title" HeaderText="Agent Title" ReadOnly="true" />
            <asp:BoundField DataField="firm_bar_num" HeaderText="Bar Num" ReadOnly="true" />
            <asp:BoundField DataField="firm_gender" HeaderText="Gender" ReadOnly="true" />
            <asp:BoundField DataField="createdby" HeaderText="Created By" ReadOnly="true" />
            <asp:BoundField DataField="createddate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/d/yyyy}" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("firm_name", "Edit: {0} ")%>'
                        href='<%#  Eval("firm_id", "~/Admin/Pages/AddEditFirm.aspx?a=e&id={0}&TB_iframe=true&height=550&width=600") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Firm" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
