#region Includes
using System;
using System.Web.UI;
using Greenspoon.Tess.Services;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class FormSetup : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthService.IsUserTessAdmin()) {
                Response.Redirect("~/NoAuth.aspx");
            }
        }

        // Reload the user form to show new data.
        protected void SetupForm_Load(object sender, EventArgs e)
        {
            LoadSetupForm();
        }

        protected void drpForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSetupForm();
        } // end of selected index

        private void LoadSetupForm()
        {
            string value;
            if (drpForms.SelectedIndex > 0) {
                value = drpForms.SelectedValue;
            }
            else { return; }
            // Load the form.
            SelectFormToLoad(value);
        }

        void SelectFormToLoad(string value)
        {
            if (string.IsNullOrEmpty(value)) {
                return;
            }

            AddSetupForm(value);
        }
        /// <summary>
        /// Loads the controls on the form.
        /// </summary>
        /// <param name="s">Name of the user control</param>
        void AddSetupForm(string s)
        {
            string path = string.Format("~/Admin/Controls/{0}.ascx", s);
            var usrContr = 
                    LoadControl(path);
            // If control is found.
            if (usrContr != null) {
                // Load user control on the page.
                plcSetupForm.Controls.Add(usrContr);
            }
        } // end of Load Setup Form.

    }
}