<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Developer.ascx.cs" EnableViewState="false"
    Inherits="Greenspoon.Tess.Admin.Controls.Developer" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Developer" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditDeveloper.aspx?a=n&TB_iframe=true&height=550&width=600") %>'
            class="thickbox">+ Add Developer</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="developer_group_id" HeaderText="Group" ReadOnly="true" />
            <asp:BoundField DataField="developer_name" HeaderText="Name" ReadOnly="true" />
            <asp:BoundField DataField="address1" HeaderText="Address 1" ReadOnly="true" />
            <asp:BoundField DataField="address2" HeaderText="Address 2" ReadOnly="true" />
            <asp:BoundField DataField="address3" HeaderText="Address 3" ReadOnly="true" />
            <asp:BoundField DataField="alt_address1" HeaderText="Alt Address1" ReadOnly="true" />
            <asp:BoundField DataField="alt_address2" HeaderText="Alt Address2" ReadOnly="true" />
            <asp:BoundField DataField="alt_address3" HeaderText="Alt Address3" ReadOnly="true" />
            <asp:BoundField DataField="reassign" HeaderText="Reassign" ReadOnly="true" />
            <asp:BoundField DataField="active" HeaderText="Active" ReadOnly="true" />
            <asp:BoundField DataField="intro_atty" HeaderText="Intro Atty" ReadOnly="true" />
            <asp:BoundField DataField="billing_atty" HeaderText="Billing Atty" ReadOnly="true" />
            <asp:TemplateField HeaderText="Text">
                <ItemTemplate>
                    <a class="info" href="#">
                        <div class="arrow-down" runat="server" id="master">
                        </div>
                        <span><b>Group Name:</b>
                            <%# Eval("developer_txt")%>
                        </span></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="developer_pg2" HeaderText="PG2" ReadOnly="true" />
            <asp:BoundField DataField="createdby" HeaderText="Created By" ReadOnly="true" />
            <asp:BoundField DataField="createddate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/d/yyyy}" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEditPartner" runat="server" class="thickbox" title='<%# Eval("developer_name", "Edit: {0} ")%>'
                        href='<%#  Eval("developer_id", "~/Admin/Pages/AddEditDeveloper.aspx?a=e&id={0}&TB_iframe=true&height=550&width=600") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Partner" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
