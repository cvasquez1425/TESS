using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Greenspoon.Tess.BusinessObjects.UIObjects;
using Greenspoon.Tess.Classes;
using Greenspoon.Tess.DataObjects.Linq;
using System.Text;
using System.Web.UI;

namespace Greenspoon.Tess.Admin.Pages
{
    public partial class RecordPolicyUpload : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdate.Attributes.Add("onClick", "return blkScanConfirm();");

            // Set Focus to Any Control After PostBack
            //int currentTabIndex = 1;
            WebControl postBackCtrl = (WebControl)GetControlThatCausedPostBack(Page);
            Control ctrl2 = txtDeedBook;
            Control ctrl3 = txtMortgageBook;
            Control ctrl4 = txtMortgageBook;

            foreach (WebControl ctrl in Panel1.Controls.OfType<WebControl>())
            {
                if (postBackCtrl != null)
                {
                    if (ctrl.ClientID == postBackCtrl.ClientID && ctrl.ClientID == txtRecordDate.ClientID)
                        ctrl2.Focus();
                    if (ctrl.ClientID == postBackCtrl.ClientID && ctrl.ClientID == txtDeedBook.ClientID)
                        ctrl3.Focus();
                    if (ctrl.ClientID == postBackCtrl.ClientID && ctrl.ClientID == txtMortgageBook.ClientID)
                        ctrl4.Focus();
                }
            }

            if (Page.IsPostBack) 
            {
                return; 
            }
            SetPageVariables();
            if (EscrowId == 0) { return; }
            txtEscrowKey.Text = EscrowId.ToString();
            SetUpPage();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var ui = BindUIData();
            var result = contract.SaveRecordPolicyInfo(ui);
            string msg = result ? "All records updated." : "ERROR: Could not update record.";
            lblMsg.Text = msg;
        }

        IList<RecordingInfoDTO> BindUIData()
        {
            var ui = new List<RecordingInfoDTO>();
            foreach (GridViewRow row in gvRecInfo.Rows)
            {
                ui.Add(new RecordingInfoDTO
                {
                    // TESS Enhancement for 2015
                    //RecordingDate = txtRecordDate.Text,
                    RecordingDate = (row.FindControl("txtRecordingDate") as TextBox).Text,
                    //DeedBook = txtDeedBook.Text,
                    DeedBook = (row.FindControl("txtBook") as TextBox).Text,
                    //MortgageBook = txtMortgageBook.Text,
                    MortgageBook = (row.FindControl("txtMortBook") as TextBox).Text,    
                    MasterId = (row.FindControl("lblMasterId") as Label).Text,
                    DeedPage = (row.FindControl("txtDeedPage") as TextBox).Text,
                    MtgPage = (row.FindControl("txtMortPage") as TextBox).Text,
                    // Myrtle Beach Bulk Upload
                    OwnerPolicy = (row.FindControl("txtOwnerPol") as TextBox).Text,  
                    LenderPolicy = (row.FindControl("txtLenderPol") as TextBox).Text ,
                    CreatedBy = UserName,
                    MortRecordingDate = (row.FindControl("txtMortRecordingDate") as TextBox).Text                    // Myrtle Beach Web Form 2015
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

            if (!data.Any())
            {
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

            foreach (var s in arg.Skip(1))
            {
                sb.Append(string.Format("{0}<br/>", s));
            }

            sb.Append(@"</span>");
            return sb.ToString();
        }

        void SetPageVariables()
        {
            EscrowId = Request.QueryString.GetValue<int>("beid");
        }

        //  OnChanged of Recording Date
        protected void txtRecordDate_TextChanged(object sender, EventArgs e)
        {
            string RecordDate = txtRecordDate.Text;           
            foreach (GridViewRow row in gvRecInfo.Rows)
            {
                (row.FindControl("txtRecordingDate") as TextBox).Text = RecordDate;
                (row.FindControl("txtMortRecordingDate") as TextBox).Text = RecordDate;
                //txtRecordDate.TabIndex = 1;
               
            }
        }
        // OnChange Event for txtDeedBook
        protected void txtDeedBook_TextChanged(object sender, EventArgs e)
        {
            string DeedDate =  txtDeedBook.Text;
            foreach (GridViewRow row in gvRecInfo.Rows)
            {
                (row.FindControl("txtBook") as TextBox).Text = DeedDate;

            }
        }
        // OnChange Event for txtMortgageBook
        protected void txtMortgageBook_TextChanged(object sender, EventArgs e)
        {
            string MortgageBook = txtMortgageBook.Text;
            foreach (GridViewRow row in gvRecInfo.Rows)
            {
                (row.FindControl("txtMortBook") as TextBox).Text = MortgageBook;

            }
        }

        /// <summary>
        /// Retrieves the control that caused the postback.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private Control GetControlThatCausedPostBack(Page page)
        {
            //initialize a control and set it to null
            Control ctrl = null;

            //get the event target name and find the control
            string ctrlName = Page.Request.Params.Get("__EVENTTARGET");
            if (!String.IsNullOrEmpty(ctrlName))
                ctrl = page.FindControl(ctrlName);

            //return the control to the calling method
            return ctrl;
        }

        // TabIndex Counter
        private int _tabIndex = 3;
        public int TabIndex
        {
            get
            {
                _tabIndex++;
                return _tabIndex;
            }
        }

    }
}