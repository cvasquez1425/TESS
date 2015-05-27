#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEdit_pgc_tmplt_premium : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set up page mode and ID.
            SetPageBase();
            if (IsPostBack == false) {
                BindProjectGroupDrowDown();
                if (PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
            }
        }

        void SetupEditForm()
        {
            var p = pgc_tmplt_premium.GetPremium(RecID);
            drpProjectGroupId.SelectedValue = p.project_group_id.ToString();
            txtCalcBasisField.Text          = p.calc_basis_field;
            txtBasisTable.Text              = p.calc_basis_table;
            txtBasisFieldMultiplier.Text    = p.basis_field_multiplier;
            txtCriteriaStartAmount.Text     = p.criteria_start_amount;
            txtCriteriaEndAmount.Text       = p.criteria_end_amount;
            txtCalcType.Text                = p.calc_type;
            txtCalcExecTable.Text           = p.calc_exec_table;
            txtCalcExecField.Text           = p.calc_exec_field;
            txtFlatAmount.Text              = p.flat_amt.ToString();
        }

        void BindProjectGroupDrowDown()
        {
            var list = project_group.GetProjectGroupDropdownList();
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
            var p = new pgc_tmplt_premium {
                pgc_tmplt_premium_id   = RecID,
                project_group_id       = int.Parse(drpProjectGroupId.SelectedValue),
                calc_basis_field       = txtCalcBasisField.Text.NullIfEmpty<string>(),
                calc_basis_table       = txtBasisTable.Text.NullIfEmpty<string>(),
                basis_field_multiplier = txtBasisFieldMultiplier.Text.NullIfEmpty<string>(),
                criteria_start_amount  = txtCriteriaStartAmount.Text.NullIfEmpty<string>(),
                criteria_end_amount    = txtCriteriaEndAmount.Text.NullIfEmpty<string>(),
                calc_type              = txtCalcType.Text.NullIfEmpty<string>(),
                calc_exec_table        = txtCalcExecTable.Text.NullIfEmpty<string>(),
                calc_exec_field        = txtCalcExecField.Text.NullIfEmpty<string>(),
                flat_amt               = txtFlatAmount.Text.NullIfEmpty<decimal?>()
            };
            var result = pgc_tmplt_premium.Save(p);
            return result;
        }

        #region Util
        void RegisterThickBoxCloseScript()
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.RulesSetup_Updated();", true);
        }

        void SetPageBase()
        {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if (PageMode == PageModeEnum.Edit) {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    } // end of class
}