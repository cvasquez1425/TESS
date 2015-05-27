#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages {
    public partial class AddEdit_pgc_tmplt_stamp_sn : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                BindProjectGroupDrowDown();
                if(PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
            }
        }

        void SetupEditForm() {
            var s = pgc_tmplt_stamp_sn.GetStampSN(RecID);
            drpProjectGroupId.SelectedValue = s.project_group_id.ToString();
            txtFlatRate.Text                = s.flat_rate.ToString();
            txtCalcMultiplierTable.Text     = s.calc_multiplier_table;
            txtCalcMultiplierField.Text     = s.calc_multiplier_field;
            txtFieldType1.Text              = s.field_type1;
            txtCalcExecTable1.Text          = s.calc_exec_table1;
            txtCalcExecField1.Text          = s.calc_exec_field1;
            txtCalcExecDivisor1.Text        = s.calc_exec_divisor1.ToString();
            txtCalcExecRounding1.Text       = s.calc_exec_rounding1.ToString();
        }

        void BindProjectGroupDrowDown() {
            var list = project_group.GetProjectGroupDropdownList();
            drpProjectGroupId.DataSource = list;
            drpProjectGroupId.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }

        bool Save() {
            var s = new pgc_tmplt_stamp_sn {
                pgc_tmplt_stamp_sn_id = RecID,
                project_group_id       = int.Parse(drpProjectGroupId.SelectedValue),
                flat_rate              = txtFlatRate.Text.NullIfEmpty<decimal?>(),
                calc_multiplier_table  = txtCalcMultiplierTable.Text.NullIfEmpty<string>(),
                calc_multiplier_field  = txtCalcMultiplierField.Text.NullIfEmpty<string>(),
                field_type1            = txtFieldType1.Text.NullIfEmpty<string>(),
                calc_exec_table1       = txtCalcExecTable1.Text.NullIfEmpty<string>(),
                calc_exec_field1       = txtCalcExecField1.Text.NullIfEmpty<string>(),
                calc_exec_divisor1     = txtCalcExecDivisor1.Text.NullIfEmpty<int?>(),
                calc_exec_rounding1    = txtCalcExecRounding1.Text.NullIfEmpty<int?>()
            };
            var result = pgc_tmplt_stamp_sn.Save(s);
            return result;
        }

        #region Util
      
        void RegisterThickBoxCloseScript() {
            Page.ClientScript.RegisterStartupScript(GetType(), "closeThickBox", "self.parent.RulesSetup_Updated();", true);
        }
      
        void SetPageBase() {
            PageMode = Request.QueryString.GetValue<PageModeEnum>("a");
            if(PageMode == PageModeEnum.Edit) {
                RecID = Request.QueryString.GetValue<int>("id");
            }
        }
        #endregion
    }  // end of class
}