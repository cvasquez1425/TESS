<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pgc_tmplt_tang_tax_mort.ascx.cs"
    Inherits="Greenspoon.Tess.Admin.Controls.pgc_tmplt_tang_tax_mort" EnableViewState="false" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Tax Mort" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEdit_pgc_tmplt_tang_tax_mort.aspx?a=n&TB_iframe=true&height=350&width=500") %>'
            class="thickbox">+ Add Tax Mort</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="project_group_id" HeaderText="Proj Group Id" ReadOnly="true" />
            <asp:BoundField DataField="calc_basis_table" HeaderText="Calc Basis Table" ReadOnly="true" />
            <asp:BoundField DataField="calc_basis_field" HeaderText="Calc Basis Field" ReadOnly="true" />
            <asp:BoundField DataField="project_field_type" HeaderText="Project Field Type" ReadOnly="true" />
            <asp:BoundField DataField="calc_multiplier_table" HeaderText="Calc Multiplier Table" ReadOnly="true" />
            <asp:BoundField DataField="calc_multiplier_field" HeaderText="Calc Multiplier Field" ReadOnly="true" />
            <asp:BoundField DataField="rounding" HeaderText="Rounding" ReadOnly="true" />
            <asp:BoundField DataField="flat_rate" HeaderText="Flat Rate" ReadOnly="true" />
            <asp:BoundField DataField="include_rounding_calc" HeaderText="Rounding Calc" ReadOnly="true" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("pgc_tmplt_tang_tax_mort_id", "Edit Tax Mort ID: {0} ")%>'
                        href='<%#  Eval("pgc_tmplt_tang_tax_mort_id", "~/Admin/Pages/AddEdit_pgc_tmplt_tang_tax_mort.aspx?a=e&id={0}&TB_iframe=true&height=350&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Tax Mort" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
