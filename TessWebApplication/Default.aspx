<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TessWebApplication._Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<%--    <h2 style="float:left; margin-left:-150px;"> Welcome to TESS! </h2>--%>
    <h2> Welcome to TESS! </h2>

    <script runat="Server" type="text/C#">
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static AjaxControlToolkit.Slide[] GetSlides()
        {
            AjaxControlToolkit.Slide[] slides = new AjaxControlToolkit.Slide[10];
            return(slides);
        }
    </script>

   <%--
    <div id="divC" runat="server">
        <img alt="Loading" src="Images/spinner.gif" />
    </div>--%>

        <div style="text-align: center">

            &nbsp; &nbsp;<br />
            <br />
            
            <asp:Image ID="Image1" runat="server" Height="316px" 
                    Width="388px" 
                    style="border:1px solid black width:auto" 
                    ImageUrl="~/Images/Slides/TESS TimeShare Resorts.jpg" 
                    AlternateText="TESS TimeShare Resorts.jpg" />
            <br />
            <br />
            
            <asp:Label ID="lblImageDescription" runat="server" /><br />
            <br />
    
    
            <asp:Button ID="Btn_Previous" runat="server" Text="Previous" />
            <asp:Button ID="Btn_Play" runat="server" Text="Play" />
            <asp:Button ID="Btn_Next" runat="server" Text="Next" Width="64px" /><br />            
    
            <cc1:SlideShowExtender ID="SlideShowExtender1" runat="server"
                AutoPlay="true" 
                ImageDescriptionLabelID="lblImageDescription"
                Loop="true" 
                NextButtonID="Btn_Next" 
                PlayButtonID="Btn_Play" 
                PlayButtonText="Play" 
                PreviousButtonID="Btn_Previous" 
                SlideShowServiceMethod="GetSlides" StopButtonText="Stop"
                TargetControlID="Image1" 
                SlideShowServicePath="SlideService.asmx">
            </cc1:SlideShowExtender>       
        </div>
</asp:Content>

