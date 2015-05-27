<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrystalSetup.aspx.cs" Inherits="Greenspoon.Tess.Admin.Pages.CrystalSetup" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="FormSetup" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table class="table">
            <caption>Crystal Report Admin Page</caption>
            <tr>
                <td style="width: 30%; white-space: nowrap;">
                    Display Name:
                </td>
                <td style="width: 70%;" class="alt">
                    <asp:TextBox ID="txtReportDisplayName" runat="server" MaxLength="500" Width="60%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtReportDisplayName"
                        runat="server" Text="*" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%; white-space: nowrap;">
                    Report Type:
                </td>
                <td style="width: 70%;" class="alt">
                    <asp:DropDownList runat="server" ID="drpFormName" Width="35%">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Text="Batch Escrow" Value="BatchEscrow"></asp:ListItem>
                        <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                        <asp:ListItem Text="Foreclosure" Value="Foreclosure"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="drpFormName"
                        runat="server" Text="*" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%; white-space: nowrap;">
                    Select .rpt File:
                </td>
                <td style="width: 70%;" class="alt">
                    <asp:FileUpload ID="updReportFile" Width="90%" runat="server"  />
                    <asp:RequiredFieldValidator ID="rqdReportFile" ControlToValidate="updReportFile"
                        runat="server" Text="*" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                   <asp:Label ID="lblMsg" runat="server" EnableViewState="false" /> &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add Report" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div>
            <asp:GridView runat="server" ID="grdP" AutoGenerateColumns="false" OnRowDataBound="grdP_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Reports By Report Types">
                        <ItemTemplate>
                            <strong style="font-size: large;">
                                <%#Eval("FolderName")%></strong>
                            <br />
                            <asp:GridView ID="grdC" DataKeyNames="ReportID" runat="server" AutoGenerateColumns="false"
                                OnRowDeleting="DeleteRecord" Font-Size="13px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Display Name">
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblReportID" runat="server" Text='<%#Eval("ReportID")%>' />
                                            </div>
                                            <%#Eval("ReportDisplayName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <%#Eval("ReportFileName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Uploaded By">
                                        <ItemTemplate>
                                            <%#Eval("CreatedBy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Uploaded On">
                                        <ItemTemplate>
                                          <%--  <%#Eval("CreatedOn")%>--%>
                                            <%#DataBinder.Eval(Container.DataItem, "CreatedOn", "{0:d}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete?">
                                        <ItemTemplate>
                                            <span onclick="return confirm('Are you sure to Delete the record?')">
                                                <asp:LinkButton ID="lnkB" runat="Server" Text="Delete" CommandName="Delete" CausesValidation="false"></asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
