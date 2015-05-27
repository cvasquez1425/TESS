<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RulesSetup.aspx.cs" Inherits="Greenspoon.Tess.Admin.Pages.RulesSetup" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="RulesSetup" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <script type="text/javascript">
                    function RulesSetup_Updated() {
                        tb_remove();
                        $('#<%= this.btnLoadForm.ClientID %>').click();
                    }
                </script>
            </div>
            <asp:Button ID="btnLoadForm" runat="server" Style="display: none" OnClick="SetupForm_Load" />
            Pick Rules Setup Form:
            <asp:DropDownList ID="drpForms" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpForms_SelectedIndexChanged"
                Width="300px">
                <asp:ListItem Text="" Value="" />
                <asp:ListItem Text="Premium" Value="pgc_tmplt_premium" />
                <asp:ListItem Text="Closed 20" Value="pgc_tmplt_closed20" />
                <asp:ListItem Text="Stamp SN" Value="pgc_tmplt_stamp_sn" />
                <asp:ListItem Text="CC100" Value="pgc_tmplt_cc100" />
                <asp:ListItem Text="RPT Title" Value="pgc_tmplt_rpt_title" />
                <asp:ListItem Text="Legal Fees" Value="pgc_tmplt_legal_fees" />
                <asp:ListItem Text="Stamp SD" Value="pgc_tmplt_stamp_sd" />
                <asp:ListItem Text="Ret" Value="pgc_tmplt_ret" />
                <asp:ListItem Text="Tang Tax Mort" Value="pgc_tmplt_tang_tax_mort" />
            </asp:DropDownList>
            <br />
            <asp:PlaceHolder ID="plcSetupForm" runat="server" EnableViewState="false"></asp:PlaceHolder>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
