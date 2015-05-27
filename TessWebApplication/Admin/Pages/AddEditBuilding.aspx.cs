using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditBuilding : PageBase
    {
        private int _buildingId;
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack != false) return;
            if (PageIsEditMode) { SetupEditForm(); }
        }

        private void SetupEditForm()
        {
            var b = inventory_building.GetBuilding(_buildingId);
            txtBuildingName.Text = b.building_name;
            txtBuildingCode.Text = b.building_code;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var bid = Save();
            if (bid > 0) {
                ReturnToParentPage(bid);
            }
            else { lblMsg.Text = "ERROR: Failed"; }
        }

        int Save()
        {
            if (ContractId == 0) { return 0; }
            var b = new BuildingDTO {
                InventoryBuildingId = _buildingId.ToString(),
                ContractId = ContractId.ToString(),
                BuildingCode = txtBuildingCode.Text,
                BuildingName = txtBuildingName.Text,
                Active = true,
                CreatedBy = UserName,
            };
            return inventory_building.Save(b);
        }

        void SetPageBase()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            ContractId = Request.QueryString.GetValue<int>("cid");
            RecID = Request.QueryString.GetValue<int>("id");
            _buildingId = Request.QueryString.GetValue<int>("buildingId");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToParentPage(_buildingId);
        }

        string GetReturnUrl(int buildingId)
        {
            return string.Format("~/Pages/Inventory.aspx?a={0}&id={1}&cid={2}&buildingId={3}",
                RecID == 0 ? "n" : "e", RecID, ContractId, RecID == 0 ? buildingId : 0);
        }

        void ReturnToParentPage(int buildingId)
        {
            Response.Redirect(GetReturnUrl(buildingId));
        }
    }
}