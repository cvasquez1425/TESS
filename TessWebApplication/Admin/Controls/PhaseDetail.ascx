<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhaseDetail.ascx.cs"
    EnableViewState="false" Inherits="Greenspoon.Tess.Admin.Controls.PhaseDetail" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Phase Detail" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEditPhaseDetail.aspx?a=n&TB_iframe=true&height=300&width=600") %>'
            class="thickbox">+ Add Phase Detail</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Project Name">
                <ItemTemplate>
                    <%# Eval("project.project_name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phase Name">
                <ItemTemplate>
                    <%# Eval("phase_name.phase_name1") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="phs_or_book" HeaderText="Phs or Book" ReadOnly="true" />
            <asp:BoundField DataField="phs_or_page" HeaderText="Phs or Page" ReadOnly="true" />
            <asp:BoundField DataField="createdby" HeaderText="Created By" ReadOnly="true" />
            <asp:BoundField DataField="createddate" HeaderText="Created On" ReadOnly="true" DataFormatString="{0:M/d/yyyy}" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("phase_detail_id", "Edit Phase Detail ID: {0} ")%>'
                        href='<%#  Eval("phase_detail_id", "~/Admin/Pages/AddEditPhaseDetail.aspx?a=e&id={0}&TB_iframe=true&height=300&width=600") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Phase Detail" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
