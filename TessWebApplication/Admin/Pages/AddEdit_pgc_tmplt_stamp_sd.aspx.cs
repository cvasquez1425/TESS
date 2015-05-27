#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class AddEdit_pgc_tmplt_stamp_sd : PageBase
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
        private void BindProjectGroupDrowDown()
        {
            var list = project_group.GetProjectGroupDropdownList();
            drpProjectGroupId.DataSource = list;
            drpProjectGroupId.DataBind();
        }
        private void SetupEditForm()
        {
            var s = pgc_tmplt_stamp_sd.GetStampSD(RecID);
            drpProjectGroupId.SelectedValue = s.project_group_id.ToString();
            txtFlatRate.Text                = s.flat_rate.ToString();
            txtCalcMultiplierTable.Text     = s.calc_multiplier_table;
            txtCalcMultiplierField.Text     = s.calc_multiplier_field;
            txtFieldType1.Text              = s.field_type1;
            txtCalcExecTable1.Text          = s.calc_exec_table1;
            txtCalcExecField1.Text          = s.calc_exec_field1;
            txtCalcExecDivisor1.Text        = s.calc_exec_divisor1.ToString();
            txtCalcExecRounding1.Text       = s.calc_exec_rounding1.ToString();
            txtCalcExecTable2.Text          = s.calc_exec_table2;
            txtCalcExecField2.Text          = s.calc_exec_field2;
            txtCalcExecDivisor2.Text        = s.calc_exec_divisor2.ToString();
            txtCalcExecRounding2.Text       = s.calc_exec_rounding2.ToString();
            txtCalcExecTable3.Text          = s.calc_exec_table3;
            txtCalcExecField3.Text          = s.calc_exec_field3;
            txtCalcExecDivisor3.Text        = s.calc_exec_divisor3.ToString();
            txtCalcExecRounding3.Text       = s.calc_exec_rounding3.ToString();
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
            var s = new pgc_tmplt_stamp_sd {
                pgc_tmplt_stamp_sd_id = RecID,
                project_group_id       = int.Parse(drpProjectGroupId.SelectedValue),
                flat_rate              = txtFlatRate.Text.NullIfEmpty<decimal?>(),
                calc_multiplier_table  = txtCalcMultiplierTable.Text.NullIfEmpty<string>(),
                calc_multiplier_field  = txtCalcMultiplierField.Text.NullIfEmpty<string>(),
                field_type1            = txtFieldType1.Text.NullIfEmpty<string>(),
                calc_exec_table1       = txtCalcExecTable1.Text.NullIfEmpty<string>(),
                calc_exec_field1       = txtCalcExecField1.Text.NullIfEmpty<string>(),
                calc_exec_divisor1     = txtCalcExecDivisor1.Text.NullIfEmpty<int?>(),
                calc_exec_rounding1    = txtCalcExecRounding1.Text.NullIfEmpty<int?>(),
                calc_exec_table2       = txtCalcExecTable2.Text.NullIfEmpty<string>(),
                calc_exec_field2       = txtCalcExecField2.Text.NullIfEmpty<string>(),
                calc_exec_divisor2     = txtCalcExecDivisor2.Text.NullIfEmpty<int?>(),
                calc_exec_rounding2    = txtCalcExecRounding2.Text.NullIfEmpty<int?>(),
                calc_exec_table3       = txtCalcExecTable3.Text.NullIfEmpty<string>(),
                calc_exec_field3       = txtCalcExecField3.Text.NullIfEmpty<string>(),
                calc_exec_divisor3     = txtCalcExecDivisor3.Text.NullIfEmpty<int?>(),
                calc_exec_rounding3    = txtCalcExecRounding3.Text.NullIfEmpty<int?>()
            };
            var result = pgc_tmplt_stamp_sd.Save(s);
            return result;
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