<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pgc_tmplt_stamp_sn.ascx.cs"
    Inherits="Greenspoon.Tess.Admin.Controls.pgc_tmplt_stamp_sn" EnableViewState="false" %>

    <div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add Stamp SN" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEdit_pgc_tmplt_stamp_sn.aspx?a=n&TB_iframe=true&height=350&width=500") %>'
            class="thickbox">+ Add Stamp SN</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="project_group_id" HeaderText="Proj Grp ID" ReadOnly="true" />
            <asp:BoundField DataField="flat_rate" HeaderText="Flat Rate" ReadOnly="true" />
            <asp:BoundField DataField="calc_multiplier_table" HeaderText="Calc Mult Table" ReadOnly="true" />
            <asp:BoundField DataField="calc_multiplier_field" HeaderText="Calc Mult Field" ReadOnly="true" />
            <asp:BoundField DataField="field_type1" HeaderText="Field Type" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_table1" HeaderText="Calc Exec Table1" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_field1" HeaderText="Calc Exec Field1" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_divisor1" HeaderText="Calc Exec Divisor1" ReadOnly="true" />
            <asp:BoundField DataField="calc_exec_rounding1" HeaderText="Calc Exec Rounding1" ReadOnly="true" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("pgc_tmplt_stamp_sn_id", "Edit Stamp SN ID: {0} ")%>'
                        href='<%#  Eval("pgc_tmplt_stamp_sn_id", "~/Admin/Pages/AddEdit_pgc_tmplt_stamp_sn.aspx?a=e&id={0}&TB_iframe=true&height=350&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Stamp SN" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

