#region Includes
using System;
using System.Linq;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
//Owner policy NameSpace for TargetTestRun 
using INTEGRATORLib;
using SCRIPTINGLIB;
using SCRIPTERLib;
using System.Web.UI.WebControls;
#endregion
namespace Greenspoon.Tess.Controls
{
    public partial class BatchEscrow : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnOwnerPolUpld.Attributes.Add("onClick", "return ownPolConfirm();");  //RIQ-296
            chkCashOut.Attributes.Add("onClick", "chkCashOutChanges();");           // 02/26/2014
            chkNonEscrow.Attributes.Add("onClick", "chkNonEscrowBatch();");              // November 2014 work order 28262
            if (Page.IsPostBack == false) {
                BindProjectDropDown();
                if (PageMode == PageModeEnum.Edit) {
                    SetUpEditForm();
                }
                else {
                    SetUpNewForm();
                }
            }
        }

        //Owner Policy using Escrow Key for Las Vegas Only
        protected void btnOwnerPolUpld_Click(object sender, EventArgs e)
        {
            //if (OwnerPolicyActive())
            //{
            string btchEscID = lblBatchId.Text.ToString();
            int BtchEscrowID  = int.Parse(btchEscID);

            Integrator ic = new Integrator();
            ic.OpenProject("C:\\TessBatchVegas\\Projects\\TessBatchVegas.trg");
            IScripting s = ic.GetItem("AddEscrowKey");
            s.GetVar("EscrowKey").value = Convert.ToString(BtchEscrowID);
            s.Run("Start");
            ic.Close();
        }

        #region Create View
        private void SetUpNewForm()
        {
            lblCreatedBy.Text  = UserName;
            lblCreateTime.Text = DateTime.Now.ToDateOnly();
            drpbatTC.SelectedIndex = 1; // default to Yes.
        }

        private void SetUpEditForm()
        {
            var ui = batch_escrow.GetBatchEscrowDTO(BatchEscrowID);
            if (ui == null) return;
            drpProject.SelectedValue = ui.ProjectId;            
            // RIQ-296
            var OwnerPolUpld   = project.IsLasVegasProjId(Convert.ToInt32(drpProject.SelectedValue));
            if (OwnerPolUpld != true) { btnOwnerPolUpld.Enabled = false; } else { btnOwnerPolUpld.Enabled = true; }

            // Set up phase drop down.
            // Phase drop down is based on project Id.
            if (drpProject.SelectedIndex > 0) {
                BindPhaseDropDown();
                BindPartnerDropDown();
            }
            drpPhase.SelectedValue = ui.PhaseId;
            drpPartner.SelectedValue = ui.PartnerId;
            lblBatchEscrowKey.Text = ui.EscrowKey;
            lblBatchId.Text        = ui.BatchEscrowId;
            drpbatTC.SelectedValue = ui.TitleInsurance.ToString();
            txtTotalDeedPages.Text = ui.TotalDeedPages;
            txtTotalDeedNotes.Text = ui.TotalNotePages;
            chkNonEscrow.Checked   = ui.NonEscrow;
            //Work Order 28262  Create Pop-up when Non-Escrow is Unchecked to confirm  chkNonEscrow
            if (chkNonEscrow.Checked) { chkNonEscrowHidden.Value = "1" ;}
            else {
                chkNonEscrowHidden.Value = "0";
            }

            chkCashOut.Checked     = ui.Cashout;    // cashout deals
            lblCreatedBy.Text      = ui.CreatedBy;
            lblCreateTime.Text     = ui.CreatedDate;
        }
        #endregion
        #region User Events

        protected void SavetBatch_OnClick(object sender, EventArgs e)
        {
            // Save the Batch Escrow.
            int beID = Save();
            if (PageMode == PageModeEnum.Edit) {
                // If save is successful or failed.
                // display message.
                lblBatchMsg.Text = beID > 0 ? "Saved" : "Failed";
            }
            else {
                RedirectForEdit(beID);
            }
        }
   
        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpPhase.Items.Clear();
            drpPhase.Enabled = false;
            // If user selects a valid project.
            // load phase specific to the project.
            if (drpProject.SelectedIndex <= 0) return;
            BindPhaseDropDown();
            BindPartnerDropDown();
            PopulateDeedNotesAndPagesFields();
        }

        private void PopulateDeedNotesAndPagesFields()
        {
            project p = 
                project.GetProjectByProjectId(int.Parse(drpProject.SelectedValue));
            txtTotalDeedNotes.Text = p.default_rec_note_pages.ToString();
            txtTotalDeedPages.Text = p.default_rec_deed_pages.ToString();
            //chkCashOut status  02/26/2014
            HiddenField1.Value = p.default_rec_note_pages.ToString();
        }
        #endregion
        #region Save Methods
        int Save()
        {
            var ui = batch_escrow.CreateBatchEscrowDTO();
            ui.BatchEscrowId = BatchEscrowID.ToString();
            ui.ProjectId = drpProject.SelectedValue;
            ui.PhaseId = drpPhase.SelectedValue;
            ui.TitleInsurance = drpbatTC.SelectedValue.NullIfEmpty<bool?>();
            ui.TotalDeedPages = txtTotalDeedPages.Text;
            ui.TotalNotePages = txtTotalDeedNotes.Text;
            ui.CreatedBy = lblCreatedBy.Text;
            ui.NonEscrow = chkNonEscrow.Checked;
            ui.Cashout   = chkCashOut.Checked;    // cashout deals
            ui.PartnerId = drpPartner.SelectedValue;
            // Resale Forclosure Orlando
            ui.ModifyDate = DateTime.Today.ToString();
            ui.ModifyBy   = UserName;
            return batch_escrow.Save(ui);
        }
        #endregion
        #region Bind Data
        void BindProjectDropDown()
        {
            var data = project.GetProjectList();
            drpProject.DataSource = data;
            drpProject.DataBind();
        }
   
        void BindPhaseDropDown()
        {
            drpPhase.ClearSelection();
            var data = phase_detail.GetPhaseListByProjectId(Convert.ToInt32(drpProject.SelectedValue));
            if (data.Any() != true) return;
            drpPhase.DataSource = data;
            drpPhase.DataBind();
            drpPhase.Enabled = true;
        }

        void BindPartnerDropDown()
        {
            drpPartner.ClearSelection();
            var data = partner.GetPartnerByProject(Convert.ToInt32(drpProject.SelectedValue));
            if (data.Count <= 1) { return; }
            drpPartner.DataSource = data;
            drpPartner.DataBind();
            drpPartner.Enabled = true;
        }

        #endregion
        #region Properties
        public int BatchEscrowID
        {
            get
            {
                return ViewState["BatchEscrowID"] == null ? 0
                    : int.Parse(ViewState["BatchEscrowID"].ToString());
            }
            set
            {
                ViewState["BatchEscrowID"] = value;
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

        public PageModeEnum PageMode
        {
            get
            {
                return ViewState["PageMode"] == null ? PageModeEnum.New
                    : (PageModeEnum)ViewState["PageMode"];
            }
            set
            {
                ViewState["PageMode"] = value;
            }
        }

        public string ProjectId
        {
            get { return drpProject.SelectedValue; }
        }
        #endregion
        #region Util
        void RedirectForEdit(int beid)
        {
            Response.Redirect(string.Format("~/Pages/BatchEscrow.aspx?a=e&beid={0}", beid));
        }
        #endregion
    }
}