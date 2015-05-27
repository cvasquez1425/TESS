<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScannedImages.ascx.cs"
    Inherits="Greenspoon.Tess.Controls.ScannedImages" %>
<div class="divSubForm">
    <div style="float: right; padding:0px 10px 0px 0px;">
      <asp:LinkButton ID="btnShowImage" runat="server" visible="true" CausesValidation="false" 
            enableviewstate="true" Text="Load Images"></asp:LinkButton>
    </div>
    <asp:GridView ID="gvScannedImages" runat="server" Font-Size="XX-Small" AutoGenerateColumns="false"
        EmptyDataText="No Images" EnableViewState="false" Visible="false">
        <Columns>
            <asp:TemplateField ItemStyle-Width="0px" ItemStyle-CssClass="headerstyle">
                <ItemTemplate>
                    <a id="btnShowImage" target="_blank" href='<%# Eval("DocLocation") %>'>
                        <img src='<%= Page.ResolveUrl("~/Images/icon_quick_search.gif") %>' alt="Click to see Image"
                            style="height: 10px; width: 10px;" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="docnum" HeaderText="Document Number" ReadOnly="true" />
            <asp:BoundField DataField="docname" HeaderText="Document Name" ReadOnly="true" />
        </Columns>
    </asp:GridView>
</div>
