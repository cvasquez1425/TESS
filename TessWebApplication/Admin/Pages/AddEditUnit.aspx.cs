using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.DataObjects.Linq;

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditUnit : PageBase
    {
        int _buildingId;
        int _unitId;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (Page.IsPostBack != false) return;
            if (PageIsEditMode) { SetupEditForm(); }
        }

        void SetupEditForm()
        {
            var ui = inventory_unit.GetInventoryUnit(_unitId);
            txtUnitNum.Text     = ui.UnitNumber;
            txtNumBed.Text      = ui.NumberOfBedroom;
            txtDescription.Text = ui.Description;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var uid = Save();
            if (uid > 0) {
                ReturnToParentPage(uid);
            }
            else { lblMsg.Text = "Error: Count not save."; }
        }

        int Save()
        {
            if (_buildingId == 0) { return 0; }
            var ui = new UnitDTO {
                InventoryBuildingId = _buildingId.ToString(),
                InventoryUnitId     = _unitId.ToString(),
                NumberOfBedroom     = txtNumBed.Text,
                Active              = true,
                UnitNumber          = txtUnitNum.Text,
                Description         = txtDescription.Text,
                CreatedBy           = UserName
            };
            return inventory_unit.Save(ui);
        }

        void SetPageBase()
        {
            PageMode    = Request.QueryString.GetValue<PageModeEnum>("a");
            ContractId  = Request.QueryString.GetValue<int>("cid");
            RecID       = Request.QueryString.GetValue<int>("id");
            _buildingId = Request.QueryString.GetValue<int>("buildingId");
            _unitId     = Request.QueryString.GetValue<int>("unitId");
        }

        string GetReturnUrl(int unitId)
        {
            var isNewPageMode = RecID == 0;
            return string.Format("~/Pages/Inventory.aspx?a={0}&id={1}&cid={2}&buildingId={3}&unitId={4}",
               isNewPageMode ? "n" : "e", RecID, ContractId, isNewPageMode? _buildingId : 0, isNewPageMode ? unitId : 0);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToParentPage(_unitId);
        }

        void ReturnToParentPage(int unitId)
        {
            Response.Redirect(GetReturnUrl(unitId));
        }
    }
}