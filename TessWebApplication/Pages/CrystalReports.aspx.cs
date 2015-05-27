#region
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Services;
using System.Text;
#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class CrystalReports : PageBase
    {
        CrystalReportConfig _config;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack == false) {
                if (RecID > 0) {
                    txtParam.Text = RecID.ToString();
                    btnReturn.HRef = GetReturnPath();
                }
                else {
                    txtParam.Focus();
                    btnReturn.Disabled = true;
                }
                switch (FormName) {
                    case FormNameEnum.BatchEscrow:
                        lblParamLabel.Text = "Batch Escrow ID: ";
                        lblHeader.Text = "Batch Escrow Reports";
                        break;
                    case FormNameEnum.Cancel:
                        lblParamLabel.Text = "Batch Cancel ID: ";
                        lblHeader.Text = "Batch Cancel Reports";
                        break;
                    case FormNameEnum.Foreclosure:
                        lblParamLabel.Text = "Batch Foreclosure ID: ";
                        lblHeader.Text = "Batch Foreclosure Reports";
                        break;
                    default:
                        lblParamLabel.Text = "Unknown ID: ";
                        lblHeader.Text = "Unknown Report form.";
                        break;
                }
                grdReport.DataSource = GetReportList(FormName);
                grdReport.DataBind();
            }
        }

        //IList<Classes.ReportInfo> GetReportList(FormNameEnum form)
        //{
        //    var reportList = CrystalReportSetting.GetReportSettingByForm(form);
        //    return reportList.Select(r => new Classes.ReportInfo {
        //        DisplayName = r.ReportDisplayName,
        //        ReportNameLocation = string.Format("{0}/{1}/{2}", _config.CrystalReportBasePath, r.FolderName, r.ReportFileName)
        //    }).ToList();
        //}

        IList<Classes.ReportInfoBO> GetReportList(FormNameEnum form)
        {
            var reportList = CrystalReportSetting.GetReportBOSettingByForm(form);

            return reportList.Select(r => new Classes.ReportInfoBO
            {
                DisplayName             =   r.ReportDisplayName,
                ReportNameLocation      =   string.Format("{0}", r.ReportHyperlinkPdf),
                ReportNameLocationExcel =   string.Format("{0}", r.ReportHyperlinkExcel),
                ReportNameLocationWord  =   string.Format("{0}", r.ReportHyperlinkWord)
            }).ToList();
        }

        void SetPageBase()
        {
            _config     = CrystalReportConfig.Instance;
            RecID       = Request.QueryString.GetValue<int>("bid");
            ContractId  = Request.QueryString.GetValue<int>("cid");
            FormName    = Request.QueryString.GetValue<FormNameEnum>("form");
        }

        string GetReturnPath()
        {
            const string defaultPath = "~/Default.aspx";
            var previousPath = string.IsNullOrEmpty(Request.UrlReferrer.ToString()) ? defaultPath : Request.UrlReferrer.ToString();

            if (FormName.isBatchEscrow()) {
                return RecID > 0 
                        ? string.Format("~/Pages/BatchEscrow.aspx?a=e&beid={0}&cid={1}", RecID, ContractId)
                        : previousPath;
            }
            if (FormName.isCancel()) {
                return RecID > 0 
                        ? string.Format("~/Pages/BatchCancel.aspx?a=e&bcid={0}&cid={1}", RecID, ContractId) 
                        : previousPath;
            }
            if (FormName.isForeclosure()) {
                return RecID > 0 
                        ? string.Format("~/Pages/foreclosure.aspx?a=e&bfid={0}&cid={1}", RecID, ContractId) 
                        : previousPath;
            }
            return defaultPath;
        }

        // BO Links for PDF, Word, and Excel replacing Crystal Report Engine
        protected void ReportGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(txtParam.Text))
            {
                lblMsg.Text = "Sorry Please provide a valid batch id";
                txtParam.Focus();
                return;
            }
            try
            {
                var fileType = e.CommandName;
                var url = e.CommandArgument;
                string urlString = url.ToString();
                var sb = new StringBuilder();
                if (urlString.Contains("Escrow ID")) sb.Append(url).Replace("Escrow ID", GetBatchId().ToString());
                if (urlString.Contains("Cancel ID")) sb.Append(url).Replace("Cancel ID", GetBatchId().ToString());
                ResponseHelper.Redirect(sb.ToString(), "_blank", "");

            }
            catch (Exception ex)
            {
                ErrorMsg(ex.ToString());
            }
        }

        //protected void ReportGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtParam.Text)) {
        //        lblMsg.Text = "Sorry Please provide a valid batch id";
        //        txtParam.Focus();
        //        return;
        //    }
        //    try {
        //        var fileType = e.CommandName;
        //        var fileFormat = GetFileFormat(fileType);
        //        var reportNameLocation = Server.MapPath(e.CommandArgument.ToString());
        //        var paramName = GetParamName();

        //        using (var doc = new ReportDocument()) {
        //            doc.Load(reportNameLocation);
        //            SetTableLocation(doc.Database.Tables);
        //            var param = new ParameterValues();
        //            var pd = new ParameterDiscreteValue { Value = GetBatchId() };
        //            param.Add(pd);
        //            doc.DataDefinition.ParameterFields[paramName].ApplyCurrentValues(param);
        //            doc.ExportToHttpResponse(fileFormat, Response, true, GetFileName(reportNameLocation));
        //        }
        //    } catch (Exception ex) {
        //        ErrorMsg(ex.ToString());
        //    }
        //}

        int GetBatchId()
        {
            return int.Parse(txtParam.Text);
        }

        string GetParamName()
        {
            return
                FormName.isBatchEscrow()  ? "@BatchEscrowID"
                : FormName.isCancel() ? "@BatchCancelID"
                : FormName.isForeclosure() ? "@BatchForeclosureID"
                : "UNDEFINED";
        }

        // BO Links for PDF, Word, and Excel replacing Crystal Report Engine
        static string GetBOUrl(string fileType)
        {
            using(var ctx = DataContextFactory.CreateContext())
            {
                return fileType == "PDF" ? ctx.BusinessObjectSettings.Select(r => r.ReportHyperlinkPdf).ToString()
                                : fileType == "EXCEL" ? ctx.BusinessObjectSettings.Select(r => r.ReportHyperlinkExcel).ToString()
                                : fileType == "WORD" ? ctx.BusinessObjectSettings.Select(r => r.ReportHyperlinkWord).ToString()
                                : ctx.BusinessObjectSettings.Select(r => r.ReportHyperlinkPdf).ToString();
            }
        }

        static ExportFormatType GetFileFormat(string fileType)
        {
            return fileType == "PDF" ? ExportFormatType.PortableDocFormat 
                            : fileType == "EXCEL" ? ExportFormatType.Excel
                            : fileType == "WORD" ? ExportFormatType.WordForWindows
                            : ExportFormatType.NoFormat;
        }

        string GetFileName(string reportNameLocation)
        {
            var today = DateTime.Today;
            var reportName = Path.GetFileNameWithoutExtension(reportNameLocation);
            return string.Format("{0} BatchID_{1}_ON_{2}_{3}_{4}", reportName, GetBatchId(), today.Month, today.Day, today.Year);
        }

        void SetTableLocation(Tables tables)
        {
            var connectionInfo = new ConnectionInfo {
                ServerName = _config.DSNFileLocation,
                DatabaseName = _config.DatabaseName,
                UserID = _config.UserID,
                Password = _config.Password
            };

            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables) {
                var tableLogOnInfo = table.LogOnInfo;
                tableLogOnInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogOnInfo);
            }
        }

        void ErrorMsg(string msg = null)
        {
            lblMsg.Text = string.IsNullOrEmpty(msg) ? "Could not load report. Please contact admin." : msg;
        }

        protected void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (!AuthService.IsUserTessAdmin()) return;
            var ex = e.Row.Cells[2].FindControl("EXCEL") as ImageButton;
            if (ex != null) {
                ex.Enabled = true;
            }
            var wd = e.Row.Cells[3].FindControl("WORD") as ImageButton;
            if (wd != null) {
                wd.Enabled = true;
            }
        }
    }
}