<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContractToForeclosure.aspx.cs"
    Inherits="Greenspoon.Tess.Pages.AddContractToForeclosure" %>

<%@ OutputCache Duration="86400" VaryByParam="none" Location="Client" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Contract to Foreclosure</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
    <div style="padding: 4px; margin: 4px; width: 100%;">

        <div style="padding: 0px; margin: 0px; width: 90%;">
        Please provide single or a list of valid Master Ids. List can be in any format.
            <asp:TextBox ID="txtContractIds" runat="server" TextMode="MultiLine" Height="100px"
                Width="95%" />
            <asp:RequiredFieldValidator ID="rfvContracts" runat="server" ControlToValidate="txtContractIds">* Required</asp:RequiredFieldValidator>
        </div>
        <div style="text-align: right; width: 90%; padding: 0px; margin: 0px;">
            <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" OnClick="btnSave_Click" />
        </div>
        <div id="divMsg" runat="server" style="color: Red; padding: 0px; margin: 0px; width: 90%; overflow: auto; vertical-align: text-top;
            height: 100px; padding: 8px; margin: 4px; vertical-align: text-top;">
        </div>
    </div>
    </form>
</body>
</html>
