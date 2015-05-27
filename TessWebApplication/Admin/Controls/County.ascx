<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="County.ascx.cs" EnableViewState="false"
    Inherits="Greenspoon.Tess.Admin.Controls.County" %>
<div style="width: 90%; text-align: right; float: right; border: none;">
    <a id="btnAdd" title="Add County" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditCounty.aspx?a=n&TB_iframe=true&height=600&width=800") %>'
        class="thickbox">+ Add County</a>
</div>
<asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
    EmptyDataText="No Data" Width="100%">
    <Columns>
        
        <asp:BoundField DataField="county_name" HeaderText="County Name" ReadOnly="true" />
        <asp:BoundField DataField="county_circuit" HeaderText="Circuit" ReadOnly="true" />
        <asp:BoundField DataField="address1" HeaderText="Address 1" ReadOnly="true" />
        <asp:BoundField DataField="address2" HeaderText="Address 2" ReadOnly="true" />
        <asp:BoundField DataField="city" HeaderText="City" ReadOnly="true" />
        <asp:TemplateField HeaderText="State">
            <ItemTemplate>
                <%# Eval("state.state_name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="zip" HeaderText="Zip" ReadOnly="true" />
        <asp:BoundField DataField="clerk" HeaderText="Clerk" ReadOnly="true" />
        <asp:BoundField DataField="phone1" HeaderText="Phone 1" ReadOnly="true" />
        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ReadOnly="true" />
        <asp:BoundField DataField="CreatedDate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/dd/yyyy}" />
        <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
            <ItemTemplate>
                <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("county_name", "Edit: {0} ")%>'
                    href='<%#  Eval("county_id", "~/Admin/Pages/AddEditCounty.aspx?a=e&id={0}&TB_iframe=true&height=600&width=800") %>'>
                    <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit County" style="height: 10px;
                        width: 10px;" />
                </a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
