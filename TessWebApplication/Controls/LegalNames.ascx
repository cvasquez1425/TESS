<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LegalNames.ascx.cs"
    Inherits="Greenspoon.Tess.Controls.LegalNames" %>
<div>
    <script type="text/javascript">
        function LegalName_Updated() {
            //  close the popup
            tb_remove();
            //  refresh the update panel so we can view the changes  
            $('#<%= this.btnLoadLegalNames.ClientID %>').click();
        }
    </script>
</div>
<div runat="server" id="divLN" class="divLNShort">
    <asp:Button ID="btnLoadLegalNames" runat="server" Style="display: none" OnClick="Page_Load" />
    <a style="float:left;" id="btnNewLegalName" title="Add Legal Name" runat="server" class="thickbox" visible="false"
        enableviewstate="false">+ Add Legal Name</a>
    <a style="float:right;" id="btnExpandedView" runat="server" visible="false" enableviewstate="false">Expanded View</a>
    <asp:GridView ID="gvLegalNames" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Data" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Pri" ItemStyle-Width="0px">
                <ItemTemplate>
                    <div class="arrow-left" runat="server" id="pri" visible='<%# Eval("Primary") %>'>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" />
            <asp:BoundField DataField="Address1" HeaderText="Address 1" ReadOnly="true" />
            <asp:BoundField DataField="Address23" HeaderText="Address 2-3" ReadOnly="true" />
            <asp:BoundField DataField="City" HeaderText="City" ReadOnly="true" />
            <asp:BoundField DataField="State" HeaderText="State" ReadOnly="true" />
            <asp:BoundField DataField="Zip" HeaderText="Zip" ReadOnly="true" />
            <asp:BoundField DataField="Country" HeaderText="Country" ReadOnly="true" />
            <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true" />
            <asp:BoundField DataField="Order" HeaderText="Order" ReadOnly="true" />
            <asp:TemplateField HeaderText="Dismiss">
                <ItemTemplate>
                    <%# ( ((bool?)Eval("Dismiss") == true) ? "Yes" : "No" ) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active">
                <ItemTemplate>
                    <%# ( ((bool?)Eval("Active") == true) ? "Yes" : "No" ) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <div runat="server" id="divEdit" visible='<%# !ReadOnly %>'>
                    <a id="btnEditName" runat="server" class="thickbox" title='<%# Eval("Name", "Details for {0} ")%>'
                        href='<%# string.Format("~/Pages/LegalName.aspx?a=e&id={0}&cid={1}&form={2}&TB_iframe=true&height=630&width=700" , Eval("LegalNameId"), Eval("ContractId"), this.FormName ) %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/Edit.gif") %>' alt="Edit Legal Name" style="height: 10px;
                            width: 10px;" />
                    </a>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
