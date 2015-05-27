<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Judge.ascx.cs" EnableViewState="false"
    Inherits="Greenspoon.Tess.Admin.Controls.Judge" %>
<div style="width: 90%; text-align: right; float: right; border: none;">
    <a id="btnAdd" title="Add Judge" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditJudge.aspx?a=n&TB_iframe=true&height=400&width=600") %>'
        class="thickbox">+ Add Judge</a>
</div>
<asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
    EmptyDataText="No Data" Width="100%">
    <Columns>
        <asp:BoundField DataField="CountyName" HeaderText="County Name" ReadOnly="true" />
        <asp:BoundField DataField="room" HeaderText="Room" ReadOnly="true" />
        <asp:BoundField DataField="division" HeaderText="Division" ReadOnly="true" />
        <asp:BoundField DataField="judge_name" HeaderText="Judge Name" ReadOnly="true" />
        <asp:BoundField DataField="judge_last_name" HeaderText="Last Name" ReadOnly="true" />
        <asp:BoundField DataField="phone" HeaderText="Phone" ReadOnly="true" />
        <asp:BoundField DataField="judge_active" HeaderText="Active" ReadOnly="true" />
        <asp:BoundField DataField="DocumentGroup" HeaderText="Document Group" ReadOnly="true" />
        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ReadOnly="true" />
        <asp:BoundField DataField="CreatedDate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/dd/yyyy}" />
        <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
            <ItemTemplate>
                <a id="btnEditPartner" runat="server" class="thickbox" title='<%# Eval("judge_name", "Edit: {0} ")%>'
                    href='<%#  Eval("judge_id", "~/Admin/Pages/AddEditJudge.aspx?a=e&id={0}&TB_iframe=true&height=400&width=600") %>'>
                    <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Partner" style="height: 10px;
                        width: 10px;" />
                </a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
