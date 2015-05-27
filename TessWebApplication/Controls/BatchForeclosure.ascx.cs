#region Includes
using System;
using System.Linq;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
#endregion
namespace Greenspoon.Tess.Controls {
    public partial class BatchForeclosure : System.Web.UI.UserControl {
        void Page_Load(object sender, EventArgs e) {
            // If page loads for the first time.
            if(Page.IsPostBack == false) {
                // Load the drop down values.
                BindFormDropDowns();
                // If form has a valid ID.
                // Load the form in edit mode.
                // Otherwise keep form in new mode.
                if(BatchForeclosureID > 0) {
                    SetUpEditForm();
                }
                else {
                    // Set up new Batch Escrow entry form.
                    SetUpNewForm();
                    // if user is in new form.
                    // focus the search box.
                    txtSearchFCBatch.Focus();
                }
            }
        }
        #region Form Setup
        void SetUpNewForm() {
            lblCreatedBy.Text  = UserName;
            lblCreateTime.Text = DateTime.Now.ToDateOnly();
        }
        void SetUpEditForm() {
            // Get the user interface for batch foreclosure
            var ui = batch_foreclosure.GetBatchForeclosureDTO(BatchForeclosureID);
            // if there is a valid user interface. Bind to the UI.
            if(ui != null) {
                drpProject.SelectedValue
                                      = ui.ProjectId;
                // Bind the project related dropdowns
                // this needs to be completed before
                // binding the dropdowns.
                BindProjectDependentDropDowns();
                drpPhase.SelectedValue
                                      = ui.PhaseId;
                drpForeclosureType.SelectedValue 
                                      = ui.ForeclosureTypeId;
                txtBatchKey.Text      = ui.BatchKey;
                chkFKA.Checked        = ui.FKA;
                drpStatus.SelectedValue          
                                      = ui.StatusId;
                drpJudge.SelectedValue
                                      = ui.JudgeId;
                txtFileDate.Text      = ui.FileDate;
                txtCaseNum.Text       = ui.CaseNumber;
                chkLLC.Checked        = ui.LLC;
                txtProcessed.Text     = ui.ProecessedDate;
                txtReturned.Text      = ui.ReturnDate;
                txtHOAFile.Text       = ui.HOAFileDate;
                lblBatchId.Text       = ui.BatchForeclosureId;
                lblCreatedBy.Text     = ui.CreatedBy;
                lblCreateTime.Text    = ui.CreateDate;
            }
        }
        #region Bind DropDowns
        void BindFormDropDowns() {
            // Set up Project dropdown.
            BindProjectDropDown();
            // Set up foreclosure dropdown.
            BindForeclosureTypeDropDown();
            // Set up Status dropdown.
            BindStatusDropDown();
            
        }
        void BindProjectDropDown() {
            var list = project.GetProjectList();
            drpProject.DataSource = list;
            drpProject.DataBind();
        }
        void BindForeclosureTypeDropDown() {
            var list = TessHelper.GetForeclosureTypeList();
            drpForeclosureType.DataSource = list;
            drpForeclosureType.DataBind();
        }
        void BindStatusDropDown() {
            var list = 
                status_master.GetStatusMasterList(FormNameEnum.Foreclosure);
                drpStatus.DataSource = list;
                drpStatus.DataBind();
        }
        /// <summary>
        /// Gets the list of phases
        /// based on project selected by user.
        /// </summary>
        void BindPhaseDropDown() {
            if(drpProject.SelectedIndex > 0) {
                var data = phase_detail.GetPhaseListByProjectId(Convert.ToInt32(drpProject.SelectedValue));
                if(data.Any()) {
                    drpPhase.DataSource = data;
                    drpPhase.Enabled = true;
                    drpPhase.DataBind();
                }
                else {
                    drpPhase.ClearSelection();
                    drpPhase.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Binds judge drop downs.
        /// based on project select.
        /// </summary>
        void BindJudgeDropDown() {
            if(drpProject.SelectedIndex > 0) {
                var list = 
                    TessHelper.GetJudgeListByProjectId(Convert.ToInt32(drpProject.SelectedValue));
                if(list.Any()) {
                    drpJudge.DataSource = list;
                    drpJudge.DataBind();
                    drpJudge.Enabled = true;
                }
                else {
                    drpJudge.ClearSelection();
                    drpJudge.Enabled = false;
                }
            }
        }
        #endregion
        #endregion
        #region Form Events
        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e) {
            drpPhase.Items.Clear();
            drpPhase.Enabled = false;
            // If user selects a valid project.
            // load phase specific to the project.
            BindProjectDependentDropDowns();
        }
        void BindProjectDependentDropDowns() {
            if(drpProject.SelectedIndex > 0) {
                BindPhaseDropDown();
                BindJudgeDropDown();
            }
        }
        protected void btnSearchFC_Click(object sender, EventArgs e) {
            if (!Page.IsValid) return;
            var id = int.Parse(txtSearchFCBatch.Text);
            if(batch_foreclosure.isValid(id)) {
                RedirectForEdit(txtSearchFCBatch.Text);
            }
            else {
                RedirectForNew();
            }
        }
        protected void btnSaveBFC_Click(object sender, EventArgs e) {
            if (!Page.IsValid) return;
            // Save Batch foreclosure
            var newBatchId = Save();
            // If save fails, return code is -1.
            const int errorCode = -1;
            // the id is returned is not -1: means there was an error
            if(newBatchId != errorCode) {
                // check to see if the new id is same as existing id
                // if both ids are same that : it has done an edit.
                // else it's a new entry
                if(BatchForeclosureID == newBatchId) {
                    lblMsg.Text = "Saved";
                }
                else {
                    RedirectForEdit(newBatchId.ToString());
                }
            }
            else {
                lblMsg.Text = "Failed";
            }
        }
        /// <summary>
        /// Save Method works for both Update and Insert.
        /// </summary>
        /// <returns>
        /// On Success: Batch Foreclosure ID 
        /// On Error: -1
        /// </returns>
        int Save() {              
            // Build batch foreclosure ui object.
            var ui = 
                batch_foreclosure.GetBatchForeclosureDTO();
            ui.BatchForeclosureId = BatchForeclosureID.ToString();
            ui.ProjectId          = drpProject.SelectedValue;
            ui.PhaseId            = drpPhase.SelectedValue;
            ui.ForeclosureTypeId  = drpForeclosureType.SelectedValue;
            ui.BatchKey           = txtBatchKey.Text;
            ui.FKA                = chkFKA.Checked;
            ui.StatusId           = drpStatus.SelectedValue;
            ui.JudgeId            = drpJudge.SelectedValue;
            ui.FileDate           = txtFileDate.Text;
            ui.CaseNumber         = txtCaseNum.Text;
            ui.LLC                = chkLLC.Checked;
            ui.ProecessedDate     = txtProcessed.Text;
            ui.ReturnDate         = txtReturned.Text;
            ui.HOAFileDate        = txtHOAFile.Text;
            // This is to safe guard from Edit and new form created by.
            ui.CreatedBy          = lblCreatedBy.Text;
            // Returns int value.
            int batchForeclosureId = batch_foreclosure.Save(ui);
            return batchForeclosureId;
        }
        #endregion
        #region Utils
        // Redirect to foreclosure page in edit mode.
        void RedirectForEdit(string bathForeclosureId) {
            Response.Redirect(string.Format("~/Pages/foreclosure.aspx?a=e&bfid={0}", bathForeclosureId));
        }
        void RedirectForNew() {
            Response.Redirect("~/Pages/foreclosure.aspx");
        }
        #endregion
        #region Properties
        public int  BatchForeclosureID {
            get {
                return ViewState["BatchForeclosureID"] == null ? 0
                    : int.Parse(ViewState["BatchForeclosureID"].ToString());
            }
            set {
                ViewState["BatchForeclosureID"] = value;
            }
        }
        public string UserName {
            get {
                return ViewState["UserName"] == null ? String.Empty
                    : ViewState["UserName"].ToString();
            }
            set {
                ViewState["UserName"] = value;
            }
        }

        public string StatusId
        {
            get
            {
                return ViewState["StatusId"] == null ? String.Empty
                    : ViewState["StatusId"].ToString();
            }
            set
            {
                ViewState["StatusId"] = value;
            }
        }
        #endregion
    } // end of class.
}    // end of namespace