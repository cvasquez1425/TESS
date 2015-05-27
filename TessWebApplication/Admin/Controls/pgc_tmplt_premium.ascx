<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pgc_tmplt_premium.ascx.cs" Inherits="Greenspoon.Tess.Admin.Controls.pgc_tmplt_premium" EnableViewState="false" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Premium" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEdit_pgc_tmplt_premium.aspx?a=n&TB_iframe=true&height=350&width=500") %>'
            class="thickbox">+ Add Premium</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="project_group_id" HeaderText="Proj Grp ID" ReadOnly="true" />
            <asp:BoundField DataField="calc_basis_field" HeaderText="Basis Field" ReadOnly="true" />
            <asp:BoundField DataField="calc_basis_table" HeaderText="Basis Table" ReadOnly="true" />
            <asp:BoundField DataField="basis_field_multiplier" HeaderText="Field Multiplier" ReadOnly="true" />
            <asp:BoundField DataField="criteria_start_amount" HeaderText="Start Amount" ReadOnly="true" />
            <asp:BoundField DataField="criteria_end_amount" HeaderText="End Amount" ReadOnly="true" />
            <asp:BoundField DataField="calc_type" HeaderText="Type" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_table" HeaderText="Exec Table" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_field" HeaderText="Exec Field" ReadOnly="true" />
            <asp:BoundField DataField="flat_amt" HeaderText="Flat Amt" ReadOnly="true" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("pgc_tmplt_premium_id", "Edit Premium ID: {0} ")%>'
                        href='<%#  Eval("pgc_tmplt_premium_id", "~/Admin/Pages/AddEdit_pgc_tmplt_premium.aspx?a=e&id={0}&TB_iframe=true&height=350&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Premium" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
