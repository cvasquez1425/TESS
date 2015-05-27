#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class StatusExpandedView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(Page.IsPostBack == false) {
                SetupPage();
            }
            // Code 99 return to inventory legal name address.
            if (Page.IsPostBack)
            {
                string URL = (string)(Session["URL"]);
                if (!String.IsNullOrEmpty(URL))
                {
                    Response.Redirect(URL);
                }
            }
        }
        
        protected void Reload_Page(object sender, EventArgs e) {
            SetupPage();
        }

        #region Page Setup
        void BindStatusList() {
            // Form name represents which form the user is coming from.
            var ui = 
                status.GetStatusUIList(FormName, ContractId);
            lvData.DataSource = ui;
            lvData.DataBind();
        }
     
        void SetupPage() {
            BindStatusList();
            btnNewStatus.HRef    = 
                    string.Format("~/Pages/status.aspx?a=n&cid={0}&form={1}&TB_iframe=true&height=600&width=550",
                                    ContractId, FormName);
            btnNewStatus.Visible = true;
            btnNewStatus.Title = string.Format("Add Status to MasterID {0}", ContractId); // RIQ-255 Need Master ID (Contract_id) on top of Contract Status Form
            btnNewStatusB.HRef    = 
                    string.Format("~/Pages/status.aspx?a=n&cid={0}&form={1}&TB_iframe=true&height=600&width=550",
                                   ContractId, FormName);
            btnNewStatusB.Visible = true;
            btnNewStatusB.Title = string.Format("Add Status to MasterID {0}", ContractId); // RIQ-255 Need Master ID (Contract_id) on top of Contract Status Form

            // RIQ-302
            btnPrintBOReportTop.Visible = true;
            btnPrintBOReportBottom.Visible = true;

            statusLbl.Text       = ContractId.ToString();  // RIQ-255 Need Master ID
            statusLblBottom.Text = ContractId.ToString();  // RIQ-302 Status Form Update Phase II

            if(Page.IsPostBack == false) {
                btnReturn.HRef = GetReturnPath();
                btnReturnB.HRef = GetReturnPath();
            }
        }

        #endregion

        #region Print Status Report
        // RIQ-302  Status Summary Report - Business Object
        void printBOReport(int ContractId)
        {
            int UploadContractD = ContractId;

            // Business Object Status Report changed as of August 13, 2014, NEW HYPERLINK.
            // Miami New Hyperlinks November 2014 - Basically, the only thing you need to update is the address from gm-atl-biprod to gm-mdv-biprod
            string urlString = "http://gm-mdv-biprod:8080/TokenLogin/StatusSummary.jsp?id=MasterID&docId=AXthFrmy7WFFmaCeaASYZ38";
            var sb = new System.Text.StringBuilder();
            if (urlString.Contains("MasterID")) sb.Append(urlString).Replace("MasterID", UploadContractD.ToString());
            ResponseHelper.Redirect(sb.ToString(), "_blank", "menubar=0,width=1200,height=900");
        }
        #endregion

        #region Util

        string GetReturnPath() {
            string returnPath;
            string previousPath = Request.UrlReferrer.ToString();
            const string defaultPath = "~/Default.aspx";
            switch(FormName) {
                case FormNameEnum.BatchEscrow:
                    returnPath = RecID > 0 
                        ? string.Format("~/Pages/BatchEscrow.aspx?a=e&beid={0}&cid={1}", RecID, ContractId) 
                        : previousPath;
                    break;
                case FormNameEnum.Cancel:
                    returnPath = RecID > 0 
                        ? string.Format("~/Pages/BatchCancel.aspx?a=e&bcid={0}&cid={1}", RecID, ContractId) 
                        : previousPath;
                    break;
                case FormNameEnum.Foreclosure:
                    returnPath = RecID > 0 
                        ? string.Format("~/Pages/foreclosure.aspx?a=e&bfid={0}&cid={1}", RecID, ContractId) 
                        : previousPath;
                    break;
                default:
                    returnPath = defaultPath;
                    break;
            }
            return string.IsNullOrEmpty(returnPath) == false ? returnPath : defaultPath;
        }
     
     
        void SetPageBase() {
            PageMode         = Request.QueryString.GetValue<PageModeEnum>("a");
            FormName         = Request.QueryString.GetValue<FormNameEnum>("form");
            // Due to RIQ-303 RecID was added            
            EscrowId = Request.QueryString.GetValue<int>("id");
            if (PageMode != PageModeEnum.View) return;
            ContractId   = Request.QueryString.GetValue<int>("cid");
            RecID        = Request.QueryString.GetValue<int>("id");
        }

        #endregion

        protected void btnPrintBOReportTop_Click(object sender, EventArgs e)
        {
            printBOReport(ContractId);
        }

        protected void btnPrintBOReportBottom_Click(object sender, EventArgs e)
        {
            printBOReport(ContractId);
        }
    } // end of class
}