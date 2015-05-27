#region Includes
using System;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Services;
//Owner policy NameSpace for TargetTestRun - TADS 4.2.4.10 11-24-2014
using INTEGRATORLib;
using SCRIPTINGLIB;
using SCRIPTERLib;
using System.DirectoryServices;
#endregion
namespace Greenspoon.Tess.Pages
{
    public partial class BatchEscrow : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageVariables();
            btnBSUpld.Attributes.Add("onClick", "return blkScanConfirm();");
            btnOPUpld.Attributes.Add("onClick", "return ownPolConfirm();");
            if (Page.IsPostBack == false) {
                SetUpBatchEscrowUsercontrol();
                if (PageIsEditMode) { SetUpEditForm(); return; }
                SetUpNewForm();
            }
            if (Page.IsPostBack)
            {
                string URL = (string)(Session["URL"]);                   
                if (!String.IsNullOrEmpty(URL) )
                {
                    Response.Redirect(URL);
                }
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            LoadContractNavigation();

            if (PageMode != PageModeEnum.Edit) return;
            var conId = ContractId;
            if (conId == 0) {
                return;
            }
            // Project Unit Level for Williamsburg Plantation project_id = 130
            int isunitlevel = int.Parse(ucBatchEscrow.ProjectId);
            var isunit      = project.GetProjectUnitLevel(isunitlevel);
            if (!isunit) { drpUnitLevel.Enabled = false; } else { drpUnitLevel.Enabled = true; };

            var usrConLegalNames = (Controls.LegalNames)LoadControl("~/Controls/LegalNames.ascx");
            if (usrConLegalNames != null) {
                usrConLegalNames.ContractID = conId;
                usrConLegalNames.FormName = FormName;
                usrConLegalNames.RecID    = EscrowId;   // RIQ-297
                usrConLegalNames.ShowActiveOnly = chkShowAllLN.Checked ? false : true;
                plcLegalNames.Controls.Add(usrConLegalNames);
            }
            var usrConStatus = (Controls.Status)LoadControl("~/Controls/Status.ascx");
            if (usrConStatus != null) {
                usrConStatus.ContractID = conId;
                // form name "bf" indicated batch foreclosure.
                usrConStatus.FormName = FormNameEnum.BatchEscrow;
                usrConStatus.RecID = EscrowId;
                plcStatus.Controls.Add(usrConStatus);
            }

            //Resale Foreclosure Web User Control Feb 2015
            var usrConTrs = (Controls.ResaleForeclosure)LoadControl("~/Controls/ResaleForeclosure.ascx");
            if (usrConTrs != null)
            {
                usrConTrs.ContractID = conId;
                usrConTrs.FormName = FormNameEnum.BatchEscrow;
                usrConTrs.RecID = EscrowId;
                plcResale.Controls.Add(usrConTrs);
            }

            var usrConInv = (Controls.Inventory)LoadControl("~/Controls/Inventory.ascx");
            if (usrConInv == null) return;
            usrConInv.ContractID = conId;
            usrConInv.FormName   = FormName;
            usrConInv.RecID      = EscrowId;
            plcInventory.Controls.Add(usrConInv);
        }

        #region Setup Form
        void SetUpNewForm()
        {
            divContract.Visible = false;
        }
        void SetUpEditForm()
        {
            if (EscrowId <= 0) return;
            //cashout deals.
            var iscashout = batch_escrow.IsCashOut(EscrowId);
            if (iscashout)
            {
                txtAmountFinanced.Enabled = false;
                txtMortDate.Enabled = false;
                txtMortRecDate.Enabled = false;
                txtMortBook.Enabled = false;
                txtMortPage.Enabled = false;
                txtExtraMortPages.Enabled = false;
            }
            BindAllContractDowpDown();
            divContract.Visible = true;
        }
        void SetUpBatchEscrowUsercontrol()
        {
            ucBatchEscrow.BatchEscrowID = EscrowId;
            ucBatchEscrow.UserName      = UserName;
            ucBatchEscrow.PageMode      = PageMode;
        }
        #endregion

        #region Button Clicks
        protected void SaveClick(object sender, EventArgs e)
        {
            var btn = sender as LinkButton;
            if (btn == null) return;

            if (btn.ID != "btnNewContract") {
                if (IsFormInValid()) return;
            }

            int ConId;
            switch (btn.ID) {
                case "btnNewContract":
                    ClearContractForm();
                    break;
                case "btnSaveNewContract":
                    ConId = Save();
                    if (ContractId == 0) {
                        if (ConId > 0) {
                            // User ba param in url indicates a button action
                            // this param will be used for user to return
                            // and start as new form. "n" represent return to a new form
                            var forwardPath =
                                string.Format("~/Pages/InventoryExpandedView.aspx?a=v&cid={0}&form={1}&id={2}&ba=n", ConId, FormName, EscrowId);
                            Response.Redirect(forwardPath);
                        }
                        else { lblMsg.Text = "Failed"; }
                    }
                    else {
                        if (ConId > 0) {
                            ClearContractForm();
                            BindContractDropDown();
                        }
                        else {
                            lblMsg.Text =    "Failed";
                        }
                    }
                    break;
                case "btnSaveContract":
                    if (ContractId == 0) {
                        ConId = Save();
                        if (ConId > 0) {
                            // User ba param in url indicates a button action
                            // this param will be used for user to return
                            // and start as new form. "s" indicates return to existing contract.
                            var forwardPath =
                                string.Format("~/Pages/InventoryExpandedView.aspx?a=v&cid={0}&form={1}&id={2}&ba=s", ConId, FormName, EscrowId);
                            Response.Redirect(forwardPath);
                        }
                        else { lblMsg.Text = "Failed"; }
                    }
                    else {
                        var conId = Save();
                        lblMsg.Text = 
                            conId > 0 
                                ? "Saved" 
                                : "Failed";
                        BindContractDropDown();
                        drpMasterId.SelectedValue = conId.ToString();
                    }
                    break;
            }
        }

        // RIQ-301 Add First and Last Record Navigation
        protected void LnkFirstClick(object sender, EventArgs e)
        {
            if (drpMasterId.Items.Count < 0) return;
            drpMasterId.SelectedIndex = 1;
            ContractId = int.Parse(drpMasterId.SelectedValue);
            SetUpEditForm();
        }
        // Last
        protected void LnkLastClick(object sender, EventArgs e)
        {
            if (drpMasterId.Items.Count < 0) return;
            drpMasterId.SelectedIndex = drpMasterId.Items.Count - 1;
            ContractId = int.Parse(drpMasterId.SelectedValue);
            SetUpEditForm();
        }

        protected void LnkNextClick(object sender, EventArgs e)
        {
            if (drpMasterId.SelectedIndex >= ( drpMasterId.Items.Count - 1 )) return;
            drpMasterId.SelectedIndex++;
            ContractId = int.Parse(drpMasterId.SelectedValue);
            SetUpEditForm();
        }

        protected void LnkPreviousClick(object sender, EventArgs e)
        {
            if (drpMasterId.SelectedIndex <= 1) return;
            drpMasterId.SelectedIndex--;
            ContractId = int.Parse(drpMasterId.SelectedValue);
            SetUpEditForm();
        }
        #endregion
        #region Save Form
        int Save()
        {
            var ui = new ContractDTO {
                ContractID      = ContractId.ToString(),
                BatchEscrowId   = EscrowId.ToString(),
                Active          = chkActive.Checked,
                NonComply       = txtNonComply.Text,
                FileOpen        = txtFileOpen.Text,
                DevK            = txtDevK.Text,
                PurchasePrice   = txtPurchasePrice.Text,
                AmountFinanced  = txtAmountFinanced.Text,
                ContractDate    = txtContractDate.Text,
                ClientBatch     = txtClientBatch.Text,
                Share           = txtShare.Text,
                PolicyNumber    = txtPolicyNum.Text,
                SeasonId        = drpSeason.SelectedValue,
                VestingID       = drpVesting.SelectedValue,
                InterSpousal    = drpIntSp.SelectedValue,
                InitialFee      = txtIniFee.Text,
                AltBuild        = txtAltBld.Text,
                FixedFloat      = drpFixFlt.SelectedValue,
                MaritalStatusId = drpMarStat.SelectedValue,
                GenderId        = drpGender.SelectedValue,
                PartialWkID     = drpPartWeek.SelectedValue,
                //AltUnit         = txtAltUnit.Text,
                MortBook        = txtMortBook.Text,
                MortRecDate     = txtMortRecDate.Text,
                MortDate        = txtMortDate.Text,
                MortPage        = txtMortPage.Text,
                DeedBook        = txtDeedBook.Text,
                DeedDate        = txtDeedDate.Text,
                DeedPage        = txtDeedPage.Text,
                PointsGroupId   = drpPointGroup.SelectedValue,
                Points          = txtPoints.Text,
                Cancel          = chkCancel.Checked,
                ExtraPages      = txtExtraPages.Text,
                ExtraMortPages  = txtExtraMortPages.Text,
                Color           = txtColor.Text,
                CreatedBy       = UserName,
                EmailOptout     = chkOptOut.Checked,     // cv092012 RIQ-257
                OwnersPolResend = chkResend.Checked,     // cv092012 RIQ-257
                StartPeriod     = txtStartPeriod.Text,   // cv092012 RIQ-263
                EndPeriod       = txtEndPeriod.Text,     // cv092012 RIQ-263
                UnitLevel       = drpUnitLevel.SelectedValue,
                //Resale Forclosure Orlando Perfect Practice
                ModifyDate      = DateTime.Today.ToString(),
                ModifyBy        = UserName
            };
            return contract.Save(ui);
        }
        #endregion
        #region DropDown index change.
        protected void drpMasterId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpMasterId.SelectedIndex > 0) {
                ContractId = 
                    int.Parse(drpMasterId.SelectedValue);
                SetUpEditForm();
            }
            else {
                ClearContractForm();
                lnkPrevious.Enabled = false;
            }
            // Reset Doc Panel
            ToggleDocPanel(false);
            Page.SetFocus(drpMasterId);
        }
        #endregion
        #region Bind Drop Downs.
        void BindAllContractDowpDown()
        {
            BindMaritalStatus();
            BindGender();
            BindSeasonDropDown();
            BindVestingDropDown();
            BindPointGroup();
            // This has to be called at the end.
            BindContractDropDown();
            // Project Unit Level
            BindProjectUnitLevelDropDown();
        }

        void BindContractDropDown()
        {
            if (EscrowId <= 0) return;
            var list = contract.GetContractListByBatchId(EscrowId);
            drpMasterId.DataSource = list;
            drpMasterId.DataBind();
            if (ContractId > 0) {
                if (drpMasterId.Items.Contains(new ListItem(ContractId.ToString()))) {
                    drpMasterId.SelectedValue = ContractId.ToString();
                    ShowContractInfo();
                    Page.SetFocus(drpMasterId);
                }
            }
            else {
                SetDefaultFieldValues();
            }
        }
        void BindSeasonDropDown()
        {
            var list = Season.GetSeasonList();
            drpSeason.DataSource = list;
            drpSeason.DataBind();
        }

        void BindMaritalStatus()
        {
            var list = marital_status.GetMaritalStatus();
            drpMarStat.DataSource = list;
            drpMarStat.DataBind();
        }
        void BindGender()
        {
            var list = gender.GetGenderTypes();
            drpGender.DataSource = list;
            drpGender.DataBind();
        }

        void BindPointGroup()
        {
            var pList = project_points.GetPointsGroup(EscrowId);
            if (pList.Count > 0) {
                drpPointGroup.DataSource = pList;
                drpPointGroup.DataBind();
                drpPointGroup.Enabled = true;
                txtPoints.Enabled     = true;
            }
            else {
                drpPointGroup.Enabled = false;
                txtPoints.Enabled     = false;
            }
        }

        void BindVestingDropDown()
        {
            var list = vesting.GetVestingList();
            drpVesting.DataSource = list;
            drpVesting.DataBind();
        }
        // Project Unit Level 
        private void BindProjectUnitLevelDropDown()
        {
            var list = project_unit_level.GetProjectUnitLevels();
            drpUnitLevel.DataSource = list;
            drpUnitLevel.DataBind();
        }

        #endregion
        #region Page Setup
        void BindForm()
        {
            var ui = contract.GetContractUI(ContractId);
            if (ui == null) return;
            drpMasterId.SelectedValue    = ui.ContractID;
            txtFileOpen.Text             = ui.FileOpen;
            chkActive.Checked            = ui.Active;
            txtNonComply.Text            = ui.NonComply;
            txtDevK.Text                 = ui.DevK;
            txtPurchasePrice.Text        = ui.PurchasePrice;
            txtAmountFinanced.Text       = ui.AmountFinanced;
            txtContractDate.Text         = ui.ContractDate;
            txtClientBatch.Text          = ui.ClientBatch;
            txtShare.Text                = ui.Share;
            txtPolicyNum.Text            = ui.PolicyNumber;
            txtMortDate.Text             = ui.MortDate; //ui.CancelDate;
            drpSeason.SelectedValue      = ui.SeasonId;
            drpVesting.SelectedValue     = ui.VestingID;
            drpIntSp.SelectedValue       = ui.InterSpousal;
            txtIniFee.Text               = ui.InitialFee;
            txtAltBld.Text               = ui.AltBuild;
            lblBankcrupt.Text            = ui.Bankrupt;
            drpFixFlt.SelectedValue      = ui.FixedFloat;
            drpMarStat.SelectedValue     = ui.MaritalStatusId;
            drpGender.SelectedValue      = ui.GenderId;
            drpPartWeek.SelectedValue    = ui.PartialWkID;
            //txtAltUnit.Text              = ui.AltUnit;     //
            lblForeclosureDate.Text      = ui.ForeclosureDate;
            txtMortBook.Text             = ui.MortBook;
            txtMortRecDate.Text          = ui.MortRecDate;
            txtMortPage.Text             = ui.MortPage;
            txtDeedBook.Text             = ui.DeedBook;
            txtDeedDate.Text             = ui.DeedDate;
            txtDeedPage.Text             = ui.DeedPage;
            drpPointGroup.SelectedValue  = ui.PointsGroupId;
            txtPoints.Text               = ui.Points;
            chkCancel.Checked            = ui.Cancel;
            txtExtraPages.Text           = ui.ExtraPages;
            txtExtraMortPages.Text       = ui.ExtraMortPages;
            txtColor.Text                = ui.Color;
            chkOptOut.Checked            = ui.EmailOptout;       // cv092012 RIQ-257
            chkResend.Checked            = ui.OwnersPolResend;  // cv092012  RIQ-257
            txtStartPeriod.Text          = ui.StartPeriod;     // cv092012 RIQ-263
            txtEndPeriod.Text            = ui.EndPeriod;       // cv092012 RIQ-263
            drpUnitLevel.SelectedValue   = ui.UnitLevel;       // Project Unit Level

            var isCode631 = status.stopGVGpopup(ContractId);    //RIQ-276

            if ( (ui.IsGVG) && (!isCode631) )                  //RIQ-276
            {
                ShowAVGWarning();
            }
            //cashout deals.
            var iscashout = batch_escrow.IsCashOut(EscrowId);
            if (iscashout)
            {
                txtAmountFinanced.Enabled = false;
                txtMortDate.Enabled = false;
                txtMortRecDate.Enabled = false;
                txtMortBook.Enabled = false;
                txtMortPage.Enabled = false;
                txtExtraMortPages.Enabled = false;
            }

            activeBG.Attributes["class"] = !ui.Active ? "redBackground" : string.Empty;
        }

        void ShowContractInfo()
        {
            BindForm();
            ApplyFormBusinessRule();
        }

        void ApplyFormBusinessRule()
        {
            var proj = 
                contract.GetProjectByContractId(ContractId);
            if (proj == null) return;
            drpSeason.Enabled = 
                proj.is_season_used.HasValue ? (bool)( proj.is_season_used ): true;
            txtClientBatch.Enabled = true; //proj.developer_id == 5 || proj.developer_id == 14;
            txtIniFee.Enabled = 
                proj.is_initial_fee_used.HasValue
                    ? (bool)( proj.is_initial_fee_used )
                    : true;
            txtAltBld.Enabled =
                proj.is_alt_building_used.HasValue
                    ? (bool)( proj.is_alt_building_used )
                    : true;
            //txtAltUnit.Enabled = 
            //    proj.is_alt_unit_used.HasValue
            //        ? (bool)( proj.is_alt_unit_used )
            //        : true;
            drpMarStat.Enabled =
                proj.is_marital_status_used.HasValue 
                    ? (bool)( proj.is_marital_status_used )
                    : true;
            drpIntSp.Enabled = 
                proj.is_marital_status_used.HasValue
                    ? (bool)( proj.is_marital_status_used )
                    : true;
            drpGender.Enabled =
                proj.is_gender_used.HasValue
                    ? (bool)( proj.is_gender_used )
                    : true;
            txtPolicyNum.Enabled = proj.is_policy_auto.HasValue
                    ? !(bool)( proj.is_policy_auto )
                    : true;
        }
        #endregion
        #region Page Util
        void SetFileOpenDefaultDate()
        {
            txtFileOpen.Text = DateTime.Now.ToDateOnly();
        }

        void SetDefaultFieldValues()
        {
            SetFileOpenDefaultDate();
            chkActive.Checked = true;
            chkCancel.Checked = false;  //help desk change request by Shirley September 19, 2014
        }

        void LoadContractNavigation()
        {
            // Load Navigation button
            if (drpMasterId.Items.Count <= 1) return;

            //RIQ-301
            if (drpMasterId.SelectedIndex == 1) { LnkFirst.Enabled = false; } else { LnkFirst.Enabled = true; }
            if (drpMasterId.SelectedIndex == drpMasterId.Items.Count - 1) {LnkLast.Enabled = false;} else { LnkLast.Enabled = true; }

            if (drpMasterId.SelectedIndex < drpMasterId.Items.Count - 1) {
                lnkNext.Enabled = true;
            }
            if (drpMasterId.SelectedIndex > 1) {
                lnkPrevious.Enabled = true;
            }
            lblNumPage.Text = string.Format("{0} / {1}", drpMasterId.SelectedIndex, drpMasterId.Items.Count - 1);
        }

        void ClearContractForm()
        {
            var empty = string.Empty;
            ContractId              = 0;
            SetDefaultFieldValues();
            drpMasterId.SelectedValue    = empty;
            txtDevK.Text                 = empty;
            txtPurchasePrice.Text        = empty;
            txtContractDate.Text         = empty;
            txtMortDate.Text             = empty;
            txtNonComply.Text            = empty;
            txtClientBatch.Text          = empty;
            txtShare.Text                = empty;
            txtAmountFinanced.Text       = empty;
            txtPolicyNum.Text            = empty;
            txtMortDate.Text             = empty;
            drpSeason.SelectedValue      = empty;
            drpVesting.SelectedValue     = empty;
            drpIntSp.SelectedValue       = empty;
            txtIniFee.Text               = empty;
            txtAltBld.Text               = empty;
            lblBankcrupt.Text            = empty;
            drpFixFlt.SelectedValue      = empty;
            drpMarStat.SelectedValue     = empty;
            drpGender.SelectedValue      = empty;
            drpPartWeek.SelectedValue    = empty;
            //txtAltUnit.Text              = empty;
            lblForeclosureDate.Text      = empty;
            txtMortBook.Text             = empty;
            txtMortRecDate.Text          = empty;
            txtMortPage.Text             = empty;
            txtDeedBook.Text             = empty;
            txtDeedDate.Text             = empty;
            txtDeedPage.Text             = empty;
            drpPointGroup.SelectedValue  = empty;
            txtPoints.Text               = empty;
            txtExtraPages.Text           = empty;
            txtExtraMortPages.Text       = empty;
            drpUnitLevel.SelectedValue   = empty;
        }

        void ToggleDocPanel(bool showDoc)
        {
            btnShowImage.Visible = !showDoc;
            gvScannedImages.Visible = showDoc;
        }

        void SetPageVariables()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            FormName = FormNameEnum.BatchEscrow;
            if (PageIsEditMode) {
                EscrowId = Request.QueryString.GetValue<int>("beid");
                ContractId =
                    drpMasterId.SelectedIndex > 0 ? int.Parse(drpMasterId.SelectedValue)
                    : ( Page.IsPostBack == false ) ? Request.QueryString.GetValue<int>("cid")
                    : 0;
            }
            ToggleDocPanel(false);
        }
        #endregion

        protected void BtnShowImageClick(object sender, EventArgs e)
        {
            ToggleDocPanel(true);
            var data = DbService.GetBulkScanImages(ucBatchEscrow.ProjectId, ContractId.ToString());
            gvScannedImages.DataSource = data; 
            gvScannedImages.DataBind();
        }

        protected string GetDocHandlerUrl(string docType, string docLoacation)
        {
            if (string.IsNullOrEmpty(docType) || string.IsNullOrEmpty(docLoacation))
                return string.Empty;
            return string.Format("../Services/DocHandler.ashx?doctype={0}&doc={1}", docType, HttpUtility.UrlEncode(docLoacation));
        }

        void RegisterAVGScript()
        {
            const string csName = "showAVGWarning";
            var csType = GetType();
            var cs = Page.ClientScript;
            if (cs.IsStartupScriptRegistered(csType, csName)) return;

            var sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">");
            sb.AppendLine("$(document).ready(function () {");
            sb.AppendLine("tb_show(\"Great Vacation Getaways\", \"../is_gvg_warning.htm?height=180&width=600&TB_iframe=true\", \"\");");
            sb.AppendLine("});");
            sb.AppendLine("</script>");
            cs.RegisterStartupScript(csType, csName, sb.ToString());
        }

        void ShowAVGWarning()
        {
            RegisterAVGScript();
        }

        bool IsFormInValid()
        {
            var isNonEscrow = batch_escrow.IsNonEscrow(EscrowId);
            if (!isNonEscrow) {
                if (string.IsNullOrEmpty(txtDevK.Text)) {
                    lblMsg.Text = "Dev K required for Non Escrow.";
                    lblMsg.ForeColor = Color.Red;
                    txtDevK.Focus();
                    return true;
                }

                if (string.IsNullOrEmpty(txtContractDate.Text)) {
                    lblMsg.Text = "Contract Date Required for Non Escrow.";
                    lblMsg.ForeColor = Color.Red;
                    txtContractDate.Focus();
                    return true;
                }
            }
            return false;
        }
        // Owner's Policy do not bulk scan when Master ID is Inactive CV082012
        bool ActiveStatusMasterId()
        {
            var IsActiveMasterID = batch_escrow.IsActiveMasterID(ContractId);
            if (IsActiveMasterID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool OwnerPolicyActive()
        {

            var IsOwnerPolicyActive = batch_escrow.IsOwnerPolicyActive(ContractId);
            if (IsOwnerPolicyActive)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //single Owner Policy
        protected void btnOPUpld_Click(object sender, EventArgs e)
        {
            if ( OwnerPolicyActive() )
            {
                //try
                //{

                    ContractId = int.Parse(drpMasterId.SelectedValue);

                    Integrator ic = new Integrator();
                    ic.OpenProject("C:\\Tess\\Projects\\Tess.trg");
                    IScripting s = ic.GetItem("WebPrint");
                    s.GetVar("MasterID").value = Convert.ToString(ContractId);
                    s.Run("Start");
                    ic.Close();

                    System.Threading.Thread.Sleep(20000);

                    lblbsMsgs.Text = "Action Successful";
                    lblbsMsgs.ForeColor = Color.Green;

                    var sb = new System.Text.StringBuilder();
                    sb.Append("~/SingleOwnerPolicy/").Append(Convert.ToString(ContractId) + ".pdf");
                    ResponseHelper.Redirect(sb.ToString(), "_blank", "menubar=0,width=1200,height=900");
                //}
                //catch
                //{
                //    var path = "IIS://TESSWEB/W3SVC/AppPools/DefaultAppPool";

                    // Create the directory entry to control the app pool
                    // VB.NET var appPool = new      DirectoryEntry(path);
                //    DirectoryEntry appPool = new DirectoryEntry(path);

                    // Invoke the recycle action.
                //    appPool.Invoke("Recycle", null);
               // }
            }
            else
            {
                lblbsMsgs.Text = "Error: Action Failed";
                lblbsMsgs.ForeColor = Color.Red;
            }

        }

        protected void btnBSUpld_Click(object sender, EventArgs e)
        {
            if (ActiveStatusMasterId())
            {
                if (DbService.BatchEscrowBulkScan(EscrowId, UserName)) {
                    lblbsMsg.Text = "Action Successful";
                    lblbsMsg.ForeColor = Color.Green;
                }
                else {
                    lblbsMsg.Text = "Error: Action Failed";
                    lblbsMsg.ForeColor = Color.Red;
                }
                //lblbsMsg.Text = "true";
            }
            else
            {
                lblbsMsg.Text = "MasterId Inactive, NOT Bulk Scan";
            }
        }
    }
}
