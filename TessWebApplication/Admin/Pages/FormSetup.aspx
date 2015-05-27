<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormSetup.aspx.cs" Inherits="Greenspoon.Tess.Admin.Pages.FormSetup" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="FormSetup" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <script type="text/javascript">
                    function FormSetup_Updated() {
                        //  close the popup
                        tb_remove();
                        //  refresh the update panel so we can view the changes  
                        $('#<%= this.btnLoadForm.ClientID %>').click();
                    }
                </script>
            </div>
            <asp:Button ID="btnLoadForm" runat="server" Style="display: none" OnClick="SetupForm_Load" />
            Pick Application Setup Form:
            <asp:DropDownList ID="drpForms" runat="server" AutoPostBack="true" 
             OnSelectedIndexChanged="drpForms_SelectedIndexChanged" Width="300px">
                <asp:ListItem Text="" Value="" />
                <asp:ListItem Text="Phase" Value="PhaseDetail" />
                <asp:ListItem Text="Title Company" Value="TitleCompany" />
                <asp:ListItem Text="Partner" Value="Partner" />
                <asp:ListItem Text="Year" Value="Year" />
                <asp:ListItem Text="Vesting" Value="Vesting" />
                <asp:ListItem Text="Developer" Value="Developer" />
                <asp:ListItem Text="Cancel Extra Type" Value="CancelExtraType" />
                <asp:ListItem Text="County" Value="County" />
                <asp:ListItem Text="Firm" Value="Firm" />
                <asp:ListItem Text="Judge" Value="Judge" />
                <asp:ListItem Text="Status" Value="StatusMaster" />
            </asp:DropDownList>
            <br />
            <asp:PlaceHolder ID="plcSetupForm" runat="server" EnableViewState="false"></asp:PlaceHolder>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
