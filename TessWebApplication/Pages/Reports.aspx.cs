#region Includes
using System;
using Rse2005 = Greenspoon.Tess.ReportExecution2005;
using System.Collections.Generic;
using Greenspoon.Tess.Classes;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.Web.UI;
#endregion
namespace Greenspoon.Tess.Pages {
    public partial class Reports : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(Page.IsPostBack == false) {
                // Create the return path link.
                btnReturn.HRef = GetReturnPath();
                if(base.RecID > 0) {
                    txtParam.Text = base.RecID.ToString();
                }
                List<ReportInfo> riList = null;
                switch(base.FormName) {
                    case FormNameEnum.BatchEscrow:
                        lblParamLabel.Text = "Batch Escrow ID: ";
                        lblHeader.Text = "Batch Escrow Reports";
                        riList = CreateBatchReportList();
                        break;
                    case FormNameEnum.Cancel:
                        lblParamLabel.Text = "Batch Cancel ID: ";
                        lblHeader.Text = "Batch Cancel Reports";
                        riList = CreateCancelReportList();
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
               if(riList != null) {
                    grdReport.DataSource = riList;
                    grdReport.DataBind();
                }
            }
        }
        
        List<ReportInfo> CreateBatchReportList() {
            ReportInfo ri = null;
            List<ReportInfo> riList = new List<ReportInfo>();

            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/Setting/BatchReports.xml"));
            XmlElement root = doc.DocumentElement;
            XmlNodeList reports = root.SelectNodes("Report");
            foreach(XmlNode item in reports) {
                ri = new ReportInfo {
                    DisplayName = item["DisplayName"].InnerText,
                    ReportNameLocation = item["ReportNameLocation"].InnerText,
                };
                riList.Add(ri);
            }
            return riList;
        }
        
        List<ReportInfo> CreateCancelReportList() {
            ReportInfo ri = null;
            List<ReportInfo> riList = new List<ReportInfo>();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/Setting/CancelReports.xml"));
            XmlElement root = doc.DocumentElement;
            XmlNodeList reports = root.SelectNodes("Report");
            foreach(XmlNode item in reports) {
                ri = new ReportInfo {
                    DisplayName = item["DisplayName"].InnerText,
                    ReportNameLocation = item["ReportNameLocation"].InnerText,
                };
                riList.Add(ri);
            }
            return riList;
        }

        void GetReport(string param1, string paramName, string reportNameLocation, string fileType, string fileExtension) {
            try {
                if(string.IsNullOrEmpty(txtParam.Text.Trim()) == true) {
                    lblMsg.Text = "Sorry Please provide a valid batch id";
                    return;
                }
                string historyID = null;
                string ReportParam1 = txtParam.Text.Trim();
                string deviceInfo = @"<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>";
                string format = fileType;
                Byte[] results;
                string encoding = String.Empty;
                string mimeType = String.Empty;
                string extension = String.Empty;
                Rse2005.Warning[] warnings = null;
                string[] streamIDs = null;

                Rse2005.ReportExecutionService rsExec = new Rse2005.ReportExecutionService();
                //rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;
                rsExec.Credentials = 
                    new NetworkCredential(TessHelper.ReportingServerUserName
                                         , TessHelper.ReportingServerPassword
                                         , TessHelper.ReportingServerUserDomain );
                Rse2005.ExecutionInfo ei = rsExec.LoadReport(reportNameLocation, historyID);
                Rse2005.ParameterValue[] rptParameters = new Rse2005.ParameterValue[1];

                rptParameters[0] = new Rse2005.ParameterValue();
                rptParameters[0].Name = paramName;
                rptParameters[0].Value = param1;

                //render the PDF
                rsExec.SetExecutionParameters(rptParameters, "en-us");
                results = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                Response.AddHeader("content-disposition:attachment;", string.Format("attachment; filename={0}_{1}_{2}_{3}.{4}",
                    param1
                    , DateTime.Now.Month
                    , DateTime.Now.Day
                    , DateTime.Now.Year
                    , fileExtension));
                // This has been added to support chrome download.
                Response.AppendHeader("Content-Type", "application/octet-stream");
                Response.OutputStream.Write(results, 0, results.Length);

                //This is very important if you want to directly download from stream instead of file
                Response.End();
            }
            catch(Exception ex) { lblMsg.Text = "ERROR: " + ex.ToString(); }
        }

        protected void ReportGridView_RowCommand(object sender, GridViewCommandEventArgs e) {
            string fileType           = e.CommandName;
            string fileExtension      = string.Empty;
            string reportNameLocation = e.CommandArgument.ToString();
            string paramName          = string.Empty;

            paramName = base.FormName.isBatchEscrow()  ? "BatchEscrowID"
                                                       : base.FormName.isCancel() ? "BatchCancelID"
                                                       : base.FormName.isForeclosure() ? "BatchForeclosureID"
                                                       : "Unknown";
            fileExtension = fileType == "PDF" ? "pdf" 
                            : fileType == "EXCEL" ? "xls" 
                            : fileType == "WORD" ? "doc" 
                            : "pdf";
            GetReport(base.RecID.ToString(), paramName, reportNameLocation, fileType, fileExtension);
        }

        void SetPageBase() {
            base.ContractId = Request.QueryString.GetValue<int>("cid");
            base.RecID      = Request.QueryString.GetValue<int>("bid");
            base.FormName   = Request.QueryString.GetValue<FormNameEnum>("form");
        }

        string GetReturnPath() {
            string returnPath   = string.Empty;
            string previousPath = Request.UrlReferrer.ToString();
            string defaultPath  = "~/Default.aspx";
            switch(base.FormName) {
                case FormNameEnum.BatchEscrow:
                    returnPath = base.RecID > 0 
                        ? string.Format("~/Pages/BatchEscrow.aspx?a=e&beid={0}&cid={1}", base.RecID, base.ContractId)
                        : previousPath;
                    break;
                case FormNameEnum.Cancel:
                    returnPath = base.RecID > 0 
                        ? string.Format("~/Pages/BatchCancel.aspx?a=e&bcid={0}&cid={1}", base.RecID, base.ContractId) 
                        : previousPath;
                    break;
                case FormNameEnum.Foreclosure:
                    returnPath = base.RecID > 0 
                        ? string.Format("~/Pages/foreclosure.aspx?a=e&bfid={0}&cid={1}", base.RecID, base.ContractId) 
                        : previousPath;
                    break;
                default:
                    returnPath = defaultPath;
                    break;
            }
            // Return the path. If empty: return default page.
            return string.IsNullOrEmpty(returnPath) == false ? returnPath : defaultPath;
        }
    } // end of class
}


#region Legacy

//private const string REPORT_NAME = "EscrowDisbursementSummary";
//        //Name of your report rdl file, If your report name is EmployeeReport.rdl, then only put     EmployeeName as the report name above
//        public const string PIPE = "|";
//        private const string DEVICE_INFO = @"<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>";
//        //Desired format goes here (PDF, Excel)
//        private const string PDF_FORMAT = "PDF";
//        private const string EXCEL_FORMAT = "Excel";
//        private const string CONTENT_TYPE_PDF = "application/pdf";
//        private const string CONTENT_DISPOSITION = "Content-disposition";
//        private const string CONTENT_TYPE_XLS = "application/excel";
//private void ShowReport() {
//    string batchEscrowId = "84363";
//    char[] seprator = { '|' };
//    string data = String.Empty;
//    string data1 = String.Empty;
//    string data2 = String.Empty;
//    string strParms = String.Empty;

//    strParms = "BatchEscrowID^" + batchEscrowId;
//    string[] parmArray = strParms.Split(seprator);

//    ReportParameter[] parm = new ReportParameter[parmArray.Length];
//    for(int i = 0; i < parmArray.Length; i++) {
//        data = parmArray[i];
//        data1 = data.Substring(0, data.IndexOf("^"));
//        data2 = data.Substring(data.IndexOf("^") + 1);
//        parm[i] = new ReportParameter(data1, data2);
//    }
//    try {
//       // displayReport(ReportViewer1, REPORT_NAME, parm);
//    }
//    catch(Exception ex) {

//    }

//}

//public void displayReport(ReportViewer reportViewer, string reportName, ReportParameter[] parm) {
//    string reportFormat = "PDF";
//    string report_file_name_pdf = reportName + ".pdf";
//    string report_file_name_xls = reportName + ".xls";
//    reportViewer.ShowCredentialPrompts = false;
//    reportViewer.ShowParameterPrompts = false;
//    reportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
//    reportViewer.ServerReport.ReportServerUrl = new System.Uri("http://10.0.0.105/ReportServer");
//    reportViewer.ServerReport.ReportPath = "/"+ "Tess Reports/Escrow Reports"+ "/" + reportName;
//     // to export in pdf format
//    string mimeType, encoding, extension, deviceInfo;
//    string[] streamids;
//    byte[] bytes;
//    Microsoft.Reporting.WebForms.Warning[] warnings;
//    deviceInfo = DEVICE_INFO;
//    try {
//        reportViewer.ServerReport.SetParameters(parm);
//        bytes = reportViewer.ServerReport.Render(reportFormat, deviceInfo, out mimeType,
//                         out encoding, out extension, out streamids, out warnings);
//        Response.Clear();
//        if(reportFormat == PDF_FORMAT) {
//            Response.ContentType = CONTENT_TYPE_PDF;
//            Response.AddHeader(CONTENT_DISPOSITION, "filename=" + report_file_name_pdf);
//        }
//        else if(reportFormat == EXCEL_FORMAT) {
//            Response.ContentType = CONTENT_TYPE_XLS;
//            Response.AddHeader(CONTENT_DISPOSITION, "filename=" + report_file_name_xls);
//        }
//        Response.OutputStream.Write(bytes, 0, bytes.Length);
//    }
//    catch(Exception ex) {
//        //Handle exception
//    }
//    finally {
//        Response.OutputStream.Flush();
//        Response.OutputStream.Close();
//        Response.Flush();
//        Response.Close();
//    }

//}

//ri = new ReportInfo
//{
//    DisplayName = "Assignment Of Mortgage (Contract)",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Assignment Of Mortgage (Contract)",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Client Advice (Contract)",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Client Advice (Contract)",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Escrow Disbursement Summary",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Escrow Disbursement Summary",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Escrow Disbursement Summary 108",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Escrow Disbursement Summary 108",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Escrow Disbursement Summary 116-118",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Escrow Disbursement Summary 116-118",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Escrow Disbursement Summary 121",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Escrow Disbursement Summary 121",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Owner Policy Inventory Register",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Owner Policy Inventory Register",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Owner Policy Remittance",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Owner Policy Remittance",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Partial Release Contract",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Partial Release Contract",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Recording Statement",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Recording Statement",
//};
//riList.Add(ri);

//ri = new ReportInfo
//{
//    DisplayName = "Recording Totals",
//    ReportNameLocation = @"/Tess Reports/Escrow Reports/Recording Totals",
//};
//riList.Add(ri); 
#endregion