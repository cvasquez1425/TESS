<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pgc_tmplt_ret.ascx.cs"
    Inherits="Greenspoon.Tess.Admin.Controls.pgc_tmplt_ret" EnableViewState="false" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Ret" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEdit_pgc_tmplt_ret.aspx?a=n&TB_iframe=true&height=350&width=500") %>'
            class="thickbox">+ Add Ret</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="project_group_id" HeaderText="Proj Group Id" ReadOnly="true" />
            <asp:BoundField DataField="calc_field" HeaderText="Calc Field" ReadOnly="true" />
            <asp:BoundField DataField="calc_type" HeaderText="Calc Type" ReadOnly="true" />
            <asp:BoundField DataField="basis_table" HeaderText="Basis Table" ReadOnly="true" />
            <asp:BoundField DataField="basis_field" HeaderText="Basis Field" ReadOnly="true" />
            <asp:BoundField DataField="calc_value" HeaderText="Calc Value" ReadOnly="true" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("pgc_tmplt_ret_id", "Edit Ret ID: {0} ")%>'
                        href='<%#  Eval("pgc_tmplt_ret_id", "~/Admin/Pages/AddEdit_pgc_tmplt_ret.aspx?a=e&id={0}&TB_iframe=true&height=350&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Legal Fee" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
