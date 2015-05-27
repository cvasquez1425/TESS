using System;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Services;

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class UpdateByEscrowKey : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                BindCountyDropwDown();
                BindStatusMasterDropDown();
                RecID       = Request.QueryString.GetValue<int>("bid");
                if (RecID > 0) {
                    txtBatchKey.Text = RecID.ToString();
                }
            }
            //RIQ-289 CVJan2013
            if (Page.IsPostBack)
            {
                int statusMasterID = int.Parse(drpStatusMaster.SelectedValue);
                chkIsComment.Checked = status.GetStatusMasterID(statusMasterID);
            }
        }

        void BindStatusMasterDropDown()
        {
            var list = 
                status_master.GetStatusMasterList();
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

        protected void btnSaveBatch_Click(object sender, EventArgs e)
        {
            if (StatusMasterIsInvalidShowError()) return; //RIQ-289 CVJan2013 
            if (BatchKeyIsInvalidShowError()) return;

            if (Save()) {
                ShowMsg("Status added successfully.", isError: false);
                return;
            }
            ShowMsg("Error: Could not update status.");
        }

        //RIQ-289 CVJan2013
        private bool StatusMasterIsInvalidShowError()
        {
            if (chkIsComment.Checked == true && txtComments.Text.Length <= 0)
            {
                ShowMsg("Comment is a required field for this STATUS MASTER CODE.");
                txtComments.Focus();
                return true;
            }
            return false;
        }

        private bool BatchKeyIsInvalidShowError()
        {
            if (!batch_escrow.IsValid(int.Parse(txtBatchKey.Text))) {
                ShowMsg("Invalid Batch Escrow Key.");
                txtBatchKey.Focus();
                return true;
            }
            return false;
        }

        bool Save()
        {
            var ui = new UpdateStatusByEscrowKeyDTO {
                StatusMasterId = drpStatusMaster.SelectedValue,
                Invoice        = txtInvoice.Text,
                EffectiveDate  = txtEffDate.Text,
                RecordDate     = txtDocRec.Text,
                BeatchEscrowId = txtBatchKey.Text,
                Book           = txtBook.Text,
                Page           = txtPage.Text,
                Assign         = txtAssignNum.Text,
                CountyId       = drpCounty.SelectedValue,
                OrigCountyId   = drpOriginalCounty.SelectedValue,
                Comments       = txtComments.Text,
                UserName       = UserName
            };
            return DbService.UpdateStatusByEscrowKey(ui);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/Admin/Pages/UpdateByEscrowKey.aspx");
        }

        void ShowMsg(string msg, bool isError = true)
        {
            lblBatchMsg.CssClass = isError ? "error" : "clear";
            lblBatchMsg.Text = msg;
        }
    }
}