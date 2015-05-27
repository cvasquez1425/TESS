#region Includes
using System;
using System.Linq;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using System.Web.UI.WebControls;
#endregion

namespace Greenspoon.Tess.Pages
{
    public partial class Inventory : PageBase
    {
        const string EDIT = "e";
        const string NEW = "n";
        int _buildingId;
        int _unitId;
        int _defaultBuildingId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack != false) return;
            BindDropDowns();
            BindInventoryGrid();
            if (PageIsEditMode && RecID > 0) {
                SetupEditForm();
                return;
            }
            if (( drpBuilding.Items.Count > 1 ) == false) {
                btnSaveClose.Enabled = false;
                btnSaveNew.Enabled   = false;
            }
            SetupNewForm();
            chkActive.Checked = true;
            drpBuilding.Focus();
        }

        void SetupNewForm()
        {
            // default can only come from new inventory grid.
            if (_defaultBuildingId > 0) {
                SelectBuildingAndBindUnit(_defaultBuildingId);
                return;
            }
            if (_buildingId <= 0) return;
            SelectBuildingAndBindUnit(_buildingId);
            if (_unitId <= 0) return;
            drpUnit.SelectedValue = _unitId.ToString();
        }

        private void SelectBuildingAndBindUnit(int buildingId)
        {
            drpBuilding.SelectedValue = buildingId.ToString();
            BindUnitDropDown(buildingId);
        }

        private void BindInventoryGrid()
        {
            var invList = contract_interval.GetInventoryUIList(ContractId);
            gvInventory.DataSource = invList;
            gvInventory.DataBind();
        }

        void SetupEditForm()
        {
            var ui = contract_interval.GetInventoryDTO(RecID);
            drpBuilding.SelectedValue = ui.InventoryBuildingId;
            // Bind Unit drop down after the building drop down.
            BindUnitDropDown(Convert.ToInt32(ui.InventoryBuildingId));
            drpUnit.SelectedValue     = ui.InventoryUnitId;
            lblFloor.Text             = ui.Floor;
            lblBedroom.Text           = ui.Bedroom;
            BindWeekDropDown();
            drpWeeks.SelectedValue    = ui.ContWeekMasterId;
            BindYearDorpDown();
            drpYears.SelectedValue    = ui.ContYearMasterId;
            lblABT.Text               = ui.ABT;
            chkActive.Checked         = ui.Active;
        }

        #region SAVE
        protected void Save_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            if (Save()) {
                var lb = sender as LinkButton;
                if (lb != null) {
                    if (lb.ID == "btnSaveClose") {
                        RegisterThickBoxCloseScript();
                    }
                    else {
                        var url = string.Format("~/Pages/Inventory.aspx?a=n&cid={0}&dbid={1}", ContractId, _defaultBuildingId);
                        Response.Redirect(url);
                    }
                }
                else { CreateMsg("Action Failed"); }
            }
            else { CreateMsg("Failed"); }
        }

        bool Save()
        {
            var ui = new InventoryDTO {
                ContractIntervalId  = RecID.ToString(),
                ContractId          = ContractId.ToString(),
                InventoryBuildingId = drpBuilding.SelectedValue,
                InventoryUnitId     = drpUnit.SelectedValue,
                ContWeekMasterId    = drpWeeks.SelectedValue,
                ContYearMasterId    = drpYears.SelectedValue,
                Active              = chkActive.Checked,
                CreatedBy           = UserName,
                //Resale Forclosure Orlando Perfect Practice
                ModifyDate = DateTime.Today.ToString(),
                ModifyBy = UserName,
            };
            var result = 
                contract_interval.Save(ui);
            return result;
        }

        #endregion

        #region DropDown SelectIndexchange Events
        protected void drpBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpUnit.Items.Clear();
            lblBedroom.Text = String.Empty;
            lblFloor.Text   = String.Empty;
            if (drpBuilding.SelectedIndex > 0) {
                BindUnitDropDown(int.Parse(drpBuilding.SelectedValue));
            }
            drpUnit.Focus();
        }

        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBedroom.Text = String.Empty;
            lblFloor.Text = String.Empty;
            using (var db  = DataContextFactory.CreateContext()) {
                if (drpUnit.SelectedIndex > 0) {
                    int unitId = int.Parse(drpUnit.SelectedValue);
                    var unit = ( from u in db.inventory_unit
                                 where u.inventory_unit_id == unitId
                                 select new {
                                     floor = u.floor_number,
                                     bedroom = u.number_of_bedrooms
                                 } ).SingleOrDefault();
                    if (unit != null) {
                        lblBedroom.Text = unit.bedroom;
                        lblFloor.Text = unit.floor.ToString();
                    }
                }
            }
            drpWeeks.Focus();
        }

        protected void drpYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblABT.Text = string.Empty;
            using (var db = DataContextFactory.CreateContext()) {
                if (drpYears.SelectedIndex <= 0) return;
                var yearId = int.Parse(drpYears.SelectedValue);
                var year = ( from y in db.cont_year_master
                             where y.cont_year_master_id == yearId
                             select new {
                                 abt = y.a_b_t
                             } ).SingleOrDefault();
                if (year != null) {
                    lblABT.Text = year.abt;
                }
            }
            btnSaveClose.Focus();
        }
        #endregion

        #region Bind DropDowns
        void BindDropDowns()
        {
            BindBuildingDropDown();
            BindWeekDropDown();
            BindYearDorpDown();
        }

        void BindBuildingDropDown()
        {
            var list = inventory_building.GetInventoryBuildingList(GetIntervalBatchEscrow());
            drpBuilding.DataSource = list;
            drpBuilding.DataBind();
        }

        void BindUnitDropDown(int buildingId)
        {
            var list = inventory_unit.GetInventoryUnitListByBuildingId(buildingId);
            drpUnit.DataSource = list;
            drpUnit.DataBind();
        }

        void BindWeekDropDown()
        {
            var list = 
                cont_week_master.GetContractWeekList();
            drpWeeks.DataSource = list;
            drpWeeks.DataBind();
        }

        void BindYearDorpDown()
        {
            var list = cont_year_master.GetContractYearList();
            drpYears.DataSource = list;
            drpYears.DataBind();
        }
        #endregion

        #region Util
        void SetPageBase()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            ContractId = Request.QueryString.GetValue<int>("cid");
            RecID = Request.QueryString.GetValue<int>("id");
            _buildingId = Request.QueryString.GetValue<int>("buildingId");
            _unitId = Request.QueryString.GetValue<int>("unitId");
            _defaultBuildingId = Request.QueryString.GetValue<int>("dbid");
        }

        batch_escrow GetIntervalBatchEscrow()
        {
            return ( PageMode == PageModeEnum.Edit ) 
                ? batch_escrow.GetBatchEscrowByContractIntervalId(RecID) 
                : batch_escrow.GetBatchEscrowByContractId(ContractId);
        }

        void RegisterThickBoxCloseScript()
        {
            Page.ClientScript
                .RegisterStartupScript(GetType(), "closeThickBox", "self.parent.Inventory_Updated();", true);
        }

        void CreateMsg(string msg)
        {
            lblMsg.Text = msg;
        }
        #endregion

        protected void btnAddEditBuilding_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            var mode = GetMode(sender);
            if (mode == EDIT && drpBuilding.SelectedIndex == 0) {
                lblMsg.Text = "Please select a building first.";
                drpBuilding.Focus();
                return;
            }
            Response.Redirect(string.Format("~/Admin/Pages/AddEditBuilding.aspx?a={0}&cid={1}&id={2}&buildingId={3}", mode, ContractId, RecID, mode == EDIT ? drpBuilding.SelectedValue : "0"));
        }

        protected void btnAddEditUnit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (drpBuilding.SelectedIndex == 0) {
                lblMsg.Text = "Please select a building first.";
                drpBuilding.Focus();
                return;
            }

            var mode = GetMode(sender);
            if (mode == EDIT && drpUnit.SelectedIndex == 0) {
                lblMsg.Text = "Please select a unit first.";
                drpUnit.Focus();
                return;
            }
            Response.Redirect(string.Format("~/Admin/Pages/AddEditUnit.aspx?a={0}&cid={1}&id={2}&buildingId={3}&unitId={4}", GetMode(sender), ContractId, RecID, drpBuilding.SelectedValue, mode == EDIT ? drpUnit.SelectedValue : "0"));
        }

        static string GetMode(object sender)
        {
            var b = sender as ImageButton;
            var mode = b != null && b.ID.ToLower().Contains("edit") ? EDIT : NEW;
            return mode;
        }
    }
}

