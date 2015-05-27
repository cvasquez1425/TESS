#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.BusinessObjects.UIObjects;
#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class Status : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack == false) {
                BindAllDropDown();
                if (PageIsEditMode && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    lblCreateBy.Text   = UserName;
                    lblCreateDate.Text = DateTime.Now.ToDateOnly();
                    chkActive.Checked = true;
                }
            }
            if (Page.IsPostBack) 
            { 
                int statusMasterID = int.Parse(drpStatusMaster.SelectedValue);
                chkIsComment.Checked = status.GetStatusMasterID(statusMasterID);
                drpStatusMaster.Focus();
            }
        } 

        void SetupEditForm()
        {
            var ui = status.GetStatusDTO(RecID);
            if (ui == null) return;
            bool isComment = status.GetStatusIsComment(RecID);            //RIQ-289 CVJan2013
            if (isComment) { chkIsComment.Checked = true; } else { chkIsComment.Checked = false; };
//            bool isDeleted = status.GetStatusIsComment(RecID);            //RIQ-289 CVJan2013
            bool isDeleted = status.GetStatusIsDeleted(RecID);             //RIQ-302 Status Form Update Phase II
            if (isDeleted) { chkActive.Enabled = false; } else { chkActive.Enabled = true; };
            drpStatusMaster.SelectedValue   = ui.StatusMasterId;
            drpCounty.SelectedValue         = ui.CountyId;
            drpOriginalCounty.SelectedValue = ui.OriginalCountyId;
            drpLegalName.SelectedValue      = ui.LegalNameId;
            txtInvoice.Text                 = ui.Invoice;
            txtRecordDate.Text              = ui.RecDate;
            txtBook.Text                    = ui.Book;
            txtPage.Text                    = ui.Page;
            txtAssignmentNumber.Text        = ui.AssignmentNumber;
            txtEffectiveDate.Text           = ui.EffectiveDate;
            txtComment.Text                 = ui.Comment;
            txtBatch.Text                   = ui.Batch;
            txtUploadBatchId.Text           = ui.UploadeBatchId;
            chkActive.Checked               = ui.Active;
            lblCreateBy.Text                = ui.CreatedBy;
            lblCreateDate.Text              = ui.CreatedDate;
        } 

        #region Save
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid == true) {
                if (Save() == true) {
                    Session["URL"] = "";
                    if (status.statusMasterLegalname(Convert.ToInt32(drpStatusMaster.SelectedValue)) && chkActive.Checked == true)
                    {
                        string NavigateUrl = string.Format("~/Pages/InventoryExpandedView.aspx?a=v&cid={0}&form={1}&id={2}&ba=s", ContractId, FormName, EscrowId);
                        Session["URL"] = NavigateUrl;
                    }
                    //RIQ-319 Open Legal Name screen status code 99, 631
                    if ((Convert.ToInt32(drpStatusMaster.SelectedValue) == 99 && PageMode == PageModeEnum.New) || Convert.ToInt32(drpStatusMaster.SelectedValue) == 631 && PageMode == PageModeEnum.New)
                    {
//                            Response.Redirect(string.Format("~/Pages/InventoryExpandedView.aspx?a=v&cid={0}&form={1}&id={2}&ba=s", ContractId, FormName, EscrowId));
                        string NavigateUrl = string.Format("~/Pages/InventoryExpandedView.aspx?a=v&cid={0}&form={1}&id={2}&ba=s", ContractId, FormName, EscrowId);
                        Session["URL"] = NavigateUrl;
                    }
                    RegisterThickBoxCloseScript();
                }
                else { lblMsg.Text = "Failed"; }
            }
        }

        bool Save()
        {
            var ui = new StatusDTO {
                StatusId         = RecID.ToString(),
                ContractId       = ContractId.ToString(),
                StatusMasterId   = drpStatusMaster.SelectedValue,
                CountyId         = drpCounty.SelectedValue,
                OriginalCountyId = drpOriginalCounty.SelectedValue,
                LegalNameId      = drpLegalName.SelectedValue,
                Invoice          = txtInvoice.Text,
                RecDate          = txtRecordDate.Text,
                Book             = txtBook.Text,
                Page             = txtPage.Text,
                AssignmentNumber = txtAssignmentNumber.Text,
                EffectiveDate    = txtEffectiveDate.Text,
                Comment          = txtComment.Text,
                Batch            = txtBatch.Text,
                UploadeBatchId   = txtUploadBatchId.Text,
                Active           = chkActive.Checked,
                CreatedBy        = lblCreateBy.Text,
                //Resale Forclosure Orlando Perfect Practice
                ModifyDate = DateTime.Today.ToString(),
                ModifyBy = UserName,
            };
            var result = status.Save(ui);
            return result;
        }
        #endregion
        #region Drop downs.
        void BindAllDropDown()
        {
            BindStatusMasterDropDown();
            BindCountyDropwDown();
            BindLegalNames();
        }
        void BindStatusMasterDropDown()
        {
            var list = 
                status_master.GetStatusMasterList(FormName);
            drpStatusMaster.DataSource = list;
            drpStatusMaster.DataBind();
        }
        void BindCountyDropwDown()
        {
            var list = county.GetCountyList();
        
            drpCounty.DataSource = list;
            drpCounty.DataBind();
         
            drpOriginalCounty.DataSource = list;
            drpOriginalCounty.DataBind();
        }
        void BindLegalNames()
        {
            drpLegalName.DataSource = 
                legal_name.GetLegalNameDropDownList(ContractId);
            drpLegalName.DataBind();
        }
        #endregion
        #region Util
        void SetPageBase()
        {
            PageMode    = Request.QueryString.GetValue<PageModeEnum>("a");
            FormName    = Request.QueryString.GetValue<FormNameEnum>("form");
            ContractId  = Request.QueryString.GetValue<int>("cid");
            if (PageMode == PageModeEnum.Edit) {
                RecID   = Request.QueryString.GetValue<int>("id");
            }
            if (PageMode == PageModeEnum.New)                       // Due to RIQ-303 RecID was added
            {
                EscrowId = Request.QueryString.GetValue<int>("id");
            }
        }

        void RegisterThickBoxCloseScript()
        {
            Page.ClientScript
                .RegisterStartupScript(GetType(), "closeThickBox", "self.parent.Status_Updated();", true);
        }
        #endregion

    } // end of class
}