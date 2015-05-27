<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="WebFormPleaseWait.aspx.cs" Inherits="Greenspoon.Tess.Pages.WebFormPleaseWait" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #UpdatePanel1
        {
            width: 300px;
            height: 100px;
        }        
    </style>
<%--http://localhost:58872/Pages/WebFormPleaseWait.aspx--%>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                    <legend>UpdatePanel</legend>
                    <asp:Label ID="Label1" runat="server" Text="Initial page rendered."></asp:Label><br />
                    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                Processing...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
