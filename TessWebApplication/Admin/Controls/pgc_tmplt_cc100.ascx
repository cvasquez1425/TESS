<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pgc_tmplt_cc100.ascx.cs"
    Inherits="Greenspoon.Tess.Admin.Controls.pgc_tmplt_cc100" EnableViewState="false" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add CC 100" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEdit_pgc_tmplt_cc100.aspx?a=n&TB_iframe=true&height=450&width=500") %>'
            class="thickbox">+ Add CC 100</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="criteria_basis_table" HeaderText="Cri Table" ReadOnly="true" />
            <asp:BoundField DataField="criteria_basis_field" HeaderText="Cri Field" ReadOnly="true" />
            <asp:BoundField DataField="criteria_start_amount" HeaderText="Start Amount" ReadOnly="true" />
            <asp:BoundField DataField="criteria_end_amount" HeaderText="End Amount" ReadOnly="true" />
            <asp:BoundField DataField="include" HeaderText="Include" ReadOnly="true" />
            <asp:BoundField DataField="percentage" HeaderText="Percentage" ReadOnly="true" />
            <asp:BoundField DataField="flat_rate" HeaderText="Flat Rate" ReadOnly="true" />
            <asp:BoundField DataField="calc_basis_table" HeaderText="Calc Table" ReadOnly="true" />
            <asp:BoundField DataField="calc_basis_field" HeaderText="Calc Field" ReadOnly="true" />
            <asp:BoundField DataField="rounding" HeaderText="Rounding" ReadOnly="true" />
            <asp:BoundField DataField="project_group_id" HeaderText="Prj Grp ID" ReadOnly="true" />
            <asp:BoundField DataField="include_rounding_calc" HeaderText="Inc Round Calc" ReadOnly="true" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("pgc_tmplt_cc100_id", "Edit CC 100 ID: {0} ")%>'
                        href='<%#  Eval("pgc_tmplt_cc100_id", "~/Admin/Pages/AddEdit_pgc_tmplt_cc100.aspx?a=e&id={0}&TB_iframe=true&height=450&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit CC 100" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>