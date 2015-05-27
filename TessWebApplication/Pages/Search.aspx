<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="Search.aspx.cs" Inherits="Greenspoon.Tess.Pages.Search" %>

<asp:Content ID="search" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="lblMessage" runat="server" ViewStateMode="Disabled" Font-Bold="true"
        ForeColor="Red" />
    <br />
    <br />
    <asp:DataGrid ID="resultView" runat="server" AutoGenerateColumns="False" 
        onitemdatabound="resultView_ItemDataBound">
        <Columns>
            <asp:TemplateColumn HeaderText="Master ID">
                <ItemTemplate>
                    <a href='BatchEscrow.aspx?a=e&beid=<%# DataBinder.Eval(Container.DataItem, "BatchEscrowId")%>&cid=<%# DataBinder.Eval(Container.DataItem, "ContractId")%>' name="MasterId">
                        <%# DataBinder.Eval(Container.DataItem, "ContractId")%> </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Batch ID">
                <ItemTemplate>
                    <a href='BatchEscrow.aspx?a=e&beid=<%# DataBinder.Eval(Container.DataItem, "BatchEscrowId")%>&cid=<%# DataBinder.Eval(Container.DataItem, "ContractId")%>'>
                        <%# DataBinder.Eval(Container.DataItem, "BatchEscrowId")%></a>
                </ItemTemplate>
            </asp:TemplateColumn>
             <asp:BoundColumn DataField="LegalLastName" HeaderText="Last Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="DevK" HeaderText="Dev K"></asp:BoundColumn>
            <asp:BoundColumn DataField="BatchEscrowNumber" HeaderText="Escrow Number"></asp:BoundColumn>
            <asp:BoundColumn DataField="ProjectId" HeaderText="Project Id"></asp:BoundColumn>
            <asp:BoundColumn DataField="BatchEscrowCreatedBy" HeaderText="Created By"></asp:BoundColumn>
            <asp:BoundColumn DataField="BatchEscrowCreateDate" HeaderText="Create Date" DataFormatString="{0:d}"></asp:BoundColumn>
            <asp:BoundColumn DataField="MasterId" HeaderText="MasterId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
