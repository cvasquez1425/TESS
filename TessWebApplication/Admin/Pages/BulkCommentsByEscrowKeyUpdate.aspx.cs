using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using System.Data;
using Greenspoon.Tess.BusinessObjects.UIObjects;

namespace Greenspoon.Tess.Admin.Pages
{
    /// <summary>
    /// /Admin/Pages/BulkCommentsByEscrowKeyUpdate.aspx
    /// Status Code screen [4] for Escrow Keys with individual comments?  
    /// </summary>
    public partial class BulkCommentsByEscrowKeyUpdate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) { return; }
            if (!Page.IsPostBack)
            {
                BindStatusMasterDropDown();
                BindCountyDropDown();
                drpStatusMaster.Focus();
            }
        }

        void BindCountyDropDown()
        {
            var list = county.GetCountyList();
            drpOriginalCounty.DataSource = list;
            drpOriginalCounty.DataBind();
        }

        void BindStatusMasterDropDown()
        {
            var list = status_master.GetStatusMasterList();
            drpStatusMaster.DataSource = list;
            drpStatusMaster.DataBind();
        }

        protected void btnAddEscComments_Click(object sender, EventArgs e)
        {
            SetInitialRow();
            SetDisplayButtons();
            gvEscInfo.Focus();
        }

        private void SetDisplayButtons()
        {
            btnClear.Enabled = true;
            btnUpdate.Enabled = true;

        }

        void ClearData()
        {
            drpStatusMaster.Text = string.Empty;
            drpOriginalCounty.Text = string.Empty;
            txtEffectiveDate.Text = string.Empty;

            gvEscInfo.DataSource = null;
            gvEscInfo.DataBind();
            btnClear.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void SetInitialRow()
        {
            // Create a new DataTable
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("EscrowKey", typeof(string)));
            dt.Columns.Add(new DataColumn("Comment", typeof(string)));

            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dr["EscrowKey"] = string.Empty;
                dr["Comment"] = string.Empty;
                dt.Rows.Add(dr);
            }

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gvEscInfo.DataSource = dt;
            gvEscInfo.DataBind();
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            //int rowFocusIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        // Extract the TextBox values
                        TextBox box1 = (TextBox)gvEscInfo.Rows[rowIndex].Cells[0].FindControl("txtEscrowKey");
                        TextBox box2 = (TextBox)gvEscInfo.Rows[rowIndex].Cells[1].FindControl("txtComments");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["EscrowKey"] = box1.Text;
                        drCurrentRow["Comment"] = box2.Text;

                        rowIndex++;
                    }

                    // Add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);

                    //rowFocusIndex = dtCurrentTable.Rows.Count;
                    //gvEscInfo.Rows[rowIndex].Cells[0].Focus();

                    // Store the current data to ViewState
                    ViewState["CurrentTable"] = dtCurrentTable;

                    // Rebind the Grid with the current data
                    gvEscInfo.DataSource = dtCurrentTable;
                    gvEscInfo.DataBind();
                }
            }

            // Set Previous Data on PostBacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        // Extract the TextBox SetPreviousData
                        TextBox box1 = (TextBox)gvEscInfo.Rows[rowIndex].Cells[0].FindControl("txtEscrowKey");
                        TextBox box2 = (TextBox)gvEscInfo.Rows[rowIndex].Cells[1].FindControl("txtComments");

                        box1.Text = dt.Rows[i]["EscrowKey"].ToString();
                        box2.Text = dt.Rows[i]["Comment"].ToString();

                        //gvEscInfo.Rows[rowIndex].Cells[0].Focus();

                        rowIndex++;

                    }
                    gvEscInfo.Rows[rowIndex].Cells[0].Focus();
                }
            }
        }

        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            AddNewRowToGrid();
            //gvEscInfo.Focus();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            SetInitialRow();
            drpStatusMaster.Focus();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var ui = BindUIData();
            var result = contract.SaveCommentsInfo(ui);
            string msg = result ? "All records updated." : "ERROR: Could not update record.";
            lblMsg.Text = msg;
        }

        IList<UpdateCommentByEscrowKeyDTO> BindUIData()
        {
            var ui = new List<UpdateCommentByEscrowKeyDTO>();
            foreach (GridViewRow row in gvEscInfo.Rows)
            {
                int EscrowKey = int.Parse((row.FindControl("txtEscrowKey") as TextBox).Text);
                int fullCount = batch_escrow.EscrowKeyValidationByEscrowId(EscrowKey);

                if (fullCount >= 1)
                {
                    var uiContract = batch_escrow.ListContractIdByEscrowId(EscrowKey);
                    foreach (var c in uiContract)
                    {
                        ui.Add(new UpdateCommentByEscrowKeyDTO
                        {
                            BatchEscrowId  = c.BatchEscrowId.ToString(),
                            MasterId       = c.MasterId.ToString(),
                            StatusMasterId = drpStatusMaster.SelectedValue,
                            Comments       = (row.FindControl("txtComments") as TextBox).Text,
                            EffectiveDate  = txtEffectiveDate.Text,
                            CreatedBy      = UserName,
                            County         = drpOriginalCounty.SelectedValue
                        });                        
                    }
                }
            }
            return ui;
        }

    }
}