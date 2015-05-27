<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pgc_tmplt_stamp_sd.ascx.cs"
    Inherits="Greenspoon.Tess.Admin.Controls.pgc_tmplt_stamp_sd" EnableViewState="false" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Stamp SD" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEdit_pgc_tmplt_stamp_sd.aspx?a=n&TB_iframe=true&height=350&width=800") %>'
            class="thickbox">+ Add Stamp SD</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="project_group_id" HeaderText="Proj Group Id" ReadOnly="true" />
            <asp:BoundField DataField="flat_rate" HeaderText="Flat Rate" ReadOnly="true" />
            <asp:BoundField DataField="calc_multiplier_table" HeaderText="Mult Table" ReadOnly="true" />
            <asp:BoundField DataField="calc_multiplier_field" HeaderText="Mult Field" ReadOnly="true" />
            <asp:BoundField DataField="field_type1" HeaderText="Field Type1" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_table1" HeaderText="Table1" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_field1" HeaderText="Field1" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_divisor1" HeaderText="Divisor1" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_rounding1" HeaderText="Rounding1"
                ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_table2" HeaderText="Table2" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_field2" HeaderText="Field2" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_divisor2" HeaderText="Divisor2" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_rounding2" HeaderText="Rounding2"
                ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_table3" HeaderText="Table3" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_field3" HeaderText="Field3" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_divisor3" HeaderText="Divisor3" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_rounding3" HeaderText="Rounding3"
                ReadOnly="true" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("pgc_tmplt_stamp_sd_id", "Edit Stamp SD ID: {0} ")%>'
                        href='<%#  Eval("pgc_tmplt_stamp_sd_id", "~/Admin/Pages/AddEdit_pgc_tmplt_stamp_sd.aspx?a=e&id={0}&TB_iframe=true&height=350&width=800") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Stamp SD" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
