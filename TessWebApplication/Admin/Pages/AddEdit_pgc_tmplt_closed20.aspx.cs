#region Includes
using System;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
#endregion

namespace Greenspoon.Tess.Admin.Pages {
    public partial class AddEdit_pgc_tmplt_closed20 : PageBase {
        protected void Page_Load(object sender, EventArgs e) {
            SetPageBase();
            if(IsPostBack == false) {
                BindProjectGroupDrowDown();
                if(PageMode == PageModeEnum.Edit && RecID > 0) {
                    // Load data based on record id.
                    SetupEditForm();
                }
            }
        }

        void SetupEditForm() {
            var c = 
                pgc_tmplt_closed20.GetClosed20(RecID);
            drpProjectGroupId.SelectedValue =  c.project_group_id.ToString();
            txtCriteriaBasisTable.Text      =  c.criteria_basis_table;
            txtCriteriaBasisField.Text      =  c.criteria_basis_field;
            txtCriteriaStartAmount.Text     =  c.criteria_start_amount.ToString();
            txtCriteriaEndAmount.Text       =  c.criteria_end_amount.ToString();
            txtInclude.Text                 =  c.include;
            txtCalcBasisTable.Text          =  c.calc_basis_table;
            txtCalcBasisField.Text          =  c.calc_basis_field;
            txtProjectFieldType.Text        =  c.project_field_type;
            txtFlatAmt.Text                 =  c.flat_amt.ToString();
        }
        void BindProjectGroupDrowDown() {
            var list = 
                project_group.GetProjectGroupDropdownList();
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
            var c = new pgc_tmplt_closed20 {
                pgc_tmplt_closed20_id  = RecID,
                project_group_id       = int.Parse(drpProjectGroupId.SelectedValue),
                criteria_basis_table   = txtCriteriaBasisTable.Text.NullIfEmpty<string>(),
                criteria_basis_field   = txtCriteriaBasisField.Text.NullIfEmpty<string>(),
                criteria_start_amount  = txtCriteriaStartAmount.Text.NullIfEmpty<decimal?>(),
                criteria_end_amount    = txtCriteriaEndAmount.Text.NullIfEmpty<decimal?>(),
                include                = txtInclude.Text.NullIfEmpty<string>(),
                calc_basis_table       = txtCalcBasisTable.Text.NullIfEmpty<string>(),
                calc_basis_field       = txtCalcBasisField.Text.NullIfEmpty<string>(),
                project_field_type     = txtProjectFieldType.Text.NullIfEmpty<string>(),
                flat_amt               = txtFlatAmt.Text.NullIfEmpty<decimal?>()
            };
            return pgc_tmplt_closed20.Save(c);
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
    }    // end of class
}