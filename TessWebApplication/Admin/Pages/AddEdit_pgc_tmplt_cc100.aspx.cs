#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEdit_pgc_tmplt_cc100 : PageBase
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save()
        {
            var c = new pgc_tmplt_cc100 {
                pgc_tmplt_cc100_id      = RecID,
                criteria_basis_table	= txtCriteriaBasisTable.Text.NullIfEmpty<string>(),
                criteria_basis_field	= txtCriteriaBasisField.Text.NullIfEmpty<string>(),
                criteria_start_amount	= txtCriteriaStartAmount.Text.NullIfEmpty<decimal?>(),
                criteria_end_amount	    = txtCriteriaEndAmount.Text.NullIfEmpty<decimal?>(),
                include	                = txtInclude.Text.NullIfEmpty<string>(),
                percentage	            = txtPercentage.Text.NullIfEmpty<double?>(),
                flat_rate	            = txtFlatRate.Text.NullIfEmpty<decimal?>(),
                calc_basis_table	    = txtCalcBasisTable.Text,
                calc_basis_field	    = txtCalcBasisField.Text,
                rounding	            = Decimal.Parse(txtRounding.Text),
                project_group_id	    = int.Parse(drpProjectGroupId.SelectedValue),
                include_rounding_calc	= chkIncludeRoundingCalc.Checked, 
                effective_date_from     = txtEffectiveDateFrom.Text.NullIfEmpty<DateTime?>(),
                effective_date_to       = txtEffectiveDateTo.Text.NullIfEmpty<DateTime?>(), 
            };
            var result = pgc_tmplt_cc100.Save(c);
            return result;
        }
        void SetupEditForm()
        {
            var c = pgc_tmplt_cc100.GetCC100(RecID);
            txtCriteriaBasisTable.Text      = c.criteria_basis_table;
            txtCriteriaBasisField.Text      = c.criteria_basis_field;
            txtCriteriaStartAmount.Text     = c.criteria_start_amount.ToString();
            txtCriteriaEndAmount.Text       = c.criteria_end_amount.ToString();
            txtInclude.Text                 = c.include;
            txtPercentage.Text              = c.percentage.ToString();
            txtFlatRate.Text                = c.flat_rate.ToString();
            txtCalcBasisTable.Text          = c.calc_basis_table;
            txtCalcBasisField.Text          = c.calc_basis_field;
            txtRounding.Text                = c.rounding.ToString();
            drpProjectGroupId.SelectedValue = c.project_group_id.ToString();
            chkIncludeRoundingCalc.Checked  = c.include_rounding_calc ?? false;
            txtEffectiveDateFrom.Text       = c.effective_date_from.ToDateOnly();
            txtEffectiveDateTo.Text         = c.effective_date_to.ToDateOnly();
        }

        void BindProjectGroupDrowDown()
        {
            var list = project_group.GetProjectGroupDropdownList();
            drpProjectGroupId.DataSource = list;
            drpProjectGroupId.DataBind();
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
    }   
}