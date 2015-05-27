<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pgc_tmplt_rpt_title.ascx.cs"
    Inherits="Greenspoon.Tess.Admin.Controls.pgc_tmplt_rpt_title" EnableViewState="false" %>
<div style="width: 100%; padding: 0px; margin: 0px;">
    <div style="width: 90%; text-align: right; float: right; border: none;">
        <a id="btnAdd" title="Add RPT Title" href='<%= Page.ResolveUrl("~/Admin/Pages/AddEdit_pgc_tmplt_rpt_title.aspx?a=n&TB_iframe=true&height=350&width=500") %>'
            class="thickbox">+ Add RPT Title</a>
    </div>
    <asp:GridView ID="gvData" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:BoundField DataField="project_group_id" HeaderText="Proj Grp ID" ReadOnly="true" />
            <asp:BoundField DataField="calc_field" HeaderText="Calc Field" ReadOnly="true" />
            <asp:BoundField DataField="criteria_start_amount" HeaderText="Start Amount" ReadOnly="true" />
            <asp:BoundField DataField="criteria_end_amount" HeaderText="End Amount" ReadOnly="true" />
            <asp:BoundField DataField="field_type" HeaderText="Field Type" ReadOnly="true" />
            <asp:BoundField DataField="calc_value" HeaderText="Calc Value" ReadOnly="true" />
            <asp:BoundField DataField="flat_value" HeaderText="Flat Value" ReadOnly="true" />
            <asp:BoundField DataField="use_base" HeaderText="Use Base" ReadOnly="true" />
            <asp:BoundField DataField="calc_basis" HeaderText="Calc Basis" ReadOnly="true" />
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnEdit" runat="server" class="thickbox" title='<%# Eval("pgc_tmplt_rpt_title_id", "Edit RPT Title ID: {0} ")%>'
                        href='<%#  Eval("pgc_tmplt_rpt_title_id", "~/Admin/Pages/AddEdit_pgc_tmplt_rpt_title.aspx?a=e&id={0}&TB_iframe=true&height=350&width=500") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit RPT Title" style="height: 10px;
                            width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
