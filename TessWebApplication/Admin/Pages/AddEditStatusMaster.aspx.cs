#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditStatusMaster : PageBase
    {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                BindStatusGroupDropDown();

                if(PageIsEditMode && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    lblCreateBy.Text   = UserName;
                    lblCreateDate.Text = DateTime.Now.ToDateOnly();
                }
            }
        }

        private void SetupEditForm() {
            var s = status_master.GetStatusMaster(RecID);
            if (s == null) return;
            drpStatusGroup.SelectedValue    = s.status_group_id.ToString();
            txtStatusMasterName.Text        = s.status_master_name;
            chkActive.Checked               = s.status_master_active;
            chkCommentReg.Checked           = s.is_comment;
            chkDeletedReq.Checked           = s.is_deleted_allowed;
            //chkInvoiceReg.Checked         = s.req_invoice;                           RIQ-289 CVJan2013
            //chkDateStampReq.Checked       = s.req_datestamp;
            //chkEffDateReg.Checked         = s.req_eff_date ?? false;
            //chkRecordDateReq.Checked      = s.req_rec_date;
            //chkBookReq.Checked            = s.req_book;
            //chkPageReq.Checked            = s.req_page;
            //chkBatchReq.Checked           = s.req_batch;
            //chkCountyNameReq.Checked      = s.req_county_name;
            //chkAssignmentNumReq.Checked   = s.req_assign_num;
            //chkOrgCountyNameReq.Checked   = s.req_original_county;
            //txtNext.Text                  = s.next.ToString();                         RIQ-308
            //txtInterval.Text              = s.interval.ToString();                     RIQ-308
            chkCancelEscrow.Checked         = s.is_cancel_escrow;
            chkLegalName.Checked            = s.is_legal_name_required;
            txtComment.Text                 = s.comments;
            lblCreateBy.Text                = s.createdby;
            lblCreateDate.Text              = s.createddate.ToDateOnly();
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save() {
            var s = new status_master
            {
                status_master_id         = RecID,
                status_group_id          = Convert.ToInt32(drpStatusGroup.SelectedValue),
                status_master_name	     = txtStatusMasterName.Text,
                status_master_active	 = chkActive.Checked,
                is_comment               = chkCommentReg.Checked,
                is_deleted_allowed       = chkDeletedReq.Checked,
                //req_invoice	         = chkInvoiceReg.Checked,         RIQ-289 CVJan2013
                //req_datestamp	         = chkDateStampReq.Checked,
                //req_eff_date 	         = chkEffDateReg.Checked,
                //req_rec_date           = chkRecordDateReq.Checked,
                //req_book	             = chkBookReq.Checked,
                //req_page	             = chkPageReq.Checked,
                //req_batch	             = chkBatchReq.Checked,
                //req_county_name	     = chkCountyNameReq.Checked,
                //req_assign_num	     = chkAssignmentNumReq.Checked,
                //req_original_county	 = chkOrgCountyNameReq.Checked,
                //next	                 = txtNext.Text.NullIfEmpty<int?>(),                   RIQ-308
                //interval	             = txtInterval.Text.NullIfEmpty<int?>(),               RIQ-308
                is_cancel_escrow         = chkCancelEscrow.Checked,                            // RIQ-308
                is_legal_name_required   = chkLegalName.Checked,
                comments                 = txtComment.Text,                                     // RIQ-308
                createdby	             = lblCreateBy.Text,
            };
            var result = status_master.Save(s);
            return result;
        }

        void BindStatusGroupDropDown() {
            var data = status_group.GetStatusGroupList();
            drpStatusGroup.DataSource = data;
            drpStatusGroup.DataBind();
        }
        #region Util
        void RegisterThickBoxCloseScript() {
            Page.ClientScript.RegisterStartupScript(
                GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
        }
      
        void SetPageBase() {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if(PageMode == PageModeEnum.Edit) {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    } 
}