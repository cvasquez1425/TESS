using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.BusinessObjects.UIObjects;

namespace Greenspoon.Tess.Pages
{
    public partial class CancelExtra : PageBase
    {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(Page.IsPostBack == false) {
                // Set page based on page mode.
                BindPage();
            }
        }

        void BindPage() {
            BindExtraTypeDropDown();
            if((PageMode == PageModeEnum.Edit) && (RecID > 0)) {
                var ui = cancel_extra.GetCancelExtraUI(RecID);
                drpExtraType.SelectedValue = ui.CancelExtraTypeId;
                txtNames.Text              = ui.Names;
                txtPages.Text              = ui.Pages;
                lblCancelId.Text           = ui.CancelId;
            }
            else {
                lblCancelId.Text           = CancelId.ToString();
            }
        }

        void BindExtraTypeDropDown() {
            drpExtraType.DataSource = cancel_extra_type.GetCancelExtraTypeList();
            drpExtraType.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e) {
            Save();
        }

        void Save() {
            var ui = new CancelExtraDTO
                         {
                             CancelId = lblCancelId.Text,
                             CancelExtraTypeId = drpExtraType.SelectedValue,
                             Names = txtNames.Text,
                             Pages = txtPages.Text,
                             CancelExtraId = RecID.ToString(),
                             CreatedBy = UserName
                         };

            if(cancel_extra.Save(ui)) {
                CloseModalWindow();
            }
            else {
                lblMsg.Text = "Failed to save data.";
            }
        }

        private void CloseModalWindow() {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.CancelExtra_Update();", true);
        }

        void SetPageBase() {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if(PageMode == PageModeEnum.New) {
                CancelId = Request.QueryString.GetValue<int>("id");
            }
            else {
                RecID    = Request.QueryString.GetValue<int>("id");
                FormName = Request.QueryString.GetValue<FormNameEnum>("form");
            }
        }
    }
}