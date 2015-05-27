#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEditTitleCompany : PageBase
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

        void SetupEditForm()
        {
            var tc = title_company.GetTitleCompany(RecID);
            if (tc == null) return;
            txtPolPrefix.Text            = tc.pol_prefix;
            txtTitleCompanyName.Text     = tc.title_company_name;
            chkActive.Checked            = tc.title_company_active;
            lblCreateBy.Text             = tc.createdby;
            lblCreateDate.Text           = tc.createddate.ToDateOnly();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save()
        {
            var tc = new title_company {
                title_company_id        = RecID,
                title_company_name      = txtTitleCompanyName.Text,
                pol_prefix              = txtPolPrefix.Text,
                title_company_active    = chkActive.Checked,
                createdby               = lblCreateBy.Text,
                createddate             = PageMode == PageModeEnum.Edit 
                                            ? ( lblCreateDate.Text.Length > 0 ) 
                                              ? DateTime.Parse(lblCreateDate.Text) 
                                              : (DateTime?)null
                                            : DateTime.Now
            };

            var result = title_company.Save(tc);
            return result;
        }

        #region Util
        // Call the method script on the parent form.
        // Parent form method will close the modal and
        // will refresh the parent page.
        void RegisterThickBoxCloseScript()
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.FormSetup_Updated();", true);
        }
        /// <summary>
        /// Read the query string and set up page values.
        /// GetValue is an extension method.
        /// </summary>
        void SetPageBase()
        {
            base.PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if (base.PageMode == PageModeEnum.Edit) {
                base.RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    } // end of class
}