#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditVesting : PageBase
    {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                if(PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
                else {
                    lblCreateBy.Text   = UserName;
                    lblCreateDate.Text = DateTime.Now.ToDateOnly();
                }
            }
        }

        void SetupEditForm() {
            var v = vesting.GetVesting(RecID);
            if (v == null) return;
            txtVestingType.Text = v.vesting_type;
            chkActive.Checked   = v.vesting_active;
            lblCreateBy.Text    = v.createdby;
            lblCreateDate.Text  = v.createddate.ToDateOnly();
        } 

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid != true) return;
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save() {
            var v = new vesting
            {
                vesting_id      = RecID,
                vesting_type    = txtVestingType.Text,
                vesting_active  = chkActive.Checked,
                createdby       = UserName

            };
            var result = vesting.Save(v);
            return result;
        }
        #region Util
        // Call the method script on the parent form.
        // Parent form method will close the modal and
        // will refresh the parent page.
        void RegisterThickBoxCloseScript() {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
        }
        /// <summary>
        /// Read the query string and set up page values.
        /// GetValue is an extension method.
        /// </summary>
        void SetPageBase() {
            base.PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if(base.PageMode == PageModeEnum.Edit) {
                base.RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    } // end of class
}