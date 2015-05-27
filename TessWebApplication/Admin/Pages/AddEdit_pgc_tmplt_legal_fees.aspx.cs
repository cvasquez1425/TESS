#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEdit_pgc_tmplt_legal_fees : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageBase();
            if (IsPostBack == false) {
                BindProjectGroupDrowDown();
                if (PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
            }
        }
        private void SetupEditForm()
        {
            var l = pgc_tmplt_legal_fees.GetLegalFee(RecID);
            if (l == null) {
                return;
            }
            drpProjectGroupId.SelectedValue = l.project_group_id.ToString();
            txtCalcField.Text               = l.calc_field;
            txtCalcBasisTable.Text          = l.calc_basis_table;
            txtCalcBasisField.Text          = l.calc_basis_field;
            txtProjectFieldType.Text        = l.project_field_type;
            txtCalcMultiplierTable.Text     = l.calc_multiplier_table;
            txtCalcMultiplierField.Text     = l.calc_multiplier_field;
            txtRounding.Text                = l.rounding.ToString();
            chkIncludeRoundingCalc.Checked  = l.include_rounding_calc ?? false;
        }
        private void BindProjectGroupDrowDown()
        {
            var list = project_group.GetProjectGroupDropdownList();
            drpProjectGroupId.DataSource = list;
            drpProjectGroupId.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save() == true) {
                // Close the modal window.
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }
        private bool Save()
        {
            var l = new pgc_tmplt_legal_fees {
                pgc_tmplt_legal_fees_id = RecID,
                project_group_id        = int.Parse(drpProjectGroupId.SelectedValue),
                calc_field              = txtCalcField.Text,
                calc_basis_table        = txtCalcBasisTable.Text.NullIfEmpty<string>(),
                calc_basis_field        = txtCalcBasisField.Text.NullIfEmpty<string>(),
                project_field_type      = txtProjectFieldType.Text,
                calc_multiplier_table   = txtCalcMultiplierTable.Text.NullIfEmpty<string>(),
                calc_multiplier_field   = txtCalcMultiplierField.Text.NullIfEmpty<string>(),
                rounding                = txtRounding.Text.NullIfEmpty<decimal?>(),
                include_rounding_calc   = chkIncludeRoundingCalc.Checked
            };
            return pgc_tmplt_legal_fees.Save(l);
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