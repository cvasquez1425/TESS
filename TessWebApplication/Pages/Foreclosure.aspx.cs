#region Includes
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using System.Data.Entity;
#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class Foreclosure : PageBase
    {
        #region Page Events
        void Page_Load(object sender, EventArgs e)
        {
            // Set page mode and batch foreclosure id.
            SetPageVariables();
            if (Page.IsPostBack == false) {
                // Check to see if page is in edit mode and batch foreclosure id
                // is Valid. If the id is invalid and page mode is edit.
                // display user error message and set the page to new state.
                if (
                    ( PageMode == PageModeEnum.Edit ) 
                    && ( BatchForeclosureId > 0 )
                    && ( batch_foreclosure.isValid(BatchForeclosureId) == false )) {
                    // Set as new. Since invalid data was passed.
                    RedirectToNew();
                }
                // Set up the user form.
                SetUserControlProperties();
                if (PageMode == PageModeEnum.Edit) {
                    SetUpEditForm();
                }
                else {
                    SetUpNewForm();
                }
            }
        }
        void Page_PreRender(object sender, EventArgs e)
        {
            if (PageMode != PageModeEnum.Edit) return;
            var conId = 0;
            if (drpContract.Items.Count > 0) {
                conId = Convert.ToInt32(drpContract.SelectedItem.Text);
            }
            // Load transactions user control from control folder.
            // ---------------------------------------------------
            //var usrContr = 
            //    (Controls.Transactions)LoadControl("~/Controls/Transactions.ascx");
            //if (usrContr != null) {
            //    usrContr.ContractID = conId;
            //    usrContr.FormName = FormName;
            //    plcTransactions.Controls.Add(usrContr);
            //}
            // Load Inventory user control from control folder.
            // ------------------------------------------------
            var usrConInv = (Controls.Inventory)LoadControl("~/Controls/Inventory.ascx");
            if (usrConInv != null) {
                usrConInv.ContractID = conId;
                usrConInv.FormName = FormName;
                usrConInv.RecID      = BatchForeclosureId;
                plcInventory.Controls.Add(usrConInv);
            }
            // Load Status user control from control folder.
            // -----------------------------------------------
            var usrConStatus = (Controls.Status)LoadControl("~/Controls/Status.ascx");
            if (usrConStatus != null) {
                usrConStatus.ContractID = conId;
                usrConStatus.FormName   = FormName;
                usrConStatus.RecID      = BatchForeclosureId;
                plcStatus.Controls.Add(usrConStatus);
            }
            // Load Legal Name user control from control folder.
            // ------------------------------------------------
            var usrConLegalNames = (Controls.LegalNames)LoadControl("~/Controls/LegalNames.ascx");
            if (usrConLegalNames != null) {
                usrConLegalNames.ContractID = conId;
                usrConLegalNames.FormName = FormName;
                plcLegalNames.Controls.Add(usrConLegalNames);
            }
        } // end of pre_render
        #endregion
        #region Form Event
        protected void btnLoadContracts_Click(object sender, EventArgs e)
        {
            LoadForeclosureForm();
        }
        protected void drpContract_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindForm();
        }
        #endregion
        #region Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var btn = sender as LinkButton;
            if (btn == null) return;
            // If contract is marked as active. 
            // Validate that there is no other contract is active.
            if (chkActive.Checked) {
                var contractId = 
                    int.Parse(drpContract.SelectedItem.ToString());
                // Check to see if the foreclosure is active.
                var foreclosedContract = 
                    foreclosure.GetActiveForeclosure(contractId, BatchForeclosureId);
                // If no contract is active
                // then allow save. 
                if (foreclosedContract == null) {
                    Save(btn.ID);
                }
                else {
                    CreateMsg(string.Format("{0} is already Active in Batch: {1}"
                                            , contractId
                                            , foreclosedContract.batch_foreclosure_id));
                    chkActive.Checked = false;
                }
            }
            else {
                Save(btn.ID);
            }
        }
        /// <summary>
        /// Save foreclosure.
        /// </summary>
        void Save(string action)
        {
            var ui = new ForeclosureDTO {
                ForeclosureId       = drpContract.SelectedValue,
                BatchForeclosureId  = BatchForeclosureId.ToString(),
                ContractId          = drpContract.SelectedItem.ToString(),
                IneterestPct        = txtInterestPct.Text,
                DefaultBalance      = txtDefaultBalance.Text,
                DefaultDate         = txtDefaultDate.Text,
                OnHold              = chkOnHold.Checked,
                Bankrupt            = chkBankrupt.Checked,
                Active              = chkActive.Checked,
                StatusId            = ucBatchForeclosure.StatusId              // April 2014 cannot insert the value null into column 'status_master_id dbo.status
            };
            // If save is successful.
            if (foreclosure.Save(ui)) {
                if (action == "btnSaveNext") {
                    // Check to see if it is end of the contract id list.
                    // if not then move to next contract.
                    // if yes, display message that user have
                    // reached the last record.
                    var nextIndex = drpContract.SelectedIndex + 1;
                    var lastIndex = drpContract.Items.Count;
                    if (nextIndex < lastIndex) {
                        // Move to next record in contract list.
                        drpContract.SelectedIndex = nextIndex;
                        // Bind form with the new contract id.
                        BindForm();
                    }
                    else {
                        CreateMsg("Save complete, reached end of record.");
                    }
                }
                else {
                    CreateMsg("Saved");
                }
            }
            else { CreateMsg("Failed to Save"); }
        }
        #endregion
        #region Form Setup
        /// <summary>
        /// Set up user control for Batch Foreclosure.
        /// </summary>
        void SetUserControlProperties()
        {
            ucBatchForeclosure.UserName             = UserName;
            ucBatchForeclosure.BatchForeclosureID   = BatchForeclosureId;
        }
        /// <summary>
        /// Do not load foreclosure form.
        /// </summary>
        void SetUpNewForm()
        {
            divForeclosure.Visible = false;
        }
        void SetUpEditForm()
        {
            if (BatchForeclosureId <= 0) return;
            divForeclosure.Visible = true;
            LoadForeclosureForm();
            bool chkActiveDefault = defaultchkActive(ucBatchForeclosure.BatchForeclosureID);
            btnAddContractId.HRef = 
                GetAddToForeclosureModalLink();
        }

        private bool defaultchkActive(int batchForeclosureId)
        {
            using(var ctx = DataContextFactory.CreateContext()) 
            {
                var activeDefault = from f in ctx.foreclosures
                                    .Where(r => r.batch_foreclosure_id == batchForeclosureId)
                                    select f;
                if (activeDefault.Any() == true) { return true; } else { return false; }
            }
        }

        void LoadForeclosureForm()
        {
            BindContractDropDown();
            BindForm();
        }
        void BindContractDropDown()
        {
            var contractList = 
                foreclosure.GetForeclosureContractList(BatchForeclosureId);
            if (contractList.Count > 0) {
                drpContract.DataSource = contractList;
                drpContract.DataBind();
                drpContract.Enabled = true;
                lnkSave.Enabled = true;
                btnSaveNext.Enabled = true;
            }
            else { drpContract.Enabled = false; lnkSave.Enabled = false; btnSaveNext.Enabled = false; }
        }
        void BindForm()
        {
            if (drpContract.Items.Count <= 0) return;
            var foreclosureId = int.Parse(drpContract.SelectedValue);
            var ui = foreclosure.GetForeclosureUI(foreclosureId);
            if (ui == null) return;
            lblDevK.Text            = ui.DevK;
            txtInterestPct.Text     = ui.IneterestPct;
            txtDefaultBalance.Text  = ui.DefaultBalance;
            txtDefaultDate.Text     = ui.DefaultDate;
            chkOnHold.Checked       = ui.OnHold;
            chkBankrupt.Checked     = ui.Bankrupt;
            lblMortBook.Text        = ui.MortgageBook;
            lblMortPage.Text        = ui.MortgagePage;
            lblMortDate.Text        = ui.MortgageDate;
            lblDeedBook.Text        = ui.DeedBook;
            lblDeedDate.Text        = ui.DeedDate;
            lblDeedPage.Text        = ui.DeedPage;
            lblVesting.Text         = ui.Vesting;
            lblPoints.Text          = ui.Points;
            lblPointsGroup.Text     = ui.PointsGroup;
            chkActive.Checked       = ui.Active;
            SetFocus(drpContract);
        }

        #endregion
        #region Util
        void SetPageVariables()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            FormName = FormNameEnum.Foreclosure;
            if (PageMode == PageModeEnum.Edit) {
                BatchForeclosureId = Request.QueryString.GetValue<int>("bfid");
            }
            if (PageMode == PageModeEnum.New) {
                chkActive.Checked = true;
            }
        }
        void RedirectToNew()
        {
            Response.Redirect("~/Pages/Foreclosure.aspx");
        }
        string GetAddToForeclosureModalLink()
        {
            return string.Format("~/Pages/AddContractToForeclosure.aspx?id={0}&TB_iframe=true&height=340&width=400",
                BatchForeclosureId);
        }
        void CreateMsg(string msg)
        {
            lblMsg.Text = msg;
        }
        #endregion
    }
}