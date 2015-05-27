<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Status.ascx.cs" Inherits="Greenspoon.Tess.Controls.Status" %>
<div>
    <script type="text/javascript">
        function Status_Updated() {
            //  close the popup
            tb_remove();
            //  refresh the update panel so we can view the changes  
            $('#<%= this.btnLoadStatusForm.ClientID %>').click();
        }
    </script>
</div>
<div class="divSubForm">
    <asp:Button ID="btnLoadStatusForm" runat="server" Style="display: none" OnClick="Page_Load" />
    <table style="padding: 0px; margin: 0px;" width="97%">
        <tr>
            <td style="width: 50%;">
                <a id="btnNewStatus" title= "Add Status" runat="server" class="thickbox" visible="false"
                    enableviewstate="false">+ Add Status</a>
            </td>
            <td style="width: 50%; text-align: right;">
                <a id="btnExpandedView" runat="server" visible="false" enableviewstate="false">Expanded
                    View</a>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvStatus" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data"
        EnableViewState="false">
        <Columns>
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <%-- RIQ-255 Need Master ID (Contract_id) on top of Contract Status Form
                    <a id="btnEditStatus" runat="server" class="thickbox" title='<%# Eval("StatusId", "Edit Record {0} ")%>'--%>
                    <a id="btnEditStatus" runat="server" class="thickbox" title='<%# string.Format("Edit Record {0}   ContractID {1} ", Eval("StatusId"), Eval("ContractId") )%>'
                        href='<%# string.Format("~/Pages/status.aspx?a=e&id={0}&cid={1}&form={2}&TB_iframe=true&height=600&width=550" , Eval("StatusId"), Eval("ContractId"), this.FormName ) %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Contract Status"
                            style="height: 10px; width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StatusMasterId" HeaderText="Status ID" ReadOnly="true" />
            <asp:BoundField DataField="Invoice" HeaderText="Invoice" ReadOnly="true" />
            <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" ReadOnly="true" />
            <asp:BoundField DataField="Book" HeaderText="Book" ReadOnly="true" />
            <asp:BoundField DataField="Page" HeaderText="Page" ReadOnly="true" />
<%-- RIQ-302           <asp:BoundField DataField="AssignmentNumber" HeaderText="Assignment #" ReadOnly="true" />--%>
            <asp:BoundField DataField="RecDate" HeaderText="Recording Date" ReadOnly="true" />
        </Columns>
    </asp:GridView>
    <table style="padding: 0px; margin: 0px;" width="97%">
        <tr>
            <td style="width: 50%;">
                <a id="btnNewStatusB" title="Add Status" runat="server" class="thickbox" visible="false"
                    enableviewstate="false">+ Add Status</a>
            </td>
            <td style="width: 50%; text-align: right;">
                <a id="btnExpandedViewB" runat="server" visible="false" enableviewstate="false">Expanded
                    View</a>
            </td>
        </tr>
    </table>
</div>
