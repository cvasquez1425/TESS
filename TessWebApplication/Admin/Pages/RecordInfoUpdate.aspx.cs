using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using System.Text;

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class RecordInfoUpdate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdate.Attributes.Add("onClick", "return blkScanConfirm();");
            if (Page.IsPostBack) { return; }
            SetPageVariables();
            if (EscrowId == 0) { return; }
            txtEscrowKey.Text = EscrowId.ToString();
            SetUpPage();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var ui = BindUIData();
            var result = contract.SaveRecordingInfo(ui);
            string msg = result ?  "All records updated." : "ERROR: Could not update record.";
            lblMsg.Text = msg;
        }

        IList<RecordingInfoDTO> BindUIData()
        {
            var ui = new List<RecordingInfoDTO>();
            foreach (GridViewRow  row in gvRecInfo.Rows) {
                ui.Add(new RecordingInfoDTO {
                    RecordingDate = txtRecordDate.Text,
                    //DeedBook      = txtDeedBook.Text,                                      // Work Order Number: 27716 - Escrow Batch Recording Info Update Screen Revision/addition
                    //MortgageBook  = txtMortgageBook.Text,                              // Bonni Approved December 2014
                    MasterId = ( row.FindControl("lblMasterId") as Label ).Text,
                    DeedPage = ( row.FindControl("txtDeedPage") as TextBox ).Text,
                    MtgPage  = ( row.FindControl("txtMortPage") as TextBox ).Text,
                    MortgageBook = (row.FindControl("txtMortBook") as TextBox).Text,    // Bonni Approved December 2014
                    DeedBook = (row.FindControl("txtBook") as TextBox).Text             // Work Order Number: 27716
                });
            }
            return ui;
        }

        protected void btnSearchBC_Click(object sender, EventArgs e)
        {
            SetUpPage();
        }

        private void SetUpPage()
        {
            ClearData();
            var data = contract.GetRecordingInfo(Convert.ToInt32(txtEscrowKey.Text));

            if (!data.Any()) {
                return;
            }

            SetUpPageFields(data);
        }

        void SetUpPageFields(IList<RecordingInfoDTO> data)
        {
            txtRecordDate.Text = data.Select(d => d.RecordingDate).FirstOrDefault();
            txtDeedBook.Text = data.Select(d => d.DeedBook).FirstOrDefault();
            txtMortgageBook.Text = data.Select(d => d.MortgageBook).FirstOrDefault();

            gvRecInfo.DataSource = data;
            gvRecInfo.DataBind();
            btnUpdate.Enabled = true;
            txtRecordDate.Focus();
        }

        void ClearData()
        {
            txtRecordDate.Text = string.Empty;
            txtDeedBook.Text = string.Empty;
            txtMortgageBook.Text = string.Empty;

            gvRecInfo.DataSource = null;
            gvRecInfo.DataBind();
            btnUpdate.Enabled = false;
        }

        protected string GetDisplay(string[] arg)
        {
            if (arg.Length == 0) { return string.Empty; }
            if (arg.Length == 1) { return arg.First(); }

            var sb = new StringBuilder();
            sb.Append(arg.First());
            sb.Append(@"<div style=""float:right; text-align: left;"" class=""arrow-down"" runat=""server"" id=""master""></div>");
            sb.Append(@"<span>");

            foreach (var s in arg.Skip(1)) {
                sb.Append(string.Format("{0}<br/>", s));
            }

            sb.Append(@"</span>");
            return sb.ToString();
        }

        void SetPageVariables()
        {
            EscrowId = Request.QueryString.GetValue<int>("beid");
        }

        // Bulk Recording Info to disable tabbing and focuses on Book Information for Las Vegas
        protected void gvRecInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int vegasID = batch_escrow.GetLasVegasProject(Convert.ToInt32(txtEscrowKey.Text));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (vegasID == 66 || vegasID == 114 || vegasID == 116 || vegasID == 118)
                {
                    //((TextBox)e.Row.FindControl("txtMortBook") as TextBox).Enabled = false;
                    //((TextBox)e.Row.FindControl("txtBook") as TextBox).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtMortBook") as TextBox).TabIndex = -1;
                    ((TextBox)e.Row.FindControl("txtBook") as TextBox).TabIndex = -1;
                }
                else
                {
                    ((TextBox)e.Row.FindControl("txtMortBook") as TextBox).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtBook") as TextBox).Enabled = true;
                }
            }
        }
    }
}