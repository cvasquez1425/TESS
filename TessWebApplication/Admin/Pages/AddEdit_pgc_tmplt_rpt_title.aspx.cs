#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages {
    public partial class AddEdit_pgc_tmplt_rpt_title : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                BindProjectGroupDrowDown();
                if(PageMode == PageModeEnum.Edit && RecID > 0) {
                    SetupEditForm();
                }
            }
        }
        void BindProjectGroupDrowDown() {
            var list = project_group.GetProjectGroupDropdownList();
            drpProjectGroupId.DataSource = list;
            drpProjectGroupId.DataBind();
        }
        void SetupEditForm() {
            var c = pgc_tmplt_rpt_title.GetRptTitle(RecID);
            drpProjectGroupId.SelectedValue = c.project_group_id.ToString();
            txtCalcField.Text               = c.calc_field;
            txtCriteriaStartAmount.Text     = c.criteria_start_amount.ToString();
            txtCriteriaEndAmount.Text       = c.criteria_end_amount.ToString();
            txtFieldType.Text               = c.field_type;
            txtCalcValue.Text               = c.calc_value.ToString();
            txtFlatValue.Text               = c.flat_value.ToString();
            chkUseBase.Checked              = c.use_base ?? false;
            txtCalcBasis.Text               = c.calc_basis;
        }
        protected void btnSave_Click(object sender, EventArgs e) {
            if(Save() == true) {
                RegisterThickBoxCloseScript();
            }
            else { lblMsg.Text = "Failed"; }
        }
        bool Save() {
            var c = new pgc_tmplt_rpt_title {
                pgc_tmplt_rpt_title_id = RecID,
                project_group_id       = int.Parse(drpProjectGroupId.SelectedValue),
                calc_field	           = txtCalcField.Text.NullIfEmpty<string>(),
                criteria_start_amount  = txtCriteriaStartAmount.Text.NullIfEmpty<int?>(),
                criteria_end_amount	   = txtCriteriaEndAmount.Text.NullIfEmpty<int?>(),
                field_type	           = txtFieldType.Text.NullIfEmpty<string>(),
                calc_value	           = txtCalcValue.Text.NullIfEmpty<decimal?>(),
                flat_value	           = txtFlatValue.Text.NullIfEmpty<decimal?>(),
                use_base	           = chkUseBase.Checked,
                calc_basis	           = txtCalcBasis.Text.NullIfEmpty<string>(),
            };
            var result = pgc_tmplt_rpt_title.Save(c);
            return result;
        }
        #region Util
        // Call the method script on the parent form.
        // Parent form method will close the modal and
        // will refresh the parent page.
        void RegisterThickBoxCloseScript() {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.RulesSetup_Updated();", true);
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
    }  // end of class
}