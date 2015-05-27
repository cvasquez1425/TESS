#region Includes
using System;
using System.Drawing;
using System.Web;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Greenspoon.Tess.Controls;

#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class BatchCancel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucBatchCancel.sendValue+=new cancelTypeID(ucBatchCancel_sendValue);
            SetPageVariables();
            btnBSUpld.Attributes.Add("onClick", "return blkScanConfirm();");
            // RIQ-309 Cancel Form Improvement - Logic to return to Legal Name and Inventory web form page.
            if (Page.IsPostBack)
            {
                string URL = (string)(Session["URL"]);
                if (!String.IsNullOrEmpty(URL))
                {
                    Response.Redirect(URL);
                }
            }   // RIQ-309
            if (Page.IsPostBack != false) return;
            if (PageIsInvalid()) { RedirectToNew(); }
            SetUserControlProperties();
            if (PageIsEditMode) { SetUpEditForm(); return; }
            SetUpNewForm();
        }

        // INVOICE-AFFIDAVIT OF REVERTER
        protected void ucBatchCancel_sendValue(string value)
        {
            string cancelTypeId = value;
            if (cancelTypeId == "INVOICE-AFFIDAVIT OF REVERTER") 
            {
                lblCancelType.Visible = true;
                txtReverter.Visible = true; 
            } 
            else 
            {
                lblCancelType.Visible = false;
                txtReverter.Visible = false; 
            } 
        }

        void SetUserControlProperties()
        {
            ucBatchCancel.UserName      =   UserName;
            ucBatchCancel.BatchCancelID =   BatchCancelId;
        }

        void SetUpNewForm()
        {
            divCancel.Visible = false;
        }

        void SetUpEditForm()
        {
            if (BatchCancelId <= 0) return;
            divCancel.Visible = true;
            LoadCancelForm();
            btnAddContractId.HRef = GetAddToCancelModalLink();
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            LoadContractNavigation();
            if (PageMode != PageModeEnum.Edit) return;
            var conId = 0;
            var cancelId = 0;

            if (drpContract.Items.Count > 0) 
            {
                if (Session["cancelId"] != null && Session["cancelId"].ToString() != "")
                {
                    if ((int)Session["cancelId"] != BatchCancelId)
                    {
                        Session["contractid"] = string.Empty;
                        Session["selectedindex"] = string.Empty;
                        ContractId = 0;
                    }
                }
                if (Session["contractid"] != null && Session["contractid"].ToString() != "")
                {
                    drpContract.SelectedIndex = (int)Session["selectedindex"];
                    ContractId = (int)Session["contractid"];
                    drpContract.SelectedItem.Text = ContractId.ToString();
                    string lblNumPage = Session["lblnumPage"].ToString();
                    SetUpEditForm();
                }
                conId = ContractId == 0 ? Convert.ToInt32(drpContract.SelectedItem.Text) : ContractId;
                cancelId        =   Convert.ToInt32(drpContract.SelectedValue);
            }

            var usrConInv = (Controls.Inventory)LoadControl("~/Controls/Inventory.ascx");
            if (usrConInv != null) {
                usrConInv.ContractID = conId;
                usrConInv.FormName = FormName;
                usrConInv.RecID    = BatchCancelId;
                plcInventory.Controls.Add(usrConInv);
            }
            
            var usrConStatus = 
                (Controls.Status)LoadControl("~/Controls/Status.ascx");
            if (usrConStatus != null) {
                usrConStatus.ContractID = conId;
                usrConStatus.FormName   = FormName;
                usrConStatus.RecID      = BatchCancelId;
                plcStatus.Controls.Add(usrConStatus);
            }
            
            var usrConLegalNames = (Controls.LegalNames)LoadControl("~/Controls/LegalNames.ascx");
            if (usrConLegalNames != null) {
                usrConLegalNames.ContractID = conId;
                usrConLegalNames.FormName   = FormName; 
                usrConLegalNames.ShowActiveOnly = chkShowAllLN.Checked ? false : true;  //RIQ-279  cv10192012
                plcLegalNames.Controls.Add(usrConLegalNames);
            }
          
            var usrCancelExtra = (Controls.CancelExtra)LoadControl("~/Controls/CancelExtra.ascx");
            if (usrCancelExtra != null) {
                usrCancelExtra.CancelID = cancelId;
                usrCancelExtra.BatchCancelID = BatchCancelId;                         // Update Current Value May 2015
                plcCancelExtra.Controls.Add(usrCancelExtra);
            }

        }

        protected void btnLoadContracts_Click(object sender, EventArgs e)
        {
            LoadCancelForm();
            //  Pages/BatchCancel.aspx?a=e&bcid=90744
            Response.Redirect(string.Format("~/Pages/BatchCancel.aspx?a={0}&bcid={1}", 'e', BatchCancelId));
        }

        void LoadCancelForm()
        {
            BindContractDropDown();
            BindForm();
        }

        void BindContractDropDown()
        {
            var contractList = cancel.GetCancelContractList(BatchCancelId);
            if (contractList.Count > 0) {
                drpContract.DataSource = contractList;
                drpContract.DataBind();
                drpContract.Enabled    = true;
                lnkSave.Enabled        = true;
            }
            else { drpContract.Enabled = false; lnkSave.Enabled = false; }
        }

        protected void drpContract_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["cancelId"]     = BatchCancelId;
            Session["contractid"]   = ContractId;
            Session["selectedindex"] = drpContract.SelectedIndex;
            BindForm();
        }

        void BindForm()
        {
            if (drpContract.Items.Count <= 0) return;
            var cancelId = int.Parse(drpContract.SelectedValue);
            var ui = cancel.GetCancelUI(cancelId);
            if (ui == null) return;
            txtDeath.Text           = ui.Death;
            txtCMA.Text             = ui.CMA;
            txtNonTax.Text          = ui.NonTax;
            txtPurchasePrice.Text   = ui.PurchasePrice;
// RIQ-309 Cancel Form Improvements
            txtMortBook.Text        = ui.MortgageBook;
            txtMortPage.Text        = ui.MortgagePage;
            txtMortDate.Text        = ui.MortgageDate;
            txtDeedBook.Text        = ui.DeedBook;
            txtDeedDate.Text        = ui.DeedDate;
            txtDeedPage.Text        = ui.DeedPage;
// ---->
            lblVesting.Text         = WrappableText(ui.Vesting);
            lblPoints.Text          = ui.Points;
            lblPointsGroup.Text     = ui.PointsGroup;
            chkActive.Checked       = ui.Active;
            txtNonComply.Text       = ui.NonComply;
            txtReverter.Text        = ui.AffidavitOfRev.ToString();
            lblDevK.Text            = ui.DevK;

            SetFocus(drpContract);
            activeBG.Attributes["class"] = !ui.Active ? "redBackground" : string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid != true) return;
            Save();
        }

        protected void lnkGotoCancelSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Pages/CancelSearch.aspx?bcid={0}&pid={1}", BatchCancelId, ucBatchCancel.ProjectID));
        }

        void Save()
        {
            var ui = new CancelDTO {
                Death         = txtDeath.Text,
                CMA           = txtCMA.Text,
                NonTax        = txtNonTax.Text,
                PurchasePrice = txtPurchasePrice.Text,
                // Cancel Id is saved on behind the dropdown selected value.
                CancelId      = drpContract.SelectedValue,
                Active        = chkActive.Checked,
                NonComply     = txtNonComply.Text,
                AffidavitOfRev= int.Parse(txtReverter.Text),
// RIQ-309 Cancel Form Improvements
                MortgageBook  = txtMortBook.Text,
                MortgagePage  = txtMortPage.Text,     
                MortgageDate  = txtMortDate.Text,   
                DeedBook      = txtDeedBook.Text,       
                DeedPage      = txtDeedPage.Text,  
                DeedDate      = txtDeedDate.Text,
            };
            lblMsg.Text = cancel.Save(ui) == true ? "Saved" : "Failed";
        }
        // Moves to First Record
        protected void LnkFirstClick(object sender, EventArgs e)
        {
            if (drpContract.Items.Count < 0) return;
            drpContract.SelectedIndex = 0;
//            ContractId = int.Parse(drpContract.SelectedValue);
            ContractId = int.Parse(drpContract.SelectedItem.Text);
            Session["cancelId"] = BatchCancelId;
            Session["contractid"] = ContractId;
            Session["selectedindex"] = drpContract.SelectedIndex;
            SetUpEditForm();
        }
        // Last
        protected void LnkLastClick(object sender, EventArgs e)
        {
            if (drpContract.Items.Count < 0) return;
            drpContract.SelectedIndex = drpContract.Items.Count - 1;
//            ContractId = int.Parse(drpContract.SelectedValue);
            ContractId = int.Parse(drpContract.SelectedItem.Text);
            Session["cancelId"] = BatchCancelId;
            Session["contractid"] = ContractId;
            Session["selectedindex"] = drpContract.SelectedIndex;
            SetUpEditForm();
        }
        // Moves to the next master id
        protected void LnkNextClick(object sender, EventArgs e)
        {
            if (drpContract.SelectedIndex >= ( drpContract.Items.Count - 1 )) return;
            drpContract.SelectedIndex++;
//            ContractId = int.Parse(drpContract.SelectedValue);
            ContractId = int.Parse(drpContract.SelectedItem.Text);
            Session["cancelId"] = BatchCancelId;
            Session["contractid"] = ContractId;
            Session["selectedindex"] = drpContract.SelectedIndex;
            SetUpEditForm();
        }
        // Moves to the previous master id.
        protected void LnkPreviousClick(object sender, EventArgs e)
        {
            if (drpContract.SelectedIndex <= 0) return;
            drpContract.SelectedIndex--;
//            ContractId = int.Parse(drpContract.SelectedValue);
            ContractId = int.Parse(drpContract.SelectedItem.Text);
            Session["cancelId"] = BatchCancelId;
            Session["contractid"] = ContractId;
            Session["selectedindex"] = drpContract.SelectedIndex;
            SetUpEditForm();
        }

        protected void btnBSUpld_Click(object sender, EventArgs e)
        {
            if (DbService.BatchCancelBulkScan(ucBatchCancel.BatchCancelID, UserName)) {
                lblbsMsg.Text = "Action Successful";
                lblbsMsg.ForeColor = Color.Green;
            }
            else {
                lblbsMsg.Text = "Error: Action Failed";
                lblbsMsg.ForeColor = Color.Red;
            }
        }

        //This is the function to make the text wrappable: 
        public static string WrappableText(string source)
        {
            string nwln = Environment.NewLine;
            return "<p>" +
            source.Replace(nwln + nwln, "</p><p>")
            .Replace(nwln, "<br />") + "</p>";
        } 

        #region Util
        void SetPageVariables()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            FormName = FormNameEnum.Cancel;
            if (PageMode == PageModeEnum.Edit) {
                BatchCancelId = Request.QueryString.GetValue<int>("bcid");   
                ContractId =
                    drpContract.Items.Count > 0 ? int.Parse(drpContract.SelectedItem.Text)
                    : ( Page.IsPostBack == false ) ? Request.QueryString.GetValue<int>("cid")
                    : 0;
            }
            ToggleDocPanel(false);
        }

        bool PageIsInvalid()
        {
            return PageIsEditMode 
                   && ( BatchCancelId > 0 )
                   && ( batch_cancel.IsValid(BatchCancelId) == false );
        }

        void LoadContractNavigation()
        {
            if (drpContract.Items.Count > 0)
            {
                if (Session["selectedindex"] != null && Session["selectedindex"].ToString() != "")
                {
                    int selectedId = (int)Session["selectedindex"];
                    drpContract.SelectedIndex = selectedId;
                }
            }
            // Load Navigation button
            if (drpContract.Items.Count < 0) return;

            //RIQ-301
            if (drpContract.SelectedIndex == 0) { LnkFirst.Enabled = false; } else { LnkFirst.Enabled = true; }
            if (drpContract.SelectedIndex == drpContract.Items.Count - 1) { LnkLast.Enabled = false; } else { LnkLast.Enabled = true; }

            if (drpContract.SelectedIndex < drpContract.Items.Count - 1)
            {
                lnkNext.Enabled = true;
            }
            if (drpContract.SelectedIndex > 0)
            {
                lnkPrevious.Enabled = true;
            }
            lblNumPage.Text = string.Format("{0} / {1}", drpContract.SelectedIndex + 1, drpContract.Items.Count);
            Session["lblnumPage"] = lblNumPage.Text;
        }

        void RedirectToNew()
        {
            Response.Redirect("~/Pages/BatchCancel.aspx");
        }

        string GetAddToCancelModalLink()
        {
            return string.Format("~/Pages/AddContractToCancel.aspx?id={0}&TB_iframe=true&height=340&width=500", BatchCancelId);
        }

        protected void BtnShowImageClick(object sender, EventArgs e)
        {
            if (ContractId == 0) { return; }
            ToggleDocPanel(true);
            var projectId = ucBatchCancel.ProjectID.ToString();
            gvScannedImages.DataSource = DbService.GetBulkScanImages(projectId, ContractId.ToString());
            gvScannedImages.DataBind();
        }

        protected string GetDocHandlerUrl(string docType, string docLoacation)
        {
            if (string.IsNullOrEmpty(docType) || string.IsNullOrEmpty(docLoacation)) { return string.Empty; }
            return string.Format("../Services/DocHandler.ashx?doctype={0}&doc={1}", docType, HttpUtility.UrlEncode(docLoacation));
        }

        void ToggleDocPanel(bool showDoc)
        {
            btnShowImage.Visible = !showDoc;
            gvScannedImages.Visible = showDoc;
        }
        #endregion
    }
}