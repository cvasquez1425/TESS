#region Includes
using System;
using System.Web.UI.WebControls;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.DataObjects.Linq;
using Greenspoon.Tess.Services;
using Greenspoon.Tess.Classes;
#endregion

namespace Greenspoon.Tess.Pages
{
    public partial class CancelSearch : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageVariables();
            if (Page.IsPostBack == false) {
                BindAllDropDowns();
                lblBatchCancelID.Text = String.Format("Batch Cancel ID: {0}", BatchCancelId);
                hrefReturnToRecord.HRef = String.Format("~/Pages/BatchCancel.aspx?a=e&bcid={0}", BatchCancelId);
            }
        }
        #region DindDropDowns
        private void BindAllDropDowns()
        {
            BindProjectDropDown();
            BindWeekDropDown();
            BindYearDorpDown();
        }
     
        void BindProjectDropDown()
        {
            var list =
                project.GetProjectList();
            drpProject.DataSource = list;
            drpProject.DataBind();

            if (RecID > 0 && drpProject.Items.FindByValue(RecID.ToString()) != null) {
                drpProject.SelectedValue = RecID.ToString();
            }
        }
        void BindYearDorpDown()
        {
            var list =
                cont_year_master.GetContractYearList();
            drpYears.DataSource = list;
            drpYears.DataBind();
        }
        void BindWeekDropDown()
        {
            var list =
                cont_week_master.GetContractWeekList();
            drpWeeks.DataSource = list;
            drpWeeks.DataBind();
        }

        #endregion
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DoCancelSearch();
        }
        void ClearForm()
        {
            Server.Transfer("~/Pages/CancelSearch.aspx");
        }
        protected void CancelSearchListView_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (!string.Equals(e.CommandName, "AddToCancelBatch")) return;
            var dataItem = (ListViewDataItem)e.Item;
            var contractId =
                lvData.DataKeys[dataItem.DisplayIndex].Value.ToString();

            var result = cancel.AddContractToCancel(BatchCancelId, int.Parse(contractId), UserName);

            lblMsg.Text = result.Item2;
            lblMsg.Visible = true;

            lblMsg.ForeColor = result.Item1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        void DoCancelSearch()
        {
            var ui = new TitleSearchDTO {
                ProjectId     = drpProject.SelectedValue,
                MasterId      = txtMasterId.Text,
                DevK          = txtDevKNo.Text.Trim(),
                AltBldg       = txtAltBldg.Text.Trim(),
                Active        = chkActive.Checked,
                FullName      = txtName.Text.Trim(),
                Zip           = txtZip.Text.Trim(),
                UnitNumber    = txtUnitNumber.Text.Trim(),
                BuildingCode  = txtBuildingCode.Text.Trim(),
                ContWeekId    = drpWeeks.SelectedValue,
                ContYearId    = drpYears.SelectedValue,
                BatchCancelId = BatchCancelId.ToString(),
            };

            lvData.DataSource = TitleSearchServie.DoTitleSearch(ui);
            lvData.DataBind();
        }

        void SetPageVariables()
        {
            BatchCancelId = Request.QueryString.GetValue<int>("bcid");
            RecID = Request.QueryString.GetValue<int>("pid");
        }

        protected bool CanAddToCancel(int? batchCancelId)
        {
            if (batchCancelId.HasValue == false) {
                return true;
            }
            return batchCancelId != BatchCancelId;
        }
    }
}