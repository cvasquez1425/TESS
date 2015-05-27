#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Pages
{
    public partial class TrsExpandedView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack == false)
            {
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

        protected void Reload_Page(object sender, EventArgs e)
        {
            SetupPage();
        }

        #region Page Setup
        void BindStatusList()
        {
            // Form name represents which form the user is coming from.
            var ui =
                trs.GetResaleForeclosureUIList(FormName, ContractId);
            lvData.DataSource = ui;
            lvData.DataBind();
        }

        void SetupPage()
        {
            BindStatusList();

            statusLbl.Text = ContractId.ToString();  // RIQ-255 Need Master ID
            statusLblBottom.Text = ContractId.ToString();  // RIQ-302 Status Form Update Phase II

            if (Page.IsPostBack == false)
            {
                btnReturn.HRef = GetReturnPath();
                btnReturnB.HRef = GetReturnPath();
            }
        }
        #endregion

        #region Util
        string GetReturnPath()
        {
            string returnPath;
            string previousPath = Request.UrlReferrer.ToString();
            const string defaultPath = "~/Default.aspx";
            switch (FormName)
            {
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

        void SetPageBase()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            FormName = Request.QueryString.GetValue<FormNameEnum>("form");
            // Due to RIQ-303 RecID was added            
            EscrowId = Request.QueryString.GetValue<int>("id");
            if (PageMode != PageModeEnum.View) return;
            ContractId = Request.QueryString.GetValue<int>("cid");
            RecID = Request.QueryString.GetValue<int>("id");
        }
        #endregion

    }
}