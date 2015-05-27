#region Includes
using System;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
#endregion

namespace Greenspoon.Tess.Controls
{
    public delegate void cancelTypeID(string valueToPass);
    public partial class BatchCancel : System.Web.UI.UserControl
    {
        public event cancelTypeID sendValue;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                BindFormDropDowns();
                if (BatchCancelID > 0)
                {
                    SetUpEditForm();
                }
                else
                {
                    SetUpNewForm();
                    txtSearchCancelBatch.Focus();
                }
                if (sendValue != null) sendValue(drpCancelType.SelectedItem.Text);
                // EnableDisablePagesAndNamesFields();
            }
        }
        #region Form Setup
        void SetUpNewForm()
        {
            lblCreatedBy.Text   = UserName;
            lblCreatedDate.Text = DateTime.Now.ToDateOnly();
            sendValue(drpCancelType.SelectedItem.Text);
        }

        void SetUpEditForm()
        {
            var ui = batch_cancel.GetBatchCancelDTO(BatchCancelID);
            if (ui == null) return;
            drpProject.SelectedValue = ui.ProjectId;
            // RIQ-306 Extra Recording Field - Batch Cancel Web Form
            var extraRecording = batch_cancel.IsProjId108(Convert.ToInt32(drpProject.SelectedValue));
            if (extraRecording != true) {  txtExtraRecording.Enabled = false; } else { txtExtraRecording.Enabled = true; }

            drpProject.SelectedValue       = ui.ProjectId;
            drpParentProject.SelectedValue = ui.ParentProjectId;
            drpCancelStatus.SelectedValue  = ui.CancelStatusId;
            drpCancelType.SelectedValue    = ui.CancelTypeId;
            txtExtraNames.Text             = ui.ExtraNames;
            txtExtraPages.Text             = ui.ExtraPages;
            txtExtraRecording.Text         = ui.ExtraRecording;          // RIQ-306
            txtCancelNumber.Text           = ui.CancelNumber;
            lblBatchId.Text                = ui.BatchCancelId;
            lblCreatedBy.Text              = ui.CreatedBy;
            lblCreatedDate.Text            = ui.CreatedDate;
            
            EnableDisableParentProject();
            //  EnableDisablePagesAndNamesFields();
        }

        #endregion

        #region Drop Down Bind
        void BindFormDropDowns()
        {
            BindProjectDropDown();
            BindCanelTypeDropDown();
            BindStatusDropDown();
        }

        void BindStatusDropDown()
        {
            var list = 
                status_master.GetStatusMasterList(FormNameEnum.Cancel);
            drpCancelStatus.DataSource = list;
            drpCancelStatus.DataBind();
        }

        void BindCanelTypeDropDown()
        {
            var list = 
                cancel_type.GetCancelyTypeList();
            drpCancelType.DataSource = list;
            drpCancelType.DataBind();
        }

        void BindProjectDropDown()
        {
            var list = project.GetProjectList();
            drpProject.DataSource = list;
            drpProject.DataBind();
            drpParentProject.DataSource = list;
            drpParentProject.DataBind();
        }
        #endregion

        protected void drpCancelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCancelStatusValue();
            EnableDisableParentProject();
            // EnableDisablePagesAndNamesFields();
        }

        protected void btnSearchBC_Click(object sender, EventArgs e)
        {
            if (Page.IsValid != true) return;
            var id = Convert.ToInt32(txtSearchCancelBatch.Text);
            if (batch_cancel.IsValid(id) == true) {
                RedirectForEdit(id.ToString());
            }
            else {
                RedirectForNew();
            }
        } 

        #region Save
        protected void btnSaveBC_Click(object sender, EventArgs e)
        {
            var newBatchId = Save();
            const int errorCode = -1;
            if (newBatchId != errorCode) {
                if (BatchCancelID == newBatchId) {
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

        int Save()
        {
            var ui = 
            batch_cancel.CreateBatchCancelDTO();
            ui.BatchCancelId   = BatchCancelID.ToString();
            ui.ProjectId       = drpProject.SelectedValue;
            ui.ParentProjectId = drpParentProject.SelectedValue;
            ui.ExtraNames      = txtExtraNames.Text;
            ui.ExtraPages      = txtExtraPages.Text;
            ui.ExtraRecording  = txtExtraRecording.Text;             // Jira
            ui.CancelTypeId    = drpCancelType.SelectedValue;
            ui.CancelNumber    = txtCancelNumber.Text;
            ui.CancelStatusId  = drpCancelStatus.SelectedValue;
            ui.CreatedBy       = UserName;
            ui.CreatedDate     = DateTime.Now.ToString();

            int batchCancelId = batch_cancel.Save(ui);
            return batchCancelId;
        } 

        #endregion

        #region Properties
        public int BatchCancelID
        {
            get
            {
                return ViewState["BatchCancelID"] == null ? 0
                    : int.Parse(ViewState["BatchCancelID"].ToString());
            }
            set
            {
                ViewState["BatchCancelID"] = value;
            }
        }

        public string UserName
        {
            get
            {
                return ViewState["UserName"] == null ? String.Empty
                    : ViewState["UserName"].ToString();
            }
            set
            {
                ViewState["UserName"] = value;
            }
        }

        public int ProjectID
        {
            get { return drpProject.SelectedIndex > 0 ? int.Parse(drpProject.SelectedValue) : 0; }
        }
        #endregion

        #region Util
        void RedirectForEdit(string bathCancelId)
        {
            Response.Redirect(string.Format("~/Pages/BatchCancel.aspx?a=e&bcid={0}", bathCancelId));
        }

        void RedirectForNew()
        {
            Response.Redirect("~/Pages/BatchCancel.aspx");
        }

        void EnableDisableParentProject()
        {
            if (drpCancelType.SelectedItem.Text.Equals("REASSIGN") == true) {
                drpParentProject.Enabled = true;
            }
            else {
                drpParentProject.SelectedIndex = 0;
                drpParentProject.Enabled = false;
            }
        }

        void LoadCancelStatusValue()
        {
            if (drpCancelType.SelectedIndex > 0) {
                int cancelTypeId              = int.Parse(drpCancelType.SelectedValue);
                int statusMasterId            = cancel_type.GetStatusMasterByCancelTypeId(cancelTypeId).status_master_id;
                drpCancelStatus.SelectedValue = statusMasterId.ToString();
                if (sendValue != null) sendValue(drpCancelType.SelectedItem.Text);
            }
            else {
                drpCancelStatus.SelectedIndex = 0;
            }
        }
        #endregion
    } 
}