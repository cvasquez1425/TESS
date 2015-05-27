using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using System.Web.UI.WebControls;

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class CancelStatusUpdate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdate.Attributes.Add("onClick", "return blkScanConfirm();");   // Added 11-05-2014 Page Validation Script         
            SetPageVariables();
            if (Page.IsPostBack) 
            {
                bool IsLvProject = GetIsLvProject(Convert.ToInt32(txtBatchCancelKey.Text));
                chkIsVegasProject.Value = IsLvProject.ToString();

                bool IsCounty16 = GetCountyId(Convert.ToInt32(txtBatchCancelKey.Text));
                if (IsCounty16) { txtProjectId.Value = "16"; } else { txtProjectId.Value = string.Empty; }

                //int batchCancelid = GetProjectId(Convert.ToInt32(txtBatchCancelKey.Text));
                //txtProjectId.Value = batchCancelid.ToString();
                return; 
            }
            BindCountyDropwDown();
            BindStatusMasterDropDown();
            if (BatchCancelId <= 0) return;
            txtBatchCancelKey.Text = BatchCancelId.ToString();
            BindGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try {
                var ui = BindUIData();
                status.Save(ui);
                lblMsg.Text = "Status Inserted.";
            } catch {
                lblMsg.Text = "ERROR: Could not create Status.";
            }
        }

        IList<StatusDTO> BindUIData()
        {
            var ui = new List<StatusDTO>();
            foreach (GridViewRow row in gvRecInfo.Rows) {
                var contractId = ( row.FindControl("lblMasterId") as Label ).Text;
                var page = ( row.FindControl("txtPage") as TextBox ).Text;
               // var legalNameId = ( row.FindControl("lnPId") as HiddenField ).Value;
                var book = (row.FindControl("txtBookGrid") as TextBox).Text;
                var comment = (row.FindControl("txtCommentGrid") as TextBox).Text;
                var recdate = (row.FindControl("txtDocRecorded") as TextBox).Text;    // change request 27504, 26530

                ui.Add(new StatusDTO {
                    StatusMasterId   = drpStatusMaster.SelectedValue,
                    Invoice          = txtInvoice.Text,
                    EffectiveDate    = txtEffDate.Text,
//                    RecDate          = txtDocRec.Text,         // change request 27504, 26530
                    RecDate          = recdate,                  // change request 27504, 26530
                    CountyId         = drpCounty.SelectedValue,
                    OriginalCountyId = drpOriginalCounty.SelectedValue,
//                    Comment          = txtComments.Text,      change request October 2014
                    Comment          =  comment,                
//                    Book             = txtBook.Text,          change request October 2014
                    Book             = book,
                    AssignmentNumber = txtAssignNum.Text,
                    ContractId       = contractId,
                    Page             = page,
                    //LegalNameId      = legalNameId,
                    Active           = true,
                    Batch            = txtBatchCancelKey.Text,
                    CreatedBy        = UserName,
                    UploadeBatchId   = GetUploadBatchId().ToString(),
                });
            }
            return ui;
        }

        void Clear()
        {
            var empty = string.Empty;
            drpStatusMaster.ClearSelection();
            drpCounty.ClearSelection();
            drpOriginalCounty.ClearSelection();
            txtInvoice.Text   = empty;
            txtEffDate.Text   = empty;
            txtDocRec.Text    = empty;
            txtComments.Text  = empty;
            txtBook.Text      = empty;
            txtAssignNum.Text = empty;
//            txtProjectId.Text = empty;
        }

        protected void btnSearchBC_Click(object sender, EventArgs e)
        {
            Clear();
            BindGrid();
        }

        private void BindGrid()
        {
            var data = status.GetCancelStatusDTO(Convert.ToInt32(txtBatchCancelKey.Text));
            gvRecInfo.DataSource = data;
            gvRecInfo.DataBind();
            if (data.Any() == false) { return; }
            btnUpdate.Enabled = data.Any();
            btnUpdate.TabIndex = (short)data.Count();
        }

        void BindCountyDropwDown()
        {
            var list = county.GetCountyList();
            drpCounty.DataSource = list;
            drpCounty.DataBind();
            drpOriginalCounty.DataSource = list;
            drpOriginalCounty.DataBind();
        }

        void BindStatusMasterDropDown()
        {
            var list = status_master.GetStatusMasterList();
            drpStatusMaster.DataSource = list;
            drpStatusMaster.DataBind();
        }

        protected string GetDisplay(string[] arg)
        {
            if (arg.Length == 0) { return string.Empty; }
            if (arg.Length == 1) { return arg.First(); }

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} &nbsp;", arg.First()));
            sb.Append(@"<div style=""float:right; text-align: left;"" class=""arrow-down""></div>");
            sb.Append(@"<span>");
            foreach (var s in arg.Skip(1)) {
                sb.Append(string.Format("{0}<br/>", s));
            }
            sb.Append(@"</span>");
            return sb.ToString();
        }

        // Add the upload_batch_id to the Bulk Cancel Update 10/20/2014
        protected int GetUploadBatchId()
        {
            int UploadBatchID = 0;
            using (var ctx = DataContextFactory.CreateContext())
            {
                var data = ctx.status.Max(a => a.upload_batch_id);
                return UploadBatchID = data.Value + 1;
            };
        }

        // for Project 129, 130, and 82 Page field in the gvRecInfo disabled
        protected int GetProjectId(int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var data = ctx.batch_cancel.Where(p => p.batch_cancel_id == batchCancelId)
                           .Select(p =>
                                    p.project_id
                                   ).SingleOrDefault();
                return data;
            }
        }

        // for Project 129, 130, and 82 Page field in the gvRecInfo disabled using county_id = 16 for 82, 129, 130, they all utilize the same county.
        internal bool GetCountyId(int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var queryProjects = (
                    from b in ctx.batch_cancel
                    join p in ctx.projects on b.project_id equals p.project_id
                    where b.batch_cancel_id == batchCancelId && p.county_id == 16
                    select new
                    {
                        IsCounty16 = true
                    }
                                ).ToList();
                bool list = queryProjects.Select(item => item.IsCounty16).SingleOrDefault();
                return list;                                                         
            }
        }

        // is_lv_project (66, 114, 116, 118 ) true or false
        internal bool GetIsLvProject(int batchCancelId)
        {
            using (var ctx = DataContextFactory.CreateContext())
            {
                var queryProjects = (
                    from b in ctx.batch_cancel
                    join p in ctx.projects on b.project_id equals p.project_id
                    where b.batch_cancel_id == batchCancelId && p.is_lv_project == true
                    select new
                    {
                        IsVegasProject = true
                    }
                                ).ToList();
                bool list = queryProjects.Select(item => item.IsVegasProject).SingleOrDefault();
                return list;
            }
        }

        void SetPageVariables()
        {
            BatchCancelId = Request.QueryString.GetValue<int>("bcid");
        }
    }
}