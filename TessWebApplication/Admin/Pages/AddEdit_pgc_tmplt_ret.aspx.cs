#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages {
    public partial class AddEdit_pgc_tmplt_ret : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if (IsPostBack == false) {
                BindProjectGroupDrowDown();
                if (PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
            }
        }
        void BindProjectGroupDrowDown()
        {
            var list = project_group.GetProjectGroupDropdownList();
            drpProjectGroupId.DataSource = list;
            drpProjectGroupId.DataBind();
        }
        void SetupEditForm()
        {
            var r = pgc_tmplt_ret.GetRet(RecID);
            drpProjectGroupId.SelectedValue =  r.project_group_id.ToString();
            txtCalcField.Text  = r.calc_field;
            txtCalcType.Text   = r.calc_type;
            txtBasisTable.Text = r.basis_table;
            txtBasisField.Text = r.basis_field;
            txtCalcValue.Text  = r.calc_value.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Calc Value Must be in 1-9.#### format."; }
        }
        bool Save()
        {
            var r = new pgc_tmplt_ret {
                pgc_tmplt_ret_id = RecID,
                project_group_id = int.Parse(drpProjectGroupId.SelectedValue),
                calc_field       = txtCalcField.Text,
                calc_type        = txtCalcType.Text.NullIfEmpty<string>(),
                basis_table      = txtBasisTable.Text.NullIfEmpty<string>(),
                basis_field      = txtBasisField.Text.NullIfEmpty<string>(),
                calc_value       = txtCalcValue.Text.NullIfEmpty<decimal?>()
            };
            return pgc_tmplt_ret.Save(r);
        }
        #region Util
        // Call the method script on the parent form.
        // Parent form method will close the modal and
        // will refresh the parent page.
        void RegisterThickBoxCloseScript()
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.RulesSetup_Updated();", true);
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
    }
}