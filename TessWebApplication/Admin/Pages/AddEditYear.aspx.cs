using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditYear : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (IsPostBack == false) {
                if (PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    lblCreateBy.Text   = UserName;
                    lblCreateDate.Text = DateTime.Now.ToDateOnly();
                }
            }
        }

        private void SetupEditForm()
        {
            var year = cont_year_master.GetYear(RecID);
            if (year == null) return;
            txtYearName.Text = year.year_name;
            txtABT.Text = year.a_b_t;
            txtNameAbbrev.Text = year.year_name_abbrev;
            chkActive.Checked = year.year_active;
            lblCreateBy.Text = year.createdby;
            lblCreateDate.Text = year.createddate.ToDateOnly();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save()) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        private bool Save()
        {
            var year = new cont_year_master {
                cont_year_master_id = RecID,
                a_b_t = txtABT.Text.NullIfEmpty<string>(),
                year_name = txtYearName.Text,
                year_active = chkActive.Checked,
                year_name_abbrev = txtNameAbbrev.Text.NullIfEmpty<string>(),
                createdby = lblCreateBy.Text.NullIfEmpty<string>(),
            };
            return cont_year_master.Save(year);
        }
        #region Util
        
        void RegisterThickBoxCloseScript()
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
        }

        void SetPageBase()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if (PageMode == PageModeEnum.Edit) {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    }
}