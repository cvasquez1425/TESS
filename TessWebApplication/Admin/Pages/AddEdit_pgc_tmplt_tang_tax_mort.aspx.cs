#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages {
    public partial class AddEdit_pgc_tmplt_tang_tax_mort : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if (IsPostBack == false) {
                BindProjectGroupDrowDown();
                if (PageMode == PageModeEnum.Edit && RecID > 0) {
                    // Load data based on record id.
                    SetupEditForm();
                }
            }
        }
        void SetupEditForm()
        {
            var t = pgc_tmplt_tang_tax_mort.GetMort(RecID);
            drpProjectGroupId.SelectedValue = t.project_group_id.ToString();
            txtCalcBasisTable.Text          = t.calc_basis_table;
            txtCalcBasisField.Text          = t.calc_basis_field;
            txtProjectFieldType.Text        = t.project_field_type;
            txtCalcMultiplierTable.Text     = t.calc_multiplier_table;
            txtCalcMultiplierField.Text     = t.calc_multiplier_field;
            txtRounding.Text                = t.rounding.ToString();
            txtFlatRate.Text                = t.flat_rate.ToString();
            chkIncludeRoundingCalc.Checked  = t.include_rounding_calc ?? false;
        }
        void BindProjectGroupDrowDown()
        {
            var list = 
                project_group.GetProjectGroupDropdownList();
            drpProjectGroupId.DataSource = list;
            drpProjectGroupId.DataBind();
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
            var t = new pgc_tmplt_tang_tax_mort {
                pgc_tmplt_tang_tax_mort_id = RecID,
                project_group_id           = int.Parse(drpProjectGroupId.SelectedValue.ToString()),
                calc_basis_table           = txtCalcBasisTable.Text.NullIfEmpty<string>(),
                calc_basis_field           = txtCalcBasisField.Text.NullIfEmpty<string>(),
                project_field_type         = txtProjectFieldType.Text.NullIfEmpty<string>(),
                calc_multiplier_table      = txtCalcMultiplierTable.Text.NullIfEmpty<string>(),
                calc_multiplier_field      = txtCalcMultiplierField.Text.NullIfEmpty<string>(),
                rounding                   = txtRounding.Text.NullIfEmpty<decimal?>(),
                flat_rate                  = txtFlatRate.Text.NullIfEmpty<decimal?>(),
                include_rounding_calc      = chkIncludeRoundingCalc.Checked
            };
            return pgc_tmplt_tang_tax_mort.Save(t);
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